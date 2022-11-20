namespace Application.Interfaces;

public interface IFileReaderFactory
{
    IFileReader GetFileReader(string fileExtension);
}