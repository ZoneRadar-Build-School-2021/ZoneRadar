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

            var isSameEmail = _repository.GetAll<Member>().Any(x => x.Email.ToUpper() == registerVM.RegisterEmail.ToUpper() && x.IsVerify);

            if (isSameEmail)
            {
                //如果已經有一樣的Email
                memberResult.ShowMessage = "該Email已有人使用，請重新註冊！";
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
                        Photo = "https://res.cloudinary.com/dt6vz3pav/image/upload/v1636172646/court/user-profile_pdbu9q.png",
                        Name = registerVM.Name,
                        ReceiveEDM = false,
                        SignUpDateTime = DateTime.UtcNow,
                        LastLogin = DateTime.UtcNow,
                        IsVerify = false,
                        IsGoogleLogin = false
                    };
                    _repository.Create(member);
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
        /// 產生驗證連結(Jenny)
        /// </summary>
        /// <param name="generateLink"></param>
        /// <returns></returns>
        public string GenerateVerifyLink(GenerateLink generateLink)
        {
            //記錄有效的期限
            var afterTenMinutes = DateTime.UtcNow.AddMinutes(10).ToString();
            var route = new RouteValueDictionary { { "email", generateLink.UserEmail }, { "expired", afterTenMinutes } };
            //製作驗證信的連結
            var verificationLink = generateLink.UrlHelper.Action("ConfirmEmail", "MemberCenter", route, generateLink.Request.Url.Scheme, generateLink.Request.Url.Host);
            return verificationLink;
        }

        /// <summary>
        /// 產生重設密碼連結(Jenny)
        /// </summary>
        /// <param name="generateLink"></param>
        /// <returns></returns>
        public string GenerateResetPasswordLink(GenerateLink generateLink)
        {
            generateLink.UserEmail = HttpUtility.HtmlEncode(generateLink.UserEmail);
            //記錄有效的期限
            var afterTenMinutes = DateTime.UtcNow.AddMinutes(10).ToString();
            //取得亂數
            var resetCode = _repository.GetAll<Member>().First(x => x.Email.ToUpper() == generateLink.UserEmail.ToUpper()).Password;
            var route = new RouteValueDictionary { { "email", generateLink.UserEmail }, { "resetCode", resetCode }, { "expired", afterTenMinutes } };
            //製作驗證信的連結
            var verificationLink = generateLink.UrlHelper.Action("ResetPassword", "MemberCenter", route, generateLink.Request.Url.Scheme, generateLink.Request.Url.Host);
            return verificationLink;
        }

        /// <summary>
        /// 寄信(Jenny)
        /// </summary>
        /// <param name="emailContent"></param>
        public void SentEmail(EmailContent emailContent)
        {
            //寄件人資訊
            string ZONERadarAccount = "zoneradar2021@gmail.com";
            string ZONERadarPassword = "aqfawvgueskwtdqd";

            //產生能寄信的SmtpClient執行個體
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(ZONERadarAccount, ZONERadarPassword),
                EnableSsl = true
            };

            //產生信件，編輯信件的相關內容
            string verificationEmailContent = File.ReadAllText(Path.Combine(emailContent.Server.MapPath($"~/Views/MemberCenter/{emailContent.EmailContentFileName}.html")));
            MailMessage mail = new MailMessage(ZONERadarAccount, emailContent.UserEmail)
            {
                Subject = emailContent.EmailSubject,
                SubjectEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Body = verificationEmailContent.Replace(emailContent.OldLink, emailContent.NewLink),
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
        public bool IsUser(string email, bool verified)
        {
            email = HttpUtility.HtmlEncode(email);
            bool hasInfo = _repository.GetAll<Member>().Any(x => x.Email.ToUpper() == email.ToUpper() && x.IsVerify == verified);
            return hasInfo;
        }

        /// <summary>
        /// 確認原始網站是否有此Gmail帳號、或已綁定GoogleLoginID(Jenny)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsGoogleUser(string email, string googleId, bool verified)
        {
            email = HttpUtility.HtmlEncode(email);
            bool isGoogleUser = false;
            //是否有此gmail
            bool hasGmail = _repository.GetAll<Member>().Any(x => x.Email.ToUpper() == email.ToUpper() && x.IsVerify == verified);
            //是否有此GoogleID
            bool hasGoogleLoginId = _repository.GetAll<Member>().Any(x => x.GoogleID == googleId && x.IsVerify == verified);
            if (hasGmail || hasGoogleLoginId)
            {
                isGoogleUser = true;
            }
            return isGoogleUser;
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
                    var users = _repository.GetAll<Member>().Where(x => x.Email.ToUpper() == email.ToUpper());
                    var maxSignUpTime = users.Max(x => x.SignUpDateTime.ToString());
                    var user = users.First(x => x.SignUpDateTime.ToString() == maxSignUpTime);
                    //將會員的註冊時間和登入時間改成現在時間，代表驗證成功
                    user.SignUpDateTime = DateTime.UtcNow;
                    user.LastLogin = DateTime.UtcNow;
                    user.IsVerify = true;
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
            var user = members.FirstOrDefault(x => x.Email.ToUpper() == loginVM.LoginEmail.ToUpper() && x.Password == loginVM.LoginPassword && x.IsVerify == true);

            //修改上次登入時間
            if (user != null)
            {
                try
                {
                    user.LastLogin = DateTime.UtcNow;
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
                MemberPhoto = user.Photo
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
            var afterTenMinutes = DateTime.UtcNow.AddMinutes(10).ToString();
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
            var user = members.FirstOrDefault(x => x.Email.ToUpper() == resetPasswordVM.UserEmail.ToUpper() && x.IsVerify == true);

            //修改上次登入時間
            if (user != null)
            {
                try
                {
                    //改成新密碼
                    user.Password = resetPasswordVM.NewPassword;
                    user.LastLogin = DateTime.UtcNow;
                    _repository.Update(user);
                    _repository.SaveChanges();
                    memberResult.User = user;
                    memberResult.IsSuccessful = true;
                    memberResult.ShowMessage = $"{user.Name}您好，密碼修改成功！";
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
        /// 綁定Google(Jenny)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="googleId"></param>
        /// <returns></returns>
        public Member BindGoogle(string email, string googleId)
        {
            var user = _repository.GetAll<Member>().First(x => x.Email.ToUpper() == email.ToUpper() && x.IsVerify);
            if (user.GoogleID == null)
            {
                user.GoogleID = googleId;
            }
            user.IsGoogleLogin = true;
            user.LastLogin = DateTime.UtcNow;
            _repository.Update(user);
            _repository.SaveChanges();
            return user;
        }

        /// <summary>
        /// 以Gmail註冊為會員(Jenny)
        /// </summary>
        /// <param name="registerWithGoogle"></param>
        /// <returns></returns>
        public Member RegisterWithGoogle(RegisterWithGoogle registerWithGoogle)
        {
            string password = "jwiejdlwkjldzdqqsq2323"; //一組亂數
            var user = new Member
            {
                Email = registerWithGoogle.GoogleEmail,
                Password = password,
                Photo = registerWithGoogle.GooglePhoto,
                Name = registerWithGoogle.GoogleName,
                ReceiveEDM = false,
                SignUpDateTime = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow,
                IsVerify = true,
                GoogleID = registerWithGoogle.GoogleId,
                IsGoogleLogin = true
            };
            _repository.Create(user);
            _repository.SaveChanges();
            return user;
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
                        SpaceId = s.SpaceID,
                        SpaceName = s.SpaceName,
                        Address = s.Address,
                        SpacePhoto = s.SpacePhoto.First().SpacePhotoUrl,
                        District = s.District.DistrictName,
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
            var resultMemberCollection = new MyCollectionViewModel
            {
                User = new User(),
                MyCollection = new List<Spaces>()
            };

            var u = _repository.GetAll<Member>().FirstOrDefault(x => x.MemberID == memberId);
            if (u == null)
            {
                return resultMemberCollection;
            }
            else
            {
                //該使用者
                resultMemberCollection.User = new User
                {
                    Id = u.MemberID,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    Description = u.Description,
                    SignUpDateTime = u.SignUpDateTime,
                    Photo = u.Photo == null ? "https://img.88icon.com/download/jpg/20200815/cacc4178c4846c91dc1bfa1540152f93_512_512.jpg!88con" : u.Photo
                };

                //會員所收藏的場地
                var collection = _repository.GetAll<Collection>().Where(x => x.MemberID == memberId).Select(x=>x.SpaceID);
                var spaces = _repository.GetAll<Space>().Where(x=> x.SpaceStatusID == 2 &&  collection.Contains(x.SpaceID));
                foreach (var sps in spaces)
                {
                    resultMemberCollection.MyCollection.Add(new Spaces
                    {
                        SpaceId = sps.SpaceID,
                        SpaceName = sps.SpaceName,
                        Address = sps.Address,
                        SpacePhoto = sps.SpacePhoto.First().SpacePhotoUrl,
                        District = sps.District.DistrictName,
                        City = sps.City.CityName,
                        PricePerHour = sps.PricePerHour,
                        ReviewCount = sps.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.Score).Count()).Sum(),
                        Score = Average(sps.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.Score).Count()).Sum(), sps.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.Score).Sum()).Sum())
                    });
                }
                return resultMemberCollection;
            }

        }


        /// <summary>
        /// Space平均評分 (Jack)
        /// </summar>
        /// <returns> 取得會員資訊 & 該會員所有被場地主的評價 </returns>
        private static double Average(int count, int score)
        {
            double result = 0;
            if (count == 0 || score == 0)
            {
                result = 0;
            }
            else
            {
                result = score / count;
            }
            return result;
        }

        /// <summary>
        /// HostToMemberReview (Jack)
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
                var review = _repository.GetAll<Review>().Where(x => x.ToHost == false && x.Order.MemberID == u.MemberID && x.Order.OrderStatusID == 4);

                foreach (var r in review) 
                {
                    resulthostinfoReview.ToUserReview.Add(new UserReview {
                            SpaceId = r.Order.SpaceID,
                            SpaceName = r.Order.Space.SpaceName,
                            District = r.Order.Space.District.DistrictName,
                            Address = r.Order.Space.Address,
                            PricePerHour = r.Order.Space.PricePerHour,
                            ReviewDate = r.ReviewDate,
                            ReviewContent = r.ReviewContent,
                            ReviewCount = r.ReviewContent.Count(),
                            Recommend  = r.Recommend,
                            SpaceMemberPhoto = r.Order.Space.Member.Photo,
                            Name = r.Order.Space.Member.Name,
                            Id = r.Order.Space.MemberID
                        });
                    
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
            var p = _repository.GetAll<Member>().First(x => x.MemberID == memberID);
            var result = new ProfileViewModel()
            {
                MemberID = p.MemberID,
                Photo = p.Photo == null ? "https://res.cloudinary.com/dt6vz3pav/image/upload/v1636172646/court/user-profile_pdbu9q.png" : p.Photo,
                Name = p.Name,
                Phone = p.Phone,
                Email = p.Email,
                Description = p.Description,
                ReceiveEDM = p.ReceiveEDM
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
            var o = _repository.GetAll<Member>().First(x => x.MemberID == edit.MemberID);
            o.Photo = edit.Photo;
            o.Name = edit.Name;
            o.Phone = edit.Phone;
            o.Description = edit.Description;
            o.ReceiveEDM = edit.ReceiveEDM;

            //更新
            _repository.Update(o);
            _repository.SaveChanges();
            _repository.Dispose();

            return edit;
        }

        /// <summary>
        /// 取得大頭照片(昶安)
        /// </summary>
        /// <returns></returns>
        public ProfileImgViewModel GetProfilePhotoFromDB(int memberID)
        {
            var url = _repository.GetAll<Member>().First(x => x.MemberID == memberID).Photo;

            var result = new ProfileImgViewModel()
            {
                Name = "dt6vz3pav",
                Preset = "yp7sicxt",
                ProfileImgUrl = url.ToString(),
            };

            return result;
        }

        /// <summary>
        /// 儲存照片至資料庫+移除大頭照(昶安)
        /// </summary>
        /// <param name="SaveProfileImgVM"></param>
        public void ReflashProfilePhotoFromDB(SaveProfileImgViewModel SaveProfileImgVM)
        {
            var profileimg = _repository.GetAll<Member>().First(x => x.MemberID == SaveProfileImgVM.MemberID);
            var imgurl = SaveProfileImgVM.ProfileImgUrl;

            profileimg.Photo = imgurl;

            _repository.Update(profileimg);
            _repository.SaveChanges();
            _repository.Dispose();
        }
    }
}