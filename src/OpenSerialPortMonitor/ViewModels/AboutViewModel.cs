using Caliburn.Micro; 
using System.Reflection; 
using System.Threading.Tasks;

namespace Whitestone.OpenSerialPortMonitor.Main.ViewModels
{
    public class AboutViewModel : Screen
    {
        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public async Task CloseWindowAsync()
        {
            await TryCloseAsync(); 
        }
    }
}
