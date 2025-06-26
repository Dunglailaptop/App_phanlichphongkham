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
using Calendar = System.Globalization.Calendar;

namespace Phanlichphongkham.View
{
    public partial class Frm_main : DevExpress.XtraEditors.XtraForm
    {

        private BindingList<DepartmentalAppointmentScheduling> modelList; // Tha
        private readonly Service_DepartmentalAppointmentScheduling _DepartmentalAppointmentScheduling;
        public Frm_main()
        {

            _DepartmentalAppointmentScheduling = new Service_DepartmentalAppointmentScheduling();
            InitializeComponent();
            loadmain();
            setuptreelist();
            SetupGridControl();
        }



        public void setuptreelist()
        {
            var menuList = new List<MenuItem>
                {
                  new MenuItem { Id = 1, ParentId = null, Title = "Chuyên khoa" },
                  new MenuItem { Id = 2, ParentId = null, Title = "Bác sĩ" },
                  new MenuItem { Id = 3, ParentId = null, Title = "Danh mục phòng khám" },
                  new MenuItem { Id = 65, ParentId = 3, Title = "khu nguyễn Du" },
                  new MenuItem { Id = 68, ParentId = 3, Title = "khu chất lượng cao" },
                  new MenuItem { Id = 66, ParentId = 3, Title = "khu SKTE" },
                  new MenuItem { Id = 4, ParentId = null, Title = "Slot thời gian" },
                  new MenuItem { Id = 5, ParentId = null, Title = "Ca khám" },
                  new MenuItem { Id = 6, ParentId = null, Title = "Khu khám" },
                };

            treeList1.DataSource = menuList;
            treeList1.KeyFieldName = "Id";
            treeList1.ParentFieldName = "ParentId";
            treeList1.Columns["Title"].Caption = "Chức năng";
            treeList1.ExpandAll();
        }
        private void LoadFormIntoPanel(Form form)
        {
            panelControl1.Controls.Clear();            // Xoá form cũ
            form.TopLevel = false;                 // Cần thiết khi nhúng vào control
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;            // Fit đầy Panel
            panelControl1.Controls.Add(form);
            form.Show();
        }

        public async void SetupGridControl()
        {
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false; // Cho phép di chuyển focu


            loadcomboboxDoctorAsync();
            loadcomboboxSpecialtyAsync();

        }
        private void GetYearWeekAndDays(int year, int weekNumber, out List<string> weekDays)
        {
            // Tính ngày bắt đầu của tuần dựa trên year và weekNumber
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var calendarWeek = calendar.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            DateTime startOfWeek = firstThursday.AddDays((weekNumber - calendarWeek) * 7).AddDays(-(int)firstThursday.DayOfWeek + 1);

            // Tạo danh sách 7 ngày trong tuần và chuyển sang tiếng Việt
            weekDays = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                DateTime day = startOfWeek.AddDays(i);
                string vietnameseDay = GetVietnameseDayOfWeek(day.DayOfWeek);
                weekDays.Add($"{vietnameseDay}, {day:dd/MM/yyyy}");
            }
        }

