using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using ZoneRadar.Models;
using ZoneRadar.Repositories;
using ZoneRadar.Models.ViewModels;
using System.Web.Mvc;

namespace ZoneRadar.Services
{
    public class ProfileService
    {
        private readonly ZONERadarRepository _repo;
        public ProfileService()
        {
            _repo = new ZONERadarRepository();        
        }

        public ProfileViewModel GetProfileData(int memberID)
        {
            var p = _repo.GetAll().ToList().FirstOrDefault(x => x.MemberID == memberID);
            var result = new ProfileViewModel() 
            {
                MemberID = p.MemberID,
                //Photo = p.Photo,
                Name = p.Name,
                Phone = p.Phone,
                Email = p.Email,
                Description = p.Description
            };

            /*
            var profiledata = _repo.GetAll().Where(x => x.MemberID == 1).ToList();
            foreach (var m in profiledata)
            {
                var profileVM = new ProfileViewModel 
                { 
                    Photo= m.Photo, 
                    Name = m.Name, 
                    Phone = m.Phone, 
                    Email = m.Email, 
                    Description = m.Description 
                };
                result = profileVM;
            }
            */
            return result;
        }

        public ProfileViewModel EditProfileData(Member edit)
        {
            var result = new ProfileViewModel();
            
            //抓取 --> 編輯資料
            var p = _repo.GetAll().ToList().FirstOrDefault(x => x.MemberID == edit.MemberID);
            //p.Photo = edit.Photo;
            p.Name = edit.Name;
            p.Phone = edit.Phone;
            p.Description = edit.Description;
            //更新
            _repo.Update(p);
            _repo.SaveChanges();

            return result;
        }
    }
}