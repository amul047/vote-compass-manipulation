using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace VoteCompass
{
    class Program
    {
        private static int NumberOfSubmissions = 10;
        private static int ClickDelayInMilliseconds = 50;
        private static int DontKnowAnswer = 6;

        private static Dictionary<string, int> QuestionAnswers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"How much tax should corporations pay?",DontKnowAnswer},
            {"How much of a role should the Treaty of Waitangi have in New Zealand law?",DontKnowAnswer},
            {"How much debt should the government incur to address the COVID-19 crisis?",DontKnowAnswer},
            {"The government should cover the cost of dental care for adults with low incomes.",DontKnowAnswer},
            {"The government should provide free lunches to students in state schools.",5},
            {"Statues of historical figures deemed as racist should be removed from public spaces.",DontKnowAnswer},
            {"How many immigrants should New Zealand admit?",DontKnowAnswer},
            {"How much should the government spend on rehabilitation services to address drug abuse?",5},
            {"Violent offenders under 18 years old should be sentenced as adults.",1},
            {"New Zealand should be less reliant on other countries for its goods and services.",DontKnowAnswer},
            {"The government should restore funding for charter schools.",1},
            {"How much should wealthier people pay in taxes?",5},
            {"The government should guarantee a minimum income for all New Zealanders regardless of whether or not they have a job.",5},
            {"How difficult should it be for businesses to dismiss new employees?",DontKnowAnswer},
            {"How much should the government do to make amends for injustices committed against Maori?",DontKnowAnswer},
            {"Abortion up to twenty weeks should not require medical approval.",5},
            {"The government should impose a royalty on companies that export New Zealand water.",DontKnowAnswer},
            {"New Zealand should deport foreigners who are convicted of a criminal offence.",DontKnowAnswer},
            {"How high should the minimum wage be?",DontKnowAnswer},
            {"How much money should the government invest in the development of railways?",DontKnowAnswer},
            {"People should be allowed to change the sex listed on their birth certificate, without having to undergo a medical treatment to change their gender.",1},
            {"How difficult should it be to access welfare in New Zealand?",DontKnowAnswer},
            {"How much should New Zealand do to reduce its greenhouse gas emissions?",5},
            {"The government should ease restrictions on property developers.",5},
            {"The government should prevent foreign ownership of residential properties.",5},
            {"How much should the government limit oil and gas exploration in New Zealand?",DontKnowAnswer},
            {"Cannabis should be legalised.",DontKnowAnswer},
            {"How difficult should it be to purchase a gun in New Zealand?",DontKnowAnswer},
            {"Patients with terminal illnesses should be allowed to end their own lives with medical assistance.",5},
            {"International students should be allowed to enter New Zealand immediately.",1},
            {"How likeable do you find Geoff Simmons?", 11 },
            {"Regardless of the party you intend to vote for in this election, in general how likely are you to support The Opportunities Party (TOP)?", 11 }
        };

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var tasks = new Task[NumberOfSubmissions];

            for (int i= 0; i< NumberOfSubmissions; i++){
                tasks[i] = Task.Factory.StartNew(() => DoSurvey());
            }

            Task.WaitAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"{NumberOfSubmissions} submissions complete in {stopwatch.Elapsed.TotalSeconds} seconds");
        }

        private static void DoSurvey()
        {
            using (var questionPage = new QuestionPage())
            {
                int i = 0;
                while(true)
                {
                    questionPage.SkipIfPossible();
                    var question = questionPage.Question;
                    if (question == null)
                    {
                        break;
                    }
                    QuestionAnswers.TryGetValue(question, out var answerNumber);
                    questionPage.Answer(answerNumber);
                    i++;
                    Thread.Sleep(ClickDelayInMilliseconds);
                }
            }
        }
    }
}
