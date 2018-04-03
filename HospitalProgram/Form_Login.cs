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
    
    public partial class Form_Login : Form
    {
     
        MySqlConnection connectinString = new MySqlConnection("server=Localhost;user id=root;database=hospitaldatabase;password=h4647dai;");
        public Form_Login()
        {
            InitializeComponent();
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i=0;

            connectinString.Open();
            MySqlCommand command = connectinString.CreateCommand();
            command.CommandText = "SELECT * FROM login_user WHERE username='"+ textBox1.Text+"' AND password ='"+textBox2.Text+"'";
            command.ExecuteNonQuery();
            DataTable data_table = new DataTable();
            MySqlDataAdapter data_adapter = new MySqlDataAdapter(command);
            data_adapter.Fill(data_table);
            i = Int32.Parse(data_table.Rows.Count.ToString());
            if (i == 0)
            {
                MessageBox.Show("Bad password or username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            { 
                connectinString.Close();
                this.Close();
            }
            connectinString.Close();

        }
    }
}
