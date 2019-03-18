using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class FeedbackConvertor : JsonConvertor<Feedback>
    {
        public override Feedback fromJson(JObject feedbackJson)
        {
            if (feedbackJson == null)
            {
                Logger.Error("Cannot convert feedback from json, value is null");
                throw new ArgumentNullException();
            }

            Feedback feedback = new Feedback()
            {
                Id = Convert.ToInt32(feedbackJson["Id"]),
                Name = Convert.ToString(feedbackJson["Name"]),
                Patronymic = Convert.ToString(feedbackJson["Patronymic"]),
                GradeValue = Convert.ToInt32(feedbackJson["GradeValue"]),
                Date = Convert.ToDateTime(feedbackJson["Date"]),
                Text = Convert.ToString(feedbackJson["Text"]),
                IdWorker = Convert.ToInt32(feedbackJson["IdWorker"])
            };

            return feedback;
        }

        public override JObject toJson(Feedback feedback)
        {
            if (feedback == null)
            {
                Logger.Error("Cannot convert feedback to json, value is null");
                throw new ArgumentNullException();
            }

            JObject feedbackJson = new JObject();
            feedbackJson["Id"] = feedback.Id;
            feedbackJson["Name"] = feedback.Name;
            feedbackJson["Patronymic"] = feedback.Patronymic;
            feedbackJson["GradeValue"] = feedback.GradeValue;
            feedbackJson["Date"] = feedback.Date;
            feedbackJson["Text"] = feedback.Text;
            feedbackJson["IdWorker"] = feedback.IdWorker;

            return feedbackJson;
        }
    }
}
