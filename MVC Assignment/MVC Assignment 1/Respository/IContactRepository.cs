using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ContactTaskFuncCodeFirstMVC.Models;

namespace ContactTaskFuncCodeFirstMVC.Respository
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task CreateAsync(Contact contact);
        Task DeleteAsync(long id);
        Task<Contact> GetByIdAsync(long id);

    }
}