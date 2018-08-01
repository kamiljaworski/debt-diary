using System;

namespace DebtDiary.Core
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's e-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's encrypted password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User's gender
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// User's register date
        /// </summary>
        public DateTime? RegisterDate { get; set; }

    }
}
