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

#pragma warning disable IDE0051 // Удалите неиспользуемые закрытые члены
    private async Task Handle1(CallbackQuery? callbackQuery, CancellationToken ct)
#pragma warning restore IDE0051 // Удалите неиспользуемые закрытые члены
    {
        if (callbackQuery?.Data == null)
            return;

        // Обновление пользовательской сессии новыми данными
        _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

        // Генерим информационное сообщение
        string languageText = callbackQuery.Data switch
        {
            "Подсчет" => " подсчёт количества символов в тексте",
            "вычисление" => " вычисление суммы чисел",
            _ => String.Empty
        };

        // Отправляем в ответ уведомление о выборе
        await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
            $"<b>Язык аудио - {languageText}.{Environment.NewLine}</b>" +
            $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
    }

#pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
    internal async Task Handle(CallbackQuery? callbackQuery, CancellationToken cancellationToken)
#pragma warning restore CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
    {
        throw new NotImplementedException();
    }
}
