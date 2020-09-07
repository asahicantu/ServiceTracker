using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Well
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FieldId { get; set; }

        public virtual Field Field { get; set; }
    }
}
