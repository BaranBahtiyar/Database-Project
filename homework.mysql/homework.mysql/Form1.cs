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

namespace homework.mysql
{
    public partial class Form1 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=homework;Uid=root;Pwd=baran2401;");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        String gender;

        public Form1()
        {
            InitializeComponent();
        }

        void veriyiGetir()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT*FROM customer", conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            veriyiGetir();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            String cins = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            if(cins == "male")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                String sql = "insert into customer(id_of_customer, namesurname_of_customer, gender_of_customer, age_of_customer, reservation, total_money_spend)" +
                    "values(@id, @namesurname, @gender, @age, @date, @money)";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", textBox4.Text);
            cmd.Parameters.AddWithValue("@namesurname", textBox1.Text);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@age", textBox2.Text);
            cmd.Parameters.AddWithValue("@date", textBox5.Text);
            cmd.Parameters.AddWithValue("@money", textBox3.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            veriyiGetir();
            MessageBox.Show("Added");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "female";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String sql = "delete from customer where id_of_customer = @id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", textBox4.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            veriyiGetir();
            MessageBox.Show("Deleted");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "namesurname_of_customer LIKE '" + textBox6.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
