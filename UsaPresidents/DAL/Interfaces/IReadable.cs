using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsaPresidents.DAL.Models;

namespace UsaPresidents.DAL.Interfaces
{
    /// <summary>
    /// Interface to reduce the dependency to CSV, is created to ensure that in the future we can read form a database for example... 
    /// </summary>
    interface IReadable
    {
        List<President> ReadAll();
        President GetById(int id);
        List<President> RemoveById(int id);

        //and so on...
    }
}