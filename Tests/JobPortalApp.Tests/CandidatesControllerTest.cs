using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JobPortalApp.Data.Context;
using JobPortalApp.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using JobPortalApp.API.Controllers;
using JobPortalApp.API.Model;
using JobPortalApp.Domain.Contracts;
using JobPortalApp.Model;
using System.Web.Http.Results;
using NotFoundResult = Microsoft.AspNetCore.Mvc.NotFoundResult;

namespace JobPortalApp.Tests
{
    [TestFixture]
    public class CandidatesControllerTest
    {
        private DbContextOptions<CandidateContext> options;
        private CandidateContext candidateContext;
        private IFilter<Candidate> _filterCandidate;
        public CandidatesControllerTest()
        {
            //NOT IMPLEMENTING MOCK BECAUSE OF USING IN MEMORY DB
            options = new DbContextOptionsBuilder<CandidateContext>()
                .UseInMemoryDatabase(databaseName: "JobPortalDatabase")
                .Options;
            
            candidateContext = new CandidateContext(options);
            _filterCandidate = new CandidateFilter();
        }


        /// <summary>
        /// Givens the candidate request save to in memory database return success.
        /// </summary>
        [Test]
        public async Task Given_CandidateRequest_SaveToInMemoryDB_ReturnSuccess()
        {
            //Arrange
            var candidateRequest = new CandidateRequest()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Amy Fish",
                Skills = new List<string> {"scala", "go"}
            };
            var candidatesController = new CandidatesController(_filterCandidate, new CandidateService(candidateContext));
            
            //Act
           var actionResult = await candidatesController.Add(candidateRequest);
           var createdResult = actionResult.Result as CreatedResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.Created, (HttpStatusCode)createdResult.StatusCode);
        }

        /// <summary>
        /// Givens the skills with no candidates added return 404.
        /// </summary>
        [Test]
        public async Task Given_Skills_With_no_candidates_added_Return_404()
        {
            //Arrange
            var candidatesController =
                new CandidatesController(_filterCandidate, new CandidateService(candidateContext));

            //Act
            var actionResult = await candidatesController.Search("javascript,react,typescript");
            var notFoundObjectResult = actionResult.Result as NotFoundResult;

            // Assert
            Assert.AreEqual(404, notFoundObjectResult.StatusCode);
        }

        /// <summary>
        /// Givens the scala and go skills return amy fish candidate.
        /// </summary>
        [Test]
        public async Task Given_scala_and_go_Skills_return_Amy_fish_Candidate()
        {
            //Arrange
            AddDefaultItems(MockCandidatesData());

            AddDefaultItems(new List<Candidate>
            {
                new Candidate
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Amy Fish",
                    Skills = new List<Skills> { new Skills{ Name = "scala" }, new Skills { Name = "go" } }
                }
            });

            var candidatesController =
                new CandidatesController(_filterCandidate, new CandidateService(candidateContext));

            //Act
            var actionResult = await candidatesController.Search("scala,go");
            var objectResult = actionResult.Result as OkObjectResult;

            // Assert
            var candidate = objectResult.Value as CandidateRequest;
            Assert.IsInstanceOf<ActionResult<CandidateRequest>>(actionResult);
            Assert.AreEqual("Amy Fish", candidate.Name);
        }

        /// <summary>
        /// Givens the express mongodb redis skills get candidate1 user.
        /// </summary>
        [Test]
        public async Task Given_express_mongodb_redis_Skills_Get_Candidate1_user()
        {
            //Arrange
            AddDefaultItems(MockCandidatesData());
            var candidatesController =
                new CandidatesController(_filterCandidate, new CandidateService(candidateContext));

            //Act
            var actionResult = await candidatesController.Search("express,mongodb,redis");
            var objectResult = actionResult.Result as OkObjectResult;

            // Assert
            var candidate = objectResult.Value as CandidateRequest;
            Assert.IsInstanceOf<ActionResult<CandidateRequest>>(actionResult);
            Assert.AreEqual("candidate1", candidate.Name);
        }

        /// <summary>
        /// Givens the express skill get candidate2 user.
        /// </summary>
        [Test]
        public async Task Given_express_Skill_Get_Candidate2_user()
        {
            //Arrange
            AddDefaultItems(MockCandidatesData());
            var candidatesController =
                new CandidatesController(_filterCandidate, new CandidateService(candidateContext));

            //Act
            var actionResult = await candidatesController.Search("express");
            var objectResult = actionResult.Result as OkObjectResult;

            // Assert
            var candidate = objectResult.Value as CandidateRequest;
            Assert.IsInstanceOf<ActionResult<CandidateRequest>>(actionResult);
            Assert.AreEqual("candidate2", candidate.Name);
        }


        /// <summary>
        /// Givens the invalid candidate name returns error message.
        /// </summary>
        [Test]
        public void Given_Invalid_Candidate_Name_ReturnsErrorMessage()
        {
            //Arrange
            var candidateRequest = new CandidateRequest()
            {
                Id = Guid.NewGuid().ToString(),
                Skills = new List<string> { "scala", "go" }
            };

            //Act
            var results = TestModelHelper.Validate(candidateRequest);

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Name field is required.", results[0].ErrorMessage);
        }

        /// <summary>
        /// Givens the duplicate skills returns error message.
        /// </summary>
        [Test]
        public void Given_Duplicate_Skills_ReturnsErrorMessage()
        {
            //Arrange
            var candidateRequest = new CandidateRequest()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test1",
                Skills = new List<string> { "scala", "scala" }
            };

            //Act
            var results = TestModelHelper.Validate(candidateRequest);

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Duplicate items found in skills", results[0].ErrorMessage);
        }

        /// <summary>
        /// Givens the duplicate skills returns error message.
        /// </summary>
        [Test]
        public void Given_Invalid_Skills_ReturnsErrorMessage()
        {
            //Arrange
            var candidateRequest = new CandidateRequest()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test1",
                Skills = new List<string> { ".net", "-scala" }
            };

            //Act
            var results = TestModelHelper.Validate(candidateRequest);

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Invalid Skills Data", results[0].ErrorMessage);
        }


        /// <summary>
        /// Adds the default items.
        /// </summary>
        private void AddDefaultItems(List<Candidate> candidates)
        {
            using (var context = new CandidateContext(options))
            {
                context.Candidates.AddRange(candidates);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Mocks the candidates data.
        /// </summary>
        /// <returns></returns>
        private List<Candidate> MockCandidatesData()
        {
            return new List<Candidate>
            {
                new Candidate
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "candidate1",
                    Skills = new List<Skills> { new Skills{ Name = "nodejs"}, new Skills { Name = "mongodb" }, new Skills { Name = "redis" }, new Skills { Name = "socketio" } }
                },
                new Candidate
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "candidate2",
                    Skills = new List<Skills> { new Skills{ Name = "nodejs" }, new Skills { Name = "express" }  }
                },
                new Candidate
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User 3",
                    Skills = new List<Skills> { new Skills{ Name = "Microsoft Net"}, new Skills { Name = "Azure" } , new Skills { Name = "SQL" } }
                },
            };
        }
    }
}