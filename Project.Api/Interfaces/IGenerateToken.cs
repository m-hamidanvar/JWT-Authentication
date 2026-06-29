using Project.Model;

namespace Project.Api.Interfaces
{
    public interface IGenerateToken
    {
        string GetToken(User user);
    }
}
