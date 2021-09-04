using Business.Comment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Comment
{
    /// <summary>
    /// Sample InMemory Business
    /// </summary>
    public interface ICommentBusiness
    {

        /// <summary>
        /// Save Comment
        /// </summary>
        /// <param name="comment"></param>
        void SaveComment(CommentDTO comment);

        /// <summary>
        /// Load Comment List
        /// </summary>
        /// <param name="contextID"></param>
        /// <returns></returns>
        IEnumerable<CommentDTO> Get(string contextID);
    }
}
