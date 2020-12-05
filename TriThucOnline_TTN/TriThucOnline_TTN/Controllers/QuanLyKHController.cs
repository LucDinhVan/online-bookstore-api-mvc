using System.Linq;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;
using PagedList;
using Newtonsoft.Json;
using RestSharp;
using System.IO;

namespace TriThucOnline_TTN.Controllers
{
    [Authorize]
    public class QuanLyKHController : Controller
    {
        // GET: QuanLyKH
        public ActionResult Index(int? page)
        {
            Users users = null;
            // GET

            Extensions.request = new RestRequest($"users?name", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                users = JsonConvert.DeserializeObject<Users>(result.Content);
            }
            else
            {

            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(users.users.ToList().OrderBy(n => n.id).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult IndexPartial(int? page)
        {
            Users users = null;
            // GET

            Extensions.request = new RestRequest($"users?name", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                users = JsonConvert.DeserializeObject<Users>(result.Content);
            }
            else
            {

            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return PartialView(users.users.ToList().OrderBy(n => n.id).ToPagedList(pageNumber, pageSize));
        }
        
        public PartialViewResult XemCTKHPartial(int? id)
        {
            User user = null;
            // GET

            Extensions.request = new RestRequest($"user/{id}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                user = JsonConvert.DeserializeObject<User>(result.Content);
            }
            else
            {

            }
            return PartialView(user);
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            Extensions.request = new RestRequest($"user/{id}", Method.DELETE);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                return Json(new { Url = Url.Action("IndexPartial") });
            }
            else
            {
                Response.StatusCode = 500;
                return null;
            }
        }

        public PartialViewResult XemCTDHKHPartial(int? id)
        {
            User user = null;
            // GET

            Extensions.request = new RestRequest($"user/{id}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                user = JsonConvert.DeserializeObject<User>(result.Content);
            }
            else
            {

            }
            return PartialView(user);
        }
    }
}