﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class TaiKhoanNganHang
{
    private int soDu = 1000;
    private Semaphore Balancesemaphore = new Semaphore(1, 1);

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
        Balancesemaphore.WaitOne();
        Thread.Sleep(100);
        soDu += soTien;
        Console.WriteLine("So tien nap: {0}. So du: {1}", soTien, soDu);
        Balancesemaphore.Release();
    }

    public void RutTienSemaphore(int soTien)
    {
        Balancesemaphore.WaitOne();
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

                        Thread[] NapTien = new Thread[5];
                        Thread[] RutTien = new Thread[5];

                        for (int i = 0; i < 5; i++)
                        {
                            NapTien[i] = new Thread(() =>
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    taiKhoan.NapTienSemaphore(100);
                                }
                            });

                            RutTien[i] = new Thread(() =>
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    taiKhoan.RutTienSemaphore(100);
                                }
                            });

                            NapTien[i].Start();
                            RutTien[i].Start();
                        }

                        for (int i = 0; i < 5; i++)
                        {
                            NapTien[i].Join();
                            RutTien[i].Join();
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

                        Thread RutTien1 = new Thread(() =>
                        {
                            taiKhoan.RutTienSemaphore(900);
                        });
                        Thread RutTien2 = new Thread(() =>
                        {
                            taiKhoan.RutTienSemaphore(200);
                        });

                        RutTien1.Start();
                        RutTien2.Start();

                        RutTien1.Join();
                        RutTien2.Join();
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

                        Thread[] NapTien = new Thread[5];
                        Thread[] RutTien = new Thread[5];

                        for (int i = 0; i < 5; i++)
                        {
                            NapTien[i] = new Thread(() =>
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    taiKhoan.NapTienNoSemaphore(100);
                                }
                            });

                            RutTien[i] = new Thread(() =>
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    taiKhoan.RutTienNoSemaphore(100);
                                }
                            });

                            NapTien[i].Start();
                            RutTien[i].Start();
                        }

                        for (int i = 0; i < 5; i++)
                        {
                            NapTien[i].Join();
                            RutTien[i].Join();
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

                        Thread RutTien1 = new Thread(() =>
                        {
                            taiKhoan.RutTienNoSemaphore(900);
                        });
                        Thread RutTien2 = new Thread(() =>
                        {
                            taiKhoan.RutTienNoSemaphore(200);
                        });

                        RutTien1.Start();
                        RutTien2.Start();

                        RutTien1.Join();
                        RutTien2.Join();

                        Console.WriteLine("\nSo du cuoi cung: " + taiKhoan.LaySoDu());
                        Console.WriteLine("------------------------------------\n\n");
                        break;
                    }
            }
        } while (lc!=0);
        Console.ReadKey();
    }
}
