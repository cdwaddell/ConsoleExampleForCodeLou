using System;
using System.Linq;

namespace TempProject
{
    public class ContactManagerUserInterface
    {
        const string path = "people.json";
        private ContactManager _contactManager = new ContactManager();

        public ContactManagerUserInterface()
        {
            _contactManager.LoadContacts(path);
        }


        internal void StartMenuUserInterface()
        {
            //loop until user exits
            var shouldExit = false;
            while (!shouldExit)
            {
                Console.WriteLine("What would you like to do?");
                //menu options
                //exit
                Console.WriteLine("0. Exit Contact Manager");
                //list contacts
                Console.WriteLine("1. List all contacts");
                //search contacts
                Console.WriteLine("2. Search contacts by name");
                //view contact
                Console.WriteLine("3. Load contact by ID");
                //add contact
                Console.WriteLine("4. Add contact");

                var action = Console.ReadLine();

                switch (action)
                {
                    case "0":
                        {
                            shouldExit = true;
                            break;
                        }
                    case "1":
                        {
                            ListAllContacts();
                            break;
                        }
                    case "2":
                        {
                            StartSearchContactUserInterface();
                            break;
                        }
                    case "3":
                        {
                            StartContactLoadUserInterface();
                            break;
                        }
                    case "4":
                        {
                            StartAddContactUserInterface();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine($"Invalid Entry {action}");
                            break;
                        }
                }
            }
            //end loop

            _contactManager.SaveContacts(path);
        }

        private void StartAddContactUserInterface()
        {
            string firstName = null;
            string lastName = null;

            while(!FirstNameIsValid(firstName))
            {
                Console.WriteLine("Enter a valid first name:");
                firstName = Console.ReadLine();
            }

            while(!LastNameIsValid(lastName))
            {
                Console.WriteLine("Enter a valid last name:");
                lastName = Console.ReadLine();
            }

            var lastContactId = _contactManager.GetNextContactId();

            var contactToAdd = new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                Id = lastContactId + 1
            };

            _contactManager.AddNewContact(contactToAdd);
        }

        private bool LastNameIsValid(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return false;

            if (lastName.Contains(" "))
                return false;

            return true;
        }

        private bool FirstNameIsValid(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return false;

            if (firstName.Contains(" "))
                return false;

            return true;
        }

        private void StartContactLoadUserInterface()
        {
            Console.WriteLine("What is the ID of the user to load?");

            if (!int.TryParse(Console.ReadLine(), out int contactId))
            {
                Console.WriteLine("Invalid Id Provided");
                return;
            }

            var contactToLoad = _contactManager.LoadContactById(contactId);

            if(contactToLoad != null)
            {
                contactToLoad.ToString();

                Console.WriteLine("What would you like to do?");
                Console.WriteLine("0. Exit contact menu");
                Console.WriteLine("1. Edit this contact");
                Console.WriteLine("2. Delete this contact");

                throw new NotImplementedException();
            }
            else
            {
                Console.WriteLine($"Invalid contact ID {contactId}");
            }
        }

        private void StartSearchContactUserInterface()
        {
            Console.WriteLine("What name do you want to search for?");
            var nameToSearch = Console.ReadLine();

            var foundContacts = _contactManager.FindByName(nameToSearch);

            Console.WriteLine($"{foundContacts.Count()} contact(s) found");
            foreach(var contact in foundContacts)
            {
                Console.WriteLine(contact.ToString());
            }
        }

        private void ListAllContacts()
        {
            foreach(var contact in _contactManager.GetAllContacts())
            {
                Console.WriteLine(contact.ToString());
            }
        }
    }
}