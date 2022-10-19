using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Socket_ServerNumeroCasuale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] bytes = new byte[1024];
            int num;
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                // Create a TCP/IP  socket.  
                Socket send = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    send.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        send.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes("dammi un numero<EOF>");

                    // Send the data through the socket.  
                    int bytesSent = send.Send(msg);

                    // Receive the response from the remote device.  
                    int bytesRec = send.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    textBox1.Text = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    // Release the socket.  
                    send.Shutdown(SocketShutdown.Both);
                    send.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception g)
                {
                    Console.WriteLine("Unexpected exception : {0}", g.ToString());
                }

            }
            catch (Exception a)
            {
                Console.WriteLine(a.ToString());
            }
        }
    }
}
