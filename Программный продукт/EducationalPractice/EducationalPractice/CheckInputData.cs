using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EducationalPractice
{
    public partial class CheckInputData
    {
        // Защита от SQLI
        public bool checkSQLI(string example)
        {
            if (example.Length == 0)
                return false;
            string[] incorrectSymbols = new string[3] { "'", "-", ";"};
            for(int symbolIndex = 0; symbolIndex < 3; symbolIndex++)
            {
                if (example.Contains(incorrectSymbols[symbolIndex]))
                    return false;
            }
            return true;
        }

        // Проверка номера телефона
        public bool controlTelephoneNumbers(string telephoneNumberData)
        {
            Regex outPlusPhoneNumberController = new Regex(
                "[8][0-9]{10}");

            Console.WriteLine("Results 1: " + outPlusPhoneNumberController.IsMatch(telephoneNumberData));
            return outPlusPhoneNumberController.IsMatch(telephoneNumberData);


        }

    }
}
