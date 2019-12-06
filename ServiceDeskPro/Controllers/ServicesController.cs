using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using ServiceDeskPro.Models;

namespace ServiceDeskPro.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            IEnumerable<Service> services = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://localhost:44339/api/");

                var responseTask = client.GetAsync("Services/GetServices");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Service>>();
                    readTask.Wait();

                    services = readTask.Result;
                }
                else
                {
                    services = Enumerable.Empty<Service>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(services);

        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Service service)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://localhost:44339/api/Services/");

                var postTask = client.PostAsJsonAsync<Service>("CrearServicio", service);
                postTask.Wait();

                var result = postTask.Result;

                if(!result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {//La Kasita 
            Service service = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://localhost:44339/api/");
                var responTask = client.GetAsync("Services/GetServices?id" + id.ToString());
                responTask.Wait();

                var result = responTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Service>();
                    readTask.Wait();

                    service = readTask.Result;
                }
            }
           
            return View(service);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service servicePut)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://localhost:44339/api/Services/EditarServicio");

                var putTask = client.PutAsJsonAsync<Service>("EditarServicio", servicePut);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Service>>();
                    readTask.Wait();

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://localhost:44339/api/");

                var deleteTask = client.DeleteAsync("Services / GetServices" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private List<Service> ListaService()
        {
            return db.Services.ToList();
        }
    }
}
