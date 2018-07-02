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
using Swashbuckle.Swagger.Annotations;

namespace UsaPresidents.Controllers
{
    public class ValuesController : ApiController
    {

        /// <summary>
        /// This endpoint return a list with all US presidents in no particular order
        /// </summary>
        /// <returns>JSON with a list of all US presidents</returns>
        // GET api/values
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<President>))]
        [Route("api/values/")]
        public HttpResponseMessage Get()
        {

            var dataReader = new ReadCSVData(WebConfigurationManager.AppSettings["CSVPath"]);
            var president = dataReader.ReadAll();
            var rec2 = MapperHelper.CSVMapping(president);
            return Request.CreateResponse(HttpStatusCode.OK, rec2);
        
        }

        /// <summary>
        /// This endpoint returns a list with US presidents filtered by an existent provided mask on the president name
        /// </summary>
        /// <param name="q">mask for filter</param>
        /// <returns>JSON with a list of US presidents that contains mask in their name</returns>
        // GET api/values/q

        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<President>))]
        [Route("api/values/{q}")]
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
                var president = dataReader.ReadAll();
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

        /// <summary>
        /// This endpoint returns a list with all US presidents ordered by birth or death dates in ascending or descending order, If the order is by death date all the alive presidents will be shown at the bottom of the list
        /// </summary>
        /// <param name="byBDate">If true orders by birth date, if false orders by death date</param>
        /// <param name="isAsc">If true shows results in an ascending order, if false shows results in a descendent order</param>
        /// <returns>List with all US presidents ordered by birth or death dates in ascending or descending order</returns>

        // GET api/values/{byBDate}/{isAsc}

        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<President>))]
        [Route("api/values/{byBDate}/{isAsc}")]
        public HttpResponseMessage Get(bool byBDate, bool isAsc = true)
        {
            try
            {
                var dataReader = new ReadCSVData(WebConfigurationManager.AppSettings["CSVPath"]);
                var president = dataReader.ReadAll();
                var rec2 = MapperHelper.CSVMapping(president);

                if (byBDate)
                {
                    if (isAsc)
                    {
                        var result = rec2.OrderBy(x => x.BirthDate).ToList();
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        var result = rec2.OrderByDescending(x => x.BirthDate).ToList();
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


    }
}
