using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;

namespace TriThucOnline_TTN.Controllers
{
    public class GioHangController : Controller
    {
        SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();
        public List<Cart> LayGioHang()
        {
            Carts carts = null;
            Extensions.request = new RestRequest($"carts", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                carts = JsonConvert.DeserializeObject<Carts>(result.Content);
                ViewBag.CacSanPhamTrongGio = carts.carts;
            }

            return carts.carts;

        }
        public ActionResult GioHangTrong()
        {
            return View();
        }
        public ActionResult Index()
        {
            Carts carts = null;
            Extensions.request = new RestRequest($"carts", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                carts = JsonConvert.DeserializeObject<Carts>(result.Content);
                ViewBag.CacSanPhamTrongGio = carts.carts;
            }
            else
            {
                //return Giohang Trống
                return RedirectToAction("GioHangTrong");
            }


            return View(carts);
        }

        [HttpGet]
        public ActionResult Confirm()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "NguoiDung");
            }

            User kh = Session["TaiKhoan"] as User;

            return View(kh);
        }

        [HttpGet]
        public ActionResult _Partial_Confirm()
        {
            List<Cart> lstGioHang = LayGioHang();
            return PartialView(lstGioHang);
        }

        //dem so luong san pham
        public ActionResult GioHangPartial()
        {
            int cartcount = 0;
            if (Session["ShoppingCart"] != null)
            {
                List<Cart> ls = (List<Cart>)Session["ShoppingCart"];
                foreach (Cart item in ls)
                {
                    cartcount += item.quantity;
                }
            }
            ViewBag.count = cartcount;


            return PartialView();
        }
        [HttpPost]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "NguoiDung");
            }

            return Json(new { Url = Url.Action("DatHangPartial") });

            return View();
        }

        [HttpPost]
        public ActionResult DatHangPartial()
        {
            return PartialView("DatHangPartial");
        }

        [HttpPost]
        public ActionResult DatHangSubmit(string NgayGiao, string MaKM)
        {
            try
            {
                if (string.IsNullOrEmpty(MaKM)) MaKM = "";
                List<Cart> carts = LayGioHang();
                List<object> list = new List<object>();
                foreach (Cart cart in carts)
                {
                    var obj = new { book_id = cart.book_id, quantity=cart.quantity };
                    list.Add(obj);
                }



                Extensions.request = new RestRequest($"orders", Method.POST);

                var data = JsonConvert.SerializeObject(new { required_date = NgayGiao, coupon_code = MaKM, order_details= list});
                Extensions.request.AddParameter("application/json", data, ParameterType.RequestBody);

                var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessful)
                {
                    dynamic obj = JsonConvert.DeserializeObject<Object>(result.Content);
                }
                else
                {
                    return Json(new { success = false, msg = "Đặt hàng thất bại, vui lòng thử lại!" });
                }

            }
            catch (Exception)
            {
                return Json(new { success = false, msg = "Đặt hàng thất bại, vui lòng thử lại!" });
            }
            return Json(new { success = true, msg = "Đặt hàng thành công!" });
        }

        [HttpPost]
        public ActionResult CheckoutCart()
        {
            return Json(new { Url = Url.Action("Checkout") });
        }

        [HttpPost]
        public ActionResult Checkout()
        {
            List<Cart> lstGioHang = LayGioHang();
            return PartialView(lstGioHang);
        }

        //cap nhat gio hang
        [HttpPost]
        public ActionResult CapNhat(int id, int sl)
        {
            bool success = true;
            string error = "";
            Carts carts = null;
            Extensions.request = new RestRequest($"carts/{id}", Method.PUT);
            var data = JsonConvert.SerializeObject(new
            {
                quantity = sl
            });
            Extensions.request.AddParameter("application/json", data, ParameterType.RequestBody);
            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);

            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                
            }
            else
            {
                success = false;
                error = "Rất tiếc, kho hàng của sản phẩm không đủ";
                
            }
            return Json(new { Success = success, ErrorMsg = error, Url = Url.Action("Success") });
        }

        [HttpGet]
        public ActionResult Success()
        {
            Carts carts = null;
            Extensions.request = new RestRequest($"carts", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                carts = JsonConvert.DeserializeObject<Carts>(result.Content);
            }
            return PartialView(carts);
        }

        //xoa gio hang
        [HttpPost]
        public ActionResult Remove(int id)
        {
            int cartCount = 0;
            Carts carts = null;
            Extensions.request = new RestRequest($"carts/{id}", Method.DELETE);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                return Json(new { Url = Url.Action("Success"), sl = cartCount });
            }
            else
            {
                //return Giohang Trống
                return RedirectToAction("GioHangTrong");
            }
            
        }

        [HttpPost]
        public ActionResult KhuyenMai(string code)
        {
            // GET
            Coupon cp = null;
            Extensions.request = new RestRequest($"coupons/{code}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                cp = JsonConvert.DeserializeObject<Coupon>(result.Content);
            }
            else
            {

            }

            if (cp == null || DateTime.ParseExact(cp.valid_from, "dd/MM/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture) > DateTime.Now || cp.is_enable == false || DateTime.ParseExact(cp.valid_until, "dd/MM/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture) < DateTime.Now || cp.count <= 0)
            {
                return Json(new { tb = "Lỗi!", id = "", gt = "" });
            }
            return Json(new { tb = "Mã khuyến mãi có hiệu lực: ", code = code, gt = cp.discount + "%" });
        }
    }
}