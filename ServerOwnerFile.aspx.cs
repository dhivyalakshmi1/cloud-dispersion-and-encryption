using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ServerOwnerFile : System.Web.UI.Page
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
        string OwnerName = grdrow.Cells[1].Text;
        string fileName = grdrow.Cells[2].Text;
        string FileSize = grdrow.Cells[3].Text;

            //cmd = new SqlCommand("insert into Ranktb values(@FileId,@OwnerName,@FileName,@Filesize,@keyword,@Rank)", con);
            //cmd.Parameters.AddWithValue("@FileId", id);
            //cmd.Parameters.AddWithValue("@OwnerName", OwnerName);
            //cmd.Parameters.AddWithValue("@FileName", fileName);
            //cmd.Parameters.AddWithValue("@Filesize", FileSize);
            //cmd.Parameters.AddWithValue("@keyword", TextBox1.Text);
            //cmd.Parameters.AddWithValue("@Rank", "0");
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();

        bind();
    }

    private void bind()
    {
        cmd = new SqlCommand("select * from filetb", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        cmd = new SqlCommand("select * from fileparttb ", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        GridView2.DataSource = dt1;
        GridView2.DataBind();
    }
}