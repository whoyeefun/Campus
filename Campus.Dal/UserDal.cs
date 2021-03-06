﻿using Campus.DalInterface;
using Campus.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Campus.Dal
{
    public class UserDal : IUserDal
    {
        private CampusDbContext _campusDbContext;

        public UserDal(CampusDbContext campusDbContext)
        {
            _campusDbContext = campusDbContext;
        }

        public bool InsertUser(User user)
        {
            try
            {
                _campusDbContext.Users.Add(user);
                int rows = _campusDbContext.SaveChanges();
                return rows > 0;
            }
            catch (Exception ex)
            {
                var sqlEx = ex.InnerException as SqlException;
                if (sqlEx != null)
                {
                    if (sqlEx.Number == 2627)
                    {
                        throw new ValidationException("用户名不能重复", sqlEx);
                    }
                }
                return false;
            }
        }

        public int GetRoleIdFormName(string name)
        {
            return _campusDbContext.Roles.Where(x => x.Name == name).Select(x => x.Id).FirstOrDefault();
        }

        public bool ValidLogin(User loginUser)
        {
            return _campusDbContext.Users.Any(x => x.UserName == loginUser.UserName && x.Password == loginUser.Password);
        }

        public int GetRoleIdFormUName(string userName)
        {
            return _campusDbContext.Users.Where(x => x.UserName == userName).Select(x => x.RoleId).FirstOrDefault();
        }

        public int GetIdByUName(string userName)
        {
            return _campusDbContext.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
        }

        public bool ExistUserName(string userName)
        {
            return _campusDbContext.Users.Any(x=>x.UserName==userName);
        }
    }
}
