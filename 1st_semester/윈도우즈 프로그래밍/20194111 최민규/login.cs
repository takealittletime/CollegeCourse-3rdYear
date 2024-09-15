using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20194111_최민규
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        string id; //입력 받은 아이디
        string pw; // 입력 받은 패스워드
        string uname; //로그인 시 사용자 이름
        bool loged = false; //로그인 성공 여부
        string pNum = ""; //사용자 전화번호
        bool master = false; //관리자 여부

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //회원가입 버튼
        {
            join joinForm = new join();
            joinForm.Show();
        }

        private void Button1_Click(object sender, EventArgs e) //로그인 버튼
        {
            id = textBox1.Text;
            pw = textBox2.Text;

            string path = @"users.txt";
            string[] textValue = System.IO.File.ReadAllLines(path);
            if (textValue.Length > 0)
            {
                for (int i = 0; i < textValue.Length; i+=4)
                {
                    
                    if (id == textValue[i] && pw == textValue[i + 1])
                    {
                        if (id == "abcde" && pw == "12345")
                        {
                            master = true;
                        }
                        loged = true;
                        uname = textValue[i + 2];
                        pNum = textValue[i + 3];
                        break;
                    }
                }
            }

            if (loged)
            {
                MessageBox.Show(uname + "님, 환영합니다.", "Log in");
                main mainForm = new main(uname,pNum,master);
                mainForm.Show();
                this.Close();
            }

            else
            {
                MessageBox.Show("존재하지 않는 회원정보 입니다.", "Log in");
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e) //pw 박스
        {

        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e) //pw 박스에서 엔터 누르면 로그인 버튼
        {
            if (e.KeyCode == Keys.Enter)
                this.Button1_Click(sender,e);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e) //id 박스에서 엔터 누르면 로그인 버튼
        {
            if (e.KeyCode == Keys.Enter)
                this.Button1_Click(sender, e);
        }

        //아래 코드는 main창을 숨김으로 인해 프로그램이 종료되지 않는 문제를 해결하기 위해 작성하였음.
        private void Login_FormClosing(object sender, FormClosingEventArgs e) //로그인 폼 닫으면 프로그램 종료.
        {
            if (!loged)
            {
                Application.Exit();
            }
        }
    }
}