using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using System.Web.Security;
using Newtonsoft.Json;
using ZoneRadar.Utilities;
using Microsoft.AspNet.Identity;
using System.Security.Policy;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using System.Diagnostics;

namespace ZoneRadar.Services
{
    public class MemberService
    {
        private readonly ZONERadarRepository _repository;
        public MemberService()
        {
            _repository = new ZONERadarRepository();
        }

        /// <summary>
        /// 將未驗證的註冊資訊先存進資料庫(Jenny)
        /// </summary>
        /// <param name="registerVM"></param>
        /// <returns>回傳會員資訊及註冊是否成功</returns>
        public MemberResult RegisterMember(RegisterZONERadarViewModel registerVM)
        {
            var memberResult = new MemberResult
            {
                IsSuccessful = false
            };

            registerVM.Name = HttpUtility.HtmlEncode(registerVM.Name);
            registerVM.RegisterEmail = HttpUtility.HtmlEncode(registerVM.RegisterEmail);
            registerVM.RegisterPassword = HttpUtility.HtmlEncode(registerVM.RegisterPassword).MD5Hash();

            var isSameEmail = _repository.GetAll<Member>().Any(x => x.Email.ToUpper() == registerVM.RegisterEmail.ToUpper() && x.SignUpDateTime.Year != 1753);

            if (isSameEmail)
            {
                //如果已經有一樣的Email
                memberResult.ShowMessage = "已有相同的Email，請重新註冊！";
                return memberResult;
            }
            else
            {
                try
                {
                    var member = new Member
                    {
                        Email = registerVM.RegisterEmail,
                        Password = registerVM.RegisterPassword,
                        Name = registerVM.Name,
                        ReceiveEDM = false,
                        SignUpDateTime = new DateTime(1753, 1, 1), //未驗證時時間為西元1753年
                        LastLogin = new DateTime(1753, 1, 1) //未驗證時時間為西元1753年
                    };
                    _repository.Create<Member>(member);
                    _repository.SaveChanges();
                    memberResult.User = member;
                    memberResult.IsSuccessful = true;
                    return memberResult;
                }
                catch (Exception ex)
                {
                    //資料庫儲存失敗
                    memberResult.ShowMessage = "註冊資料儲存過程中遇到錯誤，請檢查您的網路或嘗試重新註冊！";
                    memberResult.Exception = ex;
                    return memberResult;
                }
            }
        }

        /// <summary>
        /// 寄送驗證信(Jenny)
        /// </summary>
        /// <param name="server"></param>
        /// <param name="request"></param>
        /// <param name="urlHelper"></param>
        /// <param name="userEmail"></param>
        public void SentEmail(HttpServerUtilityBase server, HttpRequestBase request, UrlHelper urlHelper, string userEmail)
        {
            //記錄有效的期限
            var afterTenMinutes = DateTime.Now.AddMinutes(10).ToString();
            var route = new RouteValueDictionary { { "email", userEmail }, { "expired", afterTenMinutes } };
            //製作驗證信的連結
            var verificationLink = urlHelper.Action("ConfirmEmail", "MemberCenter", route, request.Url.Scheme, request.Url.Host);

            //寄件人資訊
            string ZONERadarAccount = "zoneradar2021@gmail.com";
            string ZONERadarPassword = "@Bs202106";

            //產生能寄信的SmtpClient執行個體
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(ZONERadarAccount, ZONERadarPassword),
                EnableSsl = true
            };

