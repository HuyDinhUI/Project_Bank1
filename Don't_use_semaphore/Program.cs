using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Don_t_use_semaphore
{
    class TaiKhoanNganHang
    {
        private int soDu = 1000;
        public void reset()
        {
            soDu = 1000;
        }
        public int LaySoDu()
        {
            return soDu;
        }
        public void NapTien(int soTien)
        {
            soDu += soTien;
            Console.WriteLine($"Da nap {soTien}. So du: {soDu}");
        }

        public void RutTien(int soTien)
        {
            if (soDu >= soTien)
            {
                Thread.Sleep(100);
                soDu -= soTien;
                Console.WriteLine($"Da rut {soTien}. So du: {soDu}");
            }
            else
            {
                Console.WriteLine("So du khong du!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int lc;
            TaiKhoanNganHang taiKhoan = new TaiKhoanNganHang();
            do
            {
                Console.WriteLine("------------MENU-----------");
                Console.WriteLine("1. Thuc hien nap.");
                Console.WriteLine("2. Thuc hien rut.");
                Console.WriteLine("3. Thuc hien nap va rut.");
                Console.WriteLine("4. Thuc hien rut cung luc.");
                Console.WriteLine("0. Dung chuong trinh.");
                Console.WriteLine("Nhap lua chon: ");
                lc=int.Parse(Console.ReadLine());
                switch (lc)
                {
                    case 1:
                        {
                            Console.WriteLine("------------THUC HIEN NAP------------");
                            taiKhoan.reset();
                            for (int j = 0; j < 5; j++)
                            {
                                taiKhoan.NapTien(100);
                            }
                            Console.WriteLine("So du cuoi cung: " + taiKhoan.LaySoDu());
                            Console.WriteLine("-------------------------------------\n\n");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("------------THUC HIEN RUT------------");
                            taiKhoan.reset();
                            for (int j = 0; j < 5; j++)
                            {
                                taiKhoan.RutTien(100);
                            }
                            Console.WriteLine("So du cuoi cung: " + taiKhoan.LaySoDu());
                            Console.WriteLine("-------------------------------------\n\n");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("------------NAP VA RUT------------");
                            taiKhoan.reset();
                            Thread[] luongNapTien = new Thread[5];
                            Thread[] luongRutTien = new Thread[5];

                            for (int i = 0; i < 5; i++)
                            {
                                luongNapTien[i] = new Thread(() =>
                                {
                                    for (int j = 0; j < 5; j++)
                                    {
                                        taiKhoan.NapTien(100);
                                    }
                                });

                                luongRutTien[i] = new Thread(() =>
                                {
                                    for (int j = 0; j < 5; j++)
                                    {
                                        taiKhoan.RutTien(100);
                                    }
                                });

                                luongNapTien[i].Start();
                                luongRutTien[i].Start();
                            }

                            for (int i = 0; i < 5; i++)
                            {
                                luongNapTien[i].Join();
                                luongRutTien[i].Join();
                            }
                            Console.WriteLine("So du cuoi cung: " + taiKhoan.LaySoDu());
                            Console.WriteLine("------------------------------------\n\n");
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("------------RUT CUNG LUC------------");
                            taiKhoan.reset();
                            Thread luongRutTien1 = new Thread(() =>
                            {
                                taiKhoan.RutTien(900);
                            });
                            Thread luongRutTien2 = new Thread(() =>
                            {
                                taiKhoan.RutTien(200);
                            });
                            luongRutTien1.Start();
                            luongRutTien2.Start();
                            luongRutTien1.Join();
                            luongRutTien2.Join();
                            Console.WriteLine("So du cuoi cung: " + taiKhoan.LaySoDu());
                            Console.WriteLine("------------------------------------\n\n");
                            break;
                        }
                }
            } while (lc!=0);
        }
    }
}
