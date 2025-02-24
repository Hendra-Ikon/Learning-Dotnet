using InterviewTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewTest.Factory
{
    public interface IPostFactory
    {
        Task<List<Post>> GetPosts();
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post> GetPostAsync(int id);
    }
}
