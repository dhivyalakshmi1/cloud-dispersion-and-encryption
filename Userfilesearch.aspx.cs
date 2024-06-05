using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Userfilesearch : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Graineddb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DropDownList1.Items.Clear();

        cmd = new SqlCommand("select Distinct id,OwnerName,FileName,Size  from filetb where FileName like '%" + TextBox1.Text + "%' or FileInfo like '%" + TextBox1.Text + "%'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

       // add();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        string id = grdrow.Cells[0].Text;

        con.Open();
        cmd = new SqlCommand("select * from filetb where id ='" + id + "'", con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            s1 = dr["id"].ToString();
            s2 = dr["OwnerName"].ToString();
            s3 = dr["FileName"].ToString();
            s4 = dr["Size"].ToString();
          //  s5 = dr["Keyword"].ToString();


        }
        con.Close();

        cmd = new SqlCommand("insert into userfiletb values('" + id + "','" + s2 + "','" + s3 + "','" + s4 + "','" + Session["uuname"].ToString() + "','Waiting','','" + System.DateTime.Now.ToShortDateString() + "','" + System.DateTime.Now.ToShortTimeString() + "')", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        Response.Write("<Script> alert('Request Send') </Script>");



        //bind();
    }

    int i;
    string s1, s2, s3, s4, s5, s6, s7, s8;
    private void add()
    {
        //if (TextBox2.Text != "")
        //{

        i = 1;

        con.Open();
        cmd = new SqlCommand("select distinct Fileid from ranktb where FileName like '%" + TextBox1.Text + "%' or Keyword like '%" + TextBox1.Text + "%'", con);
        SqlDataReader dr3;
        dr3 = cmd.ExecuteReader();
        while (dr3.Read())
        {

            DropDownList1.Items.Add(dr3["Fileid"].ToString());

        }
        con.Close();


        for (int ii = 0; ii < DropDownList1.Items.Count; ii++)
        {


            con.Open();
            cmd = new SqlCommand("select * from ranktb where Fileid ='" + DropDownList1.Items[ii].ToString() + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                s1 = dr["Fileid"].ToString();
                s2 = dr["OwerName"].ToString();
                s3 = dr["FileName"].ToString();
                s4 = dr["FileSize"].ToString();
                s5 = dr["Keyword"].ToString();
              

            }
            con.Close();

            cmd = new SqlCommand("insert into ranktb values(@Fileid,@OwerName,@FileName,@FileSize,@Keyword,@Rank)", con);
            cmd.Parameters.AddWithValue("@Fileid", s1);
            cmd.Parameters.AddWithValue("@OwerName", s2);
            cmd.Parameters.AddWithValue("@FileName", s3);
            cmd.Parameters.AddWithValue("@FileSize", s4);
            cmd.Parameters.AddWithValue("@Keyword", s5);
            cmd.Parameters.AddWithValue("@Rank", i);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();



         

        }


    }
}