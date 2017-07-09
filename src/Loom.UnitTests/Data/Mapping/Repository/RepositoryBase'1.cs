#region Using Directives

using System.Collections.Generic;
using AdventureWorks.HumanResources;

#endregion

namespace Loom.Data.Mapping.Repository
{
    public class RepositoryBase<T> where T : DataRecord<T>, new()
    {
        public RepositoryBase()
        {
            Entity = new T();
        }

        public T Entity { get; set; }

        protected virtual T First()
        {
            return new T();
        }

        protected virtual T FirstOrDefault()
        {
            return new T();
        }

        protected virtual List<T> List()
        {
            return new List<T>();
        }

        protected virtual List<T> All()
        {
            return new List<T>();
        }
    }

    public class EmployeeRepository : RepositoryBase<Employee>
    {
        public Employee Get()
        {
            Entity.Gender = "M";

            return First();
        }

        public void Test()
        {
            Employee employee = Get();
        }
    }
}