using System;
using System.Security.AccessControl;
using System.Threading;

class TaiKhoanNganHang
{
    private int soDu = 1000;
    private Semaphore Balancesemaphore = new Semaphore(1, 1); // Khởi tạo Semaphore với 1 tài nguyên có sẵn

    public int LaySoDu()
    {
        return soDu;
    }
    public void reset()
    {
        soDu = 1000;
    }
    public void NapTien(int soTien)
    {
        if (Balancesemaphore.WaitOne(1000))
        {
            //Thread.Sleep(2000);
            soDu += soTien;
            Console.WriteLine("Da nap {0}. So du: {1}", soTien, soDu);
            Balancesemaphore.Release();
        }
        else
        {
            Console.WriteLine("Da qua thoi gian thuc hien");
            Balancesemaphore.Release();
        }
    }

    public void RutTien(int soTien)
    {
        if (Balancesemaphore.WaitOne(1000))
        {
            if (soDu >= soTien)
            {
                soDu -= soTien;
                Console.WriteLine("Da rut {0}. So du: {1}", soTien, soDu);
            }
            else
            {
                Console.WriteLine("So du khong du!");
            }
            Balancesemaphore.Release();
        }
        else
        {
            Console.WriteLine("Da qua thoi gian thuc hien");
            Balancesemaphore.Release();
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
        Console.ReadKey();
    }
}
