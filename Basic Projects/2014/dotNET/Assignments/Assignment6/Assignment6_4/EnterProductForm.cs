using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Assignment6_4
{
    public partial class EnterProductForm : Form
    {
        ObjectSerialization os;

        public EnterProductForm()
        {
            InitializeComponent();
        }

        private void EnterProductForm_Load(object sender, EventArgs e)
        {
            //MdiParent.LayoutMdi(MdiLayout.Cascade); 

            os = new ObjectSerialization();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            string category;
            string name;
            float price;
            int amount;

            try
            {
                category = this.categoryComboBox.Text;
            }
            catch
            {
                MessageBox.Show("Category setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (this.nameTextBox.Text == "")
                {
                    MessageBox.Show("Name parameter is not set correctly!\nParameter cannot be an empty.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                name = this.nameTextBox.Text;
            }
            catch
            {
                MessageBox.Show("Name setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                price = Convert.ToSingle(this.unitPriceTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Unit Price setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                amount = Convert.ToInt32(this.amountTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Amount setting problem!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            string filePathName = "";

            if (saveFileDialog.ShowDialog().Equals(DialogResult.OK))
            {
                filePathName = saveFileDialog.FileName;

                // Hashtable got Path of files and ID information - by ID is possible to get Path
                int ID = 0;
                Hashtable productList = null;
                Product product;

                if (os.deserializeHashtable(ref productList))
                {
                    ID = productList.Count - 1;

                    ID++;
                }
                else
                {
                    productList = new Hashtable();
                }

                product = new Product(ID, category, name, price, amount);

                Hashtable newProductList = new Hashtable();
                for (int i = 0; i < productList.Count; i++)
                    newProductList[i] = productList[i];

                newProductList[productList.Count] = filePathName;


                os.serializeHashtable(newProductList);

                // Saving Product process
                Product[] productArray = null;
                Product[] newProductArray = null;

                if (os.deserializeObjectArray(ref productArray, filePathName))
                {
                    newProductArray = new Product[productArray.Length + 1];
                    for (int i = 0; i < productArray.Length; i++)
                        newProductArray[i] = productArray[i];

                    newProductArray[productArray.Length] = product;
                }
                else
                {
                    newProductArray = new Product[1];
                    newProductArray[0] = product;
                }

                if (os.serializeObjectArray(newProductArray, filePathName))
                {
                    if (os.serializeHashtable(newProductList))
                    {
                        MessageBox.Show("Product name: " + name + " saving complete, by ID : " + ID + "\nSaved to file: " + filePathName, "Object Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                MessageBox.Show("Saiving process problem to file: " + filePathName, "Object Saving Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
