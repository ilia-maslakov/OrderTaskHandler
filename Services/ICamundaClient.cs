using System.Threading.Tasks;
using SampleCamundaWorker.Ddos;


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
    Task StartProcessInstanceAsync(string processKey, string businessKey, object variables);

    /// <summary>
    /// Завершает задачу пользователя в Camunda BPM с заданным идентификатором задачи и переменными.
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="variables"></param>
    /// <returns></returns>
    Task CompleteUserTaskAsync(string taskId, object variables);

    /// <summary>
    /// Получает информацию о процессе в Camunda BPM по заданному идентификатору процесса.
    /// </summary>
    /// <param name="processId"></param>
    /// <returns></returns>
    Task<ProcessInstanceInfo> GetProcessInfoAsync(string processId);
}
