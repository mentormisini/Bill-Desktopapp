using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BlancGastroApp
{
    public partial class modifier : Form
    {
        public modifier()
        {
            InitializeComponent();
            this.Size = new Size(1250, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
           
        }
        //fast
        DataRow dreader;
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

        public void loadform()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT id_product,Descriptif,kg_pce,prix,total,rabbais,modeproduct,datep,client,prorata FROM productsofclient where id_product='" + textBox1.Text + "'", conn);
            conn.Open(); SDA.Fill(dt); dataGridView1.DataSource = dt;
            dataGridView2.AutoGenerateColumns = true;
            

            conn.Close();
           
            //dataGridView1.Columns[5].ReadOnly = true;
        }
        private void modifier_Load(object sender, EventArgs e)
        {
         
            try
            {

                loadform();
                dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView2.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                this.dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView2.Columns[5].ReadOnly = true;
                dataGridView2.Columns["prix"].DefaultCellStyle.BackColor = Color.Silver;
                dataGridView2.Columns["kg_pce"].DefaultCellStyle.BackColor = Color.Gainsboro;
                dataGridView2.Columns["client"].DefaultCellStyle.BackColor = Color.Wheat;
                

                conn.Open();
             

                MySqlDataAdapter cb = new MySqlDataAdapter("Select ID, designationd from designation", conn);
                DataTable cm = new DataTable();
                cb.Fill(cm);
                DataRow row = cm.NewRow();
                row[0] = 0;
                cm.Rows.InsertAt(row, 0);

                comboBox4.DataSource = cm;
                comboBox4.DisplayMember = "designationd";
                comboBox4.ValueMember = "ID";


                conn.Close();
                button1.PerformClick();
                kalkulimet.Class1 numrimet = new kalkulimet.Class1();
                numrimet.maxvalueedit(dataGridView2, textBox9);//perqindja nr
                calcul();
               
               

            }
            catch (Exception) { }
      


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox15.Text.Contains(","))
                {
                    MessageBox.Show("Changer la TVA 7.7");
                }
                else
                {
                    conn.Open();
                    MySqlDataAdapter daa = new MySqlDataAdapter("DELETE FROM productsofclient WHERE id_product='" + textBox1.Text + "'", conn);
                    DataSet dsa = new DataSet();
                    daa.Fill(dsa, "productsofclient");
                    dataGridView2.DataSource = dsa.Tables["productsofclient"];
                    conn.Close();




                    //list of client
                    MySqlCommand cmdi = new MySqlCommand("Update listofclient set name_surname=@name_surname,company=@company,adresse=@adresse,tel=@tel,dataf=@dataf,datam=@datam,status=@status,compte=@compte,comptenumbers=@comptenumbers,textextra=@textextra,mode=@mode,tva=@tva,bank=@bank where id=@id", conn);
                    cmdi.Parameters.AddWithValue("@name_surname", textBox2.Text.Replace("'", "’"));
                    cmdi.Parameters.AddWithValue("@company", textBox3.Text.Replace("'", "’"));
                    cmdi.Parameters.AddWithValue("@adresse", textBox4.Text.Replace("'", "’"));
                    cmdi.Parameters.AddWithValue("@tel", textBox5.Text.Replace("'", "’"));
                    cmdi.Parameters.AddWithValue("@dataf", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    cmdi.Parameters.AddWithValue("@datam", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    cmdi.Parameters.AddWithValue("@status", comboBox3.Text.Replace("'", "’"));
                    cmdi.Parameters.AddWithValue("@compte", textBox11.Text.Replace("'", "’"));
                    cmdi.Parameters.AddWithValue("@comptenumbers", richTextBox1.Text);
                    cmdi.Parameters.AddWithValue("@textextra", textBox10.Text.Replace("'", "’"));
                    cmdi.Parameters.AddWithValue("@mode", comboBox1.Text);
                    cmdi.Parameters.AddWithValue("@tva", textBox15.Text.Replace(",", "."));
                    cmdi.Parameters.AddWithValue("@bank", comboBox5.Text.Replace("'", "’"));
                    cmdi.Parameters.AddWithValue("@id", textBox1.Text);
                    conn.Open();
                    cmdi.ExecuteNonQuery();
                    conn.Close();


                    for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
                    {

                        double k = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value.ToString());
                        double o = Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value.ToString());
                        double p = Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value = Convert.ToDecimal(k * o));
                        MySqlCommand cmd = new MySqlCommand(@"INSERT into productsofclient(id_product,Descriptif,client,kg_pce,prix,total,rabbais,modeproduct,datep,prorata)VALUES(@id_product,@Descriptif,@client,@kg_pce,@prix,@total,@rabbais,@modeproduct,@datep,@prorata)", conn);
                        cmd.Parameters.AddWithValue("@id_product", dataGridView2.Rows[i].Cells[0].Value);
                        cmd.Parameters.AddWithValue("@Descriptif", dataGridView2.Rows[i].Cells[1].Value);
                        cmd.Parameters.AddWithValue("@client", dataGridView2.Rows[i].Cells[2].Value);
                        cmd.Parameters.AddWithValue("@kg_pce", k);//2
                        cmd.Parameters.AddWithValue("@prix", o);//3
                        cmd.Parameters.AddWithValue("@total", p);//4
                        cmd.Parameters.AddWithValue("@rabbais", dataGridView2.Rows[i].Cells[6].Value);
                        cmd.Parameters.AddWithValue("@modeproduct", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@datep", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@prorata", dataGridView2.Rows[i].Cells[9].Value);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    MessageBox.Show("SUCESS");
                }
                
            }
                
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form frm = new chercher();
            frm.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("Veuillez ne pas laisser les champs vides");
            }
            else
            {


                try
                {

                    //bool Found = false;
                    //for (var j = 0; j <= this.dataGridView1.Rows.Count - 1; j++)
                    //{
                    //    if (Convert.ToString(this.dataGridView1.Rows[j].Cells[1].Value) == textBox6.Text.ToString())
                    //    {
                    //        MessageBox.Show("La désignation existait\n\n( " + textBox6.Text + " )\n\n modifiez-la");
                    //        Found = true;
                    //        //dataGridView1.Rows.RemoveAt(dataGridView1.Rows[dataGridView1.Rows.GetLastRow(0)].Index);
                    //        break;
                    //    }
                    //}
                    //if (!Found)
                    //{
                    int n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells[0].Value = textBox1.Text;
                    dataGridView2.Rows[n].Cells[1].Value = textBox6.Text.Replace("'", "’");
                    dataGridView2.Rows[n].Cells[2].Value = comboBox6.Text;
                    dataGridView2.Rows[n].Cells[3].Value = textBox7.Text;
                    dataGridView2.Rows[n].Cells[4].Value = textBox8.Text;
                    double k = Convert.ToDouble(dataGridView2.Rows[n].Cells[3].Value.ToString());
                    double o = Convert.ToDouble(dataGridView2.Rows[n].Cells[4].Value.ToString());
                    double p = Convert.ToDouble(dataGridView2.Rows[n].Cells[5].Value = Convert.ToDecimal(k * o));
                    dataGridView2.Rows[n].Cells[6].Value = textBox9.Text;
                    dataGridView2.Rows[n].Cells[7].Value = comboBox1.Text;
                    dataGridView2.Rows[n].Cells[8].Value = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    dataGridView2.Rows[n].Cells[9].Value = textBox12.Text;

                    textBox6.Visible = false;
                    calcul();

                }
                catch { }
            }







                //try
                //{
                //    MySqlCommand cmd = new MySqlCommand("INSERT INTO productsofclient (id_product,Descriptif,kg_pce,prix,total,rabbais,modeproduct,datep,client,prorata) VALUES (@id_product,@Descriptif,@kg_pce,@prix,@total,@rabbais,@modeproduct,@datep,@client,@prorata)", conn);
                //    cmd.Parameters.AddWithValue("@id_product", textBox1.Text);
                //    cmd.Parameters.AddWithValue("@Descriptif", textBox6.Text.Replace("'", "’"));
                //    cmd.Parameters.AddWithValue("@kg_pce", textBox7.Text);
                //    cmd.Parameters.AddWithValue("@prix", textBox8.Text);
                //    cmd.Parameters.AddWithValue("@total", label11.Text);
                //    cmd.Parameters.AddWithValue("@rabbais", textBox9.Text);
                //    cmd.Parameters.AddWithValue("@modeproduct", comboBox1.Text);
                //    cmd.Parameters.AddWithValue("@datep", dateTimePicker1.Text);
                //    cmd.Parameters.AddWithValue("@client", comboBox6.Text);
                //    cmd.Parameters.AddWithValue("@prorata", textBox12.Text);
                //    conn.Open();
                //    cmd.ExecuteNonQuery();
                //    conn.Close();
                //    displaydata();


                //}

                //catch(Exception ex) { MessageBox.Show(ex.ToString()); }
                //MessageBox.Show("Success");




            }
        private void calcul()
        {
            kalkulimet.Class1 numrimet = new kalkulimet.Class1();
            numrimet.countnr(dataGridView2, textBox14); //nr rreshtave

            numrimet.calcsummodifier(dataGridView2, textBox16); //totali

            numrimet.perqindjafacture(textBox16, textBox9, textBox17);//perqindja

            numrimet.totali(textBox16, textBox17, textBox13); // totali
        }
        private void displaydata()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT *FROM productsofclient where id_product='" + textBox1.Text + "'", conn);
            conn.Open(); SDA.Fill(dt); dataGridView1.DataSource = dt; conn.Close();
            dataGridView1.Columns[0].ReadOnly = true;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox7.Text, @"[a-zA-Z ,;'!#%^&()_@`/\/*-+]+$"))
            {

                textBox7.Text = textBox7.Text.Remove(textBox7.Text.Length - 1);
            }
            //if (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrEmpty(textBox8.Text))
            //{
            //    label11.Text = (Convert.ToDecimal(textBox7.Text) * Convert.ToDecimal(textBox8.Text)).ToString();
            //}
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox8.Text, @"[a-zA-Z ,;'!#%^&()_@`/\/*-+]+$"))
            {

                textBox8.Text = textBox8.Text.Remove(textBox8.Text.Length - 1);
            }
            //if (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrEmpty(textBox8.Text))
            //{
            //    label11.Text = (Convert.ToDecimal(textBox7.Text) * Convert.ToDecimal(textBox8.Text)).ToString();
            //}
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    DialogResult dialogResult = MessageBox.Show("supprimer", "êtes-vous sûr !!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                        {
                            MySqlCommand cmd = conn.CreateCommand();
                            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                            int iii = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
                            cmd.CommandText = "Delete from productsofclient where id_product='" + id + "' AND iii='" + iii + "'";
                            dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    else
                    {
                        
                    }
                }
                catch (Exception) { }
            }
        }

        private void richTextBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void label10_Click(object sender, EventArgs e)
        {
           

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = comboBox4.SelectedValue.ToString();
            try
            {
                textBox6.Text = comboBox4.Text.ToString();
                if (comboBox4.Text == comboBox4.Text)
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT prix from productsofclient where client=@client and Descriptif=@Descriptif and id_product=@id_product", conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id_product", label1.Text);
                    cmd.Parameters.AddWithValue("@client", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Descriptif", comboBox4.Text.ToString());
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        textBox8.Text = dr["prix"].ToString();

                    }
                    conn.Close();
                }
                textBox6.Visible = true;

            }
            catch (Exception) { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox6.Visible = true;
            }
            else
            {
                textBox6.Visible = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow dgr =  this.dataGridView1.Rows[e.RowIndex];
               label20.Text = dgr.Cells[1].Value.ToString();

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(30);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                try
                {
                    DialogResult dialogResult = MessageBox.Show("Supprimer", "êtes-vous sûr !!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        MySqlCommand delete = new MySqlCommand("DELETE FROM productsofclient  WHERE id_product='" + textBox1.Text + "' and iii='" + label20.Text + "'", conn);
                        conn.Open();
                        delete.ExecuteNonQuery();
                        MessageBox.Show("La suppression à été effectuée avec succès");
                        conn.Close();
                        displaydata();



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

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dataGridView1;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dataGridView2;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dataGridView2;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            int n = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (dataGridView1.Rows.Count != n + 1)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells[0].Value = row.Cells[0].Value.ToString();//id_prod

                    dataGridView2.Rows[n].Cells[1].Value = row.Cells[1].Value.ToString();//descriptiv
                    dataGridView2.Rows[n].Cells[2].Value = row.Cells[8].Value.ToString();//unite
                    dataGridView2.Rows[n].Cells[3].Value = row.Cells[2].Value.ToString();//pce
                    dataGridView2.Rows[n].Cells[4].Value = row.Cells[3].Value.ToString();//prix
                    dataGridView2.Rows[n].Cells[5].Value = row.Cells[4].Value.ToString();//total

                    dataGridView2.Rows[n].Cells[6].Value = row.Cells[5].Value.ToString();//total
                    dataGridView2.Rows[n].Cells[7].Value = row.Cells[6].Value.ToString();//total
                    dataGridView2.Rows[n].Cells[8].Value = row.Cells[7].Value.ToString();//total
                    dataGridView2.Rows[n].Cells[9].Value = row.Cells[9].Value.ToString();//total





                }
                n += 1;


            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4.PerformClick();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip2.Active = true;
            toolTip3.Active = true;
            toolTip4.Active = true;
            int durationMilliseconds = 10000;
            toolTip1.Show(toolTip1.GetToolTip(comboBox6), comboBox6, durationMilliseconds);
            toolTip2.Show(toolTip2.GetToolTip(textBox6), textBox6, durationMilliseconds);
            toolTip3.Show(toolTip3.GetToolTip(button3), button3, durationMilliseconds);
            toolTip4.Show(toolTip4.GetToolTip(pictureBox1), pictureBox1, durationMilliseconds);
        }
    }
}
