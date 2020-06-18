using Bogus;
using CursosOnline.Domain.Curso;
using CursosOnline.Domain.Curso.DTOs;
using CursosOnline.Domain.Curso.Interfaces;
using CursosOnline.Domain.Curso.Resources;
using CursosOnline.Domain.Curso.Services;
using CursosOnline.DomainTest._Builders;
using CursosOnline.DomainTest._Util;
using Moq;
using System;
using Xunit;

namespace CursosOnline.DomainTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDTO _cursoDTO;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepository> _cursoRepositoryMock;

        public ArmazenadorDeCursoTest()
        {
            var dadosAleatorios = new Faker();

            _cursoDTO = new CursoDTO
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
            _armazenadorDeCurso.Armazenar(_cursoDTO);

            _cursoRepositoryMock.Verify(armazenador => armazenador.Adicionar
            (
                It.Is<Curso>(curso => curso.Nome.Equals(_cursoDTO.Nome) && curso.Descricao.Equals(_cursoDTO.Descricao))
             ));
        }

        [Theory]
        [InlineData("Medico")]
        [InlineData("Enfermeiro")]
        [InlineData("Publicitario")]
        public void NaoDeveInformarPublicoAlvoInvalido(string publicAlvoInvalido) 
        {
            _cursoDTO.PublicoAlvo = publicAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
                .ValidarMensagem(CursoResource.PublicoAlvoInvalido);
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNome()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDTO.Nome).Build();

            _cursoRepositoryMock.Setup(dados => dados.Obter(_cursoDTO.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
           .ValidarMensagem(CursoResource.NomeCursoDuplicado);
        }
    }



}

