using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class frmQLySanPham : Form
    {
        BLL_SANPHAM sp = new BLL_SANPHAM();
        BLL_THUONGHIEU th = new BLL_THUONGHIEU();
        BLL_DANHMUC dm = new BLL_DANHMUC();
        BLL_HINHANH ha = new BLL_HINHANH();
        BLL_Size s = new BLL_Size();
        BLL_MAU m = new BLL_MAU();

        private string selectedImagePath = "";
        GroupBox gbChange;
        Label lblImageName;
        TextBox txtIDSanPham;
        TextBox txtTenSanPham;
        ComboBox cmbThuongHieu;
        ComboBox cmbLoai;
        ComboBox cmbMau;
        ComboBox cmbSize;
        TextBox txtMota;
        TextBox txtDonViGia;
        TextBox txtSoLuongTon;
        Button btnAnh;
        Button btnSave;
        Button btnCancel;

        public frmQLySanPham()
        {
            InitializeComponent();
            InitializeDatagridview();
            this.dtgvProduct.CellClick += DtgvProduct_CellClick;
            this.dtgvProduct.CellLeave += DtgvProduct_CellLeave;
        }

        

        private void InitializeDatagridview()
        {
            dtgvProduct.Columns.Clear();
        
            dtgvProduct.EnableHeadersVisualStyles = false;
            dtgvProduct.DefaultCellStyle.SelectionBackColor = Color.Purple;
            dtgvProduct.DefaultCellStyle.SelectionForeColor = Color.White;
            dtgvProduct.ColumnHeadersDefaultCellStyle.BackColor = Color.Purple;
            dtgvProduct.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvProduct.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dtgvProduct.DefaultCellStyle.ForeColor = Color.Black;

            getData();

            dtgvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void getListProduct()
        {

        }
        private DataTable ConvertListToDataTable(List<SANPHAM> sanPhams)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Mã sản phẩm", typeof(string));
            dataTable.Columns.Add("Tên sản phẩm", typeof(string));
            dataTable.Columns.Add("Thương hiệu", typeof(string));
            dataTable.Columns.Add("Loại", typeof(string));
            dataTable.Columns.Add("Ảnh", typeof(string));
            dataTable.Columns.Add("Size", typeof(string));
            dataTable.Columns.Add("Màu", typeof(string));
            dataTable.Columns.Add("Mô tả", typeof(string));
            dataTable.Columns.Add("Đơn vị giá", typeof(double));
            dataTable.Columns.Add("Số lượng tồn", typeof(int));

            foreach (var sp in sanPhams)
            {
                var thuongHieu = sp.ID_ThuongHieu.HasValue
                    ? th.getTHUONGHIEU(sp.ID_ThuongHieu.Value)
                    : null;
                var loai = sp.ID_DanhMuc.HasValue
                    ? dm.getDANHMUC(sp.ID_DanhMuc.Value)
                    : null;
                var anh = sp.ID_Anh.HasValue
                    ? ha.getHINHANH(sp.ID_Anh.Value)
                    : null;
                var size = sp.ID_Size.HasValue
                    ? s.getSIZE(sp.ID_Size.Value)
                    : null;
                var mau = sp.ID_Mau.HasValue
                    ? m.getMAU(sp.ID_Mau.Value)
                    : null;

                dataTable.Rows.Add(
                    sp.ID_SanPham,
                    sp.TenSanPham,
                    thuongHieu.TenThuongHieu,
                    loai.TenDanhMuc,
                    anh.AnhChinh,
                    size.Size1,
                    mau.TenMau,
                    sp.Mota,
                    sp.DonViGia,
                    sp.SoLuongTon
                );
            }
            return dataTable;
        }
        private async void getData()
        {
            // Lấy danh sách sản phẩm bất đồng bộ từ BLL
            var sanPhams = await sp.getSANPHAMs();
            dtgvProduct.DataSource = ConvertListToDataTable(sanPhams);
        }

        private void DtgvProduct_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtgvProduct.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }

        private void DtgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dtgvProduct.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MediumPurple;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            createAddI("Thêm sản phẩm");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dtgvProduct.SelectedCells.Count > 0)
            {
                DataGridViewCell selectedCell = dtgvProduct.SelectedCells[0];
                DataGridViewRow selectedRow = selectedCell.OwningRow;

                List<string> selectedRowData = new List<string>();

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    selectedRowData.Add(cell.Value?.ToString() ?? string.Empty);
                }

                createEditI("Sửa sản phẩm", selectedRowData);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một ô để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
        private void createEditI(string tittle,List<string> row)
        {
            createGB();
            gbChange.Text = tittle;
            btnSave.Text = "Sửa";
            btnCancel.Text = "Hủy";
            txtIDSanPham.Text = row[0];
            txtTenSanPham.Text = row[1];
            cmbThuongHieu.Text = row[2];
            cmbLoai.Text = row[3];
            lblImageName.Text = row[4];
            cmbSize.Text = row[5];
            cmbMau.Text = row[6];
            txtMota.Text = row[7];
            txtDonViGia.Text = row[8];
            txtSoLuongTon.Text = row[9];
        }
        private void createAddI(string tittle)
        {
            createGB();
            gbChange.Text = tittle;
            btnSave.Text = "Thêm";
            btnCancel.Text = "Hủy";
        }
        private void createGB()
        {
            gbProduct.Hide();

            gbChange = new GroupBox();
            gbChange.Size = new System.Drawing.Size(733, 455);
            gbChange.Font = new Font("Microsoft Sans Serif", 9.75f);
            gbChange.ForeColor = Color.White;
            gbChange.Dock = DockStyle.Bottom;

            Label lblIDSanPham = new Label();
            lblIDSanPham.Text = "Mã sản phẩm:";
            lblIDSanPham.Location = new Point(20, 70);
            txtIDSanPham = new TextBox();
            txtIDSanPham.Location = new Point(150, 70);
            txtIDSanPham.Width = 200;
            gbChange.Controls.Add(lblIDSanPham);
            gbChange.Controls.Add(txtIDSanPham);

            Label lblTenSanPham = new Label();
            lblTenSanPham.Text = "Tên sản phẩm:";
            lblTenSanPham.Location = new Point(20, 130);
            txtTenSanPham = new TextBox();
            txtTenSanPham.Location = new Point(150, 130);
            txtTenSanPham.Width = 200;
            gbChange.Controls.Add(lblTenSanPham);
            gbChange.Controls.Add(txtTenSanPham);

            Label lblThuongHieu = new Label();
            lblThuongHieu.Text = "Thương hiệu:";
            lblThuongHieu.Location = new Point(20, 190);
            cmbThuongHieu = new ComboBox();
            cmbThuongHieu.Location = new Point(150, 190);
            cmbThuongHieu.Width = 200;
            gbChange.Controls.Add(lblThuongHieu);
            gbChange.Controls.Add(cmbThuongHieu);

            Label lblLoai = new Label();
            lblLoai.Text = "Loại:";
            lblLoai.Location = new Point(20, 250);
            cmbLoai = new ComboBox();
            cmbLoai.Location = new Point(150, 250);
            cmbLoai.Width = 200;
            gbChange.Controls.Add(lblLoai);
            gbChange.Controls.Add(cmbLoai);

            Label lblAnh = new Label();
            lblAnh.Text = "Ảnh:";
            lblAnh.Location = new Point(20, 310);
            btnAnh = new Button();
            btnAnh.Text = "Tải ảnh lên";
            btnAnh.Location = new Point(150, 310);
            btnAnh.Width = 200;
            lblImageName = new Label();
            lblImageName.Location = new Point(150, 335);
            lblImageName.AutoSize = true;
            gbChange.Controls.Add(lblAnh);
            gbChange.Controls.Add(btnAnh);
            gbChange.Controls.Add(lblImageName);
            btnAnh.Click += btnAnh_Click;


            Label lblSize = new Label();
            lblSize.Text = "Size:";
            lblSize.Location = new Point(400, 70);
            cmbSize = new ComboBox();
            cmbSize.Location = new Point(500, 70);
            cmbSize.Width = 200;
            gbChange.Controls.Add(lblSize);
            gbChange.Controls.Add(cmbSize);

            Label lblMau = new Label();
            lblMau.Text = "Màu:";
            lblMau.Location = new Point(400, 130);
            cmbMau = new ComboBox();
            cmbMau.Location = new Point(500, 130);
            cmbMau.Width = 200;
            gbChange.Controls.Add(lblMau);
            gbChange.Controls.Add(cmbMau);

            Label lblMota = new Label();
            lblMota.Text = "Mô tả:";
            lblMota.Location = new Point(400, 190);
            txtMota = new TextBox();
            txtMota.Location = new Point(500, 190);
            txtMota.Width = 200;
            gbChange.Controls.Add(lblMota);
            gbChange.Controls.Add(txtMota);

            Label lblDonViGia = new Label();
            lblDonViGia.Text = "Đơn vị giá:";
            lblDonViGia.Location = new Point(400, 250);
            txtDonViGia = new TextBox();
            txtDonViGia.Location = new Point(500, 250);
            txtDonViGia.Width = 200;
            gbChange.Controls.Add(lblDonViGia);
            gbChange.Controls.Add(txtDonViGia);

            Label lblSoLuongTon = new Label();
            lblSoLuongTon.Text = "Số lượng tồn:";
            lblSoLuongTon.Location = new Point(400, 310);
            txtSoLuongTon = new TextBox();
            txtSoLuongTon.Location = new Point(500, 310);
            txtSoLuongTon.Width = 200;
            gbChange.Controls.Add(lblSoLuongTon);
            gbChange.Controls.Add(txtSoLuongTon);

            btnSave = new Button();
            btnSave.Location = new Point(400, 370);
            btnSave.Size = new System.Drawing.Size(80, 30);
            btnSave.BackColor = Color.Cyan;
            btnSave.ForeColor = Color.Black;
            btnSave.Click += btnSave_Click;
            gbChange.Controls.Add(btnSave);

            btnCancel = new Button();
            btnCancel.Location = new Point(520, 370);
            btnCancel.Size = new System.Drawing.Size(80, 30);
            btnCancel.BackColor = Color.Yellow;
            btnCancel.ForeColor = Color.Black;
            btnCancel.Click += btnCancel_Click;
            gbChange.Controls.Add(btnCancel);


            this.Controls.Add(gbChange);
            getcmbMau();
            getcmbLoai();
            getcmbSize();
            getcmbThuongHieu();
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Title = "Chọn ảnh";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog.FileName;

                if (lblImageName.Text != string.Empty)
                {
                    lblImageName.Text = string.Empty;
                }
            
                lblImageName.Text = Path.GetFileName(selectedImagePath);
                
                MessageBox.Show("Ảnh đã được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedImagePath))
            {
                MessageBox.Show("Vui lòng chọn ảnh trước khi lưu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string destinationDirectory = @"C:\Users\ADMIN\Desktop\code\ptpm\doan\Manage_Shoe_Sneaker\QuanLyCuaHangGiaySneaker_Web\DoAn\DoAn\assets\Images";

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            string fileName = Path.GetFileName(selectedImagePath);
            string destinationFilePath = Path.Combine(destinationDirectory, fileName);

            try
            {
                File.Copy(selectedImagePath, destinationFilePath, true);
                MessageBox.Show("Ảnh đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gbChange.Hide();
            gbProduct.Show();
        }
        private void getcmbMau()
        {
            cmbMau.Items.Clear();
            cmbMau.Items.AddRange(m.getMAUs().Select(x => x.TenMau).ToArray());
            cmbMau.SelectedIndex = 0;
        }
        private void getcmbThuongHieu()
        {
            cmbThuongHieu.Items.Clear();
            cmbThuongHieu.Items.AddRange(th.getTHUONGHIEUs().Select(x => x.TenThuongHieu).ToArray());
            cmbThuongHieu.SelectedIndex = 0;
        }
        private void getcmbLoai()
        {
            cmbLoai.Items.Clear();
            cmbLoai.Items.AddRange(dm.getDANHMUCs().Select(x => x.TenDanhMuc).ToArray());
            cmbLoai.SelectedIndex = 0;
        }
        private void getcmbSize()
        {
            cmbSize.Items.Clear();
            cmbSize.Items.AddRange(s.getSizes().Select(x => x.Size1.ToString()).ToArray());
            cmbSize.SelectedIndex = 0;
        }

    }
}
