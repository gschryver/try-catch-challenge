using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Contact bob = new Contact() {
                FirstName = "Bob", LastName = "Smith",
                Email = "bob.smith@email.com",
                Address = "100 Some Ln, Testville, TN 11111"
            };
            Contact sue = new Contact() {
                FirstName = "Sue", LastName = "Jones",
                Email = "sue.jones@email.com",
                Address = "322 Hard Way, Testville, TN 11111"
            };
            Contact juan = new Contact() {
                FirstName = "Juan", LastName = "Lopez",
                Email = "juan.lopez@email.com",
                Address = "888 Easy St, Testville, TN 11111"
            };

            AddressBook addressBook = new AddressBook();
            addressBook.AddContact(bob);
            addressBook.AddContact(sue);
            addressBook.AddContact(juan);

            try
            {
                addressBook.AddContact(sue);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            List<string> emails = new List<string>() {
                "sue.jones@email.com",
                "juan.lopez@email.com",
                "bob.smith@email.com",
            };

            emails.Insert(1, "not.in.addressbook@email.com");

            foreach (string email in emails)
            {
                try 
                {
                    Contact contact = addressBook.GetByEmail(email);
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Name: {contact.FullName}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Address: {contact.Address}");
                }
                catch (KeyNotFoundException ex) 
                {
                    Console.WriteLine($"No contact found for email: {email}"); 
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
        }
    }
}

public class Contact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
}

public class AddressBook
{
    private Dictionary<string, Contact> _contactList {get; set;} = new Dictionary<string, Contact>();

    public void AddContact(Contact contact)
    {
        if (_contactList.ContainsKey(contact.Email))
        {
            throw new ArgumentException($"A contact with email {contact.Email} already exists.");
        }

        _contactList.Add(contact.Email, contact);
    }

    public Contact GetByEmail(string email)
    {
        if (!_contactList.ContainsKey(email))
        {
            throw new KeyNotFoundException($"No contact found for email: {email}");
        }

        return _contactList[email];
    }
}
