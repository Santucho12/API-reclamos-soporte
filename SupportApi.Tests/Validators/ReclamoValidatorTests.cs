using Xunit;
using SupportApi.DTOs;
using SupportApi.Validators;

namespace SupportApi.Tests.Validators
{
    public class ReclamoValidatorTests
    {
        [Fact]
        public void Titulo_Requerido()
        {
            var validator = new ReclamoValidator();
            var dto = new ReclamoDto { Titulo = "", Descripcion = "desc", Estado = "Abierto", UsuarioId = System.Guid.NewGuid() };
            var result = validator.Validate(dto);
            Assert.False(result.IsValid);
        }
    }
}
