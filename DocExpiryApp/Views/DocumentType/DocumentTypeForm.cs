using System;
using System.Linq;
using System.Windows.Forms;
using DocExpiryApp.Controllers;
using DocExpiryApp.Models;

namespace DocExpiryApp.Views
{
    public class DocumentTypeForm : BaseView
    {
        protected Label lblId, lblDocumentTypeName;
        protected TextBox txtId, txtDocumentTypeName;
        protected Button btnSave, btnCancel;
        
        public DocumentTypeForm() : base()
        {
            Text = this["Document Type"];
            Size = new System.Drawing.Size(300,150);
            MinimumSize = Size;
            AcceptButton = btnSave;
            CancelButton = btnCancel;
            StartPosition = FormStartPosition.CenterParent;

            lblId = new Label{
                Location = new System.Drawing.Point(10,15),
                Text = this["Id"],
            };
            lblDocumentTypeName = new Label{
                Location = new System.Drawing.Point(10,45),
                Text = this["DocumentTypeName"],
            };
            txtId = new TextBox{
                Location = new System.Drawing.Point(110,10),
                ReadOnly = true,
                Enabled = false,
                Text = "0",
            };
            txtDocumentTypeName = new TextBox{
                Location = new System.Drawing.Point(110,40)
            };
            btnSave = new Button{
                Location = new System.Drawing.Point(10,70),
                Text = this["Save"]
            };
            btnCancel = new Button{
                Location = new System.Drawing.Point(110,70),
                Text = this["Back"]
            };
            
            btnSave.Click += new EventHandler(btnSave_Click);
            
            //Adding Controls to Form
            new Control[]
            {
                lblId,
                txtId,
                lblDocumentTypeName,
                txtDocumentTypeName,
                btnSave,
                btnCancel,
            }
            .ToList().ForEach(x => Controls.Add(x));            
        }
        protected void btnSave_Click(object sender, EventArgs eventArgs)
        {
            new DocumentTypeController().Save(new DocumentType{
                Id = int.Parse(txtId.Text),
                DocumentTypeName = txtDocumentTypeName.Text 
            });
            Close();
        }
    }
}
