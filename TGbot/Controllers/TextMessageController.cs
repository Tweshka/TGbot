using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TGbot.Services;
using TGbot.Unities;

namespace TGbot.Controllers;

public class TextMessageController
{
    private readonly ITelegramBotClient _telegramClient;
    private readonly TextFunction _textFunction;
    private readonly IStorage _memoryStorage;
    public TextMessageController(ITelegramBotClient telegramBotClient,
        TextFunction textFunction, IStorage memoryStorage)


    {
        _telegramClient = telegramBotClient;
        _memoryStorage = memoryStorage;
        _textFunction = textFunction;
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
                    InlineKeyboardButton.WithCallbackData($"Подсчет" , $"len"),
                    InlineKeyboardButton.WithCallbackData($"вычисление" , $"sum"),
                });
                // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот умеет считать=)опробуйте.</b> {Environment.NewLine}" +
                    $"{Environment.NewLine}Можно что-то посчитать =).{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                break;
            default:
                switch (_memoryStorage.GetSession(message.Chat.Id).TextTask)
                {
                    case "len":
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                            $"В вашем собщение {_textFunction.GetStringLength(message.Text)} символов.", cancellationToken: ct);
                        break;
                    case "sum":
                        int? sum = _textFunction.GetSumNumbers(message.Text);
                        if (sum != null)
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                                $"Сумма чисел: {sum}", cancellationToken: ct);
                        else
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                                $"Невозможно вычислить сумму!", cancellationToken: ct);
                        break;
                    default:
                        break;
                }
                break;
        }
    }
}

