using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SampleCamundaWorker.Ddos;
using SampleCamundaWorker.Infrastructure.Camunda.Models;

namespace SampleCamundaWorker.Services
{

    /// <summary>
    /// Сервис для управления заказами.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly ICamundaClient _camunda;
        const string BPMN_ORDER_PROCESS_NAME = "process-t";

        /// <summary>
        /// Инициализирует новый экземпляр сервиса заказов.
        /// </summary>
        /// <param name="camunda"></param>
        public OrderService(ICamundaClient camunda)
        {
            _camunda = camunda;
        }

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
                ["name"] = new CamundaVariable { Value = order.Name, Type = "String" },
                ["orderNumber"] = new CamundaVariable { Value = order.OrderNumber, Type = "String" },
                ["orderDate"] = new CamundaVariable { Value = order.OrderDate.ToString("o"), Type = "String" },
                ["remarks"] = new CamundaVariable { Value = order.Remarks, Type = "String" }
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
                ["name"] = new CamundaVariable { Value = order.Name, Type = "String" },
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
                ["approve"] = new CamundaVariable { Value = dto.Approve ? "yes" : "no", Type = "String" },
                ["remarks"] = new CamundaVariable { Value = dto.Remarks, Type = "String" }
            };
            await _camunda.CompleteUserTaskAsync(dto.TaskId, formKey, variables);
        }
    }
}