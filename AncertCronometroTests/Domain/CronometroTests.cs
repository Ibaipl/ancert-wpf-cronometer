using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AncertCronometro.Domain.Tests
{
    [TestClass()]
    public class CronometroTests
    {
        [TestMethod()]
        public void CronometerOperationsTest()
        {
            var cronometro = new Cronometro();

            cronometro.CurrentTime.Should().Be(TimeSpan.Zero);

            cronometro.Start();
            cronometro.CurrentTime.Should().BeGreaterThan(TimeSpan.Zero);

            cronometro.Pause();
            var afterPauseTime = cronometro.CurrentTime;
            cronometro.CurrentTime.Should().Be(afterPauseTime);

            cronometro.Start();
            cronometro.CurrentTime.Should().BeGreaterThan(afterPauseTime);

            cronometro.Stop();
            cronometro.CurrentTime.Should().BeGreaterThan(TimeSpan.Zero);
        }
    }
}