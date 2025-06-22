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

namespace Phanlichphongkham.View
{
    public partial class Frm_main : DevExpress.XtraEditors.XtraForm
    {
        private readonly DepartmentAppointmentSchedulingController _DepartmentalAppointmentScheduling;
        public Frm_main()
        {
            _DepartmentalAppointmentScheduling = new DepartmentAppointmentSchedulingController();

            InitializeComponent();
            loadmain();
            setuptreelist();
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


        public void loadmain()
        {
            var result = _DepartmentalAppointmentScheduling.getList();
            gridControl1.DataSource = result;
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

            int title =Convert.ToInt32(e.Node.GetValue("Id")?.ToString());

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
    }
    public class MenuItem
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }  // NULL nếu là node gốc
        public string Title { get; set; }
    }
}