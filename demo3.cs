
using System;

namespace BankerAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] max = new int[5, 1] { { 3 }, { 5 }, { 2}, { 1}, { 5} }; //số lượng tiền tối đa mà khách hàng muốn rút
            int[,] allocation = new int[5, 1] { { 0 }, { 2}, { 3  }, { 2}, { 0} }; 
            int[,] need = new int[5, 1];
            int[] available = new int[1] { 3 }; //số tiền mà ngân hàng hiện có
            bool[] finish = new bool[5] { false, false, false, false, false };
            bool flag = true;
            int count = -1;
            for (int i = 0; i < max.GetLength(0); i++)
            {
                for (int j = 0; j < max.GetLength(1); j++)
                {
                    need[i, j] = max[i, j] - allocation[i, j];
                }
            }
            while (flag)
            {
                flag = false;
                count++;
                for (int i = 0; i < max.GetLength(0); i++)
                {
                    if (!finish[i])
                    {
                        bool check = true;
                        for (int j = 0; j < max.GetLength(1); j++)
                        {
                            if (need[i, j] > available[j])
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check)
                        {
                            finish[i] = true;
                            flag = true;
                            Console.Write("P" + i + " -> "); //thứ tự mà khách hàng có thể thực hiện giao dịch rút/vay tiền
                            for (int j = 0; j < max.GetLength(1); j++)
                            {
                                available[j] += allocation[i, j];
                            }
                        }
                    }
                }
            }
        }
    }
}
//Mảng max chứa số lượng tối đa của các tài nguyên mà mỗi khách hàng có thể yêu cầu.
//Mảng allocation chứa số lượng tài nguyên mà khách hàng đã được cấp phát.
//Mảng need chứa số lượng tài nguyên mà khách hàng cần thêm để hoàn thành giao dịch.
//Mảng available chứa số lượng tài nguyên hiện có trong ngân hàng.
//Biến finish là một mảng boolean để xác định xem khách hàng đã hoàn thành giao dịch hay chưa.
//Biến flag được sử dụng để kiểm tra xem thuật toán đã hoàn thành hay chưa.
//Biến count được sử dụng để đếm số lần lặp lại của vòng lặp
