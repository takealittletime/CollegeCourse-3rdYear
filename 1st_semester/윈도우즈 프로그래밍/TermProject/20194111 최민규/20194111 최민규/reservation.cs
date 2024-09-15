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
    public partial class reservation : Form
    {
        string inp; //전화번호 입력

        string phonNum; //전화번호 정보
        string title; //영화제목 정보
        string dateTime; //영화 일시 정보
        string theater; //영화관 지역 정보
        string tNum; //상영관 번호 정보
        string seat; //좌석 정보

        int n=0; //사용자 예매 내역 번호
        bool found = false; //검색 내역 유무


        public reservation()
        {
            InitializeComponent();
            groupBox2.Visible = false;
            groupBox7.Visible = false;
            groupBox12.Visible = false;
        }

        private void InitForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            groupBox2.Visible = false;
            groupBox7.Visible = false;
            groupBox12.Visible = false;
            n = 0;
            found = false;
        }

        private void Button1_Click(object sender, EventArgs e) //조회 버튼
        {
            inp = textBox1.Text + textBox2.Text + textBox3.Text;
            string path = @"reservationchart.txt";
            string[] textValue = System.IO.File.ReadAllLines(path);
            if (textValue.Length > 0)
            {
                for (int i = 0; i < textValue.Length; i += 6)
                {

                    if (inp == textValue[i])
                    {
                        n++;
                        found = true;
                        if (n == 1)
                        {
                            groupBox2.Visible = true;
                            label3.Text = textValue[i + 1];
                            label8.Text = textValue[i + 2];
                            label6.Text = textValue[i + 3];
                            label7.Text = textValue[i + 4];
                            label9.Text = textValue[i + 5];
                        }
                        if (n == 2)
                        {
                            groupBox12.Visible = true;
                            label19.Text = textValue[i + 1];
                            label18.Text = textValue[i + 2];
                            label12.Text = textValue[i + 3];
                            label17.Text = textValue[i + 4];
                            label15.Text = textValue[i + 5];
                        }
                        if (n == 3)
                        {
                            groupBox7.Visible = true;
                            label14.Text = textValue[i + 1];
                            label13.Text = textValue[i + 2];
                            label16.Text = textValue[i + 3];
                            label11.Text = textValue[i + 4];
                            label10.Text = textValue[i + 5];
                        }
                    }
                }
            }
            
            if (!found)
            {
                MessageBox.Show("해당되는 예매 내역이 없습니다.", "Failed");
                InitForm();
            }

            n = 0;
            found = false;
        }

        private void Button2_Click(object sender, EventArgs e) //첫 번째 그룹박스 취소 버튼
        {
            if (MessageBox.Show("해당 내역을 취소하시겠습니까?", "Cancel", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string path1 = @"reservationchart.txt";
                string path2 = @"tmp.txt";
                string[] textValue = System.IO.File.ReadAllLines(path1);
                if (textValue.Length > 0)
                {
                    for (int i = 0; i < textValue.Length; i += 6)
                    {
                        if (inp != textValue[i]) //전화번호 자체가 다른 경우 일단 tmp에 출력
                        {
                            System.IO.File.AppendAllText(path2, textValue[i] + "\n" + textValue[i + 1] + "\n" + textValue[i + 2]
                                + "\n" + textValue[i + 3] + "\n" + textValue[i + 4] + "\n" + textValue[i + 5]+"\n", Encoding.UTF8);
                        }
                        else //전화번호가 같은 경우, 나머지 항목들 비교.
                        {
                            if (textValue[i + 1] != label3.Text || textValue[i + 2] != label8.Text || textValue[i + 3] != label6.Text ||
                                textValue[i + 4] != label7.Text || textValue[i + 5] != label9.Text)
                            {
                                System.IO.File.AppendAllText(path2, textValue[i] + "\n" + textValue[i + 1] + "\n" + textValue[i + 2]
                                + "\n" + textValue[i + 3] + "\n" + textValue[i + 4] + "\n" + textValue[i + 5]+"\n", Encoding.UTF8);
                            }
                        }
                    }
                    System.IO.File.Delete(path1);
                    System.IO.File.Copy(path2, path1);
                    System.IO.File.WriteAllText(path2,"",Encoding.UTF8);
                    MessageBox.Show("예매 취소 완료!", "Success");

                    InitForm();
                }
            }

        }

        private void Button4_Click(object sender, EventArgs e) //두 번째 그룹박스 취소 버튼
        {
            if (MessageBox.Show("해당 내역을 취소하시겠습니까?", "Cancel", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string path1 = @"reservationchart.txt";
                string path2 = @"tmp.txt";
                string[] textValue = System.IO.File.ReadAllLines(path1);
                if (textValue.Length > 0)
                {
                    for (int i = 0; i < textValue.Length; i += 6)
                    {
                        if (inp != textValue[i]) //전화번호 자체가 다른 경우 일단 tmp에 출력
                        {
                            System.IO.File.AppendAllText(path2, textValue[i] + "\n" + textValue[i + 1] + "\n" + textValue[i + 2]
                                + "\n" + textValue[i + 3] + "\n" + textValue[i + 4] + "\n" + textValue[i + 5] + "\n", Encoding.UTF8);
                        }
                        else //전화번호가 같은 경우, 나머지 항목들 비교.
                        {
                            if (textValue[i + 1] != label19.Text || textValue[i + 2] != label18.Text || textValue[i + 3] != label12.Text ||
                                textValue[i + 4] != label17.Text || textValue[i + 5] != label15.Text)
                            {
                                System.IO.File.AppendAllText(path2, textValue[i] + "\n" + textValue[i + 1] + "\n" + textValue[i + 2]
                                + "\n" + textValue[i + 3] + "\n" + textValue[i + 4] + "\n" + textValue[i + 5] + "\n", Encoding.UTF8);
                            }
                        }
                    }
                    System.IO.File.Delete(path1);
                    System.IO.File.Copy(path2, path1);
                    System.IO.File.WriteAllText(path2, "", Encoding.UTF8);
                    MessageBox.Show("예매 취소 완료!", "Success");

                    InitForm();
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e) //세 번째 그룹박스 취소 버튼
        {
            if (MessageBox.Show("해당 내역을 취소하시겠습니까?", "Cancel", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string path1 = @"reservationchart.txt";
                string path2 = @"tmp.txt";
                string[] textValue = System.IO.File.ReadAllLines(path1);
                if (textValue.Length > 0)
                {
                    for (int i = 0; i < textValue.Length; i += 6)
                    {
                        if (inp != textValue[i]) //전화번호 자체가 다른 경우 일단 tmp에 출력
                        {
                            System.IO.File.AppendAllText(path2, textValue[i] + "\n" + textValue[i + 1] + "\n" + textValue[i + 2]
                                + "\n" + textValue[i + 3] + "\n" + textValue[i + 4] + "\n" + textValue[i + 5] + "\n", Encoding.UTF8);
                        }
                        else //전화번호가 같은 경우, 나머지 항목들 비교.
                        {
                            if (textValue[i + 1] != label14.Text || textValue[i + 2] != label13.Text || textValue[i + 3] != label16.Text ||
                                textValue[i + 4] != label11.Text || textValue[i + 5] != label10.Text)
                            {
                                System.IO.File.AppendAllText(path2, textValue[i] + "\n" + textValue[i + 1] + "\n" + textValue[i + 2]
                                + "\n" + textValue[i + 3] + "\n" + textValue[i + 4] + "\n" + textValue[i + 5] + "\n", Encoding.UTF8);
                            }
                        }
                    }
                    System.IO.File.Delete(path1);
                    System.IO.File.Copy(path2, path1);
                    System.IO.File.WriteAllText(path2, "", Encoding.UTF8);
                    MessageBox.Show("예매 취소 완료!", "Success");

                    InitForm();
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e) //초기화 버튼
        {
            InitForm();
        }
    }
}
