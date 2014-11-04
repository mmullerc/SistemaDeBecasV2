﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
        T GetByNombre(String pnombre);
        void Save();
        void Clear();
    }
}
