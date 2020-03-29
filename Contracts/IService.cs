using System;
using System.Collections.Generic;

namespace FinancasMVC.Contracts
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Insert(T model);
        void Update(T model);
        void Remove(Guid id);
    }
}