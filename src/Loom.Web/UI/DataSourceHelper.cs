#region Using Directives

using System;
using System.Collections;
using System.ComponentModel;

#endregion

namespace Loom.Web.UI
{
    public static class DataSourceHelper
    {
        /// <summary>
        ///     Returns a bindable datasource from an object.
        /// </summary>
        /// <param name="dataSource">The datasource to resolve.</param>
        /// <param name="dataMember">The datamember to look for.</param>
        /// <returns>
        ///     An <see cref="IEnumerable" /> object that can be databound,
        ///     or null if the object cannot be databound.
        /// </returns>
        public static IEnumerable GetResolvedDataSource(object dataSource, string dataMember = null)
        {
            return dataSource == null ? null : GetResolvedDataSourcePrivate(dataSource, dataMember);
        }

        private static IEnumerable GetResolvedDataSourcePrivate(object dataSource, string dataMember)
        {
            IListSource listDataSource = dataSource as IListSource;

            if (listDataSource != null)
            {
                IList bindableList = listDataSource.GetList();

                if (!listDataSource.ContainsListCollection)
                    return bindableList;

                if (bindableList != null && bindableList is ITypedList)
                    return GetEnumeratorFromTypedList(bindableList, dataMember);
            }

            return dataSource as IEnumerable;
        }

        /// <summary>
        ///     Returns a bindable object from an IList.
        /// </summary>
        /// <param name="bindableList">The list to resolve.</param>
        /// <param name="dataMember">The data member to look for.</param>
        /// <returns>An IEnnumerable object that can be databound.</returns>
        private static IEnumerable GetEnumeratorFromTypedList(IList bindableList, string dataMember)
        {
            ITypedList typedList = (ITypedList) bindableList;
            PropertyDescriptorCollection bindablePropertyCollection =
                typedList.GetItemProperties(new PropertyDescriptor[0]);

            if (bindablePropertyCollection != null && bindablePropertyCollection.Count != 0)
            {
                PropertyDescriptor propertyDescriptor = Compare.IsNullOrEmpty(dataMember)
                    ? bindablePropertyCollection[0]
                    : bindablePropertyCollection.Find(dataMember, true);

                if (propertyDescriptor != null)
                {
                    object dataList = bindableList[0];
                    IEnumerable returnSource = propertyDescriptor.GetValue(dataList) as IEnumerable;
                    if (returnSource != null)
                        return returnSource;
                }
                throw new ArgumentException("ListSource Missing DataMember");
            }
            throw new ArgumentException("ListSource Without DataMembers");
        }
    }
}