using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWebsite.Models
{
    public class Answers
    {
        [Key]
        public int Answer_ID { get; set; }
        public int Question_ID { get; set; }
        public string? Answer { get; set; }
    }
}
