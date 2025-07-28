using Xunit;
using SupportApi.DTOs;
using SupportApi.Validators;

namespace SupportApi.Tests.Validators
{
    public class UsuarioValidatorTests
    {
        [Fact]
        public void Email_RequeridoYFormato()
        {
            var validator = new UsuarioValidator();
            var dto = new UsuarioDto { Nombre = "Test", Email = "", Rol = "user" };
            var result = validator.Validate(dto);
            Assert.False(result.IsValid);
        }
    }
}
