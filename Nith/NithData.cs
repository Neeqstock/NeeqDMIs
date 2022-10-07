using System.Collections.Generic;

namespace NeeqDMIs.Nith
{
    public class NithData
    {
        public NithSensorNames SensorName { get; set; }
        public string Version { get; set; }
        public NithStatusCodes StatusCode { get; set; }
        public Dictionary<NithFields, string> Values { get; set; }
    }
}
