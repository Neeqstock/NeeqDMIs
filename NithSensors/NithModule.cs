using NeeqDMIs.ATmega;
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace NeeqDMIs.NithSensors
{
    /// <summary>
    /// A module capable to handle any NITH sensor
    /// </summary>
    public class NithModule : SensorBase
    {
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

        public NithErrors LastError { get; protected set; }
        public NithSensorData LastSensorData { get; protected set; }
        public List<INithSensorBehavior> SensorBehaviors { get; protected set; }
        public List<INithErrorBehavior> ErrorBehaviors { get; protected set; }
        public List<NithSensorNames> ExpectedSensorNames { get; set; } = new List<NithSensorNames>();
        public List<string> ExpectedVersions { get; set; } = new List<string>();
        public List<NithArguments> ExpectedArguments { get; set; } = new List<NithArguments>();

        protected override void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            NithSensorData data = new NithSensorData();
            NithErrors error = NithErrors.NaE;

            if (IsConnectionOk)
            {
                string line = serialPort.ReadLine();

                if (line.StartsWith("$"))
                {
                    error = NithErrors.OK; // Set to ok, then check if wrong
                    try
                    {
                        // Output splitting
                        string[] fields = line.Split('|');
                        string[] firstField = fields[0].Split('-');
                        string[] arguments = fields[2].Split('&');

                        // Parsings
                        data.RawLine = line;
                        data.SensorName = NithParsers.ParseSensorName(firstField[0]);
                        data.Version = firstField[1];
                        data.StatusCode = NithParsers.ParseStatusCode(fields[1]);
                        foreach (string v in arguments)
                        {
                            string[] s = v.Split('=');
                            string argumentName = s[0];
                            string value = s[1];
                            data.Values.Add(NithParsers.ParseField(argumentName), value);
                        }
                    }
                    catch
                    {
                        error = NithErrors.OutputCompliance;
                    }

                    // Further error checking
                    
                    // Check name
                    if (ExpectedSensorNames.Contains(data.SensorName) || ExpectedSensorNames.Count == 0)
                    {
                        // Check version
                        if (ExpectedVersions.Contains(data.Version) || ExpectedVersions.Count == 0)
                        {
                            // Check status code
                            if (data.StatusCode != NithStatusCodes.ERR)
                            {
                                
                                // Check arguments
                                if (ExpectedArguments.Count != 0)
                                {
                                    
                                    foreach (NithArguments arg in ExpectedArguments)
                                    {
                                        if (!data.Values.ContainsKey(arg))
                                        {
                                            error = NithErrors.Values;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                error = NithErrors.StatusCode;
                            }
                        }
                        else
                        {
                            error = NithErrors.Version;
                        }
                    }
                    else
                    {
                        error = NithErrors.Name;
                    }
                }
                else
                {
                    error = NithErrors.OutputCompliance;
                }
            }
            else
            {
                error = NithErrors.Connection;
            }

            // Checks and parsing done! Send to sensorbehaviors
            foreach (INithSensorBehavior sbeh in SensorBehaviors)
            {
                sbeh.HandleData(data);
            }

            // Send to errorbehaviors
            foreach (INithErrorBehavior ebeh in ErrorBehaviors)
            {
                ebeh.HandleError(error);
            }

            LastSensorData = data;
            LastError = error;
        }
    }
}