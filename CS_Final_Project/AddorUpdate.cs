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

namespace CS_Final_Project
{
    public partial class AddorUpdate : Form
    {
        MySqlConnection sqlconn = new MySqlConnection("SERVER = localhost; DATABASE = calender; UID = root; PWD = 8463;");
        ListViewItem lvwitem;
        Calender cld;
        public AddorUpdate(Calender clr, string str)
        {
            InitializeComponent();
            this.cld = clr;
            button3.Text = str;
        }

        private void Add_Tag(object sender, EventArgs e)
        {
            checkedListBox1.Items.Add(textBox3.Text);
        }

        private void Delete_Tag(object sender, EventArgs e)
        {
            checkedListBox1.Items.Remove(textBox3.Text);
        }

        private void AddUpdate_Btn(object sender, EventArgs e)
        {
            string tag = "";
            foreach(string str in checkedListBox1.CheckedItems)
            {
                tag += str + ", ";
            }
            if(button3.Text == "추가")
            {
                sqlconn.Open();
                MySqlCommand sqlcmd1 = new MySqlCommand("INSERT INTO cldr VALUES('" + cld.savenum + "', '" + dateTimePicker1.Text + "', '" + dateTimePicker2.Text + "', '" + textBox2.Text + "', '" + tag + "', '" + textBox1.Text + "')", sqlconn);
                sqlcmd1.Connection = sqlconn;
                sqlcmd1.ExecuteNonQuery();
                cld.savenum++;
                MySqlCommand sqlcmd2 = new MySqlCommand("UPDATE savevalue SET value1 = '" + cld.savenum + "' WHERE value2 = '" + cld.n + "'", sqlconn);
                sqlcmd2.Connection = sqlconn;
                sqlcmd2.ExecuteNonQuery();
                sqlconn.Close();
            }
            else if(button3.Text == "수정")
            {
                sqlconn.Open();
                MySqlCommand sqlcmd = new MySqlCommand("UPDATE cldr SET date1 = '" + dateTimePicker1.Text + "', date2 = '" + dateTimePicker2.Text + "', title = '" + textBox2.Text + "', tag = '" + tag + "', contents = '" + textBox1.Text + "' WHERE snum = '" + cld.updatenum + "'", sqlconn);
                sqlcmd.Connection = sqlconn;
                sqlcmd.ExecuteNonQuery();
                sqlconn.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                dateTimePicker2.Enabled = true;
            else
            {
                dateTimePicker2.Enabled = false;
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
                dateTimePicker2.Value = dateTimePicker1.Value;
        }
    }
}
