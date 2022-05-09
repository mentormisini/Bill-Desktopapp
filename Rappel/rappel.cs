using MySql.Data.MySqlClient;
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
    public partial class rappel : Form
    {
        public rappel()
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


            // Fast
            BackgroundImage = BackBmp;
        }

        //fast



        MySqlConnection conn = new MySqlConnection(dbconnect.dbcon());

        private void rappel_Load(object sender, EventArgs e)
        {
           

            if (textBox2.Text == "")
            {

            }
            else
            {
                try
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT descriptif,kg_pce,prix,total,rabbais,iii FROM productsofclient where id_product ='" + textBox2.Text + "'", conn);
                    conn.Open(); SDA.Fill(dt); dataGridView2.DataSource = dt; conn.Close();

                }

                catch (Exception) { }
            }



        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form frm = new Menu();
            frm.Show();
            this.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT id,name_surname,company,tel,rappel from listofclient where id=@id", conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", textBox2.Text);


                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        textBox1.Text = dr["name_surname"].ToString();
                        textBox3.Text = dr["company"].ToString();
                        textBox4.Text = dr["tel"].ToString();
                        comboBox1.Text = dr["Rappel"].ToString();

                    }
                    conn.Close();

                        DataTable dt = new DataTable();
                        MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT descriptif,kg_pce,prix,total,rabbais FROM productsofclient where id_product ='" + textBox2.Text + "'", conn);
                        conn.Open(); SDA.Fill(dt); dataGridView2.DataSource = dt; conn.Close();
                }
                catch (Exception) { }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("Insert into  productsofclient (id_product,Descriptif,kg_pce,prix,total,rabbais,modeproduct,datep) VALUES (@id_product,@Descriptif,@kg_pce,@prix,@total,@rabbais,@modeproduct,datep)", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@id_product", textBox2.Text);
                cmd.Parameters.AddWithValue("@descriptif", comboBox1.Text);
                cmd.Parameters.AddWithValue("@kg_pce", 1.00);
                cmd.Parameters.AddWithValue("@prix", 20.00);
                cmd.Parameters.AddWithValue("@total", 20.00);
                cmd.Parameters.AddWithValue("@rabbais", 0);
                cmd.Parameters.AddWithValue("@modeproduct", "Facture");
                cmd.Parameters.AddWithValue("@datep",dateTimePicker1.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Rappel Ajouter avec sucess ;)");
                MySqlCommand cmdI = new MySqlCommand("UPDATE listofclient set Rappel=@Rappel WHERE id=@id", conn);
                cmdI.Parameters.AddWithValue("@Rappel", comboBox1.Text);
                cmdI.Parameters.AddWithValue("@id", textBox2.Text);
                conn.Open();
                cmdI.ExecuteNonQuery();
                conn.Close();
             



                try
                {
                    this.listofclientTableAdapter.FillBy2(this.imfacture.listofclient, ((long)(System.Convert.ChangeType(textBox2.Text, typeof(long)))));
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                try
                {
                    this.productsofclientTableAdapter.FillBy2(this.imfacture.productsofclient, new System.Nullable<long>(((long)(System.Convert.ChangeType(textBox2.Text, typeof(long))))));
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                this.reportViewer1.RefreshReport();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
       

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

        private void fillByToolStripButton_Click_2(object sender, EventArgs e)
        {
         

        }

        private void fillByToolStripButton1_Click_2(object sender, EventArgs e)
        {
            

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            
        }

        private void fillByToolStripButton_Click_3(object sender, EventArgs e)
        {
           
        }

        private void fillByToolStripButton1_Click_3(object sender, EventArgs e)
        {
          

        }

        private void fillBy2ToolStripButton_Click(object sender, EventArgs e)
        {
            


        }

        private void fillBy2ToolStripButton1_Click(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                try
                {
                    DialogResult dialogResult = MessageBox.Show("Supprimer", "êtes-vous sûr !!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        MySqlCommand delete = new MySqlCommand("DELETE FROM productsofclient  WHERE id_product='" + textBox2.Text + "' and iii='" + label2.Text + "'", conn);
                        conn.Open();
                        delete.ExecuteNonQuery();
                        MessageBox.Show("La suppression à été effectuée avec succès");
                        conn.Close();
                        DataTable dt = new DataTable();
                        MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT descriptif,kg_pce,prix,total,rabbais,iii FROM productsofclient where id_product ='" + textBox2.Text + "'", conn);
                        conn.Open(); SDA.Fill(dt); dataGridView2.DataSource = dt; conn.Close();
                        try
                        {
                            this.listofclientTableAdapter.FillBy2(this.imfacture.listofclient, ((long)(System.Convert.ChangeType(textBox2.Text, typeof(long)))));
                        }
                        catch (System.Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                        try
                        {
                            this.productsofclientTableAdapter.FillBy2(this.imfacture.productsofclient, new System.Nullable<long>(((long)(System.Convert.ChangeType(textBox2.Text, typeof(long))))));
                        }
                        catch (System.Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                        this.reportViewer1.RefreshReport();




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
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dgr = this.dataGridView2.Rows[e.RowIndex];
                label2.Text = dgr.Cells[6].Value.ToString();

            }
        }
    }
}
