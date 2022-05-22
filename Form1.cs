using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace FinalProject5
{
    public partial class Form1 : Form
    {

        Task task1;
        Server server;


        public Form1()
        {
            server = new Server();
            InitializeComponent();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            int chickCount = Convert.ToInt32(txtCh.Text);
            int eggCount = Convert.ToInt32(txtEgg.Text);
            string customer = textBox1.Text;
            string drink = comboBox1.Text;
            if (server.service.Values.Where(i => i.InProccess == true).Count() == server.service.Values.Count)
            {

                while (server.CustomerCount != 8)
                {
                    button1.Enabled = false;
                    await Task.Delay(25);
                }
            }
            button1.Enabled = true;


            if (string.IsNullOrEmpty(customer))
            {
                MessageBox.Show("please enter customer name");
                return;
            }


            lock (server)
            {
                task1 = Task.Run(() => server.Receive(customer, chickCount, eggCount, drink));
            }
            if (server.CustomerCount == 8)
                button1.Enabled = false;
            button2.Enabled = true;
            string s = "Customer " + customer + " ordered ";
            if (chickCount > 0)
                s += "chicken " + chickCount;
            if (eggCount > 0)
                s += ", egg " + eggCount;
            s += " " + drink;
            listBox1.Items.Add(s);

        }

        //TODO: I ordered and sent 2 times and got this exception.
        private async void button2_Click(object sender, EventArgs e)
        {
            List<string> items;
            string eggQuality;

            await Task.Delay(2500);
            lock (server)
            {
                Task task2 = task1.ContinueWith(server.Send);
                (items, eggQuality) = task2.ContinueWith(server.Serve).Result.Result;
            }
            await Task.Delay(2500);
            listBox1.Items.Clear();
            foreach (var item in items)
            {
                listBox1.Items.Add(item);
            }
            label5.Text = eggQuality;
            button2.Enabled = false;
            button1.Enabled = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
