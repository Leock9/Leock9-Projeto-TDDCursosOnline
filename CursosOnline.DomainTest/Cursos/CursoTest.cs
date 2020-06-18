using Bogus;
using CursosOnline.Domain.Curso;
using CursosOnline.Domain.Curso.Enums;
using CursosOnline.Domain.Curso.Resources;
using CursosOnline.DomainTest._Builders;
using CursosOnline.DomainTest._Util;
using ExpectedObjects;
using System;
using Xunit;

namespace CursosOnline.DomainTest.Cursos
{
    public class CursoTest
    {
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest()
        {
            var dadosAleatorios = new Faker();

            _nome = dadosAleatorios.Name.Random.String();
            _cargaHoraria = dadosAleatorios.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = dadosAleatorios.Finance.Random.Double();
            _descricao = dadosAleatorios.Lorem.Paragraph();
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
            ).ValidarMensagem(CursoResource.NomeInvalido);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CursoNaoDeveTerDescricaoEmBranco(string descricaoInvalida)
        {

            Assert.Throws<ArgumentException>(() =>
            CursoBuilder
            .Novo()
            .ComDescricao(descricaoInvalida)
            .Build()
            ).ValidarMensagem(CursoResource.DescricaoInvalida);
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
            ).ValidarMensagem(CursoResource.CargaHorariaInvalida);

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
            ).ValidarMensagem(CursoResource.ValorCursoInvalido);
        }
    }
}

