namespace DebtDiary
{
    /// <summary>
    /// Enum that describes message that will be shown in the application forms
    /// if there is any problem with any field in the form
    /// </summary>
    public enum FormMessage
    {
        None,
        EmptyFirstName,
        EmptyLastName,
        EmptyUsername,
        EmptyEmail,
        EmptyPassword,
        EmptyRepeatedPassword,
        TakenUsername,
        TakenEmail,
        DifferentPasswords,
        PasswordTooShort,
        EmptyMessage,
        IncorrectEmail,
        IncorrectFirstName,
        IncorrectLastName,
        IncorrectUsername,
        UnselectedGender,
        IncorrectPassword,
        DebtorExist,
        EmptyValue,
        EmptyDescription,
        IncorrectValue,
        IncorrectDescription,
        NegativeNumber,
        EmptyCurrentPassword,
        EmptyNewPassword,
        EmptyRepeatedNewPassword,
        IncorrectCurrentPassword,
        IncorrectDate
    }
}
