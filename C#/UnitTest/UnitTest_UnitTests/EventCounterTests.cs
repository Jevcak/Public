namespace UnitTest_UnitTests
{
    public class EventCounterTests
    {
        [Fact]
        public void OneEvent()
        {   
            //Arrange
            var counter = new EventCounter();
            //Act
            counter.EventOcurred();
            //Assert
            Assert.Equal(1, counter.Count);
            //SUT - System under test
        }
        [Fact]
        public void TwoEvents()
        {
            //Arrange
            var counter = new EventCounter();
            //Act
            counter.EventOcurred();
            counter.EventOcurred();
            //Assert
            Assert.Equal(2, counter.Count);
            //SUT - System under test
        }
        [Fact]
        public void NoEvent()
        {
            //Arrange
            var counter = new EventCounter();
            //Act
            //DO NOTHING
            //Assert
            Assert.Equal(0, counter.Count);
            //SUT - System under test
        }
    }
}