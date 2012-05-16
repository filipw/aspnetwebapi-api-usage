using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Runtime.Serialization;

namespace StrathWeb_ApiUsage
{
    [DataContract]
    [KnownType(typeof(WebApiUsageRequest))]
    [KnownType(typeof(WebApiUsageResponse))]
    public class WebApiUsage
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string ApiKey { get; set; }
        [DataMember]
        public DateTime Timestamp { get; set; }
        [DataMember]
        public string UsageType { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public Dictionary<string, string> Headers { get; set; }

        protected void extractHeaders(HttpHeaders h)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var i in h.ToList())
            {
                if (i.Value != null)
                {
                    string header = string.Empty;
                    foreach (var j in i.Value)
                    {
                        header += j + " ";
                    }
                    dict.Add(i.Key, header);
                }
            }
            Headers = dict;
        }
    }

    [DataContract]
    public class WebApiUsageRequest : WebApiUsage
    {
        [DataMember]
        public string Uri { get; set; }
        [DataMember]
        public string RequestMethod { get; set; }
        [DataMember]
        public string IP { get; set; }

        public WebApiUsageRequest(HttpRequestMessage request, string apikey)
        {
            if (request != null)
            {
                UsageType = request.GetType().Name;
                RequestMethod = request.Method.Method;
                Uri = request.RequestUri.ToString();
                IP = ((HttpContextBase)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                ApiKey = apikey;
                Timestamp = DateTime.Now;
                base.extractHeaders(request.Headers);
            }
            else
            {
                throw new ArgumentNullException("request cannot be null");
            }
        }
    }

    [DataContract]
    public class WebApiUsageResponse : WebApiUsage
    {
        [DataMember]
        public int StatusCode { get; set; }
        
        public WebApiUsageResponse(HttpResponseMessage response, string apikey)
        {
            if (response != null)
            {
                UsageType = response.GetType().Name;
                StatusCode = Convert.ToInt32(response.StatusCode);
                base.extractHeaders(response.Headers);
                Timestamp = DateTime.Now;
                ApiKey = apikey;
            }
            else
            {
                throw new ArgumentNullException("response cannot be null");
            }
        }
    }
}