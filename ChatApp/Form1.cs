namespace ChatApp
{
    public partial class Form1 : Form
    {
        private bool mouseIsDown = false;
        private Point firstPoint;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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
            if(txtMessage.Text != "")
            {
                addOutgoing(txtMessage.Text);
                txtMessage.Text = string.Empty;
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
    }
}