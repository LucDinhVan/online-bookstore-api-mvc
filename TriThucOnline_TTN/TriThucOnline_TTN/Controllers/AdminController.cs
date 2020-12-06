using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TriThucOnline_TTN.Models;

namespace TriThucOnline_TTN.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Request.Cookies[".ASPXAUTH"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form, string ReturnUrl)
        {

            string url = Server.UrlDecode(Request.UrlReferrer.PathAndQuery);
            string[] urldecode = url.Split('=');
            string returnurl = "";
            if (urldecode.Count() > 1)
            {
                returnurl = urldecode[1];
            }
            string username = form["username"].ToString();
            string password = form["password"].ToString();




            // POST
            var loginObj = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password
            });

            Extensions.request = new RestRequest("admin/login", Method.POST);
            Extensions.request.AddHeader("Content-Type", "application/json");
            Extensions.request.AddParameter("application/json", loginObj, ParameterType.RequestBody);

            var responseTask = Extensions.client.ExecutePostAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                FormsAuthentication.SetAuthCookie(username, false);

                if (!string.IsNullOrEmpty(returnurl))
                {
                    return Redirect(returnurl);

                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Tên tài khoản hoặc mật khẩu không đúng!";

            }
            return View();
        }
        public ActionResult Logout()
        {
            Extensions.request = new RestRequest("admin/login", Method.POST);

            var responseTask = Extensions.client.ExecutePostAsync(Extensions.request);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public PartialViewResult TotalMember()
        {
            return PartialView();
        }
        public PartialViewResult TotalBook()
        {
            return PartialView();
        }
        public PartialViewResult TotalMoney()
        {
            return PartialView();
        }
        public PartialViewResult TotalDHChuaThanhToan()
        {
            return PartialView();
        }
    }
}
