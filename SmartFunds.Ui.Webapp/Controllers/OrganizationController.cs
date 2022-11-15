using Microsoft.AspNetCore.Mvc;
using SmartFunds.Ui.Webapp.Data;
using SmartFunds.Ui.Webapp.Models;

namespace SmartFunds.Ui.Webapp.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly SmartFundsDbContext _database;

        public OrganizationController(SmartFundsDbContext database)
        {
            _database = database;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var organizations = _database.Organizations;
            return View(organizations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return View(organization);
            }

            var maxId = _database.Organizations.Max(o => o.Id);
            var id = maxId + 1;
            organization.Id = id;
            _database.Organizations.Add(organization);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var organization = _database
                .Organizations
                .SingleOrDefault(o => o.Id == id);

            if (organization is null)
            {
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        [HttpPost]
        public IActionResult Edit(int id, Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return View(organization);
            }

            var dbOrganization = _database
                .Organizations
                .SingleOrDefault(o => o.Id == id);

            if (dbOrganization is null)
            {
                return RedirectToAction("Index");
            }

            dbOrganization.Name = organization.Name;
            dbOrganization.Type = organization.Type;
            dbOrganization.CompanyNumber = organization.CompanyNumber;
            dbOrganization.Email = organization.Email;

            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var organization = _database
                .Organizations
                .SingleOrDefault(o => o.Id == id);

            if (organization is null)
            {
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        [HttpPost("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var organization = _database
                .Organizations
                .SingleOrDefault(o => o.Id == id);

            if (organization is null)
            {
                return RedirectToAction("Index");
            }

            _database.Organizations.Remove(organization);

            return RedirectToAction("Index");
        }
    }
}
