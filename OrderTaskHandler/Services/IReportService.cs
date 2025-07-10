using System.Threading;
using System.Threading.Tasks;

namespace OrderTaskHandler.Services
{
    /// <summary>
    /// Интерфейс для сервиса управления заказами.
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Асинхронно генерирует отчет.
        /// </summary>
        /// <param name="businessKey">Ключ бизнеса, для отслеживания выполнения задачи.</param>
        /// <param name="cancellationToken">Токен отмены для управления асинхронной операцией.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task GenerateReportAsync(string businessKey, CancellationToken cancellationToken);
    }
}