namespace NaruuroApi.Model.Interface
{
    public interface IUser
    {
        List<UserM> GetAllUsers();
        void Add(UserM product);
        void UpdateUsers(UserM product);
        void DeleteUsers(int id);
        UserM GetUsersbyid(int id);
    }
}
