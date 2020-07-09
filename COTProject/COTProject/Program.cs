using System;

namespace COTProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private void Alg1(int[] A, int n, int i)
        {
            InsertionSort(A, n);
            Console.WriteLine(A[i-1]);
        }
        private void Alg2(int[] A, int n, int i)
        {
            Heapsort(A, n);
            Console.WriteLine(A[i-1]);
        }
        private void Alg3(int[] A, int n, int i)
        {
            int x = RandomizedSelect(A, n, n, i);
            Console.WriteLine(x);
        }

        private void InsertionSort(int[] A, int n)
        {
            for (int j = 1; j < n-1; j++)
            {
                int key = A[j];
                int i = j - 1;

                while(i > 0 && A[i] > key)
                {
                    A[i + 1] = A[i];
                    i--;
                }

                A[i + 1] = key;
            }
        }

        private void Heapsort(int[] A, int n)
        {
            n = A.Length;

            BuildMaxHeap(A);
            for (int i = n-1; i > 1; i--)
            {
                int temp = A[i];
                A[i] = A[1];
                A[1] = temp;

                n--;

                MaxHeapify(A, 0);
            }
        }

        private void BuildMaxHeap(int[] A)
        {
            int n = A.Length;
            for (int i = n/2; i > 0; i--)
            {
                MaxHeapify(A, i);
            }
        }

        private void MaxHeapify(int[] A, int i)
        {
            int n = A.Length;

            int l = A[2 * i + 1];
            int r = A[2 * i + 2];
            
            int largest = 0;

            if (l <= n && A[l] > A[i])
            {
                largest = l;
            }
            else
            {
                largest = i;
            }

            if (r <= n && A[r] > A[largest])
            {
                largest = r;
            }

            if (largest != i)
            {
                int temp = A[i];
                A[i] = A[largest];
                A[largest] = temp;

                MaxHeapify(A, largest);
            }
        }

        private int RandomizedSelect(int[] A, int p, int r, int i)
        {
            if (p == r)
                return A[p];

            int q = RandomizedPartition(A, p, r);

            int k = q - p + 1;

            if (i == k)
                return A[q];
            else if (i < k)
                return RandomizedSelect(A, p, q - 1, i);
            else
                return RandomizedSelect(A, q + 1, r, i - k);
        }

        private int RandomizedPartition(int[] A, int p, int r)
        {
            var rand = new Random();
            int i = rand.Next(p, r);

            int temp = A[i];
            A[i] = A[r];
            A[r] = temp;

            return Partition(A, p, r);
        }

        private int Partition(int[] A, int p, int r)
        {
            int x = A[r];
            int i = p - 1;

            for (int j = p; j < r - 2; j++)
            {
                if (A[j] <= x)
                {
                    i++;

                    int temp = A[i];
                    A[i] = A[j];
                    A[j] = temp;
                }
            }

            int temp2 = A[i + 1];
            A[r] = A[i + 1];
            A[i + 1] = temp2;

            return i + 1;
        }
    }
}
