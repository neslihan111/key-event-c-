using System.Windows.Forms;

namespace KeyEventLogger;

internal static class Program
{
    /// <summary>
    /// Uygulamanın giriş noktası. Yalnızca kendi penceresi içindeki tuş olaylarını dinler.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}
