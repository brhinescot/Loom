namespace Loom.Windows.Forms.Tests
{
    partial class UIEditors
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
            System.ComponentModel.StringConverter stringConverter1 = new System.ComponentModel.StringConverter();
            System.ComponentModel.StringConverter stringConverter2 = new System.ComponentModel.StringConverter();
            System.ComponentModel.StringConverter stringConverter3 = new System.ComponentModel.StringConverter();
            this.genericEditor3 = new GenericEditor();
            this.genericEditor1 = new GenericEditor();
            this.genericEditor2 = new GenericEditor();
            this.SuspendLayout();
            // 
            // genericEditor3
            // 
            this.genericEditor3.Converter = stringConverter1;
            this.genericEditor3.Location = new System.Drawing.Point(12, 65);
            this.genericEditor3.Name = "genericEditor3";
            this.genericEditor3.Size = new System.Drawing.Size(266, 20);
            this.genericEditor3.TabIndex = 5;
            this.genericEditor3.Text = "genericEditor3";
            // 
            // genericEditor1
            // 
            this.genericEditor1.Converter = stringConverter2;
            this.genericEditor1.Location = new System.Drawing.Point(13, 13);
            this.genericEditor1.Name = "genericEditor1";
            this.genericEditor1.Size = new System.Drawing.Size(267, 20);
            this.genericEditor1.TabIndex = 4;
            this.genericEditor1.Text = "genericEditor1";
            // 
            // genericEditor2
            // 
            this.genericEditor2.Converter = stringConverter3;
            this.genericEditor2.Location = new System.Drawing.Point(12, 39);
            this.genericEditor2.Name = "genericEditor2";
            this.genericEditor2.Size = new System.Drawing.Size(267, 20);
            this.genericEditor2.TabIndex = 1;
            this.genericEditor2.Text = "sj dfja sydt fasdf";
            // 
            // UIEditors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Controls.Add(this.genericEditor3);
            this.Controls.Add(this.genericEditor1);
            this.Controls.Add(this.genericEditor2);
            this.Name = "UIEditors";
            this.Text = "UIEditors";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GenericEditor genericEditor2;
        private GenericEditor genericEditor1;
        private GenericEditor genericEditor3;

    }
}