using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using CallCenterSimulation.Models;

namespace CallCenterSimulation.Models
{
    public static class FeedbackStorage
    {
        // Updated to use your custom file name
        private static readonly string filePath = "customerFeedbackList.json";

        public static List<CustomerFeedback> LoadFeedbacks()
        {
            if (!File.Exists(filePath))
                return new List<CustomerFeedback>(); // Return empty list if file doesn't exist

            var json = File.ReadAllText(filePath); // Read the JSON file
            if (string.IsNullOrEmpty(json))
                return new List<CustomerFeedback>(); // Return empty list if the file is empty

            try
            {
                // Make sure the JSON is an array of CustomerFeedback objects
                return JsonConvert.DeserializeObject<List<CustomerFeedback>>(json) ?? new List<CustomerFeedback>();
            }
            catch (JsonException ex)
            {
                // Log or handle the error here
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new List<CustomerFeedback>(); // Return empty list in case of an error
            }
        }


        // Save a new feedback to the JSON file
        public static void SaveFeedback(CustomerFeedback feedback)
        {
            // Load the existing feedbacks, add the new one, and then serialize back to the file
            var feedbacks = LoadFeedbacks();
            feedbacks.Add(feedback);
            var json = JsonConvert.SerializeObject(feedbacks, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}

