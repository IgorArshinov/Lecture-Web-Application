using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace LectureWebApplication.Tests.Extensions
{
    public static class MockDbSetExtensions
    {
        public static void SetSource<T>(this Mock<DbSet<T>> mockSet, IList<T> source) where T : class
        {
            var data = source.AsQueryable();

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);
            mockSet.As<IQueryable<T>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);
            mockSet.As<IQueryable<T>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            //            mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => source.Add(s));

            //            var data = source.AsQueryable();
            //            var dbSet = new Mock<DbSet<T>>();
            //            dbSet.As<IQueryable<T>>()
            //                .Setup(m => m.Provider)
            //                .Returns(data.Provider);
            //            dbSet.As<IQueryable<T>>()
            //                .Setup(m => m.Expression)
            //                .Returns(data.Expression);
            //            dbSet.As<IQueryable<T>>()
            //                .Setup(m => m.ElementType)
            //                .Returns(data.ElementType);
            //            dbSet.As<IQueryable<T>>()
            //                .Setup(m => m.GetEnumerator())
            //                .Returns(() => data.GetEnumerator());
            //            dbSet.Setup(d => d.Add(It.IsAny<T>()))
            //                .Callback<T>(source.Add);
            //            return dbSet.Object;
        }
    }
}