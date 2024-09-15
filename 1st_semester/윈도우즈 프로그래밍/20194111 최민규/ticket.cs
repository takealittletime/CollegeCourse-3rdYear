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
    public partial class ticket : Form
    {

        string title; //영화 제목
        string theather; //영화관
        string dateTime; //날짜, 시간
        string phonNum; //사용자 전화번호
        bool movieSel = false; //영화 선택 여부
        int tNum = 0; //상영관 번호

        public ticket()
        {
            InitializeComponent();
            groupBox3.Visible = false;
            //날짜는 오늘로부터 일주일만 설정 가능
            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker1.MaxDate = DateTime.Today.AddDays(7);
        }
        public ticket(string pn)
        {
            InitializeComponent();
            phonNum = pn;
            groupBox3.Visible = false;
            //날짜는 오늘로부터 일주일만 설정 가능
            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker1.MaxDate = DateTime.Today.AddDays(7);
        }


        public ticket(string mName, string pn) //영화 제목 받아 초기화
        {
            InitializeComponent();
            phonNum = pn;
            groupBox3.Visible = false;
            //날짜는 오늘로부터 일주일만 설정 가능
            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker1.MaxDate = DateTime.Today.AddDays(7);

            if (mName == "범죄도시3")
            {
                listBox1.Items.RemoveAt(1);
                listBox1.Items.RemoveAt(1);
                listBox1.Items.RemoveAt(1);
                listBox1.Items.RemoveAt(1);
                listBox1.SelectedIndex = 0;
            }

            else if (mName == "가디언즈 오브 갤럭시 : VOLUME 3")
            {
                listBox1.Items.RemoveAt(2);
                listBox1.Items.RemoveAt(2);
                listBox1.Items.RemoveAt(2);
                listBox1.Items.RemoveAt(0);
                listBox1.SelectedIndex = 0;
            }

            else if (mName == "분노의 질주: 라이드 오어 다이")
            {
                listBox1.Items.RemoveAt(3);
                listBox1.Items.RemoveAt(3);
                listBox1.Items.RemoveAt(1);
                listBox1.Items.RemoveAt(0);
                listBox1.SelectedIndex = 0;
            }

            else if (mName == "인어공주")
            {
                listBox1.Items.RemoveAt(0);
                listBox1.Items.RemoveAt(0);
                listBox1.Items.RemoveAt(0);
                listBox1.Items.RemoveAt(1);
                listBox1.SelectedIndex = 0;
            }

            else if (mName == "슈퍼 마리오 브라더스")
            {
                listBox1.Items.RemoveAt(0);
                listBox1.Items.RemoveAt(0);
                listBox1.Items.RemoveAt(0);
                listBox1.Items.RemoveAt(0);
                listBox1.SelectedIndex = 0;
            }

        }

        private string calcDateTime()
        {
            string tmp = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Monday)
                tmp += " (월) ";
            else if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Tuesday)
                tmp += " (화) ";
            else if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Wednesday)
                tmp += " (수) ";
            else if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Thursday)
                tmp += " (목) ";
            else if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Friday)
                tmp += " (금) ";
            else if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Saturday)
                tmp += " (토) ";
            else if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Sunday)
                tmp += " (일) ";
            return tmp;
        }


        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            movieSel = true;
            title = listBox1.Text;
            label5.Text = title;

            if (title == "범죄도시3")
            {
                pictureBox2.Load(@"범죄도시3.jpg");
                pictureBox3.Load(@"15.jpg");
            }

            else if (title == "인어공주")
            {
                pictureBox2.Load(@"인어공주.jpg");
                pictureBox3.Load(@"all.jpg");
            }

            else if (title == "가디언즈 오브 갤럭시 : VOLUME3")
            {
                pictureBox2.Load(@"가오갤.jpg");
                pictureBox3.Load(@"12.jpg");
            }

            else if (title == "분노의 질주: 라이드 오어 다이")
            {
                pictureBox2.Load(@"분노의질주.jpg");
                pictureBox3.Load(@"15.jpg");
            }

            else if (title == "슈퍼 마리오 브라더스")
            {
                pictureBox2.Load(@"슈퍼마리오브라더스.jpg");
                pictureBox3.Load(@"all.jpg");
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e) //영화관 선택
        {
            theather = e.Node.Name;
            if (!string.IsNullOrEmpty(theather))
            {
                //MessageBox.Show("영화관 선택 : " + theather);
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e) //날짜 선택
        {
            groupBox3.Visible = true;
        }

        private void Button1_Click(object sender, EventArgs e) //09:00 버튼
        {
            tNum = 6;

            if (theather == "서울" || theather == "경기/인천" || theather == "충청/대전")
            {
                MessageBox.Show("영화관을 선택해주세요.", "Warning");
            }

            else
            {
                if (movieSel)
                {
                    dateTime = calcDateTime();
                    dateTime += button1.Text;
                    seats seatsForm = new seats(title,dateTime,theather,tNum,phonNum);
                    this.Close();
                    seatsForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("영화를 선택해주세요.", "Warning");
                }
            }

        }

        private void Button2_Click(object sender, EventArgs e) //15:45 버튼
        {
            tNum = 6;

            if (theather == "서울" || theather == "경기/인천" || theather == "충청/대전")
            {
                MessageBox.Show("영화관을 선택해주세요.", "Warning");
            }

            else
            {
                if (movieSel)
                {
                    dateTime = calcDateTime();
                    dateTime += button2.Text;
                    seats seatsForm = new seats(title, dateTime, theather, tNum,phonNum);
                    this.Close();
                    seatsForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("영화를 선택해주세요.", "Warning");
                }
            }

        }

        private void Button3_Click(object sender, EventArgs e) //22:30 버튼
        {
            tNum = 6;
            if (theather == "서울" || theather == "경기/인천" || theather == "충청/대전")
            {
                MessageBox.Show("영화관을 선택해주세요.", "Warning");
            }

            else
            {
                if (movieSel)
                {
                    dateTime = calcDateTime();
                    dateTime += button3.Text;
                    seats seatsForm = new seats(title, dateTime, theather, tNum, phonNum);
                    this.Close();
                    seatsForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("영화를 선택해주세요.", "Warning");
                }
            }

        }

        private void Button4_Click(object sender, EventArgs e) //12:30 버튼
        {
           
            tNum = 7;
            if (theather == "서울" || theather == "경기/인천" || theather == "충청/대전")
            {
                MessageBox.Show("영화관을 선택해주세요.", "Warning");
            }

            else
            {
                if (movieSel)
                {
                    dateTime = calcDateTime();
                    dateTime += button4.Text;
                    seats seatsForm = new seats(title, dateTime, theather, tNum, phonNum);
                    this.Close();
                    seatsForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("영화를 선택해주세요.", "Warning");
                }
            }

        }

        private void Button5_Click(object sender, EventArgs e) //17:30 버튼
        {
            
            tNum = 7;
            if (theather == "서울" || theather == "경기/인천" || theather == "충청/대전")
            {
                MessageBox.Show("영화관을 선택해주세요.", "Warning");
            }

            else
            {
                if (movieSel)
                {
                    dateTime = calcDateTime();
                    dateTime += button5.Text;
                    seats seatsForm = new seats(title, dateTime, theather, tNum, phonNum);
                    this.Close();
                    seatsForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("영화를 선택해주세요.", "Warning");
                }
            }
        }
    }
}
