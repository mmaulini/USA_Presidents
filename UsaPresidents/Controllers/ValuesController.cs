using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UsaPresidents.Models;
using CsvHelper;
using System.Web;

namespace UsaPresidents.Controllers
{
    public class ValuesController : ApiController
    {

        static readonly string address = HttpContext.Current.Server.MapPath("~/App_Data/President.csv");

        // GET api/values
        public List<PresidentSel> Get()
        {

            string a = HttpContext.Current.Server.MapPath("~/App_Data/President.csv");
            TextReader reader = File.OpenText(address);

            CsvReader csvFile = new CsvReader(reader);
            csvFile.Configuration.HasHeaderRecord = false;

            csvFile.Read();
            var records = csvFile.GetRecords<President>().ToList();
            List<PresidentSel> rec2 = new List<PresidentSel>();


            foreach (var r in records)
            {
                rec2.Add(new PresidentSel());
                PresidentSel r2 = rec2.Last();
                r2.PresidentName = r.PresidentName;
                r2.BirdthDate = r.BirdthDate;
                r2.DeathDate = r.DeathDate;

            }

            //string theResult = "Hello from Hello Api at: \n " + result;
            return rec2;
        }


        // GET api/values/q
        public string Get(string q)
        {
            return "value:" + q;
        }


        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
