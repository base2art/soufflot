namespace Base2art.Soufflot
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using NUnit.Framework;

    public class StringExtenderFeature
    {
        private class StringInjectTestObject
        {
            public string foo { get; set; }

            public object bar { get; set; }
        }

        /// <summary>
        /// Tests for sunny day with a single string property
        ///</summary>
        [Test]
        public void ShouldInjectOneStringProperty()
        {
            string formatString = "This is a test: foo={foo}";
            object o = new { foo = "abc" };
            string expected = "This is a test: foo=abc";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Tests for sunny day with two string properties
        ///</summary>
        [Test]
        public void ShouldInjectTwoStringProperties()
        {
            string formatString = "This is a test: foo={foo}, bar={bar}";
            object o = new { foo = "abc", bar = "def" };
            string expected = "This is a test: foo=abc, bar=def";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Tests for sunny day with two properties, one string and one integer
        ///</summary>
        [Test]
        public void ShouldInjectTwoMixedProperties()
        {
            string formatString = "This is a test: foo={foo}, bar={bar}";
            object o = new { foo = "abc", bar = 123 };
            string expected = "This is a test: foo=abc, bar=123";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Tests for sunny day with two properties, one string and one integer, both repeated in the format string
        ///</summary>
        [Test]
        public void ShouldInjectTwoMixedPropertiesRepeated()
        {
            string formatString = "This is a test: foo={foo}, bar={bar}; again: foo={foo}, bar={bar}; and again: foo={foo}, bar={bar}";
            object o = new { foo = "abc", bar = 123 };
            string expected = "This is a test: foo=abc, bar=123; again: foo=abc, bar=123; and again: foo=abc, bar=123";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Tests for null value of injection object
        ///</summary>
        [Test]
        public void ShouldInjectNullObject()
        {
            string formatString = "This is a test: foo={foo}, bar={bar}";
            object o = null;
            string expected = "This is a test: foo={foo}, bar={bar}";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Tests for a missing property of the injection object
        ///</summary>
        [Test]
        public void ShouldInjectMissingProperty()
        {
            string formatString = "This is a test: foo={foo}, bar={bar}";
            object o = new { foo = 1 };
            string expected = "This is a test: foo=1, bar={bar}";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Tests for no keys in the format string
        ///</summary>
        [Test]
        public void ShouldInjectNoKeys()
        {
            string formatString = "This is a test";
            object o = new { foo = 1, bar = 2 };
            string expected = "This is a test";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Tests for null format string (returns null)
        ///</summary>
        [Test]
        public void ShouldInjectNullFormatString()
        {
            string formatString = null;
            object o = new { foo = 1, bar = 2 };
            string expected = null;
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Tests for null values in the properties
        ///</summary>
        [Test]
        public void ShouldInjectNullPropertyValue()
        {
            string formatString = "This is a test: foo={foo}, bar={bar}||END";
            StringInjectTestObject o = new StringInjectTestObject() { foo = "abc", bar = null };
            string expected = "This is a test: foo=abc, bar=||END";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Tests for custom number format
        ///</summary>
        [Test]
        public void ShouldInjectCustomNumberFormat()
        {
            string formatString = "This is a test: foo={foo:00.00}";
            object o = new { foo = 1.2345 };
            string expected = "This is a test: foo=01.23";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Tests for custom date formats
        ///</summary>
        [Test]
        public void ShouldInjectCustomDateFormats()
        {
            string formatString = "This is a test: foo={foo:MM-dd-yy HH:mm:ss} or {foo:MM/dd/yyyy}";
            object o = new { foo = new DateTime(1999, 12, 31, 23, 59, 59) };
            string expected = "This is a test: foo=12-31-99 23:59:59 or 12/31/1999";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        /// <summary>
        /// Puts all sorts of formats together in a long string
        ///</summary>
        [Test]
        public void ShouldInjectComplexTest()
        {
            string formatString = @"
Dear {Name},

It was brought to our attention that on {Date:MMM-dd yyyy} at {Date:h:mm tt} your {Animal} was seen
running loose again at {Location}. 

This is just the latest of {PreviousInfractionCount} previous infractions. 

Please keep your {Animal} on a leash, or further action will need to be taken.

Sincerely,
{From}";
            object o = new
            {
                Name = "Old McDonald",
                PreviousInfractionCount = 17,
                Animal = "cow",
                Date = new DateTime(2008, 7, 18, 17, 47, 30),
                Location = "the corner of 1st and New Jersey",
                From = "Your HOA Board"
            };
            string expected = @"
Dear Old McDonald,

It was brought to our attention that on Jul-18 2008 at 5:47 PM your cow was seen
running loose again at the corner of 1st and New Jersey. 

This is just the latest of 17 previous infractions. 

Please keep your cow on a leash, or further action will need to be taken.

Sincerely,
Your HOA Board";
            Assert.AreEqual(expected, formatString.Inject(o));
        }

        //todo: more date formats
        //todo: more numeric formats

        /// <summary>
        /// Tests for Hashtable input
        ///</summary>
        [Test]
        public void ShouldInjectTestHashTableInputFormat()
        {
            string formatString = "This is a test: foo={foo:00.00}, bar={bar}";
            Hashtable ht = new Hashtable();
            ht.Add("bar", "abc");
            ht.Add("foo", 1.2345);
            string expected = "This is a test: foo=01.23, bar=abc";
            Assert.AreEqual(expected, formatString.Inject(ht));
        }

        /// <summary>
        /// Tests for Dictionary input
        ///</summary>
        [Test]
        public void ShouldInjectTestDictionaryInputFormat()
        {
            string formatString = "This is a test: foo={foo:00.00}, bar={bar}, ya={ya}";
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("bar", "abc");
            d.Add("foo", 1.2345);
            d.Add("blah", "some not-used value");
            d.Add("ya", null);
            string expected = "This is a test: foo=01.23, bar=abc, ya=";
            Assert.AreEqual(expected, formatString.Inject(d));
        }
    }
}
