using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBookDirectory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;

        private void buttonSave_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into PhoneBookUserSQL values ('"+textBoxPhoneNo.Text+"','"+textBoxFullName.Text+ "','" + textBoxEmail.Text + "','" + textBoxAddress.Text + "','" + textBoxDescription.Text + "')", con);
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("You have successfully inserted a record.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                SqlCommand cmd = new SqlCommand("Update PhoneBookUserSQL set phoneNo='"+textBoxPhoneNo.Text+"', fullName='"+textBoxFullName.Text+ "',email='" + textBoxEmail.Text + "',_address='" + textBoxAddress.Text + "',_description='" + textBoxDescription.Text + "' where id='"+ID+"'", con);
                cmd.Connection = con;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("You have successfully updated a record.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1_Load(null,  null);
          
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Delete from PhoneBookUserSQL where id= '"+ID+"'");
            cmd.Connection = con;

            MessageBox.Show("You have successfully deleted a record.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            
            Form1_Load(null, null);




        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            textBoxPhoneNo.Clear();
            textBoxFullName.Clear();
            textBoxEmail.Clear();
            textBoxAddress.Clear();
            textBoxDescription.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from PhoneBookUserSQL");
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Datagridview ID value assignment
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }


                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                SqlCommand cmd = new SqlCommand("Select * from PhoneBookUserSQL where id = '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                textBoxPhoneNo.Text = ds.Tables[0].Rows[0][1].ToString();
                textBoxFullName.Text = ds.Tables[0].Rows[0][2].ToString();
                textBoxEmail.Text = ds.Tables[0].Rows[0][3].ToString();
                textBoxAddress.Text = ds.Tables[0].Rows[0][4].ToString();
                textBoxDescription.Text = ds.Tables[0].Rows[0][5].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
