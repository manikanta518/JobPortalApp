using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JobPortalApp.API.Controllers;
using JobPortalApp.API.Model;
using JobPortalApp.Domain.Contracts;
using JobPortalApp.Model;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace JobPortalApp.Domain.Tests
{
    [TestFixture]
    public class CandidateFilterTests
    {
        private readonly IFilter<Candidate> _filterCandidate;

        public CandidateFilterTests()
        {
            _filterCandidate = new CandidateFilter();
        }


        [Test]
        public async Task Given_Skills_When_Filter_Candidates_Then_Returns_lists()
        {
            //Arrange
            var skills = "mongodb,express";
            var data = MockCandidatesData();

            //Act
            var candidates = await _filterCandidate.Filter(data,
                new SkillsSpecification(skills.Split(',').ToList()));

            // Assert
            Assert.IsNotNull(candidates);
            Assert.AreEqual(2, candidates.Count);
        }

        [Test]
        public async Task Given_Empty_candidates_When_Filter_Candidates_Then_Returns_Null()
        {
            //Arrange
            var skills = "mongodb,express";

            //Act
            var candidates = await _filterCandidate.Filter(null,
                new SkillsSpecification(skills.Split(',').ToList()));

            // Assert
            Assert.IsNull(candidates);
           
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