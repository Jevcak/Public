namespace ExpEvalTesty
{
    public class UnitTest1
    {
        [Fact]
        public void EasyTestRecodex1()
        {
            //Arrange
            var writer = new StringWriter();
            string input = "+ ~ 1 3";
            var reader = new StringReader(input);
            //Act
            var errHandler = new ErrorHandler(writer);
            var program = new ProgramExecutor(reader, writer);
            errHandler.HandleErrors(program);
            //Assert
            Assert.Equal("2", writer.ToString());
        }
        [Fact]
        public void EasyTestRecodex2()
        {
            //Arrange
            var writer = new StringWriter();
            string input = "/ + - 5 2 * 2 + 3 3 ~ 2";
            var reader = new StringReader(input);
            //Act
            var errHandler = new ErrorHandler(writer);
            var program = new ProgramExecutor(reader, writer);
            errHandler.HandleErrors(program);
            //Assert
            Assert.Equal("-7", writer.ToString());
        }
        [Fact]
        public void EasyTestRecodex3()
        {
            //Arrange
            var writer = new StringWriter();
            string input = "- - 2000000000 2100000000 2100000000";
            var reader = new StringReader(input);
            //Act
            var errHandler = new ErrorHandler(writer);
            var program = new ProgramExecutor(reader, writer);
            errHandler.HandleErrors(program);
            //Assert
            Assert.Equal("Overflow Error", writer.ToString());
        }
        [Fact]
        public void EasyTestRecodex4()
        {
            //Arrange
            var writer = new StringWriter();
            string input = "/ 100 - + 10 10 20";
            var reader = new StringReader(input);
            //Act
            var errHandler = new ErrorHandler(writer);
            var program = new ProgramExecutor(reader, writer);
            errHandler.HandleErrors(program);
            //Assert
            Assert.Equal("Divide Error", writer.ToString());
        }
        [Fact]
        public void EasyTestRecodex5()
        {
            //Arrange
            var writer = new StringWriter();
            string input = "+ 1 2 3";
            var reader = new StringReader(input);
            //Act
            var errHandler = new ErrorHandler(writer);
            var program = new ProgramExecutor(reader, writer);
            errHandler.HandleErrors(program);
            //Assert
            Assert.Equal("Format Error", writer.ToString());
        }
        [Fact]
        public void EasyTestRecodex6()
        {
            //Arrange
            var writer = new StringWriter();
            string input = "- 2000000000 4000000000";
            var reader = new StringReader(input);
            //Act
            var errHandler = new ErrorHandler(writer);
            var program = new ProgramExecutor(reader, writer);
            errHandler.HandleErrors(program);
            //Assert
            Assert.Equal("Format Error", writer.ToString());
        }
    }
}