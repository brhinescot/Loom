#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Web.UI;

#endregion

namespace Loom.Web.Localization
{
    /// <summary>
    ///     This class provides the Data Access to the database
    ///     for the DbResourceManager, Provider and design time
    ///     services. This class acts as a Business layer
    ///     and uses the SqlDataAccess DAL for its data access.
    ///     Dependencies:
    ///     DbResourceConfiguration   (holds and reads all config data from .Current)
    ///     SqlDataAccess             (provides a data access (DAL))
    /// </summary>
    internal sealed class DbResourceDataManager
    {
        /// <summary>
        ///     Error message that can be checked after a method complets
        ///     and returns a failure result.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Returns a specific set of resources for a given culture and 'resource set' which
        ///     in this case is just the virtual directory and culture.
        /// </summary>
        /// <param name="cultureName"></param>
        /// <param name="resourceSet"></param>
        /// <returns></returns>
        public IDictionary GetResourceSet(string cultureName, string resourceSet)
        {
            if (cultureName == null)
                cultureName = "";

            const string resourceFilter = " ResourceSet=@ResourceSet";

            Dictionary<string, object> hashTable = new Dictionary<string, object>();
            //Hashtable hashTable = new Hashtable();

            SqlDataAccess data = new SqlDataAccess(DbResourceConfiguration.Current.ConnectionString);
            DbDataReader reader;

            if (string.IsNullOrEmpty(cultureName))
                reader = data.ExecuteReader("select ResourceId,Value,Type,BinFile,TextFile,FileName from " + DbResourceConfiguration.Current.ResourceTableName + " where " + resourceFilter + " and (LocaleId is null OR LocaleId = '') order by ResourceId",
                    data.CreateParameter("@ResourceSet", resourceSet));
            else
                reader = data.ExecuteReader("select ResourceId,Value,Type,BinFile,TextFile,FileName from " + DbResourceConfiguration.Current.ResourceTableName + " where " + resourceFilter + " and LocaleId=@LocaleId order by ResourceId",
                    data.CreateParameter("@ResourceSet", resourceSet),
                    data.CreateParameter("@LocaleId", cultureName));

            if (reader == null)
                return hashTable;

            try
            {
                while (reader.Read())
                {
                    // Read the value into this
                    object resourceValue = reader["Value"] as string;

                    string resourceType = reader["Type"] as string;

                    if (!string.IsNullOrEmpty(resourceType))
                    {
                        // FileResource is a special type that is raw file data stored
                        // in the BinFile or TextFile data. Value contains
                        // filename and type data which is used to create: String, Bitmap or Byte[]
                        if (resourceType == "FileResource")
                        {
                            resourceValue = LoadFileResource(reader);
                        }
                        else
                        {
                            LosFormatter formatter = new LosFormatter();
                            resourceValue = formatter.Deserialize(resourceValue as string);
                        }
                    }
                    else
                    {
                        if (resourceValue == null)
                            resourceValue = "";
                    }

                    hashTable.Add(reader["ResourceId"].ToString(), resourceValue);
                }
            }
            catch { }
            finally
            {
                // close reader and connection
                reader.Close();
                data.CloseConnection();
            }

            return hashTable;
        }

        /// <summary>
        ///     Internal method used to parse the data in the database into a 'real' value.
        ///     Value field hold filename and type string
        ///     TextFile,BinFile hold the actual file content
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private object LoadFileResource(IDataRecord reader)
        {
            object value = null;

            try
            {
                string typeInfo = reader["Value"] as string;

                if (typeInfo == null || typeInfo.IndexOf("System.String") > -1)
                {
                    value = reader["TextFile"] as string;
                }
                else if (typeInfo.IndexOf("System.Drawing.Bitmap") > -1)
                {
                    MemoryStream ms = new MemoryStream(reader["BinFile"] as byte[]);
                    value = new Bitmap(ms);
                    ms.Close();
                }
                else
                {
                    value = reader["BinFile"] as byte[];
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = reader["ResourceKey"] + ": " + ex.Message;
            }

            return value;
        }
    }
}