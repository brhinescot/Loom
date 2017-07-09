#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

#endregion

#region Using Directives

using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    /// <summary>
    /// Represents the different combinations of possible audit fields available for an 
    /// <see cref="DataRecord{TDataRecord}"/> audit property.
    /// </summary>
    [StructLayout(LayoutKind.Auto), Serializable]
    public struct AuditField : ISerializable
    {
        #region Type Fields

        private static readonly int isUser = BitVector32.CreateMask();
        private static readonly int isDate = BitVector32.CreateMask(isUser);
        private static readonly int isModified = BitVector32.CreateMask(isDate);
        private static readonly int isCreated = BitVector32.CreateMask(isModified);
        private static readonly int isDeleted = BitVector32.CreateMask(isCreated);

        public static readonly AuditField Default = new AuditField(false, false, false, false, false);

        #endregion

        #region Instance Fields

        [NonSerialized]
        private readonly BitVector32 auditFlags;

        #endregion

        #region Property Accessors

        /// <summary>
        /// Gets a value indicating if the associated field is any of the audit field types.
        /// </summary>
        public bool IsAny
        {
            get { return (auditFlags[isUser] || auditFlags[isDate] || auditFlags[isModified] || auditFlags[isCreated] || auditFlags[isDeleted]); }
        }

        /// <summary>
        /// Gets a value indicating if the associated field is not an audit field.
        /// </summary>
        public bool IsNone
        {
            get { return !(auditFlags[isUser] || auditFlags[isDate] || auditFlags[isModified] || auditFlags[isCreated] || auditFlags[isDeleted]); }
        }

        /// <summary>
        /// Gets a value indicating if the associated field is a user audit field.
        /// </summary>
        public bool IsUser
        {
            get { return auditFlags[isUser]; }
        }

        /// <summary>
        /// Gets a value indicating if the associated field is a date audit field.
        /// </summary>
        public bool IsDate
        {
            get { return auditFlags[isDate]; }
        }

        /// <summary>
        /// Gets a value indicating if the associated field is a modified audit field.
        /// </summary>
        public bool IsModified
        {
            get { return auditFlags[isModified]; }
        }

        public bool IsModifiedUser
        {
            get { return auditFlags[isModified] && auditFlags[isUser]; }
        }

        public bool IsModifiedDate
        {
            get { return auditFlags[isModified] && auditFlags[isDate]; }
        }

        /// <summary>
        /// Gets a value indicating if the associated field is a created audit field.
        /// </summary>
        public bool IsCreated
        {
            get { return auditFlags[isCreated]; }
        }

        public bool IsCreatedUser
        {
            get { return auditFlags[isCreated] && auditFlags[isUser]; }
        }

        public bool IsCreatedDate
        {
            get { return auditFlags[isCreated] && auditFlags[isDate]; }
        }

        /// <summary>
        /// Gets a value indicating if the associated field is a deleted audit field.
        /// </summary>
        public bool IsDeleted
        {
            get { return auditFlags[isDeleted]; }
        }

        #endregion

        #region .ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditField"/> class.
        /// </summary>
        /// <param name="isUser">If <see langword="true"/>, this is a user audit field.</param>
        /// <param name="isDate">If <see langword="true"/>, this is a date audit field.</param>
        /// <param name="isModified">If <see langword="true"/>, this is a modified audit field.</param>
        /// <param name="isCreated">If <see langword="true"/>, this is a created audit field.</param>
        /// <param name="isDeleted">If <see langword="true"/>, this is a deleted audit field.</param>
        public AuditField(bool isUser, bool isDate, bool isModified, bool isCreated, bool isDeleted)
        {
            auditFlags = new BitVector32(0);
            auditFlags[AuditField.isUser] = isUser;
            auditFlags[AuditField.isDate] = isDate;
            auditFlags[AuditField.isModified] = isModified;
            auditFlags[AuditField.isCreated] = isCreated;
            auditFlags[AuditField.isDeleted] = isDeleted;
        }

        #endregion

        #region Serialization

        private AuditField(SerializationInfo info, StreamingContext context)
        {
            auditFlags = new BitVector32(0);
            auditFlags[isUser] = info.GetBoolean("isUser");
            auditFlags[isDate] = info.GetBoolean("isDate");
            auditFlags[isModified] = info.GetBoolean("isModified");
            auditFlags[isCreated] = info.GetBoolean("isCreated");
            auditFlags[isDeleted] = info.GetBoolean("isDeleted");
        }

        ///<summary>
        ///Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> with the data needed to serialize the target object.
        ///</summary>
        ///
        ///<param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"></see>) for this serialization. </param>
        ///<param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> to populate with data. </param>
        ///<exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("isUser", auditFlags[isUser]);
            info.AddValue("isDate", auditFlags[isDate]);
            info.AddValue("isModified", auditFlags[isModified]);
            info.AddValue("isCreated", auditFlags[isCreated]);
            info.AddValue("isDeleted", auditFlags[isDeleted]);
        }

        #endregion
    }
}
