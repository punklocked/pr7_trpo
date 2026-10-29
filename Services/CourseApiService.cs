using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using pr7_trpo_nab.Models;

namespace pr7_trpo_nab.Services
{
    public class CourseApiService
    {
        private readonly HttpClient httpClient = new HttpClient();

        public CourseApiService()
        {
            httpClient.BaseAddress = new Uri("https://localhost:7141/");
            httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<List<Course>> GetCoursesAsync(string? teacher, int? minDuration)
        {
            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(teacher))
                parts.Add("teacher=" + Uri.EscapeDataString(teacher.Trim()));
            if (minDuration.HasValue)
                parts.Add("minDuration=" + minDuration.Value);

            var url = "api/v1/course" + (parts.Count > 0 ? "?" + string.Join("&", parts) : "");
            var response = await httpClient.GetFromJsonAsync<List<Course>>(url);
            if (response != null)
                return response;

            return new List<Course>();
        }

        public async Task<Course> GetCourseAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/v1/course/{id}");

            if (response.IsSuccessStatusCode)
            {
                var course = await response.Content.ReadFromJsonAsync<Course>();
                return course!;
            }

            throw new Exception($"Ошибка {response.StatusCode}");
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/v1/course", course);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Course>();

            throw new Exception($"Ошибка {response.StatusCode}");
        }

        public async Task UpdateCourseAsync(Course course)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync($"api/v1/course/{course.Id}", course);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Ошибка {response.StatusCode}");
        }

        public async Task PatchCourseAsync(int id, IDictionary<string, object?> fields)
        {
            HttpResponseMessage response = await httpClient.PatchAsJsonAsync($"api/v1/course/{id}", fields);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Ошибка {response.StatusCode}");
        }

        public async Task DeleteCourseAsync(int id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"api/v1/course/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Ошибка {response.StatusCode}");
        }
    }
}
