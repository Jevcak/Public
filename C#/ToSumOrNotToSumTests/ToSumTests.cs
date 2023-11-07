/*
 namespace ToSumOrNotToSumTests
{
    public class ToSumTests
    {
        [Fact]
        public void Nothing()
        {
            //Arrange
            string input = "";
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("Invalid File Format\r\n", writer.ToString());

        }
        [Fact]
        public void Normal()
        {
            //Arrange
            string input = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     Celestyn    15478       10      154780
                leden   jablka      dovoz       Adamec      1321        30      39630
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("cena\r\n----\r\n52\r\n", writer.ToString());

        }
        [Fact]
        public void InvalidInteger()
        {
            //Arrange
            string input = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     Celestyn    15478       10      154780
                leden   jablka      dovoz       Adamec      1321        necislo      39630
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("Invalid Integer Value\r\n", writer.ToString());

        }
        [Fact]
        public void CenaInvalidFormat()
        {
            //Arrange
            string input = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     Celestyn    15478       10      154780 54
                leden   jablka      dovoz       Adamec      1321        necislo      39630
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("Invalid File Format\r\n", writer.ToString());

        }
        [Fact]
        public void InvalidColumn()
        {
            //Arrange
            string input = """
                mesic   zbozi       typ         prodejce    mnozstvi    cen    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     Celestyn    15478       10      154780 54
                leden   jablka      dovoz       Adamec      1321        necislo      39630
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("Non-existent Column Name\r\n", writer.ToString());

        }
        [Fact]
        public void EmptyRow()
        {
            //Arrange
            string input = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                leden   brambory    tuzemske    Bartak      10895       12      130740

                leden   brambory    vlastni     Celestyn    15478       10      154780
                leden   jablka      dovoz       Adamec      1321        necislo      39630
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("Invalid File Format\r\n", writer.ToString());

        }
        [Fact]
        public void SameColumnName()
        {
            //Arrange
            string input = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    cena
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     Celestyn    15478       10      154780
                leden   jablka      dovoz       Adamec      1321        30      39630
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("cena\r\n----\r\n52\r\n", writer.ToString());

        }
        [Fact]
        public void OnlyHeader()
        {
            //Arrange
            string input = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    cena
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("cena\r\n----\r\n0\r\n", writer.ToString());
        }
        [Fact]
        public void HeaderAndEmptyRow()
        {
            //Arrange
            string input = """

                mesic   zbozi       typ         prodejce    mnozstvi    cena    cena
                    
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("Invalid File Format\r\n", writer.ToString());
        }
        [Fact]
        public void BigInteger()
        {
            //Arrange
            string input = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    cena
                 t      t           t               t           t       2147483647 t
                 t      t           t               t           t       2147483647 t
                 t      t           t               t           t       2147483647 t
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("cena\r\n----\r\n6442450941\r\n", writer.ToString());
        }
        [Fact]
        public void BigNegativeInteger()
        {
            //Arrange
            string input = """
                mesic   zbozi       typ         prodejce    mnozstvi    cena    cena
                 t      t           t               t           t       -2147483647 t
                 t      t           t               t           t       -2147483647 t
                 t      t           t               t           t       -2147483647 t
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("cena");

            //Assert
            Assert.Equal("cena\r\n----\r\n-6442450941\r\n", writer.ToString());
        }
        [Fact]
        public void DifferentDelimiterLength()
        {
            //Arrange
            string input = """
                mesic   zbozi       typ         prodejce    mnozstvi    Novyscitaciradek    cena
                leden   brambory    tuzemske    Bartak      10895       12      130740
                leden   brambory    vlastni     Celestyn    15478       10      154780
                leden   jablka      dovoz       Adamec      1321        30      39630
                """;
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var summer = new Summer(reader, writer, writer);
            //Act
            summer.Sum("Novyscitaciradek");

            //Assert
            Assert.Equal("Novyscitaciradek\r\n----------------\r\n52\r\n", writer.ToString());

        }
    }
}
*/