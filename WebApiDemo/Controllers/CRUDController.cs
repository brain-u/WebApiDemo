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
                BaseAddress = new Uri("https://localhost:44310/api/Employee")
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

        //Create: CRUD
        public ActionResult Create() {

            return View();
        }

        //Create: CRUD
        [HttpPost]
        public ActionResult Create(Employee InsEmployee)
        {
            HttpClient hc = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44310/api/employee")
            };
            System.Threading.Tasks.Task<HttpResponseMessage> InsertRecord = hc.PostAsJsonAsync<Employee>("Employee", InsEmployee);
            InsertRecord.Wait();

            var SaveData = InsertRecord.Result;

            if (SaveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }
    }
}