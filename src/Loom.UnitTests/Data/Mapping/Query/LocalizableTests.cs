#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom.Data.Mapping.Query
{
    [TestFixture]
    [Ignore]
    public class LocalizableTests : ActiveDataSharedSessionTestBase
    {
//        [Test]
//        public void SelectAllColumnsDefaultLocale()
//        {
//            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
//
//            const string expected =
//                "SELECT _t0.[Description], _t0.[MountId], _t0.[Name] " +
//                "FROM [Test].[Mount] _t0";
//
//            var query = OmniMountSession.Mounts.SelectAll();
//
//            AssertCommandTextSame(expected, query);
//        }
//
//        [Test]
//        public void SelectAllColumns()
//        {
//            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de-DE");
//
//            const string expected =
//                "SELECT ISNULL(_t0.[Description], _t1.[Description]) AS Description, _t1.[MountId], ISNULL(_t0.[Name], _t1.[Name]) AS Name " + 
//                "FROM [Test].[Mount] _t1 " + 
//                "LEFT JOIN [Test].[MountTranslations] _t0 ON _t1.[MountId] = _t0.[MountId] " + 
//                "WHERE (_t0.[Locale] = @_p0 OR _t0.[Locale] IS NULL)";
//
//            var query = Session.Mounts.SelectAll();
//
//            AssertParamsSame(query, "de-DE");
//            AssertCommandTextSame(expected, query);
//        }
//
//        [Test]
//        public void LocalizedAndNonLocalizedColumns()
//        {
//            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de-DE");
//
//            const string expected =
//                "SELECT _t0.[MountId], ISNULL(_t1.[Description], _t0.[Description]) AS Description, ISNULL(_t1.[Name], _t0.[Name]) AS Name " +
//                "FROM [Test].[Mount] _t0 "+
//                "LEFT JOIN [Test].[MountTranslations] _t1 ON _t0.[MountId] = _t1.[MountId] " +
//                "WHERE (_t1.[Locale] = @_p0 OR _t1.[Locale] IS NULL)";
//
//            var query = Session.Mounts.Select(Mount.Columns.MountId, Mount.Columns.Description, Mount.Columns.Name);
//
//            AssertCommandTextSame(expected, query);
//            AssertParamsSame(query, "de-DE");
//        }
//
//        [Test]
//        public void LocalizedOnlyColumns()
//        {
//            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de-DE");
//
//            const string expected =
//                "SELECT ISNULL(_t0.[Description], _t1.[Description]) AS Description, ISNULL(_t0.[Name], _t1.[Name]) AS Name " + 
//                "FROM [Test].[Mount] _t1 " + 
//                "LEFT JOIN [Test].[MountTranslations] _t0 ON _t1.[MountId] = _t0.[MountId] " + 
//                "WHERE (_t0.[Locale] = @_p0 OR _t0.[Locale] IS NULL)";
//
//            var query = Session.Mounts.Select(Mount.Columns.Description, Mount.Columns.Name);
//
//            AssertCommandTextSame(expected, query);
//            AssertParamsSame(query, "de-DE");
//        }
//
//        [Test, ThreadedRepeat(1)]
//        public void CurrentUiCultureLocale()
//        {
//            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de-DE");
//
//            const string expected =
//                "SELECT _t0.[MountId], ISNULL(_t1.[Description], _t0.[Description]) AS Description, ISNULL(_t1.[Name], _t0.[Name]) AS Name " + 
//                "FROM [Test].[Mount] _t0 " + 
//                "LEFT JOIN [Test].[MountTranslations] _t1 ON _t0.[MountId] = _t1.[MountId] " + 
//                "WHERE (_t1.[Locale] = @_p0 OR _t1.[Locale] IS NULL) AND _t0.[MountId] > @_p1";
//
//            var query = Session.Mounts.Select(Mount.Columns.MountId, Mount.Columns.Description, Mount.Columns.Name);
//            query.Where(Mount.Columns.MountId > 0);
//
//            AssertCommandTextSame(expected, query);
//            AssertParamsSame(query, "de-DE", 0);
//        }
    }
}