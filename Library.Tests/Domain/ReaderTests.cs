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

        [Fact]
        public void Reader_Name_IsSetCorrectly()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            Assert.Equal("Ana", reader.Name);
        }

        [Fact]
        public void Reader_Phone_CanBeSet()
        {
            var reader = new Reader
            {
                Id = 1,
                Name = "Ana",
                Phone = "0712345678"
            };

            Assert.Equal("0712345678", reader.Phone);
        }

        [Fact]
        public void Reader_Email_CanBeSet()
        {
            var reader = new Reader
            {
                Id = 1,
                Name = "Ana",
                Email = "ana@test.com"
            };

            Assert.Equal("ana@test.com", reader.Email);
        }

        [Fact]
        public void Reader_Id_Defaults_To_Zero()
        {
            var reader = new Reader { Name = "Ana" };
            Assert.Equal(0, reader.Id);
        }

        [Fact]
        public void Reader_IsStaff_CanBeChanged()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            reader.IsStaff = true;

            Assert.True(reader.IsStaff);
        }

        [Fact]
        public void Reader_Phone_Defaults_To_Null()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            Assert.Null(reader.Phone);
        }

        [Fact]
        public void Reader_Email_Defaults_To_Null()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            Assert.Null(reader.Email);
        }
    }
}
