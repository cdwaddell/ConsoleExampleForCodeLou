using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TempProject
{
    public class ContactManager
    {
        public void LoadContacts(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                try
                {
                    _contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
                }
                catch
                {
                }
            }
        }

        private List<Contact> _contacts = new List<Contact>();

        public void SaveContacts(string path)
        {
            //save the contact list to a file
            var contactJson = JsonConvert.SerializeObject(_contacts);
            File.WriteAllText(path, contactJson);
        }

        public int GetNextContactId()
        {
            var lastContactId = 0;
            if (_contacts.Any())
                lastContactId = _contacts.Max(c => c.Id);

            return lastContactId;
        }

        public void AddNewContact(Contact contactToAdd)
        {
            _contacts.Add(contactToAdd);
        }

        public Contact LoadContactById(int contactId)
        {
            return _contacts.Where(c => c.Id == contactId).SingleOrDefault();
        }

        public List<Contact> FindByName(string nameToSearch)
        {
            return _contacts.Where(c => c.FirstName.Contains(nameToSearch) || c.LastName.Contains(nameToSearch)).ToList();
        }

        public List<Contact> GetAllContacts()
        {
            return _contacts.ToList();
        }
    }
}