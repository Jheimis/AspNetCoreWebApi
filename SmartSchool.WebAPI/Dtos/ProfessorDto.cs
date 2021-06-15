using System;

namespace SmartSchool.WebAPI.Dtos
{
    public class ProfessorDto
    {
        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;

    }
}