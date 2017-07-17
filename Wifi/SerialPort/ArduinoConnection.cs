using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace Wifi.Arduino
{
    class SerialPortConnection
    {
        private SerialPort port;



        public SerialPortConnection() {
            port = new SerialPort();
        }

        public SerialPortConnection(string withPortName) {
            port = new SerialPort(withPortName, 9600);
        }


        public string[] getAvailablePorts() {
            return SerialPort.GetPortNames();
        }

        public void setUpSerialPort(string portName, int baudRate) {
            port = new SerialPort();
        }

        public void openPortConnection(string portName) {
            port.Open();
        }

        public void closePortConnection(string portName)
        {
            port.Open();
        }

        private string readData(bool isBinary)
        {
            if (isBinary)
            {
                int count = port.BytesToRead;
                string data = "";
                while (count > 0)
                {
                    int byteSize = port.ReadByte();
                    data = data + Convert.ToChar(byteSize);
                    count--;
                }
                return data;
            }
            else
            {
                return "";
            }
        }

    }
}
