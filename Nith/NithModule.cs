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
        public NithErrors LastError { get; protected set; }
        public NithSensorData LastSensorData { get; protected set; }
        public List<INithSensorBehavior> SensorBehaviors { get; protected set; }
        public List<INithErrorBehavior> ErrorBehaviors { get; protected set; }
        public List<NithSensorNames> ExpectedSensorNames { get; set; } = new List<NithSensorNames>();
        public List<string> ExpectedVersions { get; set; } = new List<string>();
        public List<NithArguments> ExpectedArguments { get; set; } = new List<NithArguments>();
        
        /// <summary>
        /// Initializes a Nith sensor module.
        /// </summary>
        public NithModule() : base(115200, "COM")
        {
            SensorBehaviors = new List<INithSensorBehavior>();
            ErrorBehaviors = new List<INithErrorBehavior>();
            LastSensorData = new NithSensorData();
            LastError = NithErrors.NaE;
        }

        protected override void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            LastSensorData.Reset();
            LastError = NithErrors.NaE;

            if (IsConnectionOk)
            {
                try
                {
                    // Output splitting
                    string[] fields = serialPort.ReadLine().Split('|');
                    string[] firstField = fields[0].Split('-');
                    string[] arguments = fields[2].Split('&');

                    // Parsings
                    LastSensorData.SensorName = NithParsers.ParseSensorName(firstField[0]);
                    LastSensorData.Version = firstField[1];
                    LastSensorData.StatusCode = NithParsers.ParseStatusCode(fields[1]);
                    foreach (string v in arguments)
                    {
                        string[] s = v.Split(']');
                        string argumentName = s[0].Remove(0, 1);
                        string value = s[1];
                        LastSensorData.Values.Add(NithParsers.ParseField(argumentName), value);
                    }

                    // Further error checking
                    // Check name
                    if (ExpectedSensorNames.Contains(LastSensorData.SensorName) || ExpectedSensorNames.Count == 0)
                    {
                        // Check version
                        if (ExpectedVersions.Contains(LastSensorData.Version) || ExpectedVersions.Count == 0)
                        {
                            // Check status code
                            if (LastSensorData.StatusCode != NithStatusCodes.ERR)
                            {
                                // Check arguments
                                if(ExpectedArguments.Count != 0)
                                {
                                    foreach (NithArguments arg in ExpectedArguments)
                                    {
                                        if (!LastSensorData.Values.ContainsKey(arg))
                                        {
                                            LastError = NithErrors.Values;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                LastError = NithErrors.StatusCode;
                            }
                        }
                        else
                        {
                            LastError = NithErrors.Version;
                        }
                    }
                    else
                    {
                        LastError = NithErrors.Name;
                    }
                }
                catch
                {
                    LastError = NithErrors.OutputCompliance;
                }
            }
            else
            {
                LastError = NithErrors.Connection;
            }

            // Checks and parsing done! Send to behaviors
            if(LastError == NithErrors.NaE)         
            {
                // No errors, send to sensor behaviors
                foreach(INithSensorBehavior sbeh in SensorBehaviors)
                {
                    sbeh.HandleData(LastSensorData);
                }
            }
            else                                    
            {
                // Errors, send to error behaviors
                foreach(INithErrorBehavior ebeh in ErrorBehaviors)
                {
                    ebeh.HandleError(LastError);
                }
            }
        }
            
    }
}