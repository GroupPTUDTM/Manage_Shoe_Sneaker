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

        public ActionResult DatHang()
        {

            return View();
        }


    }
}