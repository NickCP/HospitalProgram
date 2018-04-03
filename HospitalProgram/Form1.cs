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

namespace HospitalProgram
{
    public partial class Form1 : Form
    {
        // global varuable
        long last_id;
        MySqlConnection connectinString = new MySqlConnection("server=Localhost;user id=root;database=hospitaldatabase;password=h4647dai;");


        public Form1()
        {
           
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           connectinString.Open();
            MySqlCommand command = connectinString.CreateCommand();
            command.CommandText = "SELECT * FROM patient WHERE idpatient="+comboBox1.SelectedItem.ToString()+"";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                label1.Text = "Name: "+ reader["name"].ToString();
                label2.Text = "Days: " + reader["days"].ToString();
                label3.Text = "City: " + reader["city"].ToString();
            }
            //Close connections
            connectinString.Close();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Adding a new visitor information in database
            connectinString.Open();
            MySqlCommand command = connectinString.CreateCommand();
            command.CommandText = "INSERT  INTO hospitaldatabase.patient (name,days,city) values ('" +textBox1.Text + "','"+domainUpDown1.Text+"','"+textBox2.Text+"')";
            command.ExecuteNonQuery();
            last_id = last_id + 1;
            comboBox1.Items.Add(last_id);
            connectinString.Close();
            // Bottom status
            toolStripStatusLabel1.Text = toolStripStatusLabel1.Text + "Successfully added";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            connectinString.Open();
            MySqlCommand command = connectinString.CreateCommand();
            command.CommandText = "SELECT count(*) FROM hospitaldatabase.patient";
            last_id = System.Convert.ToInt32( command.ExecuteScalar());
            for (int i = 0; i< last_id; i++)
            {
                comboBox1.Items.Add(i);

            }
            connectinString.Close();
            // Bottom status
            toolStripStatusLabel1.Text = toolStripStatusLabel1.Text + "Ready";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connectinString.Open();
            MySqlCommand command = connectinString.CreateCommand();
            command.CommandText = "DELETE FROM patient WHERE name ='" + textBox3.Text+"'";
            command.ExecuteNonQuery();
            connectinString.Close();
            // Bottom status
            toolStripStatusLabel1.Text = toolStripStatusLabel1.Text + "Successfully deleted";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connectinString.Open();
            MySqlCommand command = connectinString.CreateCommand();
            command.CommandText = "UPDATE patient SET name = '" + textBox3.Text + "' WHERE name='" + textBox4.Text+"'";
            command.ExecuteNonQuery();
            connectinString.Close();
            // Bottom status
            toolStripStatusLabel1.Text = toolStripStatusLabel1.Text + "Successfully changed";
        }
    }
}
