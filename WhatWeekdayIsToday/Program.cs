// See https://aka.ms/new-console-template for more information

using System;
using Services.WeekdayCalculator;
using WhatWeekdayIsToday.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

try
{
    IWeekdayCalculatorService weekdayCalculator = new WeekdayCalculatorService();
    int day = 0, month = 0, year = 0;
    bool exitProgram = false;
    string Weekday = "";    

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("This is Program for solve the question 'What weekday is today?'");
    Console.WriteLine("===================================================================================================");

    while (!exitProgram)
    {
        Console.Write("input day, month, Christian year (Range of year is from 1900 afterward) Ex. 31/12/1900 : ");
        int maxLength = 10;

        StringBuilder input = new StringBuilder();

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            // เมื่อกด Enter ให้จบการรับอินพุต
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                break;
            }
            // เมื่อกด Backspace ให้ลบตัวอักษรสุดท้าย
            else if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input.Length--;
                // ลบตัวอักษรที่แสดงบนหน้าจอด้วย
                Console.Write("\b \b");
            }
            // ถ้าความยาวน้อยกว่าที่กำหนด ให้เพิ่มตัวอักษรลงใน input
            else if (input.Length < maxLength)
            {
                char character = keyInfo.KeyChar;
                if (!char.IsControl(character)) // ตรวจสอบไม่ให้เป็นอักขระควบคุม
                {
                    input.Append(character);
                    Console.Write(character); // แสดงตัวอักษรบนหน้าจอ
                    Weekday = input.ToString();
                }
            }
        }
        Console.WriteLine();

        string[] parts = Weekday.Split('/');
        if (parts.Length != 3 ||
            !int.TryParse(parts[0], out day) ||
            !int.TryParse(parts[1], out month) ||
            !int.TryParse(parts[2], out year))
        {
            Console.WriteLine("Invalid date format. Please use DD/MM/YYYY.");
            continue; // กลับไปเริ่มต้น Loop ใหม่
        }

        if (year < 1900)
        {
            Console.WriteLine("Year must be 1900 or later.");
            continue;
        }
        else if (month < 1 || month > 12)
        {            
            Console.WriteLine("Month must be 1-12");
            continue;
        }
        else if (day < 1 || day > 31)
        {            
            Console.WriteLine("Day must be 1-31");
            continue;
        }
        else if ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30)
        {
            Console.WriteLine("The month you entered has only 30 days.");
            continue;
        }
        else if (month == 2 && weekdayCalculator.IsLeapYear(year) && day > 29)
        {            
            Console.WriteLine("February has only 28 days in a common year and 29 days in a leap year.");
            continue;
        }
        else if (month == 2 && !weekdayCalculator.IsLeapYear(year) && day > 28)
        {
            Console.WriteLine("February has only 28 days in a common year and 29 days in a leap year.");
            continue;
        }       
        else
        {
            Weekday = weekdayCalculator.GetWeekday(day, month, year);
            Console.WriteLine($"The weekday of the input date {day}/{month}/{year} is {Weekday}.");            
        }
        Console.WriteLine("===================================================================================================");
        // 3. รับข้อมูลจากผู้ใช้
        Console.Write("Play Again input (y/Y) Or Exit input (n/N) : ");
        string userInput = Console.ReadLine();

        // 4. ตรวจสอบเงื่อนไขเพื่อออกจาก Loop
        if (userInput.ToUpper().Trim() == "N")
        {
            exitProgram = true; // ตั้งค่าให้เป็น true เพื่อออกจาก Loop ในรอบถัดไป
            Console.WriteLine("Exit program...");
            Thread.Sleep(3000);
        }
        else
        {
            Console.WriteLine("This is Program for solve the question 'What weekday is today?'");
            Console.WriteLine("===================================================================================================");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
