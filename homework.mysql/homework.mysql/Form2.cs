using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace homework.mysql
{
    public partial class Form2 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=homework;Uid=root;Pwd=baran2401;");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt1;

        public Form2()
        {
            InitializeComponent();
        }

        void veriGetir()
        {
            dt1 = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT*FROM employee", conn);
            adapter.Fill(dt1);
            dataGridView1.DataSource = dt1;
            conn.Close();
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            veriGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "insert into employee (idno_of_employee, namesurname_of_employee, age_of_employee, salary_of_employee, total_working_hours)" +
                "values(@id, @namesurname, @age, @salary, @hours)";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@namesurname", textBox2.Text);
            cmd.Parameters.AddWithValue("@age", textBox3.Text);
            cmd.Parameters.AddWithValue("@salary", textBox4.Text);
            cmd.Parameters.AddWithValue("@hours", textBox5.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            veriGetir();
            MessageBox.Show("Added");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String sql = "delete from customer where idno_of_employee = @id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            veriGetir();
            MessageBox.Show("Deleted");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt1.DefaultView;
            dv.RowFilter = "namesurname_of_employee LIKE '" + textBox2.Text + "%'";
            dataGridView1.DataSource = dv;
        }
    }
}
