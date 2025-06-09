// See https://aka.ms/new-console-template for more information

using System.Linq;
using Microsoft.EntityFrameworkCore;

using var db = new PhoneBookContext();

db.Database.EnsureCreated(); // Important: otherwise database is not created, and table contacts is missing.

Console.WriteLine($"Database path: {db.DbPath}");

// Create
Console.WriteLine("Adding new entry");
db.Add(new Contact { Name = "John Doe" , Email = "johndoe@mail.com", PhoneNumber = "0612345678"});
await db.SaveChangesAsync();

// Read
Console.WriteLine("Querying for contacts");
var contact = await db.Contacts
    .OrderBy(i => i.Id)
    .FirstAsync();

// Update
Console.WriteLine("Update a contact");
contact.PhoneNumber = "06000";
db.Update(contact);
await db.SaveChangesAsync();

// Delete
Console.WriteLine("Deleting the contact");
db.Remove(contact);
await db.SaveChangesAsync();