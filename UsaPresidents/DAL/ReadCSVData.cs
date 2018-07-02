using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UsaPresidents.DAL.Models;
using UsaPresidents.DAL.Interfaces;

namespace UsaPresidents.DAL
{
    /// <summary>
    /// Class to interact with a presidents data that is in a CSV file 
    /// </summary>
    public class ReadCSVData : IReadable
    {
        /// <summary>
        /// Http address of the csv path 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="path"> receives de csv file path</param>
        public ReadCSVData(string path)
        {
            Address = HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// Read all the presidents on the csv file
        /// </summary>
        /// <returns></returns>
        public List<President> ReadAll()
        {
            TextReader reader = File.OpenText(Address);

            CsvReader csvFile = new CsvReader(reader);
            csvFile.Configuration.HasHeaderRecord = false;

            csvFile.Read();
            return csvFile.GetRecords<President>().ToList();

        }
        /// <summary>
        /// Get a president by Id (Not implemented in this case, this is just to follow the interface that reduce dependency)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public President GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove a president by Id (Not implemented in this case, this is just to follow the interface that reduce dependency)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<President> RemoveById(int id)
        {
            throw new NotImplementedException();
        }
    }
}