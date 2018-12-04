using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CodeGenXSD
{
    public class Processor
    {
        public static CodeNamespace Process()
        {
            CodeNamespace codeNameSpace = new CodeNamespace("MilkyWay.SolarSystem");

            XmlSchemas schemas = new XmlSchemas();
            XmlSchema xsd;

            using (var fs = new FileStream(Path.GetFileName("world.xsd"), FileMode.Open,FileAccess.Read))
            {
                xsd = XmlSchema.Read(fs,null);
                xsd.Compile(null);
                schemas.Add(xsd);
            }

            XmlSchemaImporter schImporter = new XmlSchemaImporter(schemas);

            XmlCodeExporter exCode = new XmlCodeExporter(codeNameSpace);

            foreach (XmlSchemaElement  element in xsd.Elements.Values)
            {
               var importTypeMapping = schImporter.ImportTypeMapping(element.QualifiedName);

               exCode.ExportTypeMapping(importTypeMapping);
            }

            return codeNameSpace;

        }

        public static void GenerateCode(CodeNamespace ns)
        {
            CodeDomProvider provider = new CSharpCodeProvider();

            using (StreamWriter s = new StreamWriter("Auto.cs", false))
            {
                provider.CreateGenerator().GenerateCodeFromNamespace(ns, s, new CodeGeneratorOptions());
            }
        }
    }
}
