using NeeqDMIs.ATmega;
using System.Globalization;

namespace NeeqDMIs.Headtracking.NeeqHT
{
    public class SBneeqHTBehavior : ISensorBehavior
    {
        private NeeqHTModule module;
        private string[] split;

        public SBneeqHTBehavior(NeeqHTModule module)
        {
            this.module = module;
        }

        public void ReceiveSensorRead(string val)
        {
            if (val.Contains("%"))
            {
                module.Write("A"); // Wakes up the headtracker
            }
            else if (val.StartsWith("$"))
            {
                module.Data.HeadTrackerMode = HeadTrackerMode.Absolute;

                val = val.Replace("$", string.Empty);
                split = val.Split('!');

                module.Data.PosYaw = double.Parse(split[0], CultureInfo.InvariantCulture);
                module.Data.PosRoll = double.Parse(split[1], CultureInfo.InvariantCulture);
                module.Data.PosPitch = double.Parse(split[2], CultureInfo.InvariantCulture);
            }
            else if (val.StartsWith("R"))
            {
                module.Data.HeadTrackerMode = HeadTrackerMode.Acceleration;

                val = val.Replace("R", string.Empty);
                split = val.Split('!');

                module.Data.AccPitch = double.Parse(split[0], CultureInfo.InvariantCulture);
                module.Data.AccRoll = double.Parse(split[1], CultureInfo.InvariantCulture);
                module.Data.AccYaw = double.Parse(split[2], CultureInfo.InvariantCulture);
            }

            foreach (INeeqHTbehavior b in module.Behaviors)
            {
                b.ReceiveHeadTrackerData(module.Data);
            }
        }
    }
}