using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    /// <summary>
    /// Servers as an entry service for work with users
    /// </summary>
    [ServiceContract]
    public interface IUserService
    {
        /// <summary>
        /// Simple creation of an anonymous user
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        User Create();

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
        [OperationContract]
        User Create(string name, string surname, string login, string password, string email, string phone, string salt);

        /// <summary>
        /// Return user by Guid
        /// </summary>
        /// <param name="id">ID of user</param>
        /// <returns>User</returns>
        [OperationContract]
        User GetUser(Guid id);

        /// <summary>
        /// Method for finding user by login and password
        /// </summary>
        /// <param name="login">login of user</param>
        /// <param name="password">password of user</param>
        /// <returns>founded user </returns>
        [OperationContract]
        User GetUser(string login, string password);

        /// <summary>
        /// Return all registered users
        /// </summary>
        /// <returns>all registered users</returns>
        [OperationContract]
        Users GetAllUsers();

        /// <summary>
        /// Deletion of user
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Result of deletion</returns>
        [OperationContract]
        Result Delete(Guid id);

        /// <summary>
        /// Update users attributes
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="salt"></param>
        /// <returns>Result of update</returns>
        [OperationContract]
        Result UpdateUser(string name, string surname, string email, string phone, string salt);

        /// <summary>
        /// Binding address with user
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="address">Addres for user</param>
        /// <returns>Result</returns>
        [OperationContract]
        Result AddAddressToUser(Guid id, Address address);

        /// <summary>
        /// Update password of user
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="password">new password</param>
        /// <returns>Result of update</returns>
        [OperationContract]
        Result UpdateUserPassword(Guid id, string password);
    }
}
