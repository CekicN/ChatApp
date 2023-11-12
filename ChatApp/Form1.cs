using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatApp
{
    public partial class Form1 : Form
    {
        private bool mouseIsDown = false;
        private Point firstPoint;
        private Form2 f2;

        private TcpClient client;
        public StreamWriter STW;
        public StreamReader STR;
        public string receive;
        public string TextToSend;

        private Bifid bf;
        public Form1()
        {
            InitializeComponent();
            f2 = new Form2();
            f2.ButtonClicked += f2_buttonClicked;
            f2.ShowDialog();

            bf = new Bifid();
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
                backgroundWorker1.RunWorkerAsync();
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
                    backgroundWorker1.RunWorkerAsync();
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

            string cryptMsg = bf.Encrypt(txtMessage.Text);
            addOutgoing(txtMessage.Text);
            STW.WriteLine(cryptMsg);
            txtMessage.Text = string.Empty;
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
                            receive = bf.Decrypt(receive);
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

            backgroundWorker2.CancelAsync();
        }
    }
}