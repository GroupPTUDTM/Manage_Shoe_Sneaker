﻿using DoAn.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class DatHang
    {
        public int ID_DatHang { get; set; }
        public int ID_KhachHang { get; set; }
        public DateTime NgayDat { get; set; }
        public decimal TongTien { get; set; }
    }
}