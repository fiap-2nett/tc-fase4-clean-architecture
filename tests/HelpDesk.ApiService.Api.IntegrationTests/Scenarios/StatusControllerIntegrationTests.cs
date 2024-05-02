using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using HelpDesk.ApiService.Api.Controllers;
using HelpDesk.ApiService.Api.IntegrationTests.Extensions;
using HelpDesk.ApiService.Api.IntegrationTests.Fixtures;
using HelpDesk.ApiService.Api.IntegrationTests.SeedWork;
using HelpDesk.ApiService.Application.Contracts.Tickets;
using HelpDesk.ApiService.Domain.Extensions;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace HelpDesk.ApiService.Api.IntegrationTests.Scenarios
{
    [Collection(nameof(ApiCollection))]
    public sealed class StatusControllerIntegrationTests : IAsyncLifetime
    {
        #region Read-Only Fields

        private readonly UserFixture _userFixture;
        private readonly Func<Task> _resetDatabase;
        private readonly TestHostFixture _testHostFixture;
        private readonly TicketStatusFixture _ticketStatusFixture;

        #endregion

        #region Constructors

        public StatusControllerIntegrationTests(TestHostFixture fixture)
        {
            _testHostFixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
            _ticketStatusFixture = new TicketStatusFixture(_testHostFixture);
            _userFixture = new UserFixture(_testHostFixture);
            _resetDatabase = fixture.ResetDatabaseAsync;
        }

        #endregion

        #region Tests

        #region Get

        [Fact]
        public async Task Get_ShouldReturnUnauthorized_WhenWithoutIdentity()
        {
            // Act
            var response = await _testHostFixture.Server
                .CreateHttpApiRequest<StatusController>(controller => controller.Get())
                .GetAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Get_ShouldReturnStatusResponseList_WhenAutenticatedUser()
        {
            // Arrange
            var fixtureResult = await _ticketStatusFixture.SetFixtureAsync();
            var userPerformedAction = (await _userFixture.SetFixtureAsync()).FirstOrDefault();

            // Act
            var response = await _testHostFixture.Server
                .CreateHttpApiRequest<StatusController>(controller => controller.Get())
                .WithIdentity(userPerformedAction)
                .GetAsync();

            // Assert
            await response.IsSuccessStatusCodeOrThrow();
            var responseContent = await response.ReadContentAsAsync<IEnumerable<StatusResponse>>();

            responseContent.IsNullOrEmpty().Should().BeFalse();
            responseContent.Should().HaveCount(fixtureResult.Count());
            responseContent.All(x => !x.Name.IsNullOrEmpty()).Should().BeTrue();
        }

        #endregion

        #region GetById

        [Fact]
        public async Task GetById_ShouldReturnUnauthorized_WhenWithoutIdentity()
        {
            // Act
            var response = await _testHostFixture.Server
                .CreateHttpApiRequest<StatusController>(controller => controller.Get(default))
                .GetAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetById_ShouldStatusResponse_WhenValidId()
        {
            // Arrange
            var userPerformedAction = (await _userFixture.SetFixtureAsync()).FirstOrDefault();
            var targetTicketStatus = (await _ticketStatusFixture.SetFixtureAsync()).FirstOrDefault();

            // Act
            var response = await _testHostFixture.Server
                .CreateHttpApiRequest<StatusController>(controller => controller.Get(targetTicketStatus.Id))
                .WithIdentity(userPerformedAction)
                .GetAsync();

            // Assert
            await response.IsSuccessStatusCodeOrThrow();
            var responseContent = await response.ReadContentAsAsync<StatusResponse>();

            responseContent.Should().NotBeNull();
            responseContent.IdStatus.Should().Be(targetTicketStatus.Id);
            responseContent.Name.Should().Be(targetTicketStatus.Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenInvalidId()
        {
            // Arrange
            var userPerformedAction = (await _userFixture.SetFixtureAsync()).FirstOrDefault();

            // Act
            var response = await _testHostFixture.Server
                .CreateHttpApiRequest<StatusController>(controller => controller.Get(default))
                .WithIdentity(userPerformedAction)
                .GetAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion

        #endregion

        #region IAsyncLifetime Members

        public Task InitializeAsync()
            => Task.CompletedTask;

        public Task DisposeAsync()
            => _resetDatabase();

        #endregion
    }
}
