using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CS_Final_Project
{
    public partial class InfoUpdate : Form
    {
        MySqlConnection sqlconn = new MySqlConnection("SERVER = localhost; DATABASE = calender; UID = root; PWD = 8463;");
        Calender Cl;
        public InfoUpdate(Calender Cdr)
        {
            InitializeComponent();
            Cl = Cdr;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("SELECT * FROM member WHERE id = '" + Cl.id + "';", sqlconn);
            MySqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read() == true)
            {
                if(textBox1.Text == sqldr[1].ToString())
                {
                    textBox1.Text = "";
                    Join_Panel.Visible = false;
                    panel4.Visible = true;
                    join_txt2.Text = sqldr[1].ToString();
                    string[] telnum = sqldr[2].ToString().Split('-');
                    string[] mailstr = sqldr[3].ToString().Split('@');
                    join_txt3.Text = telnum[0].Trim();
                    join_txt4.Text = telnum[1].Trim();
                    join_txt5.Text = telnum[2].Trim();
                    join_txt6.Text = mailstr[0].Trim();
                    join_txt7.Text = mailstr[1].Trim();
                }
                else
                {
                    textBox1.Text = "";
                    textBox1.SelectAll();
                    textBox1.Focus();
                    MessageBox.Show("비밀번호가 틀렸습니다.", "경고");
                }
            }
            sqlconn.Close();
        }

        private void Cancel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Join_Comple_Click(object sender, EventArgs e)
        {
            string tel = join_txt3.Text + "-" + join_txt4.Text + "-" + join_txt5.Text;
            string mail = join_txt6.Text + "@" + join_txt7.Text;
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("UPDATE member SET pw = '" + join_txt2.Text + "', phone = '" + tel + "', mail = '" + mail + "' WHERE id = '" + Cl.id + "'", sqlconn);
            sqlcmd.Connection = sqlconn;
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            MessageBox.Show("수정되었습니다.", "완료");
            this.Close();
        }
    }
}
