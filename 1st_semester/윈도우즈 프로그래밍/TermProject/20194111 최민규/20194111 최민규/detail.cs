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
    public partial class detail : Form
    {
        string title = ""; //영화 제목
        string director = ""; //감독
        string actor = ""; //배우
        string genre = ""; //장르
        string basic = ""; //기본정보
        string date = ""; //개봉일
        string rate = ""; //예매율
        string story = ""; //줄거리

        string phonNum = ""; //사용자 전화번호
        bool isLoged = false; //로그인 여부
        public detail()
        {
            InitializeComponent();
        }

        public detail(string t, bool log, string pn)
        {
            InitializeComponent();
            title = t;
            isLoged = log;
            phonNum = pn;
            groupBox1.Text = title;
            initForm(title);
        }

        private void initForm(string title)
        {
            if (title == "범죄도시3")
            {
                pictureBox2.Load(@"범죄도시3.jpg");
                string path = @"범죄도시3.txt";
                string[] textValue = System.IO.File.ReadAllLines(path);
                if (textValue.Length > 0)
                {
                    director = textValue[0];
                    actor = textValue[1];
                    genre = textValue[2];
                    basic = textValue[3];
                    date = textValue[4];
                    rate = textValue[5];
                    for (int i = 6; i < textValue.Length; i ++)
                    {
                        story += textValue[i] + "\n";
                    }
                }
            }

            else if (title == "가디언즈 오브 갤럭시 : VOLUME 3")
            {
                pictureBox2.Load(@"가오갤.jpg");
                string path = @"가오갤3.txt";
                string[] textValue = System.IO.File.ReadAllLines(path);
                if (textValue.Length > 0)
                {
                    director = textValue[0];
                    actor = textValue[1];
                    genre = textValue[2];
                    basic = textValue[3];
                    date = textValue[4];
                    rate = textValue[5];
                    for (int i = 6; i < textValue.Length; i++)
                    {
                        story += textValue[i] + "\n";
                    }
                }
            }

            else if (title == "인어공주")
            {
                pictureBox2.Load(@"인어공주.jpg");
                string path = @"인어공주.txt";
                string[] textValue = System.IO.File.ReadAllLines(path);
                if (textValue.Length > 0)
                {
                    director = textValue[0];
                    actor = textValue[1];
                    genre = textValue[2];
                    basic = textValue[3];
                    date = textValue[4];
                    rate = textValue[5];
                    for (int i = 6; i < textValue.Length; i++)
                    {
                        story += textValue[i] + "\n";
                    }
                }
            }

            else if (title == "분노의 질주: 라이드 오어 다이")
            {
                pictureBox2.Load(@"분노의질주.jpg");
                string path = @"분노의질주.txt";
                string[] textValue = System.IO.File.ReadAllLines(path);
                if (textValue.Length > 0)
                {
                    director = textValue[0];
                    actor = textValue[1];
                    genre = textValue[2];
                    basic = textValue[3];
                    date = textValue[4];
                    rate = textValue[5];
                    for (int i = 6; i < textValue.Length; i++)
                    {
                        story += textValue[i] + "\n";
                    }
                }
            }

            else if (title == "슈퍼 마리오 브라더스")
            {
                pictureBox2.Load(@"슈퍼마리오브라더스.jpg");
                string path = @"슈퍼마리오.txt";
                string[] textValue = System.IO.File.ReadAllLines(path);
                if (textValue.Length > 0)
                {
                    director = textValue[0];
                    actor = textValue[1];
                    genre = textValue[2];
                    basic = textValue[3];
                    date = textValue[4];
                    rate = textValue[5];
                    for (int i = 6; i < textValue.Length; i++)
                    {
                        story += textValue[i] + "\n";
                    }
                }
            }

            label8.Text = director;
            label9.Text = actor;
            label10.Text = genre;
            label11.Text = basic;
            label12.Text = date;
            label13.Text = rate;
            label14.Text = story;
            
        }

        private void Button2_Click(object sender, EventArgs e) //예매하기 버튼
        {
            if (isLoged)
            {
                ticket ticketForm = new ticket(title, phonNum);
                this.Close();
                ticketForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("로그인 후 이용하실 수 있습니다.", "Warning");
            }
        }
    }
}
