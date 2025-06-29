namespace Phanlichphongkham.View
{
    partial class Frm_Zone
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            txtName = new DevExpress.XtraEditors.TextEdit();
            txtAddress = new DevExpress.XtraEditors.TextEdit();
            txtIdProgresql = new DevExpress.XtraEditors.TextEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtAddress.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtIdProgresql.Properties).BeginInit();
            SuspendLayout();
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = DockStyle.Fill;
            splitContainerControl1.Horizontal = false;
            splitContainerControl1.Location = new Point(0, 0);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(simpleButton1);
            splitContainerControl1.Panel1.Controls.Add(labelControl3);
            splitContainerControl1.Panel1.Controls.Add(labelControl2);
            splitContainerControl1.Panel1.Controls.Add(labelControl1);
            splitContainerControl1.Panel1.Controls.Add(txtIdProgresql);
            splitContainerControl1.Panel1.Controls.Add(txtAddress);
            splitContainerControl1.Panel1.Controls.Add(txtName);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(gridControl1);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1532, 855);
            splitContainerControl1.SplitterPosition = 135;
            splitContainerControl1.TabIndex = 0;
            // 
            // gridControl1
            // 
            gridControl1.Dock = DockStyle.Fill;
            gridControl1.Location = new Point(0, 0);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new Size(1532, 708);
            gridControl1.TabIndex = 0;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            // 
            // txtName
            // 
            txtName.Location = new Point(135, 12);
            txtName.Name = "txtName";
            txtName.Size = new Size(348, 22);
            txtName.TabIndex = 0;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(135, 49);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(348, 22);
            txtAddress.TabIndex = 1;
            // 
            // txtIdProgresql
            // 
            txtIdProgresql.Location = new Point(135, 87);
            txtIdProgresql.Name = "txtIdProgresql";
            txtIdProgresql.Size = new Size(348, 22);
            txtIdProgresql.TabIndex = 2;
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(58, 15);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(51, 16);
            labelControl1.TabIndex = 3;
            labelControl1.Text = "Tên khu:";
            // 
            // labelControl2
            // 
            labelControl2.Location = new Point(58, 55);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new Size(44, 16);
            labelControl2.TabIndex = 4;
            labelControl2.Text = "Địa chỉ:";
            // 
            // labelControl3
            // 
            labelControl3.Location = new Point(58, 93);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new Size(67, 16);
            labelControl3.TabIndex = 5;
            labelControl3.Text = "Mã liên kết:";
            // 
            // simpleButton1
            // 
            simpleButton1.Location = new Point(540, 12);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(147, 34);
            simpleButton1.TabIndex = 6;
            simpleButton1.Text = "Tạo mới";
            simpleButton1.Click += simpleButton1_Click;
            // 
            // Frm_Zone
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1532, 855);
            Controls.Add(splitContainerControl1);
            Name = "Frm_Zone";
            Text = "Frm_Zone";
            Load += Frm_Zone_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            splitContainerControl1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtAddress.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtIdProgresql.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit txtIdProgresql;
        private DevExpress.XtraEditors.TextEdit txtAddress;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}