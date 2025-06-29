using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using HospitalDataLibrarys.Models;
using HospitalDataLibrarys.Services;
using Phanlichphongkham.Controller;
using Phanlichphongkham.Helper;
using Phanlichphongkham.Model;
using Phanlichphongkham.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using DepartmentHospital = Phanlichphongkham.Model.DepartmentHospital;
using Doctor = Phanlichphongkham.Model.Doctor;
using Specialty = Phanlichphongkham.Model.Sepicalty;

namespace Phanlichphongkham.View.Views
{
    public class View_FrmMain
    {
        public int idzone = 1;
        public TreeList treeList1 { get; set; }
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1 { get; set; }
        public GridControl gridControl1 { get; set; }
        public PanelControl panelControl1 { get; set; }

        public ComboBoxEdit comboBoxEdit1 { get; set; }
        private BindingList<DepartmentalAppointmentScheduling> modelList; // Tha
        private readonly DepartmentAppointmentSchedulingPresenter _DepartmentalAppointmentScheduling;
        private readonly SpecialtyPresenter _SpecialtyPresenter;
        private readonly DoctorPresenter _DoctorPresenter;
        private readonly RoomPresenter _RoomPresenter;
        private readonly ExaminationPresenter examinationPresenter;
        private readonly DepartmentHospitalPresenter DepartmentHospitalPresenter;
        private readonly ZonePresenter zonePresenter;
        public View_FrmMain()
        {
            _DepartmentalAppointmentScheduling = new DepartmentAppointmentSchedulingPresenter();
            _SpecialtyPresenter = new SpecialtyPresenter();
            _DoctorPresenter = new DoctorPresenter();
            _RoomPresenter = new RoomPresenter();
            examinationPresenter = new ExaminationPresenter();
            DepartmentHospitalPresenter = new DepartmentHospitalPresenter();
            zonePresenter = new ZonePresenter();
        }
        public void LoadFormIntoPanel(Form form)
        {
            panelControl1.Controls.Clear();            // Xoá form cũ
            form.TopLevel = false;                 // Cần thiết khi nhúng vào control
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;            // Fit đầy Panel
            panelControl1.Controls.Add(form);
            form.Show();
        }
        public void main_setup()
        {
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
                  new MenuItem { Id = 4, ParentId = null, Title = "Khoa phòng bệnh viện" },
                  new MenuItem { Id = 5, ParentId = null, Title = "Khu khám" },
                  new MenuItem { Id = 6, ParentId = null, Title = "Ca khám" },
                };

            treeList1.DataSource = menuList;
            treeList1.KeyFieldName = "Id";
            treeList1.ParentFieldName = "ParentId";
            treeList1.Columns["Title"].Caption = "Chức năng";
            treeList1.ExpandAll();
        }


        public async void SetupGridControl()
        {
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false; // Cho phép di chuyển focu


            // Hoặc nếu muốn hiển thị nhưng không cho edit cho đến khi valid
            gridView1.OptionsView.ShowNewItemRow = true;



            loadcomboboxDoctorAsync();
            loadcomboboxSpecialtyAsync();
            loadcomboboxRoom(idzone);
            loadcomboboxExaminationAsync();
            loadcomboboxDepartmentHospitalAsync();
            loadzone();
            
        }
       

        public async void CreateNewDepartmentAppointmentScheduling()
        {
            Console.WriteLine(modelList);
             _DepartmentalAppointmentScheduling.AddNew(modelList);
        }


        public int? GetSelectedZoneId(ComboBoxEdit comboBoxEdit)
        {
            if (comboBoxEdit.SelectedIndex >= 0 && comboBoxEdit.Tag is List<Model.Zone> dataList)
            {
                if (comboBoxEdit.SelectedIndex < dataList.Count)
                {
                    var id = dataList[comboBoxEdit.SelectedIndex].Zone_Id;
                    idzone = id;
                    main_setup();
                    return dataList[comboBoxEdit.SelectedIndex].Zone_Id;
                }
            }
            return null;
        }



        public async void loadzone()
        {

            List<Model.Zone> zonelist = await zonePresenter.GetListAsync();
            SetComboBoxEditDataSimple(comboBoxEdit1, zonelist);

        }
       

