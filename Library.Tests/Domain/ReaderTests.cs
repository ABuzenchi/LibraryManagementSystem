using Library.Domain;
using Xunit;

namespace Library.Tests.Domain
{
    public class ReaderTests
    {
        [Fact]
        public void Reader_CanBeStaff()
        {
            var reader = new Reader { Id = 1, Name = "Ana", IsStaff = true };
            Assert.True(reader.IsStaff);
        }

        [Fact]
        public void Reader_DefaultIsNotStaff()
        {
            var reader = new Reader { Id = 2, Name = "Ion" };
            Assert.False(reader.IsStaff);
        }
    }
}
