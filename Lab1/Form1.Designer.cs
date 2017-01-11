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
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.wpfHost = new System.Windows.Forms.Integration.ElementHost();
            this.calc_groups_btn = new System.Windows.Forms.Button();
            this.prec_groupList = new System.Windows.Forms.ListView();
            this.groupList = new System.Windows.Forms.ListView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.vmodules_list = new System.Windows.Forms.ListView();
            this.button4 = new System.Windows.Forms.Button();
            this.modules_view = new System.Windows.Forms.TreeView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.basicStructure = new System.Windows.Forms.Panel();
            this.optimizedStructure = new System.Windows.Forms.Panel();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
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
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
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
            this.button3.Size = new System.Drawing.Size(381, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Відкрити файл";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Letter Gothic Std", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(1, 31);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(383, 387);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(3, 424);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(381, 49);
            this.button1.TabIndex = 9;
            this.button1.Text = "Розрахувати";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // resultMatrix
            // 
            this.resultMatrix.AllowUserToAddRows = false;
            this.resultMatrix.AllowUserToDeleteRows = false;
            this.resultMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultMatrix.Location = new System.Drawing.Point(6, 64);
            this.resultMatrix.Name = "resultMatrix";
            this.resultMatrix.ReadOnly = true;
            this.resultMatrix.Size = new System.Drawing.Size(437, 375);
            this.resultMatrix.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(390, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(454, 471);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.boolMatrix);
            this.tabPage1.Controls.Add(this.lResult);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(446, 445);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Унікальні елементи";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // boolMatrix
            // 
            this.boolMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.boolMatrix.Location = new System.Drawing.Point(6, 63);
            this.boolMatrix.Name = "boolMatrix";
            this.boolMatrix.Size = new System.Drawing.Size(440, 348);
            this.boolMatrix.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.resultMatrix);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(446, 445);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Матриця подібності";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(6, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Матриця подібності";
            // 
            // tabControl2
            // 
            this.tabControl2.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(897, 510);
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
            this.tabPage3.Size = new System.Drawing.Size(889, 481);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Дані";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.wpfHost);
            this.tabPage4.Controls.Add(this.calc_groups_btn);
            this.tabPage4.Controls.Add(this.prec_groupList);
            this.tabPage4.Controls.Add(this.groupList);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(889, 481);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Групи";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Уточнені групи";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Групи";
            // 
            // wpfHost
            // 
            this.wpfHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wpfHost.Location = new System.Drawing.Point(432, 6);
            this.wpfHost.Name = "wpfHost";
            this.wpfHost.Size = new System.Drawing.Size(450, 467);
            this.wpfHost.TabIndex = 3;
            this.wpfHost.Text = "elementHost1";
            this.wpfHost.Child = null;
            // 
            // calc_groups_btn
            // 
            this.calc_groups_btn.Location = new System.Drawing.Point(6, 6);
            this.calc_groups_btn.Name = "calc_groups_btn";
            this.calc_groups_btn.Size = new System.Drawing.Size(420, 47);
            this.calc_groups_btn.TabIndex = 2;
            this.calc_groups_btn.Text = "Сформувати групи";
            this.calc_groups_btn.UseVisualStyleBackColor = true;
            this.calc_groups_btn.Click += new System.EventHandler(this.calc_groups_btn_Click);
            // 
            // prec_groupList
            // 
            this.prec_groupList.Location = new System.Drawing.Point(6, 288);
            this.prec_groupList.Name = "prec_groupList";
            this.prec_groupList.Size = new System.Drawing.Size(420, 185);
            this.prec_groupList.TabIndex = 1;
            this.prec_groupList.UseCompatibleStateImageBehavior = false;
            this.prec_groupList.View = System.Windows.Forms.View.Details;
            this.prec_groupList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prec_groupList_MouseDown);
            // 
            // groupList
            // 
            this.groupList.Location = new System.Drawing.Point(6, 73);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(420, 191);
            this.groupList.TabIndex = 0;
            this.groupList.UseCompatibleStateImageBehavior = false;
            this.groupList.View = System.Windows.Forms.View.Details;
            this.groupList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.groupLis_ColumnClick);
            this.groupList.SelectedIndexChanged += new System.EventHandler(this.groupList_SelectedIndexChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label2);
            this.tabPage5.Controls.Add(this.label1);
            this.tabPage5.Controls.Add(this.vmodules_list);
            this.tabPage5.Controls.Add(this.button4);
            this.tabPage5.Controls.Add(this.modules_view);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(889, 481);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Модулі";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Модулі";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(369, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Уточнені модулі";
            // 
            // vmodules_list
            // 
            this.vmodules_list.Location = new System.Drawing.Point(372, 72);
            this.vmodules_list.Name = "vmodules_list";
            this.vmodules_list.Size = new System.Drawing.Size(360, 213);
            this.vmodules_list.TabIndex = 4;
            this.vmodules_list.UseCompatibleStateImageBehavior = false;
            this.vmodules_list.View = System.Windows.Forms.View.Details;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(8, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(358, 41);
            this.button4.TabIndex = 2;
            this.button4.Text = "Сформувати модулі";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // modules_view
            // 
            this.modules_view.Location = new System.Drawing.Point(6, 72);
            this.modules_view.Name = "modules_view";
            this.modules_view.Size = new System.Drawing.Size(360, 403);
            this.modules_view.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.tabControl3);
            this.tabPage6.Controls.Add(this.button5);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(889, 481);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Структура";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(8, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(240, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "Сформувати структуру";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // basicStructure
            // 
            this.basicStructure.Location = new System.Drawing.Point(6, 6);
            this.basicStructure.Name = "basicStructure";
            this.basicStructure.Size = new System.Drawing.Size(858, 409);
            this.basicStructure.TabIndex = 1;
            this.basicStructure.Paint += new System.Windows.Forms.PaintEventHandler(this.basicStructure_Paint);
            // 
            // optimizedStructure
            // 
            this.optimizedStructure.Location = new System.Drawing.Point(6, 3);
            this.optimizedStructure.Name = "optimizedStructure";
            this.optimizedStructure.Size = new System.Drawing.Size(858, 412);
            this.optimizedStructure.TabIndex = 0;
            this.optimizedStructure.Paint += new System.Windows.Forms.PaintEventHandler(this.optimizedStructure_Paint);
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage7);
            this.tabControl3.Controls.Add(this.tabPage8);
            this.tabControl3.Location = new System.Drawing.Point(8, 31);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(878, 447);
            this.tabControl3.TabIndex = 3;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.basicStructure);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(870, 421);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "Базова структура";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.optimizedStructure);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(870, 421);
            this.tabPage8.TabIndex = 1;
            this.tabPage8.Text = "Оптимізована структура";
            this.tabPage8.UseVisualStyleBackColor = true;
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
            this.tabPage2.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
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
        private System.Windows.Forms.TreeView modules_view;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView vmodules_list;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel basicStructure;
        private System.Windows.Forms.Panel optimizedStructure;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
    }
}

