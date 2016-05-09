﻿using System;
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
        List<User> userList = new List<User>();                         // ユーザのリスト

        List<TextBox> userNameBoxList = new List<TextBox>();            // ユーザの名前が入ったテキストボックスのリスト

        List<NumericUpDown> removeNumBoxList = new List<NumericUpDown>();  // ユーザの除去数の入ったボックスのリスト

        List<Button> deleteList = new List<Button>();                   // ユーザ削除用のボタンのリスト

        String defaultDir = "C:\\";

        String fileName;

        String jsonText;


        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MakeUserForms(20);
            ProhibitWrite();
            ProhibitAdduser();
        }


        // フォームを作る
        public void MakeUserForms()
        {

            int y = 5; // 各フォームのy軸の高さ設定用

            for(int i = 0; i < userList.Count; i++)
            {
                TextBox oneUserName = new TextBox();        // ユーザのおなまえBox
                NumericUpDown oneNum = new NumericUpDown(); // ユーザの除去数Box
                Button oneDelete = new Button();            // ユーザの削除用ボタン

                // フォームの位置を指定
                oneUserName.Location = new Point(31, y);
                oneNum.Location = new Point(298, y);
                oneDelete.Location = new Point(479, y);

                // フォームの大きさを指定
                oneUserName.Size = new Size(215, 19);
                oneNum.Size = new Size(120, 19);
                oneDelete.Size = new Size(19, 19);

                // フォームの値を設定
                oneUserName.Text = userList[i].name;
                oneNum.Value = userList[i].num;
                oneDelete.Text = "X";

                oneDelete.Click += new EventHandler(this.DeleteButtonClick);

                // フォームをリストに追加
                userNameBoxList.Add(oneUserName);
                removeNumBoxList.Add(oneNum);
                deleteList.Add(oneDelete);

                // y軸の値を変更
                y += 38;

                this.panel1.Controls.Add(oneUserName);
                this.panel1.Controls.Add(oneNum);
                this.panel1.Controls.Add(oneDelete);

            }

            foreach (var item in userList)
            {
                Console.WriteLine("Name: {0}, Num: {1}", item.name, item.num);

            }

        }


        // 新規ユーザ追加ボタン
        private void AddUserButton_Click(object sender, EventArgs e)
        {
           
            if(NewUserNameForm.Text == null || NewUserNameForm.Text == "")  // おなまえが空なら追加しない
            {
                MessageBox.Show("Input User name!!");
            }
            else if(userList.FindIndex(item => item.name == NewUserNameForm.Text) != -1)    // 同じ名前の人がいるなら追加しない
            {
                MessageBox.Show("Same name user already exists!!");
            }
            else
            {
                AddUser();
            }
        }


        // 新規ユーザ追加処理
        private void AddUser()
        {
            User newUser = new User();
            TextBox newUserName = new TextBox();
            NumericUpDown newNum = new NumericUpDown();
            Button newDelete = new Button();

            int usernum = userNameBoxList.Count;

            newUserName.Location = new Point(31, userNameBoxList[usernum - 1].Location.Y + 38);
            newNum.Location = new Point(298, removeNumBoxList[usernum - 1].Location.Y + 38);
            newDelete.Location = new Point(479, deleteList[usernum - 1].Location.Y + 38);

            newUserName.Text = NewUserNameForm.Text;
            NewUserNameForm.Text = "";
            newNum.Value = 0;
            newDelete.Text = "X";

            newUserName.Size = new Size(215, 19);
            newNum.Size = new Size(120, 19);
            newDelete.Size = new Size(19, 19);

            newUser.name = newUserName.Text;
            newUser.num = (int)newNum.Value;

            userList.Add(newUser);


            newDelete.Click += new EventHandler(this.DeleteButtonClick);

            userNameBoxList.Add(newUserName);
            removeNumBoxList.Add(newNum);
            deleteList.Add(newDelete);

            this.panel1.Controls.Add(newUserName);
            this.panel1.Controls.Add(newNum);
            this.panel1.Controls.Add(newDelete);
        }


        // ファイル読み込みボタン
        private void ReadJson_Click(object sender, EventArgs e)
        {
            // 今表示しているデータを消す
            userList.Clear();

            userNameBoxList.Clear();
            removeNumBoxList.Clear();

            deleteList.Clear();
            panel1.Controls.Clear();

            // 新規ユーザ追加を禁止
            ProhibitAdduser();

            // 書き込みを禁止
            ProhibitWrite();

            // ファイル読み込み処理へ
            ReadJsonFile();
        }


        // jsonファイルを読み込む
        private void ReadJsonFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            System.IO.Stream s;

            ofd.Title = "Please select the ROCAT file";
            ofd.InitialDirectory = defaultDir;
            ofd.Filter = "Json file(*.json)|*.json";
            ofd.FilterIndex = 0;

            ofd.RestoreDirectory = true;   // ダイアログボックスを閉じる前に、現在のディレクトリを復元する
            ofd.CheckFileExists = true;    // 存在しないファイルを指定すると警告
            ofd.CheckPathExists = true;    // 存在しないパスを指定すると警告
            ofd.DereferenceLinks = true;   // ショートカットを選択した場合、参照先のパスを取得する(Falseだとショートカットファイルそのものを取得)

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                s = ofd.OpenFile();
                if(s != null)
                {

                    // StreamReader file = File.OpenText("test2.json");

                    // Jsonファイルを読み込む
                    var text = File.ReadAllText(ofd.FileName, System.Text.Encoding.GetEncoding("Shift_JIS"));
                    jsonText = text;

                    defaultDir = System.IO.Path.GetDirectoryName(ofd.FileName);
                    fileName = Path.GetFileName(ofd.FileName);

                    // 街のJsonファイルじゃない場合はメッセージを出す
                    if (text.IndexOf("\"buildings\":") == -1)
                    {
                        MessageBox.Show("The file is not ROCAT file!!");
                    }
                    else
                    {
                        // "SATDRanking"のタグがあるトコのインデックス（タグの次の[の場所）を設定
                        int index = text.IndexOf("\"SATDRanking\": ") + 15;

                        // ランキング部分だけの文字列を作る（index～最後の}より1文字分前までがランキング部分）
                        var newtext = text.Substring(index, text.Length - index - 1);

                        // ランキング部分だけの文字列からデータを読み出す
                        var list = JsonConvert.DeserializeObject<List<User>>(newtext);

                        foreach (var item in list)
                        {
                            Console.WriteLine("Name: {0}, Num: {1}", item.name, item.num);
                            userList.Add(item);

                        }

                        // 読み込んだデータを基にフォームを作る
                        MakeUserForms();
                        s.Close();

                        // ユーザ追加と書き込みを許可
                        AllowAddUser();
                        AllowWrite();

                    }
                }
            }



        }

 
        // 新規ユーザ追加を許可
        private void AllowAddUser()
        {
            NewUserNameForm.Enabled = true;
            AddUserButton.Enabled = true;
        }

        // 新規ユーザ追加を禁止
        private void ProhibitAdduser()
        {
            NewUserNameForm.Enabled = false;
            AddUserButton.Enabled = false;
        }


        // Jsonファイルにランキング情報を書き込む
        private void WriteJsonFile()
        {
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            sfd.FileName = fileName;

            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = defaultDir;

            //[ファイルの種類]に表示される選択肢を指定する
            sfd.Filter =
                "Json file(*.json)|*.json";
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにしない
            sfd.FilterIndex = 0;

            //タイトルを設定する
            sfd.Title = "Please save the ROCAT file";

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき

                // "SATDRanking"のタグがあるトコのインデックス（タグの次の[の場所の次の改行まで）を設定
                int index = jsonText.IndexOf("\"SATDRanking\": ") + 16;

                // 書き込み用文字列
                string allText = "";

                // ユーザ部分の文字列
                string userText = "";

                // 元のJsonファイルの最初からインデックスのところまでの文字列
                string cityText = jsonText.Substring(0, index);

                //int i = 0;

                // ユーザリストのデータをJson用テキストにする
                //foreach(User one in userList)
                //{
                //    one.num = (int)removeNumBoxList[i].Value;
                //    userText = userText + "\n        {\n            \"name\": \"" + one.name.ToString() + "\",\n            \"num\": " + one.num.ToString() + "\n        },";
                //    i++;
                //}

                // ユーザの情報をJson用テキストにする
                for(int i = 0; i < userNameBoxList.Count; i++)
                {
                    // 削除状態になっていない時だけ書き込む
                    if(userNameBoxList[i].Enabled == true)
                    {
                        userText = userText + "\n        {\n            \"name\": \"" + userNameBoxList[i].Text + "\",\n            \"num\": " + removeNumBoxList[i].Value + "\n        },";
                    }
                }

                userText = userText.Substring(0, userText.Length - 1);

                allText = cityText + userText + "\n    ]\n}";

                System.IO.Stream stream;
                stream = sfd.OpenFile();

                if(stream != null)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(stream, System.Text.Encoding.GetEncoding("Shift_JIS"));
                    sw.Write(allText);
                    sw.Close();
                    stream.Close();
                }

            }


            

        }

        // 書き込みボタンクリック
        private void WriteJson_Click(object sender, EventArgs e)
        {
            WriteJsonFile();
        }

        // 書き込みを許可
        private void AllowWrite()
        {
            WriteJson.Enabled = true;
        }

        // 書き込みを禁止
        private void ProhibitWrite()
        {
            WriteJson.Enabled = false;
        }


        // 削除ボタンのクリックイベントハンドラ
        private void DeleteButtonClick(object sender, EventArgs e)
        {
            //クリックされたボタンのインデックス番号を取得する
            int index = -1;
            for (int i = 0; i < deleteList.Count; i++)
            {
                if (deleteList[i].Equals(sender))
                {
                    index = i;
                    break;
                }
            }

            DeleteUser(index);
        }

        // ユーザ削除
        private void DeleteUser(int index)
        {
            if(deleteList[index].Text == "X")
            {
                userNameBoxList[index].Enabled = false;
                removeNumBoxList[index].Enabled = false;
                deleteList[index].Text = "C";
            }
            else
            {
                userNameBoxList[index].Enabled = true;
                removeNumBoxList[index].Enabled = true;
                deleteList[index].Text = "X";
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
