using NeeqDMIs.ATmega;
using System.Collections.Generic;

namespace NeeqDMIs.Headtracking.NeeqHT
{
    public class NeeqHTModule : SensorModule
    {
        public NeeqHTModule(string portPrefix, int baudRate) : base(portPrefix, baudRate)
        {
            base.Behaviors.Add(new SBneeqHTBehavior(this));
        }

        public HeadTrackerData Data { get; set; } = new HeadTrackerData();

        public new List<INeeqHTbehavior> Behaviors { get; set; } = new List<INeeqHTbehavior>();
    }
}
