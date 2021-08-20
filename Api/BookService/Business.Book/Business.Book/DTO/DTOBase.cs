using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Book.DTO
{
    public abstract class DTOBase
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool HasID
        {
            get
            {

                return ID != default(Guid);
            }
        }
    }
}
