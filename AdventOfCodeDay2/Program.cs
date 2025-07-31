using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCodeDay2
{
    internal class Program
    {

        public static bool isSafe(int[] report)
        {
            //check if the report is in ascending or descending order
            bool ascending = true;
            bool descending = true;

            for (int i = 0; i < report.Length - 1; i++)
            {
                if (report[i] > report[i + 1])
                {
                    ascending = false;
                }
                if (report[i] < report[i + 1])
                {
                    descending = false;
                }
            }
            //if the report is neither ascending or descending, return false
            if (!ascending && !descending)
            {
                return false;
            }

            //now we know the report is either ascending or descending
            //Check if the difference between any adjecent number is greater than 3 or if they are equal
            for (int i = 0; i < report.Length - 1; i++)
            {
                if (Math.Abs(report[i] - report[i + 1]) > 3 || report[i] == report[i + 1])
                {
                    return false;
                }
            }

            //if we get here, the report is safe
            return true;
        }
        public static List<int[]> getReports(string path)
        {
            List<int[]> reports = new List<int[]>();

            //read the file and get each line
            string[] lines = File.ReadAllLines(path);

            //go through lines and turn into int arrays
            foreach (string line in lines)
            {
                //split the line into individual numbers
                string[] numbers = line.Split(' ');
                //convert the numbers to int array
                int[] report = numbers.Select(int.Parse).ToArray();

                //add the report to the list
                reports.Add(report);
            }
            return reports;
        }
        static void Main(string[] args)
        {
            //count to track number of safe reports
            int safeCount = 0;

            //turn data into list of "reports"
            List<int[]> reports = getReports("../../data.txt");

            //go through reports and check if they are safe
            foreach (int[] report in reports)
            {
                //check if the report is safe
                bool safe = isSafe(report);
                //if the report is safe, increment the count
                if (safe)
                {
                    safeCount++;
                }
            }

            //print the number of safe reports
            Console.WriteLine($"Number of safe reports: {safeCount}");
        }
    }
}
