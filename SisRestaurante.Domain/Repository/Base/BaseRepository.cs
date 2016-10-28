using System;
using System.Data.Entity;
using System.Linq;
using SisRestaurante.Domain.Context;

namespace SisRestaurante.Domain.Repository.Base
{
    public abstract class BaseRepository<TEntity> :
        IDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        BancoContext _db = new BancoContext();

        public void Alterar(TEntity obj)
        {
            _db.Entry(obj).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Excluir(Func<TEntity, bool> predicate)
        {
            _db.Set<TEntity>().Where(predicate).ToList().ForEach(del => _db.Set<TEntity>().Remove(del));
        }

        public void Inserir(TEntity obj)
        {
            _db.Set<TEntity>().Add(obj);
        }

        public TEntity Procurar(params object[] key)
        {
            return _db.Set<TEntity>().Find(key);
        }

        public IQueryable<TEntity> Retonar(Func<TEntity, bool> predicate)
        {
            return RetornaTodos().Where(predicate).AsQueryable();
        }

        public IQueryable<TEntity> RetornaTodos()
        {
            return _db.Set<TEntity>();
        }

        public void SalvarTodos()
        {
            _db.SaveChanges();
        }
    }
}


