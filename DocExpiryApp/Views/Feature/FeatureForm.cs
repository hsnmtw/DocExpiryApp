using System;
using System.Linq;
using System.Windows.Forms;
using DocExpiryApp.Controllers;
using DocExpiryApp.Models;

namespace DocExpiryApp.Views
{
    public class FeatureForm : BaseView
    {
        protected Label lblId, lblFeatureName;
        protected TextBox txtId, txtFeatureName;
        protected Button btnSave, btnCancel;
        public Feature Model {
            get{ return new Feature{
                Id = int.Parse("0"+txtId.Text),
                FeatureName = txtFeatureName.Text 
            };}
            set{
                var model = (value==null)? new Feature():value;
                txtId.Text = model.Id.ToString();
                txtFeatureName.Text = model.FeatureName;
            }
        }
        public FeatureForm() : base()
        {
            Text = this["Features"];
            Size = new System.Drawing.Size(300,150);
            MinimumSize = Size;
            MaximumSize = Size;
            AcceptButton = btnSave;
            CancelButton = btnCancel;
            MaximizeBox = false;


            lblId = new Label{
                Location = new System.Drawing.Point(10,15),
                Text = this["Id"],
            };
            lblFeatureName = new Label{
                Location = new System.Drawing.Point(10,45),
                Text = this["FeatureName"],
            };
            txtId = new TextBox{
                Location = new System.Drawing.Point(110,10),
                ReadOnly = true,
                Enabled = false,
                Text = "0",
            };
            txtFeatureName = new TextBox{
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
            btnCancel.Click += new EventHandler(btnCancel_Click);
            //Adding Controls to Form
            new Control[]
            {
                lblId,
                txtId,
                lblFeatureName,
                txtFeatureName,
                btnSave,
                btnCancel,
            }
            .ToList().ForEach(x => Controls.Add(x));            
        }
        protected void btnSave_Click(object sender, EventArgs eventArgs)
        {
            bool result = new FeatureController().Save(Model);
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
