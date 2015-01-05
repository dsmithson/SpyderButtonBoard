namespace SpyderButtonBoard
{
    partial class Form1
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
            this.lnkEditButtons = new System.Windows.Forms.LinkLabel();
            this.btnGo = new System.Windows.Forms.Button();
            this.cbServers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lnkEditButtons
            // 
            this.lnkEditButtons.AutoSize = true;
            this.lnkEditButtons.Location = new System.Drawing.Point(85, 50);
            this.lnkEditButtons.Name = "lnkEditButtons";
            this.lnkEditButtons.Size = new System.Drawing.Size(108, 13);
            this.lnkEditButtons.TabIndex = 1;
            this.lnkEditButtons.TabStop = true;
            this.lnkEditButtons.Text = "Edit Button Mappings";
            this.lnkEditButtons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEditButtons_LinkClicked);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(208, 117);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cbServers
            // 
            this.cbServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServers.FormattingEnabled = true;
            this.cbServers.Location = new System.Drawing.Point(70, 117);
            this.cbServers.Name = "cbServers";
            this.cbServers.Size = new System.Drawing.Size(132, 21);
            this.cbServers.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Spyder Server";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 249);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbServers);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.lnkEditButtons);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkEditButtons;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ComboBox cbServers;
        private System.Windows.Forms.Label label1;
    }
}

