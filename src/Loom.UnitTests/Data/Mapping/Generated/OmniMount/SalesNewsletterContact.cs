#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;
using OmniMount.Portal;

#endregion

namespace OmniMount.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.NewsletterContact table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "NewsletterContact")]
    public class NewsletterContact : DataRecord<NewsletterContact>
    {
        private int _bad_Format;
        private int _bounce;
        private string _cC_Email;
        private int _click_1;
        private int _click_10;
        private int _click_2;
        private int _click_3;
        private int _click_4;
        private int _click_5;
        private int _click_6;
        private int _click_7;
        private int _click_8;
        private int _click_9;
        private int _contactId;
        private DateTime _date_Sent;
        private int _message_Sent;

        private int _newsletterId;
        private int _soft_Bounce;
        private int _subscribe;
        private int _unsubscribe;

        public NewsletterContact() { }
        protected NewsletterContact(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("NewsletterId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("NewsletterId", typeof(Newsletter), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int NewsletterId
        {
            get => _newsletterId;
            set
            {
                if (value == _newsletterId && IsPropertyDirty("NewsletterId"))
                    return;

                _newsletterId = value;
                MarkDirty("NewsletterId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ContactId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ContactId", typeof(Contact), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ContactId
        {
            get => _contactId;
            set
            {
                if (value == _contactId && IsPropertyDirty("ContactId"))
                    return;

                _contactId = value;
                MarkDirty("ContactId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CC_Email", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 200)]
        public string CC_Email
        {
            get => _cC_Email;
            set
            {
                if (value == _cC_Email && IsPropertyDirty("CC_Email"))
                    return;

                _cC_Email = value;
                MarkDirty("CC_Email");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Message_Sent", DbType.Int32, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((0))")]
        public int Message_Sent
        {
            get => _message_Sent;
            set
            {
                if (value == _message_Sent && IsPropertyDirty("Message_Sent"))
                    return;

                _message_Sent = value;
                MarkDirty("Message_Sent");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Bounce", DbType.Int32, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
        public int Bounce
        {
            get => _bounce;
            set
            {
                if (value == _bounce && IsPropertyDirty("Bounce"))
                    return;

                _bounce = value;
                MarkDirty("Bounce");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Soft_Bounce", DbType.Int32, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "((0))")]
        public int Soft_Bounce
        {
            get => _soft_Bounce;
            set
            {
                if (value == _soft_Bounce && IsPropertyDirty("Soft_Bounce"))
                    return;

                _soft_Bounce = value;
                MarkDirty("Soft_Bounce");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Unsubscribe", DbType.Int32, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "((0))")]
        public int Unsubscribe
        {
            get => _unsubscribe;
            set
            {
                if (value == _unsubscribe && IsPropertyDirty("Unsubscribe"))
                    return;

                _unsubscribe = value;
                MarkDirty("Unsubscribe");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Subscribe", DbType.Int32, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "((0))")]
        public int Subscribe
        {
            get => _subscribe;
            set
            {
                if (value == _subscribe && IsPropertyDirty("Subscribe"))
                    return;

                _subscribe = value;
                MarkDirty("Subscribe");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Bad_Format", DbType.Int32, ColumnProperties.None, Ordinal = 9, MaxLength = 0, DefaultValue = "((0))")]
        public int Bad_Format
        {
            get => _bad_Format;
            set
            {
                if (value == _bad_Format && IsPropertyDirty("Bad_Format"))
                    return;

                _bad_Format = value;
                MarkDirty("Bad_Format");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Date_Sent", DbType.DateTime, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime Date_Sent
        {
            get => _date_Sent;
            set
            {
                if (value == _date_Sent && IsPropertyDirty("Date_Sent"))
                    return;

                _date_Sent = value;
                MarkDirty("Date_Sent");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Click_1", DbType.Int32, ColumnProperties.None, Ordinal = 11, MaxLength = 0, DefaultValue = "((0))")]
        public int Click_1
        {
            get => _click_1;
            set
            {
                if (value == _click_1 && IsPropertyDirty("Click_1"))
                    return;

                _click_1 = value;
                MarkDirty("Click_1");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Click_2", DbType.Int32, ColumnProperties.None, Ordinal = 12, MaxLength = 0, DefaultValue = "((0))")]
        public int Click_2
        {
            get => _click_2;
            set
            {
                if (value == _click_2 && IsPropertyDirty("Click_2"))
                    return;

                _click_2 = value;
                MarkDirty("Click_2");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Click_3", DbType.Int32, ColumnProperties.None, Ordinal = 13, MaxLength = 0, DefaultValue = "((0))")]
        public int Click_3
        {
            get => _click_3;
            set
            {
                if (value == _click_3 && IsPropertyDirty("Click_3"))
                    return;

                _click_3 = value;
                MarkDirty("Click_3");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Click_4", DbType.Int32, ColumnProperties.None, Ordinal = 14, MaxLength = 0, DefaultValue = "((0))")]
        public int Click_4
        {
            get => _click_4;
            set
            {
                if (value == _click_4 && IsPropertyDirty("Click_4"))
                    return;

                _click_4 = value;
                MarkDirty("Click_4");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Click_5", DbType.Int32, ColumnProperties.None, Ordinal = 15, MaxLength = 0, DefaultValue = "((0))")]
        public int Click_5
        {
            get => _click_5;
            set
            {
                if (value == _click_5 && IsPropertyDirty("Click_5"))
                    return;

                _click_5 = value;
                MarkDirty("Click_5");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Click_6", DbType.Int32, ColumnProperties.None, Ordinal = 16, MaxLength = 0, DefaultValue = "((0))")]
        public int Click_6
        {
            get => _click_6;
            set
            {
                if (value == _click_6 && IsPropertyDirty("Click_6"))
                    return;

                _click_6 = value;
                MarkDirty("Click_6");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Click_7", DbType.Int32, ColumnProperties.None, Ordinal = 17, MaxLength = 0, DefaultValue = "((0))")]
        public int Click_7
        {
            get => _click_7;
            set
            {
                if (value == _click_7 && IsPropertyDirty("Click_7"))
                    return;

                _click_7 = value;
                MarkDirty("Click_7");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Click_8", DbType.Int32, ColumnProperties.None, Ordinal = 18, MaxLength = 0, DefaultValue = "((0))")]
        public int Click_8
        {
            get => _click_8;
            set
            {
                if (value == _click_8 && IsPropertyDirty("Click_8"))
                    return;

                _click_8 = value;
                MarkDirty("Click_8");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Click_9", DbType.Int32, ColumnProperties.None, Ordinal = 19, MaxLength = 0, DefaultValue = "((0))")]
        public int Click_9
        {
            get => _click_9;
            set
            {
                if (value == _click_9 && IsPropertyDirty("Click_9"))
                    return;

                _click_9 = value;
                MarkDirty("Click_9");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Click_10", DbType.Int32, ColumnProperties.None, Ordinal = 20, MaxLength = 0, DefaultValue = "((0))")]
        public int Click_10
        {
            get => _click_10;
            set
            {
                if (value == _click_10 && IsPropertyDirty("Click_10"))
                    return;

                _click_10 = value;
                MarkDirty("Click_10");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn NewsletterId => FetchColumn("NewsletterId");

            public static QueryColumn ContactId => FetchColumn("ContactId");

            public static QueryColumn CC_Email => FetchColumn("CC_Email");

            public static QueryColumn Message_Sent => FetchColumn("Message_Sent");

            public static QueryColumn Bounce => FetchColumn("Bounce");

            public static QueryColumn Soft_Bounce => FetchColumn("Soft_Bounce");

            public static QueryColumn Unsubscribe => FetchColumn("Unsubscribe");

            public static QueryColumn Subscribe => FetchColumn("Subscribe");

            public static QueryColumn Bad_Format => FetchColumn("Bad_Format");

            public static QueryColumn Date_Sent => FetchColumn("Date_Sent");

            public static QueryColumn Click_1 => FetchColumn("Click_1");

            public static QueryColumn Click_2 => FetchColumn("Click_2");

            public static QueryColumn Click_3 => FetchColumn("Click_3");

            public static QueryColumn Click_4 => FetchColumn("Click_4");

            public static QueryColumn Click_5 => FetchColumn("Click_5");

            public static QueryColumn Click_6 => FetchColumn("Click_6");

            public static QueryColumn Click_7 => FetchColumn("Click_7");

            public static QueryColumn Click_8 => FetchColumn("Click_8");

            public static QueryColumn Click_9 => FetchColumn("Click_9");

            public static QueryColumn Click_10 => FetchColumn("Click_10");
        }

        #endregion
    }
}