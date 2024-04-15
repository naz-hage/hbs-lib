using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace homelibTests
{
    [TestClass]
    public class TestInheritanceChecker
    {
        /// <summary>
        /// Initializes the assembly before any tests are run.
        /// </summary>
        /// <param name="context">The test context.</param>
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // Initialization code here
            Console.WriteLine($"AssemblyInitialize: {context}");
            // Set up the test environment HBS_HOMELIB_DATABASE_TYPE =IN_MEMORY
            Environment.SetEnvironmentVariable("HBS_HOMELIB_DATABASE_TYPE", "IN_MEMORY");
            //Assert.Fail("AssemblyInitialize failed");
        }

        public static void AllTestClassesShouldInheritFromSandboxTest()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var testClasses = assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(TestClassAttribute), true).Length > 0)
                .ToArray();

        }
    }
}
