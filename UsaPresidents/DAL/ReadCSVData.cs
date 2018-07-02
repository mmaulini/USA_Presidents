using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UsaPresidents.DAL.Models;

namespace UsaPresidents.DAL
{
    public class ReadCSVData
    {
     
        public string Address { get; set; }


        public ReadCSVData(string path)
        {
            Address = HttpContext.Current.Server.MapPath(path);
        }

        
        public  List<President> ReadCSV()
        {
            TextReader reader = File.OpenText(Address);

            CsvReader csvFile = new CsvReader(reader);
            csvFile.Configuration.HasHeaderRecord = false;

            csvFile.Read();
            return csvFile.GetRecords<President>().ToList();

        }
    }
}