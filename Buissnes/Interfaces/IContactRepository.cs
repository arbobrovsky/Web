using Data.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(int contactId);
        void SaveContact(Contact achieve);
        void DeleteContact(Contact achieve);
    }

}
