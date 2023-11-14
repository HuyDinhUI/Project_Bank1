using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
    public void NapTienSemaphore(int soTien)
    {
        if (Balancesemaphore.WaitOne(1000))
        {
            Thread.Sleep(100);
            soDu += soTien;
            Console.WriteLine("So tien nap: {0}. So du: {1}", soTien, soDu);
            Balancesemaphore.Release();
        }
        else
        {
            Console.WriteLine("Da qua thoi gian thuc hien");
            Balancesemaphore.Release();
        }
    }

    public void RutTienSemaphore(int soTien)
    {
        if (Balancesemaphore.WaitOne(1000))
        {
            if (soDu >= soTien)
            {
                Thread.Sleep(100);
                soDu -= soTien;
                Console.WriteLine("So tien rut: {0}. So du: {1}", soTien, soDu);
            }
            else
            {
                Console.WriteLine($"So tien rut: {soTien}. So du khong du!");
            }
            Balancesemaphore.Release();
        }
        else
        {
            Console.WriteLine("Da qua thoi gian thuc hien");
            Balancesemaphore.Release();
        }
    }
    public void NapTienNoSemaphore(int soTien)
    {
        Thread.Sleep(100);
        soDu += soTien;
        Console.WriteLine($"So tien nap: {soTien}. So du: {soDu}");
    }

    public void RutTienNoSemaphore(int soTien)
    {
        if (soDu >= soTien)
        {
            Thread.Sleep(100);
            soDu -= soTien;
            Console.WriteLine($"So tien rut: {soTien}. So du: {soDu}");
        }
        else
        {
            Console.WriteLine($"So tien rut: {soTien}. So du: khong du!");
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
            Console.WriteLine("DA SU DUNG SEMAPHORE");
            Console.WriteLine("1. Thuc hien nap.");
            Console.WriteLine("2. Thuc hien rut.");
            Console.WriteLine("3. Thuc hien nap va rut.");
            Console.WriteLine("4. Thuc hien rut cung luc.");
            Console.WriteLine("CHUA SU DUNG SEMAPHORE");
            Console.WriteLine("5. Thuc hien nap.");
            Console.WriteLine("6. Thuc hien rut.");
            Console.WriteLine("7. Thuc hien nap va rut.");
            Console.WriteLine("8. Thuc hien rut cung luc.");
            Console.WriteLine("0. Dung chuong trinh.");
            Console.WriteLine("Nhap lua chon: ");
            lc=int.Parse(Console.ReadLine());
            switch (lc)
            {
                case 1:
                    {
                        Console.WriteLine("------------THUC HIEN NAP------------");
                        taiKhoan.reset();
                        Console.WriteLine("So du ban dau: {0}\n", taiKhoan.LaySoDu());

                        for (int j = 0; j < 5; j++)
                        {
                            taiKhoan.NapTienSemaphore(100);
                        }

                        Console.WriteLine("\nSo du cuoi cung: " + taiKhoan.LaySoDu());
                        Console.WriteLine("-------------------------------------\n\n");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("------------THUC HIEN RUT------------");
                        taiKhoan.reset();
                        Console.WriteLine("So du ban dau: {0}\n", taiKhoan.LaySoDu());

                        for (int j = 0; j < 5; j++)
                        {
                            taiKhoan.RutTienSemaphore(100);
                        }

                        Console.WriteLine("\nSo du cuoi cung: " + taiKhoan.LaySoDu());
                        Console.WriteLine("-------------------------------------\n\n");
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("------------NAP VA RUT------------");
                        taiKhoan.reset();
                        Console.WriteLine("So du ban dau: {0}\n", taiKhoan.LaySoDu());

                        Thread[] luongNapTien = new Thread[5];
                        Thread[] luongRutTien = new Thread[5];
                        Console.WriteLine("So du ban dau: " + taiKhoan.LaySoDu());

                        for (int i = 0; i < 5; i++)
                        {
                            luongNapTien[i] = new Thread(() =>
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    taiKhoan.NapTienSemaphore(100);
                                }
                            });

                            luongRutTien[i] = new Thread(() =>
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    taiKhoan.RutTienSemaphore(100);
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

                        Console.WriteLine("\nSo du cuoi cung: " + taiKhoan.LaySoDu());
                        Console.WriteLine("------------------------------------\n\n");
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("------------RUT CUNG LUC------------");
                        taiKhoan.reset();
                        Console.WriteLine("So du ban dau: {0}\n", taiKhoan.LaySoDu());

                        Thread luongRutTien1 = new Thread(() =>
                        {
                            taiKhoan.RutTienSemaphore(900);
                        });
                        Thread luongRutTien2 = new Thread(() =>
                        {
                            taiKhoan.RutTienSemaphore(200);
                        });

                        luongRutTien1.Start();
                        luongRutTien2.Start();

                        luongRutTien1.Join();
                        luongRutTien2.Join();
                        Console.WriteLine("\nSo du cuoi cung: " + taiKhoan.LaySoDu());
                        Console.WriteLine("------------------------------------\n\n");
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("------------THUC HIEN NAP------------");
                        taiKhoan.reset();
                        Console.WriteLine("So du ban dau: {0}\n", taiKhoan.LaySoDu());

                        for (int j = 0; j < 5; j++)
                        {
                            taiKhoan.NapTienNoSemaphore(100);
                        }

                        Console.WriteLine("\nSo du cuoi cung: " + taiKhoan.LaySoDu());
                        Console.WriteLine("-------------------------------------\n\n");
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("------------THUC HIEN RUT------------");
                        taiKhoan.reset();
                        Console.WriteLine("So du ban dau: {0}\n", taiKhoan.LaySoDu());

                        for (int j = 0; j < 5; j++)
                        {
                            taiKhoan.RutTienNoSemaphore(100);
                        }

                        Console.WriteLine("\nSo du cuoi cung: " + taiKhoan.LaySoDu());
                        Console.WriteLine("-------------------------------------\n\n");
                        break;
                    }
                case 7:
                    {
                        Console.WriteLine("------------NAP VA RUT------------");
                        taiKhoan.reset();
                        Console.WriteLine("So du ban dau: {0}\n",taiKhoan.LaySoDu());

                        Thread[] luongNapTien = new Thread[5];
                        Thread[] luongRutTien = new Thread[5];

                        for (int i = 0; i < 5; i++)
                        {
                            luongNapTien[i] = new Thread(() =>
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    taiKhoan.NapTienNoSemaphore(100);
                                }
                            });

                            luongRutTien[i] = new Thread(() =>
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    taiKhoan.RutTienNoSemaphore(100);
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
                        Console.WriteLine("\nSo du cuoi cung: " + taiKhoan.LaySoDu());
                        Console.WriteLine("------------------------------------\n\n");
                        break;
                    }
                case 8:
                    {
                        Console.WriteLine("------------RUT CUNG LUC------------");
                        taiKhoan.reset();
                        Console.WriteLine("So du ban dau: {0}\n", taiKhoan.LaySoDu());

                        Thread luongRutTien1 = new Thread(() =>
                        {
                            taiKhoan.RutTienNoSemaphore(900);
                        });
                        Thread luongRutTien2 = new Thread(() =>
                        {
                            taiKhoan.RutTienNoSemaphore(200);
                        });

                        luongRutTien1.Start();
                        luongRutTien2.Start();

                        luongRutTien1.Join();
                        luongRutTien2.Join();

                        Console.WriteLine("\nSo du cuoi cung: " + taiKhoan.LaySoDu());
                        Console.WriteLine("------------------------------------\n\n");
                        break;
                    }
            }
        } while (lc!=0);
        Console.ReadKey();
    }
}
