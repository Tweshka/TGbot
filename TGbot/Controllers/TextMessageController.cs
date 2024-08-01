using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TGbot.Controllers;

public class TextMessageController
{
    private readonly ITelegramBotClient _telegramClient;

#pragma warning disable IDE0079 // Удалить ненужное подавление
#pragma warning disable IDE0290 // Использовать основной конструктор
    public TextMessageController(ITelegramBotClient telegramBotClient)
#pragma warning restore IDE0079 // Удалить ненужное подавление
#pragma warning disable IDE0079 // Удалить ненужное подавление
#pragma warning restore IDE0290 // Использовать основной конструктор
    {
        _telegramClient = telegramBotClient;
    }
#pragma warning restore IDE0079 // Удалить ненужное подавление

    public async Task Handle(Message message, CancellationToken ct)
    {
        switch (message.Text)
        {
            case "/start":

                // Объект, представляющий кноки
#pragma warning disable IDE0028 // Упростите инициализацию коллекции
                var buttons = new List<InlineKeyboardButton[]>();
#pragma warning restore IDE0028 // Упростите инициализацию коллекции
#pragma warning disable IDE0300 // Упростите инициализацию коллекции
                buttons.Add(new[]
                {
                    InlineKeyboardButton.WithCallbackData($"Подсчет" , $"Счет"),
                    InlineKeyboardButton.WithCallbackData($"вычисление" , $"Вычисление"),
                });
#pragma warning restore IDE0300 // Упростите инициализацию коллекции

                // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот умеет считать=)опробуйте.</b> {Environment.NewLine}" +
                    $"{Environment.NewLine}Можно что-то посчитать =).{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                break;
            default:
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Посчитайте,что хотите=).", cancellationToken: ct);
                break;
        }
    }
}