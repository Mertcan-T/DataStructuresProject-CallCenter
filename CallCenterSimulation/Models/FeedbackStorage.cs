using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using CallCenterSimulation.Models;

namespace CallCenterSimulation.Models
{
    public static class FeedbackStorage
    {
        // File path for saving the feedback
        private static readonly string filePath = "customerFeedbackList.json";

        // Load feedbacks from JSON and return a Stack
        public static Stack<CustomerFeedback> LoadFeedbacks()
        {
            if (!File.Exists(filePath))
                return new Stack<CustomerFeedback>();

            var json = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(json))
                return new Stack<CustomerFeedback>();

            try
            {
                var feedbackList = JsonConvert.DeserializeObject<List<CustomerFeedback>>(json) ?? new List<CustomerFeedback>();
                feedbackList.Reverse(); // En son eklenenin en üstte olması için listeyi tersine çeviriyoruz
                return new Stack<CustomerFeedback>(feedbackList);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new Stack<CustomerFeedback>();
            }
        }

        // Save feedback to JSON file
        public static void SaveFeedback(CustomerFeedback feedback)
        {
            var feedbackStack = LoadFeedbacks();
            feedbackStack.Push(feedback); // Yeni geri bildirimi Stack'in üstüne ekliyoruz
            
            // Stack'i listeye çevirip JSON olarak kaydediyoruz
            var feedbackList = new List<CustomerFeedback>(feedbackStack);
            var json = JsonConvert.SerializeObject(feedbackList, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
