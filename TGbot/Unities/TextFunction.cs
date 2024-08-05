
namespace TGbot.Unities
{
   public class TextFunction
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
            public int GetSumNumbers(string numbersString)
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
