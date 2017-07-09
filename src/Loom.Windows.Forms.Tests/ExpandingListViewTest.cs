#region Using Directives

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms.Tests
{
    public partial class ExpandingListViewTest : Form
    {
        public ExpandingListViewTest()
        {
            InitializeComponent();
            FillList((int) groupCount.Value, (int) itemCount.Value);
        }

        private void FillList(int groups, int items)
        {
            list.Renderer = new ExpandingListViewRenderer(new TestColorTable());
            int totalItems = 0;

            for (int i = 1; i <= groups; i++)
            {
                ExpandingListViewGroup group = new ExpandingListViewGroup();
                group.Text = string.Format("Group {0}", i);
                list.Groups.Add(group);
                for (int j = 1; j <= items; j++)
                {
                    totalItems++;
                    ExpandingListViewItem item = new ExpandingListViewItem(new[] {string.Format("This is item number {0}", totalItems), DateTime.Now.AddDays(j).ToString("dddd MM/dd/yyyy")});
                    item.Summary = "Y uys udy isuyfiu iug isdh goisuh gois guihsodiugh osuidhg oiushd g sdug osdhgosdh gish dogh shg osh dfgh sdh gosuhd oguhs dough osduihgoshd.";
                    group.Items.Add(item);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            Cursor.Current = Cursors.WaitCursor;
            list.BeginUpdate();
            list.Groups.Clear();
            FillList((int) groupCount.Value, (int) itemCount.Value);
            list.EndUpdate();
            Cursor.Current = Cursors.Arrow;
            DateTime stop = DateTime.Now;

            label3.Text = string.Format("Completed in {0} milliseconds", (stop - start).TotalMilliseconds);
        }

        #region Nested type: TestColorTable

        internal class TestColorTable : DefaultColorTable
        {
            /// <summary>
            /// </summary>
            public override Color ItemBackgroundSelected => Color.FromArgb(255, 255, 255);

            /// <summary>
            /// </summary>
            public override Color ItemBackgroundSelected2 => Color.FromArgb(194, 207, 229);

            /// <summary>
            /// </summary>
            public override Color ItemBorderSelected => SystemColors.Highlight;

            /// <summary>
            /// </summary>
            public override Color ItemTextSelected => SystemColors.WindowText;

            /// <summary>
            /// </summary>
            public override Color ItemTextDimSelected => SystemColors.WindowText;
        }

        #endregion
    }
}