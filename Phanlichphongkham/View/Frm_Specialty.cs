﻿using DevExpress.XtraEditors;
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
    public partial class Frm_Specialty : DevExpress.XtraEditors.XtraForm
    {
        private readonly SpecialtyPresenter sepicaltyPresenter;
        private readonly Service_DepartmentalAppointmentScheduling _DepartmentalAppointmentScheduling;
        public Frm_Specialty()
        {
            sepicaltyPresenter = new SpecialtyPresenter();
            _DepartmentalAppointmentScheduling = new Service_DepartmentalAppointmentScheduling();
            InitializeComponent();
            loadmainAsync();
        }

        public async Task loadmainAsync()
        {
            var result = await Contants.ConvertToDataTableAsync(sepicaltyPresenter.GetListAsync());
            gridControl1.DataSource = result;
        }
        private void Frm_Specialty_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sepicaltyPresenter.UpdateDatabasePostgresqlWithLogging();
            loadmainAsync();
        }
    }
}