
namespace FSWatch
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.recursiveCheckbox = new System.Windows.Forms.CheckBox();
            this.filterInput = new System.Windows.Forms.TextBox();
            this.startingInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.copyCheckbox = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter";
            // 
            // recursiveCheckbox
            // 
            this.recursiveCheckbox.AutoSize = true;
            this.recursiveCheckbox.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recursiveCheckbox.Location = new System.Drawing.Point(16, 64);
            this.recursiveCheckbox.Name = "recursiveCheckbox";
            this.recursiveCheckbox.Size = new System.Drawing.Size(82, 20);
            this.recursiveCheckbox.TabIndex = 1;
            this.recursiveCheckbox.Text = "Recursive";
            this.recursiveCheckbox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.recursiveCheckbox.UseVisualStyleBackColor = true;
            this.recursiveCheckbox.CheckedChanged += new System.EventHandler(this.recursiveCheckbox_CheckedChanged);
            // 
            // filterInput
            // 
            this.filterInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.filterInput.Location = new System.Drawing.Point(16, 113);
            this.filterInput.Name = "filterInput";
            this.filterInput.Size = new System.Drawing.Size(78, 23);
            this.filterInput.TabIndex = 2;
            this.filterInput.Text = "*.txt";
            this.filterInput.TextChanged += new System.EventHandler(this.filterInput_TextChanged);
            // 
            // startingInput
            // 
            this.startingInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.startingInput.Location = new System.Drawing.Point(16, 35);
            this.startingInput.Name = "startingInput";
            this.startingInput.Size = new System.Drawing.Size(154, 23);
            this.startingInput.TabIndex = 4;
            this.startingInput.Text = "C:\\";
            this.startingInput.TextChanged += new System.EventHandler(this.startingInput_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Starting Directory";
            // 
            // copyCheckbox
            // 
            this.copyCheckbox.AutoSize = true;
            this.copyCheckbox.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyCheckbox.Location = new System.Drawing.Point(16, 142);
            this.copyCheckbox.Name = "copyCheckbox";
            this.copyCheckbox.Size = new System.Drawing.Size(171, 20);
            this.copyCheckbox.TabIndex = 5;
            this.copyCheckbox.Text = "Make copies of new files";
            this.copyCheckbox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.copyCheckbox.UseVisualStyleBackColor = true;
            this.copyCheckbox.CheckedChanged += new System.EventHandler(this.copyCheckbox_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(16, 169);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(154, 32);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start Watching";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 211);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.copyCheckbox);
            this.Controls.Add(this.startingInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filterInput);
            this.Controls.Add(this.recursiveCheckbox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "FSW";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox recursiveCheckbox;
        private System.Windows.Forms.TextBox filterInput;
        private System.Windows.Forms.TextBox startingInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox copyCheckbox;
        private System.Windows.Forms.Button startButton;
    }
}

