using DevExpress.XtraEditors;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phanlichphongkham.View
{
    public partial class Frm_Doctor : DevExpress.XtraEditors.XtraForm
    {
        private readonly Service_DepartmentalAppointmentScheduling _DepartmentalAppointmentScheduling;
        private readonly DoctorPresenter doctorPresenter;
        public Frm_Doctor()
        {
            doctorPresenter = new DoctorPresenter();
            _DepartmentalAppointmentScheduling = new Service_DepartmentalAppointmentScheduling();
            InitializeComponent();
            loadmain();
        }
        public async void loadmain()
        {
            var result = await Contants.ConvertToDataTableAsync(doctorPresenter.GetListAsync());
            gridControl1.DataSource = result;
        }
        private void Frm_Doctor_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            doctorPresenter.UpdateDatabasePostgresqlWithLogging();
            loadmain();
        }
    }
}