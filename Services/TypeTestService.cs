using System.Threading.Tasks;
using Typings.Data;

namespace Typings.Services
{
    public class TypeTestService
    {
        private readonly ApplicationDbContext _db;
        
        public TypeTestService(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task Add(string username, int wpm, int accuracy)
        {
            var user = await _db.Users.FindAsync(username);
            user.TestResults.Add(new TestResult
            {
                AuthorUsername = username,
                Wpm = wpm,
                Accuracy = accuracy
            });
        }
    }
}