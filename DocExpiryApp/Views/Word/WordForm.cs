using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DocExpiryApp.Controllers;
using DocExpiryApp.Models;

namespace DocExpiryApp.Views
{
    public class WordForm : BaseView
    {
        protected Label lblId, lblWordEnglish, lblWordArabic;
        protected TextBox txtId, txtWordEnglish, txtWordArabic;
        protected Button btnSave, btnCancel;
        public Word Model {
            get{ return new Word{
                Id = int.Parse("0"+txtId.Text),
                WordEnglish = txtWordEnglish.Text,
                WordArabic = txtWordArabic.Text
            };}
            set{
                var model = (value==null)? new Word():value;
                txtId.Text = model.Id.ToString();
                txtWordEnglish.Text = model.WordEnglish;
                txtWordEnglish.Text = model.WordArabic;
            }
        }
        public WordForm() : base()
        {
            Text = this["Words"];
            Size = new Size(300,250);
            MinimumSize = Size;
            MaximumSize = Size;
            AcceptButton = btnSave;
            CancelButton = btnCancel;
            MaximizeBox = false;


            lblId = new Label{
                Location = new Point(10,15),
                Text = this["Id"],
            };
            lblWordEnglish = new Label{
                Location = new Point(10,45),
                Text = this["WordEnglish"],
            };
            lblWordArabic = new Label{
                Location = new Point(10,75),
                Text = this["WordArabic"],
            };
            txtId = new TextBox{
                Location = new Point(110,10),
                ReadOnly = true,
                Enabled = false,
                Text = "0",
            };
            txtWordEnglish = new TextBox{
                Location = new Point(110,40)
            };
            txtWordArabic = new TextBox{
                Location = new Point(110,70)
            };
            btnSave = new Button{
                Location = new Point(10,100),
                Text = this["Save"]
            };
            btnCancel = new Button{
                Location = new Point(110,100),
                Text = this["Back"]
            };
            
            
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            //Adding Controls to Form
            new Control[]
            {
                lblId,
                txtId,
                lblWordEnglish,
                txtWordEnglish,
                lblWordArabic,
                txtWordArabic,
                btnSave,
                btnCancel,
            }
            .ToList().ForEach(x => Controls.Add(x));            
        }
        protected void btnSave_Click(object sender, EventArgs eventArgs)
        {
            bool result = new WordController().Save(Model);
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
