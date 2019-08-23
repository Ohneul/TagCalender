using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CS_Final_Project
{
    public partial class Tag_Calender : Form
    {
        MySqlConnection sqlconn = new MySqlConnection("SERVER = localhost; DATABASE = calender; UID = root; PWD = 8463;");

        public Tag_Calender()
        {
            InitializeComponent();
        }

        // 메인 패널 *************************************************************************************************
        private void Main_Panel_Click(object sender, EventArgs e)
        {
            Main_Panel.Visible = false;
            Login_Panel.Visible = true;
            ID_txt.Focus();
        }
        
        // 로그인 패널 ***********************************************************************************************
        private void ID_txt_Enter(object sender, EventArgs e)
        {
            if (ID_txt.Text == "아이디")
            {
                ID_txt.Text = "";
                ID_txt.ForeColor = Color.Black;
            }
        } // ID 텍박 들어갈 시

        private void ID_txt_Leave(object sender, EventArgs e)
        {
            if (ID_txt.Text == "")
            {
                ID_txt.ForeColor = Color.Silver;
                ID_txt.Text = "아이디";
            }
        } // ID 텍박 벗어날 시

        private void PW_txt_Enter(object sender, EventArgs e)
        {
            if (PW_txt.Text == "비밀번호")
            {
                PW_txt.Text = "";
                PW_txt.PasswordChar = '●';
                PW_txt.ForeColor = Color.Black;
            }
        } // PW 텍박 들어갈 시

        private void PW_txt_Leave(object sender, EventArgs e)
        {
            if (PW_txt.Text == "")
            {
                PW_txt.ForeColor = Color.Silver;
                PW_txt.PasswordChar = '\0';
                PW_txt.Text = "비밀번호";
            }
        } // PW 텍박 벗어날 시

        private void PW_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Login_btn.PerformClick();
        }

        private void Login_btn_Click(object sender, EventArgs e)
        {
            int colect = 0;
            string id = "";
            sqlconn.Open();
            MySqlCommand sqlcmd = new MySqlCommand("SELECT id, pw FROM member;", sqlconn);
            MySqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read() == true)
            {
                if (sqldr[0].ToString().Equals(ID_txt.Text) && sqldr[1].ToString().Equals(PW_txt.Text))
                {
                    colect = 1;
                    id = sqldr[0].ToString();
                    break;
                }
            }
            sqlconn.Close();
            if (colect == 1)
            {
                ID_txt.ForeColor = Color.Silver;
                ID_txt.Text = "아이디";
                PW_txt.ForeColor = Color.Silver;
                PW_txt.PasswordChar = '\0';
                PW_txt.Text = "비밀번호";
                Login_Panel.Visible = false;
                this.Hide();
                Calender Cld = new Calender(this, id);
                Cld.label10.Text = "[ " + id + " ]";
                Cld.Show();
            }
            else
            {
                MessageBox.Show("아이디 또는 비밀번호를 다시 확인하세요.", "경고");
                PW_txt.Text = "";
                PW_txt.Focus();
            }
        }

        private void FindID_btn_Click(object sender, EventArgs e)
        {
            ID_txt.ForeColor = Color.Silver;
            ID_txt.Text = "아이디";
            PW_txt.ForeColor = Color.Silver;
            PW_txt.Text = "비밀번호";
            Login_Panel.Visible = false;
            FindID_Paenl.Visible = true;
            FI_txt1.Focus();
        }

        private void FindPW_btn_Click(object sender, EventArgs e)
        {
            ID_txt.ForeColor = Color.Silver;
            ID_txt.Text = "아이디";
            PW_txt.ForeColor = Color.Silver;
            PW_txt.Text = "비밀번호";
            Login_Panel.Visible = false;
            FindPW_Paenl.Visible = true;
            FP_txt1.Focus();
        }

        private void Join_btn_Click(object sender, EventArgs e)
        {
            ID_txt.ForeColor = Color.Silver;
            ID_txt.Text = "아이디";
            PW_txt.ForeColor = Color.Silver;
            PW_txt.Text = "비밀번호";
            Login_Panel.Visible = false;
            Join_Panel.Visible = true;
            join_txt1.Focus();
        }

        // 회원가입 패널 *********************************************************************************************
        private void Cancel1_Click(object sender, EventArgs e)
        {
            join_txt1.Text = "";
            join_txt2.Text = "";
            join_txt3.Text = "";
            join_txt4.Text = "";
            join_txt5.Text = "";
            join_txt6.Text = "";
            join_txt7.Text = "";
            Join_Panel.Visible = false;
            Login_Panel.Visible = true;
        }

        private void join_txt3_KeyUp(object sender, KeyEventArgs e)
        {
            if (join_txt3.Text.Length == 3)
                join_txt4.Focus();
        }

        private void join_txt4_KeyUp(object sender, KeyEventArgs e)
        {
            if (join_txt4.Text.Length == 4)
                join_txt5.Focus();
        }

        private void join_txt5_KeyUp(object sender, KeyEventArgs e)
        {
            if (join_txt5.Text.Length == 4)
                join_txt6.Focus();
        }

        private void Join_Comple_Click(object sender, EventArgs e)
        {
            if(join_txt1.Text == "" || join_txt2.Text == "" || join_txt3.Text == "" || join_txt4.Text == "" || join_txt5.Text == "" || join_txt6.Text == "" || join_txt7.Text == "")
            {
                MessageBox.Show("입력받지 못한 정보가 있습니다.", "경고");
                return;
            }
            Regex chkvalue1 = new Regex(@"[0-9a-zA-Z]");
            Regex chkvalue2 = new Regex(@"[0-9]");
            char[] chkid = join_txt1.Text.ToCharArray();
            foreach(var cha in chkid)
            {
                if (!chkvalue1.IsMatch(cha.ToString()))
                {
                    MessageBox.Show("아이디는 영문자, 숫자만 입력해 주세요.", "경고");
                    join_txt1.SelectAll();
                    join_txt1.Focus();
                    return;
                }
            }
            string telnum = join_txt3.Text + join_txt4.Text + join_txt5.Text;
            char[] chktelnum = telnum.ToCharArray();
            foreach (var cha in chktelnum)
            {
                if (!chkvalue2.IsMatch(cha.ToString()))
                {
                    MessageBox.Show("폰번호는 숫자만 입력해 주세요.", "경고");
                    join_txt3.Text = "";
                    join_txt4.Text = "";
                    join_txt5.Text = "";
                    join_txt3.Focus();
                    return;
                }
            }
            int chk = 0;
            string tel = join_txt3.Text + "-" + join_txt4.Text + "-" + join_txt5.Text;
            string mail = join_txt6.Text + "@" + join_txt7.Text;
            sqlconn.Open();
            MySqlCommand sqlcmd1 = new MySqlCommand("SELECT id, phone, mail FROM member;", sqlconn);
            MySqlDataReader sqldr1 = sqlcmd1.ExecuteReader();
            while (sqldr1.Read() == true)
            {
                if (sqldr1[0].ToString().Equals(join_txt1.Text))
                {
                    chk = 1;
                    break;
                }
                else if (sqldr1[1].ToString().Equals(tel))
                {
                    chk = 2;
                    break;
                }
                else if (sqldr1[2].ToString().Equals(mail))
                {
                    chk = 3;
                    break;
                }
            }
            sqlconn.Close();
            if (chk == 1)
            {
                MessageBox.Show("등록된 아이디가 있습니다.", "경고");
                join_txt1.Text = "";
                join_txt1.Focus();
                return;
            }
            else if (chk == 2)
            {
                MessageBox.Show("등록된 번호가 있습니다.", "경고");
                join_txt3.Text = "";
                join_txt4.Text = "";
                join_txt5.Text = "";
                join_txt3.Focus();
                return;
            }
            else if (chk == 3)
            {
                MessageBox.Show("등록된 메일이 있습니다.", "경고");
                join_txt6.Text = "";
                join_txt7.Text = "";
                join_txt6.Focus();
                return;
            }
            sqlconn.Open();
            MySqlCommand sqlcmd2 = new MySqlCommand("INSERT INTO member VALUES('" + join_txt1.Text + "', '" + join_txt2.Text + "', '" + tel + "', '" + mail + "')", sqlconn);
            sqlcmd2.Connection = sqlconn;
            sqlcmd2.ExecuteNonQuery();
            sqlconn.Close();
            MessageBox.Show("회원가입이 완료되었습니다!", "완료");
            join_txt1.Text = "";
            join_txt2.Text = "";
            join_txt3.Text = "";
            join_txt4.Text = "";
            join_txt5.Text = "";
            join_txt6.Text = "";
            join_txt7.Text = "";
            Join_Panel.Visible = false;
            Login_Panel.Visible = true;
        }

        // 아이디찾기 패널 *******************************************************************************************
        private void FI_txt1_KeyUp(object sender, KeyEventArgs e)
        {
            if (FI_txt1.Text.Length == 3)
                FI_txt2.Focus();
        }

        private void FI_txt2_KeyUp(object sender, KeyEventArgs e)
        {
            if (FI_txt2.Text.Length == 4)
                FI_txt3.Focus();
        }

        private void FI_txt3_KeyUp(object sender, KeyEventArgs e)
        {
            if (FI_txt3.Text.Length == 4)
                FI_txt4.Focus();
        }

        private void Cancel2_Click(object sender, EventArgs e)
        {
            FI_txt1.Text = "";
            FI_txt2.Text = "";
            FI_txt3.Text = "";
            FI_txt4.Text = "";
            FI_txt5.Text = "";
            FindID_Paenl.Visible = false;
            Login_Panel.Visible = true;
        }

        private void FindID_Comple_Click(object sender, EventArgs e)
        {
            if (FI_txt1.Text == "" || FI_txt2.Text == "" || FI_txt3.Text == "" || FI_txt4.Text == "" || FI_txt5.Text == "")
            {
                MessageBox.Show("입력받지 못한 정보가 있습니다.", "경고");
                return;
            }
            string chk = "";
            string tel = FI_txt1.Text + "-" + FI_txt2.Text + "-" + FI_txt3.Text;
            string mail = FI_txt4.Text + "@" + FI_txt5.Text;
            sqlconn.Open();
            MySqlCommand sqlcmd1 = new MySqlCommand("SELECT id, phone, mail FROM member;", sqlconn);
            MySqlDataReader sqldr1 = sqlcmd1.ExecuteReader();
            while (sqldr1.Read() == true)
            {
                if (sqldr1[1].ToString().Equals(tel) && sqldr1[2].ToString().Equals(mail))
                {
                    chk = sqldr1[0].ToString();
                    break;
                }
            }
            sqlconn.Close();
            if (chk != "")
            {
                FI_txt1.Text = "";
                FI_txt2.Text = "";
                FI_txt3.Text = "";
                FI_txt4.Text = "";
                FI_txt5.Text = "";
                FindID_Paenl.Visible = false;
                Result_Panel.Visible = true;
                Result_label.Text = string.Format("찾으시는 아이디는 {0} 입니다.", chk);
            }
            else
            {
                MessageBox.Show("등록되지 않은 정보거나 맞는 정보가 없습니다.", "경고");
            }
        }

        // 비밀번호찾기 패널 *****************************************************************************************
        private void FP_txt2_KeyUp(object sender, KeyEventArgs e)
        {
            if (FP_txt2.Text.Length == 3)
                FP_txt3.Focus();
        }

        private void FP_txt3_KeyUp(object sender, KeyEventArgs e)
        {
            if (FP_txt3.Text.Length == 4)
                FP_txt4.Focus();
        }

        private void FP_txt4_KeyUp(object sender, KeyEventArgs e)
        {
            if (FP_txt4.Text.Length == 4)
                FP_txt5.Focus();
        }

        private void Cancel3_Click(object sender, EventArgs e)
        {
            FP_txt1.Text = "";
            FP_txt2.Text = "";
            FP_txt3.Text = "";
            FP_txt4.Text = "";
            FP_txt5.Text = "";
            FP_txt6.Text = "";
            FindPW_Paenl.Visible = false;
            Login_Panel.Visible = true;
        }

        private void FindPW_Comple_Click(object sender, EventArgs e)
        {
            if (FP_txt1.Text == "" || FP_txt2.Text == "" || FP_txt3.Text == "" || FP_txt4.Text == "" || FP_txt5.Text == "" || FP_txt6.Text == "")
            {
                MessageBox.Show("입력받지 못한 정보가 있습니다.", "경고");
                return;
            }
            string chk = "";
            string tel = FP_txt2.Text + "-" + FP_txt3.Text + "-" + FP_txt4.Text;
            string mail = FP_txt5.Text + "@" + FP_txt6.Text;
            sqlconn.Open();
            MySqlCommand sqlcmd1 = new MySqlCommand("SELECT * FROM member;", sqlconn);
            MySqlDataReader sqldr1 = sqlcmd1.ExecuteReader();
            while (sqldr1.Read() == true)
            {
                if (sqldr1[0].ToString().Equals(FP_txt1.Text) && sqldr1[2].ToString().Equals(tel) && sqldr1[3].ToString().Equals(mail))
                {
                    chk = sqldr1[1].ToString();
                    break;
                }
            }
            sqlconn.Close();
            if (chk != "")
            {
                FP_txt1.Text = "";
                FP_txt2.Text = "";
                FP_txt3.Text = "";
                FP_txt4.Text = "";
                FP_txt5.Text = "";
                FP_txt6.Text = "";
                FindPW_Paenl.Visible = false;
                Result_Panel.Visible = true;
                Result_label.Text = string.Format("찾으시는 비밀번호는 {0} 입니다.", chk);
            }
            else
            {
                MessageBox.Show("등록되지 않은 정보거나 맞는 정보가 없습니다.", "경고");
            }
        }

        // 결과 패널 *************************************************************************************************
        private void Cancel4_Click(object sender, EventArgs e)
        {
            Result_label.Text = "";
            Result_Panel.Visible = false;
            Login_Panel.Visible = true;
        }
    }
}
