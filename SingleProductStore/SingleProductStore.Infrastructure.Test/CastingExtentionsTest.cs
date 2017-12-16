using System;
using Xunit;

namespace SingleProductStore.Infrastructure.Test
{
    public class CastingExtentionsTest
    {
        [Fact]
        public void ToInt_DayOfWeekEnum_ShouldReturnDayNumber()
        {
            int dayOfWeek = DayOfWeek.Monday.ToInt();

            Assert.Equal(1, dayOfWeek);
        }
    }
}
