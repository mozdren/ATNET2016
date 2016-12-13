using System;
using System.Linq;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Simple creation of an anonymous user
        /// </summary>
        /// <returns>User data contract</returns>
        public User CreateUnregistered()
        {
            using (var context = new EntityModels.ServiceBusDatabaseEntities())
            {
                var id = Guid.NewGuid(); 
                try
                {
                    context.Users.Add(new EntityModels.User()
                    {
                        Id = id,
                        Email = "@",
                        Hash = "Annonymouse".GetHashCode().ToString(),
                        Salt = "",
                        Name = "Annonymouse",
                        Surname = "Annonymouse"
                    });
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new User
                    {
                        Result = Result.FatalFormat("UserService.Create Exception: {0}", ex.Message)
                    };
                }
                return new User
                {
                    Id = id,
                    Result = Result.SuccessFormat("User {0} has been added.", id)
                }; 
            }
        }

        /// <summary>
        /// Creaation of registered user
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <param name="surname">Surname of user</param>
        /// <param name="login">Login of user, shuld be mandatory for registered users</param>
        /// <param name="password">User password, shuld be mandatory for registered users</param>
        /// <param name="email">User email, shuld be mandatory for registered users</param>
        /// <param name="phone">User phone number, shuld be mandatory for registered users</param>
        /// TODO What is the purpose of this attribute ? 
        /// <param name="salt">...</param>
        /// <returns>Created user</returns>
        public User Create(string name, string surname, string login, string password, string email, string phone, string salt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return user by Guid
        /// </summary>
        /// <param name="id">ID of user</param>
        /// <returns>User</returns>
        public User GetUserById(Guid id)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var user = context.Users.FirstOrDefault(b => b.Id == id);

                    if (user == null)
                    {
                        return new User
                        {
                            Result = Result.ErrorFormat("User {0} not found.", id)
                        };
                    }
                    return CeateUserFromUser(user, Result.SuccessFormat("User {0} has been founded.", id));
                    
                }
            }
            catch (Exception ex)
            {
                return new User
                {
                    Result = Result.FatalFormat("UserService.Create Exception: {0}", ex.Message)
                };
            }
        }

        /// <summary>
        /// Convert User from database to user from datacontract
        /// </summary>
        /// <param name="u">instance of user from database</param>
        /// <param name="result">Result message</param>
        /// <returns></returns>
        private static User CeateUserFromUser(EntityModels.User u, Result result)
        {
            if (u == null)
            {
                return new User
                {
                    Result = Result.ErrorFormat("User not found.")
                };
            }
            //TODO add address here
            return new User
            {
                Name = u.Name,
                Surname = u.Surname,
                Login = u.Login,
                Password = u.Password,
                Id = u.Id,
                PhoneNumber = u.Phone,
                Salt = u.Salt,
                EmailAddress = u.Email,
                Result = result
            };
        }

        /// <summary>
        /// Method for finding user by login and password
        /// </summary>
        /// <param name="login">login of user</param>
        /// <param name="password">password of user</param>
        /// <returns>founded user </returns>
        public User GetUser(string login, string password)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var user = context.Users.FirstOrDefault(b => b.Login == login && b.Password == password);

                    if (user == null)
                    {
                        return new User
                        {
                            Result = Result.ErrorFormat("User with login {0} and password {1} not found.", login, password)
                        };
                    }
                    return CeateUserFromUser(user, Result.SuccessFormat("User {0} has been founded.", login));

                }
            }
            catch (Exception ex)
            {
                return new User
                {
                    Result = Result.FatalFormat("UserService.Create Exception: {0}", ex.Message)
                };
            }
        }

        /// <summary>
        /// Return all registered users
        /// </summary>
        /// <returns>all registered users</returns>
        public Users GetAllUsers()
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var returnCollection = context.Users.Select(user => CeateUserFromUser(user, Result.SuccessFormat("User founded."))).ToList();

                    return new Users
                    {
                        Result = Result.Success("All users returned"),
                        Items = returnCollection
                    };
                }
            }
            catch (Exception exception)
            {
                return new Users
                {
                    Result = Result.FatalFormat("UserService.GetAllUsers Exception: {0}", exception.Message)
                };
            }
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
