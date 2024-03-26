namespace TestCommon.TestConstants;

public static partial class Constants
{
    public static class User
    {
        public const string FirstName = "Thaylor";
        public const string LastName = "Santos";
        public const string Email = "thaylormichi@gmail.com";
        public const string Password = "SenhaValida";
        public static readonly Ulid Id = Ulid.NewUlid();
    }
}