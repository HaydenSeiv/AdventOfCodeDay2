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
        /// <summary>
        /// Method to check if a report is safe. Must be either ascending or descending, and the difference between any two adjacent numbers must be less than or equal to 3.
        /// </summary>
        /// <param name="report"></param>
        /// <returns> True or False </returns>
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

        /// <summary>
        /// Method to check if a report is safe with a dampener. A dampener allows us to remove one number from the report and check if it is safe.
        /// </summary>
        /// <param name="report"></param>
        /// <returns> True or False</returns>
        public static bool isSafeDampener(int[] report) 
        {
            //if the report is safe, it is also safe with the dampener
            if (isSafe(report))
            {
                return true;
            }

            //if not, remove a number and check again until all numbers have been removed
            for (int i = 0; i < report.Length; i++)
            {
                //create a new array without the current number
                int[] newReport = report.Where((val, index) => index != i).ToArray();
                //check if the new report is safe
                if (isSafe(newReport))
                {
                    return true;
                }
            }
            //if we reach here, report is not safe even with a dampener
            return false;
        }
        /// <summary>
        /// Method to read the file and turn each line into an int array.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method to count the number of safe reports.
        /// </summary>
        /// <param name="reports"></param>
        /// <returns></returns>
        public static int safeCount(List<int[]> reports)
        {
            int safeCount = 0;

            //go through reports and check if they are safe
            foreach (int[] report in reports)
            {
                //check if the report is safe
                bool safe = isSafeDampener(report);
                //if the report is safe, increment the count
                if (safe)
                {
                    safeCount++;
                }
            }

            return safeCount;

        }
        static void Main(string[] args)
        {
            //turn data into list of "reports"
            List<int[]> reports = getReports("../../data.txt");

            //print the number of safe reports
            Console.WriteLine($"Number of safe reports: {safeCount(reports)}");
        }
    }
}
