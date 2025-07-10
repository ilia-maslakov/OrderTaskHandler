namespace OrderTaskHandler.Ddos
{
    /// <summary>
    /// DTO для передачи данных при входе пользователя.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Имя пользователя, используемое для аутентификации.
        /// </summary>
        public string Username { get; set; } = default!;

        /// <summary>
        /// Пароль пользователя, используемый для аутентификации.
        /// </summary>
        public string Password { get; set; } = default!;
    }
}
