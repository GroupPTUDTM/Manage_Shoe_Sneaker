using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        QLBGDataContext db = new QLBGDataContext();

        public ActionResult Index1()
        {
            return View();
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int ms, string strURL, int quantity = 1)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.Find(s => s.ID_SanPham == ms);

            if (SanPham == null)
            {
                SanPham = new GioHang(ms);
                lstGioHang.Add(SanPham);
            }
            else
            {
                SanPham.SoLuong++;
            }

            // Update the session with the modified shopping cart
            Session["GioHang"] = lstGioHang;

            // Redirect to the shopping cart page
            return RedirectToAction("GioHang");
        }

        public int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(s => s.SoLuong);
            }
            return tsl;
        }
        private double ThanhTien()
        {
            double tt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tt += lstGioHang.Sum(s => s.ThanhTien);
            }
            return tt;
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index1", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = ThanhTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangPar()
        {
            ViewBag.TongSoLuong = TongSoLuong();

            return View();
        }
        public ActionResult TangSoLuong(int id)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.Find(s => s.ID_SanPham == id);
            if (SanPham != null)
            {
                SanPham.SoLuong++;
            }

            return RedirectToAction("GioHang");
        }

        public ActionResult GiamSoLuong(int id)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.Find(s => s.ID_SanPham == id);
            if (SanPham != null && SanPham.SoLuong > 1)
            {
                SanPham.SoLuong--;
            }

            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(int id)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.Find(s => s.ID_SanPham == id);

            if (SanPham != null)
            {
                lstGioHang.Remove(SanPham);
            }

            return RedirectToAction("GioHang");
        }

        //public ActionResult DatHang()
        //{

        //    return View();
        //}
        public ActionResult DatHang()
        {
            var lstGioHang = LayGioHang();  // Lấy giỏ hàng từ session
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("XemGioHang");  // Nếu giỏ hàng trống, yêu cầu người dùng thêm sản phẩm
            }

            // Tạo đối tượng đơn hàng mới
            DATHANG donHang = new DATHANG();
            donHang.ID_KhachHang = 1;
            donHang.NgayDat = DateTime.Now;
            donHang.TongTien = ((decimal)ThanhTien()); 
        

            db.DATHANGs.InsertOnSubmit(donHang);  // Thêm đơn hàng vào cơ sở dữ liệu
            db.SubmitChanges();  // Lưu vào database

            // Lưu chi tiết đơn hàng
            foreach (var gioHang in lstGioHang)
            {
                CT_DATHANG chiTiet = new CT_DATHANG
                {
                    ID_DatHang = donHang.ID_DatHang,
                    ID_SanPham = gioHang.ID_SanPham,
                    SoLuong = gioHang.SoLuong,
                    
                };
                db.CT_DATHANGs.InsertOnSubmit(chiTiet);  // Thêm chi tiết đơn hàng vào cơ sở dữ liệu
            }
            db.SubmitChanges();  // Lưu các chi tiết đơn hàng vào database

            // Xóa giỏ hàng trong session sau khi đã đặt hàng thành công
            Session["GioHang"] = null;

            return View();  // Chuyển đến trang thành công
        }



    }
}