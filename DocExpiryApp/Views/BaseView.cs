using System;
using System.Linq;
using System.Windows.Forms;
using DocExpiryApp.Controllers;

namespace DocExpiryApp.Views
{
    public class BaseView : Form
    {
        public MenuStrip menuStrip {get;set;}
        public ContextMenuStrip contextMenuStrip {get;set;}
        public Action<string> OnSuccess { get; set; }
        protected string this[string name] {get{return ConfigController.Instance[name];}}
        public BaseView() : base()
        {
            OnSuccess = delegate(string message){/* do nothing */};

            this.RightToLeftLayout = this["RightToLeftLayout"].Equals("yes");
            this.RightToLeft = this.RightToLeftLayout ? RightToLeft.Yes : RightToLeft.Inherit;

            this.KeyDown += new KeyEventHandler(Form_KeyDown);
            this.KeyPreview = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.contextMenuStrip = new ContextMenuStrip();
            this.menuStrip = new MenuStrip();
            this.MainMenuStrip = this.menuStrip;
            new Control[]{
                menuStrip
            }.ToList().ForEach(c => Controls.Add(c));
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("\t");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
