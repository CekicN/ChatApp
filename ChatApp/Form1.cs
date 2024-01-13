using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
namespace ChatApp
{
    public partial class Form1 : Form
    {
        private bool mouseIsDown = false;
        private Point firstPoint;
        private Form2 f2;

        //za poruke
        private TcpClient client;
        public StreamWriter STW;
        public StreamReader STR;
        public string receive;

        //za fajlove
        private TcpClient fileClient;
        public StreamReader fileSTR;
        public StreamWriter fileSTW;
        string t, n, rec, fileName;
        byte[] data;
        int size;

        TcpClient file;
        NetworkStream stream;

        private Bifid bf;
        private OFB rc;
        private byte[] key;
        private string iv;

        public Form1()
        {
            InitializeComponent();
            f2 = new Form2();
            f2.ButtonClicked += f2_buttonClicked;
            f2.ShowDialog();

            key = Encoding.UTF8.GetBytes("dnqtwjgqutlnubog");
            iv = "qyvqlvmrbcyxgjsz";
            bf = new Bifid();
            rc = new OFB(key);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void f2_buttonClicked(object sender, EventArgs e)
        {
            
            Button clickedBtn = (Button)sender;
            f2.Close();
            if (clickedBtn.Name == "button1")
            {
                //server
                TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);
                listener.Start();

                client = listener.AcceptTcpClient();
                STR = new StreamReader(client.GetStream(), Encoding.UTF8);
                STW = new StreamWriter(client.GetStream(), Encoding.UTF8);
                STW.AutoFlush = true;

                TcpListener fileListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5050);
                fileListener.Start();
                fileClient = fileListener.AcceptTcpClient();
                fileSTR = new StreamReader(fileClient.GetStream(), Encoding.UTF8);
                fileSTW = new StreamWriter(fileClient.GetStream(), Encoding.UTF8);
                fileSTW.AutoFlush = true;

                TcpListener fileLis = new TcpListener(IPAddress.Parse("127.0.0.1"), 5055);
                fileLis.Start();
                file = fileLis.AcceptTcpClient();
                stream = file.GetStream();


                backgroundWorker1.RunWorkerAsync();
                receiveFile.RunWorkerAsync();
                backgroundWorker2.WorkerSupportsCancellation = true;
            }
            else if (clickedBtn.Name == "button2")
            {
                //client
                client = new TcpClient();
                IPEndPoint IpEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);

                try
                {
                    client.Connect(IpEnd);
                    STR = new StreamReader(client.GetStream(), Encoding.UTF8);
                    STW = new StreamWriter(client.GetStream(), Encoding.UTF8);
                    STW.AutoFlush = true;


                    fileClient = new TcpClient();
                    fileClient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5050));
                    fileSTR = new StreamReader(fileClient.GetStream(), Encoding.UTF8);
                    fileSTW = new StreamWriter(fileClient.GetStream(), Encoding.UTF8);
                    fileSTW.AutoFlush = true;

                    file = new TcpClient();
                    file.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5055));
                    stream = file.GetStream();

                    backgroundWorker1.RunWorkerAsync();
                    receiveFile.RunWorkerAsync();
                    backgroundWorker2.WorkerSupportsCancellation = true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                int x = this.Location.X - xDiff;
                int y = this.Location.Y - yDiff;
                this.Location = new Point(x, y);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            backgroundWorker2.RunWorkerAsync();
        }

        void send()
        {
            if (txtMessage.Text.Trim().Length == 0) return;
            string cryptMsg = "";

            if (BifidBtn.Checked)
                cryptMsg = bf.Encrypt(txtMessage.Text);
            else if (RCBtn.Checked)
                cryptMsg = rc.Encrypt(txtMessage.Text, iv);

            addOutgoing(txtMessage.Text);
            STW.WriteLine(cryptMsg);
            txtMessage.Text = string.Empty;
        }

        void sendFileInfo()
        {
            if (n.Trim().Length == 0) return;

            fileSTW.WriteLine(n);
            fileName = n.Substring(0, n.IndexOf('|'));
            addOutgoing(fileName);
        }
        void sendFile()
        {

            data = File.ReadAllBytes(t);

            int bytesSent = 0;
            int bytesLeft = data.Length;

            int bufferSize = 1024;

            while(bytesLeft > 0)
            {
                int curDataSize = Math.Min(bufferSize, bytesLeft);

                stream.Write(data, bytesSent, curDataSize);

                bytesSent += curDataSize;
                bytesLeft -= curDataSize;
            }
            
            file.Close();
        }

        void ReceiveFile()
        {
            int bytesLeft = size;
            byte[] data = new byte[size];

            int bufferSize = 1024;
            int bytesRead = 0;

            while (bytesLeft > 0)
            {
                int curDataSize = Math.Min(bufferSize, bytesLeft);
                if (file.Available < curDataSize)
                    curDataSize = file.Available;

                stream.Read(data, bytesRead, curDataSize);

                bytesRead += curDataSize;
                bytesLeft -= curDataSize;
            }

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    File.WriteAllBytes(fbd.SelectedPath + "\\" +  fileName, data);
                }
            }
        }
        void addIncomming(string msg)
        {
            var bubble = new ChatItems.Incomming();
            panelContainer.Controls.Add(bubble);
            bubble.BringToFront();
            bubble.Dock = DockStyle.Top;
            bubble.Message = msg;
        }
        void addOutgoing(string msg)
        {
            var bubble = new ChatItems.Outgoing();
            panelContainer.Controls.Add(bubble);
            bubble.BringToFront();
            bubble.Dock = DockStyle.Top;
            bubble.Message = msg;
        }

        private void txtMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while(client.Connected)
            {
                try
                {
                    receive = STR.ReadLine();
                    if(!string.IsNullOrEmpty(receive))
                    {
                        panelContainer.Invoke((MethodInvoker)delegate ()
                        {
                            if(rjToggleButton1.Checked)
                            {
                                addIncomming(receive);
                            }

                            if (BifidBtn.Checked)
                                receive = bf.Decrypt(receive);
                            else if (RCBtn.Checked)
                                receive = rc.Decrypt(receive, iv);
                            addIncomming(receive);

                        });
                    }
                    receive = "";

                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if(client.Connected)
            {
                if(txtMessage.Text.Trim().Length != 0)
                {
                    panelContainer.Invoke((MethodInvoker)delegate ()
                    {
                        send();
                    });
                }
            }
            else
            {
                MessageBox.Show("Failed");
            }


            if (fileClient.Connected)
            {
                if (n.Trim().Length != 0)
                {
                    panelContainer.Invoke((MethodInvoker)delegate ()
                    {
                        sendFileInfo();
                        sendFile();
                    });
                }
            }
            else
            {
                MessageBox.Show("Failed");
            }

            backgroundWorker2.CancelAsync();
        }

        private void receiveFile_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (fileClient.Connected)
            {
                try
                {
                    rec = fileSTR.ReadLine();
                    if (!string.IsNullOrEmpty(rec))
                    {
                        panelContainer.Invoke((MethodInvoker)delegate ()
                        {
                            size = int.Parse(rec.Substring(rec.LastIndexOf("|") + 1));
                            fileName = rec.Substring(0, rec.IndexOf('|'));
                            addIncomming(fileName);

                            ReceiveFile();
                        });
                    }
                    rec = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void rjToggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if(op.ShowDialog() == DialogResult.OK)
            {
                t = op.FileName;//cela putanja
                FileInfo fi = new FileInfo(op.FileName);
                n = fi.Name + "|" + fi.Length;

                backgroundWorker2.RunWorkerAsync();
            }

        }
    }
}