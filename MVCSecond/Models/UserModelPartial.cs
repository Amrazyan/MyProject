using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCSecond.Models
{
    [Serializable]
    public class UserModelPartial
    {
        /// <summary>
        /// using System.ComponentModel.DataAnnotations;
        /// User.cs i mej chenq kara popoxutyunner anenq,,dra hmar es metod@ petqa ogtagorcenq
        /// 
        /// </summary>

        public int Id { get; set; }
        [Required(ErrorMessage = "Type a Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Type a Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Type a Email")]
        [RegularExpression(@"(?i)\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Unacceptable writing")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Write a password")]
        public string Password { get; set; }
    }
    [MetadataType(typeof(UserModelPartial))]
    public partial class Users
    {
    }
}