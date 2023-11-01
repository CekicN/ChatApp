namespace ChatApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.PictureBox();
            this.siticonePanel1 = new Siticone.Desktop.UI.WinForms.SiticonePanel();
            this.btnSend = new Siticone.Desktop.UI.WinForms.SiticoneImageButton();
            this.txtMessage = new Siticone.Desktop.UI.WinForms.SiticoneTextBox();
            this.siticoneSeparator1 = new Siticone.Desktop.UI.WinForms.SiticoneSeparator();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.outgoing1 = new ChatApp.ChatItems.Outgoing();
            this.incomming1 = new ChatApp.ChatItems.Incomming();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.siticonePanel1.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(22)))), ((int)(((byte)(32)))));
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 105);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Image = ((System.Drawing.Image)(resources.GetObject("closeButton.Image")));
            this.closeButton.Location = new System.Drawing.Point(323, 10);
            this.closeButton.Margin = new System.Windows.Forms.Padding(10);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(31, 30);
            this.closeButton.TabIndex = 0;
            this.closeButton.TabStop = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // siticonePanel1
            // 
            this.siticonePanel1.Controls.Add(this.btnSend);
            this.siticonePanel1.Controls.Add(this.txtMessage);
            this.siticonePanel1.Controls.Add(this.siticoneSeparator1);
            this.siticonePanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.siticonePanel1.Location = new System.Drawing.Point(0, 532);
            this.siticonePanel1.Name = "siticonePanel1";
            this.siticonePanel1.Size = new System.Drawing.Size(364, 84);
            this.siticonePanel1.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSend.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnSend.ImageRotate = 0F;
            this.btnSend.Location = new System.Drawing.Point(306, 18);
            this.btnSend.Name = "btnSend";
            this.btnSend.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSend.Size = new System.Drawing.Size(54, 54);
            this.btnSend.TabIndex = 2;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.BorderRadius = 20;
            this.txtMessage.BorderThickness = 2;
            this.txtMessage.DefaultText = "";
            this.txtMessage.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMessage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMessage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMessage.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMessage.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMessage.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMessage.Location = new System.Drawing.Point(12, 18);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.PasswordChar = '\0';
            this.txtMessage.PlaceholderText = "Enter Message";
            this.txtMessage.SelectedText = "";
            this.txtMessage.Size = new System.Drawing.Size(281, 54);
            this.txtMessage.Style = Siticone.Desktop.UI.WinForms.Enums.TextBoxStyle.Material;
            this.txtMessage.TabIndex = 1;
            // 
            // siticoneSeparator1
            // 
            this.siticoneSeparator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.siticoneSeparator1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(230)))));
            this.siticoneSeparator1.FillThickness = 2;
            this.siticoneSeparator1.Location = new System.Drawing.Point(0, 0);
            this.siticoneSeparator1.Name = "siticoneSeparator1";
            this.siticoneSeparator1.Size = new System.Drawing.Size(364, 12);
            this.siticoneSeparator1.TabIndex = 0;
            // 
            // panelContainer
            // 
            this.panelContainer.AutoScroll = true;
            this.panelContainer.Controls.Add(this.outgoing1);
            this.panelContainer.Controls.Add(this.incomming1);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 105);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(364, 427);
            this.panelContainer.TabIndex = 3;
            // 
            // outgoing1
            // 
            this.outgoing1.BackColor = System.Drawing.Color.Transparent;
            this.outgoing1.Dock = System.Windows.Forms.DockStyle.Top;
            this.outgoing1.Location = new System.Drawing.Point(0, 66);
            this.outgoing1.Message = "Hello";
            this.outgoing1.MinimumSize = new System.Drawing.Size(0, 66);
            this.outgoing1.Name = "outgoing1";
            this.outgoing1.Size = new System.Drawing.Size(364, 74);
            this.outgoing1.TabIndex = 1;
            // 
            // incomming1
            // 
            this.incomming1.Avatar = null;
            this.incomming1.BackColor = System.Drawing.Color.Transparent;
            this.incomming1.Dock = System.Windows.Forms.DockStyle.Top;
            this.incomming1.Location = new System.Drawing.Point(0, 0);
            this.incomming1.Message = "Hello";
            this.incomming1.Name = "incomming1";
            this.incomming1.Size = new System.Drawing.Size(364, 66);
            this.incomming1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(364, 616);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.siticonePanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatApp";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.siticonePanel1.ResumeLayout(false);
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private PictureBox closeButton;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Siticone.Desktop.UI.WinForms.SiticonePanel siticonePanel1;
        private Siticone.Desktop.UI.WinForms.SiticoneImageButton btnSend;
        private Siticone.Desktop.UI.WinForms.SiticoneTextBox txtMessage;
        private Siticone.Desktop.UI.WinForms.SiticoneSeparator siticoneSeparator1;
        private Panel panelContainer;
        private ChatItems.Incomming incomming1;
        private ChatItems.Outgoing outgoing1;
    }
}