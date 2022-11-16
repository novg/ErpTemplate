namespace Domain.Enums;

/// <summary>
/// Описывает тип клиентов, от которых пришёл заказ.
/// </summary>
public enum ClientType
{
    Api,
    Email,
    Ftp,
    Telegram,
}