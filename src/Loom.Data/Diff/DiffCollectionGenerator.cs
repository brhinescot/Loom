#region Using Directives

using System;
using System.Collections.Generic;
using Loom.Collections;

#endregion

namespace Loom.Data.Diff
{
    public class DiffCollectionGenerator<T>
    {
        private readonly DiffGenerator<T> diffGenerator;
        private readonly string[] keys;

        private string addedName;
        private string missingName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DiffCollectionGenerator{T}" /> class.
        /// </summary>
        /// <param name="keyProperty"></param>
        public DiffCollectionGenerator(string keyProperty)
        {
            keys = new[] {keyProperty};
            diffGenerator = new DiffGenerator<T>(true, keys);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DiffCollectionGenerator{T}" /> class.
        /// </summary>
        /// <param name="keyProperty1"></param>
        /// <param name="keyProperty2"></param>
        public DiffCollectionGenerator(string keyProperty1, string keyProperty2)
        {
            keys = new[] {keyProperty1, keyProperty2};
            diffGenerator = new DiffGenerator<T>(true, keys);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DiffCollectionGenerator{T}" /> class.
        /// </summary>
        /// <param name="keyProperty1"></param>
        /// <param name="keyProperty2"></param>
        /// <param name="keyProperty3"></param>
        public DiffCollectionGenerator(string keyProperty1, string keyProperty2, string keyProperty3)
        {
            keys = new[] {keyProperty1, keyProperty2, keyProperty3};
            diffGenerator = new DiffGenerator<T>(true, keys);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DiffCollectionGenerator{T}" /> class.
        /// </summary>
        /// <param name="keyProperties"></param>
        public DiffCollectionGenerator(params string[] keyProperties)
        {
            Argument.Assert.IsNotNull(keyProperties, nameof(keyProperties));
            Argument.Assert.IsNotZeroLength(keyProperties, nameof(keyProperties));

            keys = new string[keyProperties.Length];
            keyProperties.CopyTo(keys, 0);
            diffGenerator = new DiffGenerator<T>(true, keys);
        }

        /// <summary>
        ///     The string to use in the <see cref="DiffEntry.Name" /> property
        ///     of a <see cref="DiffEntry" /> when an item is missing
        ///     from the working collection.
        /// </summary>
        public string MissingName
        {
            get => missingName ?? (missingName = "Missing");
            set => missingName = value;
        }

        /// <summary>
        ///     The string to use in the <see cref="DiffEntry.Name" /> property
        ///     of a <see cref="DiffEntry" /> when an item is missing
        ///     from the baseline collection.
        /// </summary>
        public string AddedName
        {
            get => addedName ?? (addedName = "Added");
            set => addedName = value;
        }

        public DiffCollection<T> GenerateDiffs(ICollection<T> baselineCollection, ICollection<T> workingCollection)
        {
            AutoKeyDictionary<T> baselineItems = new AutoKeyDictionary<T>(baselineCollection, keys);
            AutoKeyDictionary<T> workingItems = new AutoKeyDictionary<T>(workingCollection, keys);

            DiffCollection<T> diffEntryCollection = new DiffCollection<T>();
            AddItemsChangedInOrMissingFromWorkingCollection(workingItems, baselineItems, diffEntryCollection);
            AddItemsMissingFromBaselineCollection(workingItems, baselineItems, diffEntryCollection);

            return diffEntryCollection;
        }

        private void AddItemsChangedInOrMissingFromWorkingCollection(IDictionary<int, T> workingItems, IDictionary<int, T> baselineItems, ICollection<Diff<T>> diffCollection)
        {
            foreach (int baselineKey in baselineItems.Keys)
            {
                T value;
                if (workingItems.TryGetValue(baselineKey, out value))
                    CreateDiff(baselineItems[baselineKey], value, diffCollection);
                else
                    AddEntry(diffCollection, DiffType.Missing);
            }
        }

        private void AddItemsMissingFromBaselineCollection(IDictionary<int, T> workingItems, IDictionary<int, T> baselineItems, ICollection<Diff<T>> diffCollection)
        {
            foreach (int workingKey in workingItems.Keys)
                if (!baselineItems.ContainsKey(workingKey))
                    AddEntry(diffCollection, DiffType.Added);
        }

        private void CreateDiff(T baselineItem, T workingItem, ICollection<Diff<T>> diffCollection)
        {
            Diff<T> diff = diffGenerator.Generate(baselineItem, workingItem);
            if (diff.Entries.Count == 0)
                return;

            diffCollection.Add(diff);
        }

        private void AddEntry(ICollection<Diff<T>> diffCollection, DiffType diffType)
        {
            DiffEntry entry;
            switch (diffType)
            {
                case DiffType.Unchanged:
                    throw new NotSupportedException("DiffType.Unchanged is not supported in this method.");
                case DiffType.Changed:
                    throw new NotSupportedException("DiffType.Changed is not supported in this method.");
                case DiffType.Added:
                    entry = new DiffEntry(AddedName, "<NONE>", "<NONE>", diffType);
                    break;
                case DiffType.Missing:
                    entry = new DiffEntry(MissingName, "<NONE>", "<NONE>", diffType);
                    break;
                default:
                    throw new NotSupportedException("Unknown DiffType is not supported in this method.");
            }

            Diff<T> diff = DiffGenerator<T>.CreateEmpty();
            diff.AddEntry(entry);
            diffCollection.Add(diff);
        }
    }
}