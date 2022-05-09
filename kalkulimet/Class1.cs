using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BlancGastroApp.kalkulimet
{
    class Class1
    {
        //winform rechercher
        public void calc(DataGridView dg,TextBox mbledhja)
        {
            decimal val = 0;
            foreach (DataGridViewRow item in dg.Rows)
            {
                if (item.Cells[3] != null && item.Cells[3].Value != null)
                    val += Convert.ToDecimal(item.Cells[3].Value);
            }
            mbledhja.Text = val.ToString("#,0.00");

         
        }
        public void countnr(DataGridView dg, TextBox txt)
        {
            ///nr rreshtave
            int count;
            count = int.Parse(dg.RowCount.ToString());
            txt.Text = count.ToString();
        }

 
         public void perqindja(TextBox aa,Label bb,TextBox cc)
        {
            ////perc
            double a, b, c;
            a = double.Parse(aa.Text);
            b = double.Parse(bb.Text);
            c = (a / 100) * b;
            cc.Text = c.ToString();
        }



        public void maxva (DataGridView dg, Label l)
        {
            //maxvalue
            int max = 0;
            for (int i = 0; i <= dg.Rows.Count - 1; i++)
            {
                if (max < int.Parse(dg.Rows[i].Cells[4].Value.ToString()))
                {
                    max = int.Parse(dg.Rows[i].Cells[4].Value.ToString());
                }
            }

            l.Text = max.ToString();

        }


        public void maxvalueedit(DataGridView dg, TextBox t)
        {
            //maxvalue
            int max = 0;
            for (int i = 0; i <= dg.Rows.Count - 1; i++)
            {
                if (max < int.Parse(dg.Rows[i].Cells[6].Value.ToString()))
                {
                    max = int.Parse(dg.Rows[i].Cells[6].Value.ToString());
                }
            }

            t.Text = max.ToString();

        }

        public void totali(TextBox t1,TextBox t2,TextBox t3)
        {
            ////totali
            double l, q, jj;
            l = double.Parse(t1.Text);
            q = double.Parse(t2.Text);
            jj = l - q;
            t3.Text = jj.ToString("#,0.00");
        }




        ///facture form 
        ///


        public void perqindjafacture(TextBox aa, TextBox bb, TextBox cc)
        {
            ////perc
            double a, b, c;
            a = double.Parse(aa.Text);
            b = double.Parse(bb.Text);
            c = (a / 100) * b;
            cc.Text = c.ToString();
        }

        public void calcsum(DataGridView dg, TextBox mbledhja)
        {
            decimal val = 0;
            foreach (DataGridViewRow item in dg.Rows)
            {
                if (item.Cells[4] != null && item.Cells[4].Value != null)
                    val += Convert.ToDecimal(item.Cells[4].Value);
            }
            mbledhja.Text = val.ToString("#,0.00");
        }

        public void calcsummodifier(DataGridView dg, TextBox mbledhja)
        {
            decimal val = 0;
            foreach (DataGridViewRow item in dg.Rows)
            {
                if (item.Cells[5] != null && item.Cells[5].Value != null)
                    val += Convert.ToDecimal(item.Cells[5].Value);
            }
            mbledhja.Text = val.ToString("#,0.00");
        }



    }
}
