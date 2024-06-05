using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Text;


public partial class UserDownload : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\VS Project\DROPS\App_Data\Graineddb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        bind();
    }

    int NrOfDays;
    string keys,filename;

    protected void lnkView_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        string id = grdrow.Cells[0].Text;


        if (TextBox1.Text == "")
        {
            Response.Write("<Script> alert('Please Enter Key') </Script>");
        }
        else
        {
            con.Open();
            cmd = new SqlCommand("select * from userfiletb where Fileid='" + id + "' and Keys='" + TextBox1.Text + "'", con);
            SqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                keys = TextBox1.Text;
             

                filename = dr1["FileName"].ToString();


                string time1 = dr1["Time"].ToString();


                string time2 = System.DateTime.Now.ToShortTimeString();



                string ddddd = System.DateTime.Now.ToShortDateString();

                DateTime d11 = Convert.ToDateTime(ddddd);

                DateTime d22 = Convert.ToDateTime(dr1["Date"].ToString());

                TimeSpan ts = d22 - d11;

                NrOfDays = (d22 - d11).Days;
                Label4.Text = NrOfDays.ToString();

                double differenceInDays = ts.TotalDays;
                Label5.Text = NrOfDays.ToString();
               // dated diff = dd - aa;
                if (NrOfDays == 0)
                {


                    DateTime d1 = new DateTime();
                    d1 = Convert.ToDateTime(time1);

                    DateTime d2 = new DateTime();
                    d2 = Convert.ToDateTime(time2);

                    TimeSpan tms = d1.Subtract(d2);


                    decimal dd = Convert.ToDecimal(tms.TotalHours.ToString());

                  //  Label5.Text = dd.ToString();

                    if (dd >= 0)
                    {
                        string filePath1 = Server.MapPath("~/Encrypt/" + filename);
                        string filePath2 = Server.MapPath("~/Decrypt/" + filename);
                        DecryptFile(filePath1, filePath2);

                        string aaa = "~/Decrypt/" + filename;

                        if (aaa != string.Empty)
                        {
                            string filePath = aaa;
                            Response.ContentType = "doc/docx";
                            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + aaa + "\"");
                            Response.TransmitFile(Server.MapPath(filePath));
                            Response.End();
                        }

                    }
                    else
                    {
                        Response.Write("<Script> alert('Time expired') </Script>");
                    }

                 

                 

                  

                }

                else if (NrOfDays > 0)
                {
                    string filePath1 = Server.MapPath("~/Encrypt/" + filename);
                    string filePath2 = Server.MapPath("~/Decrypt/" + filename);
                    DecryptFile(filePath1, filePath2);

                    string aaa = "~/Decrypt/" + filename;

                    if (aaa != string.Empty)
                    {
                        string filePath = aaa;
                        Response.ContentType = "doc/docx";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + aaa + "\"");
                        Response.TransmitFile(Server.MapPath(filePath));
                        Response.End();
                    }
                }


                else
                {
                    Response.Write("<Script> alert('date expired') </Script>");
                }



             





            }

            else
            {
                Response.Write("<Script> alert('File Key Mismatch') </Script>");
            }
            con.Close();

            //Response.Write("<Script> alert('" + id + "') </Script>");
        }

        bind();
    }

    private void bind()
    {
        cmd = new SqlCommand("select * from userfiletb where Status='waiting' and UserName='" + Session["uuname"].ToString() + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        cmd = new SqlCommand("select * from userfiletb where Status='Approved' and UserName='" + Session["uuname"].ToString() + "' ", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        GridView2.DataSource = dt1;
        GridView2.DataBind();

    }


    private void DecryptFile(string inputFile, string outputFile)
    {

        {
            string keyss = keys;
            string password = @"myKey123";

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = UE.GetBytes(password);

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            RijndaelManaged RMCrypto = new RijndaelManaged();

            CryptoStream cs = new CryptoStream(fsCrypt,
                RMCrypto.CreateDecryptor(key, key),
                CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int data;
            while ((data = cs.ReadByte()) != -1)
                fsOut.WriteByte((byte)data);

            fsOut.Close();
            cs.Close();
            fsCrypt.Close();

        }
    }
}