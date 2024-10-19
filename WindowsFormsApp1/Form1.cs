using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thực hiện hành động khi một ô trong DataGridView được nhấp vào
            if (e.RowIndex >= 0) // Kiểm tra xem có phải là một ô hợp lệ không
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                MessageBox.Show(row.Cells[0].Value.ToString()); // Hiển thị thông tin ô
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Có thể thêm mã kiểm tra độ dài mật khẩu nếu cần
            // Ví dụ: kiểm tra độ dài
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Nếu không cần thực hiện hành động nào, có thể bỏ qua
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy mật khẩu từ TextBox
            string matKhau = textBox2.Text;

            // Kết nối tới server
            var ip = "127.0.0.1"; // Địa chỉ IP của server (localhost)
            var ipAddress = IPAddress.Parse(ip);
            int port = 1308; // Cổng kết nối tới server
            var iPEndPoint = new IPEndPoint(ipAddress, port);

            try
            {
                using (var socket = new Socket(SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(iPEndPoint);

                    using (NetworkStream stream = new NetworkStream(socket))
                    using (var reader = new StreamReader(stream))
                    using (var writer = new StreamWriter(stream) { AutoFlush = true }) // Đảm bảo dữ liệu được gửi ngay lập tức
                    {
                        // Gửi mật khẩu tới server
                        writer.WriteLine(matKhau);

                        // Nhận phản hồi từ server
                        string response = reader.ReadLine();

                        // Hiển thị kết quả trong DataGridView
                        dataGridView1.Rows.Clear(); // Xóa các hàng cũ trước khi thêm mới
                        dataGridView1.Rows.Add("Chiều dài mật khẩu", matKhau.Length);
                        dataGridView1.Rows.Add("Kết quả kiểm tra", response);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cấu hình cột cho DataGridView nếu chưa được cấu hình
            if (dataGridView1.Columns.Count == 0) // Đảm bảo chỉ cấu hình khi chưa có cột
            {
                dataGridView1.Columns.Add("ThongTin", "Thông tin");
                dataGridView1.Columns.Add("KetQua", "Kết quả");

                // Điều chỉnh kích thước cột
                dataGridView1.Columns[0].Width = 200;
                dataGridView1.Columns[1].Width = 300;
            }
        }
    }
}