            //產生信件，編輯信件的相關內容
            string verificationEmailContent = File.ReadAllText(Path.Combine(server.MapPath("~/Views/MemberCenter/VerificationEmailContent.html")));
            MailMessage mail = new MailMessage(ZONERadarAccount, userEmail)
            {
                Subject = "ZONERadar會員確認信",
                SubjectEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Body = verificationEmailContent.Replace("verificationLink", verificationLink),
                BodyEncoding = Encoding.UTF8
            };

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                //未處理
                throw new NotImplementedException();
            }
            finally
            {
                mail.Dispose();
                client.Dispose();
            }
        }

        /// <summary>
        /// 確認是否有此帳號的紀錄(包含未驗證的和已是會員的)(Jenny)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool SearchUser(string email, bool verified)
        {
            email = HttpUtility.HtmlEncode(email);
            bool hasInfo;
            if (verified)
            {
                hasInfo = _repository.GetAll<Member>().Any(x => x.Email.ToUpper() == email.ToUpper() && x.SignUpDateTime.Year != 1753);
            }
            else
            {
                hasInfo = _repository.GetAll<Member>().Any(x => x.Email.ToUpper() == email.ToUpper() && x.SignUpDateTime.Year == 1753);
            }
            return hasInfo;
        }

        /// <summary>
        /// 點擊驗證連結後做確認，是否有此會員的註冊紀錄(Jenny)
        /// </summary>
        /// <param name="email"></param>
        public MemberResult ConfirmRegister(string email)
        {
            var memberResult = new MemberResult
            {
                IsSuccessful = false
            };

            var hasThisUser = _repository.GetAll<Member>().Any(x => x.Email.ToUpper() == email.ToUpper());
            if (hasThisUser)
            {
                try
                {
                    var user = _repository.GetAll<Member>().First(x => x.Email.ToUpper() == email.ToUpper());
                    //將會員的註冊時間和登入時間改成現在時間，代表驗證成功
                    user.SignUpDateTime = DateTime.Now;
                    user.LastLogin = DateTime.Now;
                    _repository.Update(user);
                    _repository.SaveChanges();
                    memberResult.User = user;
                    memberResult.IsSuccessful = true;
                    memberResult.ShowMessage = $"{user.Name}您好，歡迎您加入ZONERadar！";
                    return memberResult;
                }
                catch (Exception ex)
                {
                    memberResult.ShowMessage = "註冊資料儲存過程中遇到錯誤，請檢查您的網路或嘗試重新註冊！";
                    memberResult.Exception = ex;
                    return memberResult;
                }
            }
            else
            {
                //找不到這個會員
                memberResult.ShowMessage = "驗證失敗，請重新註冊！";
                return memberResult;
            }
        }

        /// <summary>
        /// 比對是否有此會員(Jenny)
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns></returns>
        public MemberResult UserLogin(LoginZONERadarViewModel loginVM)
        {
            var memberResult = new MemberResult
            {
                IsSuccessful = false
            };

            //使用HtmlEncode將帳密做HTML編碼, 去除有害的字元
            loginVM.LoginEmail = HttpUtility.HtmlEncode(loginVM.LoginEmail);
            loginVM.LoginPassword = HttpUtility.HtmlEncode(loginVM.LoginPassword).MD5Hash();

            //EF比對資料庫帳密
            //以Email及Password查詢比對Member資料表記錄，且註冊時間不得為預設1753年
            var members = _repository.GetAll<Member>().ToList();
            var user = members.FirstOrDefault(x => x.Email.ToUpper() == loginVM.LoginEmail.ToUpper() && x.Password == loginVM.LoginPassword && x.SignUpDateTime.Year != 1753);

            //修改上次登入時間
            if (user != null)
            {
                try
                {
                    user.LastLogin = DateTime.Now;
                    _repository.Update(user);
                    _repository.SaveChanges();
                    memberResult.User = user;
                    memberResult.IsSuccessful = true;
                    memberResult.ShowMessage = $"{user.Name}您好，歡迎您登入ZONERadar！";
                    return memberResult;
                }
                catch (Exception ex)
                {
                    //資料庫儲存失敗
                    memberResult.ShowMessage = "讀取會員資料過程中遇到錯誤，請檢查您的網路或嘗試重新登入！";
                    memberResult.Exception = ex;
                    return memberResult;
                }
            }
            else
            {
                //沒這個會員，或是密碼輸入錯誤
                memberResult.ShowMessage = "帳號或密碼不符，請重新輸入！";
                return memberResult;
            }
        }

        /// <summary>
        /// 建造加密表單驗證票證(Jenny)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string CreateEncryptedTicket(Member user)
        {
            var userInfo = new UserInfo
            {
                MemberId = user.MemberID,
                MemberPhoto = user.Photo == null ? "https://img.88icon.com/download/jpg/20200815/cacc4178c4846c91dc1bfa1540152f93_512_512.jpg!88con" : user.Photo
            };
            var jsonUserInfo = JsonConvert.SerializeObject(userInfo);
            //建立FormsAuthenticationTicket
            var ticket = new FormsAuthenticationTicket(
            version: 1,
            name: user.MemberID.ToString(), //可以放使用者Id
            issueDate: DateTime.UtcNow,//現在UTC時間
            expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
            isPersistent: true,// 是否要記住我 true or false
            userData: jsonUserInfo, //可以放使用者角色名稱
            cookiePath: FormsAuthentication.FormsCookiePath);

            //加密Ticket
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            return encryptedTicket;
        }

        /// <summary>
        /// 建造Cookie(Jenny)
        /// </summary>
        /// <param name="encryptedTicket"></param>
        /// <param name="responseBase"></param>
        public void CreateCookie(string encryptedTicket, HttpResponseBase responseBase)
        {
            //初始化Cookie的名稱和值(將加密的表單驗證票證放進Cookie裡)
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            responseBase.Cookies.Add(cookie);
        }

        /// <summary>
        /// 取得使用者原先欲造訪的路由(Jenny)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetOriginalUrl(string userName)
        {
            var url = FormsAuthentication.GetRedirectUrl(userName, true);
            return url;
        }

        /// <summary>
        /// 寄送重設密碼信(Jenny)
        /// </summary>
        /// <param name="server"></param>
        /// <param name="request"></param>
        /// <param name="urlHelper"></param>
        /// <param name="userEmail"></param>
        public void SentResetPasswordEmail(HttpServerUtilityBase server, HttpRequestBase request, UrlHelper urlHelper, string userEmail)
        {
            userEmail = HttpUtility.HtmlEncode(userEmail);
            var afterTenMinutes = DateTime.Now.AddMinutes(10).ToString();
            var resetCode = _repository.GetAll<Member>().First(x => x.Email.ToUpper() == userEmail.ToUpper()).Password;
            var route = new RouteValueDictionary { { "email", userEmail }, { "resetCode", resetCode }, { "expired", afterTenMinutes } };
            //製作驗證信的連結
            var resetLink = urlHelper.Action("ResetPassword", "MemberCenter", route, request.Url.Scheme, request.Url.Host);

            //寄件人資訊
            string ZONERadarAccount = "zoneradar2021@gmail.com";
            string ZONERadarPassword = "@Bs202106";

            //產生能寄信的SmtpClient執行個體
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(ZONERadarAccount, ZONERadarPassword),
                EnableSsl = true
            };

            //產生信件，編輯信件的相關內容
            string resetPasswordEmailContent = File.ReadAllText(Path.Combine(server.MapPath("~/Views/MemberCenter/ResetPasswordEmailContent.html")));
            MailMessage mail = new MailMessage(ZONERadarAccount, userEmail)
            {
                Subject = "重設您的ZONERadar密碼",
                SubjectEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Body = resetPasswordEmailContent.Replace("resetLink", resetLink),
                BodyEncoding = Encoding.UTF8
            };

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                //未處理
                throw ex;
            }
            finally
            {
                mail.Dispose();
                client.Dispose();
            }
        }

        /// <summary>
        /// 驗證重設密碼連結(Jenny)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="resetCode"></param>
        /// <returns></returns>
        public Member VerifyResetPasswordLink(string email, string resetCode)
        {
            var user = _repository.GetAll<Member>().FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Password == resetCode);
            return user;
        }

        /// <summary>
        /// 修改使用者密碼(Jenny)
        /// </summary>
        /// <param name="resetPasswordVM"></param>
        /// <returns></returns>
        public MemberResult EditPassword(ResetZONERadarPasswordViewModel resetPasswordVM)
        {
            var memberResult = new MemberResult
            {
                IsSuccessful = false
            };

            //使用HtmlEncode將帳密做HTML編碼, 去除有害的字元
            resetPasswordVM.UserEmail = HttpUtility.HtmlEncode(resetPasswordVM.UserEmail);
            resetPasswordVM.NewPassword = HttpUtility.HtmlEncode(resetPasswordVM.NewPassword).MD5Hash();

            //以Email及Password查詢比對Member資料表記錄，且註冊時間不得為預設1753年
            var members = _repository.GetAll<Member>().ToList();
            var user = members.FirstOrDefault(x => x.Email.ToUpper() == resetPasswordVM.UserEmail.ToUpper() && x.SignUpDateTime.Year != 1753);

            //修改上次登入時間
            if (user != null)
            {
                try
                {
                    //改成新密碼
                    user.Password = resetPasswordVM.NewPassword;
                    user.LastLogin = DateTime.Now;
                    _repository.Update(user);
                    _repository.SaveChanges();
                    memberResult.User = user;
                    memberResult.IsSuccessful = true;
                    memberResult.ShowMessage = $"{user.Name}您好，密碼修改成功，歡迎您登入ZONERadar！";
                    return memberResult;
                }
                catch (Exception ex)
                {
                    //資料庫儲存失敗
                    memberResult.ShowMessage = "讀取會員資料過程中遇到錯誤，請檢查您的網路或重新嘗試！";
                    memberResult.Exception = ex;
                    return memberResult;
                }
            }
            else
            {
                //沒這個會員
                memberResult.ShowMessage = "找不到此會員！";
                return memberResult;
            }
        }

        /// <summary>
        /// HostInfo (Jack)
        /// </summary>
        /// <returns> 取得會員資訊 & 該會員擁有的場地 </returns> 
        public HostInfoViewModel GetMemberSpace(int? memberId)
        {

            var resultMember = new HostInfoViewModel
            {
                User = new User(),
                Spaces = new List<Spaces>()
            };
            //int memberId = 1;
            var u = _repository.GetAll<Member>().FirstOrDefault(x => x.MemberID == memberId);
            if (u == null)
            {
                return resultMember;
            }
            else
            {
                resultMember.User = new User
                {
                    Id = u.MemberID,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    Description = u.Description,
                    SignUpDateTime = u.SignUpDateTime,
                    Photo = u.Photo == null ? "https://img.88icon.com/download/jpg/20200815/cacc4178c4846c91dc1bfa1540152f93_512_512.jpg!88con" : u.Photo
                };
                //會員所擁有的廠所有場地
                var sps = _repository.GetAll<Space>().Where(x => x.MemberID == memberId && x.SpaceStatus.SpaceStatusID == 2);

                foreach (var s in sps)
                {
                    resultMember.Spaces.Add(new Spaces
                    {
                        SpaceName = s.SpaceName,
                        Address = s.Address,
                        SpacePhoto = s.SpacePhoto.First().SpacePhotoUrl,
                        District = s.District.DistrictName,
                        //sps.FirstOrDefault(x => x.SpaceID == s.SpaceID).District.DistrictName,
                        City = s.City.CityName,
                        PricePerHour = s.PricePerHour,
                        Score = Average(s.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.Score).Count()).Sum(), s.Order.Select(x=>x.Review.Where(z=>z.ToHost == true).Select(y=>y.Score).Sum()).Sum()),
                        ReviewCount = s.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.ReviewContent).Count()).Sum()
                    });
                }
                return resultMember;
            }
        }

        /// <summary>
        /// MyCollection (Jack)
        /// </summary>
        /// <returns> 取得會員資訊 & 該會員所有收藏的場地 </returns>
        public MyCollectionViewModel GetMemberCollection(int? memberId)
        {
            var time = new Stopwatch();
            time.Start();
            var resultMemberCollection = new MyCollectionViewModel
            {
                User = new User(),
                MyCollection = new List<Spaces>()
            };

            var user = _repository.GetAll<Member>().FirstOrDefault(x => x.MemberID == memberId);
            if (user == null)
            {
                return resultMemberCollection;
            }
            else
            {
                //resultMemberCollection.User = new User
                //{
                //    Id = u.MemberID,
                //    Name = u.Name,
                //    Email = u.Email,
                //    Phone = u.Phone,
                //    Description = u.Description,
                //    SignUpDateTime = u.SignUpDateTime,
                //    Photo = u.Photo == null ? "https://img.88icon.com/download/jpg/20200815/cacc4178c4846c91dc1bfa1540152f93_512_512.jpg!88con" : u.Photo
                //};
                resultMemberCollection.User = (from u in _repository.GetAll<Member>()
                                               where u.MemberID == memberId
                                               select new User
                                               {
                                                   Id = u.MemberID,
                                                   Name = u.Name,
                                                   Email = u.Email,
                                                   Phone = u.Phone,
                                                   Description = u.Description,
                                                   SignUpDateTime = u.SignUpDateTime,
                                                   Photo = u.Photo == null ? "https://img.88icon.com/download/jpg/20200815/cacc4178c4846c91dc1bfa1540152f93_512_512.jpg!88con" : u.Photo
                                               }).FirstOrDefault();
                                              
                //會員所收藏的場地
                //var collection = _repository.GetAll<Collection>().Where(x => x.MemberID == memberId);
                //var spaces = _repository.GetAll<Space>();
                //var r = _repository.GetAll<Review>();

                ////c.Member.Space.Where(x => x.SpaceID == c.SpaceID && x.SpaceStatusID == 2).FirstOrDefault().SpaceName
                //foreach (var c in collection)
                //{
                //    var sps = spaces.FirstOrDefault(x => x.SpaceID == c.SpaceID && x.SpaceStatusID == 2);
                //    resultMemberCollection.MyCollection.Add(new Spaces
                //    {
                //        SpaceName = sps.SpaceName,
                //        Address = sps.Address,
                //        SpacePhoto = sps.SpacePhoto.First().SpacePhotoUrl,
                //        District = sps.District.DistrictName,
                //        City = sps.City.CityName,
                //        PricePerHour = sps.PricePerHour,
                //        ReviewCount = sps.Order.Where(x=>sps.SpaceID == x.SpaceID).Select(x=>x.Review.Where(y=>y.ToHost == true).Select(z=>z.ReviewContent).Count()).Sum(),
                //        Score = Average(sps.Order.Where(x => sps.SpaceID == x.SpaceID).Select(x => x.Review.Where(y => y.ToHost == true).Select(z => z.ReviewContent).Count()).Sum(), sps.Order.Select(x => x.Review.Where(y => y.ToHost == true).Select(z => z.Score).Sum()).Sum())
                //        /*re.Where(x => x.Order.MemberID == s.MemberID && x.ToHost == true).Select(x => x.Score).Count()*/
                //    });
                //}
                var collection = _repository.GetAll<Collection>().Where(x => x.MemberID == memberId);
                var spaces = _repository.GetAll<Space>().Where(x => x.SpaceStatusID == 2 && collection.Select(y => y.SpaceID).Contains(x.SpaceID)).ToList();

                resultMemberCollection.MyCollection = (from s in spaces
                                                      select new Spaces
                                                      {
                                                        SpaceName = s.SpaceName,
                                                        Address = s.Address,
                                                        City  = s.City.CityName,
                                                        District = s.District.DistrictName,
                                                        PricePerHour = s.PricePerHour,
                                                        ReviewCount = s.Order.Select(x => x.Review.Where(y => y.ToHost == true && y.Order.SpaceID == s.SpaceID).Select(z => z.ReviewContent).Count()).Sum(),
                                                        Score = Average(s.Order.Select(x => x.Review.Where(y => y.ToHost == true && y.Order.SpaceID == s.SpaceID).Select(z => z.ReviewContent).Count()).Sum(), s.Order.Select(x => x.Review.Where(y => y.ToHost == true).Select(z => z.Score).Sum()).Sum())
                                                      }).ToList();

                //foreach (var s in spaces)
                //{
                //    resultMemberCollection.MyCollection.Add(new Spaces
                //    {
                //        SpaceName = s.SpaceName,
                //        Address = s.Address,
                //        City = s.City.CityName,
                //        District = s.District.DistrictName,
                //        SpacePhoto = s.SpacePhoto.First().SpacePhotoUrl,
                //        PricePerHour = s.PricePerHour,
                //        ReviewCount = s.Order.Select(x => x.Review.Where(y => y.ToHost == true && y.Order.SpaceID == s.SpaceID).Select(z=>z.ReviewContent).Count()).Sum(),
                //        Score = Average(s.Order.Select(x => x.Review.Where(y => y.ToHost == true && y.Order.SpaceID == s.SpaceID).Select(z => z.ReviewContent).Count()).Sum() , s.Order.Select(x => x.Review.Where(y => y.ToHost == true).Select(z => z.Score).Sum()).Sum())
                //    });
                //}

                time.Stop();
                return resultMemberCollection;
            }

        }
        /// <summary>
        /// Space平均評分 (Jack)
        /// </summar>
        /// <returns> 取得會員資訊 & 該會員所有被場地主的評價 </returns>
        private static int Average(int count,int score)
        {
            var result = 0;
            if ( count == 0 ||  score == 0) 
            {
                result = 0;
            }
            else
            {
                result = (int)score / count;
            }
            return result;
        }

        /// <summary>
        /// HostTpMemberReview (Jack)
        /// </summar>
        /// <returns> 取得會員資訊 & 該會員所有被場地主的評價 </returns>
        public UserInfoViewModel GetHostReview(int? memberId)
        {
            var resulthostinfoReview = new UserInfoViewModel
            {
                User = new User(),
                ToUserReview = new List<UserReview>()
            };

            var u = _repository.GetAll<Member>().FirstOrDefault(x => x.MemberID == memberId);
            if (u == null)
            {
                return resulthostinfoReview;
            }
            else
            {
                //找出會員
                resulthostinfoReview.User = new User
                {
                    Id = u.MemberID,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    Description = u.Description,
                    SignUpDateTime = u.SignUpDateTime,
                    Photo = u.Photo == null ? "https://img.88icon.com/download/jpg/20200815/cacc4178c4846c91dc1bfa1540152f93_512_512.jpg!88con" : u.Photo
                };
                //找出會員是否有租借場地並且顯示 出被場地主的評價
                var order = _repository.GetAll<Order>().Where(x => x.MemberID == u.MemberID && x.OrderStatusID == 4);
                
                foreach (var o in order)
                {
                    var or = o.Review.FirstOrDefault(x => x.OrderID == o.OrderID && x.ToHost == false);
                    resulthostinfoReview.ToUserReview.Add(new UserReview
                    {
                        SpaceName = o.Space.SpaceName,
                        /*sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).SpaceName,*/
                        SpaceMemberPhoto = o.Space.Member.Photo  == null ? "https://img.88icon.com/download/jpg/20200815/cacc4178c4846c91dc1bfa1540152f93_512_512.jpg!88con" : o.Space.Member.Photo,
                        District = o.Space.District.DistrictName,
                        Address = o.Space.Address,
                        PricePerHour = o.Space.PricePerHour,
                        ReviewContent = or.ReviewContent,
                        Recommend = or.Recommend,
                        Score = or.Score,
                        ReviewDate = or.ReviewDate,
                        ReviewCount = o.Review.Where(x => x.OrderID == o.OrderID && x.ToHost == false).Count(),
                        Name = o.Space.Member.Name,
                        Id = o.Space.MemberID
                    });

                    return resulthostinfoReview;
                }
                return resulthostinfoReview;
            }
        }

        /// <summary>
        /// 讀取個人資料(昶安)
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public ProfileViewModel GetProfileData(int memberID)
        {
            var p = _repository.GetAll<Member>().ToList().FirstOrDefault(x => x.MemberID == memberID);
            var result = new ProfileViewModel()
            {
                MemberID = p.MemberID,
                //Photo = p.Photo,
                Name = p.Name,
                Phone = p.Phone,
                Email = p.Email,
                Description = p.Description
            };

            return result;
        }

        /// <summary>
        /// 編輯個人資料(昶安)
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        public ProfileViewModel EditProfileData(ProfileViewModel edit)
        {
            //抓取 --> 編輯資料
            var p = _repository.GetAll<Member>().ToList().FirstOrDefault(x => x.MemberID == edit.MemberID);
            //p.Photo = edit.Photo;
            p.Name = edit.Name;
            p.Phone = edit.Phone;
            p.Description = edit.Description;

            //更新
            _repository.Update(p);
            _repository.SaveChanges();

            return edit;
        }
    }
}