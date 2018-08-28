using Anet;
using AutoMapper;
using Bookist.Core.Dtos;
using Bookist.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookist.Core.Repositories
{
    public class TagRepository : BaseRepository<Tag>
    {
        public TagRepository(DefaultDbContext context) : base(context)
        {
        }

        public Task<Tag> GetByName(string name)
        {
            return DbSet.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<PageResult<TagResponseDto>> QueryAsync(PageQuery queryModel)
        {
            var query = DbSet.AsNoTracking();
            if (!string.IsNullOrEmpty(queryModel.Keyword))
            {
                query = query.Where(x => EF.Functions.Like(x.Name, LikeString(queryModel.Keyword)));
            }

            var result = new PageResult<TagResponseDto>();
            if (queryModel.EnableTotal)
            {
                result.Total = await query.CountAsync();
            }

            var list = await PageAsync(query.OrderBy(x => x.Name), queryModel);
            result.List = Mapper.Map<List<TagResponseDto>>(list);

            return result;
        }

        public Task<List<TagDetailResponseDto>> GetTrendingsAsync(int top)
        {
            // 由于暂时没有用户行为数据，热门度暂时取图书数量
            return GetDetailsQueryable().Take(top).ToListAsync();
        }

        public Task<List<TagDetailResponseDto>> GetAllDetailsAsync()
        {
            return GetDetailsQueryable().ToListAsync();
        }

        private IQueryable<TagDetailResponseDto> GetDetailsQueryable()
        {
            return Context.Set<BookTag>()
                .AsNoTracking()
                .GroupBy(x => new { x.TagId, x.Tag.Name })
                .Select(g => new TagDetailResponseDto { BookCount = g.Count(), Id = g.Key.TagId, Name = g.Key.Name })
                .OrderByDescending(x => x.BookCount);
        }
    }
}
