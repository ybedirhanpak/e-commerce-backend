using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using e_commerce_api.Helpers;

//Imports
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
    }

    public class UserService : IUserService
    {
        //Create a collection object to refer users
        private readonly IMongoCollection<User> _users;

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IECommerceDatabaseSettings databaseSettings)
        {
            //Create a mongo client instance to connect mongo server
            var client = new MongoClient(databaseSettings.ConnectionString);
            //Create the database which is "e-commerce"
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _appSettings = appSettings.Value;

            //Initialize collection by getting from mongodb
            _users = database.GetCollection<User>(databaseSettings.UsersCollectionName);

        }

        public User Authenticate(string username, string password)
        {
            var user = _users.Find<User>(x => x.Username == username && x.Password == password).FirstOrDefault();

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Find(u => true).ToList().Select(x =>
                {
                    x.Password = null;
                    return x;
                });
        }

        public User GetById(string id)
        {
            var user = _users.Find<User>(u => u.Id == id).FirstOrDefault();

            if (user != null)
            {
                user.Password = null;
            }

            return user;
        }
    }
}
