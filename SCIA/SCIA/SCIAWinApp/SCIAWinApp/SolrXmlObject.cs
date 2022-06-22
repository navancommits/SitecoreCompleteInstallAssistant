using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace SCIA
{
	public class SolrXmlObject
	{
		public string SolrService { get; set; }
		public string SolrVersion { get; set; }
		public string SolrRoot { get; set; }
	}

	[XmlRoot(ElementName = "int")]
		public class Int
		{
			[XmlAttribute(AttributeName = "name")]
			public string Name { get; set; }
			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "lst")]
		public class Lst
		{
			[XmlElement(ElementName = "int")]
			public List<Int> Int { get; set; }
			[XmlAttribute(AttributeName = "name")]
			public string Name { get; set; }
			[XmlElement(ElementName = "str")]
			public List<Str> Str { get; set; }
			[XmlElement(ElementName = "lst")]
			public List<Lst> LstNew { get; set; }
			[XmlElement(ElementName = "double")]
			public List<Double> Double { get; set; }
			[XmlElement(ElementName = "long")]
			public List<Long> Long { get; set; }
		}

		[XmlRoot(ElementName = "str")]
		public class Str
		{
			[XmlAttribute(AttributeName = "name")]
			public string Name { get; set; }
			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "long")]
		public class Long
		{
			[XmlAttribute(AttributeName = "name")]
			public string Name { get; set; }
			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "double")]
		public class Double
		{
			[XmlAttribute(AttributeName = "name")]
			public string Name { get; set; }
			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "arr")]
		public class Arr
		{
			[XmlElement(ElementName = "str")]
			public List<string> Str { get; set; }
			[XmlAttribute(AttributeName = "name")]
			public string Name { get; set; }
		}

		[XmlRoot(ElementName = "date")]
		public class Date
		{
			[XmlAttribute(AttributeName = "name")]
			public string Name { get; set; }
			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "response")]
		public class Response
		{
			[XmlElement(ElementName = "lst")]
			public List<Lst> Lst { get; set; }
			[XmlElement(ElementName = "str")]
			public List<Str> Str { get; set; }
		}

	}

