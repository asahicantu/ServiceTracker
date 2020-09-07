using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Field
    {
        public Field()
        {
            Well = new HashSet<Well>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Well> Well { get; set; }
    }
}
