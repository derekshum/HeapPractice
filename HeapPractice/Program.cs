using System;
using System.Collections.Generic;

namespace HeapPractice
{
    class MaxHeap
    {
        private List<int> heap;

        public MaxHeap()
        {
            heap = new List<int>();
        }

        public MaxHeap(List<int> list)
        {
            heap = list;
            for (int i = heap.Count / 2 - 1; i >= 0; i--)  //ensures max hpea is maintained
            {
                Heapify(i, heap.Count - 1);
            }
        }

        public List<int> GetHeap()
        {
            return heap;
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
            Heapify(0, heap.Count - 1);
            return root;
        }

        public void Sort()
        {
            //first heapify is unnecessary as it is handled at creation from list or insertion
            for (int i = heap.Count - 1; i > 0; i--)
            {
                swap(i, 0);
                Heapify(0, i - 1);

#if DEBUG
                printHeap(heap.GetRange(0, i - 1));
                for (int j = i; j < heap.Count; j++)
                    Console.Write(" " + heap[j]);
                Console.WriteLine();
#endif
            }
        }

        public void Heapify(int currentIndex, int endIndex)
        {
            while (currentIndex * 2 + 1 <= endIndex)  //while current index still has a child
            {
                int leftIndex = currentIndex * 2 + 1;
                int rightIndex = Math.Min(currentIndex * 2 + 2, endIndex);    //allows for comparison to just left root if only left root exists
                
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
        }

        private void swap(int index1, int index2)
        {
            int temp = heap[index1];
            heap[index1] = heap[index2];
            heap[index2] = temp;
        }

        public void printHeap()
        {
            printHeap(heap);
        }

        public static void printHeap(List<int> printHeap)   //assumes no more than 2 digits per num
        {
            if (printHeap.Count > 0)
            {
                int height = (int)Math.Log2(printHeap.Count);
                for (int i = 0; i <= height; i++)
                {
                    for (int j = 0; j < Math.Pow(2, i); j++)
                    {
                        int index = (int)Math.Pow(2, i) + j - 1;
                        if (index >= printHeap.Count)
                            break;

                        for (int k = 0; k < Math.Pow(2, height - i + 1); k++)
                            Console.Write("  ");

                        int num = printHeap[index];
                        if (num >= 0 && num < 10)
                            Console.Write(" " + num);
                        else if ((num >= 10 && num < 100) || (num > -10 && num < 0))
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

    class Program
    {
        static void Main(string[] args)
        {
            //List<int> list = new List<int>() { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            //MaxHeap test = new MaxHeap(new List<int>(list));
            MaxHeap testHeap = new MaxHeap();
            List<int> testList = new List<int>();

            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int entry = r.Next(0, 99);
                Console.WriteLine("Adding " + entry);
                testHeap.Insert(entry);
                testHeap.printHeap();
                testList.Add(entry);
            }

            /*
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Removing " + testHeap.PopRoot());
                testHeap.printHeap();
            }
            */

            foreach (int item in testList)
                Console.Write(" " + item);
            Console.WriteLine();

            testHeap.Sort();
            foreach (int item in testHeap.GetHeap())
                Console.Write(" " + item);


        }
    }
}
