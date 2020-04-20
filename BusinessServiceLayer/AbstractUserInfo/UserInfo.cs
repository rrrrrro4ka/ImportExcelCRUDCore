using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessServiceLayer.AbstractUserInfo
{
    public abstract class UserInfo<T> where T : class
    {
        public abstract Task Add(T entity);
        public abstract Task<IEnumerable<T>> GetAllInfoByDate(DateTime createDate);
        public abstract Task Update(T entity);
        public abstract Task Remove(int id);
    }
}
