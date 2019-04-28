using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace CryptoPriceViewer
{

    public partial class Form1 : Form
    {
        private List<Crypto> cryptoList = new List<Crypto>();

        int index;

        public Form1()
        {
            InitializeComponent();
            try
            {
                //Add objects to list using xml
                XmlDocument coinsXML = new XmlDocument();
                coinsXML.Load("coins.xml");

                foreach (XmlNode node in coinsXML.DocumentElement)
                {
                    //Each child node in order url, held and name
                    cryptoList.Add(new Crypto(node.ChildNodes[0].InnerText, decimal.Parse(node.ChildNodes[1].InnerText), node.ChildNodes[2].InnerText));
                }

                foreach (Crypto coin in cryptoList)
                {
                    lbxCryptoList.Items.Add(coin.getName());
                }
                
            }
            catch
            {
                MessageBox.Show("coins.xml is missing or misconfigured!");
                Environment.Exit(0);
            }
            index = 0;
            updateInfo();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Update ammount held as a preview
            cryptoList[index].setHeld(nudHeld.Value);
            updateInfo();


        }

        private void lbxCryptoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = lbxCryptoList.SelectedIndex;
            updateInfo();
            
        }

        private void updateInfo()
        {
            //Show Infomation to the screen
            //Name
            lblName.Text = cryptoList[index].getName();
            //Value of coin
            lblValue.Text = "$" + Convert.ToString(Math.Round(cryptoList[index].getValue(),2));
            //Ammount held nud
            nudHeld.Value = Math.Round(cryptoList[index].getHeld(), 3);
            //Total Value
            lblValueOfHoldings.Text = "Total Value of Holdings USD: $" +  Convert.ToString(Math.Round(cryptoList[index].getValue() * cryptoList[index].getHeld(), 2));
        }
    }
}

