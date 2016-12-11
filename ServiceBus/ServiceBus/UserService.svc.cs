using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    public class UserService : IUserService
    {
        public User Create()
        {
            throw new NotImplementedException();
        }

        public User Create(string name, string surname, string login, string password, string email, string phone, string salt)
        {
            throw new NotImplementedException();
        }

        public User GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string login, string password)
        {
            throw new NotImplementedException();
        }

        public Users GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Result Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result UpdateUser(string name, string surname, string email, string phone, string salt)
        {
            throw new NotImplementedException();
        }

        public Result AddAddressToUser(Guid id, Address address)
        {
            throw new NotImplementedException();
        }

        public Result UpdateUserPassword(Guid id, string password)
        {
            throw new NotImplementedException();
        }
    }
}
