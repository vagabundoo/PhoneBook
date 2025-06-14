using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneBook.Validators;

namespace PhoneBook.UnitTests.Validators;

[TestClass]
[TestSubject(typeof(ValidateEmail))]
public class ValidateEmailTest
{

    [TestMethod]
    public void ChecksForAllowedCharacters()
    {
        // Arrange
        var email1 = new ValidateEmail("john@live.com");
        var email2 = new ValidateEmail("john&%@live.com");
        
        // Act
        var email1Result = email1.OnlyHasAllowedCharacters();
        var email2Result = email2.OnlyHasAllowedCharacters();
        
        // Assert
        Assert.IsTrue(email1Result);;
        Assert.IsFalse(email2Result);;
    }
}