using System.Windows.Forms;

namespace NeeqDMIs.Nith
{
    public class DefaultErrorBehavior : INithErrorBehavior
    {

        public bool HandleError(NithErrors error)
        {
            switch (error)
            {
                case NithErrors.NaE:
                    return false;
                case NithErrors.Connection:
                    MessageBox.Show("Error: no connection to the selected port");
                    break;
                case NithErrors.OutputCompliance:
                    break;
                case NithErrors.Name:
                    break;
                case NithErrors.Version:
                    break;
                case NithErrors.StatusCode:
                    break;
                case NithErrors.Values:
                    break;
            }
            return true;
        }
    }
}
