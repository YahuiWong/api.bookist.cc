using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anet;
using AutoMapper;
using Bookist.Core.Dtos;
using Bookist.Core.Entities;
using Bookist.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookist.Core.Repositories
{
    public class LinkRepository : BaseRepository<Link>
    {
        public LinkRepository(DefaultDbContext context) : base(context)
        {
        }

        public Task<List<Link>> GetByBookIdAsync(long bookId)
        {
            return DbSet.Where(x => x.BookId == bookId).ToListAsync();
        }
    }
}
