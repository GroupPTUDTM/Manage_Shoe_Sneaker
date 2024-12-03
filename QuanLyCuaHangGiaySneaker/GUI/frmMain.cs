using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GUI
{
    public partial class frmMain : Form
    {
        private bool isMouseDown = false;
        private Point mouseOffset;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse
            );

        public frmMain()
        {
            InitializeComponent();
            this.MouseDown += FrmMain_MouseDown;
            this.MouseMove += FrmMain_MouseMove;
            this.MouseUp += FrmMain_MouseUp;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            this.PnlFormLoader.Controls.Clear();
            frmDashboard frmDashboard1 = new frmDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmDashboard1.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(frmDashboard1);
            frmDashboard1.Show();
        }

        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                this.Location = new Point(this.Left + e.X - mouseOffset.X, this.Top + e.Y - mouseOffset.Y);
            }
        }

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                mouseOffset = e.Location;
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);

            this.PnlFormLoader.Controls.Clear();
            frmDashboard frmDashboard1 = new frmDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true};
            frmDashboard1.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add( frmDashboard1 );
            frmDashboard1.Show();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnCategory.Height;
            pnlNav.Top = btnCategory.Top;
            btnCategory.BackColor = Color.FromArgb(46, 51, 73);

            this.PnlFormLoader.Controls.Clear();
            frmQLyDanhMuc frmQLyDanhMuc1 = new frmQLyDanhMuc() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmQLyDanhMuc1.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(frmQLyDanhMuc1);
            frmQLyDanhMuc1.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnProduct.Height;
            pnlNav.Top = btnProduct.Top;
            btnProduct.BackColor = Color.FromArgb(46, 51, 73);

            this.PnlFormLoader.Controls.Clear();
            frmQLySanPham frmQLySanPham1 = new frmQLySanPham() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmQLySanPham1.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(frmQLySanPham1);
            frmQLySanPham1.Show();
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnBrand.Height;
            pnlNav.Top = btnBrand.Top;
            btnBrand.BackColor = Color.FromArgb(46, 51, 73);

            this.PnlFormLoader.Controls.Clear();
            frmQLyThuongHieu frmQLyThuongHieu1 = new frmQLyThuongHieu() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmQLyThuongHieu1.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(frmQLyThuongHieu1);
            frmQLyThuongHieu1.Show();
        }

        private void btnAnalyst_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnAnalyst.Height;
            pnlNav.Top = btnAnalyst.Top;
            btnAnalyst.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnDashboard_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnCategory_Leave(object sender, EventArgs e)
        {
            btnCategory.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnProduct_Leave(object sender, EventArgs e)
        {
            btnProduct.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnBrand_Leave(object sender, EventArgs e)
        {
            btnBrand.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnAnalyst_Leave(object sender, EventArgs e)
        {
            btnAnalyst.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
