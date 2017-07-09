namespace Loom.Web.Portal.UI.Controls
{
    public enum AutoCompleteType
    {
        None,

        [EnumDescription("vCard.Business.City")] BusinessCity,

        [EnumDescription("vCard.Business.Country")] BusinessCountry,

        [EnumDescription("vCard.Business.Fax")] BusinessFax,

        [EnumDescription("vCard.Business.Phone")] BusinessPhone,

        [EnumDescription("vCard.Business.StreetAddress")] BusinessStreetAddress,

        [EnumDescription("vCard.Business.State")] BusinessState,

        [EnumDescription("vCard.Business.URL")] BusinessUrl,

        [EnumDescription("vCard.Business.Zipcode")] BusinessZipcode,

        [EnumDescription("vCard.Cellular")] Cellular,

        [EnumDescription("vCard.Company")] Company,

        [EnumDescription("vCard.Department")] Department,

        [EnumDescription("vCard.DisplayName")] DisplayName,

        [EnumDescription("vCard.Email")] Email,

        [EnumDescription("vCard.FirstName")] FirstName,

        [EnumDescription("vCard.Home.City")] HomeCity,

        [EnumDescription("vCard.Home.Country")] HomeCountry,

        [EnumDescription("vCard.Home.Fax")] HomeFax,

        [EnumDescription("vCard.Home.Phone")] HomePhone,

        [EnumDescription("vCard.Home.State")] HomeState,

        [EnumDescription("vCard.Home.StreetAddress")] HomeStreetAddress,

        [EnumDescription("vCard.Home.Zipcode")] HomeZipcode,

        [EnumDescription("vCard.Homepage")] Homepage,

        [EnumDescription("vCard.JobTitle")] JobTitle,

        [EnumDescription("vCard.LastName")] LastName,

        [EnumDescription("vCard.MiddleName")] MiddleName,

        [EnumDescription("vCard.Notes")] Notes,

        [EnumDescription("vCard.Office")] Office,

        [EnumDescription("vCard.Pager")] Pager
    }
}