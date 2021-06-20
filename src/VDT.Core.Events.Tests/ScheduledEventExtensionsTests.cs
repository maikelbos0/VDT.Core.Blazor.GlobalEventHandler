using NSubstitute;
using System;
using Xunit;

namespace VDT.Core.Events.Tests {
    public sealed class ScheduledEventExtensionsTests {
        [Fact]
        public void GetTimeToNextDispatch_Succeeds_For_PreviousDispatch_Utc_IncludingSeconds() {
            var scheduledEvent = Substitute.For<IScheduledEvent>();
            scheduledEvent.CronExpression.Returns("0/15 * * * * *");
            scheduledEvent.CronExpressionIncludesSeconds.Returns(true);
            scheduledEvent.PreviousDispatch.Returns(new DateTime(2000, 1, 1, 4, 3, 0, 0, DateTimeKind.Utc));

            Assert.Equal(TimeSpan.FromMilliseconds(11443), scheduledEvent.GetTimeToNextDispatch(new DateTime(2000, 1, 1, 4, 3, 3, 557, DateTimeKind.Utc)));
        }

        [Fact]
        public void GetTimeToNextDispatch_Succeeds_For_PreviousDispatch_Utc_ExcludingSeconds() {
            var scheduledEvent = Substitute.For<IScheduledEvent>();
            scheduledEvent.CronExpression.Returns("0/2 * * * *");
            scheduledEvent.CronExpressionIncludesSeconds.Returns(false);
            scheduledEvent.PreviousDispatch.Returns(new DateTime(2000, 1, 1, 4, 3, 0, 0, DateTimeKind.Utc));

            Assert.Equal(TimeSpan.FromMilliseconds(56443), scheduledEvent.GetTimeToNextDispatch(new DateTime(2000, 1, 1, 4, 3, 3, 557, DateTimeKind.Utc)));
        }

        [Fact]
        public void GetTimeToNextDispatch_Succeeds_For_PreviousDispatch_Local_IncludingSeconds() {
            var scheduledEvent = Substitute.For<IScheduledEvent>();
            scheduledEvent.CronExpression.Returns("0/15 * * * * *");
            scheduledEvent.CronExpressionIncludesSeconds.Returns(true);
            scheduledEvent.PreviousDispatch.Returns(new DateTime(2000, 1, 1, 4, 3, 0, 0, DateTimeKind.Local) + TimeZoneInfo.Local.BaseUtcOffset);

            Assert.Equal(TimeSpan.FromMilliseconds(11443), scheduledEvent.GetTimeToNextDispatch(new DateTime(2000, 1, 1, 4, 3, 3, 557, DateTimeKind.Utc)));
        }

        [Fact]
        public void GetTimeToNextDispatch_Succeeds_For_PreviousDispatch_Local_ExcludingSeconds() {
            var scheduledEvent = Substitute.For<IScheduledEvent>();
            scheduledEvent.CronExpression.Returns("0/2 * * * *");
            scheduledEvent.CronExpressionIncludesSeconds.Returns(false);
            scheduledEvent.PreviousDispatch.Returns(new DateTime(2000, 1, 1, 4, 3, 0, 0, DateTimeKind.Local) + TimeZoneInfo.Local.BaseUtcOffset);

            Assert.Equal(TimeSpan.FromMilliseconds(56443), scheduledEvent.GetTimeToNextDispatch(new DateTime(2000, 1, 1, 4, 3, 3, 557, DateTimeKind.Utc)));
        }

        [Fact]
        public void GetTimeToNextDispatch_Succeeds_For_PreviousDispatch_Null_IncludingSeconds() {
            var scheduledEvent = Substitute.For<IScheduledEvent>();
            scheduledEvent.CronExpression.Returns("0/15 * * * * *");
            scheduledEvent.CronExpressionIncludesSeconds.Returns(true);
            scheduledEvent.PreviousDispatch.Returns((DateTime?)null);

            Assert.Equal(TimeSpan.FromMilliseconds(11443), scheduledEvent.GetTimeToNextDispatch(new DateTime(2000, 1, 1, 4, 3, 3, 557, DateTimeKind.Utc)));
        }

        [Fact]
        public void GetTimeToNextDispatch_Succeeds_For_PreviousDispatch_Null_ExcludingSeconds() {
            var scheduledEvent = Substitute.For<IScheduledEvent>();
            scheduledEvent.CronExpression.Returns("0/2 * * * *");
            scheduledEvent.CronExpressionIncludesSeconds.Returns(false);
            scheduledEvent.PreviousDispatch.Returns((DateTime?)null);

            Assert.Equal(TimeSpan.FromMilliseconds(56443), scheduledEvent.GetTimeToNextDispatch(new DateTime(2000, 1, 1, 4, 3, 3, 557, DateTimeKind.Utc)));
        }

        [Fact]
        public void GetTimeToNextDispatch_Succeeds_In_The_Past() {
            var scheduledEvent = Substitute.For<IScheduledEvent>();
            scheduledEvent.CronExpression.Returns("0/15 * * * * *");
            scheduledEvent.CronExpressionIncludesSeconds.Returns(true);
            scheduledEvent.PreviousDispatch.Returns(new DateTime(2000, 1, 1, 3, 3, 0, 0, DateTimeKind.Utc));

            Assert.Equal(TimeSpan.Zero, scheduledEvent.GetTimeToNextDispatch(new DateTime(2000, 1, 1, 4, 3, 3, 557, DateTimeKind.Utc)));
        }

        [Fact]
        public void GetTimeToNextDispatch_Succeeds_In_The_Future() {
            var scheduledEvent = Substitute.For<IScheduledEvent>();
            scheduledEvent.CronExpression.Returns("0/15 * * * * *");
            scheduledEvent.CronExpressionIncludesSeconds.Returns(true);
            scheduledEvent.PreviousDispatch.Returns(new DateTime(2000, 1, 1, 4, 3, 15, 0, DateTimeKind.Utc));

            Assert.Equal(TimeSpan.FromMilliseconds(26443), scheduledEvent.GetTimeToNextDispatch(new DateTime(2000, 1, 1, 4, 3, 3, 557, DateTimeKind.Utc)));
        }
    }
}
