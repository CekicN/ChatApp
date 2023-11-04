namespace ChatApp.ChatItems
{
    partial class Outgoing
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.siticoneCustomGradientPanel1 = new Siticone.Desktop.UI.WinForms.SiticoneCustomGradientPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.siticoneCustomGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // siticoneCustomGradientPanel1
            // 
            this.siticoneCustomGradientPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.siticoneCustomGradientPanel1.BorderRadius = 20;
            this.siticoneCustomGradientPanel1.Controls.Add(this.label1);
            this.siticoneCustomGradientPanel1.FillColor = System.Drawing.Color.Blue;
            this.siticoneCustomGradientPanel1.FillColor2 = System.Drawing.Color.DeepSkyBlue;
            this.siticoneCustomGradientPanel1.FillColor4 = System.Drawing.Color.DodgerBlue;
            this.siticoneCustomGradientPanel1.Location = new System.Drawing.Point(101, 10);
            this.siticoneCustomGradientPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.siticoneCustomGradientPanel1.Name = "siticoneCustomGradientPanel1";
            this.siticoneCustomGradientPanel1.Size = new System.Drawing.Size(201, 99);
            this.siticoneCustomGradientPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 77);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hello";
            // 
            // Outgoing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.siticoneCustomGradientPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Outgoing";
            this.Size = new System.Drawing.Size(315, 125);
            this.DockChanged += new System.EventHandler(this.Outgoing_DockChanged);
            this.Resize += new System.EventHandler(this.Incomming_Resize);
            this.siticoneCustomGradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Siticone.Desktop.UI.WinForms.SiticoneCustomGradientPanel siticoneCustomGradientPanel1;
        private Label label1;
    }
}
