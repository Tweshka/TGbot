using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TGbot.Services;
namespace TGbot.Controllers;
public class InlineKeyboardController
{
    readonly IStorage _memoryStorage;
    private readonly ITelegramBotClient _telegramClient;

    public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
    {
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
    }
    public async Task Handle1(CallbackQuery? callbackQuery, CancellationToken ct)
    {
        if (callbackQuery?.Data == null)
            return;

        // Обновление пользовательской сессии новыми данными
        _memoryStorage.GetSession(callbackQuery.From.Id).numberse = callbackQuery.Data;

        // Генерим информационное сообщение
        string numbers = callbackQuery.Data switch
        {
           "Подсчет" => "Счет",
            "вычисление" => "вычисление",
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