﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AirBench.Data
{
    public interface IBenchRepository
    {
        Task<List<Bench>> GetBenchListAsync();
        Task<Bench> GetBenchSingleAsync(int id);
        Task<Bench> AddBenchAsync(int id, decimal longitude, decimal latitude, string name);
        Task<Comment> AddCommentAsync(int benchId, string text, int rating);

    }
}