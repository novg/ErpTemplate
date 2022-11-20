using Application.Interfaces;

namespace Application.Services;

public class FileReaderFactory : IFileReaderFactory
{
    public IFileReader GetFileReader(string fileExtension)
    {
        string extension = fileExtension.ToLower();
        return extension switch
        {
            ".csv" => new CsvReader(),
            _ => throw new ArgumentException($"FileReader not found for file extension '{extension}'")
        };
    }
}