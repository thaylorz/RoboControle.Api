using System.Net.Mail;

using ErrorOr;

using RoboControle.Domain.Common;

namespace RoboControle.Domain.Entities;
public partial class UserEntity : Entity
{
    private UserEntity() { }

    public string Email { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string PasswordSalt { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public static ErrorOr<UserEntity> Create(
        string firstName,
        string lastName,
        string email,
        string passwordSalt,
        string passwordHash)
    {
        if (IsInvalidValidFirstName(firstName))
        {
            return UserErrors.InvalidFirstName;
        }

        if (IsInvalidValidLastName(lastName))
        {
            return UserErrors.InvalidLastName;
        }

        if (IsInvalidValidEmail(email))
        {
            return UserErrors.InvalidEmail;
        }

        var user = new UserEntity
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordSalt = passwordSalt,
            PasswordHash = passwordHash,
        };

        return user;
    }

    public ErrorOr<Success> Update(
        string firstName,
        string lastName,
        string email,
        string passwordSalt,
        string passwordHash)
    {
        if (IsInvalidValidFirstName(firstName))
        {
            return UserErrors.InvalidFirstName;
        }

        if (IsInvalidValidLastName(lastName))
        {
            return UserErrors.InvalidLastName;
        }

        if (IsInvalidValidEmail(email))
        {
            return UserErrors.InvalidEmail;
        }

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;

        return Result.Success;
    }

    private static bool IsInvalidValidFirstName(string firstName)
    {
        return string.IsNullOrWhiteSpace(firstName);
    }

    private static bool IsInvalidValidLastName(string lastName)
    {
        return string.IsNullOrWhiteSpace(lastName);
    }

    private static bool IsInvalidValidEmail(string email)
    {
        return string.IsNullOrWhiteSpace(email) || !MailAddress.TryCreate(email, out var mailAddress); ;
    }
}