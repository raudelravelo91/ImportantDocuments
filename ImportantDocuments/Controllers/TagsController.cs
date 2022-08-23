using AutoMapper;
using ImportantDocuments.API.Domain;
using ImportantDocuments.API.DTOs;
using ImportantDocuments.API.Services;
using ImportantDocuments.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ImportantDocuments.API.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly ILogger<TagsController> _logger;
        private readonly IMapper _mapper;

        public TagsController(ITagService tagService, ILogger<TagsController> logger, IMapper mapper)
        {
            _tagService = tagService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTags()
        {
            var tags = await _tagService.GetAllAsync();
            var tagDTOs = tags.ToList().Select(_mapper.Map<Tag, TagReadDTO>);
                
            return Ok(tagDTOs);
        }

        // GET: api/tags/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tag>> GetTagById([FromRoute] int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            var tagDTO = _mapper.Map<TagDTO>(tag);

            return Ok(tagDTO);
        }

        // GET: api/tags/get?name=value
        [HttpGet("get")]
        public async Task<ActionResult<TagDTO>> GetTagByName([FromQuery] string name)
        {
            var tag = await _tagService.GetTagByNameAsync(name);
            var tagDTO = _mapper.Map<TagDTO>(tag);

            return Ok(tagDTO);
        }

        // POST: api/tags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagDTO>> PostTag(TagCreationDTO tagCreationDTO)
        {
            var tag = _mapper.Map<Tag>(tagCreationDTO);
            var tagDb = await _tagService.AddTagAsync(tag);
            
            var tagReadDTO = _mapper.Map<TagReadDTO>(tagDb);

            return Created($"api/tags/{tag.Id}", tagReadDTO);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await _tagService.DeleteAsync(id);
            return Ok();
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutTag(int id, TagPutDTO tagPutDto)
        {
            if (id != tagPutDto.Id)
                return BadRequest();
            
            var tag = _mapper.Map<Tag>(tagPutDto);
            var tagDb = await _tagService.UpdateAsync(tag);
            var tagReadDto = _mapper.Map<TagDTO>(tagDb);
            return Ok(tagReadDto);
        }
    }
}
