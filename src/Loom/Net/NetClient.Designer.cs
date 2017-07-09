using System.ComponentModel;
using System.Net;

namespace Loom.Net
{
    partial class NetClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        private WebClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetClient"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public NetClient(IContainer container)
        {
            Argument.Assert.IsNotNull(container, "container");

            container.Add(this);
            InitializeComponent();
        }


        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                
                    if(client != null)
                        client.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            client = new WebClient();
            client.DownloadProgressChanged += HandleDownloadProgressChanged;
            client.DownloadFileCompleted += HandleDownloadFileCompleted;
            components.Add(client);
        }

        #endregion
    }
}

