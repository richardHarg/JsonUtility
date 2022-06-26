namespace RLH.JsonUtility.Tests
{
    public class ParsingTests
    {
        [Fact]
        public void DefaultName_DefaultPath()
        {
            using (IJsonFileParser parser = new JsonFileParser())
            {
                TestClass parsedClass = parser.Parse<TestClass>();

                Assert.NotNull(parsedClass);
                Assert.Equal("TestClass", parsedClass.NameOfTestFile);
            }
        }
        [Fact]
        public void DefaultName_RelativePathInFIleName()
        {
            using (IJsonFileParser parser = new JsonFileParser())
            {
                TestClass parsedClass = parser.Parse<TestClass>("data\\testclass.json");

                Assert.NotNull(parsedClass);
                Assert.Equal("data\\TestClass", parsedClass.NameOfTestFile);
            }
        }
        [Fact]
        public void DefaultName_FullPathInFileName()
        {
            using (IJsonFileParser parser = new JsonFileParser())
            {
                TestClass parsedClass = parser.Parse<TestClass>(@"C:\Users\R\source\repos\JsonUtility\tests\RLH.JsonUtility.Tests\data\testclass.json");

                Assert.NotNull(parsedClass);
                Assert.Equal("data\\TestClass", parsedClass.NameOfTestFile);
            }
        }
        [Fact]
        public void DefaultName_RelativePath()
        {
            using (IJsonFileParser parser = new JsonFileParser("data"))
            {
                TestClass parsedClass = parser.Parse<TestClass>();

                Assert.NotNull(parsedClass);
                Assert.Equal("data\\TestClass", parsedClass.NameOfTestFile);
            }
        }
        [Fact]
        public void DefaultName_FullPath()
        {
            using (IJsonFileParser parser = new JsonFileParser(@"C:\Users\R\source\repos\JsonUtility\tests\RLH.JsonUtility.Tests\Data"))
            {
                TestClass parsedClass = parser.Parse<TestClass>();

                Assert.NotNull(parsedClass);
                Assert.Equal("data\\TestClass", parsedClass.NameOfTestFile);
            }
        }

        [Fact]
        public void CustomName_RelativePath()
        {
            using (IJsonFileParser parser = new JsonFileParser("data"))
            {
                TestClass parsedClass = parser.Parse<TestClass>("CustomNameSingle");

                Assert.NotNull(parsedClass);
                Assert.Equal("data\\CustomNameSingle", parsedClass.NameOfTestFile);
            }
        }


        [Theory]
        [InlineData("data","testclass")]
        [InlineData("data\\","\\testclass")]
        [InlineData("data\\", "\\testclass.json")]
        [InlineData("\\data", "testclass.json")]
        [InlineData("data\\", "testclass.json")]
        public void Path_Name_Variations_Work(string path,string fileName)
        {
            using (IJsonFileParser parser = new JsonFileParser(path))
            {
                TestClass parsedClass = parser.Parse<TestClass>(fileName);

                Assert.NotNull(parsedClass);
                Assert.Equal("data\\TestClass", parsedClass.NameOfTestFile);
            }


        }
    }
}