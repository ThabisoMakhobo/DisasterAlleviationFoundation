using DisasterAlleviationFoundation.Controllers;
using DisasterAlleviationFoundation.Data;
using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace DisasterAlleviation.Tests
{
    public class DonationsControllerTests
    {
        private readonly GiftOfTheGiversDbContext _context;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly DonationsController _controller;

        public DonationsControllerTests()
        {
            // ✅ Setup InMemory database
            var options = new DbContextOptionsBuilder<GiftOfTheGiversDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new GiftOfTheGiversDbContext(options);

            // ✅ Mock UserManager
            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            // ✅ Create controller
            _controller = new DonationsController(_context, _userManagerMock.Object);

            // ✅ Fake user for authorization
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
                new Claim(ClaimTypes.Name, "test@example.com")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task Index_ReturnsViewWithDonations()
        {
            // Arrange
            var user = new User { Id = "1", UserName = "user1" };
            var resource = new Resource { ResourceID = 1, Name = "Blankets", Quantity = 10 };
            _context.Users.Add(user);
            _context.Resources.Add(resource);
            _context.Donations.Add(new Donation
            {
                DonationID = 1,
                UserID = user.Id,
                ResourceID = resource.ResourceID,
                Amount = 100,   
                Date = DateTime.Now
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Donation>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Create_Get_ReturnsView()
        {
            // Act
            var result = _controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task Create_Post_ValidDonation_RedirectsToIndex()
        {
            // Arrange
            var user = new User { Id = "test-user-id", UserName = "test@example.com" };
            _userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            _context.Resources.Add(new Resource { ResourceID = 1, Name = "Food", Quantity = 10 });
            await _context.SaveChangesAsync();

            var donation = new Donation
            {
                ResourceID = 1,
                Amount = 100,   
            };

            // Act
            var result = await _controller.Create(donation);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.True(_context.Donations.Any());
        }

        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            _controller.ModelState.AddModelError("Quantity", "Required");

            // Act
            var result = await _controller.Create(new Donation());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesDonation_AndRedirects()
        {
            // Arrange
            var donation = new Donation { DonationID = 1, ResourceID = 1,  Amount = 100 , Date = DateTime.Now };
            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Empty(_context.Donations);
        }
    }
}
