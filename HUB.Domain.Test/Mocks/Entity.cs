using System;
using System.ComponentModel.DataAnnotations;
using HUB.Domain.Model;

namespace HUB.Domain.Tests.Mocks
{
    public class Entity : IEntity
    {
        [Required(ErrorMessage = @"Name is required"),
         StringLength(20, ErrorMessage = "Name maximum length is 50 characters")]
        public string Name { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }


        public bool IsActive { get; set; }

        [Key]
        public int Id { get; set; }
    }
}