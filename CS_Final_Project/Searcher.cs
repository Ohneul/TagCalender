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
    public partial class Searcher : Form
    {
        MySqlConnection sqlconn = new MySqlConnection("SERVER = localhost; DATABASE = calender; UID = root; PWD = 8463;");
        ListViewItem lvwitem;
        Calender cld;
        public Searcher()
        {
            InitializeComponent();
        }

        private void Option_Default(object sender, EventArgs e)
        {

        }

        private void Search_Calender(object sender, EventArgs e)
        {

        }
        
        private void AddUpdate_Calender(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
            {
                AddorUpdate f3 = new AddorUpdate(cld, "추가");
                f3.Show();
            }
            else if (listView1.SelectedIndices.Count == 1)
            {
                AddorUpdate f3 = new AddorUpdate(cld, "수정");
                f3.Show();
            }
            else
                MessageBox.Show("경고", "한개의 일정을 선택해주세요.");
        }

        private void Delete_Calender(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
