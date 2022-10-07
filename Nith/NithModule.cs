using NeeqDMIs.ATmega;
using System.Collections.Generic;
using System.IO.Ports;

namespace NeeqDMIs.Nith
{
    /// <summary>
    /// A module capable to handle any NITH sensor
    /// </summary>
    public class NithModule : SensorBase
    {
        public bool IsSensorValid { get; private set; } = false;
        public NithData LastNithData { get; protected set; }
        public List<INithBehavior> Behaviors { get; protected set; }
        public NithSensorNames ExpectedSensor { get; set; }
        public string ExpectedVersion { get; set; }
        
        /// <summary>
        /// Initializes a Nith sensor module.
        /// </summary>
        /// <param name="expectedSensor">Indicates which sensor this module will accept data from, skipping all the other incoming data. Use "NaS" enum value if you want this module to accept any data from any sensor.</param>
        /// <param name="expectedVersion">Indicates which sensor version this module will accept data from, skipping all the other incoming data. Leave null if you want this module to accept any data from any sensor.</param>
        public NithModule(NithSensorNames expectedSensor, string expectedVersion) : base(115200, "COM")
        {
            Behaviors = new List<INithBehavior>();
        }

        protected override void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                LastNithData = new NithData();

                string[] fields = serialPort.ReadLine().Split('|');

                string[] first = fields[0].Split('-');

                LastNithData.SensorName = NithParsers.ParseSensorName(first[0]);
                LastNithData.Version = first[1];

                // Check if sensor name and version were specified
                if ((ExpectedSensor == LastNithData.SensorName || ExpectedSensor == NithSensorNames.NaS) && (ExpectedVersion == LastNithData.Version || ExpectedVersion == null))
                {
                    LastNithData.StatusCode = NithParsers.ParseStatusCode(fields[1]);

                    LastNithData.Values.Clear();

                    string[] ands = fields[2].Split('&');

                    foreach (string v in ands)
                    {
                        string[] s = v.Split(']');
                        string fieldName = s[0].Remove(0, 1);
                        string value = s[1];
                        LastNithData.Values.Add(NithParsers.ParseField(fieldName), value);
                    }

                    foreach (INithBehavior behavior in Behaviors)
                    {
                        behavior.ReceiveData(LastNithData);
                    }

                    IsSensorValid = true;
                }
                else
                {
                    IsSensorValid = false;
                }
            }
            catch
            {
                IsSensorValid = false;
            }
            
        }
    }
}