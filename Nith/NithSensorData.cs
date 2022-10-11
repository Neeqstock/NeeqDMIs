using System.Collections.Generic;

namespace NeeqDMIs.Nith
{
    public class NithSensorData
    {
        public NithSensorNames SensorName { get; set; }
        public string Version { get; set; }
        public NithStatusCodes StatusCode { get; set; }
        public Dictionary<NithArguments, string> Values { get; set; }
        public NithSensorData()
        {
            SensorName = NithSensorNames.NaS;
            Version = "";
            StatusCode = NithStatusCodes.NaC;
            Values = new Dictionary<NithArguments, string>();
        }

        public void Reset()
        {
            SensorName = NithSensorNames.NaS;
            Version = "";
            StatusCode = NithStatusCodes.NaC;
            Values.Clear();
        }
    }
}
