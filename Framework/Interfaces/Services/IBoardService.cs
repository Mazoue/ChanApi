﻿using Framework.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Interfaces.Services
{
    public interface IBoardService 
    {
        Task<AllBoards> GetAllBoards();
        Task<IEnumerable<Catalogue>> GetBoardCatalog(string board);
        Task<ThreadPosts> GetThreadPosts(string board, string threadNumber);
    }
}