        public void SetComboBoxEditDataSimple(ComboBoxEdit comboBoxEdit, List<Model.Zone> dataList)
        {
            if (comboBoxEdit == null) return;

            // Xóa các mục cũ
            comboBoxEdit.Properties.Items.Clear();

            // Lưu danh sách gốc vào Tag để dễ truy xuất
            comboBoxEdit.Tag = dataList;

            // Thêm các mục từ danh sách (chỉ hiển thị Name)
            if (dataList != null && dataList.Count > 0)
            {
                foreach (var item in dataList)
                {
                    comboBoxEdit.Properties.Items.Add(item.Name);
                }
            }

            // Cấu hình ComboBox
            comboBoxEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // Reset selection
            comboBoxEdit.SelectedIndex = -1;
            comboBoxEdit.EditValue = null;
        }
        public async Task loadcomboboxRoom(int idzone)
        {
            RepositoryItemLookUpEdit repoLookUpEdit = new RepositoryItemLookUpEdit();
            var roomList = new List<ComboBoxItem>();

            List<Model.Room> listDoctor = await _RoomPresenter.GetListAsyncWithId(idzone);
            if (listDoctor != null)
            {
                foreach (Model.Room item in listDoctor)
                {
                    roomList.Add(new ComboBoxItem
                    {
                        Id = item.Room_Id.ToString(), // Sử dụng kiểu dữ liệu phù hợp (int hoặc string)
                        Name = item.Name
                    });
                }
            }

            // Cấu hình DataSource cho LookUpEdit
            repoLookUpEdit.DataSource = roomList;
            repoLookUpEdit.ValueMember = "Id"; // Giá trị thực (ID)
            repoLookUpEdit.DisplayMember = "Name"; // Văn bản hiển thị
            repoLookUpEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // Thêm vào repository và gán cho cột
            gridControl1.RepositoryItems.Add(repoLookUpEdit);
            gridView1.Columns["Room_Id"].ColumnEdit = repoLookUpEdit;
        }
        public async Task loadcomboboxDoctorAsync()
        {
            RepositoryItemLookUpEdit repoLookUpEdit = new RepositoryItemLookUpEdit();
            var doctorList = new List<ComboBoxItem>();

            List<Doctor> listDoctor = await _DoctorPresenter.GetListAsync();
            if (listDoctor != null)
            {
                foreach (Doctor item in listDoctor)
                {
                    doctorList.Add(new ComboBoxItem
                    {
                        Id = item.Doctor_Id.ToString(), // Use int directly if manhanvien is int
                        Name = item.Name
                    });
                }
            }

            // Configure DataSource for LookUpEdit
            repoLookUpEdit.DataSource = doctorList;
            repoLookUpEdit.ValueMember = "Id"; // Value to store (e.g., manhanvien)
            repoLookUpEdit.DisplayMember = "Name"; // Text to display (e.g., tennhanvien)
            repoLookUpEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // Add to repository and assign to the column
            gridControl1.RepositoryItems.Add(repoLookUpEdit);
            gridView1.Columns["Doctor_Id"].ColumnEdit = repoLookUpEdit;
        }
        public async Task loadcomboboxSpecialtyAsync()
        {
            RepositoryItemLookUpEdit repoLookUpEdit = new RepositoryItemLookUpEdit();
            var specialtyList = new List<ComboBoxItem>();

            List<Specialty> listSpecialty = await _SpecialtyPresenter.GetListAsync();
            if (listSpecialty != null)
            {
                foreach (Specialty item in listSpecialty)
                {
                    specialtyList.Add(new ComboBoxItem
                    {
                        Id = item.Sepicalty_Id.ToString(), // Sử dụng int trực tiếp nếu Specialty_Id là int
                        Name = item.Name
                    });
                }
            }

            // Cấu hình DataSource cho LookUpEdit
            repoLookUpEdit.DataSource = specialtyList;
            repoLookUpEdit.ValueMember = "Id"; // Giá trị thực (ID)
            repoLookUpEdit.DisplayMember = "Name"; // Văn bản hiển thị
            repoLookUpEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // Thêm vào repository và gán cho cột
            gridControl1.RepositoryItems.Add(repoLookUpEdit);
            gridView1.Columns["Specialty_id"].ColumnEdit = repoLookUpEdit;
        }
        public async Task loadcomboboxExaminationAsync()
        {
            RepositoryItemLookUpEdit repoLookUpEdit = new RepositoryItemLookUpEdit();
            var specialtyList = new List<ComboBoxItem>();

            List<Examination> listSpecialty = await examinationPresenter.GetListAsync();
            if (listSpecialty != null)
            {
                foreach (Examination item in listSpecialty)
                {
                    specialtyList.Add(new ComboBoxItem
                    {
                        Id = item.Examination_Id.ToString(), // Sử dụng int trực tiếp nếu Specialty_Id là int
                        Name = item.Name + "(" + item.StartTime.ToString() + "-" + item.EndTime.ToString() + ")",
                    });
                }
            }

            // Cấu hình DataSource cho LookUpEdit
            repoLookUpEdit.DataSource = specialtyList;
            repoLookUpEdit.ValueMember = "Id"; // Giá trị thực (ID)
            repoLookUpEdit.DisplayMember = "Name"; // Văn bản hiển thị
            repoLookUpEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // Thêm vào repository và gán cho cột
            gridControl1.RepositoryItems.Add(repoLookUpEdit);
            gridView1.Columns["Examination_Id"].ColumnEdit = repoLookUpEdit;
        }
        public async Task loadcomboboxDepartmentHospitalAsync()
        {
            RepositoryItemLookUpEdit repoLookUpEdit = new RepositoryItemLookUpEdit();
            var specialtyList = new List<ComboBoxItem>();

            List<DepartmentHospital> listSpecialty = await DepartmentHospitalPresenter.GetListAsync();
            if (listSpecialty != null)
            {
                foreach (DepartmentHospital item in listSpecialty)
                {
                    specialtyList.Add(new ComboBoxItem
                    {
                        Id = item.DepartmentHospital_Id.ToString(), // Sử dụng int trực tiếp nếu Specialty_Id là int
                        Name = item.Name,
                    });
                }
            }

            // Cấu hình DataSource cho LookUpEdit
            repoLookUpEdit.DataSource = specialtyList;
            repoLookUpEdit.ValueMember = "Id"; // Giá trị thực (ID)
            repoLookUpEdit.DisplayMember = "Name"; // Văn bản hiển thị
            repoLookUpEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // Thêm vào repository và gán cho cột
            gridControl1.RepositoryItems.Add(repoLookUpEdit);
            gridView1.Columns["DepartmentHospital_Id"].ColumnEdit = repoLookUpEdit;
        }

