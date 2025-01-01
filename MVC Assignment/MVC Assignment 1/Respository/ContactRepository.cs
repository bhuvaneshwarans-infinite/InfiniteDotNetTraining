using ContactTaskFuncCodeFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ContactTaskFuncCodeFirstMVC.Respository
{
    public class ContactRepository : IContactRepository
    {
        ContactContext db;

        public ContactRepository()
        {
            db = new ContactContext();
        }

        public async Task<Contact> GetByIdAsync(long id)
        {
            return await db.Contacts.FindAsync(id);
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await db.Contacts.ToListAsync();
        }


        public async Task CreateAsync(Contact contact)
        {
            db.Contacts.Add(contact);
            await db.SaveChangesAsync();
        }


        public async Task DeleteAsync(long id)
        {
            var contact = await db.Contacts.FindAsync(id);
            if (contact != null)
            {
                db.Contacts.Remove(contact);
                await db.SaveChangesAsync();
            }
        }

    }
}