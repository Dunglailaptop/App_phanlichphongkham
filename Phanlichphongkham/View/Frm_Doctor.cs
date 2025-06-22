using DevExpress.XtraEditors;
using HospitalDataLibrarys.Services;
using Phanlichphongkham.Helper;
using Phanlichphongkham.Model;
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
        public Frm_Doctor()
        {
            _DepartmentalAppointmentScheduling = new Service_DepartmentalAppointmentScheduling();
            InitializeComponent();
            loadmain();
        }
        public async void loadmain()
        {
            var result = await Contants.ConvertToDataTableAsync(_DepartmentalAppointmentScheduling.GetListDoctor());
            gridControl1.DataSource = result;
        }
        private void Frm_Doctor_Load(object sender, EventArgs e)
        {

        }
    }
}