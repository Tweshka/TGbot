using System.Collections.Concurrent;
using TGbot.Models;


namespace TGbot.Services
{
    public class MemoryStorage : IStorage
    {
        /// <summary>
        /// Хранилище сессий
        /// </summary>
        private readonly ConcurrentDictionary<long, Models.Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Models.Session>();
        }


        public Models.Session GetSession(long chatId)

        {
            // Возвращаем сессию по ключу, если она существует
#pragma warning disable CA1854 // Предпочитать метод "IDictionary.TryGetValue(TKey, out TValue)"
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];
#pragma warning restore CA1854 // Предпочитать метод "IDictionary.TryGetValue(TKey, out TValue)"

            // Создаем и возвращаем новую, если такой не было
            var newSession = new Models.Session() { LanguageCode = "ru" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}