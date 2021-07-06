using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPortalApp.Model;
using NUnit.Framework;

namespace JobPortalApp.Domain.Tests
{
    [TestFixture]
    public class SkillsSpecificationTests
    {

        /// <summary>
        /// Givens the skills when filter candidates then returns true.
        /// </summary>
        [Test]
        public void Given_Skills_When_Filter_Candidates_Then_Returns_True()
        {
            //Arrange
            var candidate = new Candidate
            {
                Id = Guid.NewGuid().ToString(),
                Name = "candidate1",
                Skills = new List<Skills>
                {
                    new Skills {Name = "nodejs"}, new Skills {Name = "mongodb"}, new Skills {Name = "redis"},
                    new Skills {Name = "socketio"}
                }
            };
          var  searchSkills = new List<string> {  "mongodb" };
           var skillsSpecification = new SkillsSpecification(searchSkills);

            //Act
            var result = skillsSpecification.IsSatisfied(candidate);

            // Assert
            
            Assert.AreEqual(true, result);
        }



        [Test]
        public void Given_Skills_When_Filter_Candidates_Then_Returns_False()
        {
            //Arrange
            var candidate = new Candidate
            {
                Id = Guid.NewGuid().ToString(),
                Name = "candidate1",
                Skills = new List<Skills>
                {
                    new Skills {Name = "nodejs"}, new Skills {Name = "mongodb"}, new Skills {Name = "redis"},
                    new Skills {Name = "socketio"}
                }
            };
            var searchSkills = new List<string> { "Azure" };
            var skillsSpecification = new SkillsSpecification(searchSkills);

            //Act
            var result = skillsSpecification.IsSatisfied(candidate);

            // Assert

            Assert.AreEqual(false, result);
        }
    }
}