namespace CursosOnline.Domain.Curso.Interfaces
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        Curso Obter(string nome);
    }
}
