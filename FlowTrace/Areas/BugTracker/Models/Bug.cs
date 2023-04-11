namespace FlowTrace.Areas.BugTracker.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Assignee { get; set; }
        public string Reporter { get; set; }
        public string Status { get; set; }
        public string StatusCategory { get; set; }
        public string Type { get; set; }
    }
}
