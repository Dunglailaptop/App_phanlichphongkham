using DevExpress.XtraEditors;
using HospitalDataLibrarys.Data;
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
    public partial class Frm_Examination : DevExpress.XtraEditors.XtraForm
    {
        private readonly ExaminationPresenter examinationPresenter;
        public Frm_Examination()
        {

            InitializeComponent();
           examinationPresenter = new ExaminationPresenter();
            loadmainAsync();
        }
        public async Task loadmainAsync()
        {
            var result = await Contants.ConvertToDataTableAsync(examinationPresenter.GetListAsync());
            gridControl1.DataSource= result;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var list = new List<Examination>
            {
                new Examination { Examination_Code = "Ca 1", Name = "Sáng",StartTime = new TimeSpan(7, 30, 0), EndTime = new TimeSpan(10, 30, 0) },
                new Examination { Examination_Code = "Ca 2", Name = "Trưa",StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 50, 0) },
                new Examination { Examination_Code = "Ca 3", Name = "Chiều",StartTime = new TimeSpan(13, 30, 0), EndTime = new TimeSpan(15, 30, 0) },
                new Examination { Examination_Code = "Ca 4", Name = "Tối",StartTime = new TimeSpan(16, 30, 0), EndTime = new TimeSpan(18, 40, 0) },
            };
            bool success = examinationPresenter.inesrt(list);
            loadmainAsync();
            simpleButton1.Enabled = false;
        }
    }
}