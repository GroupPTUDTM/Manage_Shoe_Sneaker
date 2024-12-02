using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GUI
{
    public partial class frmQLyLoaiSanPham : Form
    {
        public frmQLyLoaiSanPham()
        {
            InitializeComponent();
            InitializeDatagridview();
            this.dtgvCategory.CellClick += DtgvCategory_CellClick;
        }

        private void InitializeDatagridview()
        {
            dtgvCategory.Columns.Clear();

            dtgvCategory.Columns.Add("ID_DanhMuc", "Mã loại");
            dtgvCategory.Columns.Add("TenDanhMuc", "Tên loại");

            dtgvCategory.EnableHeadersVisualStyles = false;
            dtgvCategory.ColumnHeadersDefaultCellStyle.BackColor = Color.Cyan;
            dtgvCategory.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dtgvCategory.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dtgvCategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }


        private void DtgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dtgvCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(46, 51, 73);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
