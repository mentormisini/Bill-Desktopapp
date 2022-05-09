using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlancGastroApp.printingchercher
{
    public partial class Rechercherimprimmer : Form
    {
        public Rechercherimprimmer()
        {
            InitializeComponent();
            this.Size = new Size(650, 800);
        }

        private void Rechercherimprimmer_Load(object sender, EventArgs e)
        {
        

            label1.Text = chercher.passing;
            try
            {
                this.productsofclientTableAdapter.FillBy1(this.imfacture.productsofclient, new System.Nullable<long>(((long)(System.Convert.ChangeType(label1.Text, typeof(long))))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            try
            {
                this.listofclientTableAdapter.FillBy1(this.imfacture.listofclient, ((long)(System.Convert.ChangeType(label1.Text, typeof(long)))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            this.reportViewer1.RefreshReport();
        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
         

        }

        private void fillBy1ToolStripButton1_Click(object sender, EventArgs e)
        {
           

        }

        private void fillBy1ToolStripButton_Click_1(object sender, EventArgs e)
        {
           

        }

        private void fillBy1ToolStripButton1_Click_1(object sender, EventArgs e)
        {
           

        }

        private void fillBy1ToolStripButton_Click_2(object sender, EventArgs e)
        {
         

        }

        private void fillBy1ToolStripButton1_Click_2(object sender, EventArgs e)
        {
        

        }
    }
}
