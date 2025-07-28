using Xunit;
using SupportApi.DTOs;
using SupportApi.Validators;

namespace SupportApi.Tests.Validators
{
    public class RespuestaValidatorTests
    {
        [Fact]
        public void Contenido_Requerido()
        {
            var validator = new RespuestaValidator();
            var dto = new RespuestaDto { Contenido = "", UsuarioId = System.Guid.NewGuid() };
            var result = validator.Validate(dto);
            Assert.False(result.IsValid);
        }
    }
}
