using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles(); // Bật các kiểu hiển thị cho Windows
            Application.SetCompatibleTextRenderingDefault(false); // Thiết lập chế độ tương thích cho văn bản
            Application.Run(new Form1()); // Khởi động Form1
        }
    }
}
