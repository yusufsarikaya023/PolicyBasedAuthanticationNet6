using Model;

namespace Repository;

public class Users
{
    public static IEnumerable<User> GetUsers() => new[]{
        new User { Id = Guid.NewGuid(), Name = "Yusuf", Email = "ysf@gmail.com", Password = "123456" , Roles = new[]{"Admin", "Product"}, },
        new User { Id = Guid.NewGuid(), Name = "Hatice", Email = "htc@gmail.com", Password = "123456" ,Roles = new[]{"Account", "Product"}, },
    };
}