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
        public List<ProfileViewModel> GetProfileVMData()
        {
            List<Member> profiledata = _repo.GetProfileData();

            List<ProfileViewModel> profileVMList = new List<ProfileViewModel>();

            foreach (var m in profiledata)
            {
                profileVMList.Add(new ProfileViewModel 
                { 
                    Photo= m.Photo, 
                    Name = m.Name, 
                    Phone = m.Phone, 
                    Email = m.Email, 
                    Description = m.Description 
                });
            }

            return profileVMList;
        }
    }
}