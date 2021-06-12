using System;
using System.Security.Cryptography;
using System.Text;

namespace GetFolderName
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        public static void Start() {
            Console.Clear();
            Console.WriteLine("请输入Asset Id: ");
            string id = Console.ReadLine();
            Console.WriteLine("请输入Asset Version: ");
            int version = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(ComputeAssetHash(id));
            string subFolder = ComputeVersionString(version);
            Console.WriteLine(subFolder);
            string subFolder2 = "";
            for (int i = 0; i < (32 - Convert.ToInt32(subFolder.Length)); i++) { subFolder2 += "0"; }
            Console.WriteLine(subFolder2 + subFolder);
            Console.WriteLine("按任意键继续，按Esc退出");
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            else
            {
                Start();
            }
        }

        public static string ComputeVersionString(int version) //Int to Little Endian Hex String
        {
            var bytes = BitConverter.GetBytes(version);
            var result = "";
            foreach (var b in bytes) result += b.ToString("x2");
            return result;
        }

        public static string ComputeAssetHash(string id)
        {
            return ByteArrayToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(id))).ToUpper()
                .Substring(0, 16);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (var b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

    }
}
