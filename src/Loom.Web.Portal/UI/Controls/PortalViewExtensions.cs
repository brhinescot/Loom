#region Using Directives

using System.Collections.Generic;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public static class PortalViewExtensions
    {
        public static List<Box> FindBoxes(this PortalPartialView view)
        {
            List<Box> found = new List<Box>();

            Stack<Control> stack = new Stack<Control>();
            stack.Push(view);

            while (stack.Count > 0)
            {
                Control control = stack.Pop();
                foreach (Control child in control.Controls)
                    stack.Push(child);

                Box board = control as Box;
                if (board != null)
                    found.Add(board);
            }

            return found;
        }
    }
}