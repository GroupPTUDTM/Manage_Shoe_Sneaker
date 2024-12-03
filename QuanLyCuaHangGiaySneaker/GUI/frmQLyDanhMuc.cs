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
    public partial class frmQLyDanhMuc : Form
    {
        BLL_DANHMUC dm = new BLL_DANHMUC();

        bool flag = false;

        GroupBox gbChange;
        TextBox txtIDDanhMuc;
        TextBox txtTenDanhMuc;
        Button btnSave;
        Button btnCancel;

        public frmQLyDanhMuc()
        {
            InitializeComponent();
            InitializeDatagridview();
            loadDatagridview(null);

            this.dtgvCategory.CellClick += dtgvCategory_CellClick;
            this.dtgvCategory.CellLeave += dtgvCategory_CellLeave;
        }

        private void InitializeDatagridview()
        {
            dtgvCategory.Columns.Clear();

            dtgvCategory.EnableHeadersVisualStyles = false;
            dtgvCategory.ReadOnly = true;
            dtgvCategory.DefaultCellStyle.SelectionBackColor = Color.Teal;
            dtgvCategory.DefaultCellStyle.SelectionForeColor = Color.White;
            dtgvCategory.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
            dtgvCategory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvCategory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dtgvCategory.DefaultCellStyle.ForeColor = Color.Black;

            dtgvCategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private async Task<DataTable> ConvertListToDataTable(List<DANHMUC> danhmucs)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Mã danh mục", typeof(int));
            dataTable.Columns.Add("Tên danh mục", typeof(string));

            foreach (var danhmuc in danhmucs)
            {
                dataTable.Rows.Add(
                    danhmuc.ID_DanhMuc,
                    danhmuc.TenDanhMuc
                );
            }
            return dataTable;
        }

        private async void loadDatagridview(List<DANHMUC> danhmucs)
        {
            dtgvCategory.DataSource = null;

            if (danhmucs == null || danhmucs.Count == 0)
            {
                danhmucs = await dm.getDANHMUCs();
            }
            var dataTable = await ConvertListToDataTable(danhmucs);
            dtgvCategory.DataSource = dataTable;
        }

        private void dtgvCategory_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtgvCategory.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }

        private void dtgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dtgvCategory.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSeaGreen;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            createAddI("Thêm danh mục");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dtgvCategory.SelectedCells.Count > 0)
            {
                DataGridViewCell selectedCell = dtgvCategory.SelectedCells[0];
                DataGridViewRow selectedRow = selectedCell.OwningRow;

                List<string> selectedRowData = new List<string>();

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    selectedRowData.Add(cell.Value?.ToString() ?? string.Empty);
                }

                createEditI("Sửa danh mục", selectedRowData);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một ô để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dtgvCategory.SelectedCells.Count > 0)
            {
                DataGridViewCell selectedCell = dtgvCategory.SelectedCells[0];
                DataGridViewRow selectedRow = selectedCell.OwningRow;

                if (int.TryParse(selectedRow.Cells[0].Value?.ToString(), out int categoryId))
                {
                    DialogResult confirmResult = MessageBox.Show(
                        "Bạn có chắc chắn muốn xóa danh mục này?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirmResult == DialogResult.Yes)
                    {
                        bool isDeleted = await dm.DeleteDANHMUC(categoryId);

                        if (isDeleted)
                        {
                            MessageBox.Show("Xóa danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            loadDatagridview(null);
                        }
                        else
                        {
                            MessageBox.Show("Xóa danh mục thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xác định ID của danh mục!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            List<DANHMUC> danhmuc = await dm.SearchDANHMUCByName(search);

            loadDatagridview(danhmuc);
        }


        private void createEditI(string tittle, List<string> row)
        {
            flag = false;
            createGB();
            gbChange.Text = tittle;
            btnSave.Text = "Sửa";
            btnCancel.Text = "Hủy";
            txtIDDanhMuc.Text = row[0];
            txtIDDanhMuc.Enabled = false;
            txtTenDanhMuc.Text = row[1];
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
            gbCategory.Hide();

            gbChange = new GroupBox();
            gbChange.Size = new System.Drawing.Size(733, 455);
            gbChange.Font = new Font("Microsoft Sans Serif", 9.75f);
            gbChange.ForeColor = Color.White;
            gbChange.Dock = DockStyle.Bottom;

            Label lblIDDanhMuc = new Label();
            lblIDDanhMuc.Text = "Mã danh mục:";
            lblIDDanhMuc.Location = new Point(20, 70);
            txtIDDanhMuc = new TextBox();
            txtIDDanhMuc.Location = new Point(150, 70);
            txtIDDanhMuc.Width = 200;
            gbChange.Controls.Add(lblIDDanhMuc);
            gbChange.Controls.Add(txtIDDanhMuc);

            Label lblTenDanhMuc = new Label();
            lblTenDanhMuc.Text = "Tên danh mục:";
            lblTenDanhMuc.Location = new Point(20, 130);
            txtTenDanhMuc = new TextBox();
            txtTenDanhMuc.Location = new Point(150, 130);
            txtTenDanhMuc.Width = 200;
            gbChange.Controls.Add(lblTenDanhMuc);
            gbChange.Controls.Add(txtTenDanhMuc);

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
            lt.Add(txtIDDanhMuc.Text);
            lt.Add(txtTenDanhMuc.Text);
            return lt;
        }

        private bool validate_Product()
        {
            if (string.IsNullOrWhiteSpace(txtIDDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng nhập ID danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIDDanhMuc.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDanhMuc.Focus();
                return false;
            }
            return true;
        }

        private async Task btnSave_Click(object sender, EventArgs e)
        {
            if (validate_Product())
            {
                List<String> lt = getText();
                DANHMUC DANHMUC = new DANHMUC
                {
                    ID_DanhMuc = int.Parse(lt[0]),
                    TenDanhMuc = lt[1],
                };
                if (flag == true)
                {
                    var result = await dm.AddDANHMUC(DANHMUC);

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Thêm danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show($"Đã xảy ra lỗi khi thêm danh mục: {result.ErrorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    var result = await dm.UpdateDANHMUC(DANHMUC);
                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Sửa danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show($"Đã xảy ra lỗi khi sửa danh mục: {result.ErrorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gbChange.Hide();
            gbCategory.Show();
            loadDatagridview(null);
        }
    }
}
