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
    public partial class seats : Form
    {
        int max = 6; //인원은 최대 6명까지.

        int adult = 0;
        int teen = 0;
        int child = 0;
        int total = 0;
        int clk = 0;
        int bill = 0;

        string title; //영화 제목
        string dateTime; //상영 일시
        string theather; //상영관
        string tNum; //관 번호
        string phonNum; //사용자 전화번호

        string[] seatNum = new string[6]; //좌석을 할당할 배열
        int seatIdx = 0; //seatNum 변수에 사용할 인덱스
        string seat = ""; //마지막에 좌석 정보를 문자열로 할당

        public seats()
        {
            InitializeComponent();
        }
        public seats(string t, string d, string th, int n, string pn)
        {
            InitializeComponent();
            title = t;
            dateTime = d;
            theather = th;
            phonNum = pn;
            tNum = n.ToString() + "관";

            groupBox4.Text = title;
            label9.Text = dateTime;
            label10.Text = theather + " / " + tNum;

            if (title == "범죄도시3")
            {
                poster.Load(@"범죄도시3.jpg");
            }

            else if (title == "인어공주")
            {
                poster.Load(@"인어공주.jpg");
            }

            else if (title == "가디언즈 오브 갤럭시 : VOLUME3")
            {
                poster.Load(@"가오갤.jpg");
            }

            else if (title == "분노의 질주: 라이드 오어 다이")
            {
                poster.Load(@"분노의질주.jpg");
            }

            else if (title == "슈퍼 마리오 브라더스")
            {
                poster.Load(@"슈퍼마리오브라더스.jpg");
            }
        }

        private string seatString()
        {
            string tmp = "";

            for (int i = 0; i < seatIdx; i++)
            {
                tmp += seatNum[i] + " ";
            }

            return tmp;
        }

        private void sweepNull() //Array.Sort()에서 
        {
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max-1; j++)
                {
                    if (seatNum[j] == null)
                    {
                        string tmp = seatNum[j];
                        seatNum[j] = seatNum[j + 1];
                        seatNum[j + 1] = tmp;
                    }
                }
            }
        }

        private void addSeat(string n) //좌석 추가 함수
        {
            seatNum[seatIdx] = n;
            seatIdx++;
            Array.Sort(seatNum);
            sweepNull();
            seat = seatString();
            label2.Text = "* 현재 선택 좌석: " + seat;
        }


        private void delSeat(string n) //좌석 삭제 함수
        {
            for (int i = 0; i < seatIdx; i++)
            {
                if (seatNum[i] == n)
                {
                    for (int j = i; j < seatIdx - 1; j++)
                    {
                        seatNum[j] = seatNum[j + 1];
                    }
                    seatIdx--;
                    break;
                }
            }
            Array.Sort(seatNum);
            sweepNull();
            seat = seatString();
            label2.Text = "* 현재 선택 좌석: " + seat;
        }
        private int calc_bill() //총 합계 계산 함수
        {
            bill = 12000 * adult + 7000 * teen + 5000 * child;
            return bill;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //결제하기 버튼 (디자인 상 링크 레이블로 구현)
        {
            if (total > clk)
                MessageBox.Show("좌석을 인원수만큼 모두 선택해주세요.", "Warning");
            else
            {
                MessageBox.Show("신규 오픈 기념 할인 쿠폰 이벤트 진행중!\r\n10% 할인 쿠폰을 확인하세요.\r\n*영화 예매/스낵 구매에 이용 가능!", "Event");
                payticket payForm = new payticket(bill,title, dateTime, theather, tNum,seat,phonNum);
                payForm.ShowDialog();
                this.Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e) //일반 +
        {
            if (total < max)
            {
                adult++;
                total++;
                label6.Text = adult.ToString();
                bill = calc_bill();
                label12.Text = "총 합계 " + bill.ToString() + "원";
            }
        }

        private void Button2_Click(object sender, EventArgs e) //일반 -
        {
            if (total <= clk)
            {
                MessageBox.Show("선택한 좌석을 먼저 해제해주세요.", "Warning");
            }

            else if (total >= 1 && adult >=1)
            {
                adult--;
                total--;
                label6.Text = adult.ToString();
                bill = calc_bill();
                label12.Text = "총 합계 " + bill.ToString() + "원";
            }
        }

        private void Button3_Click(object sender, EventArgs e) //청소년 -
        {
            if (total <= clk)
            {
                MessageBox.Show("선택한 좌석을 먼저 해제해주세요.", "Warning");
            }

            else if (total >= 1 && teen >= 1 && total > clk)
            {
                teen--;
                total--;
                label7.Text = teen.ToString();
                bill = calc_bill();
                label12.Text = "총 합계 " + bill.ToString() + "원";
            }
        }

        private void Button4_Click(object sender, EventArgs e) //청소년 +
        {
            if (total < max)
            {
                teen++;
                total++;
                label7.Text = teen.ToString();
                bill = calc_bill();
                label12.Text = "총 합계 " + bill.ToString() + "원";
            }
        }

        private void Button5_Click(object sender, EventArgs e) //어린이 -
        {
            if (total <= clk)
            {
                MessageBox.Show("선택한 좌석을 먼저 해제해주세요.", "Warning");
            }

            else if (total >= 1 && child >= 1)
            {
                child--;
                total--;
                label8.Text = child.ToString();
                bill = calc_bill();
                label12.Text = "총 합계 " + bill.ToString() + "원";
            }
        }

        private void Button6_Click(object sender, EventArgs e) //어린이 +
        {
            if (total < max)
            {
                child++;
                total++;
                label8.Text = child.ToString();
                bill = calc_bill();
                label12.Text = "총 합계 "+bill.ToString()+"원";
            }
        }


        //아래부터 좌석 버튼
        private void D12_Click(object sender, EventArgs e)
        {
            if (d12.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d12.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d12.BackColor = Color.Red;
                        clk++;
                        addSeat("d12");
                    }
                }

                else
                {
                    d12.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d12");
                }
            }
        }

        private void D11_Click(object sender, EventArgs e)
        {
            if (d11.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d11.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d11.BackColor = Color.Red;
                        clk++;
                        addSeat("d11");
                    }
                }

                else
                {
                    d11.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d11");
                }
            }
        }

        private void D10_Click(object sender, EventArgs e)
        {
            if (d10.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d10.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d10.BackColor = Color.Red;
                        clk++;
                        addSeat("d10");
                    }
                }

                else
                {
                    d10.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d10");
                }
            }
        }

        private void D9_Click(object sender, EventArgs e)
        {
            if (d9.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d9.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d9.BackColor = Color.Red;
                        clk++;
                        addSeat("d9");
                    }
                }

                else
                {
                    d9.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d9");
                }
            }
        }

        private void D8_Click(object sender, EventArgs e)
        {
            if (d8.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d8.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d8.BackColor = Color.Red;
                        clk++;
                        addSeat("d8");
                    }
                }

                else
                {
                    d8.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d8");
                }
            }
        }

        private void D7_Click(object sender, EventArgs e)
        {
            if (d7.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d7.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d7.BackColor = Color.Red;
                        clk++;
                        addSeat("d7");
                    }
                }

                else
                {
                    d7.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d7");
                }
            }
        }

        private void D6_Click(object sender, EventArgs e)
        {
            if (d6.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d6.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d6.BackColor = Color.Red;
                        clk++;
                        addSeat("d6");
                    }
                }

                else
                {
                    d6.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d6");
                }
            }
        }

        private void D5_Click(object sender, EventArgs e)
        {
            if (d5.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d5.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d5.BackColor = Color.Red;
                        clk++;
                        addSeat("d5");
                    }
                }

                else
                {
                    d5.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d5");
                }
            }
        }

        private void D4_Click(object sender, EventArgs e)
        {
            if (d4.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d4.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d4.BackColor = Color.Red;
                        clk++;
                        addSeat("d4");
                    }
                }

                else
                {
                    d4.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d4");
                }
            }
        }

        private void D3_Click(object sender, EventArgs e)
        {
            if (d3.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d3.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d3.BackColor = Color.Red;
                        clk++;
                        addSeat("d3");
                    }
                }

                else
                {
                    d3.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d3");
                }
            }
        }

        private void D2_Click(object sender, EventArgs e)
        {
            if (d2.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d2.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d2.BackColor = Color.Red;
                        clk++;
                        addSeat("d2");
                    }
                }

                else
                {
                    d2.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d2");
                }
            }
        }

        private void D1_Click(object sender, EventArgs e)
        {
            if (d1.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d1.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d1.BackColor = Color.Red;
                        clk++;
                        addSeat("d1");
                    }
                }

                else
                {
                    d1.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d1");
                }
            }
        }

        private void C13_Click(object sender, EventArgs e)
        {
            if (c13.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c13.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c13.BackColor = Color.Red;
                        clk++;
                        addSeat("c13");
                    }
                }

                else
                {
                    c13.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c13");
                }
            }
        }

        private void C12_Click(object sender, EventArgs e)
        {
            if (c12.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c12.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c12.BackColor = Color.Red;
                        clk++;
                        addSeat("c12");
                    }
                }

                else
                {
                    c12.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c12");
                }
            }
        }

        private void C11_Click(object sender, EventArgs e)
        {
            if (c11.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c11.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c11.BackColor = Color.Red;
                        clk++;
                        addSeat("c11");
                    }
                }

                else
                {
                    c11.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c11");
                }
            }
        }

        private void C10_Click(object sender, EventArgs e)
        {
            if (c10.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c10.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c10.BackColor = Color.Red;
                        clk++;
                        addSeat("c10");
                    }
                }

                else
                {
                    c10.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c10");
                }
            }
        }

        private void C9_Click(object sender, EventArgs e)
        {
            if (c9.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c9.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c9.BackColor = Color.Red;
                        clk++;
                        addSeat("c9");
                    }
                }

                else
                {
                    c9.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c9");
                }
            }
        }

        private void C8_Click(object sender, EventArgs e)
        {
            if (c8.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c8.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c8.BackColor = Color.Red;
                        clk++;
                        addSeat("c8");
                    }
                }

                else
                {
                    c8.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c8");
                }
            }
        }

        private void C7_Click(object sender, EventArgs e)
        {
            if (c7.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c7.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c7.BackColor = Color.Red;
                        clk++;
                        addSeat("c7");
                    }
                }

                else
                {
                    c7.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c7");
                }
            }
        }

        private void C6_Click(object sender, EventArgs e)
        {
            if (c6.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c6.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c6.BackColor = Color.Red;
                        clk++;
                        addSeat("c6");
                    }
                }

                else
                {
                    c6.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c6");
                }
            }
        }

        private void C5_Click(object sender, EventArgs e)
        {
            if (c5.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c5.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c5.BackColor = Color.Red;
                        clk++;
                        addSeat("c5");
                    }
                }

                else
                {
                    c5.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c5");
                }
            }
        }

        private void C4_Click(object sender, EventArgs e)
        {
            if (c4.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c4.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c4.BackColor = Color.Red;
                        clk++;
                        addSeat("c4");
                    }
                }

                else
                {
                    c4.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c4");
                }
            }
        }

        private void C3_Click(object sender, EventArgs e)
        {
            if (c3.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c3.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c3.BackColor = Color.Red;
                        clk++;
                        addSeat("c3");
                    }
                }

                else
                {
                    c3.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c3");
                }
            }
        }

        private void C2_Click(object sender, EventArgs e)
        {
            if (c2.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c2.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c2.BackColor = Color.Red;
                        clk++;
                        addSeat("c2");
                    }
                }

                else
                {
                    c2.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c2");
                }
            }
        }

        private void C1_Click(object sender, EventArgs e)
        {
            if (c1.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (c1.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        c1.BackColor = Color.Red;
                        clk++;
                        addSeat("c1");
                    }
                }

                else
                {
                    c1.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("c1");
                }
            }
        }

        private void B13_Click(object sender, EventArgs e)
        {
            if (b13.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b13.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b13.BackColor = Color.Red;
                        clk++;
                        addSeat("b13");
                    }
                }

                else
                {
                    b13.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b13");
                }
            }
        }

        private void B12_Click(object sender, EventArgs e)
        {
            if (b12.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b12.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b12.BackColor = Color.Red;
                        clk++;
                        addSeat("b12");
                    }
                }

                else
                {
                    b12.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b12");
                }
            }
        }

        private void B11_Click(object sender, EventArgs e)
        {
            if (b11.BackColor != Color.DimGray)
            {
                if (b11.BackColor != Color.DimGray)
                {
                    if (total == 0)
                    {
                        MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                    }

                    else if (b11.BackColor == Color.Gainsboro)
                    {
                        if (clk < total)
                        {
                            b11.BackColor = Color.Red;
                            clk++;
                            addSeat("b11");
                        }
                    }

                    else
                    {
                        b11.BackColor = Color.Gainsboro;
                        clk--;
                        delSeat("b11");
                    }
                }
            }
        }

        private void B10_Click(object sender, EventArgs e)
        {
            if (b10.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b10.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b10.BackColor = Color.Red;
                        clk++;
                        addSeat("b10");
                    }
                }

                else
                {
                    b10.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b10");
                }
            }
        }

        private void B9_Click(object sender, EventArgs e)
        {
            if (b9.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b9.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b9.BackColor = Color.Red;
                        clk++;
                        addSeat("b9");
                    }
                }

                else
                {
                    b9.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b9");
                }
            }
        }

        private void B8_Click(object sender, EventArgs e)
        {
            if (b8.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b8.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b8.BackColor = Color.Red;
                        clk++;
                        addSeat("b8");
                    }
                }

                else
                {
                    b8.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b8");
                }
            }
        }

        private void B7_Click(object sender, EventArgs e)
        {
            if (b7.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b7.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b7.BackColor = Color.Red;
                        clk++;
                        addSeat("b7");
                    }
                }

                else
                {
                    b7.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b7");
                }
            }
        }

        private void B6_Click(object sender, EventArgs e)
        {
            if (b6.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b6.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b6.BackColor = Color.Red;
                        clk++;
                        addSeat("b6");
                    }
                }

                else
                {
                    b6.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b6");
                }
            }
        }

        private void B5_Click(object sender, EventArgs e)
        {
            if (b5.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b5.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b5.BackColor = Color.Red;
                        clk++;
                        addSeat("b5");
                    }
                }

                else
                {
                    b5.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b5");
                }
            }
        }

        private void B4_Click(object sender, EventArgs e)
        {
            if (b4.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b4.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b4.BackColor = Color.Red;
                        clk++;
                        addSeat("b4");
                    }
                }

                else
                {
                    b4.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b4");
                }
            }
        }

        private void B3_Click(object sender, EventArgs e)
        {
            if (b3.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b3.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b3.BackColor = Color.Red;
                        clk++;
                        addSeat("b3");
                    }
                }

                else
                {
                    b3.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b3");
                }
            }
        }

        private void B2_Click(object sender, EventArgs e)
        {
            if (b2.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b2.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b2.BackColor = Color.Red;
                        clk++;
                        addSeat("b2");
                    }
                }

                else
                {
                    b2.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b2");
                }
            }
        }

        private void B1_Click(object sender, EventArgs e)
        {
            if (b1.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (b1.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        b1.BackColor = Color.Red;
                        clk++;
                        addSeat("b1");
                    }
                }

                else
                {
                    b1.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("b1");
                }
            }
        }

        private void A13_Click(object sender, EventArgs e)
        {
            if (a13.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a13.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a13.BackColor = Color.Red;
                        clk++;
                        addSeat("a13");
                    }
                }

                else
                {
                    a13.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a13");
                }
            }
        }

        private void A12_Click(object sender, EventArgs e)
        {
            if (a12.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a12.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a12.BackColor = Color.Red;
                        clk++;
                        addSeat("a12");
                    }
                }

                else
                {
                    a12.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a12");
                }
            }
        }

        private void A11_Click(object sender, EventArgs e)
        {
            if (a11.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a11.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a11.BackColor = Color.Red;
                        clk++;
                        addSeat("a11");
                    }
                }

                else
                {
                    a11.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a11");
                }
            }
        }

        private void A10_Click(object sender, EventArgs e)
        {
            if (a10.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a10.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a10.BackColor = Color.Red;
                        clk++;
                        addSeat("a10");
                    }
                }

                else
                {
                    a10.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a10");
                }
            }
        }

        private void A9_Click(object sender, EventArgs e)
        {
            if (a9.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a9.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a9.BackColor = Color.Red;
                        clk++;
                        addSeat("a9");
                    }
                }

                else
                {
                    a9.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a9");
                }
            }
        }

        private void A8_Click(object sender, EventArgs e)
        {
            if (a8.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a8.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a8.BackColor = Color.Red;
                        clk++;
                        addSeat("a8");
                    }
                }

                else
                {
                    a8.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a8");
                }
            }
        }

        private void A7_Click(object sender, EventArgs e)
        {
            if (a7.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a7.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a7.BackColor = Color.Red;
                        clk++;
                        addSeat("a7");
                    }
                }

                else
                {
                    a7.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a7");
                }
            }
        }

        private void A6_Click(object sender, EventArgs e)
        {
            if (a6.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                if (a6.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a6.BackColor = Color.Red;
                        clk++;
                        addSeat("a6");
                    }
                }

                else
                {
                    a6.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a6");
                }
            }
        }

        private void A5_Click(object sender, EventArgs e)
        {
            if (a5.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a5.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a5.BackColor = Color.Red;
                        clk++;
                        addSeat("a5");
                    }
                }

                else
                {
                    a5.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a5");
                }
            }
        }

        private void A4_Click(object sender, EventArgs e)
        {
            if (a4.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a4.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a4.BackColor = Color.Red;
                        clk++;
                        addSeat("a4");
                    }
                }

                else
                {
                    a4.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a4");
                }
            }
        }

        private void A3_Click(object sender, EventArgs e)
        {
            if (a3.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a3.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a3.BackColor = Color.Red;
                        clk++;
                        addSeat("a3");
                    }
                }

                else
                {
                    a3.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a3");
                }
            }
        }

        private void A2_Click(object sender, EventArgs e)
        {
            if (a2.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a2.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a2.BackColor = Color.Red;
                        clk++;
                        addSeat("a2");
                    }
                }

                else
                {
                    a2.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a2");
                }
            }
        }

        private void D13_Click(object sender, EventArgs e)
        {
            if (d13.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (d13.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        d13.BackColor = Color.Red;
                        clk++;
                        addSeat("d13");
                    }
                }

                else
                {
                    d13.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("d13");
                }
            }
        }

        private void A1_Click(object sender, EventArgs e)
        {
            if (a1.BackColor != Color.DimGray)
            {
                if (total == 0)
                {
                    MessageBox.Show("인원을 선택해 주십시오.", "Warning");
                }

                else if (a1.BackColor == Color.Gainsboro)
                {
                    if (clk < total)
                    {
                        a1.BackColor = Color.Red;
                        clk++;
                        addSeat("a1");
                    }
                }

                else
                {
                    a1.BackColor = Color.Gainsboro;
                    clk--;
                    delSeat("a1");
                }
            }
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
        }
    }
}
