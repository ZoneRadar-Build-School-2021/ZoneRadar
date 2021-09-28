using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;

namespace ZoneRadar.Repositories
{
    public class ProfileRepository
    {
        private readonly ZoneRadarContext _ctx;
        public ProfileRepository()
        {
            _ctx = new ZoneRadarContext();
        }
        public IQueryable<T> GetAll<T>() where T : class
        {
            return _ctx.Set<T>();
        }
        public List<Member> GetProfileData()
        {
            return _ctx.Member.ToList();
        }
    }
}