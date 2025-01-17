﻿using Telegram.Bot;
using Telegram.Bot.Types;

namespace TGbot.Controllers;
public class DefaultMessageController

{
    private readonly ITelegramBotClient _telegramClient;
    public DefaultMessageController(ITelegramBotClient telegramBotClient)

    {
        _telegramClient = telegramBotClient;
    }

    public async Task Handle(Message message, CancellationToken ct)

    {
        Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Получено текстовое сообщение", cancellationToken: ct);
    }
}

