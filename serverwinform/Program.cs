using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace PasswordCheckServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập địa chỉ IP và cổng
            var localAddr = IPAddress.Parse("127.0.0.1");
            int port = 1308;

            // Tạo socket và lắng nghe kết nối
            var serverSocket = new TcpListener(localAddr, port);
            serverSocket.Start();
            Console.WriteLine("Server dang chay...");

            while (true)
            {
                // Chấp nhận kết nối từ client
                var clientSocket = serverSocket.AcceptSocket();
                Console.WriteLine("Client da ket noi!");

                // Nhận dữ liệu từ client
                var stream = new NetworkStream(clientSocket);
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream) { AutoFlush = true };

                // Đọc mật khẩu từ client
                string matKhau = reader.ReadLine();
                Console.WriteLine($"Mat khau nhan duoc: {matKhau}");

                // Kiểm tra chiều dài và có ký tự số hay không
                bool hasDigit = matKhau.Any(char.IsDigit);
                string result = $"Chieu Dai: {matKhau.Length}, Co ky tu so: {hasDigit}";

                // Gửi kết quả về cho client
                writer.WriteLine(result);

                // Đóng kết nối với client
                clientSocket.Close();
            }
        }
    }
}
