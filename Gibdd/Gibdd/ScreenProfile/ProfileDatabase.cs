using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Gibdd
{
    public class ProfileDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ProfileDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Profile>().Wait();
            _database.CreateTableAsync<TextAppeal>().Wait();
        }

        public Task<List<Profile>> GetAllProfilesAsync()
        {
            _database.Table<TextAppeal>().OrderBy(item => item.ID);
            return _database.Table<Profile>().ToListAsync();
        }

        public Task<List<TextAppeal>> GetAllTextAppealsAsync()
        {
            _database.Table<TextAppeal>().OrderBy(item => item.ID);
            return _database.Table<TextAppeal>().ToListAsync();
        }

        public Task<Profile> GetProfileAsync(int id)
        {
            return _database.Table<Profile>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveProfileAsync(Profile profile)
        {            
            if (profile.ID != 0)
            {                
                return _database.UpdateAsync(profile);
            }
            else
            {
                return _database.InsertAsync(profile);
            }
        }

        public Task<int> SaveTextAppealAsync(TextAppeal textAppeal)
        {
            if (textAppeal.ID != 0)
            {
                return _database.UpdateAsync(textAppeal);
            }
            else
            {
                return _database.InsertAsync(textAppeal);
            }
        }

        public Task<int> DeleteProfileAsync(Profile profile)
        {
            return _database.DeleteAsync(profile);
        }

        public Task<int> DeleteTextAppealAsync(TextAppeal textAppeal)
        {
            return _database.DeleteAsync(textAppeal);
        }
    }
}
