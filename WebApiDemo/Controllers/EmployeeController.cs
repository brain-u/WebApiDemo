using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using WebApiDemo.DBEntity;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using System.Data.Entity;

namespace WebApiDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        private VertrustDemoEntities em = new VertrustDemoEntities();

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

        public IHttpActionResult GetEmployeeId(int id)
        {
            Employee EmpDetail = null;

            EmpDetail = em.Employees.Where(x => x.EmployeeID == id).Select(x => new Employee()
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name,
                Position = x.Position,
                Age = x.Age,
                Salary = x.Salary,
            }).FirstOrDefault();

            if (EmpDetail == null)
            {
                return NotFound();
            }

            return Ok(EmpDetail);

        }

        public IHttpActionResult Put(Employee ec)
        {
            Employee UpDataEmp = em.Employees.Where(x => x.EmployeeID == ec.EmployeeID).FirstOrDefault<Employee>();

            if (UpDataEmp != null)
            {
                UpDataEmp.EmployeeID = ec.EmployeeID;
                UpDataEmp.Name = ec.Name;
                UpDataEmp.Position = ec.Position;
                UpDataEmp.Age = ec.Age;
                UpDataEmp.Salary = ec.Salary;
                em.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            Employee empdel = em.Employees.Where( x => x.EmployeeID == id ).FirstOrDefault();
            em.Entry(empdel).State = System.Data.Entity.EntityState.Deleted;
            em.SaveChanges();
            return Ok();

        }
    }
}