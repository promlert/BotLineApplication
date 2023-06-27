using Dapper.Contrib.Extensions;

namespace BotLineApplication.Models
{
    [Table("LogMessage")]
    public class LogMessage
    {
        [Key] 
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
