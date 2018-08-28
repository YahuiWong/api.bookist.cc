using Anet;
using AutoMapper;
using Bookist.Core.Dtos;
using Bookist.Core.Entities;
using Bookist.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bookist.Core.Services
{
    public class BookService : BaseService<Book>
    {
        private readonly BookRepository _bookRepository;
        private readonly TagRepository _tagRepository;
        private readonly LinkRepository _linkRepository;
        public BookService(
            DefaultDbContext context, 
            BookRepository bookRepository,
            TagRepository tagRepository, 
            LinkRepository linkRepository) 
            : base(context)
        {
            _bookRepository = bookRepository;
            _tagRepository = tagRepository;
            _linkRepository = linkRepository;
        }

        public async Task<long> CreateAsync(BookRequestDto dto)
        {
            dto.Sanitize();

            var book = Mapper.Map<Book>(dto);
            book.SetNewId();

            // 处理标签
            foreach (var tname in dto.Tags)
            {
                var tag = await _tagRepository.GetByName(tname);
                if (tag == null)
                    tag = new Tag() { Name = tname };
                book.BookTags.Add(new BookTag { Book = book, Tag = tag });
            }

            Context.Add(book);

            await Context.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateAsync(long id, BookRequestDto dto)
        {
            dto.Sanitize();

            var book = await _bookRepository.GetByIdAsync(id, 
                q => q.Include(x => x.BookTags).Include(x => x.Links));

            Mapper.Map(dto, book);

            // 处理标签，先移除标签与图书的关联再添加
            Context.RemoveRange(book.BookTags);
            foreach (var tname in dto.Tags)
            {
                var tag = await _tagRepository.GetByName(tname);
                if (tag == null)
                    tag = new Tag() { Name = tname };
                book.BookTags.Add(new BookTag { Book = book, Tag = tag });
            }

            // 处理下载链接，移除已删除项
            var oldLinks = await _linkRepository.GetByBookIdAsync(id);
            foreach(var oldLink in oldLinks)
            {
                if(!book.Links.Any(x=>x.Id == oldLink.Id))
                {
                    Context.Remove(oldLink);
                }
            }

            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new NotFoundException();

            Context.Remove(book);
            await Context.SaveChangesAsync();
        }
    }
}
