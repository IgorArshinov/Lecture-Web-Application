using FluentAssertions;
using LectureWebApplication.Core.Models;
using LectureWebApplication.Persistence;
using LectureWebApplication.Persistence.Repositories;
using LectureWebApplication.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace LectureWebApplication.Tests.Persistance.Repositories
{
    [TestClass]
    public class LectureRepositoryTests
    {
        private LectureRepository _lectureRepository;
        private Mock<DbSet<Lecture>> _mockLectures;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLectures = new Mock<DbSet<Lecture>>();

            var mockContext = new Mock<IApplicationDbContext>();

            _lectureRepository = new LectureRepository(mockContext.Object);

            mockContext.Setup(c => c.Lectures)
                .Returns(() => _mockLectures.Object);
        }

        [TestMethod]
        public void GetAllLecturesByArtistId_LectureIsNotInFuture_ShouldReturnNothing()
        {
            var lecture = new Lecture() { DateTime = DateTime.Now.AddDays(-1), UserId = "1" };

            var listWithLectures = new List<Lecture>() { lecture };

            _mockLectures.SetSource(listWithLectures);

            var lectures = _lectureRepository.GetAllLecturesByUserId("1");

            lectures.Should()
                .BeEmpty();

        }

        [TestMethod]
        public void GetAllLecturesByArtistId_LectureIsCanceled_LectureShouldNotBeReturned()
        {
            var lecture = new Lecture() { DateTime = DateTime.Now.AddDays(1), UserId = "1" };

            lecture.LectureIsCanceled();

            var listWithLectures = new List<Lecture>() { lecture };

            _mockLectures.SetSource(listWithLectures);

            var lectures = _lectureRepository.GetAllLecturesByUserId("1");

            lectures.Should()
                .BeEmpty();
        }

        [TestMethod]
        public void GetAllLecturesByArtistId_LectureGotDifferentArtistId_LectureShouldNotBeInList()
        {
            var lecture = new Lecture() { DateTime = DateTime.Now.AddDays(1), UserId = "1" };

            var listWithLectures = new List<Lecture>() { lecture };

            _mockLectures.SetSource(listWithLectures);

            var lectures = _lectureRepository.GetAllLecturesByUserId(lecture.UserId + "A");

            lectures.Should()
                .BeEmpty();
        }

        [TestMethod]
        public void GetAllLecturesByArtistId_LectureIsValid_LectureShouldBeInList()
        {
            var lecture = new Lecture() { DateTime = DateTime.Now.AddDays(1), UserId = "1" };

            var listWithLectures = new List<Lecture>() { lecture };

            _mockLectures.SetSource(listWithLectures);

            var lectures = _lectureRepository.GetAllLecturesByUserId(lecture.UserId);

            lectures.Should()
                .Contain(listWithLectures);
        }
    }
}