using AutoMapper;
using CandidateApp.Business.AppServices;
using CandidateApp.DataAccess.DataRepositories.EntityRepos;
using CandidateApp.Domain.AppEntities;
using CandidateApp.Domain.Shared.Utilities;
using CandidateApp.Dtos.Requests;
using CandidateApp.Dtos.Responeses;
using FluentAssertions;
using Moq;

namespace CandidateApp.Test
{
    public class JobCandidateAppServicesTests
    {
        private readonly Mock<IJobCandidateRepo> _jobCandidateRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly JobCandidateAppServices _jobCandidateAppServices;

        public JobCandidateAppServicesTests()
        {
            _jobCandidateRepoMock = new Mock<IJobCandidateRepo>();
            _mapperMock = new Mock<IMapper>();
            _jobCandidateAppServices = new JobCandidateAppServices(_jobCandidateRepoMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task CreateOrUpdateCandidateAsync_CandidateDoesNotExist_CreatesNewCandidate()
        {
            // Arrange
            var requestDto = new JobCandidateAddUpdateRequestDto
            {
                Email = "user@yes.com",
                FirstName = "sagar",
                LastName = "gupta",
                PhoneNumber = "1234567890",
                TimeIntervalInSecond = 30,
                LinkedInUrl = "https://linkedin.com/in/sagarkrgupta",
                GitHubUrl = "https://github.com/sagarkrgupta",
                Comment = "Test comment"
            };

            _jobCandidateRepoMock.Setup(repo => repo.GetJobCandidateByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                                 .ReturnsAsync((JobCandidate)null); // Simulate that the candidate does not exist

            var candidate = new JobCandidate(); // This would be the domain model
            _mapperMock.Setup(mapper => mapper.Map<JobCandidate>(It.IsAny<JobCandidateAddUpdateRequestDto>()))
                       .Returns(candidate); // Simulate mapping from DTO to domain model

            _jobCandidateRepoMock.Setup(repo => repo.CreateAsync(It.IsAny<JobCandidate>(), It.IsAny<CancellationToken>()))
                                 .Returns(Task.CompletedTask); // Simulate creation of a new candidate

            var expectedResponseDto = new JobCandidateItemResponseDto(); // Expected mapped DTO
            _mapperMock.Setup(mapper => mapper.Map<JobCandidateItemResponseDto>(It.IsAny<JobCandidate>()))
                       .Returns(expectedResponseDto); // Simulate mapping from domain model to response DTO

            // Act
            var result = await _jobCandidateAppServices.CreateOrUpdateCandidateAsync(requestDto, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResponseDto); // Assert that the result is the same as the expected response DTO
            _jobCandidateRepoMock.Verify(repo => repo.CreateAsync(It.IsAny<JobCandidate>(), It.IsAny<CancellationToken>()), Times.Once); // Verify CreateAsync is called once
        }

        [Fact]
        public async Task CreateOrUpdateCandidateAsync_CandidateExists_UpdatesCandidate()
        {
            // Arrange
            var requestDto = new JobCandidateAddUpdateRequestDto
            {
                Email = "user@yes.com",
                FirstName = "sagar kumar",
                LastName = "gupta",
                PhoneNumber = "1234567890",
                TimeIntervalInSecond = 30,
                LinkedInUrl = "https://linkedin.com/in/sagarkrgupta",
                GitHubUrl = "https://github.com/sagarkrgupta",
                Comment = "Test comment"
            };

            var existingCandidate = new JobCandidate
            {
                Id = 1,
                Email = "user@yes.com",
                FirstName = "jane",
                LastName = "doe",
                PhoneNumber = "97787654321",
                TimeIntervalInSecond = 20,
                LinkedInUrl = "https://linkedin.com/in/janedoe",
                GitHubUrl = "https://github.com/janedoe",
                Comment = "Existing comment"
            };

            _jobCandidateRepoMock.Setup(repo => repo.GetJobCandidateByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(existingCandidate);

            _mapperMock.Setup(mapper => mapper.Map<JobCandidate>(It.IsAny<JobCandidateAddUpdateRequestDto>()))
                       .Returns(existingCandidate);

            _jobCandidateRepoMock.Setup(repo => repo.UpdateAsync(It.IsAny<JobCandidate>(), It.IsAny<CancellationToken>()))
                                 .Returns(Task.CompletedTask);

            var expectedResponseDto = new JobCandidateItemResponseDto(); // Expected mapped DTO
            _mapperMock.Setup(mapper => mapper.Map<JobCandidateItemResponseDto>(It.IsAny<JobCandidate>()))
                       .Returns(expectedResponseDto);

            // Act
            var result = await _jobCandidateAppServices.CreateOrUpdateCandidateAsync(requestDto, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResponseDto);

            _jobCandidateRepoMock.Verify(repo => repo.UpdateAsync(It.IsAny<JobCandidate>(), It.IsAny<CancellationToken>()), Times.Once);
        }




        [Fact]
        public async Task GetAllAsync_ReturnsCandidates()
        {
            // Arrange
            var candidates = new List<JobCandidate>
            {
                new JobCandidate { Id = 1, FirstName = "Sean", LastName = "Sean" },
                new JobCandidate { Id = 2, FirstName = "Ram", LastName = "Rai" }
            };

            _jobCandidateRepoMock.Setup(repo => repo.ListAsync(It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(candidates); // Simulate getting all candidates

            var expectedResponse = new List<JobCandidateItemResponseDto>
            {
                new JobCandidateItemResponseDto { Id = "validEncryptedId", FirstName = "Sean", LastName = "Sean" },
                new JobCandidateItemResponseDto { Id = "validEncryptedId", FirstName = "Ram", LastName = "Rai" }
            };

            _mapperMock.Setup(mapper => mapper.Map<List<JobCandidateItemResponseDto>>(It.IsAny<List<JobCandidate>>()))
                       .Returns(expectedResponse); 

            // Act
            var result = await _jobCandidateAppServices.GetAllAsync(CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse); 
            _jobCandidateRepoMock.Verify(repo => repo.ListAsync(It.IsAny<CancellationToken>()), Times.Once); 
        }


        [Fact]
        public async Task GetCandidateAsync_CandidateExists_ReturnsCandidate()
        {
            // Arrange
            int candidateId = 1;
            var existingCandidate = new JobCandidate { Id = candidateId, FirstName = "Sean", LastName = "Kick" };

            _jobCandidateRepoMock.Setup(repo => repo.ReadAsync(candidateId, It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(existingCandidate);

            var expectedResponse = new JobCandidateItemResponseDto { Id = "validEncryptedId", FirstName = "sagar", LastName = "gupta" };

            _mapperMock.Setup(mapper => mapper.Map<JobCandidateItemResponseDto>(It.IsAny<JobCandidate>()))
                       .Returns(expectedResponse); 

            // Act
            var result = await _jobCandidateAppServices.GetCandidateAsync(candidateId, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse); 
            _jobCandidateRepoMock.Verify(repo => repo.ReadAsync(candidateId, It.IsAny<CancellationToken>()), Times.Once); 
        }



        [Fact]
        public async Task DeleteCandidateAsync_CandidateExists_DeletesCandidate()
        {
            // Arrange
            string email = "user@yes.com";
            var existingCandidate = new JobCandidate { Id = 1, Email = email };

            _jobCandidateRepoMock.Setup(repo => repo.GetJobCandidateByEmail(email, It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(existingCandidate);

            _jobCandidateRepoMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                                 .Returns(Task.CompletedTask);

            // Act
            await _jobCandidateAppServices.DeleteCandidateAsync(email, CancellationToken.None);

            // Assert
            _jobCandidateRepoMock.Verify(repo => repo.DeleteAsync(existingCandidate.Id, It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task DeleteCandidateViaIdAsync_ValidId_DeletesCandidate()
        {
            //// Arrange
            //string encryptedId = "validEncryptedId";
            //int decryptedId = 1;

        }
    }

}