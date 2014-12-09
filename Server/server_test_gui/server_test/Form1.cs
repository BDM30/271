using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label_version_api.Text = "Api version 0.4";
            label_email.Text = "email";
            label_password.Text = "password";
            label_name.Text = "name";
            label_x.Text = "x";
            label_y.Text = "y";
            label_ip.Text = "ip";
            label_result.Text = "result";

            textBox_email.Text = "starson4587@gmail.com";
            textBox_password.Text = "koolherc";
            textBox_name.Text = "test1";
            textBox_x.Text = "3";
            textBox_y.Text = "4";
            textBox_ip.Text = "http://localhost:11000/";

            button_registration.Text = "registration";
            button_entrance.Text = "entrance";
            button_remind.Text = "remind";
            button_add_note.Text = "add note";
            button_get_note.Text = "get notes";
        }

        private void button_get_note_Click(object sender, EventArgs e)
        {
            richTextBox_result.Text = "";
            string begin = textBox_ip.Text;
            string query = "{\"function\":\"get_notes\",\"email\":\"" + textBox_email.Text + "\"}";
            richTextBox_result.AppendText("Query:\n" + begin + query);
            richTextBox_result.AppendText("\nAnswer:\n" + QuerySender.GET(begin, query));
        }

        private void button_registration_Click(object sender, EventArgs e)
        {
            richTextBox_result.Text = "";
            string begin = textBox_ip.Text;
            string query = "{\"function\":\"registration\",\"email\":\"" + textBox_email.Text + "\",\"password\":\"" + textBox_password.Text + "\"}";
            richTextBox_result.AppendText("Query:\n"+begin + query);
            richTextBox_result.AppendText("\nAnswer:\n" + QuerySender.GET(begin, query));
        }

        private void button_entrance_Click(object sender, EventArgs e)
        {
            richTextBox_result.Text = "";
            string begin = textBox_ip.Text;
            string query = "{\"function\":\"entrance\",\"email\":\"" + textBox_email.Text + "\",\"password\":\"" + textBox_password.Text + "\"}";
            richTextBox_result.AppendText("Query:\n" + begin + query);
            richTextBox_result.AppendText("\nAnswer:\n" + QuerySender.GET(begin, query));
        }

        private void button_remind_Click(object sender, EventArgs e)
        {
            richTextBox_result.Text = "";
            string begin = textBox_ip.Text;
            string query = "{\"function\":\"remind\",\"email\":\"" + textBox_email.Text + "\"}";
            richTextBox_result.AppendText("Query:\n" + begin + query);
            richTextBox_result.AppendText("\nAnswer:\n" + QuerySender.GET(begin, query));
        }

        private void button_add_note_Click(object sender, EventArgs e)
        {
            richTextBox_result.Text = "";
            string begin = textBox_ip.Text;

            string query = "{\"function\":\"add_note\",\"name\":\"" + textBox_name.Text +
                "\",\"user\":\"" + textBox_email.Text + "\",\"x\":" + textBox_x.Text + ",\"y\":" + textBox_y.Text + "}";
            richTextBox_result.AppendText("Query:\n" + begin + query);
            richTextBox_result.AppendText("\nAnswer:\n" + QuerySender.GET(begin, query));
        }
    }
}
