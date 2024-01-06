namespace ExcelTesting
{
    public class UnitTest1
    {
        [Fact]
        public void TestCoordinatesEasy()
        {
            //Arrange
            Table table = new Table();
            var c = new Coordinates(5, 4);
            var cell = new EquationCell("AAB1590", "A32", '+', table);
            //Act
            Coordinates res = cell.GetCoordinates(cell.left);
            //Assert
            Assert.Equal((26*26)+(26)+2-1, res.col);
            Assert.Equal(1590-1, res.row);
        }
        [Fact]
        public void TestCoordinatesHarder()
        {
            //Arrange
            Table table = new Table();
            var c = new Coordinates(5, 4);
            var cell = new EquationCell("GBB123", "A32", '+', table);
            //Act
            Coordinates res = cell.GetCoordinates(cell.left);
            //Assert
            Assert.Equal(26*26*7 + 26*2 + 2 - 1, res.col);
            Assert.Equal(123 - 1, res.row);
        }
        [Fact]
        public void TestGetCellEmpty()
        {
            //Arrange
            Table table = new Table();
            table.AddRow();
            //Act
            Cell cell = Cell.GetCell("[]", table);
            int red = -1;
            bool test = cell.GetValue(ref red);
            //Assert
            Assert.True(test);
            Assert.Equal(0, red);
            Assert.Equal("[]", cell.ToString());
        }
        [Fact]
        public void TestGetCellValue()
        {
            //Arrange
            Table table = new Table();
            table.AddRow();
            //Act
            Cell cell = Cell.GetCell("757", table);
            int red = -1;
            bool test = cell.GetValue(ref red);
            //Assert
            Assert.True(test);
            Assert.Equal(757, red);
            Assert.Equal("757", cell.ToString());
        }
        [Fact]
        public void TestGetCellWithoutEqualSign()
        {
            //Arrange
            Table table = new Table();
            table.AddRow();
            //Act
            Cell cell = Cell.GetCell("AA1+BB1", table);
            int red = -1;
            bool test = cell.GetValue(ref red);
            //Assert
            Assert.False(test);
            Assert.Equal(-1, red);
            Assert.Equal("#INVVAL", cell.ToString());
        }
        [Fact]
        public void TestGetCellCorrect()
        {
            //Arrange
            Table table = new Table();
            table.AddRow();
            //Act
            Cell cell = Cell.GetCell("=AA12+BB21", table);
            //Assert
            Assert.Equal("AA12+BB21", cell.ToString());
        }
        [Fact]
        public void TestGetCellMissOp()
        {
            //Arrange
            Table table = new Table();
            table.AddRow();
            //Act
            Cell cell = Cell.GetCell("=AA12BB21", table);
            //Assert
            Assert.Equal("#MISSOP", cell.ToString());
        }
        [Fact]
        public void TestGetCellTooMuchOperands()
        {
            //Arrange
            Table table = new Table();
            table.AddRow();
            //Act
            Cell cell = Cell.GetCell("=AA12+BB21+CC4", table);
            //Assert
            Assert.Equal("#FORMULA", cell.ToString());
        }
        [Fact]
        public void TestGetCellFormula()
        {
            //Arrange
            Table table = new Table();
            table.AddRow();
            //Act
            Cell cell = Cell.GetCell("=AA12@+B21", table);
            //Assert
            Assert.Equal("#FORMULA", cell.ToString());
        }
        [Fact]
        public void TestTableRecodex()
        {
            //Arrange
            string inputFile = "C:\\Users\\START\\Desktop\\MFF\\Zimní semestr - 2.roèník\\C#\\Excel-data\\InputRecodex.txt";
            string expectedOutput = "C:\\Users\\START\\Desktop\\MFF\\Zimní semestr - 2.roèník\\C#\\Excel-data\\OutputRecodex.txt";
            StreamReader expOutReader = new StreamReader(expectedOutput);
            StreamReader reader = new StreamReader(inputFile);
            var writer = new StringWriter();
            //Act
            var ProgramExecutor = new ProgramExecutor(new StreamReader(inputFile),writer);
            ProgramExecutor.Run();
            //Assert
            Assert.Equal(expOutReader.ReadToEnd(), writer.ToString());
        }
        [Fact]
        public void TestTableNotACycle()
        {
            //Arrange
            string inputFile = "C:\\Users\\START\\Desktop\\MFF\\Zimní semestr - 2.roèník\\C#\\Excel-data\\InputNotACycle.txt";
            string expectedOutput = "C:\\Users\\START\\Desktop\\MFF\\Zimní semestr - 2.roèník\\C#\\Excel-data\\OutputNotACycle.txt";
            StreamReader expOutReader = new StreamReader(expectedOutput);
            StreamReader reader = new StreamReader(inputFile);
            var writer = new StringWriter();
            //Act
            var ProgramExecutor = new ProgramExecutor(new StreamReader(inputFile), writer);
            ProgramExecutor.Run();
            //Assert
            Assert.Equal(expOutReader.ReadToEnd(), writer.ToString());
        }
        [Fact]
        public void TestTableFormulaError()
        {
            //Arrange
            string inputFile = "C:\\Users\\START\\Desktop\\MFF\\Zimní semestr - 2.roèník\\C#\\Excel-data\\InputFormulaError.txt";
            string expectedOutput = "C:\\Users\\START\\Desktop\\MFF\\Zimní semestr - 2.roèník\\C#\\Excel-data\\OutputFormulaError.txt";
            StreamReader expOutReader = new StreamReader(expectedOutput);
            StreamReader reader = new StreamReader(inputFile);
            var writer = new StringWriter();
            //Act
            var ProgramExecutor = new ProgramExecutor(new StreamReader(inputFile), writer);
            ProgramExecutor.Run();
            //Assert
            Assert.Equal(expOutReader.ReadToEnd(), writer.ToString());
        }
        [Fact]
        public void TestTableCycleError()
        {
            //Arrange
            string inputFile = "C:\\Users\\START\\Desktop\\MFF\\Zimní semestr - 2.roèník\\C#\\Excel-data\\InputCycleError.txt";
            string expectedOutput = "C:\\Users\\START\\Desktop\\MFF\\Zimní semestr - 2.roèník\\C#\\Excel-data\\OutputCycleError.txt";
            StreamReader expOutReader = new StreamReader(expectedOutput);
            StreamReader reader = new StreamReader(inputFile);
            var writer = new StringWriter();
            //Act
            var ProgramExecutor = new ProgramExecutor(new StreamReader(inputFile), writer);
            ProgramExecutor.Run();
            //Assert
            Assert.Equal(expOutReader.ReadToEnd(), writer.ToString());
        }
    }
}