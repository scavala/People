using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using People.Dao;
using People.Models;


namespace People.Controllers
{
    public class PeopleController : Controller
    {
        private static readonly ICosmosDbService service = CosmosDbServiceProvider.CosmosDbService;
        public async Task<ActionResult> Index()
        {
            return View(await service.GetItemsAsync("SELECT * FROM Person"));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await service.AddItemAsync(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public async Task<ActionResult> Edit(string id) => await ShowItem(id);

        private async Task<ActionResult> ShowItem(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var item = await service.GetItemAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateItemAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public async Task<ActionResult> Delete(string id) => await ShowItem(id);

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed([Bind(Include = "Id,FirstName,LastName,Email")] Person person)
        {
            await service.DeleteItemAsync(person);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id) => await ShowItem(id);

    }
}