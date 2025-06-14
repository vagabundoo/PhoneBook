using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneBook.Validators;

namespace PhoneBook.Tests.Validators;

[TestClass]
[TestSubject(typeof(ValidateEmail))]
public class ValidateEmailTest
{
    [DataTestMethod]
    [DataRow(null, false, "No email provided")]
    [DataRow("", false, "No email provided")]
    [DataRow("a@b.c", true, "Valid email")]
    [DataRow("user@domain.com", true, "Valid email")]
    [DataRow("user.name+tag@sub.domain.net", true, "Valid email")]
    [DataRow("user@domain.co.uk", false, "Email format is invalid")]
    [DataRow("user@domain.c", false, "Email contains invalid characters")]
    [DataRow("invalid-email", false, "Email format is invalid")]
    [DataRow("user@.com", false, "Email format is invalid")]
    [DataRow("@domain.com", false, "No email provided")]
    [DataRow("user@domain_with_underscores.com", false, "Email contains invalid characters")]
    public void TestIsValid(string email, bool expectedIsValid, string expectedMessage)
    {
        // Arrange
        var validator = new ValidateEmail(email);

        // Act
        var (isValid, message) = validator.IsValid();

        // Assert
        Assert.AreEqual(expectedIsValid, isValid);
        Assert.AreEqual(expectedMessage, message);
    }

    [DataTestMethod]
    [DataRow(null, false)]
    [DataRow("", false)]
    [DataRow("user@domain.com", true)]
    [DataRow("  ", false)]
    public void TestIsNonEmptyString(string email, bool expected)
    {
        // Arrange
        var validator = new ValidateEmail(email);
        var method = typeof(ValidateEmail).GetMethod("IsNonEmptyString",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = (bool)method!.Invoke(validator, null);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow("user@domain.com", true)]
    [DataRow("user.name+tag@domain.com", true)]
    [DataRow("user@domain_with_underscores.com", false)]
    [DataRow("user@domain.c", false)]
    [DataRow("user@@domain.com", false)]
    [DataRow("", false)]
    [DataRow(null, false)]
    public void TestOnlyHasAllowedCharacters(string email, bool expected)
    {
        // Arrange
        var validator = new ValidateEmail(email);
        var method = typeof(ValidateEmail).GetMethod("OnlyHasAllowedCharacters",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = (bool)method!.Invoke(validator, null);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow("user@domain.com", true)]
    [DataRow("user.name+tag@domain.net", true)]
    [DataRow("user@domain.org", false)]
    [DataRow("user@domain.c", false)]
    [DataRow("user@.com", false)]
    [DataRow("user@@domain.com", false)]
    [DataRow(null, false)]
    public void TestHasValidFormat(string email, bool expected)
    {
        // Arrange
        var validator = new ValidateEmail(email);
        var method = typeof(ValidateEmail).GetMethod("HasValidFormat",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        var result = (bool)method!.Invoke(validator, null);

        // Assert
        Assert.AreEqual(expected, result);
    }
}