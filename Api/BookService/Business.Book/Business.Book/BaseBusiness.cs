using AutoMapper;
using BookManagementModels.Entities;
using Business.Book.DTO;
using Indexer;
using System;
using System.Threading.Tasks;
using UnitOfWork;

namespace Business.Book
{
    public class BaseBusiness
    {
        internal readonly IIndexer indexer;

        public BaseBusiness(IUnitOfWork uow, IMapper mapper, IIndexer indexer)
        {

            this.indexer = indexer;
            Uow = uow;
            Mapper = mapper;
        }

        public IUnitOfWork Uow { get; }
        public IMapper Mapper { get; set; }

        /// <summary>
        /// Generic Update/Insert method for common use
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        internal async Task<T> Upsert<T, U>(T source, bool index = false) where T : DTOBase where U : EntityBase
        {
            var repo = Uow.CreateRepository<U>();
            var entity = Mapper.Map<U>(source);
            if (source.HasID)
            {
                repo.Update(entity);
            }
            else
            {
                repo.Insert(entity);
                source.ID = entity.ID;
            }

            await Uow.Commit();
            return source;
        }


        /// <summary>
        /// Search indexed documents
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="term"></param>
        /// <returns></returns>
        public   IndexResult<T> Search<T>(string term) where T : DTOBase
        {
            var refInstance= Activator.CreateInstance<T>() as DTOBase;
            return indexer.Search<T>(refInstance.IndexName, term);

        }

 
    }
}
