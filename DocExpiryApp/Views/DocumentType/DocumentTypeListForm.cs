using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DocExpiryApp.Models;
using DocExpiryApp.Controllers;

namespace DocExpiryApp.Views
{
    public class DocumentTypeListForm : BaseView
    {

        private Button btnNewDocumentType,btnDeleteDocumentType,btnEditDocumentType;
        private Label lblSearch;
        private TextBox txtSearch;
        private List<DocumentType> datasource;
        protected DataGridView dataGridView;
        public DocumentTypeListForm() : base()
        {
            this.RightToLeftLayout = this["RightToLeftLayout"].Equals("yes");
            this.RightToLeft = this.RightToLeftLayout ? RightToLeft.Yes : RightToLeft.Inherit;
            this.Text = this["Document Types"];
            this.Width = 375;
            this.Height = 400;
            this.MinimumSize = new Size(375,400);
            
            this.dataGridView = new DataGridView{
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                Width  = 340,
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

            this.btnNewDocumentType = new Button{
                Left = 10,
                Top  = 10,
                Width= 100,
                Text = this["New Document Type"]
            };

            this.btnNewDocumentType = new Button{
                Left = 10,
                Top  = 10,
                Width= 100,
                Text = this["New"]
            };

            this.btnEditDocumentType = new Button{
                Left = 130,
                Top  = 10,
                Width= 100,
                Text = this["Edit"]
            };

            this.btnDeleteDocumentType = new Button{
                Left = 250,
                Top  = 10,
                Width= 100,
                Text = this["Delete"]
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
                Width = 280,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
            };

            //Event Handlers
            this.Load += new EventHandler(Form_Load);
            this.btnNewDocumentType.Click += new EventHandler(btnNewDocumentType_Click);
            this.btnDeleteDocumentType.Click += new EventHandler(btnDeleteDocumentType_Click);
            this.btnEditDocumentType.Click += new EventHandler(btnEditDocumentType_Click); 
            this.txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);

            //Adding Controls to Form
            new Control[]
            {
                lblSearch,
                txtSearch,
                dataGridView,                
                btnNewDocumentType,
                btnEditDocumentType,
                btnDeleteDocumentType,
            }
            .ToList().ForEach(x => Controls.Add(x));
            
        }

        protected void Form_Load(object sender, EventArgs eventArgs)
        {
            datasource = new DocumentTypeController().SelectAll();
            this.dataGridView.DataSource = datasource;
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
            dataGridView.DataSource = datasource.Where(x => x.DocumentTypeName.Contains(tx.Text)).ToList();
            dataGridView.Refresh();
        }
        protected void btnNewDocumentType_Click(object sender, EventArgs eventArgs)
        {
            new DocumentTypeForm().Show();
        }
        protected void btnEditDocumentType_Click(object sender, EventArgs eventArgs)
        {

        }
        protected void btnDeleteDocumentType_Click(object sender, EventArgs eventArgs)
        {

        }
    }
}
