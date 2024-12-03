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

        bool flag = false;

        GroupBox gbChange;
        TextBox txtIDSanPham;
        TextBox txtTenSanPham;
        ComboBox cmbThuongHieu;
        ComboBox cmbLoai;
        ComboBox cmbAnh;
        ComboBox cmbMau;
        ComboBox cmbSize;
        TextBox txtMota;
        TextBox txtDonViGia;
        TextBox txtSoLuongTon;
        Button btnSave;
        Button btnCancel;

        public frmQLySanPham()
        {
            InitializeComponent();
            InitializeDatagridview();
            loadDatagridview(null);

            this.dtgvProduct.CellClick += DtgvProduct_CellClick;
            this.dtgvProduct.CellLeave += DtgvProduct_CellLeave;
        }

        private void InitializeDatagridview()
        {
            dtgvProduct.Columns.Clear();
        
            dtgvProduct.EnableHeadersVisualStyles = false;
            dtgvProduct.ReadOnly = true;
            dtgvProduct.DefaultCellStyle.SelectionBackColor = Color.Purple;
            dtgvProduct.DefaultCellStyle.SelectionForeColor = Color.White;
            dtgvProduct.ColumnHeadersDefaultCellStyle.BackColor = Color.Purple;
            dtgvProduct.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvProduct.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dtgvProduct.DefaultCellStyle.ForeColor = Color.Black;

            dtgvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private async Task<DataTable> ConvertListToDataTable(List<SANPHAM> sanPhams)
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
            dataTable.Columns.Add("Đơn vị giá", typeof(decimal));
            dataTable.Columns.Add("Số lượng tồn", typeof(int));

            foreach (var sp in sanPhams)
            {
                var thuongHieu = sp.ID_ThuongHieu.HasValue
                    ? await th.getTHUONGHIEU(sp.ID_ThuongHieu.Value)
                    : null;
                var loai = sp.ID_DanhMuc.HasValue
                    ? await dm.getDANHMUC(sp.ID_DanhMuc.Value)
                    : null;
                var anh = sp.ID_Anh.HasValue
                    ? await ha.getHINHANH(sp.ID_Anh.Value)
                    : null;
                var size = sp.ID_Size.HasValue
                    ? await s.getSize(sp.ID_Size.Value)
                    : null;
                var mau = sp.ID_Mau.HasValue
                    ? await m.getMAU(sp.ID_Mau.Value)
                    : null;

                dataTable.Rows.Add(
                    sp.ID_SanPham,
                    sp.TenSanPham,
                    thuongHieu?.TenThuongHieu,
                    loai?.TenDanhMuc,
                    anh?.AnhChinh,
                    size?.Size1,
                    mau?.TenMau,
                    sp.Mota,
                    sp.DonViGia,
                    sp.SoLuongTon
                );
            }
            return dataTable;
        }

        private async void loadDatagridview(List<SANPHAM> sps)
        {
            dtgvProduct.DataSource = null;

            if (sps == null || sps.Count == 0)
            {
                sps = await sp.getSANPHAMs();

            }
            var dataTable = await ConvertListToDataTable(sps);
            dtgvProduct.DataSource = dataTable;
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

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dtgvProduct.SelectedCells.Count > 0)
            {
                DataGridViewCell selectedCell = dtgvProduct.SelectedCells[0];
                DataGridViewRow selectedRow = selectedCell.OwningRow;

                if (int.TryParse(selectedRow.Cells[0].Value?.ToString(), out int productId))
                {
                    DialogResult confirmResult = MessageBox.Show(
                        "Bạn có chắc chắn muốn xóa sản phẩm này?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirmResult == DialogResult.Yes)
                    {
                        BLL_SANPHAM bllSanPham = new BLL_SANPHAM();
                        bool isDeleted = await bllSanPham.DeleteSANPHAM(productId);

                        if (isDeleted)
                        {
                            MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            loadDatagridview(null);
                        }
                        else
                        {
                            MessageBox.Show("Xóa sản phẩm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xác định ID của sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một ô để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private async void btnSearch_Click(object sender, EventArgs e)
        {
            List<SANPHAM> sANPHAMs = await sp.SearchSANPHAMByName(txtSearch.Text);
            loadDatagridview(sANPHAMs);
        }

        private void createEditI(string tittle,List<string> row)
        {
            flag = false;
            createGB();
            gbChange.Text = tittle;
            btnSave.Text = "Sửa";
            btnCancel.Text = "Hủy";
            txtIDSanPham.Text = row[0];
            txtIDSanPham.Enabled = false;
            txtTenSanPham.Text = row[1];
            cmbThuongHieu.Text = row[2];
            cmbLoai.Text = row[3];
            cmbAnh.Text = row[4];
            cmbSize.Text = row[5];
            cmbMau.Text = row[6];
            txtMota.Text = row[7];
            txtDonViGia.Text = row[8];
            txtSoLuongTon.Text = row[9];       
        }
        private void createAddI(string tittle)
        {
            flag = true;
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
            lblLoai.Text = "Danh mục:";
            lblLoai.Location = new Point(20, 250);
            cmbLoai = new ComboBox();
            cmbLoai.Location = new Point(150, 250);
            cmbLoai.Width = 200;
            gbChange.Controls.Add(lblLoai);
            gbChange.Controls.Add(cmbLoai);

            Label lblAnh = new Label();
            lblAnh.Text = "Ảnh:";
            lblAnh.Location = new Point(20, 310);
            cmbAnh = new ComboBox();
            cmbAnh.Location = new Point(150, 310);
            cmbAnh.Width = 200;
            gbChange.Controls.Add(lblAnh);
            gbChange.Controls.Add(cmbAnh);


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
            btnSave.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold);
            btnSave.BackColor = Color.Cyan;
            btnSave.ForeColor = Color.Black;
            btnSave.Cursor = Cursors.Hand;
            gbChange.Controls.Add(btnSave);
            btnSave.Click += async (sender, e) => await btnSave_Click(sender, e);

            btnCancel = new Button();
            btnCancel.Location = new Point(520, 370);
            btnCancel.Size = new System.Drawing.Size(80, 30);
            btnCancel.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold);
            btnCancel.BackColor = Color.Yellow;
            btnCancel.ForeColor = Color.Black;
            btnCancel.Cursor = Cursors.Hand;
            gbChange.Controls.Add(btnCancel);
            btnCancel.Click += btnCancel_Click;

            this.Controls.Add(gbChange);
            getcmbMau();
            getcmbAnh();
            getcmbLoai();
            getcmbSize();
            getcmbThuongHieu();
        }

        private List<string> getText()
        {
            List<string> lt = new List<string>();
            lt.Add(txtIDSanPham.Text);
            lt.Add(txtTenSanPham.Text);
            lt.Add(cmbThuongHieu.Text);
            lt.Add(cmbLoai.Text);
            lt.Add(cmbAnh.Text);
            lt.Add(cmbSize.Text);
            lt.Add(cmbMau.Text);
            lt.Add(txtMota.Text);
            lt.Add(txtDonViGia.Text);
            lt.Add(txtSoLuongTon.Text);
            return lt;
        }

        private bool validate_Product()
        {
            if (string.IsNullOrWhiteSpace(txtIDSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập ID Sản Phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIDSanPham.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Sản Phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSanPham.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbThuongHieu.Text))
            {
                MessageBox.Show("Vui lòng chọn Thương Hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbThuongHieu.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn Loại Sản Phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbLoai.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbAnh.Text))
            {
                MessageBox.Show("Vui lòng chọn Ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAnh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbSize.Text))
            {
                MessageBox.Show("Vui lòng chọn Size!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSize.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbMau.Text))
            {
                MessageBox.Show("Vui lòng chọn Màu Sắc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMau.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMota.Text))
            {
                MessageBox.Show("Vui lòng nhập Mô Tả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMota.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDonViGia.Text))
            {
                MessageBox.Show("Vui lòng nhập Đơn Vị Giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonViGia.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSoLuongTon.Text))
            {
                MessageBox.Show("Vui lòng nhập Số Lượng Tồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongTon.Focus();
                return false;
            }

            return true;
        }

        private async Task btnSave_Click(object sender, EventArgs e)
        {
            if (validate_Product())
            {
                List<String> lt = getText();
                var thuonghieu = await th.getTHUONGHIEUByName(lt[2]);
                var loai = await dm.getDANHMUCByName(lt[3]);
                var anh = await ha.getHINHANHByName(lt[4]);
                var size = await s.getSizeBySize(int.Parse(lt[5]));
                var mau = await m.getMAUByName(lt[6]);
                SANPHAM sanpham = new SANPHAM
                {
                    ID_SanPham = int.Parse(lt[0]),
                    TenSanPham = lt[1],
                    ID_ThuongHieu = thuonghieu.ID_ThuongHieu,
                    ID_DanhMuc = loai.ID_DanhMuc,
                    ID_Anh = anh.ID_Anh,
                    ID_Size = size.ID_Size,
                    ID_Mau = mau.ID_Mau,
                    Mota = lt[7],
                    DonViGia = decimal.Parse(lt[8]),
                    SoLuongTon = int.Parse(lt[9])
                };
                if (flag == true)
                {
                    var result = await sp.AddSANPHAM(sanpham);

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show($"Đã xảy ra lỗi khi thêm sản phẩm: {result.ErrorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    var result = await sp.UpdateSANPHAM(sanpham);
                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Sửa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show($"Đã xảy ra lỗi khi sửa sản phẩm: {result.ErrorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gbChange.Hide();
            gbProduct.Show();
            loadDatagridview(null);
        }
        private async void getcmbMau()
        {
            cmbMau.Items.Clear();
            var maus = await m.getMAUs();

            cmbMau.Items.AddRange(maus.Select(x => x.TenMau).ToArray());
            cmbMau.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMau.SelectedIndex = 0;
        }
        private async void getcmbAnh()
        {
            cmbAnh.Items.Clear();
            var anh = await ha.getHINHANHs();

            cmbAnh.Items.AddRange(anh.Select(x => x.AnhChinh).ToArray());
            cmbAnh.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAnh.SelectedIndex = 0;
        }
        private async void getcmbThuongHieu()
        {
            cmbThuongHieu.Items.Clear();
            var thuongHieus = await th.getTHUONGHIEUs();

            cmbThuongHieu.Items.AddRange(thuongHieus.Select(x => x.TenThuongHieu).ToArray());
            cmbThuongHieu.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbThuongHieu.SelectedIndex = 0;
        }
        private async void getcmbLoai()
        {
            cmbLoai.Items.Clear();
            var loais = await dm.getDANHMUCs();

            cmbLoai.Items.AddRange(loais.Select(x => x.TenDanhMuc).ToArray());
            cmbLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLoai.SelectedIndex = 0;
        }
        private async void getcmbSize()
        {
            cmbSize.Items.Clear();
            var sizes = await s.getSizes();

            cmbSize.Items.AddRange(sizes.Select(x => x.Size1.ToString()).ToArray());
            cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSize.SelectedIndex = 0;
        }

    }
}
