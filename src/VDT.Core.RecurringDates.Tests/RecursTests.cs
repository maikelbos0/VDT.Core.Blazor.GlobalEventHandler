using Xunit;

namespace VDT.Core.RecurringDates.Tests {    
    public class RecursTests {
        [Fact]
        public void Every() {
            var recurrence = Recurs.Every(2);

            Assert.Equal(2, recurrence.Interval);
        }
    }
}
