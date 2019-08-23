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
    public partial class Calender : Form
    {
        MySqlConnection sqlconn = new MySqlConnection("SERVER = localhost; DATABASE = calender; UID = root; PWD = 8463;");
        ListViewItem lvwitem;
        public int savenum, n, updatenum;
        public Calender()
        {
            InitializeComponent(); // 가까운 일정부터 로드되게하기***************************************************
            refr();
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("SELECT * FROM savevalue;", sqlconn);
            MySqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read() == true)
            {
                savenum = int.Parse(sqldr[0].ToString());
                n = int.Parse(sqldr[1].ToString());
            }
            sqldr.Close();
            sqlconn.Close();
        }
        // 일정 검색 버튼*****************************************
        private void Search_Calender(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            int[] searchvalue = new int[savenum];
            int arrsearch = 0;
            sqlconn.Open();
            MySqlCommand sqlcm = new MySqlCommand("SELECT * FROM cldr;", sqlconn);
            MySqlDataReader sqld = sqlcm.ExecuteReader();
            while (sqld.Read())
            {
                for (DateTime dt = DateTime.Parse(dateTimePicker1.Text); dt <= DateTime.Parse(dateTimePicker2.Text); dt = dt.AddDays(1))
                {
                    if (sqld[1].ToString() == sqld[2].ToString())
                    {
                        if (dt == DateTime.Parse(sqld[1].ToString()))
                            searchvalue[arrsearch++] = int.Parse(sqld[0].ToString());
                    }
                    else if (sqld[1].ToString() != sqld[2].ToString())
                    {
                        for (DateTime dat = DateTime.Parse(sqld[1].ToString()); dat <= DateTime.Parse(sqld[2].ToString()); dat = dat.AddDays(1))
                            if (dt == dat)
                            {
                                searchvalue[arrsearch++] = int.Parse(sqld[0].ToString());
                                break;
                            }
                    }
                }
            }
            textBox3.Text = searchvalue[0] + "";
            sqld.Close();
            MySqlCommand sqlcmd = new MySqlCommand("SELECT * FROM cldr;", sqlconn);
            MySqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read() == true)
            {
                for (int i = 0; i <= arrsearch - 1; i++)
                {
                    if(searchvalue[i] == int.Parse(sqldr[0].ToString()))
                    {
                        string date = sqldr[1].ToString();
                        if (sqldr[2].ToString() != date)
                            date += " ~ " + sqldr[2].ToString();
                        lvwitem = new ListViewItem(sqldr[0].ToString());
                        lvwitem.SubItems.Add(date);
                        lvwitem.SubItems.Add(sqldr[3].ToString());
                        lvwitem.SubItems.Add(sqldr[4].ToString());
                        lvwitem.SubItems.Add(sqldr[5].ToString());
                        listView1.Items.Add(lvwitem);
                        break;
                    }
                }
            }
            sqlconn.Close();
        }
        // 선택 취소 버튼
        private void Cancel_Search(object sender, EventArgs e)
        {
            refr();
            textBox3.Clear();
            textBox6.Clear();
            textBox2.Clear();
            textBox1.Clear();
        }
        // 세부 검색 버튼
        private void Detail_Search(object sender, EventArgs e)
        {
            Searcher f2 = new Searcher();
            f2.Show();
        }
        // 추가 및 수정 버튼
        private void AddUpdate_Calender(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
            {
                AddorUpdate f3 = new AddorUpdate(this, "추가");
                f3.Show();
            }
            else if (listView1.SelectedIndices.Count == 1)
            {
                updatenum = int.Parse(listView1.FocusedItem.SubItems[0].Text);
                AddorUpdate f3 = new AddorUpdate(this, "수정");
                f3.Show();
            }
            else
                MessageBox.Show("경고", "한개의 일정을 선택해주세요.");
        }
        // 삭제 버튼
        private void Delete_Calender(object sender, EventArgs e)
        {
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("DELETE FROM cldr WHERE snum = '" + listView1.FocusedItem.SubItems[0].Text + "'", sqlconn);
            sqlcmd.Connection = sqlconn;
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            refr();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            string date = listView1.FocusedItem.SubItems[1].Text;
            string title = listView1.FocusedItem.SubItems[2].Text;
            string tag = listView1.FocusedItem.SubItems[3].Text;
            string contents = listView1.FocusedItem.SubItems[4].Text;
            textBox3.Text = date;
            textBox6.Text = tag;
            textBox2.Text = title;
            textBox1.Text = contents;
        }

        private void Calender_FormClosing(object sender, FormClosingEventArgs e)
        {
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("UPDATE savevalue SET value1 = '" + savenum + "' WHERE value2 = '" + n + "'", sqlconn);
            sqlcmd.Connection = sqlconn;
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
        }

        void refr()
        {
            listView1.Items.Clear();
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("SELECT * FROM cldr;", sqlconn);
            MySqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read() == true)
            {
                string date = sqldr[1].ToString();
                if (date != sqldr[2].ToString())
                    date += " ~ " + sqldr[2].ToString();
                lvwitem = new ListViewItem(sqldr[0].ToString());
                lvwitem.SubItems.Add(date);
                lvwitem.SubItems.Add(sqldr[3].ToString());
                lvwitem.SubItems.Add(sqldr[4].ToString());
                lvwitem.SubItems.Add(sqldr[5].ToString());
                listView1.Items.Add(lvwitem);
            }
            sqlconn.Close();
        }
    }
}
