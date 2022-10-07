using System;

namespace NeeqDMIs.Nith
{
    internal static class NithParsers
    {
        public static NithStatusCodes ParseStatusCode(string code)
        {
            NithStatusCodes ret = NithStatusCodes.NaC;
            try
            {
                ret = (NithStatusCodes)Enum.Parse(typeof(NithStatusCodes), code);
            }
            catch
            {

            }
            return ret;
        }

        public static NithFields ParseField(string field)
        {
            NithFields ret = NithFields.NaF;
            try
            {
                ret = (NithFields)Enum.Parse(typeof(NithFields), field);
            }
            catch
            {

            }
            return ret;
        }

        public static NithSensorNames ParseSensorName(string name)
        {
            NithSensorNames ret = NithSensorNames.NaS;
            try
            {
                ret = (NithSensorNames)Enum.Parse(typeof(NithSensorNames), name);
            }
            catch
            {

            }
            return ret;
        }
    }
}
