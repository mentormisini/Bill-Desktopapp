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

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace BlancGastroApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on the link below to continue learning how to build a desktop app using WinForms!


        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        MySqlConnection conn = new MySqlConnection(dbconnect.dbcon());
    
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {


                conn.Open();
           
                label1.Text = "Connexion:OK";
                conn.Close();

                MySqlDataAdapter cb = new MySqlDataAdapter("Select Username from users", conn);
             
                DataTable cm = new DataTable();
                cb.Fill(cm);
                DataRow row = cm.NewRow();
                comboBox1.DataSource = cm;
                comboBox1.DisplayMember = "Username";

            }
            catch (MySqlException)
            {
                label1.ForeColor = Color.Red;
                label1.Text = "Connexion:Pas connecté";
                button2.Visible = true;

            }

          
        }
        public static string passingtext;
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string select = "SELECT * FROM users where Username='" + comboBox1.Text + "' AND Password='" + this.textBox1.Text + "'";
                MySqlCommand cmd = new MySqlCommand(select, conn);
                MySqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    passingtext = comboBox1.Text;
                    Form frm = new Menu();
                    frm.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Mode de passe Incorrect");
                    textBox1.Clear();
                }
                conn.Close();

            }
            catch (Exception) { }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==Keys.Enter)
            {
                button1.PerformClick();
               
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            pictureBox5.Visible = true;
            pictureBox5.Image = Properties.Resources.hid;
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {

            textBox1.UseSystemPasswordChar= PasswordPropertyTextAttribute.Yes.Password;
            pictureBox5.Image = Properties.Resources.vv;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
            pictureBox5.Image = Properties.Resources.hid;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form frm = new Form1();
            frm.Show();
            this.Hide();
        }
    }
}
