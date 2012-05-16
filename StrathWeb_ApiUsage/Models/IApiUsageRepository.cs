using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrathWeb_ApiUsage
{
    public interface IApiUsageRepository
    {
        IEnumerable<WebApiUsage> GetAll();
        WebApiUsage Get(int id);
        IEnumerable<WebApiUsage> GetAll(string key);
        WebApiUsage Add(WebApiUsage au);
    }
}