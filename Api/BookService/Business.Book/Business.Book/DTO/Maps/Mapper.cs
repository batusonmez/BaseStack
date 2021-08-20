using BookManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Book.DTO.Maps
{
    /// <summary>
    /// Mapping extansions for entities and DTO's
    /// </summary>
   public static class Mapper
    {
        public static BooksDTO Map(this Books source)
        {
            return new BooksDTO()
            {
                CreationDate = source.CreationDate,
                Description=source.Description,
                ID=source.ID,
                Title=source.Title
            };
        }

        public static Books Map(this BooksDTO source)
        {
            return new Books()
            {
                CreationDate = source.CreationDate,
                Description = source.Description,
                ID = source.ID,
                Title = source.Title
            };
        }
    }
}
