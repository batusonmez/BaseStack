using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Comment.DTO
{
  public  class CommentDTO
    {
        public string ContextID { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public CommentDTO()
        {
            Date = DateTime.Now;
        }
    }
}
