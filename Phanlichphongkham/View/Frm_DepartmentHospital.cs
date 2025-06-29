using DevExpress.XtraEditors;
using Phanlichphongkham.Helper;
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
    public partial class Frm_DepartmentHospital : DevExpress.XtraEditors.XtraForm
    {
        private readonly DepartmentHospitalPresenter presenter;
        public Frm_DepartmentHospital()
        {
            InitializeComponent();
            presenter = new DepartmentHospitalPresenter();
           
            loadmain();
        }
        public async void loadmain()
        {
            var result = await Contants.ConvertToDataTableAsync(presenter.GetListAsync());
            gridControl1.DataSource = result;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            presenter.UpdateDatabasePostgresqlWithLogging();
            loadmain();
        }
    }
}