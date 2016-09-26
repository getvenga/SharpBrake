using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SharpBrake.Serialization;

namespace SharpBrake.Tests
{
    [TestFixture]
    public class ExtensionTests
    {
        [Test]
        public void CgiDataIsAddedToTheNotice()
        {
            AirbrakeNotice notice = null;
            var client = new Mock<AirbrakeClient>();
            client.Setup(c => c.Send(It.IsAny<AirbrakeNotice>())).Callback<AirbrakeNotice>(r => notice = r);
            var exception = new ApplicationException("Something happened");
            var cgiData = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };
            Extensions.SendToAirbrake(exception, cgiData, client.Object);
            Assert.NotNull(notice);
            Assert.That(notice.Request, Is.Not.Null);
            Assert.That(notice.Request.CgiData.Length, Is.GreaterThanOrEqualTo(2));
            Assert.That(notice.Request.CgiData, Contains.Item(new AirbrakeVar("key1", "value1")));
        }
    }
}
