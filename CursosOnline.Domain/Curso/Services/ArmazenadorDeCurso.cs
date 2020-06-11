using CursosOnline.Domain.Curso.DTOs;
using CursosOnline.Domain.Curso.Enums;
using CursosOnline.Domain.Curso.Interfaces;
using System;

namespace CursosOnline.Domain.Curso.Services
{
    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepository _cursoRepository;

        public ArmazenadorDeCurso(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public void Armazenar(CursoDTO cursoDto)
        {

            if (!Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, out var publicoAlvo))
                throw new ArgumentException("Publico Alvo invalido");

            var cursoJaSalvo = _cursoRepository.Obter(cursoDto.Nome);

            if (cursoJaSalvo != null)
                throw new ArgumentException("Nome do curso ja consta no banco de dados");

            var curso = new Curso
                (
                    cursoDto.Nome,
                    cursoDto.CargaHorario,
                    publicoAlvo,
                    cursoDto.Valor,
                    cursoDto.Descricao
                 ); ;

            _cursoRepository.Adicionar(curso);
        }
    }
}
