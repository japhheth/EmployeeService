﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeAccess;

namespace EmployeeService
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (EmployeeRecordsEntities entities = new EmployeeRecordsEntities())
            {
                return entities.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}