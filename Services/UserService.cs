using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

//Imports
using e_commerce_api.Helpers;
using e_commerce_api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

//MongoDB
using MongoDB.Driver;

namespace e_commerce_api.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(string id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(string id);
    }

    public class UserService : IUserService
    {
        //Create a collection object to refer users
        private readonly IMongoCollection<User> _users;

        public UserService(IECommerceDatabaseSettings databaseSettings)
        {
            //Create a mongo client instance to connect mongo server
            var client = new MongoClient(databaseSettings.ConnectionString);
            //Create the database which is "e-commerce"
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            //Initialize collection by getting from mongodb
            _users = database.GetCollection<User>(databaseSettings.UsersCollectionName);

        }

        public User Authenticate(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _users.Find<User>(x => x.Username == username ).FirstOrDefault();

            // return null if user not found
            if (user == null)
                return null;

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) {
                return null;
            }

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Find(u => true).ToList();
        }

        public User GetById(string id)
        {
            return _users.Find<User>(u => u.Id == id).FirstOrDefault();
        }

        public User Create(User user, string password)
        {
            //validation
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("User not found");
            }

            if (_users.Find<User>(u => u.Username == user.Username).Any<User>())
            {
                throw new AppException("Username " + user.Username + " already exists.");
                
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _users.InsertOne(user);
            return user;

        }

        public void Update(User userIn, string password=null)
        {
            var user = _users.Find<User>(u => u.Id == userIn.Id).FirstOrDefault();

            if (user == null) throw new AppException("User not found");

            if(userIn.Username != user.Username)
            {
                if(_users.Find<User>(u => u.Username == userIn.Username).Any())
                {
                    throw new AppException("Username " + userIn.Username + " is already exists.");
                }
            }

            //update user properties
            if (userIn.FirstName != null)
            {
                user.FirstName = userIn.FirstName;
            }

            if (userIn.LastName != null)
            {
                user.LastName = userIn.LastName;
            }

            if (userIn.Username != null)
            {
                user.Username = userIn.Username;
            }

            if (userIn.Role != null)
            {
                user.Role = userIn.Role;
            }

            //update password if it was provided
            if(!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

            }

            _users.ReplaceOne<User>(u => u.Id == user.Id, user);

        }

        public void Delete(string id)
        {
            var user = _users.Find<User>(u => u.Id == id).FirstOrDefault();

            if(user!=null)
            {
                _users.DeleteOne(u => u.Id == id);
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt )
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
                
            }
            if (string.IsNullOrWhiteSpace(password))
            {

                throw new ArgumentException("Value cannot be empty or whitespace only string.",nameof(password));
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty....",nameof(password));
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash(64 bytes expected)", nameof(storedHash));
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt(128 bytes expected)", nameof(storedSalt));

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i< computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
