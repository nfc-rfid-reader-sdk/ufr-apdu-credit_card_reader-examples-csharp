namespace C_sharp_Credit_Card_Reader
{
    partial class FormMain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tDLLVersion = new System.Windows.Forms.RichTextBox();
            this.labelDLL = new System.Windows.Forms.Label();
            this.bReaderClose = new System.Windows.Forms.Button();
            this.bReaderReset = new System.Windows.Forms.Button();
            this.bReaderOpen = new System.Windows.Forms.Button();
            this.groupPSE = new System.Windows.Forms.GroupBox();
            this.rPSE2 = new System.Windows.Forms.RadioButton();
            this.rPSE1 = new System.Windows.Forms.RadioButton();
            this.tabCardControls = new System.Windows.Forms.TabControl();
            this.tabTransactions = new System.Windows.Forms.TabPage();
            this.dLogs = new System.Windows.Forms.DataGridView();
            this.columnATCounter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columntTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAmmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCurrency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabCheckPSE = new System.Windows.Forms.TabPage();
            this.CheckPSERtb = new System.Windows.Forms.RichTextBox();
            this.CheckPseButton = new System.Windows.Forms.Button();
            this.tabReadParseEMV = new System.Windows.Forms.TabPage();
            this.ReadPSERtb = new System.Windows.Forms.RichTextBox();
            this.ReadPSEButton = new System.Windows.Forms.Button();
            this.a = new System.Windows.Forms.TabPage();
            this.tDebug = new System.Windows.Forms.RichTextBox();
            this.CheckPSEClear = new System.Windows.Forms.Button();
            this.ReadParsePSEClear = new System.Windows.Forms.Button();
            this.tCNR1 = new System.Windows.Forms.RichTextBox();
            this.ccLabel = new System.Windows.Forms.Label();
            this.bReadTransactions = new System.Windows.Forms.Button();
            this.bClearTransactions = new System.Windows.Forms.Button();
            this.tCNR4 = new System.Windows.Forms.RichTextBox();
            this.tCNR3 = new System.Windows.Forms.RichTextBox();
            this.tCNR2 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupPSE.SuspendLayout();
            this.tabCardControls.SuspendLayout();
            this.tabTransactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dLogs)).BeginInit();
            this.tabCheckPSE.SuspendLayout();
            this.tabReadParseEMV.SuspendLayout();
            this.a.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tDLLVersion);
            this.groupBox1.Controls.Add(this.labelDLL);
            this.groupBox1.Controls.Add(this.bReaderClose);
            this.groupBox1.Controls.Add(this.bReaderReset);
            this.groupBox1.Controls.Add(this.bReaderOpen);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reader commands";
            // 
            // tDLLVersion
            // 
            this.tDLLVersion.Location = new System.Drawing.Point(370, 18);
            this.tDLLVersion.Name = "tDLLVersion";
            this.tDLLVersion.Size = new System.Drawing.Size(100, 24);
            this.tDLLVersion.TabIndex = 5;
            this.tDLLVersion.Text = "";
            // 
            // labelDLL
            // 
            this.labelDLL.AutoSize = true;
            this.labelDLL.Location = new System.Drawing.Point(296, 24);
            this.labelDLL.Name = "labelDLL";
            this.labelDLL.Size = new System.Drawing.Size(68, 13);
            this.labelDLL.TabIndex = 4;
            this.labelDLL.Text = "DLL Version:";
            // 
            // bReaderClose
            // 
            this.bReaderClose.Location = new System.Drawing.Point(195, 19);
            this.bReaderClose.Name = "bReaderClose";
            this.bReaderClose.Size = new System.Drawing.Size(85, 23);
            this.bReaderClose.TabIndex = 3;
            this.bReaderClose.Text = "Reader Close";
            this.bReaderClose.UseVisualStyleBackColor = true;
            this.bReaderClose.Click += new System.EventHandler(this.bReaderClose_Click);
            // 
            // bReaderReset
            // 
            this.bReaderReset.Location = new System.Drawing.Point(94, 18);
            this.bReaderReset.Name = "bReaderReset";
            this.bReaderReset.Size = new System.Drawing.Size(95, 23);
            this.bReaderReset.TabIndex = 2;
            this.bReaderReset.Text = "Reader Reset";
            this.bReaderReset.UseVisualStyleBackColor = true;
            this.bReaderReset.Click += new System.EventHandler(this.bReaderReset_Click);
            // 
            // bReaderOpen
            // 
            this.bReaderOpen.Location = new System.Drawing.Point(6, 19);
            this.bReaderOpen.Name = "bReaderOpen";
            this.bReaderOpen.Size = new System.Drawing.Size(82, 23);
            this.bReaderOpen.TabIndex = 1;
            this.bReaderOpen.Text = "Reader Open";
            this.bReaderOpen.UseVisualStyleBackColor = true;
            this.bReaderOpen.Click += new System.EventHandler(this.bReaderOpen_Click);
            // 
            // groupPSE
            // 
            this.groupPSE.Controls.Add(this.rPSE2);
            this.groupPSE.Controls.Add(this.rPSE1);
            this.groupPSE.Location = new System.Drawing.Point(553, 12);
            this.groupPSE.Name = "groupPSE";
            this.groupPSE.Size = new System.Drawing.Size(226, 52);
            this.groupPSE.TabIndex = 1;
            this.groupPSE.TabStop = false;
            this.groupPSE.Text = "Select Payment System Environment (PSE)";
            // 
            // rPSE2
            // 
            this.rPSE2.AutoSize = true;
            this.rPSE2.Location = new System.Drawing.Point(135, 25);
            this.rPSE2.Name = "rPSE2";
            this.rPSE2.Size = new System.Drawing.Size(52, 17);
            this.rPSE2.TabIndex = 1;
            this.rPSE2.TabStop = true;
            this.rPSE2.Text = "PSE2";
            this.rPSE2.UseVisualStyleBackColor = true;
            // 
            // rPSE1
            // 
            this.rPSE1.AutoSize = true;
            this.rPSE1.Location = new System.Drawing.Point(38, 24);
            this.rPSE1.Name = "rPSE1";
            this.rPSE1.Size = new System.Drawing.Size(52, 17);
            this.rPSE1.TabIndex = 0;
            this.rPSE1.TabStop = true;
            this.rPSE1.Text = "PSE1";
            this.rPSE1.UseVisualStyleBackColor = true;
            // 
            // tabCardControls
            // 
            this.tabCardControls.Controls.Add(this.tabTransactions);
            this.tabCardControls.Controls.Add(this.tabCheckPSE);
            this.tabCardControls.Controls.Add(this.tabReadParseEMV);
            this.tabCardControls.Controls.Add(this.a);
            this.tabCardControls.Location = new System.Drawing.Point(12, 83);
            this.tabCardControls.Name = "tabCardControls";
            this.tabCardControls.SelectedIndex = 0;
            this.tabCardControls.Size = new System.Drawing.Size(767, 489);
            this.tabCardControls.TabIndex = 2;
            // 
            // tabTransactions
            // 
            this.tabTransactions.Controls.Add(this.bClearTransactions);
            this.tabTransactions.Controls.Add(this.bReadTransactions);
            this.tabTransactions.Controls.Add(this.dLogs);
            this.tabTransactions.Location = new System.Drawing.Point(4, 22);
            this.tabTransactions.Name = "tabTransactions";
            this.tabTransactions.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransactions.Size = new System.Drawing.Size(759, 463);
            this.tabTransactions.TabIndex = 0;
            this.tabTransactions.Text = "Transactions";
            this.tabTransactions.UseVisualStyleBackColor = true;
            // 
            // dLogs
            // 
            this.dLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dLogs.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnATCounter,
            this.columnDate,
            this.columntTime,
            this.columnAmmount,
            this.columnCurrency});
            this.dLogs.Location = new System.Drawing.Point(6, 37);
            this.dLogs.Name = "dLogs";
            this.dLogs.Size = new System.Drawing.Size(741, 420);
            this.dLogs.TabIndex = 1;
            // 
            // columnATCounter
            // 
            this.columnATCounter.HeaderText = "ATCounter";
            this.columnATCounter.Name = "columnATCounter";
            // 
            // columnDate
            // 
            this.columnDate.HeaderText = "Date";
            this.columnDate.Name = "columnDate";
            // 
            // columntTime
            // 
            this.columntTime.HeaderText = "Time";
            this.columntTime.Name = "columntTime";
            // 
            // columnAmmount
            // 
            this.columnAmmount.HeaderText = "Ammount";
            this.columnAmmount.Name = "columnAmmount";
            // 
            // columnCurrency
            // 
            this.columnCurrency.HeaderText = "Currency";
            this.columnCurrency.Name = "columnCurrency";
            // 
            // tabCheckPSE
            // 
            this.tabCheckPSE.Controls.Add(this.CheckPSEClear);
            this.tabCheckPSE.Controls.Add(this.CheckPSERtb);
            this.tabCheckPSE.Controls.Add(this.CheckPseButton);
            this.tabCheckPSE.Location = new System.Drawing.Point(4, 22);
            this.tabCheckPSE.Name = "tabCheckPSE";
            this.tabCheckPSE.Padding = new System.Windows.Forms.Padding(3);
            this.tabCheckPSE.Size = new System.Drawing.Size(759, 463);
            this.tabCheckPSE.TabIndex = 1;
            this.tabCheckPSE.Text = "Check if card supports PSE";
            this.tabCheckPSE.UseVisualStyleBackColor = true;
            // 
            // CheckPSERtb
            // 
            this.CheckPSERtb.Location = new System.Drawing.Point(6, 37);
            this.CheckPSERtb.Name = "CheckPSERtb";
            this.CheckPSERtb.Size = new System.Drawing.Size(747, 420);
            this.CheckPSERtb.TabIndex = 2;
            this.CheckPSERtb.Text = "";
            // 
            // CheckPseButton
            // 
            this.CheckPseButton.Location = new System.Drawing.Point(3, 6);
            this.CheckPseButton.Name = "CheckPseButton";
            this.CheckPseButton.Size = new System.Drawing.Size(75, 25);
            this.CheckPseButton.TabIndex = 1;
            this.CheckPseButton.Text = "CHECK";
            this.CheckPseButton.UseVisualStyleBackColor = true;
            this.CheckPseButton.Click += new System.EventHandler(this.CheckPseButton_Click);
            // 
            // tabReadParseEMV
            // 
            this.tabReadParseEMV.Controls.Add(this.tCNR4);
            this.tabReadParseEMV.Controls.Add(this.tCNR3);
            this.tabReadParseEMV.Controls.Add(this.tCNR2);
            this.tabReadParseEMV.Controls.Add(this.tCNR1);
            this.tabReadParseEMV.Controls.Add(this.ccLabel);
            this.tabReadParseEMV.Controls.Add(this.ReadParsePSEClear);
            this.tabReadParseEMV.Controls.Add(this.ReadPSERtb);
            this.tabReadParseEMV.Controls.Add(this.ReadPSEButton);
            this.tabReadParseEMV.Location = new System.Drawing.Point(4, 22);
            this.tabReadParseEMV.Name = "tabReadParseEMV";
            this.tabReadParseEMV.Padding = new System.Windows.Forms.Padding(3);
            this.tabReadParseEMV.Size = new System.Drawing.Size(759, 463);
            this.tabReadParseEMV.TabIndex = 2;
            this.tabReadParseEMV.Text = "Read and parse cards EMV";
            this.tabReadParseEMV.UseVisualStyleBackColor = true;
            // 
            // ReadPSERtb
            // 
            this.ReadPSERtb.Location = new System.Drawing.Point(6, 37);
            this.ReadPSERtb.Name = "ReadPSERtb";
            this.ReadPSERtb.Size = new System.Drawing.Size(747, 417);
            this.ReadPSERtb.TabIndex = 1;
            this.ReadPSERtb.Text = "";
            // 
            // ReadPSEButton
            // 
            this.ReadPSEButton.Location = new System.Drawing.Point(2, 6);
            this.ReadPSEButton.Name = "ReadPSEButton";
            this.ReadPSEButton.Size = new System.Drawing.Size(75, 25);
            this.ReadPSEButton.TabIndex = 0;
            this.ReadPSEButton.Text = "READ";
            this.ReadPSEButton.UseVisualStyleBackColor = true;
            this.ReadPSEButton.Click += new System.EventHandler(this.ReadPSEButton_Click);
            // 
            // a
            // 
            this.a.Controls.Add(this.tDebug);
            this.a.Location = new System.Drawing.Point(4, 22);
            this.a.Name = "a";
            this.a.Padding = new System.Windows.Forms.Padding(3);
            this.a.Size = new System.Drawing.Size(759, 463);
            this.a.TabIndex = 3;
            this.a.Text = "Read and parse cards EMV log";
            this.a.UseVisualStyleBackColor = true;
            // 
            // tDebug
            // 
            this.tDebug.Location = new System.Drawing.Point(6, 6);
            this.tDebug.Name = "tDebug";
            this.tDebug.Size = new System.Drawing.Size(750, 454);
            this.tDebug.TabIndex = 3;
            this.tDebug.Text = "";
            // 
            // CheckPSEClear
            // 
            this.CheckPSEClear.Location = new System.Drawing.Point(90, 6);
            this.CheckPSEClear.Name = "CheckPSEClear";
            this.CheckPSEClear.Size = new System.Drawing.Size(75, 25);
            this.CheckPSEClear.TabIndex = 3;
            this.CheckPSEClear.Text = "CLEAR";
            this.CheckPSEClear.UseVisualStyleBackColor = true;
            this.CheckPSEClear.Click += new System.EventHandler(this.CheckPSEClear_Click);
            // 
            // ReadParsePSEClear
            // 
            this.ReadParsePSEClear.Location = new System.Drawing.Point(90, 6);
            this.ReadParsePSEClear.Name = "ReadParsePSEClear";
            this.ReadParsePSEClear.Size = new System.Drawing.Size(75, 25);
            this.ReadParsePSEClear.TabIndex = 2;
            this.ReadParsePSEClear.Text = "CLEAR";
            this.ReadParsePSEClear.UseVisualStyleBackColor = true;
            this.ReadParsePSEClear.Click += new System.EventHandler(this.ReadParsePSEClear_Click);
            // 
            // tCNR1
            // 
            this.tCNR1.Location = new System.Drawing.Point(278, 6);
            this.tCNR1.Name = "tCNR1";
            this.tCNR1.Size = new System.Drawing.Size(55, 25);
            this.tCNR1.TabIndex = 6;
            this.tCNR1.Text = "";
            // 
            // ccLabel
            // 
            this.ccLabel.AutoSize = true;
            this.ccLabel.Location = new System.Drawing.Point(173, 12);
            this.ccLabel.Name = "ccLabel";
            this.ccLabel.Size = new System.Drawing.Size(99, 13);
            this.ccLabel.TabIndex = 4;
            this.ccLabel.Text = "Credit card number:";
            // 
            // bReadTransactions
            // 
            this.bReadTransactions.Location = new System.Drawing.Point(2, 6);
            this.bReadTransactions.Name = "bReadTransactions";
            this.bReadTransactions.Size = new System.Drawing.Size(75, 25);
            this.bReadTransactions.TabIndex = 1;
            this.bReadTransactions.Text = "READ";
            this.bReadTransactions.UseVisualStyleBackColor = true;
            this.bReadTransactions.Click += new System.EventHandler(this.bReadTransactions_Click);
            // 
            // bClearTransactions
            // 
            this.bClearTransactions.Location = new System.Drawing.Point(90, 6);
            this.bClearTransactions.Name = "bClearTransactions";
            this.bClearTransactions.Size = new System.Drawing.Size(75, 25);
            this.bClearTransactions.TabIndex = 4;
            this.bClearTransactions.Text = "CLEAR";
            this.bClearTransactions.UseVisualStyleBackColor = true;
            this.bClearTransactions.Click += new System.EventHandler(this.bClearTransactions_Click);
            // 
            // tCNR4
            // 
            this.tCNR4.Location = new System.Drawing.Point(461, 6);
            this.tCNR4.Name = "tCNR4";
            this.tCNR4.Size = new System.Drawing.Size(55, 25);
            this.tCNR4.TabIndex = 8;
            this.tCNR4.Text = "";
            // 
            // tCNR3
            // 
            this.tCNR3.Location = new System.Drawing.Point(400, 6);
            this.tCNR3.Name = "tCNR3";
            this.tCNR3.Size = new System.Drawing.Size(55, 25);
            this.tCNR3.TabIndex = 7;
            this.tCNR3.Text = "";
            // 
            // tCNR2
            // 
            this.tCNR2.Location = new System.Drawing.Point(339, 6);
            this.tCNR2.Name = "tCNR2";
            this.tCNR2.Size = new System.Drawing.Size(55, 25);
            this.tCNR2.TabIndex = 5;
            this.tCNR2.Text = "";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 584);
            this.Controls.Add(this.tabCardControls);
            this.Controls.Add(this.groupPSE);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormMain";
            this.Text = "C# Credit Card Reader";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupPSE.ResumeLayout(false);
            this.groupPSE.PerformLayout();
            this.tabCardControls.ResumeLayout(false);
            this.tabTransactions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dLogs)).EndInit();
            this.tabCheckPSE.ResumeLayout(false);
            this.tabReadParseEMV.ResumeLayout(false);
            this.tabReadParseEMV.PerformLayout();
            this.a.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelDLL;
        private System.Windows.Forms.Button bReaderClose;
        private System.Windows.Forms.Button bReaderReset;
        private System.Windows.Forms.Button bReaderOpen;
        private System.Windows.Forms.GroupBox groupPSE;
        private System.Windows.Forms.RadioButton rPSE2;
        private System.Windows.Forms.RadioButton rPSE1;
        private System.Windows.Forms.TabControl tabCardControls;
        private System.Windows.Forms.TabPage tabTransactions;
        private System.Windows.Forms.TabPage tabCheckPSE;
        private System.Windows.Forms.TabPage tabReadParseEMV;
        private System.Windows.Forms.RichTextBox tDLLVersion;
        private System.Windows.Forms.DataGridView dLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnATCounter;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn columntTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAmmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCurrency;
        private System.Windows.Forms.RichTextBox tDebug;
        private System.Windows.Forms.Button CheckPseButton;
        private System.Windows.Forms.Button ReadPSEButton;
        private System.Windows.Forms.RichTextBox ReadPSERtb;
        private System.Windows.Forms.RichTextBox CheckPSERtb;
        private System.Windows.Forms.TabPage a;
        private System.Windows.Forms.Button CheckPSEClear;
        private System.Windows.Forms.Button ReadParsePSEClear;
        private System.Windows.Forms.RichTextBox tCNR1;
        private System.Windows.Forms.Label ccLabel;
        private System.Windows.Forms.Button bClearTransactions;
        private System.Windows.Forms.Button bReadTransactions;
        private System.Windows.Forms.RichTextBox tCNR4;
        private System.Windows.Forms.RichTextBox tCNR3;
        private System.Windows.Forms.RichTextBox tCNR2;
    }
}

