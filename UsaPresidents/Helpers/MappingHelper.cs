using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsaPresidents.Models;

namespace UsaPresidents.Helpers
{
    public static class MapperHelper
    {
        public static List<President> CSVMapping(List<DAL.Models.President> president)
        {
            List<President> rec2 = new List<President>();
            foreach (var r in president)
            {
                rec2.Add(new President());
                President r2 = rec2.Last();
                r2.PresidentName = r.PresidentName;
                DateTime bd = new DateTime();
                DateTime dd = new DateTime();
                if (DateTime.TryParse(r.BirthDate, out bd)) { r2.BirthDate = bd; }
                if (DateTime.TryParse(r.DeathDate, out dd)) { r2.DeathDate = dd; }

            }
            return rec2;
        }
    }
}