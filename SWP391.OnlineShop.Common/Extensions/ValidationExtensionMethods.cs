﻿using SWP391.OnlineShop.Common.Constraints;
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

    public static bool IsPhone(this string phone)
    {
        var checkPhone = new PhoneAttribute();
        var isValidPhone = checkPhone.IsValid(phone);
        return isValidPhone;
    }

    public static string ReplaceSpecialCharacters(this string input)
    {
        var output = string.Empty;
        foreach (var charData in input)
        {
            var data = charData.ToString();
            if (CommonConstraints.InvalidDictionary.Contains(data))
            {
                output += " ";
            }
            else
            {
                output += charData;
            }
        }

        return output;
    }
}