namespace ROCATManagerTool
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.AddUserButton = new System.Windows.Forms.Button();
            this.NewUserNameForm = new System.Windows.Forms.TextBox();
            this.ReadJson = new System.Windows.Forms.Button();
            this.WriteJson = new System.Windows.Forms.Button();
            this.UserName = new System.Windows.Forms.Label();
            this.RemoveName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Line2 = new System.Windows.Forms.Label();
            this.Line1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddUserButton
            // 
            this.AddUserButton.Location = new System.Drawing.Point(331, 527);
            this.AddUserButton.Name = "AddUserButton";
            this.AddUserButton.Size = new System.Drawing.Size(75, 23);
            this.AddUserButton.TabIndex = 0;
            this.AddUserButton.Text = "ユーザ追加";
            this.AddUserButton.UseVisualStyleBackColor = true;
            // 
            // NewUserNameForm
            // 
            this.NewUserNameForm.Location = new System.Drawing.Point(45, 529);
            this.NewUserNameForm.Name = "NewUserNameForm";
            this.NewUserNameForm.Size = new System.Drawing.Size(215, 19);
            this.NewUserNameForm.TabIndex = 2;
            // 
            // ReadJson
            // 
            this.ReadJson.Location = new System.Drawing.Point(544, 358);
            this.ReadJson.Name = "ReadJson";
            this.ReadJson.Size = new System.Drawing.Size(125, 23);
            this.ReadJson.TabIndex = 3;
            this.ReadJson.Text = "Json読み込み";
            this.ReadJson.UseVisualStyleBackColor = true;
            // 
            // WriteJson
            // 
            this.WriteJson.Location = new System.Drawing.Point(544, 429);
            this.WriteJson.Name = "WriteJson";
            this.WriteJson.Size = new System.Drawing.Size(125, 23);
            this.WriteJson.TabIndex = 4;
            this.WriteJson.Text = "Json書き出し";
            this.WriteJson.UseVisualStyleBackColor = true;
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Location = new System.Drawing.Point(115, 16);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(43, 12);
            this.UserName.TabIndex = 7;
            this.UserName.Text = "おなまえ";
            // 
            // RemoveName
            // 
            this.RemoveName.AutoSize = true;
            this.RemoveName.Location = new System.Drawing.Point(337, 16);
            this.RemoveName.Name = "RemoveName";
            this.RemoveName.Size = new System.Drawing.Size(41, 12);
            this.RemoveName.TabIndex = 8;
            this.RemoveName.Text = "除去数";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.Line1);
            this.panel1.Controls.Add(this.Line2);
            this.panel1.Controls.Add(this.RemoveName);
            this.panel1.Controls.Add(this.UserName);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(504, 501);
            this.panel1.TabIndex = 7;
            // 
            // Line2
            // 
            this.Line2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Line2.Location = new System.Drawing.Point(332, 28);
            this.Line2.Name = "Line2";
            this.Line2.Size = new System.Drawing.Size(50, 1);
            this.Line2.TabIndex = 8;
            // 
            // Line1
            // 
            this.Line1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Line1.Location = new System.Drawing.Point(111, 28);
            this.Line1.Name = "Line1";
            this.Line1.Size = new System.Drawing.Size(50, 1);
            this.Line1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 565);
            this.Controls.Add(this.WriteJson);
            this.Controls.Add(this.ReadJson);
            this.Controls.Add(this.NewUserNameForm);
            this.Controls.Add(this.AddUserButton);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "ROCAT Manager Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddUserButton;
        private System.Windows.Forms.TextBox NewUserNameForm;
        private System.Windows.Forms.Button ReadJson;
        private System.Windows.Forms.Button WriteJson;
        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.Label RemoveName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Line2;
        private System.Windows.Forms.Label Line1;
    }
}

