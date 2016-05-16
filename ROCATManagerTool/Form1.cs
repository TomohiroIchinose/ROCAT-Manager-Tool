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
        List<User> userList = new List<User>();                         // ユーザのリスト

        List<TextBox> userNameBoxList = new List<TextBox>();            // ユーザの名前が入ったテキストボックスのリスト

        List<NumericUpDown> removeNumBoxList = new List<NumericUpDown>();  // ユーザの除去数の入ったボックスのリスト

        List<Button> deleteList = new List<Button>();                   // ユーザ削除用のボタンのリスト

        List<User> tempUserList = new List<User>();     // 新規Jsonファイル作成時に既に同名ファイルがあった時にユーザを保持しておくためのリスト

        String defaultDir = "C:\\";     // ファイル読み込みの時に最初にアクセスするディレクトリ

        String fileName;    // 読み込んだJsonファイルのなまえ

        String jsonText;    // 読み込んだJsonファイルの全部の内容


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

            // 読み込み時のユーザが0でないとき
            if(usernum != 0)
            {
                newUserName.Location = new Point(31, userNameBoxList[usernum - 1].Location.Y + 38);
                newNum.Location = new Point(298, removeNumBoxList[usernum - 1].Location.Y + 38);
                newDelete.Location = new Point(479, deleteList[usernum - 1].Location.Y + 38);
            }
            else
            {
                newUserName.Location = new Point(31, 5);
                newNum.Location = new Point(298, 5);
                newDelete.Location = new Point(479, 5);
            }
            

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
                        // フォームのタイトルを書き替える
                        this.Text = "ROCAT Manager Tool - " + fileName;

                        // "SATDRanking"のタグがあるトコのインデックス（タグの次の[の場所）を設定
                        int index = text.IndexOf("\"SATDRanking\": ") + 15;


                        // SATDRankingが見つかった時
                        if (index != 14)
                        {

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
                                                       
                        }

                        // ユーザ追加と書き込みを許可
                        AllowAddUser();
                        AllowWrite();

                    }
                }

                s.Close();
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

                string cityText = "";

                // SATDRankingが見つかった時
                if (index != 15)
                {
                    // 元のJsonファイルの最初からインデックスのところまでの文字列
                    cityText = jsonText.Substring(0, index);
                }
                // SATDRankingが見つからなかったとき（ランキング情報登録がはじめてのJsonファイル）
                else
                {
                    // 元のJsonファイルの最初から最後の方の]まで取ってきてそこにランキング情報を追加する
                    index = jsonText.Length - 3;

                    cityText = jsonText.Substring(0, index) + ",\n    \"SATDRanking\": [";
                }

                

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

                // ユーザが0じゃないときだけテキストを調整
                if(userText.Length != 0)
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


        // 街のJsonファイルをつくる
        private void MakeCity_Click(object sender, EventArgs e)
        {
            Boolean flag = false;

            //FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = "Select Git Repository";

            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;

            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
                fbd.SelectedPath = @"C:";

            //ユーザーが新しいフォルダを作成できるようにしない
                fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                int find = fbd.SelectedPath.LastIndexOf("\\");

                string jsonFileName = ".\\Results\\" + fbd.SelectedPath.Substring(find + 1, fbd.SelectedPath.Length - find - 1) + ".json";


                // 既に同名ファイルが存在するかチェック
                if (File.Exists(jsonFileName))
                {
                    SaveTempUser(jsonFileName);
                    flag = true;

                }


                string checkedLang = "";

                if (JavaRadio.Checked)
                {
                    checkedLang = "\"java\"";
                }
                else if (PythonRadio.Checked)
                {
                    checkedLang = "\"py\"";
                }
                else if (RubyRadio.Checked)
                {
                    checkedLang = "\"rb\"";
                }

                System.Diagnostics.Process pro = new System.Diagnostics.Process();

                pro.StartInfo.FileName = "python";            // コマンド名
                pro.StartInfo.Arguments = ".\\Scripts\\project-analyzer.py " + fbd.SelectedPath.ToString() + " " + checkedLang + " " + jsonFileName;               // 引数
                //pro.StartInfo.Arguments = ".\\Scripts\\project-analyzer.py \"C:\\Users\\Ichinose\\guice\" \"java\" \"testcity.json\"";               // 引数
                pro.StartInfo.CreateNoWindow = true;            // DOSプロンプトの黒い画面を非表示
                pro.StartInfo.UseShellExecute = false;          // プロセスを新しいウィンドウで起動するか否か
                pro.StartInfo.RedirectStandardOutput = true;    // 標準出力をリダイレクトして取得したい

                Form2 frm = new Form2();
                frm.Show();

                pro.Start();

                string output = pro.StandardOutput.ReadToEnd();
                output.Replace("\r\r\n", "\n"); // 改行コード変換

                //プロセス終了まで待機する
                //WaitForExitはReadToEndの後である必要がある
                //(親プロセス、子プロセスでブロック防止のため)
                pro.WaitForExit();
                pro.Close();
                frm.Close();


                // ファイルがちゃんとつくれたかチェック
                if(File.Exists(jsonFileName))
                {
                    MessageBox.Show("Success!");
                }
                else
                {
                    MessageBox.Show("Failed!");
                }

                if (flag == true)
                    WriteTempUser(jsonFileName);

                
                
            }

            //MessageBox.Show(output);


            /*
            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                Console.WriteLine(fbd.SelectedPath);


                if (JavaRadio.Checked)
                {
                    Console.WriteLine(JavaRadio.Text);
                }
                else if(PythonRadio.Checked)
                {
                    Console.WriteLine(PythonRadio.Text);
                }
                else if(RubyRadio.Checked)
                {
                    Console.WriteLine(RubyRadio.Text);
                }


                ScriptEngine engine = Python.CreateEngine();
                ScriptScope scope = engine.ExecuteFile("Scripts/project-analyzer.py");
                var result =
                    (int)engine.Operations.InvokeMember(scope, "main", "C:\\Users\\Ichinose\\guice", "java");

                System.Console.Write(result);
            }
            */

        }


        // 同名ファイルが存在していた時にユーザ情報を保持するための関数
        private void SaveTempUser(string existFileName)
        {
            // Jsonファイルを読み込む
            var text = File.ReadAllText(existFileName, System.Text.Encoding.GetEncoding("Shift_JIS"));

            // 街のJsonファイルじゃない場合はメッセージを出す
            if (text.IndexOf("\"buildings\":") == -1)
            {
                MessageBox.Show("The file is not ROCAT file!!");
            }
            else
            {

                // "SATDRanking"のタグがあるトコのインデックス（タグの次の[の場所）を設定
                int index = text.IndexOf("\"SATDRanking\": ") + 15;


                // SATDRankingが見つかった時
                if (index != 14)
                {

                    // ランキング部分だけの文字列を作る（index～最後の}より1文字分前までがランキング部分）
                    var newtext = text.Substring(index, text.Length - index - 1);

                    // ランキング部分だけの文字列からデータを読み出す
                    var list = JsonConvert.DeserializeObject<List<User>>(newtext);


                    // tempUserListにユーザを残しとく
                    foreach (var item in list)
                    {
                        Console.WriteLine("Name: {0}, Num: {1}", item.name, item.num);
                        tempUserList.Add(item);

                    }


                }

            }
        }


        // tempUserListにあるユーザを生成したJsonファイルに書き込む
        private void WriteTempUser(string newFileName)
        {
            // 新しいJsonファイルを読み込む
            var text = File.ReadAllText(newFileName, System.Text.Encoding.GetEncoding("Shift_JIS"));
            string newJsonText = text;


            // tempUserListのデータを文字列に変換
            string userText = "";

            for(int i = 0; i < tempUserList.Count; i++)
            {
               userText = userText + "\n        {\n            \"name\": \"" + tempUserList[i].name + "\",\n            \"num\": " + tempUserList[i].num.ToString() + "\n        },";
            }

            // ユーザが0じゃないときだけテキストを調整
            if (userText.Length != 0)
                userText = userText.Substring(0, userText.Length - 1);

            // 街の部分の文字列を作る
            string cityText = "";
            int index = newJsonText.Length - 3;

            cityText = newJsonText.Substring(0, index) + ",\n    \"SATDRanking\": [";

            // 文字列全体
            string allText = cityText + userText + "\n    ]\n}";

            // 新しいファイルに書き込み
            System.IO.StreamWriter sw = new StreamWriter(newFileName, false, Encoding.GetEncoding("Shift_JIS"));
            sw.Write(allText);
            sw.Close();

            // tempUserListをクリア
            tempUserList.Clear();
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
