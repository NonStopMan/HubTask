using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HUB.Domain.Model
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string EmailAddress { get; set; }
    }
}
