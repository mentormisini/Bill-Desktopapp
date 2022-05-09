namespace BlancGastroApp
{
    partial class BulletinFacture
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.listofclientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.imfacture = new BlancGastroApp.printing.imfacture();
            this.productsofclientibfk1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.listofclientTableAdapter = new BlancGastroApp.printing.imfactureTableAdapters.listofclientTableAdapter();
            this.productsofclientTableAdapter = new BlancGastroApp.printing.imfactureTableAdapters.productsofclientTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.listofclientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imfacture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsofclientibfk1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // listofclientBindingSource
            // 
            this.listofclientBindingSource.DataMember = "listofclient";
            this.listofclientBindingSource.DataSource = this.imfacture;
            // 
            // imfacture
            // 
            this.imfacture.DataSetName = "imfacture";
            this.imfacture.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // productsofclientibfk1BindingSource
            // 
            this.productsofclientibfk1BindingSource.DataMember = "productsofclient_ibfk_1";
            this.productsofclientibfk1BindingSource.DataSource = this.listofclientBindingSource;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.listofclientBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.productsofclientibfk1BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BlancGastroApp.bulletinfacture.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.ShowBackButton = false;
            this.reportViewer1.ShowContextMenu = false;
            this.reportViewer1.ShowCredentialPrompts = false;
            this.reportViewer1.ShowDocumentMapButton = false;
            this.reportViewer1.ShowFindControls = false;
            this.reportViewer1.ShowPageNavigationControls = false;
            this.reportViewer1.ShowParameterPrompts = false;
            this.reportViewer1.ShowPrintButton = false;
            this.reportViewer1.ShowProgress = false;
            this.reportViewer1.ShowPromptAreaButton = false;
            this.reportViewer1.ShowRefreshButton = false;
            this.reportViewer1.ShowStopButton = false;
            this.reportViewer1.ShowZoomControl = false;
            this.reportViewer1.Size = new System.Drawing.Size(1112, 1050);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // listofclientTableAdapter
            // 
            this.listofclientTableAdapter.ClearBeforeFill = true;
            // 
            // productsofclientTableAdapter
            // 
            this.productsofclientTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(931, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // BulletinFacture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 1050);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label1);
            this.Name = "BulletinFacture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BulletinFacture";
            this.Load += new System.EventHandler(this.BulletinFacture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listofclientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imfacture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsofclientibfk1BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private printing.imfacture imfacture;
        private System.Windows.Forms.BindingSource listofclientBindingSource;
        private printing.imfactureTableAdapters.listofclientTableAdapter listofclientTableAdapter;
        private System.Windows.Forms.BindingSource productsofclientibfk1BindingSource;
        private printing.imfactureTableAdapters.productsofclientTableAdapter productsofclientTableAdapter;
        private System.Windows.Forms.Label label1;
    }
}