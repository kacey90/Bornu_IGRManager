﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Frontend.Contracts.Repository
{
    public interface IGenericRepository
    {
        Task<T> GetAsync<T>(string uri);
        Task<T> PostAsync<T>(string uri, T data);
        Task<T> PutAsync<T>(string uri, T data);
        Task DeleteAsync(string uri);
        Task<R> PostAsync<T, R>(string uri, T data);
    }
}
