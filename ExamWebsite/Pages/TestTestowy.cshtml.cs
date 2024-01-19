using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExamWebsite.Models;

namespace ExamWebsite.Pages
{
    public class TestTestowyModel : PageModel
    {
        private readonly ExamWebsite.Data.ApplicationDbContext _context;
        public TestTestowyModel(ExamWebsite.Data.ApplicationDbContext context)
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
                var odpowiedzi = queryAnswers.Where(x => x.Question_ID == item.Question_ID).ToList();
                var poprawne = queryAnswers.Where(x => x.Answer_ID == item.Correct_ID).ToList();

                questions = new List<QuestionTest>()
                {
                    // Dane testowe
                    new QuestionTest
                    {
                        QuestionText = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(item.Question)),//item.Question,
                        Answers = odpowiedzi.Select(x => x.Answer).ToArray(),
                        CorrectAnswer = poprawne.FirstOrDefault()?.Answer
                    },
                };
                QuestionsU.AddRange(questions);
            }

            QuestionsT = QuestionsU.Select(q => new QuestionTest
            {
                QuestionText = q.QuestionText,
                Answers = q.Answers,
                CorrectAnswer = q.CorrectAnswer
            }).ToList();
        }
    }
}
