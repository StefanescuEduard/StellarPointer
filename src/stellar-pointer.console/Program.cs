using System;
using System.IO.Ports;

namespace StellarPointer.SerialWriter
{
    public class Program
    {
        public static void Main()
        {
            using (var serialPort = new SerialPort())
            {
                serialPort.PortName = "COM6";
                serialPort.BaudRate = 9600;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                serialPort.RtsEnable = true;

                serialPort.Open();

                bool canWrite = true;
                while (canWrite)
                {
                    string command = Console.ReadLine();
                    if (command == null)
                    {
                        continue;
                    }
                    if (command.Equals("quit"))
                    {
                        canWrite = false;
                    }
                    else
                    {
                        serialPort.WriteLine(command);
                    }
                }
            };
        }
    }
}