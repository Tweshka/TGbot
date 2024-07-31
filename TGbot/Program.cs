using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using TelegramBot;
using VoiceTexterBot.Services;
using TextMessageController;
using VoiceTexterBot.Controllers;

namespace VoiceTexterBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddTransient<TextFunction>();
            services.AddSingleton<IStorage, MemoryStorage>();  
            services.AddTransient<TextMessageController>();
            services.AddTransient<DefaultMessageController>;
            services.AddTransient<InlineKeyboardController>;
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("7307550605:AAHPSiu5VLY3J41afiHV9x-OtDLH798VGSw"));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }
    }
}