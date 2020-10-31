using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetaPoco;
using DocExpiryApp.Models;
using DocExpiryApp.Controllers;

namespace DocExpiryApp.Views
{
    public class DocumentListForm : Form
    {
        protected string this[string name] {get{return ConfigController.Instance[name];}}

        private Button btnDocumentAttribute, btnDocumentType, btnNewDocument;
        private Label lblSearch;
        private TextBox txtSearch;
        private List<Document> documents;
        protected IDatabase db;
        protected DataGridView dataGridView;
        public DocumentListForm() : base()
        {
            this.db = new Database("db");
            this.RightToLeftLayout = this["RightToLeftLayout"].Equals("yes");
            this.RightToLeft = this.RightToLeftLayout ? RightToLeft.Yes : RightToLeft.Inherit;
            this.Text = this["Document Expiry Tracker Application"];
            this.Width = 575;
            this.Height = 400;
            this.MinimumSize = new Size(375,400);
            
            this.dataGridView = new DataGridView{
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                Width  = 540,
                Height = 280,
                Left   = 10,
                Top    = 70,
                AutoGenerateColumns = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToAddRows = false,
                AllowDrop = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true
            };

            this.btnNewDocument = new Button{
                Left = 10,
                Top  = 10,
                Width= 100,
                Text = this["New Document"]
            };

            this.btnDocumentAttribute = new Button{
                Left = 130,
                Top  = 10,
                Width= 100,
                Text = this["Document Attributes"]
            };
            
            this.btnDocumentType = new Button{
                Left = 250,
                Top  = 10,
                Width= 100,
                Text = this["Document Types"]
            };

            this.lblSearch = new Label{
                Text = this["Search"],
                Left = 10,
                Top  = 45,
                Width= 50 
            };

            this.txtSearch = new TextBox{
                Left = 70,
                Top = 40,
                Width = 480,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
            };

            //Event Handlers
            this.Load += new EventHandler(Form_Load);
            this.btnNewDocument.Click += new EventHandler(btnNewDocument_Click);
            this.btnDocumentType.Click += new EventHandler(btnDocumentType_Click);
            this.btnDocumentAttribute.Click += new EventHandler(btnDocumentAttribute_Click); 
            this.txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);

            //Adding Controls to Form
            new Control[]
            {
                lblSearch,
                txtSearch,
                dataGridView,
                btnNewDocument,
                btnDocumentAttribute,
                btnDocumentType,
            }
            .ToList().ForEach(x => Controls.Add(x));
            
        }

        protected void Form_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                documents = db.Fetch<Document>(this["sql.documents.query"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,this["Error while connecting to Database"],MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            this.dataGridView.DataSource = documents;
            this.dataGridView.Columns
                .Cast<DataGridViewColumn>()
                .ToList()
                .ForEach(x => {x.HeaderText = this[x.HeaderText]; x.Name = x.HeaderText;});
            dataGridView.Refresh();
            this.Invalidate();
            this.Refresh();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs eventArgs)
        {
            var tx = sender as TextBox;
            dataGridView.DataSource = documents.Where(x => x.DocumentNumber.Contains(tx.Text)).ToList();
            dataGridView.Refresh();
        }
        protected void btnDocumentType_Click(object sender, EventArgs eventArgs)
        {
            new DocumentTypeListForm().Show();
        }
        protected void btnNewDocument_Click(object sender, EventArgs eventArgs)
        {

        }
        protected void btnDocumentAttribute_Click(object sender, EventArgs eventArgs)
        {

        }
    }
}
