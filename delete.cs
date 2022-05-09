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
    public partial class delete : Form
    {
        public delete()
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
        private void delete_Load(object sender, EventArgs e)
        {
            
            comboBox1.Text = "Sélectionner";
            dataGridView1.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView1, true, null);
            dataGridView2.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView1, true, null);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form frm = new Menu();
            frm.Show();
            this.Close();
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT * FROM listofclient  where company LIKE'" + textBox1.Text + "%' and mode='" + comboBox1.Text + "'", conn);
                conn.Open(); SDA.Fill(dt); dataGridView1.DataSource = dt; conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (comboBox1.Text == "Facture")
                {

                    DataTable dt = new DataTable();
                    MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT * FROM listofclient where mode='" + comboBox1.Text + "'", conn);
                    conn.Open(); SDA.Fill(dt); dataGridView1.DataSource = dt; conn.Close();

                }
                else
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT * FROM listofclient where mode='" + comboBox1.Text + "'", conn);
                    conn.Open(); SDA.Fill(dt); dataGridView1.DataSource = dt; conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("supprimer", "êtes-vous sûr !!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    conn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter("DELETE FROM listofclient WHERE id='" + label1.Text + "'", conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "listofclient");
                    dataGridView1.DataSource = ds.Tables["listofclient"];
                    MessageBox.Show("La suppression à été effectuée avec succès");
                    conn.Close();

                    MySqlDataAdapter daa = new MySqlDataAdapter("DELETE FROM productsofclient WHERE id_product='" + label1.Text + "'", conn);
                    DataSet dsa = new DataSet();
                    daa.Fill(dsa, "productsofclient");
                    dataGridView1.DataSource = dsa.Tables["productsofclient"];
            
                    conn.Close();

                    comboBox1.Text = "Sélectionner";

                    dataGridView2.DataSource = null;
                    dataGridView2.Rows.Clear();



                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                label1.Text = row.Cells[0].Value.ToString();
            }
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT descriptif,kg_pce,prix,total,rabbais from productsofclient where id_product='" + label1.Text + "'", conn);
            conn.Open();
            da.Fill(dt); dataGridView2.DataSource = dt;
            conn.Close();
        }
    }
}
