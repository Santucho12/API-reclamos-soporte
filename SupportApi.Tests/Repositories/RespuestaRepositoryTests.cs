using Xunit;
using SupportApi.Repositories;
using SupportApi.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SupportApi.Tests.Repositories
{
    public class RespuestaRepositoryTests
    {
        [Fact]
        public async Task CreateAsync_EmptyContent_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<SupportDbContext>().UseInMemoryDatabase(databaseName: "TestDbRespuestas").Options;
            using var context = new SupportDbContext(options);
            var repo = new RespuestaRepository(context);
            var respuesta = new Respuesta { Id = Guid.NewGuid(), Contenido = "", UsuarioId = Guid.NewGuid() };
            var result = await repo.CreateAsync(respuesta);
            Assert.Null(result);
        }
    }
}
