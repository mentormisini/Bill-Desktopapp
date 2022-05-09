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
    public partial class detailsf : Form
    {
        public detailsf()
        {
            InitializeComponent();
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
        public void loaddata()
        {
            try
            {
                DataTable dta = new DataTable();
                MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT ID,designationd FROM designation", conn);
                conn.Open(); SDA.Fill(dta); dataGridView1.DataSource = dta;

                conn.Close();
            }
            catch (Exception)
            {

            }
        }
        public void reload()
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT IFNULL(MAX(ID),0) FROM designation", conn))
            {

                conn.Open();
                double result = (Convert.ToDouble(cmd.ExecuteScalar()) + 1);
                label1.Text = result.ToString();
                conn.Close();

            }
        }
        private void detailsf_Load(object sender, EventArgs e)
        {
            try
            {
               
                dataGridView1.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView1, true, null);
                reload();

                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                loaddata();
            }
            catch (Exception)
            {
                //
            }
          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                label1.Text = row.Cells[1].Value.ToString();
                textBox1.Text = row.Cells[2].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                reload();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO designation (ID,designationd) VALUES (@ID,@designationd)", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@ID", label1.Text);
                cmd.Parameters.AddWithValue("@designationd", textBox1.Text.Replace("'", "’"));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Success ;)");
                conn.Close();
                textBox1.Clear();
                reload();
                loaddata();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Supprimer", "êtes-vous sûr !!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    MySqlCommand delete = new MySqlCommand("DELETE FROM designation  WHERE ID='" + label1.Text + "'", conn);
                    conn.Open();
                    delete.ExecuteNonQuery();
                    MessageBox.Show("La suppression à été effectuée avec succès");
                    conn.Close();
                    DataTable dt = new DataTable();
                    loaddata();
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand change = new MySqlCommand("UPDATE designation set designationd=@designationd where ID=@ID", conn);
                conn.Open();
                change.Parameters.AddWithValue("@ID", label1.Text);
                change.Parameters.AddWithValue("@designationd", textBox1.Text.Replace("'", "’"));
                change.ExecuteNonQuery();
                conn.Close();
                loaddata();
            }
            catch(Exception ex)
            { //
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int row = 0;
            row = dataGridView1.Rows.Count - 1;

            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            dataGridView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
