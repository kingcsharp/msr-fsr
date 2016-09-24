using System.Linq;
using Msr.Repositories;

namespace Msr.Services.Orders
{
   public class PeopleService
    {
        private readonly MsrDbContext _dbContext;

        public PeopleService()
        {
            _dbContext = new MsrDbContext();
        }

        public bool CheckUserExists(string login, string password)
        {
            var user = _dbContext.ApprovedPeoples.SingleOrDefault(x => x.Login == login && password == password);

            if (user != null && user.SystemStatus != "ACTIVE")
            {
                return false;
            }

            return true;
        }

       
    }
}
