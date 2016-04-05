using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.IO;

namespace ROCATManagerTool
{
    public partial class Form1 : Form
    {
        List<string> userNameList = new List<string>();
        List<TextBox> userNameBoxList = new List<TextBox>();
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

                userNameList.Add(oneUser.Text);
                userNameBoxList.Add(oneUser);
                removeNumList.Add(oneNum);
                deleteList.Add(oneDelete);

                y += 38;

                this.panel1.Controls.Add(oneUser);
                this.panel1.Controls.Add(oneNum);
                this.panel1.Controls.Add(oneDelete);

            }
        }


        // 新規ユーザ追加ボタン
        private void AddUserButton_Click(object sender, EventArgs e)
        {
            
            if(NewUserNameForm.Text == null || NewUserNameForm.Text == "")  // おなまえが空なら追加しない
            {
                MessageBox.Show("Input User Name!!");
            }
            else if(userNameList.Contains(NewUserNameForm.Text))            // 同じ名前の人がいるなら追加しない
            {
                MessageBox.Show("Same Name User already exists!!");
            }
            else
            {
                AddUser();
            }
        }


        // 新規ユーザ追加処理
        private void AddUser()
        {
            TextBox newUser = new TextBox();
            NumericUpDown newNum = new NumericUpDown();
            Button newDelete = new Button();

            int usernum = userNameBoxList.Count;

            newUser.Location = new Point(31, userNameBoxList[usernum - 1].Location.Y + 38);
            newNum.Location = new Point(298, removeNumList[usernum - 1].Location.Y + 38);
            newDelete.Location = new Point(479, deleteList[usernum - 1].Location.Y + 38);

            newUser.Text = NewUserNameForm.Text;
            NewUserNameForm.Text = "";
            newNum.Value = 0;
            newDelete.Text = "×";

            newUser.Size = new Size(215, 19);
            newNum.Size = new Size(120, 19);
            newDelete.Size = new Size(19, 19);

            userNameList.Add(newUser.Text);
            userNameBoxList.Add(newUser);
            removeNumList.Add(newNum);
            deleteList.Add(newDelete);

            this.panel1.Controls.Add(newUser);
            this.panel1.Controls.Add(newNum);
            this.panel1.Controls.Add(newDelete);
        }


        // ファイル読み込みボタン
        private void ReadJson_Click(object sender, EventArgs e)
        {
            userNameBoxList.Clear();
            removeNumList.Clear();
            deleteList.Clear();
            panel1.Controls.Clear();

            NewUserNameForm.Enabled = true;
            AddUserButton.Enabled = true;

            MakeUserForms(20);
            ReadJsonFile();
        }


        // jsonファイルを読み込む
        private void ReadJsonFile()
        {
           // StreamReader file = File.OpenText("test2.json");

            // Jsonファイルを読み込む
            var text = File.ReadAllText("test.json");

            // "SATDRanking"のタグがあるトコのインデックス（タグの次の[の場所）を設定
            int index = text.IndexOf("\"SATDRanking\": ") + 15;

            // ランキング部分だけの文字列を作る（index～最後の}より1文字分前までがランキング部分）
            var newtext = text.Substring(index, text.Length - index - 1);

            // ランキング部分だけの文字列からデータを読み出す
            var list = JsonConvert.DeserializeObject<List<User>>(newtext);

            foreach (var item in list)
            {
                Console.WriteLine("Name: {0}, Num: {1}", item.name, item.num);
            }

        }
    }

    // ユーザのクラス
    [DataContract]
    public class User
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int num { get; set; }
    }
}
