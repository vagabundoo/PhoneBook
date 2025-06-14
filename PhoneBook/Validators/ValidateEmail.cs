using System.Text.RegularExpressions;

namespace PhoneBook.Validators;

public class ValidateEmail(string email)
{
    private string Email { get; } = email;
    private readonly Regex _allowedCharacters = new Regex("^[a-zA-Z0-9_.+-@]*$");
    private readonly string _allowedDomains = "com|nl|net|es|fr";
    
    // Methods for Validation rules
    public bool IsNonEmptyString()
    {
        return !string.IsNullOrEmpty(Email);
    }

    public bool OnlyHasAllowedCharacters()
    {
        var match = _allowedCharacters.IsMatch(Email);
        return match;   
    }

    public bool HasValidFormat()
    {
        var allowedFormat = new Regex(@".*@.+\..+");
        var match = allowedFormat.IsMatch(Email);

        return match;
    }
    
    
    
    public (bool,string) IsValid()
    {
        if (!IsNonEmptyString())
            return (false, "No email provided");
        if (!OnlyHasAllowedCharacters())
            return (false, "Email contains invalid characters");
        if (!HasValidFormat())
            return (false, "Email format is invalid");
        return (true, "Valid email");
    }
}