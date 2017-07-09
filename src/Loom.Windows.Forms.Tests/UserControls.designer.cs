namespace Loom.Windows.Forms.Tests
{
    partial class UserControls
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

            if(cursor != null)
                cursor.Dispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.shortcutTextBox1 = new ShortcutTextBox();
            this.tracker1 = new Tracker();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new ShortcutTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(419, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Parse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 55);
            this.button2.TabIndex = 5;
            this.button2.Text = "Bitmap Cursor";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // shortcutTextBox1
            // 
            this.shortcutTextBox1.Alt = false;
            this.shortcutTextBox1.Control = false;
            this.shortcutTextBox1.KeyCode = System.Windows.Forms.Keys.None;
            this.shortcutTextBox1.KeyData = System.Windows.Forms.Keys.None;
            this.shortcutTextBox1.Location = new System.Drawing.Point(246, 124);
            this.shortcutTextBox1.Modifiers = System.Windows.Forms.Keys.None;
            this.shortcutTextBox1.Name = "shortcutTextBox1";
            this.shortcutTextBox1.Shift = false;
            this.shortcutTextBox1.Size = new System.Drawing.Size(248, 20);
            this.shortcutTextBox1.TabIndex = 1;
            this.shortcutTextBox1.Text = "None";
            // 
            // tracker1
            // 
            this.tracker1.LargeChange = 5;
            this.tracker1.Location = new System.Drawing.Point(62, 11);
            this.tracker1.Maximum = 20;
            this.tracker1.Minimum = -1;
            this.tracker1.Name = "tracker1";
            this.tracker1.Size = new System.Drawing.Size(432, 55);
            this.tracker1.SmallChange = 1;
            this.tracker1.TabIndex = 0;
            this.tracker1.Text = "tracker1";
            this.tracker1.TickFrequency = 1;
            this.tracker1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tracker1.Value = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(419, 237);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Change";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Alt = false;
            this.textBox1.Control = false;
            this.textBox1.KeyCode = System.Windows.Forms.Keys.None;
            this.textBox1.KeyData = System.Windows.Forms.Keys.None;
            this.textBox1.Location = new System.Drawing.Point(246, 211);
            this.textBox1.Modifiers = System.Windows.Forms.Keys.None;
            this.textBox1.Name = "textBox1";
            this.textBox1.Shift = false;
            this.textBox1.Size = new System.Drawing.Size(248, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "None";
            // 
            // UserControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 298);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.shortcutTextBox1);
            this.Controls.Add(this.tracker1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "UserControls";
            this.Text = "UserControls";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tracker tracker1;
        private ShortcutTextBox shortcutTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private ShortcutTextBox textBox1;
    }
}