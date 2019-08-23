using System;
using System.Collections;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CS_Final_Project
{
    public partial class Calender : Form
    {
        Tag_Calender TC;
        MySqlConnection sqlconn = new MySqlConnection("SERVER = localhost; DATABASE = calender; UID = root; PWD = 8463;");
        ListViewItem lvwitem;
        public int savenum, n, updatenum;
        public string id;
        int sortColumn = -1;
        int quitvalue = 0;
        public Calender(Tag_Calender T_C, string str)
        {
            InitializeComponent();
            TC = T_C;
            id = str;
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
            refr();
        }
        // 검색
        private void Search_Calender(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel4.Visible = true;
            label12.Text = " 검색";
            checkedListBox1.Items.Clear();
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("SELECT tagvalue FROM tag WHERE id = '" + id + "';", sqlconn);
            MySqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read() == true)
                checkedListBox1.Items.Add(sqldr[0].ToString());
            sqlconn.Close();

        }
        // 새로 고침
        private void Cancel_Search(object sender, EventArgs e)
        {
            button8.Text = "1";
            refr();
        }
        // 추가 및 수정
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
                listView1.SelectedItems.Clear();
                AddorUpdate f3 = new AddorUpdate(this, "수정");
                f3.Show();
            }
            else
                MessageBox.Show("한개의 일정을 선택해주세요.", "경고");
        }
        // 삭제
        private void Delete_Calender(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 0)
            {
                if (MessageBox.Show("선택한 일정을 삭제하시겠습니까?", "경고", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    sqlconn.Open();
                    for (int i = 0; i < listView1.SelectedIndices.Count; i++)
                    {
                        MySqlCommand sqlcmd = new MySqlCommand("DELETE FROM cldr WHERE snum = '" + listView1.SelectedItems[i].SubItems[0].Text + "'", sqlconn);
                        sqlcmd.Connection = sqlconn;
                        sqlcmd.ExecuteNonQuery();
                    }
                    sqlconn.Close();
                    refr();
                }
            }
        }
        // 로그아웃
        private void Click_Logout(object sender, EventArgs e)
        {
            label10.Text = "";
            id = "";
            listView1.Clear();
            TC.Show();
            TC.Main_Panel.Visible = true;
            quitvalue = 1;
            this.Close();
        }
        // 정보 수정
        private void button7_Click(object sender, EventArgs e)
        {
            InfoUpdate IFUP = new InfoUpdate(this);
            IFUP.Show();
        }
        // 리스트뷰 클릭
        private void listView1_Click(object sender, EventArgs e)
        {
            if (!panel1.Visible)
            {
                panel4.Visible = false;
                panel1.Visible = true;
                label12.Text = " 일정 보기";
            }
            if (listView1.SelectedIndices.Count == 1)
            {
                textBox3.Text = listView1.FocusedItem.SubItems[1].Text;
                textBox6.Text = listView1.FocusedItem.SubItems[2].Text;
                textBox2.Text = listView1.FocusedItem.SubItems[3].Text;
                textBox1.Text = listView1.FocusedItem.SubItems[4].Text;
            }
        }
        // 검색 > 닫기
        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel1.Visible = true;
            label12.Text = " 일정 보기";
        }
        // 검색 > 일정검색
        private void button2_Click(object sender, EventArgs e)
        {
            label13.Text = " 일정 검색결과";
            listView1.Items.Clear();
            int[] searchvalue = new int[savenum + 1];
            int arrsearch = 0;
            sqlconn.Open();
            MySqlCommand sqlcm = new MySqlCommand("SELECT * FROM cldr WHERE id = '" + id + "';", sqlconn);
            MySqlDataReader sqld = sqlcm.ExecuteReader();
            while (sqld.Read())
            {
                if (checkBox1.Checked)
                {
                    if ((textBox8.Text == "" || sqld[4].ToString().Contains(textBox8.Text)) && (textBox9.Text == "" || sqld[6].ToString().Contains(textBox9.Text)))
                    {
                        if (checkedListBox1.CheckedItems.Count == 0)
                            searchvalue[arrsearch++] = int.Parse(sqld[0].ToString());
                        else
                        {
                            string[] arrtag = sqld[5].ToString().Split(',');
                            int chkequal = 0;
                            foreach (string tagstr in checkedListBox1.CheckedItems)
                            {
                                for (int i = 0; i < arrtag.Length; i++)
                                {
                                    if (arrtag[i].Trim() == tagstr)
                                    {
                                        chkequal++;
                                        break;
                                    }
                                }
                            }
                            if (chkequal >= checkedListBox1.CheckedItems.Count)
                                searchvalue[arrsearch++] = int.Parse(sqld[0].ToString());
                        }
                    }
                }
                else
                {
                    for (DateTime dt = DateTime.Parse(dateTimePicker1.Text); dt <= DateTime.Parse(dateTimePicker2.Text); dt = dt.AddDays(1))
                    {
                        if (sqld[2].ToString() == sqld[3].ToString() && dt == DateTime.Parse(sqld[2].ToString()) &&
                            (textBox8.Text == "" || sqld[4].ToString().Contains(textBox8.Text)) && (textBox9.Text == "" || sqld[6].ToString().Contains(textBox9.Text)))
                        {
                            if (checkedListBox1.CheckedItems.Count == 0)
                                searchvalue[arrsearch++] = int.Parse(sqld[0].ToString());
                            else
                            {
                                string[] arrtag = sqld[5].ToString().Split(',');
                                int chkequal = 0;
                                foreach (string tagstr in checkedListBox1.CheckedItems)
                                {
                                    for (int i = 0; i < arrtag.Length; i++)
                                    {
                                        if (arrtag[i].Trim() == tagstr)
                                        {
                                            chkequal++;
                                            break;
                                        }
                                    }
                                }
                                if (chkequal >= checkedListBox1.CheckedItems.Count)
                                    searchvalue[arrsearch++] = int.Parse(sqld[0].ToString());
                            }
                        }
                        else
                        {
                            for (DateTime dat = DateTime.Parse(sqld[2].ToString()); dat <= DateTime.Parse(sqld[3].ToString()); dat = dat.AddDays(1))
                            {
                                if (dt == dat && (textBox8.Text == "" || sqld[4].ToString().Contains(textBox8.Text)) && (textBox9.Text == "" || sqld[6].ToString().Contains(textBox9.Text)))
                                {
                                    if (checkedListBox1.CheckedItems.Count == 0)
                                    {
                                        int chk1 = 0;
                                        for (int k = 0; k < searchvalue.Length; k++)
                                        {
                                            if (searchvalue[k] != int.Parse(sqld[0].ToString()))
                                                chk1++;
                                        }
                                        if (chk1 == searchvalue.Length)
                                        {
                                            searchvalue[arrsearch++] = int.Parse(sqld[0].ToString());
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        string[] arrtag = sqld[5].ToString().Split(',');
                                        int chkequal = 0;
                                        foreach (string tagstr in checkedListBox1.CheckedItems)
                                        {
                                            for (int i = 0; i < arrtag.Length; i++)
                                            {
                                                if (arrtag[i].Trim() == tagstr)
                                                {
                                                    chkequal++;
                                                    break;
                                                }
                                            }
                                        }
                                        if (chkequal >= checkedListBox1.CheckedItems.Count)
                                        {
                                            int chk1 = 0;
                                            for (int k = 0; k < searchvalue.Length; k++)
                                            {
                                                if (searchvalue[k] != int.Parse(sqld[0].ToString()))
                                                    chk1++;
                                            }
                                            if (chk1 == searchvalue.Length)
                                            {
                                                searchvalue[arrsearch++] = int.Parse(sqld[0].ToString());
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            sqld.Close();
            for (int i = 0; i < arrsearch; i++)
            {
                MySqlCommand sqlcmd = new MySqlCommand("SELECT * FROM cldr WHERE id = '" + id + "' AND snum = '" + searchvalue[i] + "';", sqlconn);
                MySqlDataReader sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read() == true)
                {
                    string date = sqldr[2].ToString();
                    if (sqldr[3].ToString() != date)
                        date += " ~ " + sqldr[3].ToString();
                    lvwitem = new ListViewItem(sqldr[0].ToString());
                    lvwitem.SubItems.Add(date);
                    lvwitem.SubItems.Add(sqldr[4].ToString());
                    lvwitem.SubItems.Add(sqldr[5].ToString());
                    lvwitem.SubItems.Add(sqldr[6].ToString());
                    listView1.Items.Add(lvwitem);
                }
                sqldr.Close();
            }
            sqlconn.Close();
            listView1.Sorting = SortOrder.Ascending;
            listView1.Sort();
            this.listView1.ListViewItemSorter = new MyListViewComparer(1, listView1.Sorting);
        }
        // 검색 > 초기화
        private void button1_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            checkBox1.Checked = false;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            textBox8.Text = "";
            textBox9.Text = "";
        }
        // 달력 날짜 선택
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            label13.Text = " 해당 날짜 일정";
            string dte1 = monthCalendar1.SelectionStart.ToShortDateString();
            string dte2 = monthCalendar1.SelectionEnd.ToShortDateString();
            int[] arrdatenum = new int[savenum + 1];
            int arrsort = 0;
            listView1.Items.Clear();
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("SELECT * FROM cldr WHERE id = '" + id + "';", sqlconn);
            MySqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read() == true)
            {
                for (DateTime dt = DateTime.Parse(dte1); dt <= DateTime.Parse(dte2); dt = dt.AddDays(1))
                {
                    if (sqldr[2].ToString() == sqldr[3].ToString() && dt == DateTime.Parse(sqldr[2].ToString()))
                        arrdatenum[arrsort++] = int.Parse(sqldr[0].ToString());
                    else
                    {
                        for (DateTime dat = DateTime.Parse(sqldr[2].ToString()); dat <= DateTime.Parse(sqldr[3].ToString()); dat = dat.AddDays(1))
                        {
                            if (dt == dat)
                            {
                                int chk1 = 0;
                                for (int k = 0; k < arrdatenum.Length; k++)
                                {
                                    if (arrdatenum[k] != int.Parse(sqldr[0].ToString()))
                                        chk1++;
                                }
                                if (chk1 == arrdatenum.Length)
                                {
                                    arrdatenum[arrsort++] = int.Parse(sqldr[0].ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            sqldr.Close();
            for (int i = 0; i < arrsort; i++)
            {
                MySqlCommand sqlcmd1 = new MySqlCommand("SELECT * FROM cldr WHERE id = '" + id + "' AND snum = '" + arrdatenum[i] + "';", sqlconn);
                MySqlDataReader sqldr1 = sqlcmd1.ExecuteReader();
                while (sqldr1.Read() == true)
                {
                    string date = sqldr1[2].ToString();
                    if (sqldr1[3].ToString() != date)
                        date += " ~ " + sqldr1[3].ToString();
                    lvwitem = new ListViewItem(sqldr1[0].ToString());
                    lvwitem.SubItems.Add(date);
                    lvwitem.SubItems.Add(sqldr1[4].ToString());
                    lvwitem.SubItems.Add(sqldr1[5].ToString());
                    lvwitem.SubItems.Add(sqldr1[6].ToString());
                    listView1.Items.Add(lvwitem);
                    break;
                }
                sqldr1.Close();
            }
            sqlconn.Close();
            listView1.Sorting = SortOrder.Ascending;
            listView1.Sort();
            this.listView1.ListViewItemSorter = new MyListViewComparer(1, listView1.Sorting);
        }

        // 종료
        private void Calender_FormClosing(object sender, FormClosingEventArgs e)
        {
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("UPDATE savevalue SET value1 = '" + savenum + "' WHERE value2 = '" + n + "'", sqlconn);
            sqlcmd.Connection = sqlconn;
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            if (quitvalue == 0)
                TC.Close();
        }
        // 리스트뷰 초기화
        public void refr()
        {
            label13.Text = " 다가오는 일정";
            listView1.Items.Clear();
            int[] arrdatenum = new int[savenum + 2];
            int arrsort = 0;
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("SELECT * FROM cldr WHERE id = '" + id + "' AND date1 >= '" + DateTime.Now.ToShortDateString() + "';", sqlconn);
            MySqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read() == true)
            {
                int chk1 = 0;
                for (int k = 0; k < arrdatenum.Length; k++)
                {
                    if (arrdatenum[k] != int.Parse(sqldr[0].ToString()))
                        chk1++;
                }
                if (chk1 == arrdatenum.Length)
                    arrdatenum[arrsort++] = int.Parse(sqldr[0].ToString());
            }
            sqldr.Close();
            MySqlCommand sqlcmd1 = new MySqlCommand("SELECT * FROM cldr WHERE id = '" + id + "';", sqlconn);
            MySqlDataReader sqldr1 = sqlcmd1.ExecuteReader();
            while (sqldr1.Read() == true)
            {
                for (int i = 0; i <= arrsort - 1; i++)
                {
                    if (arrdatenum[i] == int.Parse(sqldr1[0].ToString()))
                    {
                        string date = sqldr1[2].ToString();
                        if (sqldr1[3].ToString() != date)
                            date += " ~ " + sqldr1[3].ToString();
                        lvwitem = new ListViewItem(sqldr1[0].ToString());
                        lvwitem.SubItems.Add(date);
                        lvwitem.SubItems.Add(sqldr1[4].ToString());
                        lvwitem.SubItems.Add(sqldr1[5].ToString());
                        lvwitem.SubItems.Add(sqldr1[6].ToString());
                        listView1.Items.Add(lvwitem);
                        break;
                    }
                }
            }
            sqlconn.Close();
            listView1.Sorting = SortOrder.Ascending;
            listView1.Sort();
            this.listView1.ListViewItemSorter = new MyListViewComparer(1, listView1.Sorting);
        }
        // 리스트뷰 정렬
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn) {
                sortColumn = e.Column;
                listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listView1.Sorting == SortOrder.Ascending)
                {
                    listView1.Sorting = SortOrder.Descending;
                }
                else
                {
                    listView1.Sorting = SortOrder.Ascending;
                }
            }
            listView1.Sort();
            this.listView1.ListViewItemSorter = new MyListViewComparer(e.Column, listView1.Sorting);
        }

        class MyListViewComparer : IComparer
        {
            private int col; private SortOrder order;
            public MyListViewComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public MyListViewComparer(int column, SortOrder order)
            {
                col = column;
                this.order = order;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                if (order == SortOrder.Descending)
                    returnVal *= -1; return returnVal;
            }
        }
    }
}
