using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace homelibTests
{
    [TestClass]
    public class TestChecker
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
            
            Prerequisites();
        }

        public static void Prerequisites()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var testClasses = assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(TestClassAttribute), true).Length > 0)
                .ToArray();

            // display the test classes and methods of each test class
            foreach (var testClass in testClasses)
            {
                Console.WriteLine($"-Test class: {testClass.Name}");
                var testMethods = testClass.GetMethods()
                    .Where(m => m.GetCustomAttributes(typeof(TestMethodAttribute), true).Length > 0)
                    .ToArray();
                foreach (var testMethod in testMethods)
                {
                    Console.WriteLine($"  -- method: {testMethod.Name}");
                }
            }
        }
    }
}
