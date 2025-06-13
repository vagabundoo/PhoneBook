using System.Text.RegularExpressions;

namespace PhoneBook.Validators;

public class ValidatePhoneNumber(string phoneNumber)
{
    private string PhoneNumber { get; } = phoneNumber;

    // Methods for Validation rules
    bool IsNonEmptyString()
    {
        return !string.IsNullOrEmpty(PhoneNumber);
    }
    
    // Only numbers
    bool IsNumber()
    {
        var match = Regex.Match(PhoneNumber , @"[0-9]*");
        return match.Success;
    }
    // 10 digits
    bool Is10Digits()
    {
        var match = Regex.Match(PhoneNumber , @"[0-9]{10}");
        return match.Success;
    }
    
    public (bool,string) IsValid()
    {
        if (!IsNonEmptyString())
            return (false, "No phone number provided");
        if (!IsNumber())
            return (false, "Phone number is not a number");
        if (!Is10Digits())
            return (false, "Phone number length must be 10 characters");
        
        return (true, "Valid phone number");
    }
}