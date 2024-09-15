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
    public partial class snack : Form
    {
        string menu = "";
        int price;
        bool isSnack = true;

        public snack()
        {
            InitializeComponent();
        }

        private void Button4_Click(object sender, EventArgs e) //팝콘L사이즈 담기버튼
        {
            menu += label2.Text + "(" + comboBox3.Text + ")\n";
            price += 5500;
            label14.Text = price.ToString() + " 원";
        }

        private void Button5_Click(object sender, EventArgs e) //팝콘 M사이즈 담기 버튼
        {
            menu += label3.Text + "(" + comboBox4.Text + ")\n";
            price += 5000;
            label14.Text = price.ToString() + " 원";
        }

        private void Button8_Click(object sender, EventArgs e) //더블 콤보 담기 버튼
        {
            menu += label26.Text + "(" + comboBox6.Text + ")\n";
            price += 14000;
            label14.Text = price.ToString() + " 원";
        }

        private void Button2_Click(object sender, EventArgs e) //사이다 담기 버튼
        {
            menu += label6.Text + "(" + comboBox2.Text + ")\n";
            if (comboBox2.Text == "M")
                price += 2800;
            else
                price += 3300;
            label14.Text = price.ToString() + " 원";
        }

        private void Button3_Click(object sender, EventArgs e) //나쵸 담기 버튼
        {
            menu += label9.Text + "\n";
            price += 4900;
            label14.Text = price.ToString() + " 원";
        }

        private void Button6_Click(object sender, EventArgs e) //땅콩 버터 오징어 담기 버튼
        {
            menu += label15.Text + "\n";
            price += 3500;
            label14.Text = price.ToString() + " 원";
        }

        private void Button7_Click(object sender, EventArgs e) //스위트 콤보 담기 버튼
        {
            menu += label23.Text + "(" + comboBox5.Text + ")\n";
            price += 10000;
            label14.Text = price.ToString() + " 원";
        }

        private void Button1_Click(object sender, EventArgs e) //콜라 담기 버튼
        {
            menu += label5.Text + "(" + comboBox1.Text + ")\n";
            if (comboBox1.Text == "M")
                price += 2800;
            else
                price += 3300;
            label14.Text = price.ToString() + " 원";
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e) //콜라 사이즈 선택
        {
            if (comboBox1.Text == "M")
            {
                label17.Text = "2800원";
            }
            else
            {
                label17.Text = "3300원";
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e) //사이다 사이즈 선택
        {
            if (comboBox2.Text == "M")
            {
                label7.Text = "2800원";
            }
            else
            {
                label7.Text = "3300원";
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (price == 0)
            {
                MessageBox.Show("구매하실 스낵을 먼저 담아주세요.", "Warning");
            }
            else
            {
                if (MessageBox.Show("<구매 목록>\n" + menu + "총 결제 금액: "+price.ToString()+"원\n"+
                    "결제하시겠습니까?", "payment", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MessageBox.Show("신규 오픈 기념 할인 쿠폰 이벤트 진행중!\r\n10% 할인 쿠폰을 확인하세요.\r\n*영화 예매/스낵 구매에 이용 가능!", "Event");
                    this.Close();
                    payticket payForm = new payticket(price,isSnack);
                    payForm.ShowDialog();
                }
            }
        }
    }
}
