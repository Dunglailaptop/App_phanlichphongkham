using DevExpress.XtraEditors;
using Phanlichphongkham.Controller;
using Phanlichphongkham.Data;
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
    public partial class Frm_SlotTime : DevExpress.XtraEditors.XtraForm
    {
        private readonly SlotTimeController slotTimeController;
        public Frm_SlotTime()
        {
            slotTimeController = new SlotTimeController();
            InitializeComponent();
            loadmain();
            
        }

        public void loadmain()
        {
            try
            {
                var result = slotTimeController.getlistExmination();
                gridControl1.DataSource = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
     

        private void Frm_SlotTime_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var list = new List<Examination>
            {
                new Examination { Examination_Code = "Ca 1", Name = "Sáng",StartTime = new TimeSpan(7, 30, 0), EndTime = new TimeSpan(10, 30, 0), Total = 0 },
                new Examination { Examination_Code = "Ca 2", Name = "Trưa",StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 50, 0), Total = 0 },
                new Examination { Examination_Code = "Ca 3", Name = "Chiều",StartTime = new TimeSpan(13, 30, 0), EndTime = new TimeSpan(15, 30, 0), Total = 0 },
                new Examination { Examination_Code = "Ca 4", Name = "Tối",StartTime = new TimeSpan(16, 30, 0), EndTime = new TimeSpan(18, 40, 0), Total = 0 },
            };
            bool success = slotTimeController.InsertExaminations(list);
            loadmain();
            simpleButton1.Enabled = false;
        }
    }
}