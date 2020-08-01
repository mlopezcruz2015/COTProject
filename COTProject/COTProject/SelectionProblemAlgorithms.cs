using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace COTProject
{
    class SelectionProblemAlgorithms
    {
        private static StringBuilder sb = new StringBuilder();                     //For logging to .txt

        static void Main(string[] args)
        {
            //Create necessary and Reused Variables
            int m = 5;                                                  //m = iterations (to get averages)
            Random rnd = new Random();                                  //Random class to generate random numbers 

            //Begin Analysis
            for (int n = 10000; n <= 200000; n += 10000)
            {
                int i = (int)Math.Ceiling((double)(2.0 * n / 3.0));         //Get i   
                int[,] A = new int[m, n];                                   //Generate m Randomized Arrays        

                List<double[]> ms = new List<double[]>();                   //Generate List of arrays to hold microseconds which will be used to compute average for all 3 algos
                ms.Add(new double[m]);
                ms.Add(new double[m]);
                ms.Add(new double[m]);


                for (int j = 0; j < m; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        //Generate random number between 0 and 30000.
                        A[j, k] = rnd.Next(32767);
                    }
                }

                //TEST
                Console.WriteLine($"-------------------------------------------------------------------------------------------");
                Console.WriteLine($"n={n}");
                Console.WriteLine("What ith should be:");
                for (int j = 0; j < m; j++)
                {
                    //Initialize Array that holds our values for jth Array
                    int[] B = new int[n];
                    for (int x = 0; x < n; x++)
                    {
                        B[x] = A[j, x];
                    }

                    Array.Sort(B);
                    Console.WriteLine(B[i - 1]);
                }
                Console.WriteLine("");


                //ALG1
                for (int j = 0; j < m; j++)
                {
                    //Initialize Array that holds our values for jth Array
                    int[] B = new int[n];
                    for (int x = 0; x < n; x++)
                    {
                        B[x] = A[j, x];
                    }

                    //Run Algo and measure time
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    Alg1(B, n, i);
                    stopwatch.Stop();

                    //Get Microseconds for this iteration 
                    long microseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                    ms[0][j] = microseconds;
                }

                //Get Average Micro Seconds for ALG1.
                PrintAverageMicroSeconds("ALG1", ms[0], n);

                //ALG2
                for (int j = 0; j < m; j++)
                {
                    //Initialize Array that holds our values for jth Array
                    int[] B = new int[n];
                    for (int x = 0; x < n; x++)
                    {
                        B[x] = A[j, x];
                    }

                    //Run Algo and measure time
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    Alg2(B, n, i);
                    stopwatch.Stop();

                    //Get Microseconds for this iteration 
                    long microseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                    ms[1][j] = microseconds;
                }

                //Get Average Micro Seconds for ALG1.
                PrintAverageMicroSeconds("ALG2", ms[1], n);

                //ALG3
                for (int j = 0; j < m; j++)
                {
                    //Initialize Array that holds our values for jth Array
                    int[] B = new int[n];
                    for (int x = 0; x < n; x++)
                    {
                        B[x] = A[j, x];
                    }

                    //Run Algo and measure time
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    Alg3(B, n, i);
                    stopwatch.Stop();

                    //Get Microseconds for this iteration 
                    long microseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                    ms[2][j] = microseconds;
                }

                //Get Average Micro Seconds for ALG1.
                PrintAverageMicroSeconds("ALG3", ms[2], n);


                //Add line for better log formatting.
                Console.WriteLine($"-------------------------------------------------------------------------------------------");
                sb.AppendLine("");
            }

            if (File.Exists("log2.txt"))
                File.Delete("log2.txt");

            File.AppendAllText("log2.txt", sb.ToString());
            sb.Clear();
        }

        private static void PrintAverageMicroSeconds(string algName, double[] ms, int n)
        {
            int count = 0;
            double averageMs = 0;
            foreach (var microSeconds in ms)
            {
                averageMs += microSeconds;
                count++;
            }

            averageMs /= count;
            Console.WriteLine($"Avg MicroSeconds for {algName} at n = {n} : {averageMs} μs\n");
            sb.AppendLine($"{algName} with n = {n} : {averageMs} μs");
        }

        private static void Alg1(int[] A, int n, int i)
        {
            InsertionSort(A, n);
            Console.WriteLine(A[i-1]);
        }

        private static void Alg2(int[] A, int n, int i)
        {
            Heapsort(A, n);
            Console.WriteLine(A[i-1]);
        }

        private static void Alg3(int[] A, int n, int i)
        {
            int x = RandomizedSelect(A, 0, n-1, i);
            Console.WriteLine(x);
        }

        private static void InsertionSort(int[] A, int n)
        {
            for (int j = 1; j < n; j++)
            {
                int key = A[j];
                int i = j - 1;

                while(i >= 0 && A[i] > key)
                {
                    A[i + 1] = A[i];
                    i--;
                }

                A[i + 1] = key;
            }
        }

        private static void Heapsort(int[] A, int n)
        {
            BuildMaxHeap(A);
            for (int i = n-1; i > 0; i--)
            {
                int temp = A[i];
                A[i] = A[0];
                A[0] = temp;

                n--;

                MaxHeapify(A, 0, n);
            }
        }

        private static void BuildMaxHeap(int[] A)
        {
            int n = A.Length;
            for (int i = (n/2)-1; i >= 0; i--)
            {
                MaxHeapify(A, i, n);
            }
        }

        private static void MaxHeapify(int[] A, int i, int n)
        {
            //Get Left Node
            int l = 2 * i + 1;

            //Get Right Node
            int r =  2 * i + 2;
            
            int largest = 0;

            if (l < n && A[l] > A[i])
            {
                largest = l;
            }
            else
            {
                largest = i;
            }

            if (r < n && A[r] > A[largest])
            {
                largest = r;
            }

            if (largest != i)
            {
                int temp = A[i];
                A[i] = A[largest];
                A[largest] = temp;

                MaxHeapify(A, largest, n);
            }
        }

        private static int RandomizedSelect(int[] A, int p, int r, int i)
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

        private static int RandomizedPartition(int[] A, int p, int r)
        {
            var rand = new Random();
            int i = rand.Next(p, r);

            int temp = A[i];
            A[i] = A[r];
            A[r] = temp;

            return Partition(A, p, r);
        }

        private static int Partition(int[] A, int p, int r)
        {
            int x = A[r];
            int i = p - 1;

            for (int j = p; j < r; j++)
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
            A[i+1] = A[r];
            A[r] = temp2;

            return i + 1;
        }
    }
}
