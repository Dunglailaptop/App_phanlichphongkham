using DevExpress.Mvvm.POCO;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using HospitalDataLibrarys.Services;
using Phanlichphongkham.Helper;
using Phanlichphongkham.Model;
using Phanlichphongkham.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phanlichphongkham.View
{
    public partial class Frm_Room : DevExpress.XtraEditors.XtraForm
    {
        private readonly ZonePresenter zonePresenter;
        private readonly RoomPresenter roomPresenter;
        private readonly Service_DepartmentalAppointmentScheduling _DepartmentalAppointmentScheduling;
        public Frm_Room()
        {
            zonePresenter = new ZonePresenter();
            roomPresenter = new RoomPresenter();
            _DepartmentalAppointmentScheduling = new Service_DepartmentalAppointmentScheduling();
            InitializeComponent();
            loadmain();
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
        public int? GetSelectedZoneId(ComboBoxEdit comboBoxEdit)
        {
            if (comboBoxEdit.SelectedIndex >= 0 && comboBoxEdit.Tag is List<Model.Zone> dataList)
            {
                if (comboBoxEdit.SelectedIndex < dataList.Count)
                {
                    var id = dataList[comboBoxEdit.SelectedIndex].Zone_Id;
                    loadmainWith(id);
                    return dataList[comboBoxEdit.SelectedIndex].Zone_Id;
                }
            }
            return null;
        }

        public async void loadmain()
        {
            var result = await Contants.ConvertToDataTableAsync(roomPresenter.GetListAsync());
            gridControl1.DataSource = result;
            List<Model.Zone> zonelist = await zonePresenter.GetListAsync();
            SetComboBoxEditDataSimple(comboBoxEdit1, zonelist);

        }
        public async void loadmainWith(int Zone_id)
        {
            var result = await Contants.ConvertToDataTableAsync(roomPresenter.GetListAsyncWithId(Zone_id));
            gridControl1.DataSource = result;
        }

        private void Frm_Room_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedZoneId(comboBoxEdit1);
        }

        private async void btnUpdateWithPosgresql_Click(object sender, EventArgs e)
        {
            bool check = await roomPresenter.UpdateDatabasePostgresqlWithLogging();
            if (check)
            {
                loadmain();
            }
            else
            {
                MessageBox.Show("không thành công");
            }

        }
    }
}