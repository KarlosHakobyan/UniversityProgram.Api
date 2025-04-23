using System.Net.Http.Json;
using UniversityProgram.Blazor.Models;

namespace UniversityProgram.Blazor.Apis
{
    public class StudentApi : IStudentApi
    {
        private readonly HttpClient _client;

        public StudentApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<StudentModel> GetById(int Id)
        { 
            var response = await _client.GetAsync($"/Student/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudentModel>();
            }
            else
            { 
               throw new Exception("Error" + response.ReasonPhrase);
            }
        }

        public async Task<IEnumerable<StudentModel>> GetAll()
        {
            var response = await _client.GetAsync("/Student");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<StudentModel>>();
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("Error" + response.ReasonPhrase);
                return Array.Empty<StudentModel>();
            }
            else
            {
                throw new Exception("Error" + response.ReasonPhrase);
            }
        }

        public async Task UpdateStudent(int Id,StudentUpdateModel model)
        {
            var response = await _client.PutAsJsonAsync($"/Student/{Id}", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error" + response.ReasonPhrase);
            }
        }

        public async Task Delete(int id)
        {
            var response = await _client.DeleteAsync($"/Student/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error" + response.ReasonPhrase);
            }
        }
    }
}
