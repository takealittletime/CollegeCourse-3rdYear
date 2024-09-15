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
    public partial class join : Form
    {
        public join()
        {
            InitializeComponent();
        }

        string id; //입력받은 아이디
        string pw; //입력받은 패스워드
        string pwchk; //패스워드 확인
        string name; //이름
        string pn; //전화번호
        bool idchk; //아이디 중복 체크 여부

        void initpwBox()
        {
            textBox2.Text = "";
            textBox3.Text = "";
        }

        bool checkBox()
        {
            bool tmp = true;
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == ""
                || comboBox1.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                tmp = false;
            }
            return tmp;
        }

        private void Button1_Click(object sender, EventArgs e) //가입 버튼
        {
            id = textBox1.Text;
            pw = textBox2.Text;
            pwchk = textBox3.Text;
            name = textBox4.Text;
            pn = comboBox1.Text + textBox5.Text + textBox6.Text;

            if (!checkBox()) //빈 칸이 있는 경우
            {
                MessageBox.Show("모든 칸을 채워주세요.", "Join Failed");
            }

            else if (pw != pwchk) //패스워드 체크
            {
                MessageBox.Show("입력하신 두 패스워드가 다릅니다.", "Password Check");
                initpwBox();
            }

            else if (idchk == false)
            {
                MessageBox.Show("아이디 중복체크를 해주세요.", "id-check");
            }

            else //조건 모두 충족 시
            {
                string path = @"users.txt";
                string txtValue = id + "\n" + pw + "\n" + name + "\n" + pn + "\n";
                System.IO.File.AppendAllText(path, txtValue, Encoding.UTF8);
                MessageBox.Show(name + "님, 가입해주셔서 감사합니다.", "Join Success");
                this.Close();
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e) //패스워드 체크
        {
            if (textBox2.Text != textBox3.Text)
            {
                label8.Text = "* 위에 입력하신 패스워드와 다릅니다.";
            }
            else
            {
                label8.Text = "* 입력하신 두 패스워드가 일치합니다.";
            }
        }

        private void Button2_Click(object sender, EventArgs e) //중복 체크 버튼
        {
            string path = @"users.txt";
            string[] textValue = System.IO.File.ReadAllLines(path);
            bool tmp = true;

            if (textValue.Length > 0)
            {
                for (int i = 0; i < textValue.Length; i += 4)
                {

                    if (textBox1.Text == textValue[i]) //이미 존재하는 아이디일 경우
                    {
                        idchk = false;
                        tmp = false;
                        MessageBox.Show("이미 사용중인 아이디입니다!","id-check");
                        textBox1.Text = "";
                        break;
                    }
                }
            }

            if (tmp) //사용 가능한 아이디인 경우
            {
                idchk = true;
                MessageBox.Show("사용 가능한 아이디입니다.", "id-check");
            }

        }
    }
}
