using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace DocExpiryApp.Views
{
    public class MainForm : BaseView
    {
        public MainForm() : base()
        {
            this.Text = this["Document Expiry Tracker Application"];
            this.Width = 575;
            this.Height = 400;
            this.MinimumSize = new Size(575,400);
            this.MaximizeBox = false;

            var file = this.menuStrip.Items.Add(this["File"]) as ToolStripMenuItem;
                file
                .DropDownItems.Add(this["Documents"])
                .Click += new EventHandler(menuDocuments_Click);

                file
                .DropDownItems.Add(this["Document Types"])
                .Click += new EventHandler(menuDocumentTypes_Click);

                file
                .DropDownItems.Add(this["Features"])
                .Click += new EventHandler(menuFeatures_Click);

                file
                .DropDownItems.Add(this["Enumerations"])
                .Click += new EventHandler(menuEnumerations_Click);

                file
                .DropDownItems.Add(this["Words"])
                .Click += new EventHandler(menuWords_Click);

        }
        protected void menuDocuments_Click(object sender, EventArgs eventArgs)
        {
            new DocumentListForm().Show();
        }
        protected void menuDocumentTypes_Click(object sender, EventArgs eventArgs)
        {
            new DocumentTypeListForm().Show();
        }
        protected void menuFeatures_Click(object sender, EventArgs eventArgs)
        {
            new FeatureListForm().Show();
        }
        protected void menuEnumerations_Click(object sender, EventArgs eventArgs)
        {
            new EnumerationListForm().Show();
        }
        protected void menuWords_Click(object sender, EventArgs eventArgs)
        {
            new WordListForm().Show();
        }
    }
}