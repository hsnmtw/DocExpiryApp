using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DocExpiryApp.Controllers;
using DocExpiryApp.Models;

namespace DocExpiryApp.Views
{
    public class EnumerationForm : BaseView
    {
        protected Label lblId, lblEnumerationName;
        protected TextBox txtId, txtEnumerationName;
        protected Button btnSave, btnCancel;
        public Enumeration Model {
            get{ return new Enumeration{
                Id = int.Parse("0"+txtId.Text),
                EnumerationName = txtEnumerationName.Text 
            };}
            set{
                var model = (value==null)? new Enumeration():value;
                txtId.Text = model.Id.ToString();
                txtEnumerationName.Text = model.EnumerationName;
            }
        }
        public EnumerationForm() : base()
        {
            Text = this["Enumerations"];
            Size = new System.Drawing.Size(300,150);
            MinimumSize = Size;
            MaximumSize = Size;
            AcceptButton = btnSave;
            CancelButton = btnCancel;
            MaximizeBox = false;


            lblId = new Label{
                Location = new Point(10,15),
                Text = this["Id"],
            };
            lblEnumerationName = new Label{
                Location = new Point(10,45),
                Text = this["EnumerationName"],
            };
            txtId = new TextBox{
                Location = new Point(110,10),
                ReadOnly = true,
                Enabled = false,
                Text = "0",
            };
            txtEnumerationName = new TextBox{
                Location = new Point(110,40)
            };
            btnSave = new Button{
                Location = new Point(10,70),
                Text = this["Save"]
            };
            btnCancel = new Button{
                Location = new Point(110,70),
                Text = this["Back"]
            };
            
            
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            //Adding Controls to Form
            new Control[]
            {
                lblId,
                txtId,
                lblEnumerationName,
                txtEnumerationName,
                btnSave,
                btnCancel,
            }
            .ToList().ForEach(x => Controls.Add(x));            
        }
        protected void btnSave_Click(object sender, EventArgs eventArgs)
        {
            bool result = new EnumerationController().Save(Model);
            LogController.Information(result);
            if(result){
                OnSuccess("insert/update successful");
            }
            Close();
        }

        protected void btnCancel_Click(object sender, EventArgs eventArgs)
        {
            Close();
        }
    }
}
