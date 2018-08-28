using Anet;
using AutoMapper;
using Bookist.Core.Dtos;
using Bookist.Core.Models;
using Bookist.Core.Repositories;
using Bookist.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookist.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookRepository _bookRepository;
        private readonly BookService _bookService;
        public BooksController(BookRepository bookRepository, BookService bookService)
        {
            _bookRepository = bookRepository;
            _bookService = bookService;
        }

        [HttpGet]
        public Task<PageResult<BookResponseDto>> Get([FromQuery]BookQueryModel queryModel)
        {
            // 限制一页请求大小
            if (queryModel.Size > 50)
                throw new BadRequestException("一页最多请求50本书！");

            return _bookRepository.QueryAsync(queryModel);
        }

        [HttpGet("{id}")]
        public async Task<BookResponseDto> Get(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return Mapper.Map<BookResponseDto>(book);
        }

        [HttpPost]
        [AdminAuthorize]
        public async Task<long> PostAsync(BookRequestDto requestDto)
        {
            return await _bookService.CreateAsync(requestDto);
        }

        [HttpPut("{id}")]
        [AdminAuthorize]
        public async Task Put(long id, BookRequestDto requestDto)
        {
            await _bookService.UpdateAsync(id, requestDto);
        }

        [HttpDelete("{id}")]
        [AdminAuthorize]
        public async Task DeleteAsync(int id)
        {
            await _bookService.DeleteAsync(id);
        }
    }
}
