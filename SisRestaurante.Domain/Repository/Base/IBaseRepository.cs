using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRestaurante.Domain.Repository.Base
{
    interface IBaseRepository<TEntity> where TEntity : class 
    {
        IQueryable<TEntity> RetornaTodos();
        IQueryable<TEntity> Retonar(Func<TEntity, bool> predicate);
        TEntity Procurar(params object[] key);
        void SalvarTodos();
        void Alterar(TEntity obj);
        void Excluir(Func<TEntity, bool> predicate);
        void Inserir(TEntity obj);

    }
}
