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

        [Fact]
        public void Reader_Name_CanBeChanged()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            reader.Name = "Ion";

            Assert.Equal("Ion", reader.Name);
        }

        [Fact]
        public void Reader_Phone_CanBeChanged()
        {
            var reader = new Reader { Id = 1, Name = "Ana", Phone = "0711111111" };
            reader.Phone = "0722222222";

            Assert.Equal("0722222222", reader.Phone);
        }

        [Fact]
        public void Reader_Email_CanBeChanged()
        {
            var reader = new Reader { Id = 1, Name = "Ana", Email = "a@test.com" };
            reader.Email = "b@test.com";

            Assert.Equal("b@test.com", reader.Email);
        }

        [Fact]
        public void Reader_Name_CanBeEmptyString()
        {
            var reader = new Reader { Id = 1, Name = "" };
            Assert.Equal(string.Empty, reader.Name);
        }

        [Fact]
        public void Reader_Name_CanBeWhitespace()
        {
            var reader = new Reader { Id = 1, Name = "   " };
            Assert.Equal("   ", reader.Name);
        }

        [Fact]
        public void Reader_Phone_CanBeEmptyString()
        {
            var reader = new Reader { Id = 1, Name = "Ana", Phone = "" };
            Assert.Equal(string.Empty, reader.Phone);
        }

        [Fact]
        public void Reader_Email_CanBeEmptyString()
        {
            var reader = new Reader { Id = 1, Name = "Ana", Email = "" };
            Assert.Equal(string.Empty, reader.Email);
        }

        [Fact]
        public void Reader_CanBeCreated_WithoutPhoneAndEmail()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };

            Assert.Null(reader.Phone);
            Assert.Null(reader.Email);
        }

        [Fact]
        public void Reader_IsStaff_DefaultsToFalse_WhenExplicitlySetFalse()
        {
            var reader = new Reader { Id = 1, Name = "Ana", IsStaff = false };
            Assert.False(reader.IsStaff);
        }

        [Fact]
        public void Reader_IsStaff_CanToggle_FromTrueToFalse()
        {
            var reader = new Reader { Id = 1, Name = "Ana", IsStaff = true };
            reader.IsStaff = false;

            Assert.False(reader.IsStaff);
        }

        [Fact]
        public void Reader_Id_CanBeChanged()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            reader.Id = 10;

            Assert.Equal(10, reader.Id);
        }

        [Fact]
        public void Two_Readers_CanHave_Same_Name()
        {
            var r1 = new Reader { Id = 1, Name = "Ana" };
            var r2 = new Reader { Id = 2, Name = "Ana" };

            Assert.Equal(r1.Name, r2.Name);
        }

        [Fact]
        public void Two_Readers_CanHave_Same_Email()
        {
            var r1 = new Reader { Id = 1, Name = "Ana", Email = "a@test.com" };
            var r2 = new Reader { Id = 2, Name = "Ion", Email = "a@test.com" };

            Assert.Equal(r1.Email, r2.Email);
        }

        [Fact]
        public void Reader_All_Properties_CanBeSet()
        {
            var reader = new Reader
            {
                Id = 5,
                Name = "Ana",
                Phone = "0700000000",
                Email = "ana@test.com",
                IsStaff = true
            };

            Assert.True(reader.IsStaff);
            Assert.Equal("Ana", reader.Name);
            Assert.Equal("0700000000", reader.Phone);
            Assert.Equal("ana@test.com", reader.Email);
            Assert.Equal(5, reader.Id);
        }

    }
}
