using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;

namespace StrathWeb_ApiUsage.Models
{
    public class ApiUsageRepository : IApiUsageRepository
    {
        private int _nextId = 1;
        private static ConcurrentQueue<WebApiUsage> _aus = new ConcurrentQueue<WebApiUsage>();

        public IEnumerable<WebApiUsage> GetAll()
        {
            return _aus.AsQueryable();
        }

        public WebApiUsage Get(int id)
        {
            return _aus.ToList().Find(i => i.id == id);
        }

        public IEnumerable<WebApiUsage> GetAll(string key)
        {
            return _aus.ToList().FindAll(i => i.ApiKey == key);
        }

        public WebApiUsage Add(WebApiUsage aus)
        {
            aus.id = _nextId++;
            _aus.Enqueue(aus);
            return aus;
        }
    }
}