using System.Collections.Generic;
using System.Threading.Tasks;
using Anet;
using Bookist.Core.Dtos;
using Bookist.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly TagRepository _tagRepository;
        public TagsController(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public Task<PageResult<TagResponseDto>> Get([FromQuery]PageQuery queryModel)
        {
            return _tagRepository.QueryAsync(queryModel);
        }

        [HttpGet("details")]
        public async Task<IEnumerable<TagDetailResponseDto>> GetAllDetails()
        {
            return await _tagRepository.GetAllDetailsAsync();
        }

        [HttpGet("trending")]
        public async Task<IEnumerable<TagDetailResponseDto>> GetTrendings()
        {
            return await _tagRepository.GetTrendingsAsync(30);
        }
    }
}
