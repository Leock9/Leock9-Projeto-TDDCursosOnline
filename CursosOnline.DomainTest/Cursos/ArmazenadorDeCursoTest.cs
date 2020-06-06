using CursosOnline.Domain.Curso;
using CursosOnline.Domain.Curso.Enum;
using Moq;
using Xunit;

namespace CursosOnline.DomainTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDto = new CursoDto
            {
                Nome = "Curso A",
                Descricao = "Teste",
                CargaHorario = 80,
                PublicoAlvoId = 1,
                Valor = 850.00
            };

            var cursoRepositoryMock = new Mock<ICursoRepository>();

            var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositoryMock.Object);

            armazenadorDeCurso.Armazenar(cursoDto);

            cursoRepositoryMock.Verify(armazenador => armazenador.Adicionar(It.IsAny<Curso>()));
        }
    }

    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
    }

    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepository _cursoRepository;

        public ArmazenadorDeCurso(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var curso = new Curso
                (
                    cursoDto.Nome,
                    cursoDto.CargaHorario,
                    PublicoAlvo.Estudante,
                    cursoDto.Valor,
                    cursoDto.Descricao
                 );

            _cursoRepository.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHorario { get; set; }
        public int PublicoAlvoId { get; set; }
        public double Valor { get; set; }
    }
}

