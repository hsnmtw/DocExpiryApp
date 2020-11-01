using System.Windows.Forms;
using DocExpiryApp.Controllers;

namespace DocExpiryApp.Views
{
    public class BaseView : Form
    {
        protected string this[string name] {get{return ConfigController.Instance[name];}}
        public BaseView() : base()
        {
            this.RightToLeftLayout = this["RightToLeftLayout"].Equals("yes");
            this.RightToLeft = this.RightToLeftLayout ? RightToLeft.Yes : RightToLeft.Inherit;
        }
    }
}
