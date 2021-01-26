using RawInputProcessor;

namespace NeeqDMIs.Keyboard
{
    public interface IKeyboardBehavior
    {
        int ReceiveEvent(RawInputEventArgs e);
    }
}