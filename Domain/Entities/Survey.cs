using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
   public class Survey : AuditableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        public string Location { get; set; }
        public int NumberOfPage { get; set; }
        public string Images { get; set; }
        public bool IsActive { get; set; }
    }
}
