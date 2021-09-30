using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ProfileViewModel GetProfileData()
        {
            int memberID = 28;
            var p = _repo.GetAll().ToList().FirstOrDefault(x => x.MemberID == memberID);
            var result = new ProfileViewModel() 
            {
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
    }
}