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
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("テストで～す");
            MakeUserForms(20);
        }

        public void MakeUserForms(int num)
        {
            List<TextBox> userName = new List<TextBox>();
            List<NumericUpDown> removeNum = new List<NumericUpDown>();

            int y = 78;

            System.Diagnostics.Trace.WriteLine("テストで～す");

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

        
    }
}
