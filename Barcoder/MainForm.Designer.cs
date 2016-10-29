namespace Barcoder
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEncode = new System.Windows.Forms.Button();
            this.cboFormat = new System.Windows.Forms.ComboBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.picOutput = new System.Windows.Forms.PictureBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cboModulo = new System.Windows.Forms.ComboBox();
            this.cboWeightType = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(422, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // cmdEncode
            // 
            this.cmdEncode.Location = new System.Drawing.Point(243, 69);
            this.cmdEncode.Name = "cmdEncode";
            this.cmdEncode.Size = new System.Drawing.Size(80, 28);
            this.cmdEncode.TabIndex = 5;
            this.cmdEncode.Text = "&Encode";
            this.cmdEncode.UseVisualStyleBackColor = true;
            this.cmdEncode.Click += new System.EventHandler(this.cmdEncode_Click);
            // 
            // cboFormat
            // 
            this.cboFormat.FormattingEnabled = true;
            this.cboFormat.Location = new System.Drawing.Point(261, 42);
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.Size = new System.Drawing.Size(148, 21);
            this.cboFormat.TabIndex = 1;
            this.cboFormat.SelectedIndexChanged += new System.EventHandler(this.cboFormat_SelectedIndexChanged);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 42);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(243, 20);
            this.txtMessage.TabIndex = 0;
            // 
            // picOutput
            // 
            this.picOutput.BackColor = System.Drawing.Color.White;
            this.picOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOutput.Location = new System.Drawing.Point(12, 105);
            this.picOutput.Name = "picOutput";
            this.picOutput.Size = new System.Drawing.Size(397, 154);
            this.picOutput.TabIndex = 4;
            this.picOutput.TabStop = false;
            // 
            // cmdSave
            // 
            this.cmdSave.Enabled = false;
            this.cmdSave.Location = new System.Drawing.Point(329, 69);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(80, 28);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "bmp";
            this.saveFileDialog1.Filter = "Bitmap files|*.bmp|GIF files|*.gif|Jpeg files|*.jpg";
            // 
            // cboModulo
            // 
            this.cboModulo.FormattingEnabled = true;
            this.cboModulo.Location = new System.Drawing.Point(12, 69);
            this.cboModulo.Name = "cboModulo";
            this.cboModulo.Size = new System.Drawing.Size(121, 21);
            this.cboModulo.TabIndex = 3;
            this.cboModulo.Visible = false;
            this.cboModulo.SelectedIndexChanged += new System.EventHandler(this.cboModulo_SelectedIndexChanged);
            // 
            // cboWeightType
            // 
            this.cboWeightType.FormattingEnabled = true;
            this.cboWeightType.Location = new System.Drawing.Point(139, 69);
            this.cboWeightType.Name = "cboWeightType";
            this.cboWeightType.Size = new System.Drawing.Size(62, 21);
            this.cboWeightType.TabIndex = 4;
            this.cboWeightType.Visible = false;
            this.cboWeightType.SelectedIndexChanged += new System.EventHandler(this.cboWeightType_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 271);
            this.Controls.Add(this.cboWeightType);
            this.Controls.Add(this.cboModulo);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.picOutput);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.cboFormat);
            this.Controls.Add(this.cmdEncode);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Barcoder";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button cmdEncode;
        private System.Windows.Forms.ComboBox cboFormat;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.PictureBox picOutput;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox cboModulo;
        private System.Windows.Forms.ComboBox cboWeightType;
    }
}

