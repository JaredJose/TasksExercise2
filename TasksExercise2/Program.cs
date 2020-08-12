using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace TasksExercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list1 = new List<int>() { 10, 24, 13, 12, 100, 234, 1, 0, 0, 12 };
            List<int> list2 = new List<int>() { 14, 56, 55, 1 };
            List<int> list3 = new List<int>() { 100, 15, 4, 12 };
            List<int> list4 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<List<int>> masterList = new List<List<int>>() { list1, list2, list3, list4 };

            var sum = processList(masterList);

            var maxAwaiter = sum.GetAwaiter();
            var minAwaiter = sum.GetAwaiter();
            var avgAwaiter = sum.GetAwaiter();
            var diffAwaiter = sum.GetAwaiter();


            maxAwaiter.OnCompleted(() =>
            {
                //Console.WriteLine("max completed");
                List<int> sumList = maxAwaiter.GetResult();
                int max = Int32.MinValue;
                for(int i = 0; i < sumList.Count; i++)
                {
                    if(sumList[i] > max)
                    {
                        max = sumList[i];
                    }
                }
                Console.WriteLine("Max Sum: " + max);
            });

            minAwaiter.OnCompleted(() =>
            {
                //Console.WriteLine("min completed");
                List<int> sumList = minAwaiter.GetResult();
                int min = Int32.MaxValue;
                for (int i = 0; i < sumList.Count; i++)
                {
                    if (sumList[i] < min)
                    {
                        min = sumList[i];
                    }
                }
                Console.WriteLine("Min Sum: " + min);
            });

            avgAwaiter.OnCompleted(() =>
            {
                //Console.WriteLine("avg compelted");
                List<int> sumList = avgAwaiter.GetResult();
                double sum = 0;
                double avg = 0;
                for(int i = 0; i < sumList.Count; i++)
                {
                    sum += sumList[i];
                }
                avg = sum / sumList.Count;
                Console.WriteLine("Average Sum: " + avg);
            });

            diffAwaiter.OnCompleted(() =>
            {
                //Console.WriteLine("diff completed");
                List<int> sumList = maxAwaiter.GetResult();
                int max = Int32.MinValue;
                int min = Int32.MaxValue;
                int difference = 0;
                for (int i = 0; i < sumList.Count; i++)
                {
                    if (sumList[i] > max)
                    {
                        max = sumList[i];
                    }
                }
                for (int i = 0; i < sumList.Count; i++)
                {
                    if (sumList[i] < min)
                    {
                        min = sumList[i];
                    }
                }
                difference = max - min;
                Console.WriteLine("Difference of Max/Min: " + difference);
            });

            Task.WaitAll(sum);
            Console.WriteLine("Waiting Complete");
            Console.ReadLine();
        }

        static async Task<List<int>> processList(List<List<int>> masterList)
        {
            int sum = 0;
            Task<List<int>> summation = new Task<List<int>>(() =>
            {
                List<int> retList = new List<int>();
                for (int i = 0; i < masterList.Count; i++)
                {
                    for(int j = 0; j < masterList[i].Count; j++)
                    {
                        sum += masterList[i][j];
                    }
                    retList.Add(sum);
                    sum = 0;
                }
                Thread.Sleep(5000);
                return retList;
            });

            summation.Start();
            await summation;

            return summation.Result;
        }
    }
}
