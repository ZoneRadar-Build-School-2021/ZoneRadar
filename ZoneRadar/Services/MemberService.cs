﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using ZoneRadar.Models;
using System.Web.Security;
using Newtonsoft.Json;
using ZoneRadar.Utilities;

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
        /// 註冊會員
        /// </summary>
        /// <param name="registerVM"></param>
        /// <returns>回傳會員資訊及註冊是否成功</returns>
        public RegisterResult RegisterMember(RegisterZONERadarViewModel registerVM)
        {
            var registerResult = new RegisterResult
            {
                user = null,
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
                registerResult.user = member;
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
            name: user.Name.ToString(), //可以放使用者Id
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
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetUrl(Member user)
        {
            var url = FormsAuthentication.GetRedirectUrl(user.Name, true);
            return url;
        }
    }
}