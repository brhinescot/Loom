namespace Loom.Web.UI.WebControls
{
    /// <summary>
    /// </summary>
    public class CloudItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CloudItem" /> class.
        /// </summary>
        public CloudItem() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CloudItem" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="weight">The weight.</param>
        public CloudItem(string text, double weight)
        {
            Text = text;
            Weight = weight;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CloudItem" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="href">The href.</param>
        public CloudItem(string text, double weight, string href) : this(text, weight)
        {
            Href = href;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CloudItem" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="href">The href.</param>
        /// <param name="title">The title.</param>
        public CloudItem(string text, double weight, string href, string title) : this(text, weight, href)
        {
            Title = title;
        }

        /// <summary>
        ///     Gets or sets the text for individual hyperlinks.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Get or sets the address of the HTML anchor.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        ///     Gets or sets the title (tooltip) of the HTML anchor.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the weight of the item.
        /// </summary>
        public double Weight { get; set; }
    }
}