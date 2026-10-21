using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using pr7_trpo_nab.Models;

namespace pr7_trpo_nab.Services
{
    public class CourseApiService
    {
        private readonly HttpClient httpClient = new HttpClient();

        public CourseApiService()
        {
            httpClient.BaseAddress = new Uri("https://localhost:7038/");
            httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        // TODO

        //public async Task<List<Course>> GetCoursesAsync(string? teacher, int? minDuration)
        //{
            
        //}

        public async Task<Course> GetCourseAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/v1/course/{id}");

            if (response.IsSuccessStatusCode)
            {
                var course = await response.Content.ReadFromJsonAsync<Course>();
                return course!;
            }
            else
                throw new ApiException($"Ошибка: {response.StatusCode}");
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/v1/course", course);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Course>();
            else
                throw new ApiException($"Ошибка: {response.StatusCode}");
        }

        public async Task UpdateCourseAsync(Course course)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync($"api/v1/course/{course.Id}", course);

            if (!response.IsSuccessStatusCode)
                throw new ApiException($"Ошибка: {response.StatusCode}");
        }

        // TODO

        //public async Task PatchCourseAsync(Course course)
        //{
        //    HttpResponseMessage response = await httpClient.PutAsJsonAsync($"api/v1/course/{course.Id}", course);

        //    if (!response.IsSuccessStatusCode)
        //        throw new ApiException($"Ошибка: {response.StatusCode}");
        //}

        public async Task DeleteCourseAsync(int id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"api/v1/course/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApiException($"Ошибка: {response.StatusCode}");
        }
    }
}
