using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace BlancGastroApp
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            this.Size = new Size(1250, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //fast
        protected override CreateParams CreateParams
        {

            get
            {
                CreateParams handleparm = base.CreateParams;
                handleparm.ExStyle |= 0x02000000;
                return handleparm;

            }
        }
        Bitmap BackBmp;
        Bitmap BackImg;
        Graphics memoryGraphics;

        private void InitAppearance()
        {
            //Added performance improvements by caching the image.  Only decodes once here at startup

            BackImg = Properties.Resources.Background;
            BackBmp = new Bitmap(BackImg.Width, BackImg.Height);
            memoryGraphics = Graphics.FromImage(BackBmp);

            memoryGraphics.DrawImage(BackImg, 0, 0, BackImg.Width, BackImg.Height);

            // Slow
            //BackgroundImage = Resources.Background;


            // Fastreduce
            BackgroundImage = BackBmp;
        }

        //fastreduce bitmaps

            //constring
        MySqlConnection conn = new MySqlConnection(dbconnect.dbcon());
        //constring
       
        private void Menu_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'imfacture.listofclient' table. You can move, or remove it, as needed.
           

            textBox1.Select();
            pictureBox4.Enabled = false;
            //design
            groupBox3.Visible = false;


            label2.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");//date clock
            dataGridView1.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView1, true, null);
            dataGridView1.Columns["status"].DefaultCellStyle.BackColor = Color.Silver;
            dataGridView1.Columns["status"].DefaultCellStyle.ForeColor = Color.Firebrick;
            try
            {

                DataTable dta = new DataTable();
                MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT id,status,Rappel,name_surname,company,compte,adresse,tel,dataf,datam FROM listofclient where datam <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and status ='Non payé' and mode='Facture'", conn);
                conn.Open(); SDA.Fill(dta); dataGridView1.DataSource = dta;
                conn.Close();
                if(dataGridView1.Rows.Count > 0)
                {
                    pictureBox2.Visible = true;
                }
                else
                {
                    pictureBox2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            Form frm = new Facture();
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form frm = new chercher();
            frm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form frm = new delete();
            frm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form frm = new raportsprinting.rapports();
            frm.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://tawk.to/chat/5eb56eaa8ee2956d739f4a53/1g06vrgbs");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form frm = new rappel();
            frm.Show();
            this.Hide();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BlancGastroApp.rappel frm = new rappel();
            frm.textBox2.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frm.comboBox1.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            frm.textBox1.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            frm.textBox3.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            frm.textBox4.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();

            frm.Show();
            this.Hide();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form frm = new detailsf();
            frm.Show();
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                label4.Text = row.Cells[0].Value.ToString();
                groupBox3.Visible = true;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MySqlCommand cmdi = new MySqlCommand("Update listofclient set status=@status where id=@id", conn);
            conn.Open();
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                cmdi.Parameters.AddWithValue("@status", dataGridView1.Rows[i].Cells[1].Value);
                cmdi.Parameters.AddWithValue("@id", label4.Text);
            }

         
            cmdi.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("OK");
            dataGridView1.Refresh();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Payé")
            {
                MySqlCommand cmdi = new MySqlCommand("Update listofclient set status=@status,name_surname=@name_surname where id=@id", conn);
                conn.Open();

                cmdi.Parameters.AddWithValue("@status", comboBox1.Text);
                cmdi.Parameters.AddWithValue("@id", label4.Text);
                cmdi.Parameters.AddWithValue("@name_surname", dateTimePicker1.Value.ToString("dd-MM-yyyy HH:mm:ss"));
                cmdi.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("OK");
                DataTable dta = new DataTable();
                MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT id,status,Rappel,company,adresse,tel,dataf,datam FROM listofclient where datam <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and status ='Non payé' and mode='Facture'", conn);
                conn.Open(); SDA.Fill(dta); dataGridView1.DataSource = dta;
                conn.Close();
                groupBox3.Visible = false;
                if (dataGridView1.Rows.Count > 0)
                {
                    pictureBox2.Visible = true;
                }
                else
                {
                    pictureBox2.Visible = false;
                }
            }
        }

        private void Menu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                Form frm = new chercher();
                frm.Show();
                this.Close();
            }
        }

        private void Menu_KeyPress(object sender, KeyPressEventArgs e)
        {
         
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.KeyPreview = true;
            if (e.KeyCode == Keys.D)
            {
                Form frm = new chercher();
                frm.Show();
                this.Hide();
            }
            if (e.KeyCode == Keys.C)
            {
                Form frm = new Facture();
                frm.Show();
                this.Hide();
            }

            if (e.KeyCode == Keys.S)
            {
                Form frm = new delete();
                frm.Show();
                this.Hide();
            }
            if (e.KeyCode == Keys.R)
            {
                Form frm = new raportsprinting.rapports();
                frm.Show();
                this.Hide();
            }
            if (e.KeyCode == Keys.E)
            {
                Form frm = new rappel(); 
                frm.Show();
                this.Hide();
            }
         


        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Enabled = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Enabled = false;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            
           
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip2.Active = true;
            int durationMilliseconds = 10000;
            toolTip1.Show(toolTip1.GetToolTip(comboBox1), comboBox1, durationMilliseconds);
            toolTip2.Show(toolTip2.GetToolTip(label15), label15, durationMilliseconds);
        }
    }
}
