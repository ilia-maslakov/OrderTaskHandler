using System.Threading.Tasks;
using SampleCamundaWorker.Ddos;
using SampleCamundaWorker.Infrastructure.Camunda.Models;

namespace SampleCamundaWorker.Services
{
    /// <summary>
    /// Интерфейс для взаимодействия с Camunda BPM.
    /// </summary>
    public interface ICamundaClient
    {
        /// <summary>
        /// Запускает новый экземпляр процесса в Camunda BPM с заданным ключом процесса и бизнес-ключом.
        /// </summary>
        /// <param name="processKey"></param>
        /// <param name="businessKey"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        Task StartProcessInstanceAsync(string processKey, string businessKey, CamundaVariables variables);

        /// <summary>
        /// Завершает задачу пользователя в Camunda BPM с заданным идентификатором задачи и переменными.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        Task CompleteUserTaskAsync(string taskId, string formKey, CamundaVariables variables);

        /// <summary>
        /// Получает информацию о процессе в Camunda BPM по заданному идентификатору процесса.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns>Задача, представляющая асинхронную операцию, возвращающая информацию о процессе.</returns>
        Task<ProcessInstanceInfo> GetProcessInfoAsync(string processId);

        /// <summary>
        /// Завершает внешнюю задачу (External Task) в Camunda BPM по её идентификатору.
        /// </summary>
        /// <param name="taskId">Идентификатор external task.</param>
        /// <param name="variables">Переменные, которые будут переданы в процесс.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task CompleteExternalTaskAsync(string taskId, CamundaVariables variables);

        /// <summary>
        /// Отправляет сообщение в Camunda BPM (correlate message).
        /// </summary>
        /// <param name="messageName">Имя сообщения (должно соответствовать catch event).</param>
        /// <param name="businessKey">Бизнес-ключ, по которому будет найден процесс.</param>
        /// <param name="variables">Переменные, передаваемые вместе с сообщением.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task CorrelateMessageAsync(string messageName, string businessKey, CamundaVariables variables);
    }
}
