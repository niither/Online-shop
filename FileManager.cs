using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Online_shop
{
    /// <summary>
    /// Class manages file operations like saving and loading categories to or from XML file
    /// </summary>
    internal class FileManager : IFileManager<object>
    {
        /// <summary>
        /// Saves the list of categories to an XML file
        /// </summary>
        /// <param name="categories">Categories to be saved</param>
        /// <param name="filePath">Path to the XML file</param>
        public void SaveCategories(List<Category> categories, string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<Category>));

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    serializer.Serialize(sw, categories);
                }

                Console.WriteLine($"Categories saved to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads the list of categories from an XML file
        /// </summary>
        /// <param name="filePath">Path to the XML file</param>
        /// <returns>List of loaded categories</returns>
        public List<Category> LoadCategories(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return new List<Category>();

                var serializer = new XmlSerializer(typeof(List<Category>));

                using (StreamReader sr = new StreamReader(filePath))
                {
                    var categories = (List<Category>)serializer.Deserialize(sr);
                    Console.WriteLine($"Categories loaded from {filePath}");
                    return categories;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading: {ex.Message}");
                return new List<Category>();
            }
        }
    }
}