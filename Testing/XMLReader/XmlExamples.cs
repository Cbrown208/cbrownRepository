using System;
using System.IO;
using System.Text;
using System.Xml;

namespace XMLReader
{
    public class XmlExamples
    {
        public void Example1()
        {
            StringBuilder output = new StringBuilder();

            String xmlString =
                @"<?xml version='1.0'?> <!-- This is a sample XML document --> <Items> <Item>test with a child element <more/> stuff</Item></Items>";
// Create an XmlReader
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(output, ws))
                {

                    // Parse the file and display each of the nodes.
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                writer.WriteStartElement(reader.Name);
                                break;
                            case XmlNodeType.Text:
                                writer.WriteString(reader.Value);
                                break;
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.ProcessingInstruction:
                                writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                break;
                            case XmlNodeType.Comment:
                                writer.WriteComment(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                writer.WriteFullEndElement();
                                break;
                        }
                    }

                }
            }
            Console.WriteLine(output.ToString());
        }

        public void Example2()
        {
            StringBuilder output = new StringBuilder();

            String xmlString =
                @"<bookstore>
        <book genre='autobiography' publicationdate='1981-03-22' ISBN='1-861003-11-0'>
            <title>The Autobiography of Benjamin Franklin</title>
            <author>
                <first-name>Benjamin</first-name>
                <last-name>Franklin</last-name>
            </author>
            <price>8.99</price>
        </book>
    </bookstore>";

            // Create an XmlReader
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing("book");
                reader.MoveToFirstAttribute();
                string genre = reader.Value;
                output.AppendLine("The genre value: " + genre);

                reader.ReadToFollowing("title");
                output.AppendLine("Content of the title element: " + reader.ReadElementContentAsString());
            }

            Console.WriteLine(output.ToString());
        }
    }
}
