using System.ComponentModel.DataAnnotations;

namespace SWP391.OnlineShop.Common.Extensions;

public static class ValidationExtensionMethods
{
    public static bool IsEmail(this string email)
    {
        var checkEmail = new EmailAddressAttribute();
        var isValidEmail = checkEmail.IsValid(email);
        return isValidEmail;
    }
}