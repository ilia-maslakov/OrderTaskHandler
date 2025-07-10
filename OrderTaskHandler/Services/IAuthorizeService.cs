namespace OrderTaskHandler.Services
{

    /// <summary>
    /// Интерфейс для сервиса авторизации.
    /// </summary>
    public interface IAuthorizeService
    {
        /// <summary>
        /// Генерирует JWT-токен для пользователя с заданным именем и ролью.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        string GenerateToken(string username, string role);

        /// <summary>
        /// Проверяет учетные данные пользователя (имя пользователя и пароль).
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Validate(string username, string password);

        /// <summary>
        /// Получает роль пользователя по его имени.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string? GetRole(string username);
    }
}