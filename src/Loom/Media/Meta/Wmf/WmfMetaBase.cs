#region Using Directives

using System;
using System.Text;

#endregion

namespace Loom.Media.Meta.Wmf
{
    public abstract class WmfMetaBase : IDisposable
    {
        private readonly NativeMethods.IWMMetadataEditor metaEditor;

        protected WmfMetaBase(string fileName)
        {
            Argument.Assert.IsNotNullOrEmpty(fileName, nameof(fileName));
            Argument.Assert.FileExists(fileName);

            FileName = fileName;
            NativeMethods.WMCreateEditor(out metaEditor);
            metaEditor.Open(fileName);
        }

        public string FileName { get; }

        internal NativeMethods.IWMMetadataEditor MetaEditor => metaEditor;

        #region IDisposable Members

        public void Dispose()
        {
            metaEditor.Flush();
            metaEditor.Close();
        }

        #endregion

        internal static string ConvertAttributeToString(byte[] pbValue, ushort dwValueLen, NativeMethods.WMT_ATTR_DATATYPE wAttribType)
        {
            if (0 == dwValueLen)
                return string.Empty;

            //check if the type is a Boolean and return a yes or no           
            if (wAttribType == NativeMethods.WMT_ATTR_DATATYPE.WMT_TYPE_BOOL)
                return pbValue[0] == 1 ? "yes" : "no";

            if (wAttribType == NativeMethods.WMT_ATTR_DATATYPE.WMT_TYPE_QWORD)
                return BitConverter.ToInt64(pbValue, 0).ToString();

            if (wAttribType == NativeMethods.WMT_ATTR_DATATYPE.WMT_TYPE_DWORD)
                return BitConverter.ToInt32(pbValue, 0).ToString();

            //otherwise convert byte[] to string
            StringBuilder value;

            if (0xFE == Convert.ToInt16(pbValue[0]) && 0xFF == Convert.ToInt16(pbValue[1]))
            {
                value = new StringBuilder("UTF-16LE BOM+");

                if (4 <= dwValueLen)
                    for (int i = 0; i < pbValue.Length - 2; i += 2)
                        value.Append(BitConverter.ToChar(pbValue, i));
            }
            else if (0xFF == Convert.ToInt16(pbValue[0]) && 0xFE == Convert.ToInt16(pbValue[1]))
            {
                value = new StringBuilder("UTF-16BE BOM+");
                if (4 <= dwValueLen)
                    for (int i = 0; i < pbValue.Length - 2; i += 2)
                        value.Append(BitConverter.ToChar(pbValue, i));
            }
            else
            {
                value = new StringBuilder();
                if (2 <= dwValueLen)
                    for (int i = 0; i < pbValue.Length - 2; i += 2)
                        value.Append(BitConverter.ToChar(pbValue, i));
            }

            return value.ToString();
        }

        public int GetAttributeCount()
        {
            const ushort streamIndex = 0;
            ushort attrCount;

            NativeMethods.IWMHeaderInfo3 headerInfo = (NativeMethods.IWMHeaderInfo3) metaEditor;
            headerInfo.GetAttributeCount(streamIndex, out attrCount);
            return attrCount;
        }

        protected MetaAttribute GetMetaAttributeByIndex(int index)
        {
            try
            {
                ushort i = (ushort) index;
                ushort streamNum = 0; //media stream to interrogate
                NativeMethods.WMT_ATTR_DATATYPE wAttribType; //data type of attribute
                byte[] pbAttribValue = null; //value of attribute (as returned by method call)
                ushort wAttribValueLen = 0; //length of attribute (byte array)
                StringBuilder pwszName = null;
                ushort pcchNameLen = 0;

                NativeMethods.IWMHeaderInfo3 headerInfo = (NativeMethods.IWMHeaderInfo3) metaEditor;

                //make call to get attribute length
                headerInfo.GetAttributeByIndex(i, ref streamNum, pwszName, ref pcchNameLen, out wAttribType, pbAttribValue, ref wAttribValueLen);
                //set byte array length
                pwszName = new StringBuilder(pcchNameLen, pcchNameLen);
                pbAttribValue = new byte[wAttribValueLen];
                //make call again, which will get value into correct-length byte array
                headerInfo.GetAttributeByIndex(i, ref streamNum, pwszName, ref pcchNameLen, out wAttribType, pbAttribValue, ref wAttribValueLen);

                return new MetaAttribute(pwszName.ToString(), ConvertAttributeToString(pbAttribValue, wAttribValueLen, wAttribType));
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected string GetAttributeNameByIndex(int index)
        {
            try
            {
                ushort i = (ushort) index;
                ushort streamNum = 0; //media stream to interrogate
                ushort wAttribValueLen = 0; //length of attribute (byte array)
                ushort pcchNameLen = 0;
                byte[] pbAttribValue = null; //value of attribute (as returned by method call)
                StringBuilder pwszName = null;
                NativeMethods.WMT_ATTR_DATATYPE wAttribType; //data type of attribute

                NativeMethods.IWMHeaderInfo3 headerInfo = (NativeMethods.IWMHeaderInfo3) metaEditor;

                //make call to get attribute length
                headerInfo.GetAttributeByIndex(i, ref streamNum, pwszName, ref pcchNameLen, out wAttribType, pbAttribValue, ref wAttribValueLen);
                //set byte array length
                pwszName = new StringBuilder(pcchNameLen, pcchNameLen);
                pbAttribValue = new byte[wAttribValueLen];
                //make call again, which will get value into correct-length byte array
                headerInfo.GetAttributeByIndex(i, ref streamNum, pwszName, ref pcchNameLen, out wAttribType, pbAttribValue, ref wAttribValueLen);

                return pwszName.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}