using Telegram.Bot;
using Telegram.Bot.Types;

namespace TGbot.Controllers;

public class DefaultMessageController
#pragma warning disable format
    {
#pragma warning restore format
#pragma warning disable format
        private readonly ITelegramBotClient _telegramClient;
#pragma warning restore format

        public DefaultMessageController(ITelegramBotClient telegramBotClient)
#pragma warning disable format
        {
            _telegramClient = telegramBotClient;
        }
#pragma warning restore format
        public async Task Handle(Message message, CancellationToken ct)
#pragma warning disable format
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Получено текстовое сообщение", cancellationToken: ct);
        }
#pragma warning restore format
#pragma warning disable format
    }
#pragma warning restore format
