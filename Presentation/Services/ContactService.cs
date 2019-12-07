using BusinessLogic;
using Data.Entityes;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class ContactService
    {
        private readonly DataManager _dataManager;
        public ContactService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }


        public async Task<ContactViewModel> ContactDBToViewModelById(int ContactId = 0)
        {
            var contactModelDb = await _dataManager.Contacts.GetContactById(ContactId);
            return ModelViewContact(contactModelDb);
        }


        public async Task<ContactViewModel> SaveContactEditModelToDb(ContactViewModel model)
        {
            Contact contactDbModel = null;
            if (model.Id != 0)
            {
                contactDbModel = await _dataManager.Contacts.GetContactById(model.Id);
            }
            else
            {
                contactDbModel = new Contact();
            }

            contactDbModel.Address = model.Address;
            contactDbModel.Phone = model.Phone;
            contactDbModel.Email = model.Email;
            contactDbModel.TimeWorkStart = model.TimeWorkStart;
            contactDbModel.TimeWorkDown = model.TimeWorkDown;
            contactDbModel.GeoLat = model.GeoLat;
            contactDbModel.GeoLong = model.GeoLong;
            _dataManager.Contacts.SaveContact(contactDbModel);

            return await ContactDBToViewModelById(contactDbModel.Id);
        }

        public void DeleteContact(ContactViewModel model)
        {
            Contact contactDbModel = new Contact()
            {
                Id = model.Id,
                Address = model.Address,
                Phone = model.Phone,
                Email = model.Email,
                TimeWorkStart = model.TimeWorkStart,
                TimeWorkDown = model.TimeWorkDown,
                WorkWeek = model.WorkWeek,
                GeoLat = model.GeoLat,
                GeoLong = model.GeoLong
            };
            _dataManager.Contacts.DeleteContact(contactDbModel);
        }

        public ContactViewModel CreateNewContactViewModel()
        {
            return new ContactViewModel();
        }

        public async Task<List<ContactViewModel>> GetCategoriesViewModelList()
        {
            var contacts = await _dataManager.Contacts.GetAllContacts();
            List<ContactViewModel> ContactList = new List<ContactViewModel>();
            foreach (var item in contacts)
            {
                ContactList.Add(new ContactViewModel
                {
                    Id = item.Id,
                    Address = item.Address,
                    Phone = item.Phone,
                    Email = item.Email,
                    TimeWorkStart = item.TimeWorkStart,
                    TimeWorkDown = item.TimeWorkDown,
                    WorkWeek = item.WorkWeek,
                    GeoLat = item.GeoLat,
                    GeoLong = item.GeoLong
                });
            }

            return ContactList;
        }


        private ContactViewModel ModelViewContact(Contact contactDbModel)
        {
            return new ContactViewModel
            {
                Id = contactDbModel.Id,
                Address = contactDbModel.Address,
                Phone = contactDbModel.Phone,
                Email = contactDbModel.Email,
                TimeWorkStart = contactDbModel.TimeWorkStart,
                TimeWorkDown = contactDbModel.TimeWorkDown,
                WorkWeek = contactDbModel.WorkWeek,
                GeoLat = contactDbModel.GeoLat,
                GeoLong = contactDbModel.GeoLong
            };
        }
    }
}