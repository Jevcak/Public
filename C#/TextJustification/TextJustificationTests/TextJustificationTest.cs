namespace TextJustificationTests
{
    public class TextJustificationTest
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            string input = "tr rt rt";
            var reader = new StringReader(input);
            var writer = new StringWriter();
            var justifier = new Justifier(reader, writer, writer);
            //Act
            justifier.Justify(6);
            //Assert
            Assert.Equal("tr  rt\nrt", writer.ToString());
        }
    }
}