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


namespace Wifi
{
    public partial class Serial : Form
    {
        SerialPort currentPort;
        bool portFound;


        public Serial()
        {
            InitializeComponent();
        }

 
        private void Serial_Load(object sender, EventArgs e)
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    listBox1.Items.Add(port);
                }
            }
            catch (Exception a)
            {
                Console.Write(a.Message);
            }

        }


    }
}
