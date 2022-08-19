using AutoMapper;
using ImportantDocuments.API.Domain;
using ImportantDocuments.DTOs;
using ImportantDocuments.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImportantDocuments.Controllers
{
    [ApiController]
    [Route("api/docs")]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;
        private readonly IDocService _docService;
        private readonly IMapper _mapper;
        

        public DocumentsController(IDocService docService, ILogger<DocumentsController> logger, IMapper mapper)
        {
            _docService = docService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/docs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocs()
        {
            var docs = await _docService.GetAllAsync();
            var docDTOs = docs.ToList().Select(_mapper.Map<Document, DocumentReadDTO>);

            return Ok(docDTOs);
        }

        // GET: api/docs/5
        [HttpGet("{id:int}", Name = "GetDocumentById")]
        public async Task<ActionResult<Document>> GetDoc([FromRoute] int id)
        {
            var doc = await _docService.GetByIdAsync(id);
            var docDTO = _mapper.Map<DocumentDTO>(doc);

            return Ok(docDTO);
        }

        // POST: api/docs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentDTO>> PostTag(DocumentCreationDTO docCreationDTO)
        {
            var doc = _mapper.Map<Document>(docCreationDTO);
            var docDB = await _docService.InsertAsync(doc);

            doc = await _docService.GetByIdAsync(docDB.Id);
            var docReadDTO = _mapper.Map<DocumentDTO>(doc);

            return CreatedAtRoute("GetDocumentById", new { id = docDB.Id }, docReadDTO);
        }
    }
}
