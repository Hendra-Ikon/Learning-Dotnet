using InterviewTest.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace InterviewTest.Factory
{
    public class PostFactory : IPostFactory
    {
        private readonly HttpClient _httpClient;

        public PostFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Post>>("api/posts") ?? Enumerable.Empty<Post>();
        }

        public async Task<Post> GetPostAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Post>($"api/posts/{id}");
        }

        public async Task<List<Post>> GetPosts()
        {
            var response = await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
            var allPosts = JsonConvert.DeserializeObject<List<Post>>(response);

            return allPosts.Select(post => new Post { Id = post.Id, Title = post.Title }).ToList();
        }
    }
}
