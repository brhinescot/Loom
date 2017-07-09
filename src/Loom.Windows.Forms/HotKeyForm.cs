#region Using Directives

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    public partial class HotKeyForm : Form
    {
        private readonly List<HotKey> hotKeys = new List<HotKey>();

        public HotKeyForm()
        {
            InitializeComponent();
        }

        public event KeyEventHandler HotKeyPress;

        protected void RegisterHotKey(string uniqueName, Keys keyCode, Keys modifiers)
        {
            ushort id = NativeMethods.GlobalAddAtom(uniqueName);
            int mods = 0;
            if ((modifiers & Keys.Alt) == Keys.Alt)
                mods += NativeMethods.MOD_ALT;
            if ((modifiers & Keys.Shift) == Keys.Shift)
                mods += NativeMethods.MOD_SHIFT;
            if ((modifiers & Keys.Control) == Keys.Control)
                mods += NativeMethods.MOD_CONTROL;

            NativeMethods.RegisterHotKey(Handle, id, mods, (int) keyCode);
            hotKeys.Add(new HotKey(uniqueName, id, keyCode | modifiers));
        }

        protected void UnregisterAllHotKeys()
        {
            foreach (HotKey key in hotKeys)
            {
                NativeMethods.GlobalDeleteAtom(key.Id);
                NativeMethods.UnregisterHotKey(Handle, key.Id);
            }
            hotKeys.Clear();
        }

        protected void UnregisterHotKey(string uniqueName)
        {
            foreach (HotKey key in hotKeys)
            {
                if (key.Name != uniqueName)
                    continue;

                NativeMethods.GlobalDeleteAtom(key.Id);
                NativeMethods.UnregisterHotKey(Handle, key.Id);
            }
        }

        protected virtual void OnHotKeyPress(KeyEventArgs e)
        {
            KeyEventHandler handler = HotKeyPress;
            if (handler != null && !e.Handled)
                HotKeyPress(this, e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_HOTKEY)
            {
                foreach (HotKey key in hotKeys)
                    if (m.WParam == (IntPtr) key.Id)
                    {
                        KeyEventArgs args = new KeyEventArgs(key.Keys);
                        OnHotKeyPress(args);
                    }
                return;
            }

            base.WndProc(ref m);
        }

        #region Nested type: HotKey

        private class HotKey
        {
            public HotKey(string name, ushort id, Keys keys)
            {
                Name = name;
                Id = id;
                Keys = keys;
            }

            public string Name { get; }

            public ushort Id { get; }

            public Keys Keys { get; }
        }

        #endregion
    }
}