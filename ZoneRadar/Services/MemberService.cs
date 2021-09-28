using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using ZoneRadar.Models;

namespace ZoneRadar.Services
{
    public class MemberService
    {
        private readonly ZONERadarRepository _repository;
        public MemberService()
        {
            _repository = new ZONERadarRepository();
        }
        public bool RegisterMember(RegisterZONERadarViewModel registerVM)
        {
            var isSameEmail = _repository.GetAll<Member>().Any(x => x.Email == registerVM.Email);
            if (isSameEmail)
            {
                return false;
            }
            else
            {
                var member = new Member
                {
                    Email = registerVM.Email,
                    Password = registerVM.Password,
                    Name = registerVM.Name,
                    ReceiveEDM = false,
                    SignUpDateTime = DateTime.UtcNow,
                    LastLogin = DateTime.UtcNow
                };
                _repository.Create<Member>(member);
                _repository.SaveChanges();
                return true;
            }           
        }
    }
}