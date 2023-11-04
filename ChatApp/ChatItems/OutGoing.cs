using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp.ChatItems
{
    public partial class Outgoing : UserControl
    {
        private string message;
        private Image avatar;

        public Outgoing()
        {
            InitializeComponent();
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
            label1.Height = Utils.GetTextHeight(label1) + 10;
            siticoneCustomGradientPanel1.Height = label1.Top + siticoneCustomGradientPanel1.Top + label1.Height;
            this.Height = siticoneCustomGradientPanel1.Bottom + 10;
        }


        private void Incomming_Resize(object sender, EventArgs e)
        {
            AdjustHeight();
        }

        private void Outgoing_DockChanged(object sender, EventArgs e)
        {
            AdjustHeight();
        }
    }


}
