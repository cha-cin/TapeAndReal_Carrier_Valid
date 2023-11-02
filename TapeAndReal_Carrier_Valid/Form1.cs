using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TapeAndReal_Carrier_Valid;
using System.Drawing;

namespace TapeAndReal_Carrier_Valid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // add 1 items to comboBox
            comboBox1.Items.Add("TAPE AND REEL");
            comboBox2.Items.Add("Valid");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show((string)comboBox1.SelectedItem);
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show((string)comboBox2.SelectedItem);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if ((string)textBox3.Text != "") { button1.Enabled = false; }
            else { button1.Enabled = true; }
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show((string)comboBox1.SelectedItem + " " + (string)comboBox2.SelectedItem + " "+ textBox1.Text);
            if ((string)textBox2.Text != "" && (string)textBox2.Text != "Username" && (string)textBox2.Text != "Wrong!!")
            {
                StreamWriter sw = new StreamWriter(@"C:\TapeAndReal_Carrier_Valid\log.txt", true);//set the log path
                sw.WriteLine(DateTime.Now.ToString("MM-dd-yyyy-hh:mm:ss") + " " + "Username : " + (string)textBox2.Text); // Insert the controll user.
                sw.Close();
                await comman.SendMipcCommandAsync((string)textBox2.Text, (string)comboBox1.SelectedItem, (string)comboBox2.SelectedItem, textBox1.Text);
                await comman.SendEmailAsync((string)textBox2.Text, (string)comboBox1.SelectedItem, (string)comboBox2.Text, (string)textBox1.Text.Replace(" ", ""));
            }
            else {
                textBox2.ForeColor = Color.Red;
                textBox2.Text = "Wrong!!";
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if ((string)textBox3.Text != "") 
            {
                StreamWriter sw = new StreamWriter(@"C:\TapeAndReal_Carrier_Valid\log.txt", true);//set the log path
                sw.WriteLine(DateTime.Now.ToString("MM-dd-yyyy-hh:mm:ss") + " " + "Username : " + (string)textBox2.Text); // Insert the controll user.
                sw.Close();
                await comman.SendMipcCommand_SubstrateLotAssociation((string)textBox2.Text, (string)textBox1.Text, (string)textBox3.Text);
                await comman.SendEmailAsync_SubstrateLotAssociation((string)textBox2.Text, (string)textBox1.Text, (string)textBox3.Text);
            }
        }
    }
}
