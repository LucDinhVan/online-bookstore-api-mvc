using System.Linq;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;
using PagedList;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace TriThucOnline_TTN.Controllers
{
    [Authorize]
    public class QLDonHangController : Controller
    {
        // GET: QLDonHang
        public ActionResult Index(int? page)
        {
            Orders orders = null;
            // GET

            Extensions.request = new RestRequest($"orders?name", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                orders = JsonConvert.DeserializeObject<Orders>(result.Content);
            }
            else
            {

            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(orders.orders.ToList().OrderByDescending(n => n.id).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult IndexPartial(int? page)
        {
            Orders orders = null;
            // GET

            Extensions.request = new RestRequest($"orders?name", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                orders = JsonConvert.DeserializeObject<Orders>(result.Content);
            }
            else
            {

            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return PartialView(orders.orders.ToList().OrderByDescending(n => n.id).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            Extensions.request = new RestRequest($"orders/{id}", Method.DELETE);

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

        public PartialViewResult XemCTDHPartial(int? id)
        {
            Order details = null;
            // GET

            Extensions.request = new RestRequest($"orders/{id}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                details = JsonConvert.DeserializeObject<Order>(result.Content);
                ViewBag.KhuyenMai = details.coupon_code;
            }
            else
            {

            }
            return PartialView(details);
        }

        [HttpPost]
        public JsonResult Update(int id, int status)
        {
            Extensions.request = new RestRequest($"orders/{id}", Method.PUT);
            string stt = "";
            switch (status)
            {
                case 0:
                    {
                        stt = "Đặt hàng";
                        break;
                    }
                case 1:
                    {
                        stt = "Đóng gói";
                        break;
                    }
                case 2:
                    {
                        stt = "Vận chuyển";
                        break;
                    }
                case 3:
                    {
                        stt = "Nhận hàng";
                        break;
                    }
                case 4:
                    {
                        stt = "Hủy";
                        break;
                    }
                default:
                    stt = "Đặt hàng";
                    break;
            }
            var data = JsonConvert.SerializeObject(new { id = id, status = stt, shipped_date = 1607185187 });
            Extensions.request.AddParameter("application/json", data, ParameterType.RequestBody);

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
                Response.Write(result.Content);
                return null;
            }

        }

        public ActionResult ExportClientsListToExcel()
        {
            // var grid = new GridView();
            // grid.DataSource = products;
            // grid.DataBind();

            // Response.ClearContent();
            // Response.Buffer = true;
            // Response.AddHeader("content-disposition", "attachment; filename=DONHANG.xls");
            // Response.ContentType = "application/ms-excel";

            // Response.Charset = "";
            // StringWriter sw = new StringWriter();
            // HtmlTextWriter htw = new HtmlTextWriter(sw);

            // grid.RenderControl(htw);

            // Response.Output.Write(sw.ToString());
            // Response.Flush();
            // Response.End();

            return View("MyView");
        }

        public ActionResult Edit(){
            return View();
        }
    }
}