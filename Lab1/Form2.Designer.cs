namespace Lab1
{
    partial class Form2
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
            this.lGroups = new System.Windows.Forms.Label();
            this.lGroupsAfterV = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lGroups
            // 
            this.lGroups.AutoSize = true;
            this.lGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lGroups.Location = new System.Drawing.Point(13, 13);
            this.lGroups.Name = "lGroups";
            this.lGroups.Size = new System.Drawing.Size(55, 16);
            this.lGroups.TabIndex = 0;
            this.lGroups.Text = "Groups:";
            // 
            // lGroupsAfterV
            // 
            this.lGroupsAfterV.AutoSize = true;
            this.lGroupsAfterV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lGroupsAfterV.Location = new System.Drawing.Point(354, 13);
            this.lGroupsAfterV.Name = "lGroupsAfterV";
            this.lGroupsAfterV.Size = new System.Drawing.Size(154, 16);
            this.lGroupsAfterV.TabIndex = 1;
            this.lGroupsAfterV.Text = "Groups after clarification:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(668, 349);
            this.Controls.Add(this.lGroupsAfterV);
            this.Controls.Add(this.lGroups);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.Text = "Групи";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lGroups;
        private System.Windows.Forms.Label lGroupsAfterV;
    }
}