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
    public partial class main : Form
    {
        string pNum = ""; //사용자 전화번호
        bool isMaster = false; //관리자 여부

        public main()
        {
            InitializeComponent();

            button6.Visible = false;
            //필요한 텍스트 파일들이 있는지 체크하고, 없을 시 생성.
            string path1 = @"reservationchart.txt";
            if (!(System.IO.File.Exists(path1)))
            {
                System.IO.File.WriteAllText(path1, "", Encoding.UTF8);
            }
            string path2 = @"tmp.txt";
            if (!(System.IO.File.Exists(path2)))
            {
                System.IO.File.WriteAllText(path2, "", Encoding.UTF8);
            }
            string path3 = @"creditcard.txt";
            if (!(System.IO.File.Exists(path3)))
            {
                System.IO.File.WriteAllText(path3, "NH 농협카드\n1111222233334444\n", Encoding.UTF8);
            }
            string path4 = @"users.txt";
            if (!(System.IO.File.Exists(path4)))
            {
                System.IO.File.WriteAllText(path4, "abcd\n1234\n홍길동\n01011112222\nabcde\n12345\n관리자\n01011111111\n", Encoding.UTF8);
            }
            string path5 = @"snackbill.txt";
            if (!(System.IO.File.Exists(path5)))
            {
                System.IO.File.WriteAllText(path5, "0", Encoding.UTF8);
            }
            string path6 = @"moviebill.txt";
            if (!(System.IO.File.Exists(path6)))
            {
                System.IO.File.WriteAllText(path6, "0", Encoding.UTF8);
            }
        }

        public main(string uname, string pn, bool master)
        {
            InitializeComponent();

            isMaster = master;
            if (!isMaster)
            {
                button6.Visible = false;
            }

            //필요한 텍스트 파일들이 있는지 체크하고, 없을 시 생성.
            string path1 = @"reservationchart.txt";
            if (!(System.IO.File.Exists(path1)))
            {
                System.IO.File.WriteAllText(path1, "", Encoding.UTF8);
            }
            string path2 = @"tmp.txt";
            if (!(System.IO.File.Exists(path2)))
            {
                System.IO.File.WriteAllText(path2, "", Encoding.UTF8);
            }
            string path3 = @"creditcard.txt";
            if (!(System.IO.File.Exists(path3)))
            {
                System.IO.File.WriteAllText(path3, "NH 농협카드\n1111222233334444\n", Encoding.UTF8);
            }
            string path4 = @"users.txt";
            if (!(System.IO.File.Exists(path4)))
            {
                System.IO.File.WriteAllText(path4, "abcd\n1234\n홍길동\n01011112222\nabcde\n12345\n관리자\n01011111111\n", Encoding.UTF8);
            }
            string path5 = @"snackbill.txt";
            if (!(System.IO.File.Exists(path5)))
            {
                System.IO.File.WriteAllText(path5, "0", Encoding.UTF8);
            }
            string path6 = @"moviebill.txt";
            if (!(System.IO.File.Exists(path6)))
            {
                System.IO.File.WriteAllText(path6, "0", Encoding.UTF8);
            }
            pNum = pn;
            label3.Text = uname + "님,\n환영합니다.";
            button1.Text = "로그아웃";
        }

        private void Button1_Click(object sender, EventArgs e) //로그인/아웃 버튼
        {
            if (button1.Text == "로그인")
            {
                login loginForm = new login();
                this.Hide();
                loginForm.ShowDialog();

            }

            else
            {
                button6.Visible = false;
                label3.Text = "로그아웃 상태";
                button1.Text = "로그인";
                MessageBox.Show("로그아웃 하셨습니다.", "Log Out");
            }
        }

        private void Button2_Click(object sender, EventArgs e) //예매 버튼
        {
            if (button1.Text == "로그인")
            {
                MessageBox.Show("로그인 후 이용하실 수 있습니다.", "Warning");
            }
            else
            {
                ticket ticketForm = new ticket(pNum);
                ticketForm.ShowDialog();
            }
        }

        private void Button3_Click(object sender, EventArgs e) //예매 내역 버튼
        {
            reservation resForm = new reservation();
            resForm.ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e) //종료 버튼
        {
            MessageBox.Show("이용 해 주셔서 감사합니다.", "Exit");
            Application.Exit();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e) // 영화 정렬
        {
            if (comboBox1.Text == "개봉일 순") //개봉일 순 정렬
            {
                //1번
                rankGroup1.Text = "1. 슈퍼 마리오 브라더스";
                rank1image.Load(@"슈퍼마리오브라더스.jpg");
                rankrate1.Text = "예매율 1.0%";
                rankout1.Text = "2023.04.26 개봉";

                //2번
                rankGroup2.Text = "2. 가디언즈 오브 갤럭시 : VOLUME 3";
                rank2image.Load(@"가오갤.jpg");
                rankrate2.Text = "예매율 2.7%";
                rankout2.Text = "2023.05.03 개봉";

                //3번
                rankGroup3.Text = "3. 분노의 질주: 라이드 오어 다이";
                rank3image.Load(@"분노의질주.jpg");
                rankrate3.Text = "예매율 1.5%";
                rankout3.Text = "2023.05.17 개봉";

                //4번
                rankGroup4.Text = "4. 인어공주";
                rank4image.Load(@"인어공주.jpg");
                rankrate4.Text = "예매율 1.8%";
                rankout4.Text = "2023.05.24 개봉";

                //5번
                rankGroup5.Text = "5. 범죄도시3";
                rank5image.Load(@"범죄도시3.jpg");
                rankrate5.Text = "예매율 75.1%";
                rankout5.Text = "2023.05.31 개봉";
            }

            else //예매율 순 정렬
            {
                //1번
                rankGroup1.Text = "1. 범죄도시3";
                rank1image.Load(@"범죄도시3.jpg");
                rankrate1.Text = "예매율 75.1%";
                rankout1.Text = "2023.05.31 개봉";

                //2번
                rankGroup2.Text = "2. 가디언즈 오브 갤럭시 : VOLUME 3";
                rank2image.Load(@"가오갤.jpg");
                rankrate2.Text = "예매율 2.7%";
                rankout2.Text = "2023.05.03 개봉";

                //3번
                rankGroup3.Text = "3. 인어공주";
                rank3image.Load(@"인어공주.jpg");
                rankrate3.Text = "예매율 1.8%";
                rankout3.Text = "2023.05.24 개봉";

                //4번
                rankGroup4.Text = "4. 분노의 질주: 라이드 오어 다이";
                rank4image.Load(@"분노의질주.jpg");
                rankrate4.Text = "예매율 1.5%";
                rankout4.Text = "2023.05.17 개봉";

                //5번
                rankGroup5.Text = "5. 슈퍼 마리오 브라더스";
                rank5image.Load(@"슈퍼마리오브라더스.jpg");
                rankrate5.Text = "예매율 1.0%";
                rankout5.Text = "2023.04.26 개봉";
            }
        }

        private void Rankbutton1_Click(object sender, EventArgs e) //예매 버튼1
        {
            if (button1.Text == "로그인")
            {
                MessageBox.Show("로그인 후 이용하실 수 있습니다.", "Warning");
            }
            else
            {
                string mName = (rankGroup1.Text).Substring(3);
                ticket ticketForm = new ticket(mName,pNum);
                ticketForm.Show();
            }
        }

        private void Rankbutton2_Click(object sender, EventArgs e) //예매 버튼2
        {
            if (button1.Text == "로그인")
            {
                MessageBox.Show("로그인 후 이용하실 수 있습니다.", "Warning");
            }
            else
            {
                string mName = (rankGroup2.Text).Substring(3);
                ticket ticketForm = new ticket(mName,pNum);
                ticketForm.Show();
            }
        }

        private void Rankbutton3_Click(object sender, EventArgs e) //예매 버튼3
        {
            if (button1.Text == "로그인")
            {
                MessageBox.Show("로그인 후 이용하실 수 있습니다.", "Warning");
            }
            else
            {
                string mName = (rankGroup3.Text).Substring(3);
                ticket ticketForm = new ticket(mName, pNum);
                ticketForm.Show();
            }
        }

        private void Rankbutton4_Click(object sender, EventArgs e) //예매 버튼4
        {
            if (button1.Text == "로그인")
            {
                MessageBox.Show("로그인 후 이용하실 수 있습니다.", "Warning");
            }
            else
            {
                string mName = (rankGroup4.Text).Substring(3);
                ticket ticketForm = new ticket(mName, pNum);
                ticketForm.Show();
            }
        }

        private void Rankbutton5_Click(object sender, EventArgs e) //예매 버튼5
        {
            if (button1.Text == "로그인")
            {
                MessageBox.Show("로그인 후 이용하실 수 있습니다.", "Warning");
            }
            else
            {
                string mName = (rankGroup5.Text).Substring(3);
                ticket ticketForm = new ticket(mName, pNum);
                ticketForm.Show();
            }
        }

        private void Button5_Click(object sender, EventArgs e) //스낵 구매 버튼
        {
            if (button1.Text == "로그인")
            {
                MessageBox.Show("로그인 후 이용하실 수 있습니다.", "Warning");
            }
            else
            {
                snack snackForm = new snack();
                snackForm.Show();
            }
        }

        //포스터 클릭 시 영화 상세 페이지
        private void Rank1image_Click(object sender, EventArgs e) //첫 번째 포스터 클릭
        {
            bool log = false;
            if (button1.Text == "로그아웃")
            {
                log = true;
            }
            string mName = (rankGroup1.Text).Substring(3);
            detail detailForm = new detail(mName,log,pNum);
            detailForm.Show();
        }

        private void Rank2image_Click(object sender, EventArgs e) //두 번째 포스터 클릭
        {
            bool log = false;
            if (button1.Text == "로그아웃")
            {
                log = true;
            }
            string mName = (rankGroup2.Text).Substring(3);
            detail detailForm = new detail(mName, log, pNum);
            detailForm.Show();
        }

        private void Rank3image_Click(object sender, EventArgs e) //세 번째 포스터 클릭
        {
            bool log = false;
            if (button1.Text == "로그아웃")
            {
                log = true;
            }
            string mName = (rankGroup3.Text).Substring(3);
            detail detailForm = new detail(mName, log, pNum);
            detailForm.Show();
        }

        private void Rank4image_Click(object sender, EventArgs e) //네 번째 포스터 클릭
        {
            bool log = false;
            if (button1.Text == "로그아웃")
            {
                log = true;
            }
            string mName = (rankGroup4.Text).Substring(3);
            detail detailForm = new detail(mName, log, pNum);
            detailForm.Show();
        }

        private void Rank5image_Click(object sender, EventArgs e) //다섯 번째 포스터 클릭
        {
            bool log = false;
            if (button1.Text == "로그아웃")
            {
                log = true;
            }
            string mName = (rankGroup5.Text).Substring(3);
            detail detailForm = new detail(mName, log, pNum);
            detailForm.Show();
        }



        //포스터 위에 마우스 올렸을 때 마우스 커서 hand로 변경
        private void Rank1image_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Rank2image_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Rank3image_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Rank4image_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Rank5image_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        //포스터 위가 아닐 때 마우스 커서 기본값으로 변경
        private void Rank1image_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Rank2image_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Rank3image_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Rank4image_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Rank5image_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Button6_Click(object sender, EventArgs e) //관리자 메뉴 버튼
        {
            masterMenu masterForm = new masterMenu();
            masterForm.Show();
        }
    }
}
