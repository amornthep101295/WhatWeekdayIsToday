using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatWeekdayIsToday.Services.Interfaces;

namespace Services.WeekdayCalculator
{
    public class WeekdayCalculatorService : IWeekdayCalculatorService
    {
        // A constant array to store the names of the weekdays for mapping the result.
        private static readonly string[] weekdays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        // Index 0 is a placeholder, so we can use month numbers (1-12) directly as indices.
        private static readonly int[] daysInMonth = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        // <returns>True if the year is a leap year, otherwise false.</returns>
        public bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        // <returns>The total number of days passed in the year up to the given date.</returns>
        private int CalculateDaysInYear(int day, int month, int year)
        {
            try
            {
                int totalDays = 0;
                // Use a temporary array to account for leap year day count.
                int[] currentDaysInMonth = (int[])daysInMonth.Clone();
                if (IsLeapYear(year))
                {
                    currentDaysInMonth[2] = 29; // February
                }

                for (int m = 1; m < month; m++)
                {
                    totalDays += currentDaysInMonth[m];
                }

                totalDays += (day - 1); // Add the days of the current month (minus one since we count from day 0)

                return totalDays;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return -1;
            }            
        }

        public string GetWeekday(int day, int month, int year)
        {   
            try
            {                
                int totalDays = 0;

                // 1. Calculate days for each full year passed since 1900.
                for (int y = 1900; y < year; y++)
                {
                    totalDays += IsLeapYear(y) ? 366 : 365;
                }

                // 2. Add days for the months and day of the current year.
                totalDays += CalculateDaysInYear(day, month, year);

                // 3. The reference date Jan 1, 1900 was a Monday, which corresponds to index 0.
                // We find the remainder of the total days divided by 7.
                int weekdayIndex = totalDays % 7;

                return weekdays[weekdayIndex];
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return "Error";
            }            
        }
    }
}
