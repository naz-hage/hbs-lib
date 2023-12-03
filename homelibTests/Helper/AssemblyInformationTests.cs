using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace homelib.Helper.Tests
{
    [TestClass()]
    public class AssemblyInformationTests
    {
        [TestMethod()]
        public void GetTest()
        {
            // Add unit test content here

            // Arrange
            var expected = $" *** homelib, homelib, blue-man, 2020-{DateTime.Now.Year} - Version:";
            Console.WriteLine(expected);
            // Act
            var actual = AssemblyInformation.Get();
            Console.WriteLine(actual);
            // Assert
            Assert.IsTrue(actual.Contains(expected));
        }
    }
}