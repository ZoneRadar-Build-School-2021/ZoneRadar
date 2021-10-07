using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ZoneRadar.Data;
using ZoneRadar.Models;

namespace ZoneRadar.Repositories
{
    public class ProfileRepository
    {
        private readonly ZONERadarContext _ctx;
        public ProfileRepository()
        {
            _ctx = new ZONERadarContext();
        }
        public IQueryable<Member> GetAll()
        {
            return _ctx.Member;
        }
        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
        public void Update<T>(T value) where T : class
        {
            _ctx.Entry(value).State = EntityState.Modified;
        }
    }
}