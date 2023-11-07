using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
namespace ChatApp
{
    public partial class Form2 : Form
    {
        public event EventHandler ButtonClicked;
        public Form2()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonClicked.Invoke(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClicked.Invoke(sender, e);
        }
    }
}
