﻿namespace Index.Application.Common
{
    public interface IIndexer
    {
        Task<bool> Index(string indexName, string id, object data);
        Task InitIndex(string indexName);
    }
}
