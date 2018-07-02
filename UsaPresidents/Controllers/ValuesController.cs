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
using UsaPresidents.DAL;
using System.Web.Configuration;
using UsaPresidents.Helpers;

namespace UsaPresidents.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/values
        public HttpResponseMessage Get()
        {

            var dataReader = new ReadCSVData(WebConfigurationManager.AppSettings["CSVPath"]);
            var president = dataReader.ReadCSV();
            var rec2 = MapperHelper.CSVMapping(president);
            return Request.CreateResponse(HttpStatusCode.OK, rec2);
        
        }


        // GET api/values/q
        public HttpResponseMessage Get(string q)
        {
            if (q == null)
            {
                HttpError err = new HttpError("q should not be empty");
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }

            try
            {
                var dataReader = new ReadCSVData(WebConfigurationManager.AppSettings["CSVPath"]);
                var president = dataReader.ReadCSV();
                var rec2 = MapperHelper.CSVMapping(president);
                var result = rec2.Where(x => x.PresidentName.ToLower().Contains(q.ToLower())).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }
        }


        // GET api/values/5
        public HttpResponseMessage Get(bool byDate, bool isAsc = true)
        {
            try
            {
                var dataReader = new ReadCSVData(WebConfigurationManager.AppSettings["CSVPath"]);
                var president = dataReader.ReadCSV();
                var rec2 = MapperHelper.CSVMapping(president);

                if (byDate)
                {
                    if (isAsc)
                    {
                        var result = rec2.OrderBy(x => x.BirdthDate).ToList();
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        var result = rec2.OrderByDescending(x => x.BirdthDate).ToList();
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                else
                {
                    var deathPresidents = rec2.Where(x => x.DeathDate != null).ToList();
                    var alivePresidents = rec2.Where(x => x.DeathDate == null).ToList();
                    if (isAsc)
                    {
                        var orderedList = deathPresidents.OrderBy(x => x.DeathDate).ToList();
                        orderedList.AddRange(alivePresidents);
                      
                        return Request.CreateResponse(HttpStatusCode.OK, orderedList);

                    }
                    else
                    {
                        var orderedList = deathPresidents.OrderByDescending(x => x.DeathDate).ToList();
                        orderedList.AddRange(alivePresidents);
                        return Request.CreateResponse(HttpStatusCode.OK, orderedList);

                    }

                }
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }



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
