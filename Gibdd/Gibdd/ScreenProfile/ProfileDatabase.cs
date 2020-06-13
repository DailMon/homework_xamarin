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
            return _database.Table<Profile>().OrderByDescending(x => x.ID).ToListAsync();
        }

        public Task<List<TextAppeal>> GetAllTextAppealsAsync()
        {
           var allText = _database.Table<TextAppeal>().OrderByDescending(x => x.ID).ToListAsync();           
           if (allText.Result.Count > 10)
            {
                DeleteTextAppealAsync(allText.Result[0]);
            }
            return allText;
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
