namespace OrderTaskHandler;

/// <summary>
/// Настройки JWT для аутентификации и авторизации.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Ключ для подписи JWT-токенов.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Издатель (Issuer) JWT-токенов.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Аудитория (Audience) JWT-токенов.
    /// </summary>
    public string Audience { get; set; } = string.Empty;
}
