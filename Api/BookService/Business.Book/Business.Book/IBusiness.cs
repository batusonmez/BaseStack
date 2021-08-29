using BookManagementModels.Entities;
using Business.Book.DTO;
using Indexer;
using System.Threading.Tasks;

namespace Business.Book
{
    public interface IBusiness
    { 
        IndexResult<T> Search<T>(string term) where T : DTOBase;
    }
}
