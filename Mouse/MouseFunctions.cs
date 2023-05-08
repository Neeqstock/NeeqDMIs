using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NeeqDMIs.Mouse
{
    public static class MouseFunctions
    {
        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);
        [DllImport("user32")]
        public static extern int ShowCursor(bool bShow);


        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        private static POINT lpPoint;
        public static Point GetCursorPosition()
        {
            GetCursorPos(out lpPoint);
            //bool success = User32.GetCursorPos(out lpPoint);
            //if (!success)
            return lpPoint;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

    }
}