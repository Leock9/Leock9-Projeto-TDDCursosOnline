using Bogus;
using CursosOnline.Domain.Curso;
using CursosOnline.Domain.Curso.Enums;
using CursosOnline.DomainTest._Builders;
using CursosOnline.DomainTest._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursosOnline.DomainTest.Cursos
{
    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _testOutputHelper.WriteLine("Contrutor sendo executado");
            var dadosAleatorios = new Faker();

            _nome = dadosAleatorios.Name.Random.String();
            _cargaHoraria = dadosAleatorios.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = dadosAleatorios.Finance.Random.Double();
            _descricao = dadosAleatorios.Lorem.Paragraph();
        }

        public void Dispose()
        {
            _testOutputHelper.WriteLine("Dispose sendo executado");
        }

        [Fact]
        public void CriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor, cursoEsperado.Descricao);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CursoNaoDeveTerNomeInvalido(string nomeInvalido)
        {

            Assert.Throws<ArgumentException>(() =>
            CursoBuilder
            .Novo()
            .ComNome(nomeInvalido)
            .Build()
            ).ValidarMensagem("Nome invalido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void CursoNaoDeveTerCargaHorariaMenorQueUm(double cargaHorariaInvalida)
        {
            Assert.Throws<ArgumentException>(() => CursoBuilder
            .Novo()
            .ComCargaHoraria(cargaHorariaInvalida)
            .Build()
            ).ValidarMensagem("Carga horária não pode ser menor que 0 horas");

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void CursoNaoDeveTerValorMenorQueUm(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() => CursoBuilder
            .Novo()
            .ComValor(valorInvalido)
            .Build()
            ).ValidarMensagem("O valor do curso não pode ser menor que 0 reais");
        }
    }
}

