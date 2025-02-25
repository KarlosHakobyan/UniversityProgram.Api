namespace UniversityProgram.LocalData;

public interface IJsonDataService
{
    T ReadData<T>();
    void WriteData<T>(T data);
}