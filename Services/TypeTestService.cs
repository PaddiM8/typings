using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Typings.Data;

namespace Typings.Services
{
    public class TypeTestService
    {
        private readonly ApplicationDbContext _db;

        private readonly ILogger<TypeTestService> _logger;

        public TypeTestService(ApplicationDbContext db, ILogger<TypeTestService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task Add(string username, int wpm, int accuracy)
        {
            var user = await _db.Users.FindAsync(username);
            _logger.LogInformation($"Creating test result: {username}, {wpm.ToString()}, {accuracy.ToString()}");
            user.TestResults.Add(new TestResult
            {
                Wpm = wpm,
                Accuracy = accuracy
            });
            await _db.SaveChangesAsync();
        }

        public IEnumerable<TestResult> GetAll(string username)
        {
            var tests = new List<TestResult>();
            var rnd = new Random();
            var date = DateTime.Now;
            int day = 0;
            for (double i = 0; i < 100; i += 0.25)
            {
                tests.Add(new TestResult
                {
                    Wpm = rnd.Next(60, 110),
                    Accuracy = rnd.Next(5, 100),
                    Date = date.AddDays(day),
                });
                Console.WriteLine(date.AddDays(day));

                day++;
            }

            _db.Users.Find(username).TestResults.RemoveAll(x => true);
            _db.Users.Find(username).TestResults.AddRange(tests);
            _db.SaveChanges();

            var user = _db.Users
                .Where(x => x.Username == username)
                .Include(u => u.TestResults)
                .SingleOrDefault();

            return user?.TestResults ?? new List<TestResult>();
        }

        public IEnumerable<TestResult> GetMany(string username, int amount)
        {
            var user = _db.Users
                .Where(x => x.Username == username)
                .Include(u => u.TestResults)
                .SingleOrDefault();

            return user?.TestResults.Take(amount) ?? new List<TestResult>();
        }

        public IEnumerable<TestResult> GetBest(string username, int amount)
        {
            var user = _db.Users
                .Where(x => x.Username == username)
                .Include(u => u.TestResults)
                .SingleOrDefault();

            return user?.TestResults.OrderBy(x => x.Wpm).Take(amount) ?? new List<TestResult>();
        }
    }
}