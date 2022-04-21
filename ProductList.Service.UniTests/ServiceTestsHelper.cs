using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Illustration.Service.UnitTests
{
    public static class ServiceTestsHelper
    {
        /// <summary>
        /// Gets an <see cref="ODataQueryOptions"/>
        /// </summary>
        /// <seealso cref="https://stackoverflow.com/a/49678474/117127"/>
        /// <seealso cref="https://github.com/OData/WebApi/issues/1645"/>
        public static ODataQueryOptions<T> GetODataQueryOptions<T>(String queryString = null) where T : class
        {
            var collection = new ServiceCollection();

            collection.AddOData();

            var provider = collection.BuildServiceProvider();

            var routeBuilder = new RouteBuilder(Mock.Of<IApplicationBuilder>(x => x.ApplicationServices == provider));
            routeBuilder.EnableDependencyInjection();

            var modelBuilder = new ODataConventionModelBuilder(provider);
            modelBuilder.EntitySet<T>("MyType");
            var model = modelBuilder.GetEdmModel();

            var httpContext = new DefaultHttpContext
            {
                RequestServices = provider
            };
            if (queryString != null)
            {
                httpContext.Request.QueryString = new QueryString(queryString);
            }

            var context = new ODataQueryContext(model, typeof(T), new Microsoft.AspNet.OData.Routing.ODataPath());
            return new ODataQueryOptions<T>(context, httpContext.Request);
        }

        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}
