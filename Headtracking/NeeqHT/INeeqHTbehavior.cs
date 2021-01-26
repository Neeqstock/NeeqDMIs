namespace NeeqDMIs.Headtracking.NeeqHT
{
    public interface INeeqHTbehavior
    {
        void ReceiveHeadTrackerData(HeadTrackerData headTrackerData);
    }
}