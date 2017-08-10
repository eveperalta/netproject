using System;
using System.Drawing;
using System.IO;
using System.Net;
using CefSharp;

namespace PruebaWebBrowser
{
    public  class RenderProcessMessageHandler : IRenderProcessMessageHandler
    {

        public void OnFocusedNodeChanged(IWebBrowser browserControl, IBrowser browser, IFrame frame, IDomNode node)
        {
            throw new NotImplementedException();
        }

        // Wait for the underlying `Javascript Context` to be created, this is only called for the main frame.
        // If the page has no javascript, no context will be created.
        void IRenderProcessMessageHandler.OnContextCreated(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            const string script = "document.addEventListener('DOMContentLoaded', function(){  bound.initalizateAPP(); });" +
                "";

             frame.ExecuteJavaScriptAsync(script);
      
        }

     



    }

}