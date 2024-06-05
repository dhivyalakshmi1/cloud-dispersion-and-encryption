using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ServerHome : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\VS Project\DROPS\App_Data\Graineddb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        bind();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        string id = grdrow.Cells[0].Text;

        con.Open();
        cmd = new SqlCommand("update ownertb  set Status='Approved' where id='" + id + "' ", con);
        cmd.ExecuteNonQuery();
        con.Close();
        bind();
    }

    private void bind()
    {
        cmd = new SqlCommand("select id, Name,Gender,Age,Mobileno,Emailid,Comapny,Position,Status from ownertb where Status='waiting'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        cmd = new SqlCommand("select id,Name,Gender,Age,Mobileno,Emailid,Comapny,Position,Status from Ownertb where Status='Approved'", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        GridView2.DataSource = dt1;
        GridView2.DataBind();

    }
}