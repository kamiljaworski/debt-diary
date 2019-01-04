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
                TrySaveChanges();
                return true;
            }
            catch (Exception)
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
                    return false;

                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool IsEmailTaken(string email)
        {
            try
            {
                User user = dbContext.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool UserExist(string username, string hashedPassword)
        {
            try
            {
                User user = dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
                if (user == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UserAddedThisDebtor(int usersId, string debtorsFirstName, string debtorsLastName)
        {
            try
            {
                User user = dbContext.Users.FirstOrDefault(u => u.Id == usersId);
                if (user == null)
                    return false;

                Debtor debtor = user.Debtors.FirstOrDefault(d => d.FirstName == debtorsFirstName && d.LastName == debtorsLastName);
                if (debtor == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryGetUser(string username, string hashedPassword, out User user)
        {
            try
            {
                user = dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
                if(user == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                user = null;
                return false;
            }
        }

        public bool TrySaveChanges()
        {
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose() => dbContext.Dispose();
    }
}
