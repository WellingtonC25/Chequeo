using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceDeskPro.Models;
using ServiceDeskPro.Models.DTO;

namespace ServiceDeskPro.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RequestsController()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        }
        public ActionResult Index()
        {
            IEnumerable<Request> requests = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44339/api/");

                var responseTask = client.GetAsync("Requests/GetSolicitudes");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Request>>();
                    readTask.Wait();

                    requests = readTask.Result;
                }
                else
                {
                    requests = Enumerable.Empty<Request>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(requests);
        }
        public ActionResult SolucionesAsignada()
        {
            var app = ListaSolucionesAsignada();
            return View(app.ToList());
        }
        public ActionResult SolicitudesSinAsignar()
        {
            var servicios = ListaDeSolicitudesSinAsignar();
            return View(servicios.ToList());
        }

        [HttpGet]
        public ActionResult AsignarUsuario(int? Id)
        {
            var solicitud = ListaDeSolicitudesSinAsignar();
            var solicitudSeleccionada = solicitud.FirstOrDefault(c => c.Id == Id);
            var usuarios = _userManager.Users.ToList();
            ViewBag.Usuarios = new SelectList(usuarios, "Id", "FirstName");
            var solicitudDto = new SolicitudesSinAsignarDTO { Usuarios = usuarios, Solicitudes = solicitudSeleccionada };
            return View(solicitudDto);
        }

        [HttpPost]
        public ActionResult AsignarUsuario(SolicitudesSinAsignarDTO model)
        {
            var solicitud = db.Requests.SingleOrDefault(c => c.Id == model.Solicitudes.Id);
            solicitud.UsuarioId = model.UsuarioId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult TransferirSolicitud(int? id)
        {
            var requestT = ListaSolucionesAsignada();
            var requestTSelected = requestT.FirstOrDefault(a => a.Id == id);
            var userLoged = _userManager.Users.ToList();
            ViewBag.Usuarios = new SelectList(userLoged, "Id","FirstName");

            var requestsDTO = new TransferirSolicitudDTO { Usuarios = userLoged, Solicitudes = requestTSelected};

            return View(requestsDTO);
        }
        [HttpPost]
        public ActionResult TransferirSolicitud(TransferirSolicitudDTO model)
        {
            var solicitud = db.Requests.SingleOrDefault(c => c.Id == model.Solicitudes.Id);
            solicitud.UsuarioId = model.UsuarioId;
            solicitud.AdditionalComment = model.Solicitudes.AdditionalComment;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "ServiceType");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UsuarioId,CustomerId,ServiceId,Date,Description,AdditionalComment,Asigned,Resolved,CommentResolve")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", request.CustomerId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "ServiceType", request.ServiceId);
            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", request.CustomerId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "ServiceType", request.ServiceId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UsuarioId,CustomerId,ServiceId,Date,Description,AdditionalComment,Asigned,Resolved,CommentResolve")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", request.CustomerId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "ServiceType", request.ServiceId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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

        //Listado de servicios sin usuario asignado.
        private List<Request> ListaDeSolicitudesSinAsignar()
        {
            return db.Requests.Where(c => c.UsuarioId == null).ToList();
        }

        private List<Request> ListaSolucionesAsignada()
        {
            return db.Requests.Where(c => c.UsuarioId != null).ToList();
        }

        private List<Request> ListaSoluciones()
        {
            return db.Requests.ToList();
        }
    }
}
