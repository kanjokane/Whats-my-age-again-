using System;
using System.Globalization;
using System.Threading;
using static System.Console;

namespace SocialSecurityNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            bool wrongInput = true;           
            do
            {
                Write("Social Security Number: ");

                string socialSecurityNumber = ReadLine();

                if (CalculateAge(socialSecurityNumber) != 0)
                {
                    int age = CalculateAge(socialSecurityNumber);

                    bool isFemale = IsFemale(socialSecurityNumber);

                    string gender = isFemale ? "Female" : "Male";

                    WriteLine($"{age}, {gender}");
                    SleepAndClear(2);
                    wrongInput = false;
                }
            } while (wrongInput);
        }
        static int CalculateAge(string socialSecurityNumber)
        {
            try
            {
                // Skapa ny textsträng (string) som utgörs av samma första 6 tecken som i 
                // värdet som lagras i variabeln "socialSecurityNumber", t.ex. "900101".
                string datePart = socialSecurityNumber.Substring(0, 6);


                // "Parsa" textsträngen (t.ex. "900101-2010") till ett nytt värde av typ DateTime.
                DateTime dateOfBirth = DateTime.ParseExact(datePart, "yyMMdd", CultureInfo.InvariantCulture);


                // Räkna ut hur många år som gått mellan dagens år och födelseårdet.
                // Detta ger oss ett heltal som kommer vara av typ "int" och därför fångar
                // vi upp detta i en variabeln deklarerad att hålla en "int".
                int age = DateTime.Now.Year - dateOfBirth.Year;  // 

                // När vi har uttryck som det nedan, där vi kontrollerar om vi inte har 
                // passerat födelsedag, är det en bra idé att skapa en variabel som fångar upp
                // resultatet av uttrycket, och ger denna ett beskrivande namn. Tänk på att
                // kod är dokumentation - vi dokumenterar vår avsikt.
                bool haveNotPassedBirthDay =
                    dateOfBirth.Month > DateTime.Now.Month ||
                    dateOfBirth.Month == DateTime.Now.Month && dateOfBirth.Day > DateTime.Now.Day;

                if (haveNotPassedBirthDay)
                {
                    // Kortform av "age = age - 1".
                    age--;
                }

                return age;
            }
            catch 
            {
                Clear();
                WriteLine("Please write in the correct format \"YYMMDDXXXX\"");
                SleepAndClear(2);
            }
            return 0;

        }
        static bool IsFemale(string socialSecurityNumber)
        {
            string genderNumberPart = socialSecurityNumber.Substring(socialSecurityNumber.Length - 2, 1);

            // Konvertera värdet till en int ("int" är alias för System.Int32, därav ToInt32()).
            int genderIdentifier = Convert.ToInt32(genderNumberPart); // "1" -> 1

            //string gender;

            //if (genderIdentifier % 2 == 0)
            //{
            //    gender = "female";
            //}
            //else
            //{
            //    gender = "male";
            //}

            bool isFemale = genderIdentifier % 2 == 0;

            return isFemale;
        }
        static void SleepAndClear(int seconds)
        {
            Thread.Sleep(seconds * 1000);
            Clear();
        }
        
    }
}
