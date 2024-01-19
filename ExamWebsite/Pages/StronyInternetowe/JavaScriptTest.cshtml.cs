using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExamWebsite.Data;
using ExamWebsite.Models;

namespace ExamWebsite.Pages.StronyInternetowe
{
    public class JavaScriptTestModel : PageModel
    {
        private readonly ExamWebsite.Data.ApplicationDbContext _context;

        public JavaScriptTestModel(ExamWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public class QuestionTest
        {
            public string? QuestionText { get; set; }
            public string[]? Answers { get; set; }
            public string CorrectAnswer { get; set; }
        }

        private List<QuestionTest> questions { get; set; }
        public List<QuestionTest> QuestionsT { get; set; }

        public void OnGet()
        {
            var queryQuestions = _context.Questions.ToList();
            var queryAnswers = _context.Answers.ToList();

            List<QuestionTest> QuestionsU = new List<QuestionTest>();

            foreach (var item in queryQuestions)
            {
                if (item.Section.ToUpper() == "JAVASCRIPT")
                {
                    var answ = queryAnswers.Where(x => x.Question_ID == item.Question_ID).ToList();
                    var c_answ = queryAnswers.Where(x => x.Answer_ID == item.Correct_ID).ToList();

                    questions = new List<QuestionTest>()
                    {
                        
                        new QuestionTest
                        {
                            QuestionText = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(item.Question)),//item.Question,
                            Answers = answ.Select(x => x.Answer).ToArray(),
                            CorrectAnswer = c_answ.FirstOrDefault()?.Answer
                        },
                    };
                    QuestionsU.AddRange(questions);
                }
            }

            QuestionsT = QuestionsU.OrderBy(x => Guid.NewGuid())
                                   .Select(q => new QuestionTest
                                   {
                                       QuestionText = q.QuestionText,
                                       Answers = q.Answers,
                                       CorrectAnswer = q.CorrectAnswer
                                   })
                                   .Take(20)
                                   .ToList();
        }
    }
}
