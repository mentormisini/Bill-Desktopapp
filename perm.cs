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
    public partial class perm : Form
    {
        public perm()
        {
            InitializeComponent();
        }
        private void moveUp()
        {
            if (dataGridView.RowCount > 0)
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    int rowCount = dataGridView.Rows.Count;
                    int index = dataGridView.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dataGridView.Rows;

                    // remove the previous row and add it behind the selected row.
                    DataGridViewRow prevRow = rows[index - 1];
                    rows.Remove(prevRow);
                    prevRow.Frozen = false;
                    rows.Insert(index, prevRow);
                    dataGridView.ClearSelection();
                    dataGridView.Rows[index - 1].Selected = true;
                }
            }
        }

        private void moveDown()
        {
            if (dataGridView.RowCount > 0)
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    int rowCount = dataGridView.Rows.Count;
                    int index = dataGridView.SelectedCells[0].OwningRow.Index;

                    if (index == (rowCount - 2)) // include the header row
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dataGridView.Rows;

                    // remove the next row and add it in front of the selected row.
                    DataGridViewRow nextRow = rows[index + 1];
                    rows.Remove(nextRow);
                    nextRow.Frozen = false;
                    rows.Insert(index, nextRow);
                    dataGridView.ClearSelection();
                    dataGridView.Rows[index + 1].Selected = true;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Add();
            moveUp();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            moveDown();
        }

        MySqlConnection conn = new MySqlConnection(dbconnect.dbcon());
        private void perm_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter SDA = new MySqlDataAdapter("SELECT * FROM productsofclient where id_product=1", conn);
            conn.Open(); SDA.Fill(dt); dataGridView2.DataSource = dt;
            int n = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (dataGridView2.Rows.Count != n + 1)
                {
                    dataGridView.Rows.Add();
                    dataGridView.Rows[n].Cells[0].Value = row.Cells[0].Value.ToString();//prod
                    dataGridView.Rows[n].Cells[1].Value = row.Cells[1].Value.ToString();//sasia
                    dataGridView.Rows[n].Cells[2].Value = row.Cells[2].Value.ToString();//cmimi/
                    dataGridView.Rows[n].Cells[3].Value = row.Cells[3].Value.ToString();//total
                    dataGridView.Rows[n].Cells[4].Value = row.Cells[4].Value.ToString();//kamarjeri tav
                    dataGridView.Rows[n].Cells[5].Value = row.Cells[5].Value.ToString();//uid
                    dataGridView.Rows[n].Cells[6].Value = row.Cells[6].Value.ToString();//data


                }
                n += 1;


            }
            conn.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            // dataGridView1.Columns[5].ReadOnly = true;


            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (dataGridView2.Rows.Count != n + 1)
                {
                    dataGridView.Rows.Add();
                    dataGridView.Rows[n].Cells[0].Value = row.Cells[0].Value.ToString();//prod
                    dataGridView.Rows[n].Cells[1].Value = row.Cells[1].Value.ToString();//sasia
                    dataGridView.Rows[n].Cells[2].Value = row.Cells[2].Value.ToString();//cmimi/
                    dataGridView.Rows[n].Cells[3].Value = row.Cells[3].Value.ToString();//total
                    dataGridView.Rows[n].Cells[4].Value = row.Cells[4].Value.ToString();//kamarjeri tav
                    dataGridView.Rows[n].Cells[5].Value = row.Cells[5].Value.ToString();//uid
                    dataGridView.Rows[n].Cells[6].Value = row.Cells[6].Value.ToString();//data
     

                }
                n += 1;


            }
        }
    }
    
}
