using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlancGastroApp
{
    public partial class BulletinFacture : Form
    {
        public BulletinFacture()
        {
            InitializeComponent();
            this.Size = new Size(650, 800);
        }

        private void BulletinFacture_Load(object sender, EventArgs e)
        {
            label1.Text = chercher.passing;
            try
            {
                this.listofclientTableAdapter.FillBy3(this.imfacture.listofclient, ((long)(System.Convert.ChangeType(label1.Text, typeof(long)))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            try
            {
                this.productsofclientTableAdapter.FillBy3(this.imfacture.productsofclient, ((long)(System.Convert.ChangeType(label1.Text, typeof(long)))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            this.reportViewer1.RefreshReport();
        }

        private void fillBy3ToolStripButton_Click(object sender, EventArgs e)
        {
          

        }

        private void fillBy3ToolStripButton1_Click(object sender, EventArgs e)
        {
           

        }
    }
}
