using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using WebApiDemo.DBEntity;

namespace WebApiDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        public IHttpActionResult GetEmployee() {
            VertrustDemoEntities em = new VertrustDemoEntities();
            List<Employee> result = em.Employees.ToList();

            return Ok(result);

        }
    }
}