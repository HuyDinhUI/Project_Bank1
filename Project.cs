using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Project_Bank
{
    internal class Project
    {
        class khachhang
        {
            public int stt { get; set; }
            public string stk { get; set; }
            public string name { get; set; }
            public double nap { get; set; }
            public double rut { get; set; }
            public double sodu { get; set; }
            public string loaithe { get; set; }
        }
        class nhapxuat
        {
            static void Main(string[] args)
            {
                string filePath = "D:\\OneDrive - sbsdolar\\Desktop\\thongtinkhachhang.txt";
                try
                {
                    List<khachhang> k1 = new List<khachhang>();
                    string[] lines = File.ReadAllLines(filePath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] parts = lines[i].Split(',');
                        if (parts.Length == 7)
                        {
                            khachhang k = new khachhang()
                            {
                                stt = int.Parse(parts[0]),
                                name = parts[1],
                                stk = parts[2],
                                nap = double.Parse(parts[3]),
                                rut = double.Parse(parts[4]),
                                sodu = double.Parse(parts[5]),
                                loaithe = parts[6],
                            };
                            k1.Add(k);
                        }
                        else
                        {
                            Console.WriteLine("Khach hang thu {0} khong hop le! \nDong thu {1} ", i + 1, lines[i]);
                            break;
                        }
                    }
                    foreach (var k in k1)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t\t{3}\t\t{4}\t\t\t{5}\t\t\t{6} ", k.stt, k.stk, k.name, k.nap, k.rut, k.sodu, k.loaithe);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Loi xay ra khi doc tep");
                }
                Console.ReadKey();
            }
        }
    }
}