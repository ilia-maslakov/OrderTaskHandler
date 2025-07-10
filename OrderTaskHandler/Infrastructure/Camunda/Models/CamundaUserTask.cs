namespace OrderTaskHandler.Infrastructure.Camunda.Models
{
    /// <summary>
    /// Представляет переменные, используемые в Camunda BPM для пользовательских задач и процессов.
    /// </summary>
    public class CamundaUserTask
    {
        /// <summary>
        /// Уникальный идентификатор задачи.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Имя связанной формы.
        /// </summary>
        public string FormKey { get; set; } = string.Empty;
    }
}
