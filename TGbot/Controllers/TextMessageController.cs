using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TGbot.Controllers;

public class TextMessageController
{
    private readonly ITelegramBotClient _telegramClient;


    public TextMessageController(ITelegramBotClient telegramBotClient)

    {
        _telegramClient = telegramBotClient;
    }


    public async Task Handle(Message message, CancellationToken ct)
    {
        switch (message.Text)
        {
            case "/start":

                // Объект, представляющий кноки

                var buttons = new List<InlineKeyboardButton[]>();

                buttons.Add(new[]
                {
                    InlineKeyboardButton.WithCallbackData($"Подсчет" , $"Счет"),
                    InlineKeyboardButton.WithCallbackData($"вычисление" , $"Вычисление"),
                });


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