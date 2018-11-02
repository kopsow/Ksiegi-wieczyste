using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ksiegi_wieczyste
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=145.239.91.163;Database=polskie_znaki;User Id=tomek;Password=koperski82!;";

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "select distinct(kw),dzial_1o from eukw";


                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    string kw = dr.GetString(0).Replace(" ", string.Empty);
                    string[] numer_kw = kw.Split("/".ToCharArray());
                    richTextBox1.AppendText(kw+": "+Parsery.powierzchnia(dr.GetString(1)));
                    richTextBox1.AppendText(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {

                conn.Close();
            }

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void przywrócProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
}
