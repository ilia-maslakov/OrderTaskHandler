namespace SampleCamundaWorker.Ddos
{
    public class ApproveOrderDto
    {
        public required string TaskId { get; set; }
        public bool Approve { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }
}