using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using ZoneRadar.Models;
using ZoneRadar.Repositories;
using ZoneRadar.ViewModels;

namespace ZoneRadar.Services
{
    public class ProfileService
    {
        private readonly ProfileRepository _repo;
        public ProfileService()
        {
            _repo = new ProfileRepository();        
        }
        public ProfileViewModel GetProfileData(int memberID)
        {
            var p = _repo.GetAll().ToList().FirstOrDefault(x => x.MemberID == memberID);
            var result = new ProfileViewModel() 
            {
                MemberID = p.MemberID,
                Photo = p.Photo,
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
        public EditResult EditProfileData(ProfileViewModel edit)
        {
            var result = new EditResult();
            try
            {
                //抓取 --> 編輯資料
                var p = _repo.GetAll().ToList().FirstOrDefault(x => x.MemberID == edit.MemberID);
                p.Photo = edit.Photo;
                p.Name = edit.Name;
                p.Phone = edit.Phone;
                p.Description = edit.Description;

                //更新
                _repo.Update(p);
                _repo.SaveChanges();
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
            return result;
        }
    }
}