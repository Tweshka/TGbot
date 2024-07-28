using System;
using System.Linq;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        private static string BotToken = "7307550605:AAHPSiu5VLY3J41afiHV9x-OtDLH798VGSw"; 
        private static TelegramBotClient botClient;

        static void Main(string[] args)
        {
            botClient = new TelegramBotClient(BotToken);

            Console.WriteLine("Подключение к боту...");
            botClient.StartReceiving();
            Console.WriteLine("Бот запущен. Нажмите Ctrl+C для завершения работы.");

            botClient.OnMessage += BotOnMessageReceived;

            Thread.Sleep(int.MaxValue);
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            Console.WriteLine($"Получено сообщение: {message.Text} от {message.Chat.Id}");

            if (message.Text == "/start")
            {
                // Отправка приветственного сообщения с меню
                var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { "Подсчёт символов" },
                    new KeyboardButton[] { "Сумма чисел" }
                });
                await botClient.SendTextMessageAsync(message.Chat.Id, "Привет!  Выберите действие:", replyMarkup: keyboard);
            }
            else
            {
                // Обработка выбранного действия
                switch (message.Text)
                {
                    case "Подсчёт символов":
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Отправьте текст, чтобы подсчитать символы.");
                        botClient.OnMessage += CountCharacters;
                        break;

                    case "Сумма чисел":
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Отправьте числа через пробел, чтобы посчитать сумму.");
                        botClient.OnMessage += CalculateSum;
                        break;

                    default:
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Неверный выбор. Пожалуйста, выберите действие из меню.");
                        break;
                }
            }
        }

        // Обработчик для подсчета символов
        private static async void CountCharacters(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            int count = message.Text.Length;
            await botClient.SendTextMessageAsync(message.Chat.Id, $"В вашем сообщении {count} символов.");
            botClient.OnMessage -= CountCharacters;
        }

        // Обработчик для вычисления суммы чисел
        private static async void CalculateSum(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            var numbers = message.Text.Split(' ').Select(int.Parse).ToArray();
            int sum = numbers.Sum();
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел: {sum}");
            botClient.OnMessage -= CalculateSum;
        }
    }
}