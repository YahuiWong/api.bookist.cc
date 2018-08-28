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
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(DefaultDbContext context) : base(context)
        {
        }

        public async Task<PageResult<BookResponseDto>> QueryAsync(BookQueryModel queryModel)
        {
            var query = DbSet.AsNoTracking();
            if (!string.IsNullOrEmpty(queryModel.Keyword))
            {
                query = query.Where(x => EF.Functions.Like(x.Title, LikeString(queryModel.Keyword)));
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Tag))
            {
                query = query.Where(x => x.BookTags.Any(bt => bt.Tag.Name == queryModel.Tag));
            }

            if (queryModel.Status.HasValue)
            {
                query = query.Where(x => x.Status == queryModel.Status.Value);
            }

            var result = new PageResult<BookResponseDto>();
            if (queryModel.EnableTotal)
            {
                result.Total = await query.CountAsync();
            }

            query = query.Include(x => x.Links)
                .Include(x => x.BookTags).ThenInclude(x => x.Tag)
                .OrderByDescending(x => x.Id);
            var list = await PageAsync(query, queryModel);
            result.List = Mapper.Map<List<BookResponseDto>>(list);

            return result;
        }
    }
}
