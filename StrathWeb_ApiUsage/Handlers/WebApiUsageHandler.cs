using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Web.Http;
using StrathWeb_ApiUsage.Models;

namespace StrathWeb_ApiUsage
{
    public class WebApiUsageHandler : DelegatingHandler
    {
        private static readonly IApiUsageRepository _repo = new ApiUsageRepository();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string apikey = HttpUtility.ParseQueryString(request.RequestUri.Query).Get("apikey");

            var apiRequest = new WebApiUsageRequest(request, apikey);
            request.Content.ReadAsStringAsync().ContinueWith(t =>
            {
                apiRequest.Content = t.Result;
                _repo.Add(apiRequest);
            });

            return base.SendAsync(request, cancellationToken).ContinueWith(
                task =>
                {
                    var apiResponse = new WebApiUsageResponse(task.Result, apikey);
                    apiResponse.Content = task.Result.Content.ReadAsStringAsync().Result;
                    _repo.Add(apiResponse);
                    return task.Result;
                }
            );
        }
    }
}