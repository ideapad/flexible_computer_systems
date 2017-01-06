namespace Lab1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lNumberRows = new System.Windows.Forms.Label();
            this.numericRows = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.lResult = new System.Windows.Forms.Label();
            this.lNumberCols = new System.Windows.Forms.Label();
            this.numericCols = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.resultMatrix = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.boolMatrix = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.wpfHost = new System.Windows.Forms.Integration.ElementHost();
            this.calc_groups_btn = new System.Windows.Forms.Button();
            this.prec_groupList = new System.Windows.Forms.ListView();
            this.groupList = new System.Windows.Forms.ListView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.numericRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultMatrix)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boolMatrix)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lNumberRows
            // 
            this.lNumberRows.AutoSize = true;
            this.lNumberRows.Location = new System.Drawing.Point(894, 18);
            this.lNumberRows.Name = "lNumberRows";
            this.lNumberRows.Size = new System.Drawing.Size(137, 13);
            this.lNumberRows.TabIndex = 0;
            this.lNumberRows.Text = "Виберіть кількість рядків:";
            this.lNumberRows.Visible = false;
            // 
            // numericRows
            // 
            this.numericRows.Location = new System.Drawing.Point(1049, 18);
            this.numericRows.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numericRows.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericRows.Name = "numericRows";
            this.numericRows.Size = new System.Drawing.Size(35, 20);
            this.numericRows.TabIndex = 1;
            this.numericRows.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericRows.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lResult
            // 
            this.lResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lResult.Location = new System.Drawing.Point(6, 12);
            this.lResult.Name = "lResult";
            this.lResult.Size = new System.Drawing.Size(434, 44);
            this.lResult.TabIndex = 4;
            // 
            // lNumberCols
            // 
            this.lNumberCols.AutoSize = true;
            this.lNumberCols.Location = new System.Drawing.Point(897, 45);
            this.lNumberCols.Name = "lNumberCols";
            this.lNumberCols.Size = new System.Drawing.Size(148, 13);
            this.lNumberCols.TabIndex = 5;
            this.lNumberCols.Text = "Виберіть кількість стовпців:";
            this.lNumberCols.Visible = false;
            // 
            // numericCols
            // 
            this.numericCols.Location = new System.Drawing.Point(1048, 43);
            this.numericCols.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numericCols.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericCols.Name = "numericCols";
            this.numericCols.Size = new System.Drawing.Size(35, 20);
            this.numericCols.TabIndex = 6;
            this.numericCols.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericCols.Visible = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button3.Location = new System.Drawing.Point(3, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "File";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1, 31);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(375, 329);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 366);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 49);
            this.button1.TabIndex = 9;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // resultMatrix
            // 
            this.resultMatrix.AllowUserToAddRows = false;
            this.resultMatrix.AllowUserToDeleteRows = false;
            this.resultMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultMatrix.Location = new System.Drawing.Point(3, 6);
            this.resultMatrix.Name = "resultMatrix";
            this.resultMatrix.ReadOnly = true;
            this.resultMatrix.Size = new System.Drawing.Size(437, 322);
            this.resultMatrix.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(390, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(454, 436);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.boolMatrix);
            this.tabPage1.Controls.Add(this.lResult);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(446, 410);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // boolMatrix
            // 
            this.boolMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.boolMatrix.Location = new System.Drawing.Point(6, 59);
            this.boolMatrix.Name = "boolMatrix";
            this.boolMatrix.Size = new System.Drawing.Size(437, 348);
            this.boolMatrix.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.resultMatrix);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(446, 410);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(897, 472);
            this.tabControl2.TabIndex = 12;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.tabControl1);
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(889, 443);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.wpfHost);
            this.tabPage4.Controls.Add(this.calc_groups_btn);
            this.tabPage4.Controls.Add(this.prec_groupList);
            this.tabPage4.Controls.Add(this.groupList);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(889, 443);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // wpfHost
            // 
            this.wpfHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wpfHost.Location = new System.Drawing.Point(366, 59);
            this.wpfHost.Name = "wpfHost";
            this.wpfHost.Size = new System.Drawing.Size(438, 314);
            this.wpfHost.TabIndex = 3;
            this.wpfHost.Text = "elementHost1";
            this.wpfHost.Child = null;
            // 
            // calc_groups_btn
            // 
            this.calc_groups_btn.Location = new System.Drawing.Point(6, 6);
            this.calc_groups_btn.Name = "calc_groups_btn";
            this.calc_groups_btn.Size = new System.Drawing.Size(171, 47);
            this.calc_groups_btn.TabIndex = 2;
            this.calc_groups_btn.Text = "Сформувати групи";
            this.calc_groups_btn.UseVisualStyleBackColor = true;
            this.calc_groups_btn.Click += new System.EventHandler(this.calc_groups_btn_Click);
            // 
            // prec_groupList
            // 
            this.prec_groupList.Location = new System.Drawing.Point(8, 219);
            this.prec_groupList.Name = "prec_groupList";
            this.prec_groupList.Size = new System.Drawing.Size(343, 154);
            this.prec_groupList.TabIndex = 1;
            this.prec_groupList.UseCompatibleStateImageBehavior = false;
            this.prec_groupList.View = System.Windows.Forms.View.Details;
            this.prec_groupList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prec_groupList_MouseDown);
            // 
            // groupList
            // 
            this.groupList.Location = new System.Drawing.Point(6, 59);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(345, 154);
            this.groupList.TabIndex = 0;
            this.groupList.UseCompatibleStateImageBehavior = false;
            this.groupList.View = System.Windows.Forms.View.Details;
            this.groupList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.groupLis_ColumnClick);
            this.groupList.SelectedIndexChanged += new System.EventHandler(this.groupList_SelectedIndexChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(889, 443);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(898, 510);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.numericCols);
            this.Controls.Add(this.lNumberCols);
            this.Controls.Add(this.numericRows);
            this.Controls.Add(this.lNumberRows);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "GKS";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultMatrix)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.boolMatrix)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lNumberRows;
        private System.Windows.Forms.NumericUpDown numericRows;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lResult;
        private System.Windows.Forms.Label lNumberCols;
        private System.Windows.Forms.NumericUpDown numericCols;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView resultMatrix;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView boolMatrix;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView prec_groupList;
        private System.Windows.Forms.ListView groupList;
        private System.Windows.Forms.Button calc_groups_btn;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Integration.ElementHost wpfHost;
    }
}

