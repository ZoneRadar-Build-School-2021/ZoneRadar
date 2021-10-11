using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using ZoneRadar.Models;
using System.Web.Security;
using Newtonsoft.Json;
using ZoneRadar.Utilities;
using Microsoft.AspNet.Identity;
using System.Security.Policy;
using System.Net.Mail;
using System.Net;
using System.Text;

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
                    Photo = u.Photo
                };
                //會員所擁有的廠所有場地
                var sps = _repository.GetAll<Space>().Where(x => x.MemberID == memberId);
                var re = _repository.GetAll<Review>();
                var spt = _repository.GetAll<SpacePhoto>();

                foreach (var s in sps)
                {
                    resultMember.Spaces.Add(new Spaces
                    {
                        SpaceName = s.SpaceName,
                        Address = s.Address,
                        SpacePhoto = sps.FirstOrDefault(x => x.SpaceID == s.SpaceID).SpacePhoto.First().SpacePhotoUrl,
                        District = sps.FirstOrDefault(x => x.SpaceID == s.SpaceID).District.DistrictName,
                        City = sps.FirstOrDefault(x => x.SpaceID == s.SpaceID).City.CityName,
                        PricePerHour = s.PricePerHour,
                        ReviewCount = re.Where(x => x.Order.MemberID == s.MemberID && x.ToHost == true).Select(x => x.Score).Count()
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
                resultMemberCollection.User = new User
                {
                    Id = u.MemberID,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    Description = u.Description,
                    SignUpDateTime = u.SignUpDateTime,
                    Photo = u.Photo
                };
                //會員所收藏的場地
                var collection = _repository.GetAll<Collection>().Where(x => x.MemberID == memberId);
                var sps = _repository.GetAll<Space>();

                foreach (var c in collection)
                {
                    resultMemberCollection.MyCollection.Add(new Spaces
                    {
                        SpaceName = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).SpaceName,
                        Address = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).Address,
                        SpacePhoto = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).SpacePhoto.First().SpacePhotoUrl,
                        District = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).District.DistrictName,
                        City = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).City.CityName,
                        PricePerHour = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).PricePerHour,
                        ReviewCount = sps.FirstOrDefault(x => x.SpaceID == c.SpaceID).Order.Select(x => x.Review).Count()
                        /*re.Where(x => x.Order.MemberID == s.MemberID && x.ToHost == true).Select(x => x.Score).Count()*/
                    });
                }
                return resultMemberCollection;
            }

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
                    Photo = u.Photo
                };
                //找出會員是否有租借場地並且顯示 出被場地主的評價
                var order = _repository.GetAll<Order>().Where(x => x.MemberID == u.MemberID && x.OrderStatusID == 4);
                var sps = _repository.GetAll<Space>();
                foreach (var o in order)
                {
                    resulthostinfoReview.ToUserReview.Add(new UserReview
                    {
                        SpaceName = sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).SpaceName,
                        SpacePhoto = sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).Member.Photo,
                        District = sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).District.DistrictName,
                        Address = sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).Address,
                        PricePerHour = sps.FirstOrDefault(x => x.SpaceID == o.SpaceID).PricePerHour,
                        ReviewContent = o.Review.FirstOrDefault(x => x.OrderID == o.OrderID && x.ToHost == false).ReviewContent,
                        Recommend = o.Review.FirstOrDefault(x => x.OrderID == o.OrderID && x.ToHost == false).Recommend,
                        Score = o.Review.FirstOrDefault(x => x.OrderID == o.OrderID && x.ToHost == false).Score,
                        ReviewDate = o.Review.FirstOrDefault(x => x.OrderID == o.OrderID && x.ToHost == false).ReviewDate,
                        ReviewCount = o.Review.Where(x => x.OrderID == o.OrderID && x.ToHost == false).Count()
                    });

                    return resulthostinfoReview;
                }
                return resulthostinfoReview;
            }
        }

        public void SentEmail()
        {
            string account = "testing@gmail.com";
            string Password = "test123";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential(account, Password);
            client.EnableSsl = true;

            MailMessage mail = new MailMessage(account, "test1@test.com");
            mail.Subject = "測試信";
            mail.SubjectEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Body = "第一行<br> 第二行<br>第三行<br>";
            mail.BodyEncoding = Encoding.UTF8;

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mail.Dispose();
                client.Dispose();
            }
        }


        /// <summary>
        /// 註冊會員
        /// </summary>
        /// <param name="registerVM"></param>
        /// <returns>回傳會員資訊及註冊是否成功</returns>
        public RegisterResult RegisterMember(RegisterZONERadarViewModel registerVM)
        {
            var registerResult = new RegisterResult
            {
                User = null,
                IsSuccessful = false
            };

            var isSamePassword = registerVM.Password == registerVM.ConfirmPassword;

            registerVM.Name = HttpUtility.HtmlEncode(registerVM.Name);
            registerVM.Email = HttpUtility.HtmlEncode(registerVM.Email);
            registerVM.Password = HttpUtility.HtmlEncode(registerVM.Password).MD5Hash();

            var isSameEmail = _repository.GetAll<Member>().Any(x => x.Email.ToUpper() == registerVM.Email.ToUpper());

            if (isSameEmail || !isSamePassword || registerVM == null)
            {
                return registerResult;
            }
            else
            {
                var member = new Member
                {
                    Email = registerVM.Email,
                    Password = registerVM.Password,
                    Name = registerVM.Name,
                    ReceiveEDM = false,
                    SignUpDateTime = DateTime.Now,
                    LastLogin = DateTime.Now
                };
                _repository.Create<Member>(member);
                _repository.SaveChanges();
                registerResult.User = member;
                registerResult.IsSuccessful = true;
                return registerResult;
            }           
        }
        /// <summary>
        /// 比對是否有此會員
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns></returns>
        public Member UserLogin(LoginZONERadarViewModel loginVM)
        {          
            //使用HtmlEncode將帳密做HTML編碼, 去除有害的字元
            loginVM.Email = HttpUtility.HtmlEncode(loginVM.Email);
            loginVM.Password = HttpUtility.HtmlEncode(loginVM.Password).MD5Hash();

            //EF比對資料庫帳密
            //以Email及Password查詢比對Member資料表記錄
            var members = _repository.GetAll<Member>().ToList();
            var user = members.SingleOrDefault(x => x.Email.ToUpper() == loginVM.Email.ToUpper() && x.Password == loginVM.Password);

            //修改上次登入時間
            if(user != null)
            {
                user.LastLogin = DateTime.Now;
                _repository.Update(user);
                _repository.SaveChanges();
            }

            return user;
        }

        /// <summary>
        /// 建造加密表單驗證票證
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
        /// 建造Cookie
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
        /// 取得使用者原先欲造訪的路由
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetReturnUrl(string userName)
        {
            var url = FormsAuthentication.GetRedirectUrl(userName, true);
            return url;
        }
    }
}