using Microsoft.AspNetCore.Mvc;
using ImportantDocuments.API.Domain;
using ImportantDocuments.Services;
using ImportantDocuments.DTOs;
using AutoMapper;

namespace ImportantDocuments.Controllers
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
            var tags = await _tagService.GetAllTagsAsync();
            var tagDTOs = tags.ToList().Select(_mapper.Map<Tag, TagReadDTO>);
                
            return Ok(tagDTOs);
        }

        // GET: api/tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetTagById([FromRoute] int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
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
            var tagDB = await _tagService.AddTagAsync(tag);

            tag = await _tagService.GetTagByIdAsync(tagDB.Id);
            var tagReadDTO = _mapper.Map<TagReadDTO>(tag);

            return Created($"api/tags/{tag.Id}", tagReadDTO);
        }

        //// DELETE: api/Tags/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTag(int id)
        //{
        //    var tag = await _context.Tags.FindAsync(id);
        //    if (tag == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Tags.Remove(tag);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
