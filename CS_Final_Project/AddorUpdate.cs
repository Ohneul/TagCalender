using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CS_Final_Project
{
    public partial class AddorUpdate : Form
    {
        MySqlConnection sqlconn = new MySqlConnection("SERVER = localhost; DATABASE = calender; UID = root; PWD = 8463;");
        string lvwitem;
        Calender cld;
        public AddorUpdate(Calender clr, string str)
        {
            InitializeComponent();
            cld = clr;
            button3.Text = str;
            checkedListBox1.Items.Clear();
        }

        private void AddorUpdate_Load(object sender, EventArgs e)
        {
            string[] tg = cld.textBox2.Text.Split(',');
            int tgleng = tg.Length;
            if (button3.Text == "수정")
            {
                textBox2.Text = cld.textBox6.Text;
                textBox1.Text = cld.textBox1.Text;
                string[] dt = cld.textBox3.Text.Split('~');
                dateTimePicker1.Value = DateTime.Parse(dt[0].Trim());
                if (dt.Length > 1)
                    dateTimePicker2.Value = DateTime.Parse(dt[1].Trim());
                else
                    dateTimePicker2.Value = DateTime.Parse(dt[0].Trim());
                cld.textBox1.Text = "";
                cld.textBox2.Text = "";
                cld.textBox3.Text = "";
                cld.textBox6.Text = "";
            }
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("SELECT tagvalue FROM tag WHERE id = '" + cld.id + "';", sqlconn);
            MySqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read() == true)
            {
                int cheq = 0;
                lvwitem = sqldr[0].ToString();
                if (button3.Text == "수정")
                {
                    for (int i = 0; i < tgleng; i++)
                    {
                        if (tg[i].Trim() == lvwitem.Trim())
                        {
                            cheq = 1;
                            break;
                        }
                    }
                }
                if (cheq == 1)
                    checkedListBox1.Items.Add(lvwitem, true);
                else
                    checkedListBox1.Items.Add(lvwitem);
            }
            sqlconn.Close();
        }

        private void Add_Tag(object sender, EventArgs e)
        {
            string str = textBox3.Text.Trim();
            if (str == "")
            {
                MessageBox.Show("추가할 태그를 입력해주세요.", "경고");
                textBox3.Focus();
                return;
            }
            else if (str.Contains(","))
            {
                MessageBox.Show(", 를 제외한 태그를 입력해주세요.", "경고");
                textBox3.Focus();
                return;
            }
            else if (str.Length > 10)
            {
                MessageBox.Show("10byte 이하의 태그를 입력해주세요.", "경고");
                textBox3.Focus();
                return;
            }
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("INSERT INTO tag VALUES('" + textBox3.Text.Trim() + "', '" + cld.id + "')", sqlconn);
            sqlcmd.Connection = sqlconn;
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            checkedListBox1.Items.Add(textBox3.Text);
        }

        private void Delete_Tag(object sender, EventArgs e)
        {
            string str = textBox3.Text.Trim();
            if (str == "")
            {
                MessageBox.Show("삭제할 태그를 입력해주세요.", "경고");
                textBox3.Focus();
                return;
            }
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("DELETE FROM tag WHERE tagvalue = '" + textBox3.Text + "'", sqlconn);
            sqlcmd.Connection = sqlconn;
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            checkedListBox1.Items.Remove(textBox3.Text);
        }

        private void AddUpdate_Btn(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("잘못된 기간입니다.", "경고");
                return;
            }
            else if (checkedListBox1.CheckedItems.Count > 5)
            {
                MessageBox.Show("태그는 5개 이하로 선택해주세요.", "경고");
                return;
            }
            else if (textBox2.Text.Length > 50)
            {
                MessageBox.Show("50byte 이하의 제목을 적어주세요.", "경고");
                textBox2.Focus();
                return;
            }
            else if (textBox1.Text.Length > 2000)
            {
                MessageBox.Show("2000byte 이하의 내용을 적어주세요.", "경고");
                textBox1.Focus();
                return;
            }
            string tag = "";
            int x = 0;
            foreach (string str in checkedListBox1.CheckedItems)
            {
                if (checkedListBox1.CheckedItems.Count - 1 == x)
                    tag += str.Trim();
                else
                {
                    tag += str.Trim() + ", ";
                    x++;
                }
            }
            if (button3.Text == "추가")
            {
                int max = 0;
                sqlconn.Open();
                MySqlCommand sqlcmd = new MySqlCommand("SELECT snum FROM cldr;", sqlconn);
                MySqlDataReader sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read() == true)
                {
                    if (max < int.Parse(sqldr[0].ToString()))
                    {
                        max = int.Parse(sqldr[0].ToString());
                        cld.savenum = max + 1;
                    }
                }
                sqlconn.Close();
                sqlconn.Open();
                MySqlCommand sqlcmd1 = new MySqlCommand("INSERT INTO cldr VALUES('" + cld.savenum + "', '" + cld.id + "', '" + dateTimePicker1.Text + "', '" + dateTimePicker2.Text + "', '" + textBox2.Text + "', '" + tag + "', '" + textBox1.Text + "')", sqlconn);
                sqlcmd1.Connection = sqlconn;
                sqlcmd1.ExecuteNonQuery();
                MySqlCommand sqlcmd2 = new MySqlCommand("UPDATE savevalue SET value1 = '" + cld.savenum + "' WHERE value2 = '" + cld.n + "'", sqlconn);
                sqlcmd2.Connection = sqlconn;
                sqlcmd2.ExecuteNonQuery();
                sqlconn.Close();
                MessageBox.Show("추가되었습니다.", "완료");
                cld.button8.Text = "";
                this.Close();
            }
            else if (button3.Text == "수정")
            {
                sqlconn.Open();
                MySqlCommand sqlcmd3 = new MySqlCommand("UPDATE cldr SET date1 = '" + dateTimePicker1.Text + "', date2 = '" + dateTimePicker2.Text + "', title = '" + textBox2.Text + "', tag = '" + tag + "', contents = '" + textBox1.Text + "' WHERE snum = '" + cld.updatenum + "'", sqlconn);
                sqlcmd3.Connection = sqlconn;
                sqlcmd3.ExecuteNonQuery();
                sqlconn.Close();
                MessageBox.Show("수정되었습니다.", "완료");
                cld.button8.Text = "";
                this.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker2.Enabled = false;
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
            else
                dateTimePicker2.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                dateTimePicker2.Value = dateTimePicker1.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
