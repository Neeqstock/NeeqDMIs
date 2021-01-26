using RawInputProcessor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Interop;

namespace NeeqDMIs.Keyboard
{
    public sealed class KeyboardModule
    {
        private RawPresentationInput _rawinput;

        public KeyboardModule(IntPtr parentHandle)
        {
            _rawinput = new RawPresentationInput(HwndSource.FromHwnd(parentHandle), RawInputCaptureMode.Foreground);

            _rawinput.AddMessageFilter();

            _rawinput.KeyPressed += OnKeyPressed;
        }

        /// <summary>
        /// Contains all the behavior modules set.
        /// </summary>
        public List<IKeyboardBehavior> KeyboardBehaviors { get; set; } = new List<IKeyboardBehavior>();

        private void OnKeyPressed(object sender, RawInputEventArgs e)
        {
            foreach (IKeyboardBehavior behavior in KeyboardBehaviors)
            {
                behavior.ReceiveEvent(e);
            }
        }

    }

}
