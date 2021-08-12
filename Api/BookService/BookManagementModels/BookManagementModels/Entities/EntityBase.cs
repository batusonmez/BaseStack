using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementModels.Entities
{
    /// <summary>
    /// Common properties for entities
    /// </summary>
    public abstract class EntityBase
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
