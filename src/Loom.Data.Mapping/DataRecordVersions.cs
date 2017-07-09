#region Using Directives

using System.Collections.Generic;
using Loom.Dynamic;

#endregion

namespace Loom.Data.Mapping
{
    internal class DataRecordVersions<T> where T : DataRecord<T>, new()
    {
        private readonly DataRecord<T> item;
        private readonly DynamicProperties<T> properties;
        private Stack<Dictionary<string, object>> history;

        public DataRecordVersions(DynamicProperties<T> properties, DataRecord<T> item)
        {
            this.properties = properties;
            this.item = item;
        }

        public int HistorySteps => history?.Count ?? 0;

        public int Version()
        {
            if (history == null)
                history = new Stack<Dictionary<string, object>>();

            Dictionary<string, object> version = new Dictionary<string, object>();
            foreach (DynamicProperty<T> property in properties)
                version.Add(property.AttributeName, property.InvokeGetterOn((T) item));

            history.Push(version);
            return history.Count;
        }

        public bool Undo(int steps)
        {
            if (history == null)
                return false;

            if (history.Count < steps || steps < 0)
                return false;

            for (int i = 0; i < steps - 1; i++)
                history.Pop();

            Dictionary<string, object> version = history.Pop();
            foreach (KeyValuePair<string, object> pair in version)
                properties[pair.Key].InvokeSetterOn((T) item, pair.Value);

            return true;
        }

        public void ClearHistory()
        {
            if (history == null)
                return;

            history.Clear();
            history = null;
        }
    }
}