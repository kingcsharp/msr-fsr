using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Answer.Web.Controllers;
using Answer.Web.Filters;
using Hangfire.Annotations;
using RestSharp;
using HttpCookie = System.Web.HttpCookie;

namespace Msr.Web.Controllers
{
    [AuthorizeUser]
    public class ReportController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.ActiveClass = "WIP";
            return View();
        }

        public ActionResult FinancialDashboard()
        {
            ViewBag.ActiveClass = "WIP";
            ViewBag.CombinedURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/CombinedFinancialData";
            ViewBag.WONoInvoiceURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/WorkOrdersWithoutInvoices";

            return View();
        }

        public ActionResult WidestageLogin()
        {
            return WidestageLoginResult();
        }

        public ActionResult Findreport(string data, string id)
        { //'/api/reports/get-report/' + id, { id: id, mode: 'preview', linked: isLinked }
            var reportURL = ConfigurationManager.AppSettings["ReportingURL"].ToString();
            var client = new RestClient("http://"+ reportURL +"/api/reports/get-report/" + id + "?data=" + HttpUtility.UrlEncode(data));
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            SetCookiesForWidestageRequests(request);

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://" + reportURL + "/login");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            request.AddHeader("accept", "application/json, text/plain, */*");

            IRestResponse response = client.Execute(request);

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Findallreports(string data)
        {
            var reportURL = ConfigurationManager.AppSettings["ReportingURL"].ToString();
            var client = new RestClient("http://" + reportURL + "/api/reports/find-all?data=" + HttpUtility.UrlEncode(data));
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            SetCookiesForWidestageRequests(request);

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://" + reportURL + "/login");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            request.AddHeader("accept", "application/json, text/plain, */*");

            IRestResponse response = client.Execute(request);

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dashboardsv2get(string id, string data)
        {
            var reportURL = ConfigurationManager.AppSettings["ReportingURL"].ToString();
            var client = new RestClient("http://" + reportURL + "/api/dashboardsv2/get/" + id + "?data=" + HttpUtility.UrlEncode(data));
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            SetCookiesForWidestageRequests(request);

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://" + reportURL + "/login");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            request.AddHeader("accept", "application/json, text/plain, */*");

            IRestResponse response = client.Execute(request);

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public class ReportData
        {
            public string data { get; set; }
        }

        [HttpPost]
        public ActionResult getreportsdata(string data)
        {
            var reportURL = ConfigurationManager.AppSettings["ReportingURL"].ToString();
            var client = new RestClient("http://" + reportURL + "/api/reports/get-data/?data=" + HttpUtility.UrlEncode(data));
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            SetCookiesForWidestageRequests(request);

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://" + reportURL + "/login");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            request.AddHeader("accept", "application/json, text/plain, */*");

            IRestResponse response = client.Execute(request);

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public void SetCookiesForWidestageRequests(RestRequest request)
        {
            if (Request.Cookies["Widestage"] == null)
            {
                var response = LoginToWidestage();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var sessionCookies = response.Cookies.Select(x => x.Value).ToArray();
                    SetCookies(sessionCookies[0], sessionCookies[1]);
                    request.AddParameter("session", sessionCookies[0], ParameterType.Cookie);
                    request.AddParameter("session.sig", sessionCookies[1], ParameterType.Cookie);
                }
                else
                {
                    throw new Exception("Could not connect to reports");
                }
            }
            else
            {
                request.AddParameter("session", Request.Cookies["Widestage"]["session"], ParameterType.Cookie);
                request.AddParameter("session.sig", Request.Cookies["Widestage"]["sessionSig"], ParameterType.Cookie);
            }
        }

        public ActionResult dashboardsv2findall(string data)
        {

            var reportURL = ConfigurationManager.AppSettings["ReportingURL"].ToString();
            var client = new RestClient("http://" + reportURL + "/api/dashboardsv2/find-all?data=" + HttpUtility.UrlEncode(data));
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            SetCookiesForWidestageRequests(request);

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://" + reportURL + "/login");
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            request.AddHeader("accept", "application/json, text/plain, */*");

            IRestResponse response = client.Execute(request);

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserData(string data)
        {
            var reportURL = ConfigurationManager.AppSettings["ReportingURL"].ToString();
            var client = new RestClient("http://" + reportURL + "/api/get-user-data?data=" + HttpUtility.UrlEncode(data));

            //var client = new RestClient("http://"+ reportURL +"/api/get-user-data?data=U2FsdGVkX19sUL12lTKj9FEriHlH%2FX%2F4DIDMS8IiKws%3D");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            SetCookiesForWidestageRequests(request);

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://" + reportURL + "/login");
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

        public void SetCookies(string session, string sessionsig)
        {
            HttpCookie myCookie = new HttpCookie("Widestage");
            myCookie["session"] = session;
            myCookie["sessionSig"] = sessionsig;
            //myCookie.Expires = DateTime.Now.AddDays(1d); want to expire when user ends session
            Response.Cookies.Add(myCookie);
        }

        private JsonResult WidestageLoginResult()
        {
            var response = LoginToWidestage();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var sessionCookies = response.Cookies.Select(x => x.Value).ToArray();
                SetCookies(sessionCookies[0], sessionCookies[1]);
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

        private IRestResponse LoginToWidestage()
        {
            var reportURL = ConfigurationManager.AppSettings["ReportingURL"].ToString();
            var client = new RestClient("http://" + reportURL + "/api/login");
            var request = new RestRequest(Method.POST);
            var cookies = Request.Cookies;
            foreach (var cookieKey in cookies.AllKeys)
            {
                if (cookies[cookieKey] != null)
                {
                    request.AddCookie(cookieKey, cookies[cookieKey].Value);
                }
            }

            request.AddHeader("accept-language", "en-US,en;q=0.9,es-UY;q=0.8,es;q=0.7");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("referer", "http://"+ reportURL +"/login");
            request.AddHeader("content-type", "application/json;charset=UTF-8");
            request.AddHeader("user-agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            request.AddHeader("accept", "application/json, text/plain, */*");
            request.AddParameter("application/json;charset=UTF-8",
                "{\"userName\":\"administrator\",\"password\":\"Djmj07Uyea8s/1;ja&v+\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}