using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace DatabaseExample
{
    public partial class Form1 : Form
    {
        SQLiteConnection con = new SQLiteConnection("Data Source=Example.db;version=3");
        SQLiteCommand cmd;
        SQLiteDataAdapter ad;
        DataSet ds;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SQLiteConnection("Data Source=Example.db;version=3");
            // TODO: This line of code loads data into the 'databaseExampleDataSet.Info' table. You can move, or remove it, as needed.
            // this.infoTableAdapter.Fill(this.databaseExampleDataSet.Info);
            FillData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string name = textBox2.Text;
            string address = textBox3.Text;

            cmd = new SQLiteCommand();

           
            cmd.CommandText = "select * from Info where id='" + textBox1.Text + "'";
            
            cmd.Connection = con;
            con.Open();
            SQLiteDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                MessageBox.Show("insert new id");
                con.Close();
            }
            else
            {
                con.Close();
                
                con = new SQLiteConnection("Data Source=Example.db;version=3");
                cmd = new SQLiteCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into Info(id,name,address) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("inserted successfully");
                con.Close();
                FillData();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "update Info set name='" + textBox2.Text + "',Address='" + textBox3.Text + "' where id='" + textBox1.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Update Successfull");
            FillData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "delete from Info where id='"+textBox1.Text+"'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Delete Successfull");
            FillData();
        }

        public void FillData()
        {
            DataTable dt = new DataTable();
            con = new SQLiteConnection("Data Source=Example.db;Version=3;");
            ad = new SQLiteDataAdapter("Select *From Info", con);
            ds = new DataSet();
            con.Open();
            ad.Fill(ds, "Info");
            dataGridView1.DataSource = ds.Tables["Info"];

            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();

                //textBox1.Text = row.Cells["Id"].Value.ToString();
                //textBox2.Text = row.Cells["Name"].Value.ToString();
                //textBox3.Text = row.Cells["Address"].Value.ToString();

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MysqlexampleDbEx my = new MysqlexampleDbEx();
            my.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("fxdf");
        }
    }
}