        // Phương án 1: Setup trong Designer hoặc Form Load
     

        public async void loadmain()
        {
            DateTime now = DateTime.Now;
            DateInfo dateDetails = DateHelper.GetDateDetails(now);
            var result = await _DepartmentalAppointmentScheduling.GetListAsync();
            modelList = new BindingList<DepartmentalAppointmentScheduling>(result.ToList());
            gridControl1.DataSource = null;
            gridControl1.DataSource = modelList;
            gridControl1.RefreshDataSource();
        }



        public void keyAddnew()
        {
            // Kiểm tra xem có dòng nào được focus không
            if (gridView1.FocusedRowHandle < 0)
                return;

            try
            {
                // Lấy giá trị của các cột từ hàng hiện tại
                DateTime dateInWeek = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateInWeek"));
                int roomId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Room_Id"));
                int specialtyId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Specialty_Id"));
                int doctorId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Doctor_Id"));

                // Thêm 1 ngày vào ngày hiện tại để tạo dòng mới
                DateTime nextDate = dateInWeek.AddDays(1);
                DateInfo dateDetails = DateHelper.GetDateDetails(nextDate);

                // Thêm dữ liệu mới
                modelList.Add(new DepartmentalAppointmentScheduling
                {
                    DepartmentalAppointmentScheduling_Id = 0,
                    Year = dateDetails.Year,
                    Week = dateDetails.WeekOfYear,
                    DayInWeek = dateDetails.DayOfWeekName,
                    DateInWeek = dateDetails.InputDate,
                    Total = 10,
                    Status = true,
                    Specialty_id = specialtyId,
                    Room_Id = roomId,
                    Examination_Id = 0,
                    Doctor_Id = doctorId,
                    DepartmentHospital_Id = 0,
                    Username = "admin",
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now
                });

                // Refresh grid
                gridControl1.RefreshDataSource();

                // Focus vào dòng mới
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                gridView1.SelectRow(gridView1.FocusedRowHandle);

                // Focus vào cột DateInWeek để người dùng có thể chỉnh sửa ngay
                gridView1.FocusedColumn = gridView1.Columns["DateInWeek"];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}");
            }
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
        // Class để lưu thông tin ngày trong tuần
        public class WeekDayItem
        {
            public DateTime Date { get; set; }
            public string DisplayText { get; set; }
            public string DayName { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }

    }
}
