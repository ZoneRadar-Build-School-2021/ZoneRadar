using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ZoneRadar.Data;

namespace ZoneRadar.Repositories
{
    public class ZONERadarRepository
    {
        private readonly ZONERadarContext _context;

        public ZONERadarRepository()
        {
            _context = new ZONERadarContext();
        }
        public IQueryable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }
        public void Create<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Added;
        }
        public void CreateRange<T>(IEnumerable<T> value) where T : class
        {
            _context.Set<T>().AddRange(value);
        }
        public void Update<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Modified;
        }
        public void Delete<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Deleted;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}