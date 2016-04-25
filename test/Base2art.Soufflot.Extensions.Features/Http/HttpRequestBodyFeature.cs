namespace Base2art.Soufflot.Http
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;

    using Base2art.Collections;
    using Base2art.Soufflot.Http;

    using FakeItEasy;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class HttpRequestBodyFeature
    {
        string JSON = "application/json";
        string FormEncoded = "application/x-www-form-urlencoded";
        
        [Test]
        public void ShouldObeyMaxSize()
        {
            var request = A.Fake<IHttpRequestBody>();
            A.CallTo(() => request.IsMaxSizeExceeded).Returns(true);
            request.As<Person>().Should().BeNull();
            request.AsFormUrlEncoded().Should().BeEmpty();
            request.AsXml().Should().BeNull();
            //            request.AsJson().Should().BeNull();
        }
        
        [Test]
        public void ShouldLoad_AsJsonInternally()
        {
            var json = this.CreateRequest("[{name:'Youngblut'}]", JSON).As<Person[]>();
            json.Should().NotBeNull();
            json[0].Name.Should().Be("Youngblut");
//            Console.WriteLine(json);
        }

        [Test]
        public void ShouldLoadJson_Object()
        {
            var json = this.CreateBody("{scott:'Youngblut'}").AsJson();
            object obj = json;
            obj.Should().NotBeNull();
            
            string s = json.Value<string>("scott");
            s.Should().Be("Youngblut");
            
            //            string s1 = json.Value<string>("Scott");
            //            s1.Should().Be("Youngblut");
            
            string s2 = json.scott;
            s2.Should().Be("Youngblut");
            
//            Console.WriteLine(json);
        }

        [Test]
        public void ShouldLoadJson_Array()
        {
            var json = this.CreateBody("[{scott:'Youngblut'}]").AsJson();
            object obj = json;
            obj.Should().NotBeNull();
            
            string s = json[0].Value<string>("scott");
            s.Should().Be("Youngblut");
            
            //            string s1 = json.Value<string>("Scott");
            //            s1.Should().Be("Youngblut");
            
            string s2 = json[0].scott;
            s2.Should().Be("Youngblut");
            
//            Console.WriteLine(json);
        }

        [Test]
        public void ShouldLoadXml()
        {
            var xml = this.CreateBody("<root name='Value'>test</root>").AsXml();
            xml.Should().NotBeNull();
            xml.Name.ToString().Should().Be("root");
            xml.Attribute("name").Value.Should().Be("Value");
            xml.Value.Should().Be("test");
        }

        [Test]
        public void ShouldLoadBinary()
        {
            var person = new Person { Name = "Scott" };
            var formatter = new BinaryFormatter();
            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, person);
                stream.Flush();
                bytes = stream.ToArray();
            }

            var p1 = this.CreateRequest(bytes).As<Person>();
            p1.Should().NotBeNull();
            p1.Name.Should().Be("Scott");
        }

        [Test]
        // SAMPLE DATA FROM HERE:
        //http://www.restfm.com/restfm-manual/web-api-reference-documentation/submitting-data/applicationx-www-form-urlencoded-and
        public void ShouldLoadFormEncoded_Explicit()
        {
            var dict = this.CreateBody("RFMformat=JSON&RFMdata=%7B%22data%22%3A%5B%7B%22Pcode%22%3A%229998%22%2C%22Locality%22%3A%22A%20New%20Location%201%22%7D%2C%7B%22Pcode%22%3A%229999%22%2C%22Locality%22%3A%22A%20New%20Location%202%22%7D%5D%7D").AsFormUrlEncoded();
            dict.Should().NotBeNull();
            dict["RFMformat"].FirstOrDefault().Should().Be("JSON");
            dict["RFMformat"].Should().HaveCount(1);
            dict["RFMdata"].FirstOrDefault().Should().Be(@"{""data"":[{""Pcode"":""9998"",""Locality"":""A New Location 1""},{""Pcode"":""9999"",""Locality"":""A New Location 2""}]}");
            dict["RFMdata"].Should().HaveCount(1);
        }

        [Test]
        public void ShouldLoadFormEncoded_Implicit_Object()
        {
            var content = "Name=Scott";
            var dict = this.CreateRequest(content, FormEncoded)
                .As<Person>();
            dict.Should().NotBeNull();
            dict.Name.Should().Be("Scott");
        }

        [Test]
        public void ShouldLoadFormEncoded_Implicit_ObjectComplex()
        {
            var content = "Driver.name=Scott";
            var dict = this.CreateRequest(content, FormEncoded)
                .As<Car>();
            dict.Should().NotBeNull();
            dict.Driver.Name.Should().Be("Scott");
        }

        [Test]
        public void ShouldLoadFormEncoded_Implicit_ObjectComplexWithArray()
        {
            var content = "Passengers[0].name=Scott&Passengers[1].name=Matt";
            var dict = this.CreateRequest(content, FormEncoded)
                .As<Car>();
            dict.Should().NotBeNull();
            dict.Passengers.Length.Should().Be(2);
            dict.Passengers[0].Name.Should().Be("Scott");
            dict.Passengers[1].Name.Should().Be("Matt");
        }

        [Test]
        public void ShouldLoadFormEncoded_Implicit_ObjectComplexWithArray_Reversed()
        {
            var content = "Passengers[1].name=Matt&Passengers[0].name=Scott&Driver.Name=SjY";
            var dict = this.CreateRequest(content, FormEncoded)
                .As<Car>();
            dict.Should().NotBeNull();
            dict.Passengers.Length.Should().Be(2);
            dict.Passengers[0].Name.Should().Be("Scott");
            dict.Passengers[1].Name.Should().Be("Matt");
            dict.Driver.Name.Should().Be("SjY");
        }

        [Test]
        public void ShouldLoadFormEncoded_Implicit_ObjectComplexWithArray_MisMatchedType()
        {
            var content = "Passengers[0].name=Scott&Passengers.name=Matt&Driver.Name=SjY";
            var dict = this.CreateRequest(content, FormEncoded)
                .As<Car>();
            dict.Should().NotBeNull();
            dict.Passengers.Length.Should().Be(1);
            dict.Passengers[0].Name.Should().Be("Scott");
//            dict.Passengers[1].Name.Should().Be("Matt");
            dict.Driver.Name.Should().Be("SjY");
        }

        [Test]
        public void ShouldLoadFormEncoded_Implicit_ObjectComplexWithArray_Reversed_MisMatchedType()
        {
            var content = "Passengers.name=Matt&Passengers[0].name=Scott&Driver.Name=SjY";
            var dict = this.CreateRequest(content, FormEncoded)
                .As<Car>();
            dict.Should().NotBeNull();
            dict.Passengers.Length.Should().Be(1);
            dict.Passengers[0].Name.Should().Be("Scott");
//            dict.Passengers[1].Name.Should().Be("Matt");
            dict.Driver.Name.Should().Be("SjY");
        }

        private IHttpRequest CreateRequest(string text, string type=null)
        {
            var request = A.Fake<IHttpRequest>();
            var requestBody = A.Fake<IHttpRequestBody>();
            A.CallTo(() => request.RequestBody).Returns(requestBody);
            A.CallTo(() => requestBody.AsText()).Returns(text);
            
            var readonlyCollection = new HeaderCollection();
            readonlyCollection.Add("content-type", type);
            A.CallTo(() => request.Headers).Returns(readonlyCollection);
            
            return request;
        }

        private IHttpRequestBody CreateBody(string text)
        {
            var request = A.Fake<IHttpRequestBody>();
            A.CallTo(() => request.AsRaw()).Returns(System.Text.Encoding.Default.GetBytes(text));
            A.CallTo(() => request.AsText()).Returns(text);
            return request;
        }

        private IHttpRequest CreateRequest(byte[] bytes)
        {
            var request = A.Fake<IHttpRequest>();
            var requestBody = A.Fake<IHttpRequestBody>();
            A.CallTo(() => request.RequestBody).Returns(requestBody);
            A.CallTo(() => requestBody.AsRaw()).Returns(bytes);
            return request;
        }

        private IHttpRequestBody CreateBody(byte[] bytes)
        {
            var request = A.Fake<IHttpRequestBody>();
            A.CallTo(() => request.AsRaw()).Returns(bytes);
            return request;
        }

        [Serializable]
        private class Person
        {
            public string Name { get; set; }
        }
        
        [Serializable]
        private class Car
        {
            public Person Driver { get; set; }
            
            public Person[] Passengers { get; set; }
        }
        
        
        private class HeaderCollection : MultiMap<string,string>, IHttpReadOnlyHeaderCollection
        {
        }
    }
}
