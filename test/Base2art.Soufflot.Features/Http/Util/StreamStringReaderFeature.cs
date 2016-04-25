namespace Base2art.Soufflot.Http.Util
{
    using System;
    using System.IO;
    using System.Text;

    using Base2art.Soufflot.Http.Util;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class StreamStringReaderFeature
    {
//        [Test]
//        public void ShouldReadMemoryStream()
//        {
//            using (var memStr = new MemoryStream())
//            {
//                memStr.WriteByte(01);
//                memStr.WriteByte(03);
//                memStr.WriteByte(02);
//                memStr.Flush();
//
//                memStr.Seek(0L, SeekOrigin.Begin);
//                
//                var rez = memStr.ReadFully().Value;
//                rez.Length.Should().Be(3);
//                rez[0].Should().Be(01);
//                rez[1].Should().Be(03);
//                rez[2].Should().Be(02);
//            }
//        }

        [Test]
        public void ShouldReadFileStream()
        {
            var buf = this.Repeat("01234567890qwertyuiopasdfghjklzxcvbnm", 1024*16);

            string tempFileName = Path.GetTempFileName();
            File.WriteAllText(tempFileName, buf);

            using (var memStr = File.OpenRead(tempFileName))
            {
                var rez1 = memStr.ReadFullyAsString();
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
            var buf = this.Repeat("01234567890qwertyuiopasdfghjklzxcvbnm", 1024 * 16);

            string tempFileName = Path.GetTempFileName();
            File.WriteAllText(tempFileName, buf);

            using (var memStr = File.OpenRead(tempFileName))
            {
                var rez1 = memStr.ReadFullyAsString(1024);
                var rez = rez1.Value;
                rez1.MaxLengthExceded.Should().BeTrue();
                rez.Length.Should().Be((1024 * 16));
                for (int i = 0; i < (1024*16); i++)
                {
                    rez[i].Should().Be(buf[i]);
                }
            }
        }

        private string Repeat(string text, int iter)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iter; i++)
            {
                sb.Append(text);
            }

            return sb.ToString();
        }
    }
}
