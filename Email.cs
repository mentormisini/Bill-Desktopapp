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
    public partial class Email : Form
    {
        public Email()
        {
            InitializeComponent();
            this.Size = new Size(1250, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Email_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
         



        }
     

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form frm = new Menu();
            frm.Show();
            this.Close();
        }
    }
}
