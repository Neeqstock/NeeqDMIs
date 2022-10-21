namespace NeeqDMIs.NithSensors
{
    public interface INithErrorBehavior
    {
        bool HandleError(NithErrors error);
    }
}
