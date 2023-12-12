namespace NezarkaBookstoreTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            string text = """
                DATA-BEGIN
                BOOK;4748;_BookTitle_;_BookAuthor_;785
                CUSTOMER;789;_CustName_;_CustSurname_
                CART-ITEM;789;789;5
                DATA-END
                """;
            var W = new StringWriter();
            var R = new StringReader(text);
            //Act
            var c = ModelStore.LoadFrom(R, W);
            //Assert
            Assert.Equal("", W.ToString());

        }
    }
}