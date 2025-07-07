using System;

/// <summary>
/// DTO для создания нового заказа.
/// </summary>
public class CreateOrderDto
{
    /// <summary>
    /// Номер заказа.
    /// </summary>

    public required string OrderNumber { get; set; }

    /// <summary>
    /// Описание заказа.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Дата заказа.
    /// </summary>
    /// <value>Дата и время создания заказа.</value>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Дополнительные примечания по заказу.
    /// </summary>
    public string Remarks { get; set; } = string.Empty;
}
