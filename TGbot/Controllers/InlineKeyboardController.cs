using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TGbot.Services;

namespace TGbot.Controllers;

public class InlineKeyboardController
{
    readonly Services.IStorage _memoryStorage;
    private readonly ITelegramBotClient _telegramClient;

    public InlineKeyboardController(ITelegramBotClient telegramBotClient, Services.IStorage memoryStorage)
    {
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
    }


    private async Task Handle1(CallbackQuery? callbackQuery, CancellationToken ct)

    {
        if (callbackQuery?.Data == null)
            return;

        // Обновление пользовательской сессии новыми данными
        _memoryStorage.GetSession(callbackQuery.From.Id).numberse = callbackQuery.Data;

        // Генерим информационное сообщение
        string numbers = callbackQuery.Data switch
        {
            "Подсчет" => " подсчёт количества символов в тексте",
            "вычисление" => " вычисление суммы чисел",
            _ => String.Empty
        };

        // Отправляем в ответ уведомление о выборе
        await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
            $"<b>Счет - {numbers}.{Environment.NewLine}</b>" +
            $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
    }


    internal async Task Handle(CallbackQuery? callbackQuery, CancellationToken cancellationToken)

    {
        throw new NotImplementedException();
    }
}
