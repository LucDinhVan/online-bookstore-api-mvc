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
        SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();
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

        [HttpPost]
        public JsonResult XemCTDHKH(int makh)
        {
            TempData["makh"] = makh;
            return Json(new { Url = Url.Action("XemCTDHKHPartial") });
        }
        public PartialViewResult XemCTDHKHPartial()
        {
            int maKH = (int)TempData["makh"];
            var lstKH = db.DONHANGs.Where(n => n.MaKH == maKH).ToList();
            return PartialView(lstKH);
        }
    }
}