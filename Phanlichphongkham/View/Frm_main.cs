using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalDataLibrarys.Services;
using Phanlichphongkham.Model;
using System.Windows.Controls;
using Phanlichphongkham.Helper;
using Phanlichphongkham.Controller;
using System.Collections.ObjectModel;
using DevExpress.XtraScheduler.Commands;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using System.Windows.Threading;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using HospitalDataLibrarys.Models;
using System.Globalization;
using HospitalDataLibrarys.Models;
using Doctor = HospitalDataLibrarys.Models.Doctor;
using DepartmentHospital = HospitalDataLibrarys.Models.DepartmentHospital;
using Phanlichphongkham.View.Views;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace Phanlichphongkham.View
{
    public partial class Frm_main : DevExpress.XtraEditors.XtraForm
    {


        private readonly View_FrmMain _FrmMain;
        public Frm_main()
        {
            InitializeComponent();
            _FrmMain = new View_FrmMain();
            _FrmMain.treeList1 = treeList1;
            _FrmMain.gridView1 = gridView1;
            _FrmMain.gridControl1 = gridControl1;
            _FrmMain.panelControl1 = panelControl1;
            _FrmMain.comboBoxEdit1 = comboBoxEdit1;
            _FrmMain.main_setup();
            DateTime now = DateTime.Now;
            DateInfo dateDetails = DateHelper.GetDateDetails(now);
            textEdit1.Text = dateDetails.Year.ToString();
            textEdit2.Text = dateDetails.WeekOfYear.ToString();
        }
        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Frm_main_Load(object sender, EventArgs e)
        {
            SetupDateColumnAdvanced();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _FrmMain.loadmain();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null) return;

            int title = Convert.ToInt32(e.Node.GetValue("Id")?.ToString());

            switch (title)
            {
                case 1:
                    _FrmMain.LoadFormIntoPanel(new Frm_Specialty());  // hoặc ShowDialog() nếu muốn modal
                    break;
                case 2:
                    _FrmMain.LoadFormIntoPanel(new Frm_Doctor());  // hoặc ShowDialog() nếu muốn modal
                    break;
                case 3:
                    _FrmMain.LoadFormIntoPanel(new Frm_Room());  // hoặc ShowDialog() nếu muốn modal
                    break;
                case 4:
                    _FrmMain.LoadFormIntoPanel(new Frm_DepartmentHospital());  // hoặc ShowDialog() nếu muốn modal
                    break;
                case 5:
                    _FrmMain.LoadFormIntoPanel(new Frm_Zone());
                    break;
                case 6:
                    _FrmMain.LoadFormIntoPanel(new Frm_Examination());
                    break;
                default:
                    XtraMessageBox.Show("Chức năng đang phát triển", "Thông báo");
                    break;
            }
        }

        private void gridView1_GotFocus(object sender, EventArgs e)
        {

        }



        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab && !gridView1.IsNewItemRow(gridView1.FocusedRowHandle))
            {
                // Lấy index của cột hiện tại
                int currentColumnIndex = gridView1.FocusedColumn.VisibleIndex;

                // Lấy index của cột cuối cùng (visible)
                int lastColumnIndex = gridView1.VisibleColumns.Count - 1;

                // Chỉ tạo mới khi đang ở cột cuối cùng
                if (currentColumnIndex == lastColumnIndex)
                {
                    _FrmMain.keyAddnew();
                    // Refresh GridView để hiển thị ngay lập tức
                    gridView1.RefreshData();

                    // Focus vào dòng mới và cột đầu tiên
                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    gridView1.FocusedColumn = gridView1.VisibleColumns[0];

                    // Ngăn không cho Tab tiếp tục xử lý mặc định
                    e.Handled = true;
                }
                // Nếu chưa phải cột cuối, để Tab hoạt động bình thường (chuyển sang cột tiếp theo)
            }

        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép số (0-9) và phím Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Loại bỏ ký tự không phải số
                return;
            }

            // Giới hạn tối đa 4 ký tự
            TextEdit textBox = sender as TextEdit;
            if (textBox.Text.Length >= 4 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ngăn nhập thêm nếu đã đủ 4 ký tự
            }
        }

        private void textEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép số (0-9) và phím Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Loại bỏ ký tự không phải số
                return;
            }

            // Giới hạn tối đa 2 ký tự
            TextEdit textBox = sender as TextEdit;
            if (textBox.Text.Length >= 2 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ngăn nhập thêm nếu đã đủ 2 ký tự
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FrmMain.GetSelectedZoneId(comboBoxEdit1);
        }
        // Phương án 1: Sử dụng MinValue và MaxValue của DateEdit
        private void SetDateRangeForWeek(DateEdit dateEdit, DateTime currentDate)
        {
            // Tính ngày đầu tuần (Chủ Nhật)
            DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);

            // Tính ngày cuối tuần (Thứ Bảy)
            DateTime endOfWeek = startOfWeek.AddDays(6);

            // Set giới hạn cho DateEdit
            dateEdit.Properties.MinValue = startOfWeek;
            dateEdit.Properties.MaxValue = endOfWeek;
        }
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;

            // Chỉ xử lý cho cột DateInWeek
            if (view.FocusedColumn.FieldName == "DateInWeek")
            {
                // Lấy ngày hiện tại từ dòng đang edit
                DateTime currentDate = Convert.ToDateTime(view.GetRowCellValue(view.FocusedRowHandle, "DateInWeek"));

                // Lấy DateEdit đang được sử dụng
                if (view.ActiveEditor is DateEdit dateEdit)
                {
                    SetDateRangeForWeek(dateEdit, currentDate);
                }
            }
        }
        private void SetupDateColumnAdvanced()
        {
            GridColumn dateColumn = gridView1.Columns["DateInWeek"];

            if (dateColumn != null)
            {
                // Tạo Repository
                RepositoryItemDateEdit dateEdit = new RepositoryItemDateEdit();

                // Format hiển thị và edit
                dateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dateEdit.DisplayFormat.FormatString = "dd/MM/yyyy";
                dateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dateEdit.EditFormat.FormatString = "dd/MM/yyyy";

                // Mask cho input
                dateEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                dateEdit.Mask.EditMask = "dd/MM/yyyy";
                dateEdit.Mask.UseMaskAsDisplayFormat = true;

                // Các thiết lập cho Calendar
                dateEdit.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.MonthView;
                dateEdit.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.Default;

                // Thiết lập button
                dateEdit.Buttons.Clear();
                dateEdit.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo));

                // Assign cho cột
                dateColumn.ColumnEdit = dateEdit;

                // Format cho cột
                dateColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dateColumn.DisplayFormat.FormatString = "dd/MM/yyyy";
            }
        }
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DateInWeek")
            {
                GridView view = sender as GridView;
                DateTime newDate = Convert.ToDateTime(e.Value);

                // Lấy thông tin ngày mới
                DateInfo dateDetails = DateHelper.GetDateDetails(newDate);

                // Cập nhật các cột khác trong cùng dòng
                view.SetRowCellValue(e.RowHandle, "Year", dateDetails.Year);
                view.SetRowCellValue(e.RowHandle, "Week", dateDetails.WeekOfYear);
                view.SetRowCellValue(e.RowHandle, "DayInWeek", dateDetails.DayOfWeekName);

                // Refresh để hiển thị thay đổi
                view.RefreshRow(e.RowHandle);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            _FrmMain.CreateNewDepartmentAppointmentScheduling();
        }
    }
}