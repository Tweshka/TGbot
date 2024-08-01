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
            public int GetStringLength(string text)
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
            public int SumNumbers(string numbersString)
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
