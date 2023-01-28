﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIStatistikaApp.DataAccess.DbAccess
{
    public interface ISQLDataAccess
    {
        Task<IEnumerable<T>> LoadAll<T>(string sqlString, string connectionString = "Default");
        Task<IEnumerable<T>> LoadOne<T, U>(string sqlString, U parameter, string connectionString = "Default");
        Task SaveOne<T>(string sqlString, T parameter, string connectionString = "Default");
    }
}
