namespace PropEase.Repository
{
    public interface IContactMessageRepository
    {
        Task SaveContactMessage(string name, string email, string subject, string message);
    }
}
