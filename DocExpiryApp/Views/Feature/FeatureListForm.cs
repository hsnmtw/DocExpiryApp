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
    public class FeatureListForm : BaseView
    {

        private Button btnNewFeature,btnDeleteFeature,btnEditFeature;
        private Label lblSearch;
        private TextBox txtSearch;
        

        private List<Feature> datasource;
        protected DataGridView dataGridView;
        public FeatureListForm() : base()
        {
            this.Text = this["Features"];
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
                "FeatureName"
            }.ToList().ForEach(c => dataGridView.Columns.Add(c,this[c]));            
            

            this.btnNewFeature = new Button{
                Left = 10,
                Top  = 10,
                Width= 100,
                Text = this["New Feature"]
            };

            this.btnNewFeature = new Button{
                Left = 10,
                Top  = 10,
                Width= 100,
                Text = this["New"]
            };

            this.btnEditFeature = new Button{
                Left = 130,
                Top  = 10,
                Width= 100,
                Text = this["Edit"]
            };

            this.btnDeleteFeature = new Button{
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
            
            this.btnNewFeature.Click += new EventHandler(btnNewFeature_Click);
            this.btnDeleteFeature.Click += new EventHandler(btnDeleteFeature_Click);
            this.btnEditFeature.Click += new EventHandler(btnEditFeature_Click); 
            this.txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            this.dataGridView.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView_CellDoubleClick);
            this.dataGridView.MouseClick += new MouseEventHandler(dataGridView_MouseClick);
            
            this.contextMenuStrip.Items.Add(this["Edit"]).Click += new EventHandler(btnEditFeature_Click);
            this.contextMenuStrip.Items.Add(this["Delete"]).Click += new EventHandler(btnDeleteFeature_Click);
            

            //Adding Controls to Form
            new Control[]
            {
                lblSearch,
                txtSearch,
                dataGridView,                
                btnNewFeature,
                btnEditFeature,
                btnDeleteFeature,
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
            btnEditFeature_Click(sender,null);
        }

        protected void requery()
        {
            datasource = new FeatureController().SelectAll();
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
            dataGridView.DataSource = datasource.Where(x => x.FeatureName.Contains(tx.Text)).ToList();
            dataGridView.Refresh();
        }
        protected void btnNewFeature_Click(object sender, EventArgs eventArgs)
        {
            new FeatureForm(){
                OnSuccess = delegate(string message){
                    requery();
                }
            }.Show();
        }
        protected void btnEditFeature_Click(object sender, EventArgs eventArgs)
        {
            new FeatureForm(){
                Model = GetSelectedModel(),
                OnSuccess = delegate(string message){
                    requery();
                }
            }.Show();
        }

        public Feature GetSelectedModel()
        {
            if(dataGridView.SelectedRows.Count==0) return null;
            var row = dataGridView.SelectedRows[0] as DataGridViewRow;
            return new Feature{
                Id = int.Parse(row.Cells["Id"].Value.ToString()),
                FeatureName = row.Cells["FeatureName"].Value.ToString()
            };
        }
        
        protected void btnDeleteFeature_Click(object sender, EventArgs eventArgs)
        {
            if(dataGridView.SelectedRows.Count==0) return;
            if(new FeatureController().Delete(GetSelectedModel())){
                requery();
            }
        }
    }
}
