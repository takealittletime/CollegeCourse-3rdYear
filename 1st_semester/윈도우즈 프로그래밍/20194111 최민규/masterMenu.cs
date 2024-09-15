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
    public partial class masterMenu : Form
    {
        string txt = "";
        public masterMenu()
        {
            InitializeComponent();
            label1.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
        }
        private void Button2_Click(object sender, EventArgs e) //전체 예매 현황 조회
        {
            label1.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            string txt = "";
            string path = @"reservationchart.txt";
            string[] textValue = System.IO.File.ReadAllLines(path);
            if (textValue.Length > 0)
            {
                for (int i = 0; i < textValue.Length; i ++)
                {
                    if (i % 6 == 0)
                    {
                        txt += "\n";
                    }
                    txt += textValue[i] + "\n";
                }
            }
            label1.Text = txt;
        }

        private void Button1_Click(object sender, EventArgs e) //현재 판매액 조회
        {
            label1.Visible = false;
            groupBox1.Visible = true;
            groupBox2.Visible = true;

            string path1 = @"snackbill.txt";
            string path2 = @"moviebill.txt";

            string snack = System.IO.File.ReadAllText(path1) +" 원";
            string movie = System.IO.File.ReadAllText(path2) +" 원";

            label2.Text = snack;
            label3.Text = movie;
        }
    }
}
