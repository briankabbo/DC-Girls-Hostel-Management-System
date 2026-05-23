using System.Windows.Forms;

namespace GMS_Kabbo.Forms
{
    internal static class NavigationHelper
    {
        public static void Open<T>(Form current) where T : Form, new()
        {
            var next = new T();
            next.Show();
            current.Hide();
        }
    }
}
