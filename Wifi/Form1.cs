using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using Wifi.utils;
namespace Wifi
{
    public partial class Form1 : Form
    {
        String text;
        private const String DATA = "d:";
        private const String LOG = "log";
        private const int OBJ_SIZE = 5;

        SerialPort port;
        Serial s;
        Output o;
        List<char> l;
        char[] sendTxt;
        int byteSize;
        
        public Form1()
        {
            InitializeComponent();
        }

        public void sendData(char i,string text) {
            l = new List<char>();
            l.Add(i);
            sendTxt = text.ToCharArray();
            foreach (char x in sendTxt) {
                l.Add(x);
            }
            sendTxt = l.ToArray<char>();
            Console.WriteLine(sendTxt);
            port.Write(text);
        }

        private void wifiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void readData() {
            port.DataReceived += arduinoBoard_DataReceived;
                //int count = port.BytesToRead;
                //string data = "";
                //while (count > 0)
                //{
                //    byteSize = port.ReadByte();
                //    data = data + Convert.ToChar(byteSize);
                //    count--;
                //}
        }
        delegate void SetTextCallback(string text);

        //Captura los datos del arduino
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (text.Contains(DATA))
                {
                    string[] tokens = text.Split(':');
                    string data = tokens[1];
                    //size tiene el tamaño del objeto
                    int size = Int32.Parse(data);
                    if (size >= OBJ_SIZE)
                    {
                        //Este metodo pinta de color el texto y lo escribe en el textview
                        //Rojo si es mayor o igual que OBJ_SIZE (5) por que esta muy grande
                        RichTextBoxExtensions.AppendText(richTextBox1, text, Color.DarkRed);
                        DateTime time = new DateTime();
                        time = DateTime.Now;
                        bool isGoodBox = true;
                    }
                    else {
                        //Verde si es menor que OBJ_SIZE (5) por que esta bien el tamano
                        RichTextBoxExtensions.AppendText(richTextBox1, text, Color.Green);
                        DateTime time = new DateTime();
                        time = DateTime.Now;
                        bool isGoodBox = false;
                    }
                }
                else if (text.Contains(LOG))
                {
                    RichTextBoxExtensions.AppendText(richTextBox1, text, Color.OrangeRed);
                }else
                {
                    RichTextBoxExtensions.AppendText(richTextBox1, text, Color.Black);
                }
            }
        }

        void arduinoBoard_DataReceived(object sender, SerialDataReceivedEventArgs e){
            int count = port.BytesToRead;
            string data = "";
            while (count > 0)
            {
                byteSize = port.ReadByte();
                data = data + Convert.ToChar(byteSize);
                count--;
            }
            SetText(data);

        }

        private void serialPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s = new Serial();
            s.button1.Click += new System.EventHandler(this.button1_Click);
            s.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string p = s.listBox1.SelectedItem.ToString();
            try {
                port = new SerialPort(p, 9600);
                port.Open();
                readData();
                s.label1.BackColor = Color.Green;
                this.label2.Text = "CONNECTED";
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            port.Close();
            s.label1.BackColor = Color.Red;
            this.label2.Text = "DISCONNECTED";
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aprovedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void disaprovedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clrButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
