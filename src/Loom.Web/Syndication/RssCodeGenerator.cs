#region Using Directives

using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;

#endregion

// ReSharper disable BitwiseOperatorOnEnumWihtoutFlags

namespace Loom.Web.Syndication
{
    // helper class to generate CodeDom (and source) of strongly-typed definition 
    // for a RSS channel
    public static class RssCodeGenerator
    {
        public static void GenerateCode(GenericRssChannel channelDefinition,
            string outputLanguage, string namespaceName, string classNamePrefix,
            TextWriter outputCode)
        {
            // get the CodeDom provider for the language
            CodeDomProvider provider = CodeDomProvider.CreateProvider(outputLanguage);

            // generate the CodeDom tree
            CodeCompileUnit unit = new CodeCompileUnit();
            GenerateCodeDomTree(channelDefinition, namespaceName, classNamePrefix, unit);

            // generate source
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BlankLinesBetweenMembers = true;
            options.BracingStyle = "Block";
            options.ElseOnClosing = false;
            options.IndentString = "    ";

            provider.GenerateCodeFromCompileUnit(unit, outputCode, options);
        }

        public static void GenerateCodeDomTree(GenericRssChannel channelDefinition,
            string namespaceName, string classNamePrefix,
            CodeCompileUnit outputCodeCompileUnit)
        {
            // generate namespace

            CodeNamespace generatedNamespace = new CodeNamespace(namespaceName);
            generatedNamespace.Imports.Add(new CodeNamespaceImport("System"));

            // generate item class

            string itemTypeName = classNamePrefix + "Item";
            CodeTypeDeclaration itemType = new CodeTypeDeclaration(itemTypeName);
            itemType.BaseTypes.Add(typeof(RssElementBase));

            if (channelDefinition.Items.Count > 0)
            {
                // item ctors (one default and one that takes attributes)
                GenerateCtorsFromAttributes(itemType, channelDefinition.Items[0]);

                // item atributes as explicit properties
                GenerateAttributes(itemType, channelDefinition.Items[0]);
            }

            generatedNamespace.Types.Add(itemType);

            // generate image class

            bool hasImage = channelDefinition.Image != null;
            string imageTypeName;

            if (hasImage)
            {
                imageTypeName = classNamePrefix + "Image";
                CodeTypeDeclaration imageType = new CodeTypeDeclaration(imageTypeName);
                imageType.BaseTypes.Add(typeof(RssElementBase));

                // image atributes as explicit properties
                GenerateAttributes(imageType, channelDefinition.Image);

                // generate SetDefaults method for image attribute
                GenerateSetDefaultsMethod(imageType, channelDefinition.Image);

                generatedNamespace.Types.Add(imageType);
            }
            else
            {
                imageTypeName = typeof(GenericRssElement).FullName;
            }

            // generate channel class

            string channelTypeName = classNamePrefix + "Channel";
            CodeTypeDeclaration channelType = new CodeTypeDeclaration(channelTypeName);
            channelType.BaseTypes.Add(new CodeTypeReference("RssToolkit.RssChannelBase", new CodeTypeReference(itemTypeName), new CodeTypeReference(imageTypeName)));

            // channel atributes as explicit properties
            GenerateAttributes(channelType, channelDefinition);

            if (hasImage)
                GenerateImageProperty(channelType, imageTypeName);

            // generate SetDefaults method
            GenerateSetDefaultsMethod(channelType, channelDefinition);

            if (!Compare.IsNullOrEmpty(channelDefinition.Url))
            {
                // LoadChannel (and LoadChannelItems) method
                GenerateLoadChannel(channelType, channelDefinition.Url);
                GenerateLoadChannelItems(channelType, itemTypeName);
            }

            generatedNamespace.Types.Add(channelType);

            // generate http handler base class
            string handlerTypeName = classNamePrefix + "HttpHandlerBase";
            CodeTypeDeclaration handlerType = new CodeTypeDeclaration(handlerTypeName);

            handlerType.BaseTypes.Add(new CodeTypeReference("RssToolkit.RssHttpHandlerBase", new CodeTypeReference(channelTypeName), new CodeTypeReference(itemTypeName), new CodeTypeReference(imageTypeName)));

            generatedNamespace.Types.Add(handlerType);

            // add the generated namespace to the code compile unit
            outputCodeCompileUnit.Namespaces.Add(generatedNamespace);
        }

        private static void GenerateCtorsFromAttributes(CodeTypeDeclaration type, RssElementBase element)
        {
            // argless ctor first
            CodeConstructor c0 = new CodeConstructor();
            c0.Attributes &= ~MemberAttributes.AccessMask;
            c0.Attributes |= MemberAttributes.Public;
            type.Members.Add(c0);

            // ctor with attributes as arguments
            if (element.Attributes.Count > 0)
            {
                CodeConstructor cN = new CodeConstructor();
                cN.Attributes &= ~MemberAttributes.AccessMask;
                cN.Attributes |= MemberAttributes.Public;

                // param list
                foreach (KeyValuePair<string, string> attr in element.Attributes)
                    cN.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string),
                        CodeParamNameFromRssName(attr.Key)));

                // ctor body
                foreach (KeyValuePair<string, string> attr in element.Attributes)
                    cN.Statements.Add(new CodeMethodInvokeExpression(
                        new CodeBaseReferenceExpression(),
                        "SetAttributeValue", new CodePrimitiveExpression(attr.Key), new CodeArgumentReferenceExpression(CodeParamNameFromRssName(attr.Key))));

