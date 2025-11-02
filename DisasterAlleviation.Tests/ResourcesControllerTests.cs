using DisasterAlleviationFoundation.Controllers;
using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DisasterAlleviation.Tests
{
    public class ResourcesControllerTests
    {
        private GiftOfTheGiversDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<GiftOfTheGiversDbContext>()
                .UseInMemoryDatabase(databaseName: "ResourcesControllerTestDB")
                .Options;

            var context = new GiftOfTheGiversDbContext(options);

            // Clear existing data to ensure test isolation
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public async Task Index_ReturnsViewWithResources()
        {
            // Arrange
            var context = GetDbContext();
            context.Resources.AddRange(
                new Resource { ResourceID = 1, Name = "Water", Quantity = 10 },
                new Resource { ResourceID = 2, Name = "Blankets", Quantity = 5 }
            );
            await context.SaveChangesAsync();

            var controller = new ResourcesController(context);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Resource>>(result.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsView_WhenResourceExists()
        {
            // Arrange
            var context = GetDbContext();
            var resource = new Resource { ResourceID = 1, Name = "Food", Quantity = 100 };
            context.Resources.Add(resource);
            await context.SaveChangesAsync();

            var controller = new ResourcesController(context);

            // Act
            var result = await controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(resource, result.Model);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenResourceMissing()
        {
            // Arrange
            var controller = new ResourcesController(GetDbContext());

            // Act
            var result = await controller.Details(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_Post_ValidResource_RedirectsToIndex()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new ResourcesController(context);
            var resource = new Resource { ResourceID = 1, Name = "Medicine", Quantity = 50 };

            // Act
            var result = await controller.Create(resource) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Single(context.Resources);
        }

        [Fact]
        public async Task Edit_Post_ValidResource_UpdatesAndRedirects()
        {
            // Arrange
            var context = GetDbContext();
            var resource = new Resource { ResourceID = 1, Name = "Tents", Quantity = 10 };
            context.Resources.Add(resource);
            await context.SaveChangesAsync();

            var controller = new ResourcesController(context);

            // Act
            resource.Quantity = 20;
            var result = await controller.Edit(1, resource) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            var updated = await context.Resources.FindAsync(1);
            Assert.Equal(20, updated.Quantity);
        }

        [Fact]
        public async Task Edit_Post_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new ResourcesController(GetDbContext());
            var resource = new Resource { ResourceID = 2, Name = "Shoes", Quantity = 10 };

            // Act
            var result = await controller.Edit(1, resource);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesResourceAndRedirects()
        {
            // Arrange
            var context = GetDbContext();
            var resource = new Resource { ResourceID = 1, Name = "Soap", Quantity = 30 };
            context.Resources.Add(resource);
            await context.SaveChangesAsync();

            var controller = new ResourcesController(context);

            // Act
            var result = await controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Empty(context.Resources);
        }

        [Fact]
        public async Task Delete_ReturnsView_WhenResourceExists()
        {
            // Arrange
            var context = GetDbContext();
            var resource = new Resource { ResourceID = 1, Name = "Rice", Quantity = 200 };
            context.Resources.Add(resource);
            await context.SaveChangesAsync();

            var controller = new ResourcesController(context);

            // Act
            var result = await controller.Delete(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(resource, result.Model);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenResourceMissing()
        {
            // Arrange
            var controller = new ResourcesController(GetDbContext());

            // Act
            var result = await controller.Delete(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
