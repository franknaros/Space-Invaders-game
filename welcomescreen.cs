using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UbatSpill
{
    public partial class welcomescreen : Form
    {


        public static string NAVN;

        public welcomescreen()
        {
            InitializeComponent();
           
        }


        public void btn_Start_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            welcomescreen wc = new welcomescreen();
            this.Hide();
            try
            {
                if (NAVN.Length != 0);
               
            }
            catch { MessageBox.Show("Navn ikke utfyllt. Start spillet på nytt");
                Application.Exit();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_How_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file://C:\\Users\\Franknaros\\Dropbox\\UBAT SPILL\\bin\\Debug\\Bruker Manual.chm");

        }

        public void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            NAVN = this.textBox1.Text;
            
            }
        }
    }

