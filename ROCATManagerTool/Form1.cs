using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROCATManagerTool
{

    public partial class Form1 : Form
    {
        List<TextBox> userName = new List<TextBox>();
        List<NumericUpDown> removeNum = new List<NumericUpDown>();

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MakeUserForms(20);
            NewUserNameForm.Enabled = false;
            AddUserButton.Enabled = false;
        }


        // はじめにフォームを作る（値は適当）
        public void MakeUserForms(int num)
        {
            

            int y = 40;

            for(int i = 0; i < num; i++)
            {
                TextBox oneUser = new TextBox();
                NumericUpDown oneNum = new NumericUpDown();

                oneUser.Location = new Point(31, y);
                oneNum.Location = new Point(298, y);

                oneUser.Size = new Size(215,19);
                oneNum.Size = new Size(120, 19);

                oneUser.Text = i.ToString();
                oneNum.Value = i;

                userName.Add(oneUser);
                removeNum.Add(oneNum);
                y += 38;

                this.panel1.Controls.Add(oneUser);
                this.panel1.Controls.Add(oneNum);

            }
        }


        // ユーザ追加
        private void AddUserButton_Click(object sender, EventArgs e)
        {
            TextBox newUser = new TextBox();
            NumericUpDown newNum = new NumericUpDown();

            int usernum = userName.Count;

            newUser.Location = new Point(31, userName[usernum-1].Location.Y + 38);
            newNum.Location = new Point(298, removeNum[usernum - 1].Location.Y + 38);

            newUser.Text = NewUserNameForm.Text;
            NewUserNameForm.Text = "";
            newNum.Value = 0;

            newUser.Size = new Size(215, 19);
            newNum.Size = new Size(120, 19);

            userName.Add(newUser);
            removeNum.Add(newNum);

            this.panel1.Controls.Add(newUser);
            this.panel1.Controls.Add(newNum);


            // お名前が空欄時に追加しない
            // 既にある名前は追加しない
        }


        // ファイル読み込み
        private void ReadJson_Click(object sender, EventArgs e)
        {
            NewUserNameForm.Enabled = true;
            AddUserButton.Enabled = true;
            MakeUserForms(20);
        }
    }
}
