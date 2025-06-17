using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using DTO;
using BP;
using Formatting;

namespace KUAPPAS
{
    public partial class frmADK : Form
    {
        public frmADK()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            PK04_01Logic oLogic = new PK04_01Logic(GlobalVar.TahunAnggaran);
            List<PMK04_01> _lst = new List<PMK04_01>();
            _lst = oLogic.Get((int)GlobalVar.TahunAnggaran, 1);
            if (_lst != null)
            {

               var xEle = new XElement("APBD",
                   from l in _lst
                   select new XElement("Data",
                                new XAttribute("Tahun", l.Tahun ),
                                  new XElement("JenisLaporan", l.JenisLaporan ),
                                  new XElement("TypeDaerah", l.TypeDaerah),
                                  new XElement("NamaProvinsi", l.NamaProvinsi ),
                                  new XElement("NamaDaerah", l.NamaDaerah )
                              ));

                xEle.Save("D:\\employees2.xml");
                Console.WriteLine("Converted to XML");

                ////create the serialiser to create the xml
                //XmlSerializer serialiser = new XmlSerializer(typeof(List<PMK04_01>));

                //// Create the TextWriter for the serialiser to use
                //TextWriter Filestream = new StreamWriter(@"D:\output.xml");

                ////write to the file
                //serialiser.Serialize(Filestream,serialiser );

                //// Close the file
                //Filestream.Close();
                ////WriteFileStream.Close();

            }
            else
            {
                MessageBox.Show(oLogic.LastError());
            }
        }
        

        /// <summary>
        /// This function will return an XML Document object that has all the
        /// data from the reffered DataSet.
        /// </summary>
        /// <param name="Ds">DataSet object, from which we need to create the XML Document Object</param>
        /// <returns>Returns an XML Document which have all the data in the reffered DataSet</returns>
        private XmlDocument GenerateXML(DataSet Ds)
        {
            //Creates an XML Document object
            XmlDocument xmlDoc = new XmlDocument();

            //Creates an XML Root Node Element named "Collection"
            XmlElement rootNode = xmlDoc.CreateElement("Collection");

            //Append this to the XML Document object
            xmlDoc.AppendChild(rootNode);

            //Loop through the Tables in the DataSet Object
            if (Ds.Tables.Count > 0)
            {
                DataTable dt = Ds.Tables[0];

                //Loops through the records in the table
                foreach (DataRow dr in dt.Rows)
                {
                    //Creates an XML Sub Node Element named "Records"
                    XmlNode SubNode = xmlDoc.CreateElement("Records");
                    rootNode.AppendChild(SubNode);

                    //Loops through the columns collection to get the Column Name
                    //and corresponding data
                    foreach (DataColumn dc in dt.Columns)
                    {
                        //Creates an XML Node with the Column Name
                        XmlNode invNode = xmlDoc.CreateElement(dc.ColumnName);

                        //Get the data from the corresponding column
                        invNode.InnerText = dr[dc].ToString();
                        SubNode.AppendChild(invNode);

                    }
                }
            }
            return xmlDoc;
        }
        private void SimpanAsXML()
        {
            DataSet ds = new DataSet("DS");
            ds.Namespace = "StdNamespace";
            DataTable stdTable = new DataTable("Student");
            DataColumn col1 = new DataColumn("Name");
            DataColumn col2 = new DataColumn("Address");
            stdTable.Columns.Add(col1);
            stdTable.Columns.Add(col2);
            ds.Tables.Add(stdTable);

            DataRow newRow; newRow = stdTable.NewRow();
            newRow["Name"] = "M C";
            newRow["Address"] = "address 1";
            stdTable.Rows.Add(newRow);
            newRow = stdTable.NewRow();
            newRow["Name"] = "M G";
            newRow["Address"] = "address1";
            stdTable.Rows.Add(newRow);
            ds.AcceptChanges();

            StreamWriter myStreamWriter = new StreamWriter(@"c:\stdData.xml");

            ds.WriteXml(myStreamWriter);
            myStreamWriter.Close();
        }

        private void frmADK_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Generate dan Upload Data ke Pusat.. ");

            cmbJenis.Items.Add("APBD");
            cmbJenis.Items.Add("APBD P");
            //cmbJenis.Items.Add("APBD");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //frmReportViewer fViewer = new frmReportViewer();
            //ParameterLaporan p = new ParameterLaporan();
            //p.JabatanPimpinan ="Sekretaris Daerah";
            //p.NamaPimpinan ="";

            //fViewer.CetakPMK04II(p);
            //fViewer.Show();
        }

    }
}
