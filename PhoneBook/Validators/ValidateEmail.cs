using System.Text.RegularExpressions;

namespace PhoneBook.Validators;

public class ValidateEmail(string email)
{
    private string Email { get; } = email;
    private readonly string _allowedCharacters = "[a-zA-Z0-9_.+-]";
    private readonly string _allowedDomains = "com|nl|net|es|fr";
    
    // Methods for Validation rules
    bool IsNonEmptyString()
    {
        return !string.IsNullOrEmpty(Email);
    }

    bool OnlyHasAllowedCharacters()
    {
        var match = Regex.Match(Email, $"{_allowedCharacters}*");
        return match.Success;   
    }

    bool HasValidFormat()
    {
        var pattern = Regex.Escape(_allowedCharacters) 
                      + "@" 
                      + Regex.Escape(_allowedCharacters) 
                      + "." 
                      + Regex.Escape(_allowedDomains)
                      + Regex.Escape("$");
        
        return Regex.IsMatch(Email, pattern);
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