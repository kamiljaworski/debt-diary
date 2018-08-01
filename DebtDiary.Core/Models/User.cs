using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [StringLength(80)]
        [Index(IsUnique=true)]
        public string Username { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        [StringLength(80)]
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        [StringLength(80)]
        public string LastName { get; set; }

        /// <summary>
        /// User's e-mail
        /// </summary>
        [StringLength(80)]
        [Index(IsUnique=true)]
        public string Email { get; set; }

        /// <summary>
        /// User's encrypted password
        /// </summary>
        [StringLength(256)]
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
