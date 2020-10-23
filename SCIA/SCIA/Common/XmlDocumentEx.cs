#region

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NJsonSchema.Annotations;
using NUnit.Framework;

#endregion
namespace SCIA.Common
{
  

    public class XmlDocumentEx : XmlDocument
  {
    #region Constructors

    public XmlDocumentEx()
    {
    }

    public XmlDocumentEx([NotNull] string filePath)
    {

      FilePath = filePath;
      Load(filePath);
    }

    #endregion

    #region Properties

    public string FilePath { get; protected set; }

    #endregion

    #region Public Methods

    [CanBeNull]
    public new static XmlDocumentEx LoadXml(string xml)
    {
      try
      {
        XmlDocument doc = new XmlDocumentEx();
        doc.LoadXml(xml);
        return (XmlDocumentEx)doc;
      }
      catch (Exception ex)
      {
        return null;
      }
    }
    
    [CanBeNull]
    public static XmlDocumentEx LoadStream(Stream stream)
    {
      try
      {
        XmlDocument doc = new XmlDocumentEx();
        doc.Load(stream);
        return (XmlDocumentEx)doc;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public override sealed void Load([NotNull] string filename)
    {

      FilePath = filename;
      base.Load(filename);
    }

    public void Save()
    {
      Assert.IsNotNull(FilePath.EmptyToNull(), "FilePath is empty");

      Save(FilePath);
    }

    #endregion

    #region Public properties

    #endregion

    #region Public methods

    [NotNull]
    public static XmlDocumentEx LoadFile([NotNull] string path)
    {
      if (!File.Exists(path))
      {
        throw new FileIsMissingException("The " + path + " doesn't exists");
      }

      var document = new XmlDocumentEx
      {
        FilePath = path
      };
      document.Load(path);
      return document;
    }

    [CanBeNull]
    public static XmlDocumentEx LoadFileSafe([NotNull] string path)
    {

      if (!File.Exists(path))
      {
        return null;
      }

      var document = new XmlDocumentEx
      {
        FilePath = path
      };

      document.Load(path);
      return document;
    }

    public static string Normalize(string xml)
    {
      var doc = LoadXml(xml);
      var stringWriter = new StringWriter(new StringBuilder());
      var xmlTextWriter = new XmlTextWriter(stringWriter)
      {
        Formatting = Formatting.Indented
      };
      doc.Save(xmlTextWriter);
      return stringWriter.ToString();
    }

    // returns itself after applying changes
    public string GetElementAttributeValue(string xpath, string attributeName)
    {
      // Assert.IsTrue(xpath[0] == '/', "The xpath expression must be rooted");
      XmlElement element = this.SelectSingleElement(FixNotRootedXpathExpression(xpath));
      if (element != null && element.Attributes[attributeName] != null)
      {
        return element.Attributes[attributeName].Value;
      }

      return string.Empty;
    }


    public void SetElementAttributeValue(string xpath, string attributeName, string value)
    {
      // Assert.IsTrue(xpath[0] == '/', "The xpath expression must be rooted");
      XmlElement element = this.SelectSingleElement(FixNotRootedXpathExpression(xpath));
      if (element != null && element.Attributes[attributeName] != null)
      {
        element.Attributes[attributeName].Value = value;
      }
    }

    public void SetElementValue(string xpath, string value)
    {
      // Assert.IsTrue(xpath[0] == '/', "The xpath expression must be rooted");
      XmlElement element = this.SelectSingleElement(FixNotRootedXpathExpression(xpath));

      if (element != null)
      {
        if (value.IsNullOrEmpty()) return;

        element.InnerText = value;
        return;
      }

      var segments = xpath.Split('/').Where(w => !string.IsNullOrEmpty(w)).ToArray();

      var path = Prefix.TrimEnd("/");
      element = DocumentElement;
      for (int i = 1; i < segments.Length; ++i)
      {
        var segment = segments[i];
        path += "/" + segment;
        var newElement = this.SelectSingleElement(Prefix + path);
        if (newElement == null)
        {
          newElement = CreateElement(segment);
          element.AppendChild(newElement);
        }

        element = newElement;
      }

      if (value.IsNullOrEmpty()) return;

      element.InnerText = value;
    }

    #endregion

    #region Private methods



    private string FixNotRootedXpathExpression(string xpathExpr)
    {
      if (xpathExpr[0] == '/')
      {
        return xpathExpr;
      }
      else
      {
        return @"/" + xpathExpr;
      }
    }

    #endregion

    #region Nested type: FileIsMissingException

    public class FileIsMissingException : Exception
    {
      #region Constructors

      public FileIsMissingException(string message)
        : base(message)
      {
      }

      #endregion
    }

    #endregion

    
    
  }
}