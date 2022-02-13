using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeacherConnect.Data.Interfaces;
using TeacherConnect.Dto;
using TeacherConnect.Entities;

namespace TeacherConnect.Data
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration) : base(configuration, "TeacherConnectDB")
        {
            _configuration = configuration;

        }

        public async Task<int> AddUser(AppUser user)
        {
            string command = @"INSERT INTO dbo.user(firstName, lastName, username, email, passwordhash, passwordsalt, userrole, createdatdate, lastloggedindate)
              VALUES(@firstName, @lastName, @username, @email, @passwordhash, @passwordsalt, @userrole, @createdatdate, @lastloggedindate) returning Id;";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            parameters.Add("@username", user.UserName);
            parameters.Add("@firstname", user.FirstName);
            parameters.Add("@lastname", user.LastName);
            parameters.Add("@email", user.Email);
            parameters.Add("@passwordhash", user.PasswordHash);
            parameters.Add("@passwordsalt", user.PasswordSalt);
            parameters.Add("@userrole", user.UserRole.ToString());
            parameters.Add("@createdatdate", user.CreatedAtDate);
            parameters.Add("@lastloggedindate", user.CreatedAtDate);

            var result = await ExecuteScalarAsync<int>(sql: command, param: parameters);
            return result;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            string sql = @"SELECT id FROM dbo.user WHERE id = @id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var result = await QueryFirstOrDefaultAsync<AppUser>(sql: sql, param: parameters);
            return result;
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            string sql = @"SELECT id, username, passwordhash, passwordsalt, firstname, lastname, email, userrole FROM dbo.user WHERE username = @username";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@username", username);
            var result = await QueryFirstOrDefaultAsync<AppUser>(sql: sql, param: parameters);
            return result;

        }

        public async Task<int> LogUser(AppUser user)
        {
            string sql = @"UPDATE dbo.user SET lastloggedindate = @lastloggedindate WHERE id =@id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", user.Id);
            parameters.Add("@lastloggedindate", user.LastLoggedInDate);
            var result = await ExecuteAsync(sql: sql, param: parameters);
            return result;
        }
    }
}