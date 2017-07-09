#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    /// </summary>
    public class Cloud : CompositeDataBoundControl, IPostBackEventHandler
    {
        private static readonly object EventItemClick = new object();
        private static readonly string[] FontSizes = {"xx-small", "x-small", "small", "medium", "large", "x-large", "xx-large"};

        /// <summary>
        ///     Collection of CloudItems. <see cref="CloudItem" />
        /// </summary>
        [Themeable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [MergableProperty(false)]
        public Collection<CloudItem> Items { get; } = new Collection<CloudItem>();

        /// <summary>
        ///     Gets or sets the name of the data field that is bound to the Text property of an item.
        /// </summary>
        [Category("Data")]
        [TypeConverter(typeof(DataFieldConverter))]
        public string DataTextField
        {
            get
            {
                string val = ViewState["DataTextField"] as string;
                return val ?? string.Empty;
            }
            set
            {
                ViewState["DataTextField"] = value;

                if (Initialized)
                    RequiresDataBinding = true;
            }
        }

        /// <summary>
        ///     Gets or sets the format string for the Text property.
        /// </summary>
        [Category("Data")]
        public string DataTextFormatString
        {
            get
            {
                string val = ViewState["DataTextFormatString"] as string;

                if (val != null)
                    return val;

                return string.Empty;
            }
            set
            {
                ViewState["DataTextFormatString"] = value;

                if (Initialized)
                    RequiresDataBinding = true;
            }
        }

        /// <summary>
        ///     Gets or sets the data field which is bound to the Href property of an item.
        /// </summary>
        [Category("Data")]
        [TypeConverter(typeof(DataFieldConverter))]
        public string DataHrefField
        {
            get
            {
                string val = ViewState["DataHrefField"] as string;

                if (val != null)
                    return val;

                return string.Empty;
            }
            set
            {
                ViewState["DataHrefField"] = value;

                if (Initialized)
                    RequiresDataBinding = true;
            }
        }

        /// <summary>
        ///     Gets or sets the format string to format the Href property value.
        /// </summary>
        [Category("Data")]
        public string DataHrefFormatString
        {
            get
            {
                string val = ViewState["DataHrefFormatString"] as string;

                if (val != null)
                    return val;

                return string.Empty;
            }
            set
            {
                ViewState["DataHrefFormatString"] = value;

                if (Initialized)
                    RequiresDataBinding = true;
            }
        }

        /// <summary>
        ///     Gets or sets the data field which is bound to the Title property of an item.
        /// </summary>
        [Category("Data")]
        [TypeConverter(typeof(DataFieldConverter))]
        public string DataTitleField
        {
            get
            {
                string val = ViewState["DataTitleField"] as string;

                if (val != null)
                    return val;

                return string.Empty;
            }
            set
            {
                ViewState["DataTitleField"] = value;

                if (Initialized)
                    RequiresDataBinding = true;
            }
        }

        /// <summary>
        ///     The format string for the title(tooltip) of an item. {0} in this string is replaced with the
        ///     value of the field specified as the DataTitleField.
        /// </summary>
        [Category("Data")]
        public string DataTitleFormatString
        {
            get
            {
                string val = ViewState["DataTitleFormatString"] as string;

                if (val != null)
                    return val;

                return string.Empty;
            }
            set
            {
                ViewState["DataTitleFormatString"] = value;

                if (Initialized)
                    RequiresDataBinding = true;
            }
        }

        /// <summary>
        ///     The field from the Data Source where the weight of an item is to be obtained.
        /// </summary>
        [Category("Data")]
        [TypeConverter(typeof(DataFieldConverter))]
        public string DataWeightField
        {
            get
            {
                string val = ViewState["DataWeightField"] as string;

                if (val != null)
                    return val;

                return string.Empty;
            }
            set
            {
                ViewState["DataWeightField"] = value;

                if (Initialized)
                    RequiresDataBinding = true;
            }
        }

        /// <summary>
        ///     Gets or sets the prefix for CSS class names for individual items.
        /// </summary>
        [Category("Appearance")]
        public string ItemCssClassPrefix
        {
            get
            {
                string val = ViewState["ItemCssClassPrefix"] as string;

                if (val != null)
                    return val;

                return string.Empty;
            }
            set => ViewState["ItemCssClassPrefix"] = value;
        }

        /// <summary>
        ///     Gets the <see cref="HtmlTextWriterTag"></see> value that corresponds to this Web
        ///     server control. This property is used primarily by control developers.
        /// </summary>
        /// <value></value>
        /// <returns>One of the <see cref="HtmlTextWriterTag"></see> enumeration values.</returns>
        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        private IEnumerable<double> ItemWeights
        {
            get
            {
                foreach (CloudItem item in Items)
                    yield return item.Weight;
            }
        }

        #region IPostBackEventHandler Members

        /// <summary>
        ///     When implemented by a class, enables a server control to process an event raised
        ///     when a form is posted to the server.
        /// </summary>
        /// <param name="eventArgument">
        ///     A <see cref="string"></see> that represents an
        ///     optional event argument to be passed to the event handler.
        /// </param>
        public void RaisePostBackEvent(string eventArgument)
        {
            int selectedIndex;
            if (int.TryParse(eventArgument, out selectedIndex))
            {
                RequiresDataBinding = true;
                EnsureDataBound();

                if (selectedIndex >= 0 && selectedIndex < Items.Count)
                    OnItemClick(new CloudItemClickEventArgs(Items[selectedIndex]));
            }
        }

        #endregion

        /// <summary>
        /// </summary>
        public event EventHandler<CloudItemClickEventArgs> ItemClick
        {
            add => Events.AddHandler(EventItemClick, value);
            remove => Events.RemoveHandler(EventItemClick, value);
        }

        private static int NormalizeWeight(double weight, double mean, double stdDev)
        {
            double factor = weight - mean;

            if (factor != 0 && stdDev != 0)
                factor /= stdDev;

            return
                factor > 2 ? 7 : factor > 1 ? 6 : factor > 0.5 ? 5 : factor > -0.5 ? 4 : factor > -1 ? 3 : factor > -2 ? 2 : 1;
        }

        /// <summary>
        ///     Creates the control hierarchy that is used to render the composite data-bound control
        ///     based on the values from the specified data source.
        /// </summary>
        /// <param name="dataSource">
        ///     An <see cref="IEnumerable"></see> that contains
        ///     the values to bind to the control.
        /// </param>
        /// <param name="dataBinding">
        ///     true to indicate that the
        ///     <see cref="CreateChildControls"></see> is called
        ///     during data binding; otherwise, false.
        /// </param>
        /// <returns>
        ///     The number of items created by the <see cref="CreateChildControls" />.
        /// </returns>
        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            if (dataBinding && !DesignMode)
                CreateItemsFromData(dataSource);

            double mean;
            double stdDev = Statistics.CalculateStandardDeviation(ItemWeights, out mean);

            bool hasCssClassPrefix = !Compare.IsNullOrEmpty(ItemCssClassPrefix);
            int index = 0;

            foreach (CloudItem item in Items)
            {
                HtmlAnchor a = new HtmlAnchor();
                a.HRef = Compare.IsNullOrEmpty(item.Href) ? Page.ClientScript.GetPostBackClientHyperlink(this, index.ToString()) : item.Href;
                a.InnerText = item.Text;
                a.Title = item.Title;
                int normalWeight = NormalizeWeight(item.Weight, mean, stdDev);

                if (hasCssClassPrefix)
                    a.Attributes["class"] = ItemCssClassPrefix + normalWeight;
                else
                    a.Style.Add(HtmlTextWriterStyle.FontSize, FontSizes[normalWeight - 1]);

                Controls.Add(a);
                Controls.Add(new LiteralControl("&nbsp;"));
                index++;
            }

            if (DesignMode && Items.Count == 0)
            {
                HtmlAnchor a = new HtmlAnchor();
                a.HRef = "javascript:void(0)";
                a.InnerText = "Cloud";
                Controls.Add(a);
            }

            return Items.Count;
        }

        private void CreateItemsFromData(IEnumerable dataSource)
        {
            foreach (object data in dataSource)
            {
                CloudItem item = new CloudItem();

                if (Compare.IsNullOrEmpty(DataHrefField))
                    item.Href = Compare.IsNullOrEmpty(DataHrefFormatString) ? string.Empty : string.Format(CultureInfo.CurrentCulture, DataHrefFormatString, new[] {data});
                else
                    item.Href = DataBinder.Eval(data, DataHrefField, DataHrefFormatString);

                if (!Compare.IsNullOrEmpty(DataTextField))
                    item.Text = DataBinder.Eval(data, DataTextField, DataTextFormatString);

                if (!Compare.IsNullOrEmpty(DataTitleField))
                    item.Title = DataBinder.Eval(data, DataTitleField, DataTitleFormatString);

                if (!Compare.IsNullOrEmpty(DataWeightField))
                    item.Weight = Convert.ToDouble(DataBinder.GetPropertyValue(data, DataWeightField));

                Items.Add(item);
            }
        }

        /// <summary>
        ///     Raises the <see cref="ItemClick" /> event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="CloudItemClickEventArgs" /> instance containing
        ///     the event data.
        /// </param>
        protected virtual void OnItemClick(CloudItemClickEventArgs e)
        {
            EventHandler<CloudItemClickEventArgs> handler = (EventHandler<CloudItemClickEventArgs>) Events[EventItemClick];
            if (handler != null)
                handler(this, e);
        }
    }
}