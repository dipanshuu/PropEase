using PropEase.Context;

namespace PropEase.Repository
{
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly AppDBContext appDbContext;

        public ContactMessageRepository(AppDBContext a) {
            this.appDbContext = a;
        }
        public async Task SaveContactMessage(string name, string email, string subject, string message)
        {
            await this.appDbContext.SaveContactDetailAsync(name,email,subject,message);
        }
    }
}
