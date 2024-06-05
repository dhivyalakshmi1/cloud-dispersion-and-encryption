using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net;
using System.IO;

public partial class UserReg : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\VS Project\DROPS\App_Data\Graineddb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        //if (TextBox6.Text == "")
        //{

        //    Response.Write("<Script> alert('Please Get OTP') </Script>");
        //}
        //else
        //{
        //    if (TextBox6.Text == Session["otp"].ToString())
        //    {

                cmd = new SqlCommand("insert into usertb values('" + TextBox1.Text + "','" + RadioButtonList1.SelectedItem.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox8.Text + "','" + TextBox9.Text + "','Waiting','')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<Script> alert('user Info Saved') </Script>");
        //    }
        //    else
        //    {
        //        Response.Write("<Script> alert('OTP Incorrect!') </Script>");
        //    }
        //}
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox10.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
      // TextBox6.Text = "";
      
        TextBox8.Text = "";
        TextBox9.Text = "";
    }
    public void sendmessage(string targetno, string message)
    {

        //String query = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=fantasy5535&password=1163974702&sendername=Sample&mobileno=" + targetno + "&message=" + message;
        //WebClient client = new WebClient();
        //Stream sin = client.OpenRead(query);
        //Response.Write("<script> alert('Message Send') </script>");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //Random rr = new Random();
        //int i = rr.Next(11111, 99999);
        //Session["otp"] = i.ToString();
        //sendmessage(TextBox3.Text, "Your OTP :" + i.ToString());
    }
}