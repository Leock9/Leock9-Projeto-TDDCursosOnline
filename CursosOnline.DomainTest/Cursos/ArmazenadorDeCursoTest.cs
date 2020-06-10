using Bogus;
using CursosOnline.Domain.Curso;
using CursosOnline.Domain.Curso.Enum;
using CursosOnline.DomainTest._Util;
using Moq;
using System;
using Xunit;

namespace CursosOnline.DomainTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepository> _cursoRepositoryMock;

        public ArmazenadorDeCursoTest()
        {
            var dadosAleatorios = new Faker();

            _cursoDto = new CursoDto
            {
                Nome = dadosAleatorios.Name.Random.String(),
                Descricao = dadosAleatorios.Lorem.Paragraph(),
                CargaHorario = dadosAleatorios.Random.Double(50, 100),
                PublicoAlvo = "Estudante",
                Valor = dadosAleatorios.Finance.Random.Double(),
            };

            _cursoRepositoryMock = new Mock<ICursoRepository>();

            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositoryMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositoryMock.Verify(armazenador => armazenador.Adicionar
            (
                It.Is<Curso>(curso => curso.Nome.Equals(_cursoDto.Nome) && curso.Descricao.Equals(_cursoDto.Descricao))
             ));
        }

        [Theory]
        [InlineData("Medico")]
        [InlineData("Enfermeiro")]
        [InlineData("Publicitario")]
        public void NaoDeveInformarPublicoAlvoInvalido(string publicAlvoInvalido) 
        {
            _cursoDto.PublicoAlvo = publicAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ValidarMensagem("Publico Alvo invalido");
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

            Enum.TryParse(typeof(PublicoAlvo), cursoDto.PublicoAlvo, out var publicoAlvo);

            if (publicoAlvo == null)
                throw new ArgumentException("Publico Alvo invalido");

            var curso = new Curso
                (
                    cursoDto.Nome,
                    cursoDto.CargaHorario,
                    (PublicoAlvo)publicoAlvo,
                    cursoDto.Valor,
                    cursoDto.Descricao
                 ); ;

            _cursoRepository.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHorario { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}

