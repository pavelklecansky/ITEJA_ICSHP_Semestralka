
namespace DrWren
{
    partial class ColorSettingsForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colorDialogKeywords = new System.Windows.Forms.ColorDialog();
            this.colorDialogOperators = new System.Windows.Forms.ColorDialog();
            this.colorDialogLiterals = new System.Windows.Forms.ColorDialog();
            this.buttonKeywords = new System.Windows.Forms.Button();
            this.buttonOperators = new System.Windows.Forms.Button();
            this.buttonLiterals = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(24, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Keywords";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(24, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Operators";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(24, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Literals";
            // 
            // buttonKeywords
            // 
            this.buttonKeywords.Location = new System.Drawing.Point(237, 42);
            this.buttonKeywords.Name = "buttonKeywords";
            this.buttonKeywords.Size = new System.Drawing.Size(47, 34);
            this.buttonKeywords.TabIndex = 3;
            this.buttonKeywords.UseVisualStyleBackColor = true;
            this.buttonKeywords.Click += new System.EventHandler(this.buttonKeywords_Click);
            // 
            // buttonOperators
            // 
            this.buttonOperators.Location = new System.Drawing.Point(237, 92);
            this.buttonOperators.Name = "buttonOperators";
            this.buttonOperators.Size = new System.Drawing.Size(47, 34);
            this.buttonOperators.TabIndex = 4;
            this.buttonOperators.UseVisualStyleBackColor = true;
            this.buttonOperators.Click += new System.EventHandler(this.buttonOperators_Click);
            // 
            // buttonLiterals
            // 
            this.buttonLiterals.Location = new System.Drawing.Point(237, 150);
            this.buttonLiterals.Name = "buttonLiterals";
            this.buttonLiterals.Size = new System.Drawing.Size(47, 34);
            this.buttonLiterals.TabIndex = 5;
            this.buttonLiterals.UseVisualStyleBackColor = true;
            this.buttonLiterals.Click += new System.EventHandler(this.buttonLiterals_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(258, 213);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ColorSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 248);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLiterals);
            this.Controls.Add(this.buttonOperators);
            this.Controls.Add(this.buttonKeywords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "ColorSettingsForm";
            this.Text = "Color Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog colorDialogKeywords;
        private System.Windows.Forms.ColorDialog colorDialogOperators;
        private System.Windows.Forms.ColorDialog colorDialogLiterals;
        private System.Windows.Forms.Button buttonKeywords;
        private System.Windows.Forms.Button buttonOperators;
        private System.Windows.Forms.Button buttonLiterals;
        private System.Windows.Forms.Button buttonSave;
    }
}