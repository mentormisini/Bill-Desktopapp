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

namespace BlancGastroApp
{
    public partial class chercher : Form
    {
        public chercher()
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

        MySqlConnection conn = new MySqlConnection(dbconnect.dbcon());
        private void chercher_Load(object sender, EventArgs e)
        {
            dataGridView1.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView1, true, null);
            displayclients();
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form frm = new Menu();
            frm.Show();
            this.Close();
        }
        public void displayclients()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT * FROM listofclient where mode='" + comboBox1.Text + "'", conn);
            conn.Open(); SDA.Fill(dt); dataGridView1.DataSource = dt; conn.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (comboBox1.Text =="Facture")
                {

                    displayclients();

                }
                else
                {
                    displayclients();
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    textBox1.Text = row.Cells[0].Value.ToString();
                }
                DataTable dt = new DataTable();
                MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT descriptif,kg_pce,prix,total,rabbais FROM productsofclient where id_product LIKE'" + textBox1.Text + "%';", conn);
                conn.Open(); SDA.Fill(dt); dataGridView2.DataSource = dt; conn.Close();



                //count lines
                kalkulimet.Class1 numrimet = new kalkulimet.Class1();
                numrimet.countnr(dataGridView2, textBox11); //nr rreshtave

                numrimet.calc(dataGridView2, textBox12); //totali

                numrimet.maxva(dataGridView2, label1);//perqindja nr

                numrimet.perqindja(textBox12, label1, textBox13);//perqindja

                numrimet.totali(textBox12, textBox13, textBox14); // totali

                //#end







            }





            catch (Exception) { }
        }
        public static string passing;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Sélectionner un client");

                }
                else
                {
                    passing = textBox1.Text;
                    Form frm = new printingchercher.Rechercherimprimmer();
                    frm.Show();
                }
            }
            catch {}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT * FROM listofclient  where id LIKE'" + textBox1.Text + "%' and mode='" + comboBox1.Text + "'", conn);
                conn.Open(); SDA.Fill(dt); dataGridView1.DataSource = dt; conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT * FROM listofclient  where company LIKE'" + textBox2.Text + "%' and mode='"+comboBox1.Text+"'", conn);
                conn.Open(); SDA.Fill(dt); dataGridView1.DataSource = dt; conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.CustomFormat = "yyyy.MM.dd";
                dateTimePicker2.CustomFormat = "yyyy.MM.dd";
                DataTable dt = new DataTable();
                MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT * FROM listofclient  where dataf BETWEEN'" + dateTimePicker1.Text + "' and '" + dateTimePicker2.Text + "' and mode='"+comboBox1.Text+"'", conn);
                conn.Open(); SDA.Fill(dt); dataGridView1.DataSource = dt; conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                BlancGastroApp.modifier frm = new modifier();
                frm.textBox1.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                frm.textBox2.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                frm.textBox3.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                frm.textBox4.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                frm.textBox5.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
                frm.dateTimePicker1.Text= this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                frm.dateTimePicker2.Text= this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                if (frm.comboBox3.SelectedIndex == 0)
                {
                    frm.comboBox3.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                }
                else
                {
                    frm.comboBox3.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                }
                frm.textBox11.Text= this.dataGridView1.CurrentRow.Cells[8].Value.ToString();
                frm.richTextBox1.Text= this.dataGridView1.CurrentRow.Cells[9].Value.ToString();
                frm.textBox10.Text= this.dataGridView1.CurrentRow.Cells[10].Value.ToString();
                frm.comboBox1.Text= this.dataGridView1.CurrentRow.Cells[11].Value.ToString();
                frm.textBox15.Text = this.dataGridView1.CurrentRow.Cells[12].Value.ToString();
                frm.comboBox5.Text = this.dataGridView1.CurrentRow.Cells[14].Value.ToString();




                frm.Show();
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                BlancGastroApp.modifier frm = new modifier();
                frm.textBox1.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                frm.textBox2.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                frm.textBox3.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                frm.textBox4.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                frm.textBox5.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
                frm.dateTimePicker1.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                frm.dateTimePicker2.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                if (frm.comboBox3.SelectedIndex == 0)
                {
                    frm.comboBox3.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                }
                else
                {
                    frm.comboBox3.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                }
                frm.textBox11.Text = this.dataGridView1.CurrentRow.Cells[8].Value.ToString();
                frm.richTextBox1.Text = this.dataGridView1.CurrentRow.Cells[9].Value.ToString();
                frm.textBox10.Text = this.dataGridView1.CurrentRow.Cells[10].Value.ToString();
                frm.comboBox1.Text = this.dataGridView1.CurrentRow.Cells[11].Value.ToString();
                frm.textBox15.Text = this.dataGridView1.CurrentRow.Cells[12].Value.ToString();
                frm.comboBox5.Text = this.dataGridView1.CurrentRow.Cells[14].Value.ToString();




                frm.Show();
                this.Close();
            }
            catch (Exception) { }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Sélectionner un client");

                }
                else
                {
                    passing = textBox1.Text;
                    Form frm = new BulletinFacture();
                    frm.Show();
                }
            }
            catch { }
        }
    }
}
