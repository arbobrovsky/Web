using BusinessLogic.Interfaces;
using Data;
using Data.Entityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class EFContactRepository : IContactRepository
    {

        private readonly EFDBContext _context;

        public EFContactRepository(EFDBContext context)
        {
            _context = context;
        }

        public void DeleteContact(Contact achieve)
        {
            if (achieve != null)
            {
                _context.Contacts.Remove(achieve);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactById(int contactId)
        {
            return await _context.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);
        }

        public void SaveContact(Contact achieve)
        {
            if (achieve.Id == 0)
                _context.Contacts.Add(achieve);
            else
                _context.Entry(achieve).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
