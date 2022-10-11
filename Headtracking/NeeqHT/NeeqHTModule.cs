﻿using NeeqDMIs.ATmega;
using System.Collections.Generic;

namespace NeeqDMIs.Headtracking.NeeqHT
{
    public class NeeqHTModule : SensorModule
    {
        public NeeqHTModule(int baudRate, string portPrefix) : base(baudRate, portPrefix)
        {
            base.Behaviors.Add(new SBneeqHTBehavior(this));
        }
                public NeeqHTModes HeadTrackerMode { get; set; } = NeeqHTModes.Absolute;

        public NeeqHTData Data { get; set; } = new NeeqHTData();

        public new List<INeeqHTbehavior> Behaviors { get; set; } = new List<INeeqHTbehavior>();
    }
}
