namespace PhoneBook;

public class InputValidator
{
    
    string ValidatePhoneNumber(string phoneNumber)
    {
        if (phoneNumber.Length == 0) 
            return "No phone number provided";
        if (phoneNumber.Length != 9) 
            return "Phone number length must be 9 characters";
        if (!int.TryParse(phoneNumber, out _))
            return "Phone number is not a number";
        return "Valid phone number";
    }
    
    bool ValidateEmail()
    {
        return false;
    }
}




