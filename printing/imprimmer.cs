using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlancGastroApp.printing
{
    public partial class imprimmer : Form
    {
        public imprimmer()
        {
            InitializeComponent();
            this.Size = new Size(650, 800);
        }

        private void imprimmer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'imfacture.listofclient' table. You can move, or remove it, as needed.
  


            label1.Text = Facture.passvalue;
            try
            {
                this.listofclientTableAdapter.FillBy(this.imfacture.listofclient, ((long)(System.Convert.ChangeType(label1.Text, typeof(long)))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            try
            {
                this.productsofclientTableAdapter.FillBy(this.imfacture.productsofclient, new System.Nullable<long>(((long)(System.Convert.ChangeType(label1.Text, typeof(long))))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            this.reportViewer1.RefreshReport();
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
         

        }

        private void fillByToolStripButton1_Click(object sender, EventArgs e)
        {


        }

        private void fillByToolStripButton_Click_1(object sender, EventArgs e)
        {
            

        }

        private void fillByToolStripButton1_Click_1(object sender, EventArgs e)
        {
         

        }

        private void fillBy2ToolStripButton_Click(object sender, EventArgs e)
        {
            

        }

        private void fillBy2ToolStripButton1_Click(object sender, EventArgs e)
        {
            

        }

        private void fillByToolStripButton_Click_2(object sender, EventArgs e)
        {
       

        }

        private void fillByToolStripButton1_Click_2(object sender, EventArgs e)
        {
          

        }
    }
}
