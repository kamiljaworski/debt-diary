using DebtDiary.Core;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DebtDiary.DataProvider
{
    public class DataAccess : IDataAccess, IDisposable
    {
        private DebtDiaryDbContext dbContext = new DebtDiaryDbContext();

        public bool TryCreateUser(User user)
        {
            try
            {
                dbContext.Users.Add(user);
                SaveChanges();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool IsUsernameTaken(string username)
        {
            try
            {
                User user = dbContext.Users.FirstOrDefault(x => x.Username == username);

                if (user == null)
                    return true;

                return false;

            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool IsEmailTaken(string email)
        {
            try
            {
                User user = dbContext.Users.FirstOrDefault(x => x.Email == email);

                if (user == null)
                    return true;

                return false;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public User GetUser(string username, string hashedPassword)
        {
            try
            {
                return dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
            }
            catch (SqlException exception)
            {
                throw new NoInternetConnectionException("There was a problem with database connection", exception);
            }
        }

        public void SaveChanges()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (SqlException exception)
            {
                throw new NoInternetConnectionException("There was a problem with database connection", exception);
            }
        }

        public void Dispose() => dbContext.Dispose();
    }
}
