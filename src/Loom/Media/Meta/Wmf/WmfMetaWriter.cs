#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Loom.Media.Meta.Wmf
{
    public class WmfMetaWriter : WmfMetaBase
    {
        private readonly NativeMethods.IWMHeaderInfo3 headerInfo;

        public WmfMetaWriter(string fileName) : base(fileName)
        {
            headerInfo = (NativeMethods.IWMHeaderInfo3) MetaEditor;
        }

        public void SetAttributes(IEnumerable<MetaAttribute> attributes)
        {
            foreach (MetaAttribute attribute in attributes)
                SetAttribute(attribute.Name, attribute.Value);
        }

        public bool SetAttribute(MetaAttribute attribute)
        {
            return SetAttribute(attribute.Name, attribute.Value);
        }

        public bool SetAttribute(string name, string value)
        {
            try
            {
                int index = GetAttributeIndexByName(name);
                if (index == -1)
                    AddAttribute(name, value);

                return SetAttribute(index, value);
            }
            catch
            {
                return false;
            }
        }

        public bool SetAttribute(int index, string value)
        {
            try
            {
                const ushort streamNum = 0;
                NativeMethods.WMT_ATTR_DATATYPE wAttribType; //data type of attribute
                ushort langIndex; //index of the language to be associated with the new attribute. This is the index of the language in the language list for the file
                byte[] pbAttribValue = null; //value of attribute (as returned by method call)
                ushort wAttribValueLen = 0; //length of attribute (byte array)
                const StringBuilder pwszName = null;
                ushort pcchNameLen = 0;
                ushort wIndex = (ushort) index;

                //make call to get attribute length
                headerInfo.GetAttributeByIndexEx(streamNum, wIndex, pwszName, ref pcchNameLen, out wAttribType, out langIndex, pbAttribValue, ref wAttribValueLen);

                //convert string to byte array
                int vLen = value.Length * 2 + 2;
                byte[] pbNewValue = new byte[vLen];
                if (2 <= vLen)
                {
                    int strLoc = 0;
                    for (int i = 0; i < vLen - 2; i += 2)
                    {
                        pbNewValue[i] = Convert.ToByte(value[strLoc]);
                        strLoc++;
                    }
                    wAttribValueLen = (ushort) pbNewValue.Length;
                    //make call to modify the attribute
                    headerInfo.ModifyAttribute(streamNum, wIndex, wAttribType, langIndex, pbNewValue, wAttribValueLen);
                    return true; //success
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAttribute(string name)
        {
            try
            {
                int index = GetAttributeIndexByName(name);
                return index != -1 && DeleteAttribute(index);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAttribute(int index)
        {
            try
            {
                const ushort streamNum = 0;

                //make call to get attribute length
                headerInfo.DeleteAttribute(streamNum, (ushort) index);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Flush()
        {
            MetaEditor.Flush();
        }

        protected bool AddAttribute(string name, string value)
        {
            const ushort streamNum = 0;
            const ushort langIndex = 0;

            try
            {
                int vLen = value.Length * 2 + 2;
                byte[] pbNewValue = new byte[vLen];
                if (2 <= vLen)
                {
                    ushort wIndex;
                    int strLoc = 0;
                    for (int i = 0; i < vLen - 2; i += 2)
                    {
                        pbNewValue[i] = Convert.ToByte(value[strLoc]);
                        strLoc++;
                    }

                    ushort wAttribValueLen = (ushort) pbNewValue.Length;

                    headerInfo.AddAttribute(streamNum, name, out wIndex, NativeMethods.WMT_ATTR_DATATYPE.WMT_TYPE_STRING, langIndex, pbNewValue, wAttribValueLen);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private int GetAttributeIndexByName(string name)
        {
            int count = GetAttributeCount();
            for (int i = 0; i < count; i++)
            {
                string attrName = GetAttributeNameByIndex(i);
                if (attrName == name)
                    return i;
            }
            //Name not found
            return -1;
        }
    }
}