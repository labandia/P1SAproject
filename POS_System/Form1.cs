using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public void DisplayProducts()
        {
            var getprod = POS_Services.GetProductList();

            dataGridView2.DataSource = getprod; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupGrid();

            flowLayoutPanel1.Controls.Clear();
            var getprod = POS_Services.GetProductList();
            //DisplayProducts();


            foreach (var product in getprod)
            {
                flowLayoutPanel1.Controls.Add(CreateProductCard(product));
            }
        }


        private Panel CreateProductCard(Product product)
        {
            Panel card = new Panel
            {
                Width = 220,
                Height = 150,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Tag = product, // store whole object
                Cursor = Cursors.Hand
            };

            Label lblName = new Label
            {
                Text = product.Name,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(200, 25)
            };

            Label lblDesc = new Label
            {
                Text = product.Description,
                Location = new Point(10, 40),
                Size = new Size(200, 40)
            };

            Label lblPrice = new Label
            {
                Text = $"₱ {product.Price:N2}",
                ForeColor = Color.Green,
                Location = new Point(10, 90),
                Size = new Size(200, 20)
            };

            card.Controls.Add(lblName);
            card.Controls.Add(lblDesc);
            card.Controls.Add(lblPrice);

            // Make entire card clickable
            card.Click += Card_Click;

            // Also allow child controls to trigger same event
            foreach (Control ctrl in card.Controls)
            {
                ctrl.Click += Card_Click;
            }

            return card;
        }


        private void Card_Click(object sender, EventArgs e)
        {
            Control clickedControl = sender as Control;

            // If user clicked label, get parent panel
            Panel card = clickedControl as Panel ?? clickedControl.Parent as Panel;

            if (card?.Tag is Product selectedProduct)
            {
                SetProductToGrid(selectedProduct);
            }
        }
        BindingList<Product> selectedProducts = new BindingList<Product>();

        private void SetProductToGrid(Product product)
        {
            // Prevent duplicates (optional)
            if (selectedProducts.Any(p => p.Id == product.Id))
                return;

            selectedProducts.Add(product);


            dataGridView1.DataSource = selectedProducts;
        }


        private void SetupGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Product"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Price"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Qty",
                Width = 50
            });

            // ➖ Minus Button
            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Minus",
                Text = "-",
                UseColumnTextForButtonValue = true,
                Width = 30
            });

            // ➕ Plus Button
            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Plus",
                Text = "+",
                UseColumnTextForButtonValue = true,
                Width = 30
            });

            dataGridView1.DataSource = selectedProducts;
        }

    }
}
