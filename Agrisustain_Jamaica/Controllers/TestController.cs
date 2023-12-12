//using Agrisustain_Jamaica.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;

//namespace Agrisustain_Jamaica.Controllers
//{
//    public class TestController : Controller
//    {
//        public ActionResult DripIrrigationTest()
//        {
//            // Retrieve Drip Irrigation questions from your data source
//            var dripIrrigationQuestions = GetDripIrrigationQuestions();
//            return View(dripIrrigationQuestions);
//        }

//        private List<DripIrrigationQuestion> GetDripIrrigationQuestions()
//        {
//            var questions = new List<DripIrrigationQuestion>
//            {
//                new DripIrrigationQuestion
//                {
//                    QuestionId = 1,
//                    QuestionText = "What is the primary advantage of drip irrigation over traditional methods?",
//                    Options = new List<string> { "Reduced water usage", "Faster crop growth", "Lower initial cost", "Minimal maintenance" },
//                    CorrectAnswer = "Reduced water usage"
//                },
//                new DripIrrigationQuestion
//                {
//                    QuestionId = 2,
//                    QuestionText = "How does drip irrigation contribute to water conservation?",
//                    Options = new List<string> { "It uses large volumes of water", "It promotes water runoff", "It delivers water directly to the plant roots", "It relies on frequent flooding" },
//                    CorrectAnswer = "It delivers water directly to the plant roots"
//                },
//                new DripIrrigationQuestion
//                {
//                    QuestionId = 3,
//                    QuestionText = "Which components are essential for a basic drip irrigation system?",
//                    Options = new List<string> { "Sprinklers and pumps", "Pipes, valves, and emitters", "Floodgates and canals", "Windmills and reservoirs" },
//                    CorrectAnswer = "Pipes, valves, and emitters"
//                },
//                new DripIrrigationQuestion
//                {
//                    QuestionId = 4,
//                    QuestionText = "What is the purpose of emitters in a drip irrigation system?",
//                    Options = new List<string> { "To control water pressure", "To measure soil moisture", "To deliver water to plants", "To filter out impurities" },
//                    CorrectAnswer = "To deliver water to plants"
//                },
//                new DripIrrigationQuestion
//                {
//                    QuestionId = 5,
//                    QuestionText = "How does drip irrigation contribute to improved crop yield?",
//                    Options = new List<string> { "By increasing soil erosion", "By promoting waterlogging", "By providing precise water delivery", "By relying on natural rainfall" },
//                    CorrectAnswer = "By providing precise water delivery"
//                },

//            };

//            return questions;
//        }
//    }
//}
