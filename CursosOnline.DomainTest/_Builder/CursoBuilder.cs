using CursosOnline.Domain.Curso;
using CursosOnline.Domain.Curso.Enums;

namespace CursosOnline.DomainTest._Builders
{
    public class CursoBuilder
    {
        private string _nome = "Leonardo Carvalho";
        private double _cargaHoraria = 80;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private double _valor = 950;
        private string _descricao = "Curso teste";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public Curso Build() => new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor, _descricao);

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }
    }
}
