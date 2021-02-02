using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using WebApiDemo.DBEntity;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace WebApiDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly VertrustDemoEntities em = new VertrustDemoEntities();

        public IHttpActionResult GetEmployee() {
            
            List<Employee> result = em.Employees.ToList();

            return Ok(result);

        }

        [HttpPost]
        public IHttpActionResult InsEmployee(Employee EmpData)
        {
            em.Employees.Add(EmpData);
            em.SaveChanges();

            return Ok();

        }
    }
}