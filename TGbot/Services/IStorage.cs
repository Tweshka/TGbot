using TGbot.Models;

namespace TGbot.Services
{
    public interface IStorage
#pragma warning disable format
{
#pragma warning restore format
        /// <summary>
        /// Получение сессии пользователя по идентификатору
        /// </summary>
        Session GetSession(long chatId);
#pragma warning disable format
} }
#pragma warning restore format