using System;
using System.Windows.Forms;
using DocExpiryApp.Controllers;

namespace DocExpiryApp.Views
{
    public class BaseView : Form
    {
        public Action<string> OnSuccess { get; set; }
        protected string this[string name] {get{return ConfigController.Instance[name];}}
        public BaseView() : base()
        {
            OnSuccess = delegate(string message){/* do nothing */};

            this.RightToLeftLayout = this["RightToLeftLayout"].Equals("yes");
            this.RightToLeft = this.RightToLeftLayout ? RightToLeft.Yes : RightToLeft.Inherit;
        }
    }
}
