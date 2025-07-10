using System;
using System.Threading.Tasks;
using OrderTaskHandler.Ddos;
using OrderTaskHandler.Infrastructure.Camunda.Models;

namespace OrderTaskHandler.Services
{

    /// <summary>
    /// Сервис для управления заказами.
    /// </summary>
    /// <remarks>
    /// Инициализирует новый экземпляр сервиса заказов.
    /// </remarks>
    /// <param name="camunda"></param>
    public class OrderService(ICamundaClient camunda) : IOrderService
    {
        private readonly ICamundaClient _camunda = camunda;
        const string BPMN_ORDER_PROCESS_NAME = "process-t";

        /// <summary>
        /// Создает новый заказ.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Возвращает ID bpmn процесса.</returns>
        public async Task<string> CreateOrderAsync(CreateOrderDto order)
        {
            const string formKey = "order/create";
            var businessKey = Guid.NewGuid().ToString();

            var variables = new CamundaVariables
            {
                ["name"] = new CamundaVariable(order.Name),
                ["orderNumber"] = new CamundaVariable(order.OrderNumber),
                ["orderDate"] = new CamundaVariable(order.OrderDate.ToString("o")),
                ["remarks"] = new CamundaVariable(order.Remarks),
                ["neededit"] = new CamundaVariable("no"),
            };

            await _camunda.StartProcessInstanceAsync(BPMN_ORDER_PROCESS_NAME, businessKey, variables);

            await _camunda.CompleteUserTaskAsync(businessKey, formKey, variables);

            return businessKey;
        }

        /// <summary>
        /// Редактирует существующий заказ.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task EditOrderAsync(EditOrderDto order)
        {
            const string formKey = "order/edit";
            var variables = new CamundaVariables
            {
                ["name"] = new CamundaVariable(order.Name),
            };
            await _camunda.CompleteUserTaskAsync(order.BusinessKey, formKey, variables);
        }

        /// <summary>
        /// Одобряет заказ, который был создан ранее и ожидает одобрения.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task ApproveOrderAsync(ApproveOrderDto dto)
        {
            const string formKey = "order/approve";
            var variables = new CamundaVariables
            {
                ["approve"] = new CamundaVariable(dto.Approve ? "yes" : "no"),
                ["remarks"] = new CamundaVariable(dto.Remarks)
            };
            await _camunda.CompleteUserTaskAsync(dto.TaskId, formKey, variables);
        }
    }
}