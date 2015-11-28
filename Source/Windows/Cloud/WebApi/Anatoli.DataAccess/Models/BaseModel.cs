using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models
{
    public abstract class BaseModel
    {
        public int Number_ID { get; set; }

        [Key]
        public Guid Id { get; set; }
    }
}