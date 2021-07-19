using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);
            var professoresDto = _mapper.Map<IEnumerable<ProfessorDto>>(professores);
            return Ok(professoresDto);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, true);
            if (professor == null) return BadRequest("Professor não encontrado!");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }

        [HttpGet("byaluno/{alunoId}")]
        public IActionResult GetByAlunoId(int alunoId)
        {
            var professores = _repo.GetProfessoresByAlunoId(alunoId, true);
            if (professores == null) return BadRequest("Professores não encontrados!");

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistarDto model)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado !");

            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistarDto model)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado !");

            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado !");

            _repo.Delete(professor);
            _repo.SaveChanges();
            return Ok(professor);

        }
    }
}