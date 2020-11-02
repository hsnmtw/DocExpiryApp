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
    public class EnumerationListForm : BaseView
    {

        private Button btnNewEnumeration,btnDeleteEnumeration,btnEditEnumeration;
        private Label lblSearch;
        private TextBox txtSearch;
        

        private List<Enumeration> datasource;
        protected DataGridView dataGridView;
        public EnumerationListForm() : base()
        {
            this.Text = this["Enumerations"];
            this.Width = 375;
            this.Height = 400;
            this.MinimumSize = new Size(375,400);
            

            this.dataGridView = new DataGridView{
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                Width  = 340,
                Height = 280,
                Left   = 10,
                Top    = 70,
                AutoGenerateColumns = false,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToAddRows = false,
                AllowDrop = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true
            };

            new[]{
                "Id",
                "EnumerationName"
            }.ToList().ForEach(c => dataGridView.Columns.Add(c,this[c]));            
            

            this.btnNewEnumeration = new Button{
                Left = 10,
                Top  = 10,
                Width= 100,
                Text = this["New Enumeration"]
            };

            this.btnNewEnumeration = new Button{
                Left = 10,
                Top  = 10,
                Width= 100,
                Text = this["New"]
            };

            this.btnEditEnumeration = new Button{
                Left = 130,
                Top  = 10,
                Width= 100,
                Text = this["Edit"]
            };

            this.btnDeleteEnumeration = new Button{
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
            
            this.btnNewEnumeration.Click += new EventHandler(btnNewEnumeration_Click);
            this.btnDeleteEnumeration.Click += new EventHandler(btnDeleteEnumeration_Click);
            this.btnEditEnumeration.Click += new EventHandler(btnEditEnumeration_Click); 
            this.txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            this.dataGridView.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView_CellDoubleClick);
            this.dataGridView.MouseClick += new MouseEventHandler(dataGridView_MouseClick);
            
            this.contextMenuStrip.Items.Add(this["Edit"]).Click += new EventHandler(btnEditEnumeration_Click);
            this.contextMenuStrip.Items.Add(this["Delete"]).Click += new EventHandler(btnDeleteEnumeration_Click);
            

            //Adding Controls to Form
            new Control[]
            {
                lblSearch,
                txtSearch,
                dataGridView,                
                btnNewEnumeration,
                btnEditEnumeration,
                btnDeleteEnumeration,
            }
            .ToList().ForEach(x => Controls.Add(x));
            
        }

        protected void dataGridView_MouseClick(object sender, MouseEventArgs mea)
        {
            if(mea.Button == MouseButtons.Right){
                int row = dataGridView.HitTest(mea.X, mea.Y).RowIndex;
                if(row==-1) return;
                dataGridView.ClearSelection();
                dataGridView.Rows[row].Selected = true;
                contextMenuStrip.Show(dataGridView, new Point(mea.X, mea.Y));
            }
        }

        protected void dataGridView_CellDoubleClick(object sender,DataGridViewCellEventArgs ea)
        {
            btnEditEnumeration_Click(sender,null);
        }

        protected void requery()
        {
            datasource = new EnumerationController().SelectAll();
            this.dataGridView.DataSource = datasource;
        }

        protected void Form_Load(object sender, EventArgs eventArgs)
        {
            requery();
            this.dataGridView.Columns
                .Cast<DataGridViewColumn>()
                .ToList()
                .ForEach(x => x.DataPropertyName = x.Name);
            dataGridView.Refresh();
            this.Invalidate();
            this.Refresh();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs eventArgs)
        {
            var tx = sender as TextBox;
            dataGridView.DataSource = datasource.Where(x => x.EnumerationName.Contains(tx.Text)).ToList();
            dataGridView.Refresh();
        }
        protected void btnNewEnumeration_Click(object sender, EventArgs eventArgs)
        {
            new EnumerationForm(){
                OnSuccess = delegate(string message){
                    requery();
                }
            }.Show();
        }
        protected void btnEditEnumeration_Click(object sender, EventArgs eventArgs)
        {
            new EnumerationForm(){
                Model = GetSelectedModel(),
                OnSuccess = delegate(string message){
                    requery();
                }
            }.Show();
        }

        public Enumeration GetSelectedModel()
        {
            if(dataGridView.SelectedRows.Count==0) return null;
            var row = dataGridView.SelectedRows[0] as DataGridViewRow;
            return new Enumeration{
                Id = int.Parse(row.Cells["Id"].Value.ToString()),
                EnumerationName = row.Cells["EnumerationName"].Value.ToString()
            };
        }
        
        protected void btnDeleteEnumeration_Click(object sender, EventArgs eventArgs)
        {
            if(dataGridView.SelectedRows.Count==0) return;
            if(new EnumerationController().Delete(GetSelectedModel())){
                requery();
            }
        }
    }
}
