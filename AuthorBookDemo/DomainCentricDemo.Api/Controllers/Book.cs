using AutoMapper;
using DomainCentricDemo.Application;
using DomainCentricDemo.Application.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainCentricDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Book : ControllerBase
    {
        private readonly IBookQuery _queryService;
        private readonly IBookCommand _command;
        private readonly IMapper _mapper;

        public Book(IBookQuery queryService, IBookCommand command, IMapper mapper)
        {
            _queryService = queryService;
            _command = command;
            _mapper = mapper;
        }
        // GET: api/<Book>
        [HttpGet]
        public IEnumerable<BookDto> Get()
        {
            return _queryService.GetAll();
        }

        // GET api/<Book>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _queryService.Get(id);
            if (result is null) return NotFound();
            
            return Ok(result);
        }

        // POST api/<Book>
        [HttpPost]
        public void Post([FromBody] BookDto book)
        {
            var commandDto = _mapper.Map<BookCreateCommandRequestDto>(book);
            _command.Create(commandDto);
        }

        // PUT api/<Book>
        [HttpPut]
        public void Put([FromBody] BookDto book)
        {
            var commandDto = _mapper.Map<BookUpdateRequestDto>(book);
            _command.Update(commandDto);
        }

        // DELETE api/<Book>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _command.Delete(id);
        }
    }
}
