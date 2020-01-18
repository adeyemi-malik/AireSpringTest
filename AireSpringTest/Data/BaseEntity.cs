using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireSpringTest.Data
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastModified { get; set; }
    }
}
