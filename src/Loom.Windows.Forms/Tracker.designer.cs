using System.Windows.Forms;

namespace Loom.Windows.Forms
{
    partial class Tracker
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.value = new System.Windows.Forms.TextBox();
            this.trackbar = new System.Windows.Forms.TrackBar();
            this.text = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // value
            // 
            this.value.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.value.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.value.Location = new System.Drawing.Point(140, 20);
            this.value.MaxLength = 3;
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(28, 23);
            this.value.TabIndex = 1;
            this.value.Validated += new System.EventHandler(this.HandleValueValidated);
            this.value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HandleTextBoxKeyDown);
            // 
            // trackbar
            // 
            this.trackbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.trackbar.Location = new System.Drawing.Point(174, 9);
            this.trackbar.Maximum = 20;
            this.trackbar.Minimum = -1;
            this.trackbar.Name = "trackbar";
            this.trackbar.Size = new System.Drawing.Size(267, 45);
            this.trackbar.TabIndex = 2;
            this.trackbar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackbar.ValueChanged += new System.EventHandler(this.HandleTrackbarValueChanged);
            // 
            // text
            // 
            this.text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.text.AutoSize = true;
            this.text.Location = new System.Drawing.Point(3, 24);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(131, 15);
            this.text.TabIndex = 0;
            this.text.Text = "tracker1";
            this.text.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.Controls.Add(this.trackbar, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.value, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.text, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(444, 63);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Tracker
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Tracker";
            this.Size = new System.Drawing.Size(444, 63);
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TextBox value;
        private TrackBar trackbar;
        private Label text;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
