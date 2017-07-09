#region Using Directives

using System;
using System.Web;

#endregion

namespace Loom.Web.IO
{
    /*
     * Module is an IHttpModules that calls into Cache object to perform
     * output cache operations.
     * 
     * On ResolveRequestCache event it performs the lookup and either
     * (a) ignores the current request (from cache perspective) or
     * (b) serves the cached response and completes the request or
     * (c) captures the output using response filter
     * 
     * On UpdateRequestCache event the module inserts the new content
     * into cache
     * 
     * On EndResponse it performs cleanup
     * 
     * The cache functionality is implemented by Cache object.
     * Individual cached Urls are tracked via Tracker objects.
     * 
     */

    public sealed class DiskOutputCacheModule : IHttpModule
    {
        private HttpApplication context;
        private DiskOutputCacheTracker trackerCapturingResponse;

        #region IHttpModule Members

        void IHttpModule.Init(HttpApplication app)
        {
            context = app;
            // module's Init method could be called many times,
            // while Cache needs to be initialized only once.
            // EnsureInitilized takes care of that
            DiskOutputCache.EnsureInitialized();

            app.ResolveRequestCache += OnResolveRequestCache;
            app.UpdateRequestCache += OnUpdateRequestCache;
            app.EndRequest += OnEndRequest;
        }

        void IHttpModule.Dispose() { }

        #endregion

        private void OnResolveRequestCache(object sender, EventArgs e)
        {
            // start clean
            trackerCapturingResponse = null;

            DiskOutputCacheTracker tracker = DiskOutputCache.Lookup(context.Context);

            if (tracker == null)
                return;

            // try to send response or start capture
            // (use 'finally' because starting capture would lock)
            try { }
            finally
            {
                if (tracker.TrySendResponseOrStartResponseCapture(context.Response))
                    context.CompleteRequest();
                else
                    trackerCapturingResponse = tracker;
            }
        }

        private void OnUpdateRequestCache(object sender, EventArgs e)
        {
            if (trackerCapturingResponse != null)
            {
                // if capturing, finish the capture and save the file
                trackerCapturingResponse.FinishCaptureAndSaveResponse(context.Response);
                trackerCapturingResponse = null;
            }
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            if (trackerCapturingResponse != null)
                try
                {
                    trackerCapturingResponse.CancelCapture(context.Response);
                }
                finally
                {
                    trackerCapturingResponse = null;
                }
        }
    }
}