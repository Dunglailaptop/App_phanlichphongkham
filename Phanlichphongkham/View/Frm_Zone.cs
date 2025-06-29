using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Phanlichphongkham.Helper;
using Phanlichphongkham.Presenters;
using Phanlichphongkham.View.Views;
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
    public partial class Frm_Zone : DevExpress.XtraEditors.XtraForm
    {
  
        private readonly ZonePresenter zonePresenter;
        public Frm_Zone()
        {
            InitializeComponent();
            zonePresenter = new ZonePresenter();
            setupmain();

        }

        public void setupmain()
        {
            try
            {
                loadData();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddNew(string Name, string Address, int Idprogresql)
        {
            try
            {
                var ZoneNew = new Model.Zone();
                ZoneNew.Name = Name;
                ZoneNew.Address = Address;
                ZoneNew.Zone_Id_posgres = Idprogresql;
                ZoneNew.DateUpdate = DateTime.Now;
                ZoneNew.DateCreate = DateTime.Now;
                ZoneNew.Enable = true;
                ZoneNew.Zone_code = Contants.RemoveVietnameseAndSpaces(ZoneNew.Name);
                bool check = zonePresenter.AddNew(ZoneNew);

                return check;

            }
            catch (Exception ex)
            {

                return false;
            }

        }
        public async Task loadData()
        {
            List<Model.Zone> datares = await zonePresenter.GetListAsync();
            if (datares != null && datares.Count > 0)
            {
                gridControl1.DataSource = datares;
            }
            else
            {
                gridControl1.DataSource = new List<Model.Zone>
                {
                    new Model.Zone
                    {
                        Zone_Id = 0,
                        Name = "",
                        Address = "",
                        Zone_code = "",
                        Enable = true,
                        Zone_Id_posgres = 0,
                        DateCreate = DateTime.Now,
                        DateUpdate = DateTime.Now,
                    }


                };
            }

          
        }


        private void Frm_Zone_Load(object sender, EventArgs e)
        {
          setupmain();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtName.Text != null && txtAddress.Text != null && txtIdProgresql.Text != null)
            {
                AddNew(txtName.Text, txtAddress.Text,Convert.ToInt32(txtIdProgresql.Text));
                loadData();
            }
        }
    }
}