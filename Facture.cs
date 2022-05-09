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
    public partial class Facture : Form
    {
        public Facture()
        {
            InitializeComponent();
            this.Size = new Size(1250, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            
        }
        DataRow dreader;
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
        private void ShowToolTip(object sender, string message)
        {
            new ToolTip().Show(message, this, Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y, 5000);
        }


        MySqlConnection conn = new MySqlConnection(dbconnect.dbcon());
        private void Facture_Load(object sender, EventArgs e)
        {
            try
                 
            {
                
                textBox2.Visible = false;
                dateTimePicker1.CustomFormat = "dd-MM-yyyy";
     
                textBox3.Focus();
                textBox3.Select();
                dateTimePicker2.Value = dateTimePicker1.Value.AddDays(31);
                dateTimePicker2.CustomFormat = "dd-MM-yyyy";
             
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                using (MySqlCommand cmd = new MySqlCommand("SELECT MAX(id) FROM listofclient", conn))
                {
                    conn.Open();
                    double result = (Convert.ToDouble(cmd.ExecuteScalar()) + 1);
                    textBox1.Text = result.ToString();
                    conn.Close();
                }





                MySqlCommand kompania1;
                MySqlDataReader drr1;
                kompania1 = new MySqlCommand("SELECT company FROM listofclient", conn); // per emrin mbiemrin
                conn.Open();
                drr1 = kompania1.ExecuteReader();
                AutoCompleteStringCollection Collection11 = new AutoCompleteStringCollection();
                while (drr1.Read())
                {
                    Collection11.Add(drr1.GetString(0));

                }
                textBox3.AutoCompleteCustomSource = Collection11;

                drr1.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            MySqlDataAdapter cb = new MySqlDataAdapter("Select ID, designationd from designation", conn);
            conn.Open();
            DataTable cm = new DataTable();
            cb.Fill(cm);
            DataRow row = cm.NewRow();
            row[0] = 0;
            cm.Rows.InsertAt(row, 0);

            comboBox3.DataSource = cm;
            comboBox3.DisplayMember = "designationd";
            comboBox3.ValueMember = "ID";
            conn.Close();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form frm = new Menu();
            frm.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
                try
                {
             
                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO listofclient (id,company,adresse,tel,dataf,datam,status,compte,comptenumbers,textextra,mode,tva,bank) VALUES (@id,@company,@adresse,@tel,@dataf,@datam,@status,@compte,@comptenumbers,@textextra,@mode,@tva,@bank)", conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", textBox1.Text); 
                    cmd.Parameters.AddWithValue("@company", textBox3.Text.Replace("'", "’"));
                    cmd.Parameters.AddWithValue("@adresse", textBox4.Text.Replace("'", "’"));
                    cmd.Parameters.AddWithValue("@tel", textBox5.Text.Replace("'", "’"));
                    cmd.Parameters.AddWithValue("@dataf", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@datam", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@status", comboBox4.Text);           
                    cmd.Parameters.AddWithValue("@compte", textBox6.Text.Replace("'", "’"));
                    cmd.Parameters.AddWithValue("@comptenumbers", richTextBox1.Text.Replace("'", "’"));
                    cmd.Parameters.AddWithValue("@textextra", textBox10.Text.Replace("'", "’"));
                    cmd.Parameters.AddWithValue("@mode", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@tva", textBox15.Text.Replace(",","."));
                cmd.Parameters.AddWithValue("@bank", comboBox5.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        MySqlCommand cmdi = new MySqlCommand(@"INSERT into productsofclient(id_product,Descriptif,client,kg_pce,prix,total,rabbais,modeproduct,datep,prorata)VALUES('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + dataGridView1.Rows[i].Cells[7].Value + "','" + dataGridView1.Rows[i].Cells[8].Value + "','" + dataGridView1.Rows[i].Cells[9].Value + "')", conn);
                        conn.Open();
                        cmdi.ExecuteNonQuery();
                        conn.Close();

                    }

                MessageBox.Show("Enregistré avec succès :)");
                button3.Enabled = false;


                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            
        }

        private void richTextBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        public static string passvalue;
        private void button2_Click(object sender, EventArgs e)
        {
            passvalue = textBox1.Text;
            Form frm = new printing.imprimmer();
            frm.Show();
            

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox7.Text == "" || textBox8.Text == "")
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
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                        dataGridView1.Rows[n].Cells[1].Value = textBox2.Text.Replace("'", "’");
                        dataGridView1.Rows[n].Cells[2].Value = comboBox6.Text;
                        dataGridView1.Rows[n].Cells[3].Value = textBox7.Text.Replace("'", "’");
                        dataGridView1.Rows[n].Cells[4].Value = textBox8.Text.Replace("'", "’");
                        double k = Convert.ToDouble(dataGridView1.Rows[n].Cells[3].Value.ToString());
                        double o = Convert.ToDouble(dataGridView1.Rows[n].Cells[4].Value.ToString());
                        double p = Convert.ToDouble(dataGridView1.Rows[n].Cells[5].Value = Convert.ToDecimal(k * o));
                        dataGridView1.Rows[n].Cells[6].Value = textBox9.Text;
                        dataGridView1.Rows[n].Cells[7].Value = comboBox1.Text;
                        dataGridView1.Rows[n].Cells[8].Value = dateTimePicker1.Text;                      
                        dataGridView1.Rows[n].Cells[9].Value = textBox16.Text.Replace("'", "’");



                    textBox7.Clear();
                    textBox8.Clear();
 
                    //kalkulimet

                    //count lines
                    kalkulimet.Class1 numrimet = new kalkulimet.Class1();
                    numrimet.countnr(dataGridView1, textBox11); //nr rreshtave

                    numrimet.calcsum(dataGridView1, textBox12); //totali

                    numrimet.perqindjafacture(textBox12, textBox9, textBox13);//perqindja

                    numrimet.totali(textBox12, textBox13, textBox14); // totali

                    //#end

                    //
                    textBox2.Visible = false;

                }

                catch (Exception)
                {

                }
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int row = 0;
            row = dataGridView1.Rows.Count - 1;
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox7.Text, @"[a-zA-Z ,;'!#%^&()_@`/\/*-+]+$"))
            {

                textBox7.Text = textBox7.Text.Remove(textBox7.Text.Length - 1);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox8.Text, @"[a-zA-Z ,;'!#%^&()_@`/\/*-+]+$"))
            {

                textBox8.Text = textBox8.Text.Remove(textBox8.Text.Length - 1);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {

                using (MySqlCommand cmd = new MySqlCommand("SELECT MAX(id) FROM listofclient where mode='"+comboBox1.Text+"'", conn))
                {
                    conn.Open();
                    double result = (Convert.ToDouble(cmd.ExecuteScalar()) + 1);
                    textBox1.Text = result.ToString();
                    conn.Close();
                }
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = textBox1.Text;
                    dataGridView1.Rows[i].Cells[7].Value = comboBox1.Text;
                }
                button3.Enabled = true;
            }



            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form frm = new Facture();
            frm.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                

            if (comboBox1.Text=="Devis")
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT IFNULL(MAX(id),0) FROM listofclient ", conn))
                {

                    conn.Open();
                    double result = (Convert.ToDouble(cmd.ExecuteScalar()) + 1);
                    textBox1.Text = result.ToString();
                    conn.Close();

                }

                comboBox4.Text = "";
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    
                    dataGridView1.Rows[i].Cells[7].Value = comboBox1.Text;
                }
            }
            else
            {
               
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                 
                    dataGridView1.Rows[i].Cells[7].Value = comboBox1.Text;
                }
                comboBox4.Text = "Non Payé";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
     
        private void button7_Click_1(object sender, EventArgs e)
        {

         
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT id,name_surname,company,adresse,tel,compte,comptenumbers,textextra,bank from listofclient where company=@company", conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@company", textBox3.Text);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                       
                        textBox4.Text = dr["adresse"].ToString();
                        textBox5.Text = dr["tel"].ToString();
                        textBox6.Text = dr["compte"].ToString();
                        richTextBox1.Text = dr["comptenumbers"].ToString();
                        textBox10.Text = dr["textextra"].ToString();
                        comboBox5.Text = dr["bank"].ToString();


                    }
                    conn.Close();
              

                    // MySqlDataAdapter cb = new MySqlDataAdapter("Select iii,Descriptif from productsofclient where client='" + textBox3.Text + "'", conn);
                    //MySqlCommand cmda = new MySqlCommand("select id_product,Descriptif from productsofclient where client='" + textBox3.Text + "'", conn);
                    //MySqlDataAdapter sda = new MySqlDataAdapter(cmda);
                    //DataTable dtt = new DataTable();
                    //sda.Fill(dtt);

                    //dreader = dtt.NewRow();
                    //dreader.ItemArray = new object[] { 0, "--Sélectionner--" };
                    //dtt.Rows.InsertAt(dreader, 0);

                    //comboBox3.ValueMember = "id_product";
                  
                    //comboBox3.DisplayMember = "Descriptif";
                    //comboBox3.DataSource = dtt;
                   
                    //conn.Close();
                 

                }
                catch (Exception) { }
              


            }
        }
     

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            


          


        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4.PerformClick();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4.PerformClick();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(30);


        }

        private void dateTimePicker1_Validated(object sender, EventArgs e)
        {
           
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button7_Click_3(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
           
         
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox3_Leave(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void comboBox3_Validating(object sender, CancelEventArgs e)
        {
         
        }

        private void comboBox3_SelectedIndexChanged_3(object sender, EventArgs e)
        {
            label8.Text = comboBox3.SelectedValue.ToString();
            try
            {
                textBox2.Text = comboBox3.Text.ToString();
                if (comboBox3.Text == comboBox3.Text)
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT prix from productsofclient where client=@client and Descriptif=@Descriptif and id_product=@id_product", conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id_product", label8.Text);
                    cmd.Parameters.AddWithValue("@client", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Descriptif", comboBox3.Text.ToString());
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        textBox8.Text = dr["prix"].ToString();

                    }
                    textBox2.Visible = true;
                    conn.Close();
                }

            }
            catch (Exception) { }

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
          
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
          

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                try
                {
                    int rowindex = dataGridView1.CurrentCell.RowIndex;
                    dataGridView1.Rows.RemoveAt(rowindex);
                    //count lines
                    kalkulimet.Class1 numrimet = new kalkulimet.Class1();
                    numrimet.countnr(dataGridView1, textBox11); //nr rreshtave

                    numrimet.calcsum(dataGridView1, textBox12); //totali

                    numrimet.perqindjafacture(textBox12, textBox9, textBox13);//perqindja

                    numrimet.totali(textBox12, textBox13, textBox14); // totali

                    //#end
                }

                catch (Exception)
                {

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dataGridView1;
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

        private void button7_Click_4(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            
        }
       
        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void richTextBox1_MultilineChanged(object sender, EventArgs e)
        {
            
      
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
    
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
        
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
        
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    for (int i = 0; i <= dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[6].Value = textBox9.Text;
                        double a, b, c, d;
                        a = double.Parse(textBox12.Text);
                        b = double.Parse(textBox9.Text);
                        c = (a / 100) * b;
                        d = (a / 100) * (100 - b);
                        textBox13.Text = c.ToString("#,0.00");
                        textBox14.Text = d.ToString("#,0.00");
                    }


                }
                catch (Exception)
                {

                }
            }
        }

        private void textBox16_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                try
                {
                    for (int i = 0; i <= dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[9].Value = textBox16.Text;

                    }
                    MessageBox.Show("Prorata: " + textBox16.Text);


                }
                catch (Exception)
                {

                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
           
        }

        private void label11_Click(object sender, EventArgs e)
        {
            ShowToolTip(sender, "Entre la rabbais et presse ENTER");
        }

        private void label21_Click(object sender, EventArgs e)
        {
         
            ShowToolTip(sender, "Entre Prorata et presse ENTER");
        }

        private void label11_MouseHover(object sender, EventArgs e)
        {
    
        }

        private void label21_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.Visible = true;
            }
            else
            {
                textBox2.Visible = false;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
           
        }

        private void label23_Click(object sender, EventArgs e)
        {
            
            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip2.Active = true;
            toolTip3.Active = true;
            toolTip4.Active = true;
            toolTip5.Active = true;
            toolTip6.Active = true;
            int durationMilliseconds = 10000;
            toolTip1.Show(toolTip1.GetToolTip(label11), label11, durationMilliseconds);
            toolTip2.Show(toolTip2.GetToolTip(tableLayoutPanel8), tableLayoutPanel8, durationMilliseconds);
            toolTip3.Show(toolTip3.GetToolTip(pictureBox1), pictureBox1, durationMilliseconds);
            toolTip4.Show(toolTip4.GetToolTip(panel8), panel8, durationMilliseconds);
            toolTip5.Show(toolTip5.GetToolTip(comboBox1), comboBox1, durationMilliseconds);
            toolTip6.Show(toolTip6.GetToolTip(button5), button5, durationMilliseconds);
        }
    }
}
