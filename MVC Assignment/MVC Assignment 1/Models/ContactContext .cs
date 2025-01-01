using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
namespace ContactTaskFuncCodeFirstMVC.Models
{
    public class ContactContext: DbContext
    {
        public ContactContext() : base("name = ContactConnection") { }

        public DbSet<Contact> Contacts { get; set; }
    }
}