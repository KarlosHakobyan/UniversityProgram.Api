using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UniversityProgram.LocalData;
public class JsonDataService : IJsonDataService
{
    const string filePath = "data1.json";
    public void WriteData<T>(T data)
    {
        using (StreamWriter streamWriter = new StreamWriter(filePath))
        {
            string json = JsonSerializer.Serialize(data);
            streamWriter.Write(json);
        };

    }

    public T ReadData<T>()
    {
        using (StreamReader streamReader = new StreamReader(filePath))
        {
            string json = streamReader.ReadToEnd();
            return JsonSerializer.Deserialize<T>(json);
        };

    }
}
