using System;
using System.Collections.Generic;

namespace HeapPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> heap = CreateList(10, 6, 5, 4, 3, 2, 1, -1, 1, 3, -12, 20);
            printHeap(heap);
        }
        static List<T> CreateList<T>(params T[] values)
        {
            return new List<T>(values);
        }

        static void printHeap(List<int> heap)   //assumes no more than 2 digits per num
        {
            if (heap.Count > 0)
            {
                int height = (int)Math.Log2(heap.Count);
                for (int i = 0; i <= height; i ++)
                {
                    for (int j = 0; j < Math.Pow(2,i); j++)
                    {
                        int index = (int)Math.Pow(2, i) + j - 1;
                        if (index >= heap.Count)
                            break;

                        for (int k = 0; k < Math.Pow(2, height - i + 1); k++)
                            Console.Write("  ");

                        int num = heap[index];
                        if (num >= 0 && num < 10)
                            Console.Write(" " + num);
                        else if ((num >= 10 && num < 100)|| (num > -10 && num < 0))
                            Console.Write(num);
                        else
                            Console.Write("__");    //indicates number cannot be printed
                        
                        for (int k = 0; k < Math.Pow(2, height - i + 1) - 1; k++)
                            Console.Write("  ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Empty tree");
            }
        }
    }
}
