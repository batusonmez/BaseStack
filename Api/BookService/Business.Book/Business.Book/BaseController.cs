using AutoMapper;
using BookManagementModels.Entities;
using Business.Book.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace Business.Book
{
    public class BaseController
    {
        public BaseController(IUnitOfWork uow, IMapper mapper)
        {
            Uow = uow;
            Mapper = mapper;
        }

        public IUnitOfWork Uow { get; }
        public IMapper Mapper { get; }

       
        /// <summary>
        /// Generic Update/Insert method for common use
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        internal async Task<T> Upsert<T, U>(T source) where T : DTOBase where U : EntityBase
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

    }
}
