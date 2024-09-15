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
    public partial class payticket : Form
    {
        int sum = 0; //상품 금액
        double dc = 0; //할인 금액
        int result = 0; //결제 금액

        string title; //영화 제목
        string dateTime; //영화 일시
        string theater; //영화관
        string tNum; //상영관 번호
        string seat; //좌석 정보
        string phonNum; //사용자 전화번호

        string cardCom = ""; //카드사 정보 입력
        string cardNum = ""; //카드 번호 입력

        bool isSnack = false; //스낵코너에서 결제창으로 넘어왔는지 여부 결정

        private void turnCredit(bool flag) //신용카드 입력 창 보이기 여부 결정 함수
        {
            label7.Visible = flag;
            label8.Visible = flag;
            comboBox2.Visible = flag;
            textBox1.Visible = flag;
            textBox2.Visible = flag;
            textBox3.Visible = flag;
            textBox4.Visible = flag;
            label4.Visible = flag;
            label5.Visible = flag;
            label6.Visible = flag;
        }

        public payticket()
        {
            InitializeComponent();
            groupBox3.Visible = false;
        }

        //스낵 구매에서 넘어오는 생성자
         public payticket(int tot, bool snack)
        {
            InitializeComponent();
            groupBox3.Visible = false;
            isSnack = snack;
            sum = tot;
            result = sum;
            label11.Text = (sum.ToString()) + " 원";
            label14.Text = (result.ToString()) + " 원";
        }

        //영화 예매에서 넘어올 때 실행되는 생성자
        public payticket(int tot, string titl, string date, string theat, string tn, string st, string pn)
        {
            InitializeComponent();
            sum = tot;
            result = sum;
            title = titl;
            dateTime = date;
            theater = theat;
            tNum = tn;
            seat = st;
            phonNum = pn;
            groupBox3.Visible = false;
            label11.Text = (sum.ToString()) + " 원";
            label14.Text = (result.ToString()) + " 원";
        }

        private void clearText()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void Button1_Click(object sender, EventArgs e) //신용카드 버튼
        {
            button1.BackColor = Color.DarkGray;
            groupBox3.Visible = true;
        }

        private void Button2_Click(object sender, EventArgs e) //할인쿠폰 적용 버튼
        {
            try
            {
                double tmp = int.Parse((comboBox1.Text).Substring(0, 2)); //쿠폰 할인율
                dc = (double)sum * (tmp / 100); //할인 금액 계산
                result = sum - (int)dc; //결제 금액 계산
                label13.Text = (dc.ToString()) + " 원";
                label14.Text = (result.ToString()) + " 원";
            }
            catch
            {
                MessageBox.Show("할인쿠폰을 선택 후 적용하기 버튼을 눌러주세요.", "Warning");
            }
        }

        private bool cardCheck(string cCom, string cNum)
        {
            bool tmp = false;
            string path = @"creditcard.txt";
            string[] textValue = System.IO.File.ReadAllLines(path);
            if (textValue.Length > 0)
            {
                for (int i = 0; i < textValue.Length; i += 3)
                {

                    if (cCom == textValue[i] && cNum == textValue[i+1])
                    {
                        tmp = true;
                        break;
                    }
                }
            }
            return tmp;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e) //카드사 선택
        {
            cardCom = comboBox2.Text;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //결제하기 버튼
        {
            cardNum = textBox1.Text + textBox2.Text + textBox3.Text + textBox4.Text;
            if (cardCheck(cardCom, cardNum))
            {
                if (isSnack) //스낵 구매 결제인 경우
                {
                    //스낵 판매 금액 증가, 저장
                    string path = @"snackbill.txt";
                    int tmp = int.Parse((System.IO.File.ReadAllText(path)).ToString());
                    System.IO.File.WriteAllText(path, (tmp+result).ToString(), Encoding.UTF8);
                    MessageBox.Show("결제가 완료되었습니다.\r\n", "Success");
                    this.Close();
                }

                else //영화 예매인 경우
                {
                    string res = title + "\n";
                    res += dateTime + "\n";
                    res += theater + "\n";
                    res += tNum + "\n";
                    res += seat + "\n";

                    string path = @"reservationchart.txt";
                    System.IO.File.AppendAllText(path, phonNum + "\n" + res, Encoding.UTF8);

                    res += "총 결제 금액: " + result.ToString() + "원\n";
                    res += "예매내역 메뉴에서 조회하실 수 있습니다.\n";

                    path = @"moviebill.txt";
                    int tmp = int.Parse((System.IO.File.ReadAllText(path)).ToString());
                    System.IO.File.WriteAllText(path, (tmp + result).ToString(), Encoding.UTF8);

                    MessageBox.Show("예매가 완료되었습니다.\r\n" + res, "Success");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("카드 정보를 다시 한 번 확인해주세요.","Warning");
                clearText();
            }
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Label14_Click(object sender, EventArgs e)
        {

        }
    }
}
