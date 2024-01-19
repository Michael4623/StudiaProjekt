using System.ComponentModel.DataAnnotations;

namespace ExamWebsite.Models
{
    public class Questions
    {
        [Key]
        public int Question_ID { get; set; }
        public int Correct_ID { get; set; }
        public string? Question { get; set; }
        public string? PicturePath { get; set; }
        public string? Subject { get; set; }
        public string? Section { get; set; }
    }
}
