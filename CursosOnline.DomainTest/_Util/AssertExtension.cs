using System;
using Xunit;

namespace CursosOnline.DomainTest._Util
{
    public static class AssertExtension
    {
        public static void ValidarMensagem(this ArgumentException exception, string mensagem)
        {
            if (exception.Message == mensagem)
                Assert.True(true);
            else
                Assert.False(true, $"Esperava a mensagem '(mensagem)'");
        }
    }
}
