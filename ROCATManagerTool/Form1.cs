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
        List<TextBox> userNameList = new List<TextBox>();
        List<NumericUpDown> removeNumList = new List<NumericUpDown>();
        List<Button> deleteList = new List<Button>();

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
            

            int y = 5;

            for(int i = 0; i < num; i++)
            {
                TextBox oneUser = new TextBox();
                NumericUpDown oneNum = new NumericUpDown();
                Button oneDelete = new Button();

                oneUser.Location = new Point(31, y);
                oneNum.Location = new Point(298, y);
                oneDelete.Location = new Point(479, y);

                oneUser.Size = new Size(215,19);
                oneNum.Size = new Size(120, 19);
                oneDelete.Size = new Size(19, 19);

                oneUser.Text = i.ToString();
                oneNum.Value = i;
                oneDelete.Text = "×";

                userNameList.Add(oneUser);
                removeNumList.Add(oneNum);
                deleteList.Add(oneDelete);

                y += 38;

                this.panel1.Controls.Add(oneUser);
                this.panel1.Controls.Add(oneNum);
                this.panel1.Controls.Add(oneDelete);

            }
        }


        // ユーザ追加
        private void AddUserButton_Click(object sender, EventArgs e)
        {
            TextBox newUser = new TextBox();
            NumericUpDown newNum = new NumericUpDown();
            Button newDelete = new Button();

            int usernum = userNameList.Count;

            newUser.Location = new Point(31, userNameList[usernum-1].Location.Y + 38);
            newNum.Location = new Point(298, removeNumList[usernum - 1].Location.Y + 38);
            newDelete.Location = new Point(479, deleteList[usernum - 1].Location.Y + 38);

            newUser.Text = NewUserNameForm.Text;
            NewUserNameForm.Text = "";
            newNum.Value = 0;
            newDelete.Text = "×";

            newUser.Size = new Size(215, 19);
            newNum.Size = new Size(120, 19);
            newDelete.Size = new Size(19, 19);

            userNameList.Add(newUser);
            removeNumList.Add(newNum);
            deleteList.Add(newDelete);

            this.panel1.Controls.Add(newUser);
            this.panel1.Controls.Add(newNum);
            this.panel1.Controls.Add(newDelete);


            // お名前が空欄時に追加しない
            // 既にある名前は追加しない
        }


        // ファイル読み込み
        private void ReadJson_Click(object sender, EventArgs e)
        {
            userNameList.Clear();
            removeNumList.Clear();
            deleteList.Clear();
            panel1.Controls.Clear();

            NewUserNameForm.Enabled = true;
            AddUserButton.Enabled = true;

            MakeUserForms(20);
        }
    }
}
