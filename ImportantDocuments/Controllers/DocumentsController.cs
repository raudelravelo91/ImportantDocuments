using AutoMapper;
using ImportantDocuments.API.DTOs;
using ImportantDocuments.API.Services;
using ImportantDocuments.Domain;
using ImportantDocuments.DTOs;
using ImportantDocuments.Services;
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
            var docs = await _docService.GetAll();
            var docDTOs = docs.ToList().Select(_mapper.Map<Document, DocumentReadDTO>);

            return Ok(docDTOs);
        }

        // GET: api/docs/5
        [HttpGet("{id:int}", Name = "GetDocumentById")]
        public async Task<ActionResult<Document>> GetDoc([FromRoute] int id)
        {
            var doc = await _docService.GetById(id);
            var docDTO = _mapper.Map<DocumentDTO>(doc);

            return Ok(docDTO);
        }

        // POST: api/docs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentDTO>> PostDoc(DocumentCreationDTO docCreationDTO)
        {
            var doc = _mapper.Map<Document>(docCreationDTO);
            var docDb = await _docService.AddDocAsync(doc);
            
            var docReadDto = _mapper.Map<DocumentDTO>(docDb);

            return CreatedAtRoute("GetDocumentById", new { id = docDb.Id }, docReadDto);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteDoc(int id)
        {
            await _docService.Delete(id);
            return Ok();
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutDoc(int id, DocumentPutDto docPutDto)
        {
            if (id != docPutDto.Id)
                return BadRequest();
            
            var doc = _mapper.Map<Document>(docPutDto);
            var docDb = await _docService.Update(doc);
            var docReadDto = _mapper.Map<DocumentDTO>(docDb);
            return Ok(docReadDto);
        }
    }
}
