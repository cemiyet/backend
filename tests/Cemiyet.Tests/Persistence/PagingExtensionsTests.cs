using System.Collections.Generic;
using System.Linq;
using Cemiyet.Persistence.Extensions;
using Xunit;

namespace Cemiyet.Tests.Persistence
{
    // TODO (v0.1): add tests for PagedToListAsync()
    public class PagingExtensionsTests
    {
        private readonly List<int> _data;
        private readonly IQueryable<int> _dataQueryable;

        public PagingExtensionsTests()
        {
            _data = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            _dataQueryable = _data.AsQueryable();
        }

        [Fact]
        public void Page_Should_Work()
        {
            var rNotAsync = _dataQueryable.PagedToList();
            Assert.NotEmpty(rNotAsync);
            Assert.Equal(rNotAsync.Count, actual: _data.Count);

            var rEmptyPage = _dataQueryable.PagedToList(2);
            Assert.Empty(rEmptyPage);
        }

        [Fact]
        public void PageSize_Should_Work()
        {
            var pageSize = _data.Count / 2;
            var rFirstPage = _dataQueryable.PagedToList(1, pageSize);
            Assert.NotEmpty(rFirstPage);
            Assert.Equal(pageSize, actual: rFirstPage.Count);
            Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, rFirstPage);

            pageSize = _data.Count / 3;
            var rSecondPage = _dataQueryable.PagedToList(2, pageSize);
            Assert.NotEmpty(rSecondPage);
            Assert.Equal(pageSize, actual: rSecondPage.Count);
            Assert.Equal(new List<int> { 4, 5, 6 }, rSecondPage);
        }
    }
}
