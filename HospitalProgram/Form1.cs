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
        // global variable
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
            toolStripStatusLabel1.Text = "Status: successfully geted";


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
            toolStripStatusLabel1.Text = "Status: successfully added";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            connectinString.Open();
            MySqlCommand command = connectinString.CreateCommand();
            command.CommandText = "SELECT count(*) FROM hospitaldatabase.patient";
            last_id = System.Convert.ToInt32( command.ExecuteScalar());
            for (int i = 1; i<= last_id; i++)
            {
                comboBox1.Items.Add(i);

            }
            connectinString.Close();
            Form_Login fl = new Form_Login();
            fl.ShowDialog();
            // Bottom status
            toolStripStatusLabel1.Text = "Status: ready";
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connectinString.Open();
            MySqlCommand command = connectinString.CreateCommand();
            command.CommandText = "DELETE FROM patient WHERE name ='" + textBox3.Text+"'";
            command.ExecuteNonQuery();
            connectinString.Close();
            // Bottom status
            toolStripStatusLabel1.Text = "Status: successfully deleted";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connectinString.Open();
            MySqlCommand command = connectinString.CreateCommand();
            command.CommandText = "UPDATE patient SET name = '" + textBox3.Text + "' WHERE name='" + textBox4.Text+"'";
            command.ExecuteNonQuery();
            connectinString.Close();
            // Bottom status
            toolStripStatusLabel1.Text = "Status: successfully changed";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("ID | Name | Days | City");
            string list_line = ""; // string line to add all information in listBox1
            connectinString.Open();
            MySqlCommand command = connectinString.CreateCommand();
            command.CommandText = "SELECT * FROM patient";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list_line = list_line + reader["idpatient"].ToString() + "  | ";
                list_line = list_line + reader["name"].ToString()+" | ";
                list_line = list_line + reader["days"].ToString() + " | ";
                list_line = list_line + reader["city"].ToString();
                list_line = list_line + "\n";
               listBox1.Items.Add(list_line);
                list_line = "";
            }
            //Close connections
            connectinString.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Error, choose other index","Error" , MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            string templ_line = ""; // string line to get information from listbox1
            int templ_indx = 0;   // index of listbox line
            int length_line = 0; // length of line
            int [] symbol_position = new int[3];
            int counter = 0;
            int [] cut_len = new int[3]; // variable to length cut
            
            // main variables
            int idpatient;
            int days;
            string name;
            string city;

            templ_indx =listBox1.SelectedIndex;
            templ_line = listBox1.Items[templ_indx].ToString();
            
            length_line = templ_line.Length;
            for (int i = 0;i<length_line ; ++i)
            {
                if (templ_line[i] == '|')
                {
                    symbol_position[counter] = i+2;
                    ++counter;
                }  

            }
            cut_len[0] = (symbol_position[1] - symbol_position[0])-2;
            cut_len[1] = (symbol_position[2] - symbol_position[1])-2;
            cut_len[2] = length_line - symbol_position[2];
            idpatient = Int32.Parse(templ_line.Substring(0,1));
            name = templ_line.Substring(symbol_position[0],cut_len[0]);
            days = Int32.Parse(templ_line.Substring(symbol_position[1], cut_len[1]));
            city = templ_line.Substring(symbol_position[2], cut_len[2]);

            // show information
            label1.Text = "Name: " + name;
            label2.Text = "Days: " + days;
            label3.Text = "City: " + city;
            comboBox1.Text = idpatient.ToString();
            textBox3.Text = name;

        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            
                
        
         
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Login fl = new Form_Login();
            fl.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "Name:";
            label2.Text = "Days:";
            label3.Text = "City:";
            textBox1.Text = "Name";
            domainUpDown1.Text = "1";
            textBox2.Text = "City";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "-select id-";
            listBox1.Items.Clear();
            listBox1.Items.Add("ID | Name | Days | City");

        }
    }
}
