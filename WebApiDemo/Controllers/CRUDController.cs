using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiDemo.DBEntity;
using System.Net.Http;

namespace WebApiDemo.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult Index()
        {
            IEnumerable<Employee> employeeData = null;
            HttpClient hc = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44310/api/employee")
            };

            var consumeapi = hc.GetAsync("employee");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode) {
                System.Threading.Tasks.Task<IList<Employee>> displaydata = readdata.Content.ReadAsAsync<IList<Employee>>();
                displaydata.Wait();
                employeeData = displaydata.Result;
            }
            return View(employeeData);
        }
    }
}