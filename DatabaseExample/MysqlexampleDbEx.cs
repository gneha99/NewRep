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

namespace DatabaseExample
{
    public partial class MysqlexampleDbEx : Form
    {
        MySqlConnection con = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=dbex; SslMode = none");
        MySqlCommand cmd;
        MySqlDataAdapter ad;
        DataSet ds;
           
        public MysqlexampleDbEx()
        {
            InitializeComponent();
        }

        private void MysqlexampleDbEx_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand();

            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from tableex where id='" + textBox1.Text + "'";
            MySqlDataReader rd =cmd.ExecuteReader();
            if (rd.HasRows)
            {
                MessageBox.Show("insert new id");
                con.Close();
            }
            else
            {
                con.Close();
                cmd = new MySqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into tableex(id,name,address) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("inserted successfully");
                con.Close();
                FillData();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "update tableex set name='" + textBox2.Text + "',Address='" + textBox3.Text + "' where id='" + textBox1.Text + "'";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Update Successfull");
            FillData();
        }

        public void FillData()
        {
            DataTable dt = new DataTable();
            con = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=dbex; SslMode = none");
            ad = new MySqlDataAdapter("Select *From tableex", con);
            ds = new DataSet();
            con.Open();
            ad.Fill(ds, "tableex");
            dataGridView1.DataSource = ds.Tables["tableex"];

            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "delete from tableex where id='" + textBox1.Text + "'";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Delete Successfull");
            FillData();
        }
    }
}