        private string GetVietnameseDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: return "Thứ 2";
                case DayOfWeek.Tuesday: return "Thứ 3";
                case DayOfWeek.Wednesday: return "Thứ 4";
                case DayOfWeek.Thursday: return "Thứ 5";
                case DayOfWeek.Friday: return "Thứ 6";
                case DayOfWeek.Saturday: return "Thứ 7";
                case DayOfWeek.Sunday: return "Chủ nhật";
                default: return "Không xác định";
            }
        }

        private void DisplayYearWeekAndDays(int year,int weekNumber)
        {
            
            GetYearWeekAndDays(year, weekNumber, out List<string> weekDays);
            Console.WriteLine($"Năm: {year}");
            Console.WriteLine($"Tuần thứ: {weekNumber}");
            Console.WriteLine("Danh sách ngày trong tuần:");
            foreach (var day in weekDays)
            {
                Console.WriteLine(day);
            }
        }
        public async Task loadcomboboxRoom()
        {
            RepositoryItemComboBox repoComboBoxDoctor = new RepositoryItemComboBox();
            var doctorList = new List<ComboBoxItem>();
            List<DepartmentHospital> listDoctor = await _DepartmentalAppointmentScheduling.GetListRoom(65);
            if (listDoctor != null) // Giả sử Ma là List<int> hoặc int[]
            {
                foreach (DepartmentHospital item in listDoctor)
                {
                    doctorList.Add(new ComboBoxItem { Id = item.idkhoaphong.ToString(), Name = item.tenkhoaphong }); // Tạo Name động từ Id
                }
            }
            repoComboBoxDoctor.Items.AddRange(doctorList.ToArray());
            repoComboBoxDoctor.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            gridControl1.RepositoryItems.Add(repoComboBoxDoctor);
            gridView1.Columns["Room_Id"].ColumnEdit = repoComboBoxDoctor;
        }
        public async Task loadcomboboxDoctorAsync()
        {
            RepositoryItemComboBox repoComboBoxDoctor = new RepositoryItemComboBox();
            var doctorList = new List<ComboBoxItem>();
            List<Doctor> listDoctor = await _DepartmentalAppointmentScheduling.GetListDoctor();
            if (listDoctor != null) // Giả sử Ma là List<int> hoặc int[]
            {
                foreach (Doctor item in listDoctor)
                {
                    doctorList.Add(new ComboBoxItem { Id = item.manhanvien.ToString(), Name = item.tennhanvien }); // Tạo Name động từ Id
                }
            }
            repoComboBoxDoctor.Items.AddRange(doctorList.ToArray());
            repoComboBoxDoctor.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            gridControl1.RepositoryItems.Add(repoComboBoxDoctor);
            gridView1.Columns["Doctor_Id"].ColumnEdit = repoComboBoxDoctor;
        }
        public async Task loadcomboboxSpecialtyAsync()
        {
            RepositoryItemComboBox repoComboBoxDoctor = new RepositoryItemComboBox();
            var doctorList = new List<ComboBoxItem>();
            List<Specialty> listDoctor = await _DepartmentalAppointmentScheduling.GetListSpecialty();
            if (listDoctor != null) // Giả sử Ma là List<int> hoặc int[]
            {
                foreach (Specialty item in listDoctor)
                {
                    doctorList.Add(new ComboBoxItem { Id = item.idnhomphongkham.ToString(), Name = item.tennhomphongkham }); // Tạo Name động từ Id
                }
            }
            repoComboBoxDoctor.Items.AddRange(doctorList.ToArray());
            repoComboBoxDoctor.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            gridControl1.RepositoryItems.Add(repoComboBoxDoctor);
            gridView1.Columns["Specialty_Id"].ColumnEdit = repoComboBoxDoctor;
        }
        public void loadmain()
        {
            modelList = new BindingList<DepartmentalAppointmentScheduling>();

            // Thêm dữ liệu mẫu hoặc load từ DB
            modelList.Add(new DepartmentalAppointmentScheduling
            {
                Room_Id = 1,
                Specialty_Id = 1,
                DateInWeek = DateTime.Now
            });

            gridControl1.DataSource = modelList;

        }

        public void keyAddnew()
        {
            // Lấy giá trị của các cột từ hàng hiện tại
            DateTime dateInWeek = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateInWeek"));
            int roomId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Room_Id"));
            int specialtyId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Specialty_Id"));
            int doctorId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Doctor_Id"));

            // Thêm dữ liệu mẫu hoặc load từ DB
            modelList.Add(new DepartmentalAppointmentScheduling
            {
                Room_Id = 1,
                Specialty_Id = 1,
                DateInWeek = DateTime.Now
            });
            gridControl1.DataSource = modelList;
        }




        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Frm_main_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            loadmain();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null) return;

            int title = Convert.ToInt32(e.Node.GetValue("Id")?.ToString());

            switch (title)
            {
                case 1:
                    LoadFormIntoPanel(new Frm_Specialty());  // hoặc ShowDialog() nếu muốn modal
                    break;

                case 2:
                    LoadFormIntoPanel(new Frm_Doctor());  // hoặc ShowDialog() nếu muốn modal
                    break;
                case 3:
                    LoadFormIntoPanel(new Frm_Room(0));  // hoặc ShowDialog() nếu muốn modal
                    break;
                case 65:
                    LoadFormIntoPanel(new Frm_Room(65));  // hoặc ShowDialog() nếu muốn modal
                    break;
                case 68:
                    LoadFormIntoPanel(new Frm_Room(68));  // hoặc ShowDialog() nếu muốn modal
                    break;
                case 66:
                    LoadFormIntoPanel(new Frm_Room(66));  // hoặc ShowDialog() nếu muốn modal
                    break;
                case 4:
                    LoadFormIntoPanel(new Frm_SlotTime());  // hoặc ShowDialog() nếu muốn modal
                    break;
                case 5:

                    break;
                default:
                    XtraMessageBox.Show("Chức năng đang phát triển", "Thông báo");
                    break;
            }
        }

        private void gridView1_GotFocus(object sender, EventArgs e)
        {

        }

        public class MenuItem
        {
            public int Id { get; set; }
            public int? ParentId { get; set; }  // NULL nếu là node gốc
            public string Title { get; set; }
        }
        public class ComboBoxItem
        {
            public string Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && !gridView1.IsNewItemRow(gridView1.FocusedRowHandle))
            {
                keyAddnew();
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
            DisplayYearWeekAndDays(Convert.ToInt32(textEdit1.EditValue), Convert.ToInt32(textEdit2.EditValue));
        }
    }
}