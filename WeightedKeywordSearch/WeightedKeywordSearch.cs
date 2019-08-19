using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WeightedKeywordSearch
{
    public enum Services {
        Wiki, Calendar
    }

    public static class WeightedKeywordSearch {
        public static IDictionary<Services, int> Search(string question) {
            
            // build index 
            var index = new List<(Services service, string keyword, int weight)> {
                (Services.Calendar, "when", 5),
                (Services.Calendar, "appointment", 10),
                (Services.Calendar, "meeting", 10),
                (Services.Wiki, "vpn", 10),
                (Services.Wiki, "toilet", 5),
                (Services.Wiki, "intranet", 10),
            };

            // build scores from question and index
            var scores = new Dictionary<Services, int>();
            foreach (var (service, keyword, weight) in index) {
                if (!question.ToLower().Contains(keyword)) continue;
                // add weight to the services score
                scores.TryGetValue(service, out var score);
                scores[service] = score + weight;
            }

            return scores;
        }

        public static string Readable(this IDictionary<Services, int> scores) {
            var sb = new StringBuilder();
            foreach (var (service, score) in scores.OrderByDescending(k=>k.Value)) {
                sb.AppendLine($"{service}: {score}");
            }
            return sb.ToString();
        }
    }



    public class Tests
    {
        [Test]
        public void ToiletVpn() {
            var question = "Can i use vpn on the toilet to check my mails?";
            var scores = WeightedKeywordSearch.Search(question);
            TestContext.WriteLine(scores.Readable());

            // Result:
            //  Wiki: 15
        }

        [Test]
        public void ToiletMeeting() {
            var question = "When is my meeting about the new toilets?";
            var scores = WeightedKeywordSearch.Search(question);
            TestContext.WriteLine(scores.Readable());

            // Result:
            //  Calendar: 15
            //  Wiki: 5
        }

    }



}