                type.Members.Add(cN);
            }
        }

        private static void GenerateAttributes(CodeTypeDeclaration type, RssElementBase element)
        {
            foreach (KeyValuePair<string, string> attr in element.Attributes)
            {
                string rssName = attr.Key;
                string codeName = CodeNameFromRssName(rssName);

                CodeMemberProperty prop = new CodeMemberProperty();
                prop.Attributes &= ~MemberAttributes.AccessMask;
                prop.Attributes |= MemberAttributes.Public;
                prop.Name = codeName;
                prop.Type = new CodeTypeReference(typeof(string));

                prop.GetStatements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(
                    new CodeBaseReferenceExpression(),
                    "GetAttributeValue", new CodePrimitiveExpression(rssName))));

                prop.SetStatements.Add(new CodeMethodInvokeExpression(
                    new CodeBaseReferenceExpression(),
                    "SetAttributeValue", new CodePrimitiveExpression(rssName), new CodePropertySetValueReferenceExpression()));

                type.Members.Add(prop);
            }
        }

        private static void GenerateImageProperty(CodeTypeDeclaration channelType, string imageTypeName)
        {
            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Attributes &= ~MemberAttributes.AccessMask;
            prop.Attributes |= MemberAttributes.Public;
            prop.Name = "Image";
            prop.Type = new CodeTypeReference(imageTypeName);

            prop.GetStatements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(
                new CodeBaseReferenceExpression(),
                "GetImage")));

            channelType.Members.Add(prop);
        }

        private static void GenerateSetDefaultsMethod(CodeTypeDeclaration type, RssElementBase element)
        {
            CodeMemberMethod m = new CodeMemberMethod();
            m.Name = "SetDefaults";
            m.Attributes &= ~MemberAttributes.AccessMask;
            m.Attributes &= ~MemberAttributes.ScopeMask;
            m.Attributes |= MemberAttributes.Public | MemberAttributes.Override;

            // attributes' defaults
            foreach (KeyValuePair<string, string> attr in element.Attributes)
                m.Statements.Add(new CodeMethodInvokeExpression(
                    new CodeBaseReferenceExpression(),
                    "SetAttributeValue", new CodePrimitiveExpression(attr.Key), new CodePrimitiveExpression(attr.Value)));

            // image defaults if this is channel with an image
            if (element is GenericRssChannel && (element as GenericRssChannel).Image != null)
                m.Statements.Add(new CodeMethodInvokeExpression(
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Image"),
                    "SetDefaults"));

            type.Members.Add(m);
        }

        private static void GenerateLoadChannel(CodeTypeDeclaration channelType, string url)
        {
            const string varName = "channel";
            string typeName = channelType.Name;

            CodeMemberMethod m = new CodeMemberMethod();
            m.Name = "LoadChannel";
            m.Attributes &= ~MemberAttributes.AccessMask;
            m.Attributes |= MemberAttributes.Public | MemberAttributes.Static;
            m.ReturnType = new CodeTypeReference(typeName);

            m.Statements.Add(new CodeVariableDeclarationStatement(typeName, varName));

            m.Statements.Add(new CodeAssignStatement(
                new CodeVariableReferenceExpression(varName),
                new CodeObjectCreateExpression(typeName)));

            m.Statements.Add(new CodeMethodInvokeExpression(
                new CodeVariableReferenceExpression(varName),
                "LoadFromUrl", new CodePrimitiveExpression(url)));

            m.Statements.Add(new CodeMethodReturnStatement(
                new CodeVariableReferenceExpression(varName)));

            channelType.Members.Add(m);
        }

        private static void GenerateLoadChannelItems(CodeTypeDeclaration channelType, string itemTypeName)
        {
            CodeMemberMethod m = new CodeMemberMethod();
            m.Name = "LoadChannelItems";
            m.Attributes &= ~MemberAttributes.AccessMask;
            m.Attributes |= MemberAttributes.Public | MemberAttributes.Static;
            m.ReturnType = new CodeTypeReference("System.Collections.Generic.List", new CodeTypeReference(itemTypeName));

            m.Statements.Add(new CodeMethodReturnStatement(
                new CodePropertyReferenceExpression(
                    new CodeMethodInvokeExpression(
                        new CodeMethodReferenceExpression(null, "LoadChannel", null)
                    ),
                    "Items"
                )
            ));

            channelType.Members.Add(m);
        }

        private static string CodeNameFromRssName(string rssName)
        {
            StringBuilder sb = new StringBuilder(rssName.Length);

            if (rssName.IndexOf(':') >= 0)
            {
                foreach (string s in rssName.Split(':'))
                {
                    if (s.Length > 0)
                        sb.Append(s.Substring(0, 1).ToUpperInvariant());

                    if (s.Length > 1)
                        sb.Append(s.Substring(1));
                }
            }
            else
            {
                if (rssName.Length > 0)
                    sb.Append(rssName.Substring(0, 1).ToUpperInvariant());

                if (rssName.Length > 1)
                    sb.Append(rssName.Substring(1));
            }

            return sb.ToString();
        }

        private static string CodeParamNameFromRssName(string rssName)
        {
            string name = CodeNameFromRssName(rssName);

            if (name.Length > 1)
                name = name.Substring(0, 1).ToLowerInvariant() + name.Substring(1);
            else
                name = name.ToLowerInvariant();

            return name;
        }
    }
}