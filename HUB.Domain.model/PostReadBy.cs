using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HUB.Domain.Model
{
    public class PostReadByUser
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }

    }
}
