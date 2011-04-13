using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace QuantumCode.Common.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            Sections s = new Sections();

            s.Add(new Section("test1"));
            s.Add(new Section("test2"));
            s.Add(new Section("test3"));

            foreach (Section ss in s)
            {
                Console.WriteLine(ss.Name);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            string path = Path.Combine(TestContext.TestDir, @"..\..\..\..\test\QuantumCode.Common.Test");
            INIFile testFile = INIFile.ReadINI(Path.Combine(path, "test.ini"));

            Assert.AreEqual(2, testFile.Sections.Count);
            Assert.AreEqual(4, testFile[0].KeyValues.Count);
            Assert.AreEqual(2, testFile[1].KeyValues.Count);

            Assert.AreEqual("Default", testFile.Sections[0]);
            Assert.AreEqual("Default2", testFile.Sections[1]);

            Assert.AreEqual("Default", testFile[0].Value("MainForm"));
            Assert.AreEqual("Test1", testFile[0].Value("Test1"));
            Assert.AreEqual("Test2", testFile[0].Value("Test2"));
            Assert.AreEqual("Test3", testFile[0]["Test3"]);

            Assert.AreEqual("Test4", testFile["Default2"].Value("Test4"));
            Assert.AreEqual("Test5", testFile["Default2"]["Test5"]);
        }
    }
}
