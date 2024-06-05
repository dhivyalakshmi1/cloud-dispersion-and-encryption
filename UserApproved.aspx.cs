using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using System.Net;

public partial class UserApproved : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\VS Project\DROPS\App_Data\Graineddb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        bind();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {

        Random r = new Random();
        int i = r.Next(1111, 9999);

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        string id = grdrow.Cells[0].Text;

        con.Open();
        cmd = new SqlCommand("update usertb  set Status='Approved' ,keys='" + i.ToString() + "' where id='" + id + "' ", con);
        cmd.ExecuteNonQuery();
        con.Close();
        bind();

        

        string to = grdrow.Cells[5].Text;
        string from = "projectmailm@gmail.com";
        // string subject = "Key";
        // string body = TextBox1.Text;
        string password = "qmgn xecl bkqv musr";
        using (MailMessage mm = new MailMessage(from, to))
        {
            mm.Subject = "Keys";
            mm.Body = "Your Login Key:" + i.ToString();
            //if (fuAttachment.HasFile)
            //{
            //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
            //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
            //}
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential(from, password);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);

        }


    }

    private void bind()
    {
        cmd = new SqlCommand("select * from usertb where Status='waiting'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        cmd = new SqlCommand("select * from usertb where Status='Approved'", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        GridView2.DataSource = dt1;
        GridView2.DataBind();

    }
}