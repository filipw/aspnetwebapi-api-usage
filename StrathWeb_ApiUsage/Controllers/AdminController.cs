using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using StrathWeb_ApiUsage.Models;

namespace StrathWeb_ApiUsage.Controllers
{
    public class AdminController : ApiController
    {
        private IApiUsageRepository _usage;

        public AdminController()
        {
            _usage = new ApiUsageRepository();
        }

        public IEnumerable<WebApiUsage> Get()
        {
            return _usage.GetAll();
        }

        public IEnumerable<WebApiUsage> Get(string key)
        {
            return _usage.GetAll(key);
        }

        public WebApiUsage get(int id)
        {
            return _usage.Get(id);
        }

    }
}
