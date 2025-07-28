using Xunit;
using SupportApi.Repositories;
using SupportApi.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SupportApi.Tests.Repositories
{
    public class ReclamoRepositoryTests
    {
        [Fact]
        public async Task CreateAsync_DuplicateTitle_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<SupportDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            using var context = new SupportDbContext(options);
            var repo = new ReclamoRepository(context);
            var usuarioId = Guid.NewGuid();
            var reclamo1 = new Reclamo { Id = Guid.NewGuid(), Titulo = "Duplicado", Descripcion = "Desc", UsuarioId = usuarioId };
            var reclamo2 = new Reclamo { Id = Guid.NewGuid(), Titulo = "Duplicado", Descripcion = "Desc", UsuarioId = usuarioId };
            await repo.CreateAsync(reclamo1);
            var result = await repo.CreateAsync(reclamo2);
            Assert.Null(result);
        }
    }
}
