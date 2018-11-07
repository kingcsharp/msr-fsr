using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.Users;
using Answer.Web.Controllers;
using Answer.Web.Filters;
using RestSharp;

namespace Msr.Web.Controllers
{
    [AuthorizeUser]
    public class ReportController : BaseController
    {

        //[HttpGet,Route("reports/{reportID}")]
        public ActionResult Index()
        {
            ViewBag.ActiveClass = "WIP";

            //var loggedUser = User.Identity.GetUserId();

            //var userService = new UserService();
            //var loggedIn = WidestageLogin();
            //var company = userService.GetCompanyId(loggedUser);

            //ViewBag.ClientName = company.Name;

            return View();
        }
        //Report/WidestageLogin
        public ActionResult WidestageLogin()
        {
            return WidestageLoginResult();
        }


        public ActionResult Dashboardsv2get(string id, string session, string sessionsig, string data)
        {
            var client = new RestClient("http://reports.msr-fsr.com/api/dashboardsv2/get/" + id + "?data=" + HttpUtility.UrlEncode(data));
            var request = new RestRequest(Method.GET);
            var cookies = Request.Cookies;
            var machineId = cookies["mongoMachineId"].Value;
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("mongoMachineId", machineId, ParameterType.Cookie);
            request.AddParameter("session", session, ParameterType.Cookie);
            request.AddParameter("session.sig", sessionsig, ParameterType.Cookie);

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://reports.msr-fsr.com/login");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            request.AddHeader("accept", "application/json, text/plain, */*");

            IRestResponse response = client.Execute(request);

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getreportsdata(string session, string sessionsig, string data)
        {
            var client = new RestClient("http://reports.msr-fsr.com/api/reports/get-data/?data=" + HttpUtility.UrlEncode(data));
            var request = new RestRequest(Method.GET);
            var cookies = Request.Cookies;
            var machineId = cookies["mongoMachineId"].Value;
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("mongoMachineId", machineId, ParameterType.Cookie);
            request.AddParameter("session", session, ParameterType.Cookie);
            request.AddParameter("session.sig", sessionsig, ParameterType.Cookie);

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://reports.msr-fsr.com/login");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            request.AddHeader("accept", "application/json, text/plain, */*");

            IRestResponse response = client.Execute(request);

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult dashboardsv2findall(string session, string sessionsig, string data)
        {

            var client = new RestClient("http://reports.msr-fsr.com/api/dashboardsv2/find-all?data=" + HttpUtility.UrlEncode(data));
            var request = new RestRequest(Method.GET);
            var cookies = Request.Cookies;
            var machineId = cookies["mongoMachineId"].Value;
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("mongoMachineId", machineId, ParameterType.Cookie);
            request.AddParameter("session", session, ParameterType.Cookie);
            request.AddParameter("session.sig", sessionsig, ParameterType.Cookie);

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://reports.msr-fsr.com/login");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            request.AddHeader("accept", "application/json, text/plain, */*");

            IRestResponse response = client.Execute(request);

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserData(string session, string sessionsig, string data)
        {

            var client = new RestClient("http://reports.msr-fsr.com/api/get-user-data?data=" + HttpUtility.UrlEncode(data));

            //var client = new RestClient("http://reports.msr-fsr.com/api/get-user-data?data=U2FsdGVkX19sUL12lTKj9FEriHlH%2FX%2F4DIDMS8IiKws%3D");
            var request = new RestRequest(Method.GET);
            var cookies = Request.Cookies;
            var machineId = cookies["mongoMachineId"].Value;
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("cookie", "mongoMachineId=" + machineId + ";");
            //request.AddHeader("cookie", "session=" + session + ";");
            //request.AddHeader("cookie", "session.sig=" + sessionsig+";");

            request.AddParameter("mongoMachineId", machineId, ParameterType.Cookie);
            request.AddParameter("session", session, ParameterType.Cookie);
            request.AddParameter("session.sig", sessionsig, ParameterType.Cookie);

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://reports.msr-fsr.com/login");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            request.AddHeader("accept", "application/json, text/plain, */*");

            //var cookies = Request.Cookies;
            //foreach (var cookieKey in cookies.AllKeys)
            //{
            //    if (cookies[cookieKey] != null)
            //    {
            //        request.AddCookie(cookieKey, cookies[cookieKey].Value);
            //    }
            //}

            IRestResponse response = client.Execute(request);
            //var ret = new
            //{
            //    data = response.Content
            //};

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        private JsonResult WidestageLoginResult()
        {

            var client = new RestClient("http://reports.msr-fsr.com/api/login");
            var request = new RestRequest(Method.POST);
            var cookies = Request.Cookies;
            foreach (var cookieKey in cookies.AllKeys)
            {
                if (cookies[cookieKey] != null)
                {
                    request.AddCookie(cookieKey, cookies[cookieKey].Value);
                }
            }

            //request.AddHeader("postman-token", "177c58c8-d52b-2c69-ed50-00c6efa96ffb");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("cookie", "mongoMachineId=9465429; session=eyJwYXNzcG9ydCI6e319; session.sig=uk7TSGaVKke76WBERG6PeB1xQlA; mongoMachineId=9465429; session=eyJwYXNzcG9ydCI6e30sImVycm9yIjoiQWNjZXNzIGRlbmllZCEifQ==; session.sig=aPOWi221pdLCb2Xd_DL2QAQPmB0");
            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://reports.msr-fsr.com/login");
            request.AddHeader("content-type", "application/json;charset=UTF-8");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            //request.AddHeader("origin", "http://reports.msr-fsr.com");
            request.AddHeader("accept", "application/json, text/plain, */*");
            request.AddParameter("application/json;charset=UTF-8", "{\"userName\":\"administrator\",\"password\":\"Djmj07Uyea8s/1;ja&v+\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var response2 = new
                {
                    response.StatusCode,
                    response.Content,
                    Cookies = response.Cookies.Select(x => new { name = x.Name, value = x.Value })
                };
                return Json(response2, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var response2 = new
                {
                    response.StatusCode
                };
                return Json(response2, JsonRequestBehavior.AllowGet);
            }
        }

    }
}