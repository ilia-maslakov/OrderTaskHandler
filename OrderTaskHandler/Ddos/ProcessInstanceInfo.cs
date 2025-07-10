namespace OrderTaskHandler.Ddos
{

    /// <summary>
    /// Информация об экземпляре процесса Camunda.
    /// </summary>
    public class ProcessInstanceInfo
    {
        /// <summary>
        /// Уникальный идентификатор экземпляра процесса.
        /// </summary>
        public string Id { get; set; } = default!;

        /// <summary>
        /// Идентификатор определения процесса.
        /// </summary>
        public string DefinitionId { get; set; } = default!;

        /// <summary>
        /// Бизнес-ключ экземпляра процесса.
        /// </summary>
        public string BusinessKey { get; set; } = default!;

        /// <summary>
        /// Идентификатор связанного экземпляра кейса (если есть).
        /// </summary>
        public string CaseInstanceId { get; set; } = default!;

        /// <summary>
        /// Признак завершённости экземпляра процесса.
        /// </summary>
        public bool Ended { get; set; }

        /// <summary>
        /// Признак приостановленного состояния процесса.
        /// </summary>
        public bool Suspended { get; set; }

        /// <summary>
        /// Идентификатор арендатора (tenant), если используется multi-tenancy.
        /// </summary>
        public string TenantId { get; set; } = default!;
    }
}