using System;
using System.Collections.Generic;
using UsaPresidents.Dal;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.hht;
using UsaPresidents.Models;
using CsvHelper;
using System.Web;

namespace DAL
{
    public class ReadCsv
    {
        public static List<President> ReadCsvPresident(string Path)
        {
            string a = HttpContext.Current.Server.MapPath(Path);
            TextReader reader = File.OpenText(address);

            CsvReader csvFile = new CsvReader(reader);
            csvFile.Configuration.HasHeaderRecord = false;

            csvFile.Read();
            var records = csvFile.GetRecords<President>().ToList();
        }

    }
}
