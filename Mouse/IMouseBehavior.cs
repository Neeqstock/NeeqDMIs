namespace NeeqDMIs.Mouse
{
    public interface IMouseBehavior
    {
        int ReceiveSample(MouseModuleSample sample);
    }
}