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

        // GET: CRUD
        public ActionResult Detail(int id)
        {
            Employee EmpObj = null;
            HttpClient hc = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44310/api/employee")
            };

            var consumeapi = hc.GetAsync("employee?id=" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                System.Threading.Tasks.Task<Employee> DisplayData = readdata.Content.ReadAsAsync<Employee>();

                DisplayData.Wait();
                EmpObj = DisplayData.Result;
            }
            return View(EmpObj);
        }

        public ActionResult Edit(int id)
        {
            Employee EmpObj = null;
            HttpClient hc = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44310/api/employee")
            };

            var consumeapi = hc.GetAsync("employee?id=" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                System.Threading.Tasks.Task<Employee> DisplayData = readdata.Content.ReadAsAsync<Employee>();

                DisplayData.Wait();
                EmpObj = DisplayData.Result;
            }
            return View(EmpObj);
        }

        [HttpPost]
        public ActionResult Edit(Employee em)
        {
            HttpClient hc = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44310/api/employee")
            };
            System.Threading.Tasks.Task<HttpResponseMessage> InsertRecord = hc.PutAsJsonAsync<Employee>("Employee", em);
            InsertRecord.Wait();

            var SaveData = InsertRecord.Result;

            if (SaveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message("無可更新員工資料");
            }
            return View(em);

        }

        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44310/api/Employee");

            var delrecord = hc.DeleteAsync("Employee/" + id.ToString());
            delrecord.Wait();

            var displaydatee = delrecord.Result;
            if (displaydatee.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}