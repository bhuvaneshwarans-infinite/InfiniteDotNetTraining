using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using WebApiAssessment.Models;

namespace WebApiAssessment.Controllers
{
    public class CountryController : ApiController
    {
        public static List<CountryMdl> countries = new List<CountryMdl>();
        
        [Route("api/Country")]
        public IEnumerable<CountryMdl> GetCountries()
        {
            return countries;
        }

        [HttpPost]
        [Route("api/Country/Create")]
        public List<CountryMdl> CreateCountry(CountryMdl country)
        {
            countries.Add(country);
            return countries;
        }
 

        [HttpPut]
        [Route("api/Country/Update")]
        public IEnumerable<CountryMdl> UpdateCountry(int Cid,string country,string captial)
        {
            CountryMdl UpdateCOuntryObj = new CountryMdl();
            UpdateCOuntryObj.ID = Cid;
            UpdateCOuntryObj.CountryName = country;
            UpdateCOuntryObj.Capital = captial;
            countries[Cid - 1] = UpdateCOuntryObj;
            return countries;
        }

        [HttpDelete]
        [Route("api/Country/Delete")]
        public IEnumerable<CountryMdl> Delete(int Cid)
        {
            countries.RemoveAt(Cid - 1);
            return countries;
        }

    }
}