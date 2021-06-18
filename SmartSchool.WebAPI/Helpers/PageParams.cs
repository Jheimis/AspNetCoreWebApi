namespace SmartSchool.WebAPI.Helpers
{
    public class PageParams
    {
        /// <summary>
        /// Número maximo de paginas
        /// </summary>
        public const int MaxPageSize = 50;

        /// <summary>
        /// Parametro para navegar nas páginas
        /// </summary>
        public int PageNumber { get; set; } = 1;

        private int pageSize { get; set; } = 10;

        /// <summary>
        /// Parametro que controla a quantidade de objetos por pagina
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        /// <summary>
        /// Parametro para busca pela matricula do aluno
        /// </summary>
        public int? Matricula { get; set; } = null;

        /// <summary>
        /// Parametro para busca pelo nome do aluno
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Parametro para busca de aluno ativos(1) ou inativos(0)
        /// </summary>
        public int? Ativo { get; set; } = null;

        /// <summary>
        /// Parametro que controla a ordenação em ordem alfabetica, onde 0 será em ordem desc e 1 para asc
        /// </summary>
        public int? orderByNome { get; set; } = null;
    }
}