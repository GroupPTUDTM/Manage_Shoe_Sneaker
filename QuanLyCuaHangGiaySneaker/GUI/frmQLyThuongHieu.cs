using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class frmQLyThuongHieu : Form
    {
        BLL_THUONGHIEU th = new BLL_THUONGHIEU();

        bool flag = false;

        GroupBox gbChange;
        TextBox txtIDTHUONGHIEU;
        TextBox txtTenTHUONGHIEU;
        Button btnSave;
        Button btnCancel;

        public frmQLyThuongHieu()
        {
            InitializeComponent();
            InitializeDatagridview();
            loadDatagridview(null);

            this.dtgvBrand.CellClick += dtgvBrand_CellClick;
            this.dtgvBrand.CellLeave += dtgvBrand_CellLeave;
        }

        private void InitializeDatagridview()
        {
            dtgvBrand.Columns.Clear();

            dtgvBrand.EnableHeadersVisualStyles = false;
            dtgvBrand.ReadOnly = true;
            dtgvBrand.DefaultCellStyle.SelectionBackColor = Color.DarkOrange;
            dtgvBrand.DefaultCellStyle.SelectionForeColor = Color.White;
            dtgvBrand.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkOrange;
            dtgvBrand.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvBrand.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dtgvBrand.DefaultCellStyle.ForeColor = Color.Black;

            dtgvBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private async Task<DataTable> ConvertListToDataTable(List<THUONGHIEU> thuonghieus)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Mã thương hiệu", typeof(int));
            dataTable.Columns.Add("Tên thương hiệu", typeof(string));

            foreach (var thuonghieu in thuonghieus)
            {
                dataTable.Rows.Add(
                    thuonghieu.ID_ThuongHieu,
                    thuonghieu.TenThuongHieu
                );
            }
            return dataTable;
        }

        private async void loadDatagridview(List<THUONGHIEU> thuonghieus)
        {
            dtgvBrand.DataSource = null;

            if (thuonghieus == null || thuonghieus.Count == 0)
            {
                thuonghieus = await th.getTHUONGHIEUs();
            }
            var dataTable = await ConvertListToDataTable(thuonghieus);
            dtgvBrand.DataSource = dataTable;
        }

        private void dtgvBrand_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtgvBrand.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }

        private void dtgvBrand_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dtgvBrand.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
            }
        }
        private void createEditI(string tittle, List<string> row)
        {
            flag = false;
            createGB();
            gbChange.Text = tittle;
            btnSave.Text = "Sửa";
            btnCancel.Text = "Hủy";
            txtIDTHUONGHIEU.Text = row[0];
            txtIDTHUONGHIEU.Enabled = false;
            txtTenTHUONGHIEU.Text = row[1];
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
            gbBrand.Hide();

            gbChange = new GroupBox();
            gbChange.Size = new System.Drawing.Size(733, 455);
            gbChange.Font = new Font("Microsoft Sans Serif", 9.75f);
            gbChange.ForeColor = Color.White;
            gbChange.Dock = DockStyle.Bottom;

            Label lblIDTHUONGHIEU = new Label();
            lblIDTHUONGHIEU.Text = "Mã thương hiệu:";
            lblIDTHUONGHIEU.Location = new Point(20, 70);
            lblIDTHUONGHIEU.AutoSize = true;
            txtIDTHUONGHIEU = new TextBox();
            txtIDTHUONGHIEU.Location = new Point(150, 70);
            txtIDTHUONGHIEU.Width = 200;
            gbChange.Controls.Add(lblIDTHUONGHIEU);
            gbChange.Controls.Add(txtIDTHUONGHIEU);

            Label lblTenTHUONGHIEU = new Label();
            lblTenTHUONGHIEU.Text = "Tên thương hiệu:";
            lblTenTHUONGHIEU.Location = new Point(20, 130);
            lblTenTHUONGHIEU.AutoSize = true;
            txtTenTHUONGHIEU = new TextBox();
            txtTenTHUONGHIEU.Location = new Point(150, 130);
            txtTenTHUONGHIEU.Width = 200;
            gbChange.Controls.Add(lblTenTHUONGHIEU);
            gbChange.Controls.Add(txtTenTHUONGHIEU);

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
        }
        private List<string> getText()
        {
            List<string> lt = new List<string>();
            lt.Add(txtIDTHUONGHIEU.Text);
            lt.Add(txtTenTHUONGHIEU.Text);
            return lt;
        }

        private bool validate_Product()
        {
            if (string.IsNullOrWhiteSpace(txtIDTHUONGHIEU.Text))
            {
                MessageBox.Show("Vui lòng nhập ID thương hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIDTHUONGHIEU.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenTHUONGHIEU.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên thương hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTHUONGHIEU.Focus();
                return false;
            }
            return true;
        }

        private async Task btnSave_Click(object sender, EventArgs e)
        {
            if (validate_Product())
            {
                List<String> lt = getText();
                THUONGHIEU THUONGHIEU = new THUONGHIEU
                {
                    ID_ThuongHieu = int.Parse(lt[0]),
                    TenThuongHieu  = lt[1],
                };
                if (flag == true)
                {
                    var result = await th.AddTHUONGHIEU(THUONGHIEU);

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Thêm thương hiệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show($"Đã xảy ra lỗi khi thêm thương hiệu: {result.ErrorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    var result = await th.UpdateTHUONGHIEU(THUONGHIEU);
                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Sửa thương hiệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show($"Đã xảy ra lỗi khi sửa thương hiệu: {result.ErrorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            gbChange.Hide();
            gbBrand.Show();
            loadDatagridview(null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            createAddI("Thêm thương hiệu");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dtgvBrand.SelectedCells.Count > 0)
            {
                DataGridViewCell selectedCell = dtgvBrand.SelectedCells[0];
                DataGridViewRow selectedRow = selectedCell.OwningRow;

                List<string> selectedRowData = new List<string>();

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    selectedRowData.Add(cell.Value?.ToString() ?? string.Empty);
                }

                createEditI("Sửa thương hiệu", selectedRowData);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một ô để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dtgvBrand.SelectedCells.Count > 0)
            {
                DataGridViewCell selectedCell = dtgvBrand.SelectedCells[0];
                DataGridViewRow selectedRow = selectedCell.OwningRow;

                if (int.TryParse(selectedRow.Cells[0].Value?.ToString(), out int brandId))
                {
                    DialogResult confirmResult = MessageBox.Show(
                        "Bạn có chắc chắn muốn xóa thương hiệu này?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirmResult == DialogResult.Yes)
                    {
                        bool isDeleted = await th.DeleteTHUONGHIEU(brandId);

                        if (isDeleted)
                        {
                            MessageBox.Show("Xóa thương hiệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            loadDatagridview(null);
                        }
                        else
                        {
                            MessageBox.Show("Xóa thương hiệu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xác định ID của thương hiệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một ô để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            List<THUONGHIEU> thuonghieu = await th.SearchTHUONGHIEUByName(search);

            loadDatagridview(thuonghieu);
        }
    }
}
