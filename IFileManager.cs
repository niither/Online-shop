namespace Online_shop
{
    /// <summary>
    /// Interface defines file management operations. SOLID principles and extensibility
    /// </summary>
    /// <typeparam name="T">Generic paramener</typeparam>
    internal interface IFileManager<T>
    {
        void SaveCategories(List<Category> categories, string filePath);
        List<Category> LoadCategories(string filePath);
    }
}
