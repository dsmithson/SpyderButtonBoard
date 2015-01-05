namespace Spyder.Client.ButtonBoardUI
{
    partial class EditButtonMappingsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.udGridHeight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.udGridWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabActions = new System.Windows.Forms.TabPage();
            this.lbActions = new System.Windows.Forms.ListBox();
            this.chkShowAssignedActions = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabButtons = new System.Windows.Forms.TabPage();
            this.lblNoButtonsPressedMsg = new System.Windows.Forms.Label();
            this.cbShowAssignedPhysicalButtons = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPhysicalButtonsReset = new System.Windows.Forms.Button();
            this.btnActionsReset = new System.Windows.Forms.Button();
            this.btnPhysicalButtonSkip = new System.Windows.Forms.Button();
            this.buttonEditor = new Spyder.Client.ButtonBoardUI.Controls.ButtonBoardControl();
            this.rbLearnMode = new System.Windows.Forms.RadioButton();
            this.rbTestMode = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udGridHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udGridWidth)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabActions.SuspendLayout();
            this.tabButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 426);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 40);
            this.panel1.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(514, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(595, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.label5);
            this.pnlTop.Controls.Add(this.udGridHeight);
            this.pnlTop.Controls.Add(this.label4);
            this.pnlTop.Controls.Add(this.udGridWidth);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(682, 80);
            this.pnlTop.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(604, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "x";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // udGridHeight
            // 
            this.udGridHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.udGridHeight.Location = new System.Drawing.Point(622, 54);
            this.udGridHeight.Name = "udGridHeight";
            this.udGridHeight.Size = new System.Drawing.Size(48, 20);
            this.udGridHeight.TabIndex = 2;
            this.udGridHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udGridHeight.ValueChanged += new System.EventHandler(this.udGridHeight_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(432, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Button board grid size:";
            // 
            // udGridWidth
            // 
            this.udGridWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.udGridWidth.Location = new System.Drawing.Point(550, 54);
            this.udGridWidth.Name = "udGridWidth";
            this.udGridWidth.Size = new System.Drawing.Size(48, 20);
            this.udGridWidth.TabIndex = 1;
            this.udGridWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udGridWidth.ValueChanged += new System.EventHandler(this.udGridWidth_ValueChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 9F);
            this.label2.Location = new System.Drawing.Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(663, 42);
            this.label2.TabIndex = 1;
            this.label2.Text = "Use this form to configure the size of your button board, the physical  keys asso" +
    "ciated with each button, and it\'s logical action.";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(179, 346);
            this.panel2.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabActions);
            this.tabControl1.Controls.Add(this.tabButtons);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(179, 346);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabActions
            // 
            this.tabActions.Controls.Add(this.lbActions);
            this.tabActions.Controls.Add(this.label1);
            this.tabActions.Controls.Add(this.chkShowAssignedActions);
            this.tabActions.Controls.Add(this.btnActionsReset);
            this.tabActions.Location = new System.Drawing.Point(4, 22);
            this.tabActions.Name = "tabActions";
            this.tabActions.Padding = new System.Windows.Forms.Padding(3);
            this.tabActions.Size = new System.Drawing.Size(171, 320);
            this.tabActions.TabIndex = 0;
            this.tabActions.Text = "Logical Actions";
            this.tabActions.UseVisualStyleBackColor = true;
            // 
            // lbActions
            // 
            this.lbActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbActions.FormattingEnabled = true;
            this.lbActions.Location = new System.Drawing.Point(3, 27);
            this.lbActions.Name = "lbActions";
            this.lbActions.Size = new System.Drawing.Size(165, 250);
            this.lbActions.TabIndex = 2;
            // 
            // chkShowAssignedActions
            // 
            this.chkShowAssignedActions.AutoSize = true;
            this.chkShowAssignedActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chkShowAssignedActions.Location = new System.Drawing.Point(3, 277);
            this.chkShowAssignedActions.Name = "chkShowAssignedActions";
            this.chkShowAssignedActions.Size = new System.Drawing.Size(165, 17);
            this.chkShowAssignedActions.TabIndex = 1;
            this.chkShowAssignedActions.Text = "Show Assigned Actions";
            this.chkShowAssignedActions.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Available Actions";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabButtons
            // 
            this.tabButtons.Controls.Add(this.rbTestMode);
            this.tabButtons.Controls.Add(this.rbLearnMode);
            this.tabButtons.Controls.Add(this.btnPhysicalButtonSkip);
            this.tabButtons.Controls.Add(this.label3);
            this.tabButtons.Controls.Add(this.cbShowAssignedPhysicalButtons);
            this.tabButtons.Controls.Add(this.btnPhysicalButtonsReset);
            this.tabButtons.Controls.Add(this.lblNoButtonsPressedMsg);
            this.tabButtons.Location = new System.Drawing.Point(4, 22);
            this.tabButtons.Name = "tabButtons";
            this.tabButtons.Padding = new System.Windows.Forms.Padding(3);
            this.tabButtons.Size = new System.Drawing.Size(171, 320);
            this.tabButtons.TabIndex = 1;
            this.tabButtons.Text = "Physical Buttons";
            this.tabButtons.UseVisualStyleBackColor = true;
            // 
            // lblNoButtonsPressedMsg
            // 
            this.lblNoButtonsPressedMsg.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblNoButtonsPressedMsg.Location = new System.Drawing.Point(18, 94);
            this.lblNoButtonsPressedMsg.Name = "lblNoButtonsPressedMsg";
            this.lblNoButtonsPressedMsg.Size = new System.Drawing.Size(133, 54);
            this.lblNoButtonsPressedMsg.TabIndex = 6;
            this.lblNoButtonsPressedMsg.Text = "Press the lit button to the right on your button board now";
            // 
            // cbShowAssignedPhysicalButtons
            // 
            this.cbShowAssignedPhysicalButtons.AutoSize = true;
            this.cbShowAssignedPhysicalButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbShowAssignedPhysicalButtons.Location = new System.Drawing.Point(3, 277);
            this.cbShowAssignedPhysicalButtons.Name = "cbShowAssignedPhysicalButtons";
            this.cbShowAssignedPhysicalButtons.Size = new System.Drawing.Size(165, 17);
            this.cbShowAssignedPhysicalButtons.TabIndex = 4;
            this.cbShowAssignedPhysicalButtons.Text = "Show Assigned Buttons";
            this.cbShowAssignedPhysicalButtons.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Button Mode";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPhysicalButtonsReset
            // 
            this.btnPhysicalButtonsReset.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPhysicalButtonsReset.Location = new System.Drawing.Point(3, 294);
            this.btnPhysicalButtonsReset.Name = "btnPhysicalButtonsReset";
            this.btnPhysicalButtonsReset.Size = new System.Drawing.Size(165, 23);
            this.btnPhysicalButtonsReset.TabIndex = 7;
            this.btnPhysicalButtonsReset.Text = "Reset";
            this.btnPhysicalButtonsReset.UseVisualStyleBackColor = true;
            // 
            // btnActionsReset
            // 
            this.btnActionsReset.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnActionsReset.Location = new System.Drawing.Point(3, 294);
            this.btnActionsReset.Name = "btnActionsReset";
            this.btnActionsReset.Size = new System.Drawing.Size(165, 23);
            this.btnActionsReset.TabIndex = 8;
            this.btnActionsReset.Text = "Reset";
            this.btnActionsReset.UseVisualStyleBackColor = true;
            // 
            // btnPhysicalButtonSkip
            // 
            this.btnPhysicalButtonSkip.Location = new System.Drawing.Point(21, 151);
            this.btnPhysicalButtonSkip.Name = "btnPhysicalButtonSkip";
            this.btnPhysicalButtonSkip.Size = new System.Drawing.Size(130, 23);
            this.btnPhysicalButtonSkip.TabIndex = 8;
            this.btnPhysicalButtonSkip.Text = "Skip";
            this.btnPhysicalButtonSkip.UseVisualStyleBackColor = true;
            this.btnPhysicalButtonSkip.Click += new System.EventHandler(this.btnPhysicalButtonSkip_Click);
            // 
            // buttonEditor
            // 
            this.buttonEditor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonEditor.ButtonPadding = 4;
            this.buttonEditor.DefaultButtonColor = System.Drawing.Color.WhiteSmoke;
            this.buttonEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEditor.HorizontalCount = 2;
            this.buttonEditor.Location = new System.Drawing.Point(179, 80);
            this.buttonEditor.Name = "buttonEditor";
            this.buttonEditor.Size = new System.Drawing.Size(503, 346);
            this.buttonEditor.TabIndex = 0;
            this.buttonEditor.VerticalCount = 2;
            // 
            // rbLearnMode
            // 
            this.rbLearnMode.AutoSize = true;
            this.rbLearnMode.Checked = true;
            this.rbLearnMode.Location = new System.Drawing.Point(21, 30);
            this.rbLearnMode.Name = "rbLearnMode";
            this.rbLearnMode.Size = new System.Drawing.Size(82, 17);
            this.rbLearnMode.TabIndex = 9;
            this.rbLearnMode.TabStop = true;
            this.rbLearnMode.Text = "Learn Mode";
            this.rbLearnMode.UseVisualStyleBackColor = true;
            // 
            // rbTestMode
            // 
            this.rbTestMode.AutoSize = true;
            this.rbTestMode.Location = new System.Drawing.Point(21, 53);
            this.rbTestMode.Name = "rbTestMode";
            this.rbTestMode.Size = new System.Drawing.Size(76, 17);
            this.rbTestMode.TabIndex = 10;
            this.rbTestMode.Text = "Test Mode";
            this.rbTestMode.UseVisualStyleBackColor = true;
            // 
            // EditButtonMappingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 466);
            this.Controls.Add(this.buttonEditor);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.panel1);
            this.Name = "EditButtonMappingsForm";
            this.Text = "Edit Button Mappings";
            this.panel1.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udGridHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udGridWidth)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabActions.ResumeLayout(false);
            this.tabActions.PerformLayout();
            this.tabButtons.ResumeLayout(false);
            this.tabButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ButtonBoardControl buttonEditor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabActions;
        private System.Windows.Forms.TabPage tabButtons;
        private System.Windows.Forms.ListBox lbActions;
        private System.Windows.Forms.CheckBox chkShowAssignedActions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udGridHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udGridWidth;
        private System.Windows.Forms.Label lblNoButtonsPressedMsg;
        private System.Windows.Forms.CheckBox cbShowAssignedPhysicalButtons;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnActionsReset;
        private System.Windows.Forms.Button btnPhysicalButtonsReset;
        private System.Windows.Forms.Button btnPhysicalButtonSkip;
        private System.Windows.Forms.RadioButton rbTestMode;
        private System.Windows.Forms.RadioButton rbLearnMode;
    }
}