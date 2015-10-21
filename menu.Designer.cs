namespace Chess
{
    partial class menu
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
            this.label1 = new System.Windows.Forms.Label();
            this.playButton = new System.Windows.Forms.Button();
            this.radioButtonWhite = new System.Windows.Forms.RadioButton();
            this.radioButtonBlack = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonResume = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Buxton Sketch", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(194, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 119);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chess";
            // 
            // playButton
            // 
            this.playButton.Enabled = false;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.playButton.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playButton.Location = new System.Drawing.Point(214, 300);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(140, 50);
            this.playButton.TabIndex = 4;
            this.playButton.Text = "Resume Game!";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // radioButtonWhite
            // 
            this.radioButtonWhite.AutoSize = true;
            this.radioButtonWhite.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonWhite.Location = new System.Drawing.Point(214, 158);
            this.radioButtonWhite.Name = "radioButtonWhite";
            this.radioButtonWhite.Size = new System.Drawing.Size(119, 23);
            this.radioButtonWhite.TabIndex = 6;
            this.radioButtonWhite.Text = "Play as White";
            this.radioButtonWhite.UseVisualStyleBackColor = true;
            this.radioButtonWhite.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // radioButtonBlack
            // 
            this.radioButtonBlack.AutoSize = true;
            this.radioButtonBlack.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonBlack.Location = new System.Drawing.Point(214, 182);
            this.radioButtonBlack.Name = "radioButtonBlack";
            this.radioButtonBlack.Size = new System.Drawing.Size(114, 23);
            this.radioButtonBlack.TabIndex = 7;
            this.radioButtonBlack.Text = "Play as Black";
            this.radioButtonBlack.UseVisualStyleBackColor = true;
            this.radioButtonBlack.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Candara", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(214, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "Start a new game..";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Candara", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(214, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "Or resume previous game";
            // 
            // radioButtonResume
            // 
            this.radioButtonResume.AutoSize = true;
            this.radioButtonResume.Checked = true;
            this.radioButtonResume.Enabled = false;
            this.radioButtonResume.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonResume.Location = new System.Drawing.Point(219, 252);
            this.radioButtonResume.Name = "radioButtonResume";
            this.radioButtonResume.Size = new System.Drawing.Size(82, 23);
            this.radioButtonResume.TabIndex = 10;
            this.radioButtonResume.TabStop = true;
            this.radioButtonResume.Text = "Resume";
            this.radioButtonResume.UseVisualStyleBackColor = true;
            this.radioButtonResume.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Brown;
            this.ClientSize = new System.Drawing.Size(642, 428);
            this.Controls.Add(this.radioButtonResume);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButtonBlack);
            this.Controls.Add(this.radioButtonWhite);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.label1);
            this.Name = "menu";
            this.Text = "Chess - marno365, marpe524";
            this.Load += new System.EventHandler(this.menu_Load);
            this.VisibleChanged += new System.EventHandler(this.menu_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.RadioButton radioButtonWhite;
        private System.Windows.Forms.RadioButton radioButtonBlack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonResume;
    }
}