using NeeqDMIs.ATmega;
using System;
using System.Globalization;

namespace NeeqDMIs.Headtracking.NeeqHT
{
    public class SBneeqHTBehavior : ISensorReaderBehavior
    {
        private string[] split;
        private NeeqHTModule module;

        public SBneeqHTBehavior(NeeqHTModule module)
        {
            this.module = module;
        }

        public void ReceiveSensorRead(string val)
        {
            if (val.Contains("%"))
            {
                module.Write("A");
            }
            else if (val.StartsWith("$"))
            {
                val = val.Replace("$", string.Empty);
                split = val.Split('!');

                module.Data.Yaw = double.Parse(split[0], CultureInfo.InvariantCulture);
                module.Data.Pitch = double.Parse(split[1], CultureInfo.InvariantCulture);
                module.Data.Roll = double.Parse(split[2], CultureInfo.InvariantCulture);
            }
            
            foreach(INeeqHTbehavior b in module.Behaviors)
            {
                b.ReceiveHeadTrackerData(module.Data);
            }
        }

    }
}