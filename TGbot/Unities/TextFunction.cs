using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;


namespace TGbot.Unities
{
    class TextFunction
    {


        public class StringAndNumberUtils
        {
            // Метод для определения длины строки
#pragma warning disable CA1822 // Пометьте члены как статические
            public int GetStringLength(string text)
#pragma warning restore CA1822 // Пометьте члены как статические
            {
                if (text == null)
                {
                    return 0;
                }
                else
                {
                    return text.Length;
                }
            }

            // Метод для вычисления суммы чисел
#pragma warning disable CA1822 // Пометьте члены как статические
            public int SumNumbers(string numbersString)
#pragma warning restore CA1822 // Пометьте члены как статические
            {
                string[] numbers = numbersString.Split(' ');

                int sum = 0;
                foreach (string number in numbers)
                {
                    if (int.TryParse(number, out int parsedNumber))
                    {
                        sum += parsedNumber;
                    }
                }
                return sum;
            }
        }
    }
}
