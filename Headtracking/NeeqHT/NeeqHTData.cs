using NeeqDMIs.Mappers;

namespace NeeqDMIs.Headtracking.NeeqHT
{
    public class NeeqHTData
    {
        private AngleBaseChanger pitchBaseChanger;
        private AngleBaseChanger yawBaseChanger;
        private AngleBaseChanger rollBaseChanger;
        public Polar3DData Position { get; set; }
        public Polar3DData Acceleration { get; set; }
        public Polar3DData CenteredPosition
        {
            get
            {
                return new Polar3DData { Yaw = yawBaseChanger.Transform(Position.Yaw), Pitch = pitchBaseChanger.Transform(Position.Pitch), Roll = yawBaseChanger.Transform(Position.Roll) };
            }
        }
        public double Velocity { get; set; }

        public NeeqHTData()
        {
            pitchBaseChanger = new AngleBaseChanger();
            yawBaseChanger = new AngleBaseChanger();
            rollBaseChanger = new AngleBaseChanger();
        }

        public Polar3DData GetCenter()
        {
            return new Polar3DData
            {
                Yaw = yawBaseChanger.Delta,
                Pitch = pitchBaseChanger.Delta,
                Roll = rollBaseChanger.Delta

            };
        }

        public void SetCenter()
        {
            pitchBaseChanger.Delta = Position.Pitch;
            yawBaseChanger.Delta = Position.Yaw;
            rollBaseChanger.Delta = Position.Roll;
        }
    }

}
