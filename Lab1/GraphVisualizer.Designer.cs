namespace Lab1
{
    partial class GraphVisualizer
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
            this.wpfHost = new System.Windows.Forms.Integration.ElementHost();
            this.Generate = new System.Windows.Forms.Button();
            this.Reload = new System.Windows.Forms.Button();
            this.createM = new System.Windows.Forms.Button();
            this.backG = new System.Windows.Forms.Button();
            this.nextG = new System.Windows.Forms.Button();
            this.modulsV = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // wpfHost
            // 
            this.wpfHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wpfHost.Location = new System.Drawing.Point(-1, 0);
            this.wpfHost.Name = "wpfHost";
            this.wpfHost.Size = new System.Drawing.Size(229, 428);
            this.wpfHost.TabIndex = 0;
            this.wpfHost.Text = "elementHost1";
            this.wpfHost.Child = null;
            // 
            // Generate
            // 
            this.Generate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Generate.Location = new System.Drawing.Point(1045, 40);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(85, 23);
            this.Generate.TabIndex = 1;
            this.Generate.Text = "Відобразити";
            this.Generate.UseVisualStyleBackColor = true;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // Reload
            // 
            this.Reload.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Reload.Location = new System.Drawing.Point(1045, 88);
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(85, 23);
            this.Reload.TabIndex = 2;
            this.Reload.Text = "Оновити";
            this.Reload.UseVisualStyleBackColor = true;
            this.Reload.Click += new System.EventHandler(this.Reload_Click);
            // 
            // createM
            // 
            this.createM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createM.Location = new System.Drawing.Point(1388, 305);
            this.createM.Name = "createM";
            this.createM.Size = new System.Drawing.Size(114, 23);
            this.createM.TabIndex = 3;
            this.createM.Text = "Сформувати модулі";
            this.createM.UseVisualStyleBackColor = true;
            this.createM.Click += new System.EventHandler(this.createM_Click);
            // 
            // backG
            // 
            this.backG.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.backG.Location = new System.Drawing.Point(1388, 201);
            this.backG.Name = "backG";
            this.backG.Size = new System.Drawing.Size(48, 39);
            this.backG.TabIndex = 4;
            this.backG.Text = "<-";
            this.backG.UseVisualStyleBackColor = true;
            this.backG.Click += new System.EventHandler(this.backG_Click);
            // 
            // nextG
            // 
            this.nextG.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.nextG.Location = new System.Drawing.Point(1444, 201);
            this.nextG.Name = "nextG";
            this.nextG.Size = new System.Drawing.Size(48, 39);
            this.nextG.TabIndex = 5;
            this.nextG.Text = "->";
            this.nextG.UseVisualStyleBackColor = true;
            this.nextG.Click += new System.EventHandler(this.nextG_Click);
            // 
            // modulsV
            // 
            this.modulsV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modulsV.Location = new System.Drawing.Point(1388, 354);
            this.modulsV.Name = "modulsV";
            this.modulsV.Size = new System.Drawing.Size(114, 23);
            this.modulsV.TabIndex = 6;
            this.modulsV.Text = "Уточнити модулі";
            this.modulsV.UseVisualStyleBackColor = true;
            this.modulsV.Click += new System.EventHandler(this.modulsV_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(779, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 240);
            this.label1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(1136, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 265);
            this.label2.TabIndex = 8;
            // 
            // GraphVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 428);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modulsV);
            this.Controls.Add(this.nextG);
            this.Controls.Add(this.backG);
            this.Controls.Add(this.createM);
            this.Controls.Add(this.Reload);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.wpfHost);
            this.Name = "GraphVisualizer";
            this.Text = "GraphVisualizer";
            this.Load += new System.EventHandler(this.GraphVisualizer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost wpfHost;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Button Reload;

        private System.Windows.Forms.Button createM;
        private System.Windows.Forms.Button backG;
        private System.Windows.Forms.Button nextG;
        private System.Windows.Forms.Button modulsV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}