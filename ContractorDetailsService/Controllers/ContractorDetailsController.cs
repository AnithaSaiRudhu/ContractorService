using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContractorDataAccess;

namespace ContractorDetailsService.Controllers
{
    public class ContractorDetailsController:ApiController
    {
        //public IEnumerable<mst_Contractor> Get()
        //{
        //    using (mydbkrEntities entities = new mydbkrEntities())
        //    {
        //        return entities.mst_Contractor.ToList();
        //    }
        //}


        public HttpResponseMessage Get(string tallyID = "0")
        {
            using (mydbkrEntities entities = new mydbkrEntities())
            {

                bool tallyStatus = tallyID == "0" ? false : true;
                var entity = entities.mst_Contractor.Where(e => e.Tally_Status == tallyStatus).Select(e => e.Contractor_Name).ToList();
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "tallyId not found");
                }
                else
                {                    
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }

            }
        }


        public HttpResponseMessage Put(int id ,[FromBody]mst_Contractor contract)
        {
            try
            {
                using (mydbkrEntities entities = new mydbkrEntities())
                {
                    var entity = entities.mst_Contractor.FirstOrDefault(e => e.Contractor_Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contractor with id =" + id.ToString() + "Not Found.");
                    }
                    else
                    {
                        entity.Tally_Status = contract.Tally_Status;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}