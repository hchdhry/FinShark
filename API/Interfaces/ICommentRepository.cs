using API.Models;

namespace API.interfaces{

public interface ICommentRepository
{

    public Task<List<Comment>> GetAll();

    public Task<Comment> GetAsync(int id);
    

}
}
