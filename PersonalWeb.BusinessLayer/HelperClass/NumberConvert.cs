﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.HelperClass
{
    public static class NumberConvert
    {
        public static string PersianToEnglish(this string persianStr)
        {
            Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9',
                ['0'] = '0',
                ['1'] = '1',
                ['2'] = '2',
                ['3'] = '3',
                ['4'] = '4',
                ['5'] = '5',
                ['6'] = '6',
                ['7'] = '7',
                ['8'] = '8',
                ['9'] = '9',
            };
            foreach (var item in persianStr)
            {
                persianStr = persianStr.Replace(item, LettersDictionary[item]);
            }
            return persianStr;
        }
    }
}
