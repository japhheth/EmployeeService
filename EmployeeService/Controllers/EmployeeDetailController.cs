using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeAccess;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;

namespace EmployeeService.Controllers
{
    //Formatting JsonFormat

    //public class CustomJsonFormatter : JsonMediaTypeFormatter
    //{
    //    public CustomJsonFormatter()
    //    {
    //        this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
    //    }

    //    public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
    //    {
    //        base.SetDefaultContentHeaders(type, headers, mediaType);
    //        headers.ContentType = new MediaTypeHeaderValue("application/json");
    //    }
    //}



    public class EmployeeDetailController : ApiController
    {
       [HttpGet,  BasicAuthentication]
        public HttpResponseMessage LoadEmployees(string gender="All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (EmployeeRecordsEntities entities = new EmployeeRecordsEntities()) {

                //switch(gender.ToLower()){
                //    case "all" : return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
                //    case "male": return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender == "male").ToList());
                //    case "female": return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender == "female").ToList());
                //    default:  return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Employee Gender must be male or female");
                //}
                //   return entities.Employees.ToList();


                 if (username.ToLower() == "male")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender == "male").ToList());
                }
                else if (username.ToLower() == "female") {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender == "female").ToList());
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }




            }

        }
        [HttpGet]
        public HttpResponseMessage LoadAllEmployees(int id)
        {
            using (EmployeeRecordsEntities entities = new EmployeeRecordsEntities()){
              var result =   entities.Employees.FirstOrDefault(e => e.ID == id);

                if (result != null)
                {
                       return Request.CreateResponse(HttpStatusCode.OK, result);
                  //  return Ok(result);
                }
                else
                {
                  //  return BadRequest("Employee with ID " + id.ToString() + " is not found");
                   return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID " + id.ToString() + " is not found");
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage post(Employee employee)
        {
            try
            {
                using (EmployeeRecordsEntities entities = new EmployeeRecordsEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "Added Successfully");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
          }
            
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeRecordsEntities entities = new EmployeeRecordsEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee  with ID =" + id.ToString() + " is not found");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
            }
                catch (Exception ex){
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

        [HttpPut]
        public HttpResponseMessage Put(int id, Employee employee)
        {
            try
            {
                using (EmployeeRecordsEntities entities = new EmployeeRecordsEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity != null)
                    {
                       
                        entity.firstName = employee.firstName;
                        entity.lastName = employee.lastName;
                        entity.Gender = employee.Gender;
                        entity.salary = employee.salary;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Success");
                    }
                   
                 
                    else{
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID =" + id.ToString() + " is not found");
                    }

                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }

      
}
