using System;

namespace IoTControlR
{
    public class UserInfo
    {
        static readonly public UserInfo Default = new UserInfo
        {
            AccountName = "User Test",
            FirstName = "User",
            LastName = "Test"
        };

        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";

        public bool IsEmpty => String.IsNullOrEmpty(DisplayName.Trim());
    }
}
