#region Using Directives

using System;
using System.Drawing;
using Loom.Collections;

#endregion

namespace Loom.Drawing
{
    //TODO [Brian,20140606] This class is not used. Test.
    public sealed class PropertyValueCollection : DataValueCollection<string>
    {
        public event EventHandler<EventArgs> ValueUpdated;

        protected override void OnValueSet(string key, object value)
        {
            base.OnValueSet(key, value);

            EventHandler<EventArgs> handler = ValueUpdated;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public Bitmap RetrieveBitmap(string key, int headerSize = 0)
        {
            ValidateKey(key);
            Argument.Assert.IsNotNegative(headerSize, nameof(headerSize));

            object result = Retrieve(key);
            if (result == null)
                return null;

            Bitmap bitmap = result as Bitmap;
            if (bitmap != null)
                return bitmap;

            byte[] imageBytes = result as byte[];

            if (imageBytes != null)
                bitmap = MemoryBitmap.FromBuffer(imageBytes, headerSize);

            return bitmap;
        }
    }
}