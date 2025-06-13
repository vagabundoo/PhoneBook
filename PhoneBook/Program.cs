using System.Linq;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

var db = new PhoneBookContext();
await db.Database.EnsureDeletedAsync();
db.Database.EnsureCreated(); // Important: otherwise the database is not created, and table contacts is missing.

// Prefill the database 
Console.WriteLine("Adding sample contacts data");
db.Add(new Contact("John Doe" , "johndoe@mail.com", "0612345678"));
db.Add(new Contact("John Mark" , "johndoe2@mail.com", "0612345"));
db.Add(new Contact("Dude Dude" , "dd@mail.com", "000000"));
await db.SaveChangesAsync();


// Terminal interaction
bool closeApp = false;

var choiceMenu = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("What would you like to do?")
        .AddChoices(new[]
        {
            "Show contacts", 
            "Add Contact", 
            "Edit Contact", 
            "Delete Contact"
        }));


switch (choiceMenu)
{
    case "Show contacts":
        var contacts = db.Contacts
            .OrderBy(i => i.Id);
        AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a contact to edit:")
                .AddChoices(new[]
                {
                    
                })
        
        foreach (var c in contacts)
        {
            Console.WriteLine($"Id: {c.Id}, Name: {c.Name}, Email: {c.Email}, Phone: {c.PhoneNumber}");
        }

        break;
    case "Add Contact":
        Console.WriteLine("Name: ");
        var name = Console.ReadLine();
        Console.WriteLine("Email: ");
        var email = Console.ReadLine();
        Console.WriteLine("Phonenumber: ");
        var phonenumber = Console.ReadLine();

        // shorthand for if null, assign this value.
        name ??= "InvalidName";
        email ??= "InvalidEmail";

        db.Contacts.Add(new Contact(name, email, phonenumber));
        await db.SaveChangesAsync();
        break;
    // Add a wat to search for contacts and edit or delete them.
    case "Edit Contact":
        break;
    case "Delete Contact":
        break;
}

//
// Test CRUD operations
//

Console.WriteLine($"Database path: {db.DbPath}");

// Create
Console.WriteLine("Adding new entry");
db.Add(new Contact("John Doe" , "johndoe@mail.com", "0612345678"));
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