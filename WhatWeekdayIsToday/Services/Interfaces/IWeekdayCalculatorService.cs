using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatWeekdayIsToday.Services.Interfaces
{
    public interface IWeekdayCalculatorService
    {
        string GetWeekday(int day, int month, int year);
        bool IsLeapYear(int year);
    }
}
