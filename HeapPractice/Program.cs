using System;
using System.Collections.Generic;

namespace HeapPractice
{
    class MaxHeap
    {
        public List<int> heap;

        public MaxHeap()
        {
            heap = new List<int>();
        }

        public MaxHeap(List<int> list)
        {
            heap = list;
        }

        public void Insert(int entry)
        {
            int currentIndex = heap.Count;
            heap.Add(entry);
            while (entry > heap[(currentIndex - 1) / 2] && currentIndex != 0)
            {
                swap(currentIndex, (currentIndex - 1) / 2);
                currentIndex = (currentIndex - 1) / 2;
            }
        }

        public int PopRoot()
        {
            int root = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            int currentIndex = 0;
            while (currentIndex * 2 + 1 <= heap.Count - 1)  //while current index still has a child
            {
                int leftIndex = currentIndex * 2 + 1;
                int rightIndex = Math.Min(currentIndex * 2 + 2, heap.Count - 1);    //allows for comparison to just left root if only left root exists
                
                if (heap[currentIndex] >= Math.Max(heap[leftIndex], heap[rightIndex]))
                    break;
                
                if (heap[leftIndex] >= heap[rightIndex])
                {
                    swap(currentIndex, leftIndex);
                    currentIndex = leftIndex;
                }
                else
                {
                    swap(currentIndex, rightIndex);
                    currentIndex = rightIndex;
                }
            }
            return root;
        }

        private void swap(int index1, int index2)
        {
            int temp = heap[index1];
            heap[index1] = heap[index2];
            heap[index2] = temp;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //List<int> list = new List<int>() { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            //MaxHeap test = new MaxHeap(new List<int>(list));
            MaxHeap test = new MaxHeap();
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int entry = r.Next(0, 99);
                Console.WriteLine("Adding " + entry);
                test.Insert(entry);
                printHeap(test.heap);
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Removing " + test.PopRoot());
                printHeap(test.heap);
            }
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
