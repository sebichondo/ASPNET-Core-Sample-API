using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApp.Models
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
