using NeeqDMIs.Utils;
using System.Collections.Generic;

namespace NeeqDMIs.Headtracking.NeeqHT
{
    public class HeadTrackerData
    {
        public HeadTrackerMode HeadTrackerMode { get; set; } = HeadTrackerMode.Absolute;

        private AngleBaseChanger pitchTransf;
        private AngleBaseChanger yawTransf;
        private AngleBaseChanger rollTransf;
        public double PosPitch { get; set; }
        public double PosYaw { get; set; }
        public double PosRoll { get; set; }
        public double AccPitch { get; set; }
        public double AccYaw { get; set; }
        public double AccRoll { get; set; }
        public double TranspPitch { get { return pitchTransf.Transform(PosPitch); } }
        public double TranspYaw { get { return yawTransf.Transform(PosYaw); } }
        public double TranspRoll { get { return rollTransf.Transform(PosRoll); } }
        public double Velocity { get; set; }

        public HeadTrackerData()
        {
            pitchTransf = new AngleBaseChanger();
            yawTransf = new AngleBaseChanger();
            rollTransf = new AngleBaseChanger();
        }

        public double GetYawDeltaBar()
        {
            return yawTransf.getDeltaBar();
        }

        public void CalibrateCenter()
        {
            pitchTransf.Delta = PosPitch;
            yawTransf.Delta = PosYaw;
            rollTransf.Delta = PosRoll;
            //MessageBox.Show(yawTransf.Delta.ToString(CultureInfo.InvariantCulture) + "\n" + yawTransf.getDeltaBar());
        }

        public void SetPitchDelta()
        {
            pitchTransf.Delta = PosPitch;
        }

        public void SetYawDelta()
        {
            yawTransf.Delta = PosYaw;
        }

        public void SetRollDelta()
        {
            rollTransf.Delta = PosRoll;
        }
    }
    
    public enum HeadTrackerMode
    {
        Absolute,
        Acceleration
    }
}
