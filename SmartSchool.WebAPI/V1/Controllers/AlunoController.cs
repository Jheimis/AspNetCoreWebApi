using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.V1.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        public readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Método responsável por retornar todos os alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);
            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }

        /// <summary>
        /// Método responsável por retornar um único aluno por meio do id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        /// <summary>
        /// Método responsável por cadastrar um novo aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado");
        }

        /// <summary>
        /// Método responsável por atualizar um aluno por meio do id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado");
        }

        /// <summary>
        /// Método responsável por atualizar um aluno por meio do id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado");
        }

        /// <summary>
        /// Método responsável por deletar o aluno pelo id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {

                return Ok("Aluno deletado");

            }

            return BadRequest("Aluno não deletado");
        }
    }
}