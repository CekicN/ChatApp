using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp.ChatItems
{
    public partial class Incomming : UserControl
    {
        private string message;
        private Image avatar;

        public Incomming()
        {
            InitializeComponent();
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle(0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1));
                Region rg = new Region(gp);
                pictureBox1.Region = rg;
            }
        }

        public string Message
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
                AdjustHeight();
            }
        }

        void AdjustHeight()
        {
            pictureBox1.Location = new Point(4, 3);
            label1.Height = Utils.GetTextHeight(label1) + 10;
            siticoneCustomGradientPanel1.Height = label1.Top + siticoneCustomGradientPanel1.Top + label1.Height;
            this.Height = siticoneCustomGradientPanel1.Bottom + 10;
        }

        public Image Avatar { get => pictureBox1.Image; set => pictureBox1.Image = value; }

        private void Incomming_Resize(object sender, EventArgs e)
        {
            AdjustHeight();
        }

       
    }


}
