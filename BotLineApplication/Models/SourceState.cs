using Dapper.Contrib.Extensions;

namespace BotLineApplication.Models
{
    [Table("SourceState")]
    public class SourceState
    {
        [ExplicitKey]
       // public int Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string GroupName { get; set; }
        public string Room { get; set; }
        public string SourceType { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
