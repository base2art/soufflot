namespace Base2art.Soufflot.Http.Util
{
    using System;
    using System.IO;

    using Base2art.Soufflot.Http.Util;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class StreamByteReaderFeature
    {
        [Test]
        public void ShouldReadMemoryStream()
        {
            using (var memStr = new MemoryStream())
            {
                memStr.WriteByte(01);
                memStr.WriteByte(03);
                memStr.WriteByte(02);
                memStr.Flush();

                memStr.Seek(0L, SeekOrigin.Begin);

                var rez = memStr.ReadFully().Value;
                rez.Length.Should().Be(3);
                rez[0].Should().Be(01);
                rez[1].Should().Be(03);
                rez[2].Should().Be(02);
            }
        }

        [Test]
        public void ShouldReadFileStream()
        {
            var buf = new byte[2000];
            new Random().NextBytes(buf);

            string tempFileName = Path.GetTempFileName();
            File.WriteAllBytes(tempFileName, buf);

            using (var memStr = File.OpenRead(tempFileName))
            {
                var rez1 = memStr.ReadFully();
                var rez = rez1.Value;
                rez1.MaxLengthExceded.Should().BeFalse();
                rez.Length.Should().Be(buf.Length);
                for (int i = 0; i < rez.Length; i++)
                {
                    rez[i].Should().Be(buf[i]);
                }
            }
        }

        [Test]
        public void ShouldReadFileStreamMaxLen()
        {
            var buf = new byte[1024 * 34];
            new Random().NextBytes(buf);

            string tempFileName = Path.GetTempFileName();
            File.WriteAllBytes(tempFileName, buf);

            using (var memStr = File.OpenRead(tempFileName))
            {
                var rez = memStr.ReadFully(1024);
                rez.MaxLengthExceded.Should().BeTrue();
                rez.Value.Length.Should().Be(1024 * 16);

                for (int i = 0; i < 1024 * 16; i++)
                {
                    rez.Value[i].Should().Be(buf[i]);
                }
            }
        }
    }
}
