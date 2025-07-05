using System.Threading.Tasks;

public interface ICamundaClient
{
    Task StartProcessInstanceAsync(string processKey, string businessKey, object variables);

    Task CompleteUserTaskAsync(string taskId, object variables);
}
