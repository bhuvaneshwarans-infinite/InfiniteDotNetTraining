using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactTaskFuncCodeFirstMVC.Models;
using ContactTaskFuncCodeFirstMVC.Respository;
using System.Threading.Tasks;


namespace ContactTaskFuncCodeFirstMVC.Controllers
{
    public class ContactController : Controller
    {
        IContactRepository contactRepo;
        public ContactController()
        {
            contactRepo = new ContactRepository();
        }
        public async Task<ActionResult> Index()
        {
            var contact = await contactRepo.GetAllAsync();
            return View(contact);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await contactRepo.CreateAsync(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public async Task<ActionResult> Delete(long id)
        {
            var contact = await contactRepo.GetByIdAsync(id);
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteContact(long id)
        {
            await contactRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}