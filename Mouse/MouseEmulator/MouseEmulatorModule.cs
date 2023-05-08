using NeeqDMIs.Eyetracking.PointFilters;
using NeeqDMIs.Mouse;
using System.Drawing;

namespace NeeqDMIs.Eyetracking.MouseEmulator
{
    public class MouseEmulatorModule
    {
        private IPointFilter filter;
        private Point currentInput = new Point();
        private bool enabled = false;
        private bool cursorVisible = true;

        public MouseEmulatorModule(IPointFilter filter)
        {
            Filter = filter;
        }

        public IPointFilter Filter { get => filter; set => filter = value; }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public bool CursorVisible
        {
            get { return cursorVisible; }
            set
            {
                cursorVisible = value;
                MouseFunctions.ShowCursor(cursorVisible);
                
            }
        }

        public void ReceiveInputCoordinates(double X, double Y)
        {
            if (enabled)
            {
                currentInput.X = (int)X;
                currentInput.Y = (int)Y;

                Filter.Push(currentInput);

                MouseFunctions.SetCursorPos(Filter.GetOutput().X, Filter.GetOutput().Y);
            }
        }
    }
}