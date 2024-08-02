﻿using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using TGbot.Controllers;

namespace TGbot
#pragma warning disable format
{ 
#pragma warning restore format

    internal class Bot : BackgroundService
    {
        // Клиент к Telegram Bot API

        private ITelegramBotClient _telegramClient;


        // Контроллеры различных видов сообщений

        private InlineKeyboardController _inlineKeyboardController;


        private TextMessageController _textMessageController;


        private DefaultMessageController _defaultMessageController;



        public Bot(

                ITelegramBotClient telegramClient,
              InlineKeyboardController inlineKeyboardController,
             TextMessageController textMessageController,

               DefaultMessageController defaultMessageController)
        {
            _telegramClient = telegramClient;
            _inlineKeyboardController = inlineKeyboardController;
            _textMessageController = textMessageController;

            _defaultMessageController = defaultMessageController;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)

#pragma warning disable format
        

        {
            _telegramClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions() { AllowedUpdates = { } }, // Здесь выбираем, какие обновления хотим получать. В данном случае - разрешены все
                cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен.");
        }
#pragma warning restore format

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            //  Обрабатываем нажатия на кнопки  из Telegram Bot API: https://core.telegram.org/bots/api#callbackquery
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyboardController.Handle(update.CallbackQuery, cancellationToken);
                return;
            }

            // Обрабатываем входящие сообщения из Telegram Bot API: https://core.telegram.org/bots/api#message
            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {

                    case MessageType.Text:
                        await _textMessageController.Handle(update.Message, cancellationToken);
                        return;
                    default:
                        await _defaultMessageController.Handle(update.Message, cancellationToken);
                        return;
                }
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }

#pragma warning disable format
        
    }

}