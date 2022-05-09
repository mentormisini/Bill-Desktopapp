using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlancGastroApp.raportsprinting
{
    public partial class rapports : Form
    {
        public rapports()
        {
            InitializeComponent();
            this.Size = new Size(1250, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void rapports_Load(object sender, EventArgs e)
        {


        }

        private void fillBy2ToolStripButton_Click(object sender, EventArgs e)
        {
           

        }

        private void fillBy3ToolStripButton_Click(object sender, EventArgs e)
        {
            

        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
         
            
        }

        private void fillBy2ToolStripButton_Click_1(object sender, EventArgs e)
        {
         

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
          

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void fillByToolStripButton_Click_1(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label2.Text = "Non Payé";
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                try
                {
                    this.listofclientTableAdapter.FillBy(this.DataSet1.listofclient, checkBox1.Text);
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                this.reportViewer1.RefreshReport();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                label2.Text="Payé";
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                try
                {
                    this.listofclientTableAdapter.FillBy(this.DataSet1.listofclient, checkBox2.Text);
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                this.reportViewer1.RefreshReport();
            }
        }

        private void fillByToolStripButton_Click_2(object sender, EventArgs e)
        {
           

        }

        private void fillBy1ToolStripButton_Click_1(object sender, EventArgs e)
        {
           

        }

        private void fillByToolStripButton_Click_3(object sender, EventArgs e)
        {
           

        }

        private void fillBy1ToolStripButton_Click_2(object sender, EventArgs e)
        {
         

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Checked = false;
                checkBox1.Checked = false;
                try
                {
                    this.listofclientTableAdapter.FillBy1(this.DataSet1.listofclient, "Facture");
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                this.reportViewer1.RefreshReport();
            }
        }

        private void fillBy1ToolStripButton_Click_3(object sender, EventArgs e)
        {
            

        }

        private void fillBy2ToolStripButton_Click_2(object sender, EventArgs e)
        {
          

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.listofclientTableAdapter.FillBy3(this.DataSet1.listofclient, dateTimePicker1.Text, dateTimePicker2.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            this.reportViewer1.RefreshReport();
        }

        private void fillBy3ToolStripButton_Click_1(object sender, EventArgs e)
        {
           

        }

        private void fillBy4ToolStripButton_Click(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            try
            {
                this.listofclientTableAdapter.FillBy4(this.DataSet1.listofclient, dateTimePicker1.Text, dateTimePicker2.Text, label2.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            this.reportViewer1.RefreshReport();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form frm = new Menu();
            frm.Show();
            this.Close();
        }
    }
}
