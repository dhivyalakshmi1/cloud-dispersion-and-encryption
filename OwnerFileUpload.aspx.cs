using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public partial class OwnerFileUpload : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Graineddb.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label5.Text = Session["uname"].ToString();

    }

    public static string ECC(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqstuuvwxyz";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    string filename;
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label6.Text = "Encrypt key:" + ECC(10).ToString();



       filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Upload/" + filename));
        string filePath = Server.MapPath("~/Upload/" + filename);

        decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);

        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        br.Close();
        fs.Close();

        string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
        string path7 = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Upload/" + path7));
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Split/" + path7));
        string s12 = Server.MapPath("~/Upload/" + path7);
        string ss = Server.MapPath("~/Split/" + path7);
        SplitFile(Server.MapPath("~/Split/" + path7), Convert.ToInt32(4), ss, ext);

        cmd = new SqlCommand("insert into filetb values(@OwnerName,@FileInfo,@FileName,@FilePath,@FileData,@size,@keys)", con);
        cmd.Parameters.AddWithValue("@OwnerName", Label5.Text);
        cmd.Parameters.AddWithValue("@FileInfo", TextBox1.Text);
        cmd.Parameters.AddWithValue("@FileName", filename);
        cmd.Parameters.AddWithValue("@FilePath", "~/Upload/" + filename);
        cmd.Parameters.AddWithValue("@FileData", bytes);
        cmd.Parameters.AddWithValue("@size", size.ToString() + "KB");
        cmd.Parameters.AddWithValue("@keys", ECC(10).ToString());

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        string filePath1 = Server.MapPath("~/Encrypt/" + filename);
        EncryptFile(filePath, filePath1);
        Response.Write("<Script> alert('File Encrypt and Saved') </Script>");

      

    }

    private void EncryptFile(string inputFile, string outputFile)
    {

        
        string password = @"myKey123"; 
        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] key = UE.GetBytes(password);

        string cryptFile = outputFile;
        FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

        RijndaelManaged RMCrypto = new RijndaelManaged();

        CryptoStream cs = new CryptoStream(fsCrypt,
            RMCrypto.CreateEncryptor(key, key),
            CryptoStreamMode.Write);

        FileStream fsIn = new FileStream(inputFile, FileMode.Open);

        int data;
        while ((data = fsIn.ReadByte()) != -1)
            cs.WriteByte((byte)data);

        fsIn.Close();
        cs.Close();
        fsCrypt.Close();
        Response.Write("Encryption Sucessfully Completed");

    }


    public FileStream fs;
    string mergeFolder;
    List<string> Packets = new List<string>();
    FileStream outputFile;
    public bool SplitFile(string SourceFile, int nNoofFiles, string splitfile, string ext)
    {
        

        bool Split = false;
        try
        {
            FileStream fs = new FileStream(SourceFile, FileMode.Open, FileAccess.Read);
            int SizeofEachFile = (int)Math.Ceiling((double)fs.Length / nNoofFiles);

            for (int i = 0; i < nNoofFiles; i++)
            {
                string baseFileName = Path.GetFileNameWithoutExtension(SourceFile);
                string Extension = Path.GetExtension(SourceFile);



                if (i == 0)
                {

                    string sss = Server.MapPath("~/Amazon/");

                    outputFile = new FileStream(Path.GetDirectoryName(sss) + "\\" + baseFileName + "." +
                       i.ToString().PadLeft(3, Convert.ToChar("0")) + ext, FileMode.Create, FileAccess.Write);

                    cmd = new SqlCommand("insert into fileparttb values(@OwnerName,@FileInfo,@FileName,@FilePath,@FilePart,@servername)", con);
                    cmd.Parameters.AddWithValue("@OwnerName", Label5.Text);
                    cmd.Parameters.AddWithValue("@FileInfo", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@FileName", filename);
                    cmd.Parameters.AddWithValue("@FilePath", "~/Amazon/" + baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + ext);
                    cmd.Parameters.AddWithValue("@FilePart", baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + ext);
                    cmd.Parameters.AddWithValue("@Servername", "Amazon");

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                if (i == 1)
                {
                    string sss = Server.MapPath("~/IBM/");
                    outputFile = new FileStream(Path.GetDirectoryName(sss) +  "\\" + baseFileName + "." +
                      i.ToString().PadLeft(3, Convert.ToChar("0")) + ext, FileMode.Create, FileAccess.Write);


                    cmd = new SqlCommand("insert into fileparttb values(@OwnerName,@FileInfo,@FileName,@FilePath,@FilePart,@servername)", con);
                    cmd.Parameters.AddWithValue("@OwnerName", Label5.Text);
                    cmd.Parameters.AddWithValue("@FileInfo", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@FileName", filename);
                    cmd.Parameters.AddWithValue("@FilePath", "~/IBM/" + baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + ext);
                    cmd.Parameters.AddWithValue("@FilePart", baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + ext);
                    cmd.Parameters.AddWithValue("@Servername", "IBM");

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (i == 2)
                {
                    string sss = Server.MapPath("~/Google/");
                    outputFile = new FileStream(Path.GetDirectoryName(sss) +  "\\" + baseFileName + "." +
                        i.ToString().PadLeft(3, Convert.ToChar("0")) + ext, FileMode.Create, FileAccess.Write);


                    cmd = new SqlCommand("insert into fileparttb values(@OwnerName,@FileInfo,@FileName,@FilePath,@FilePart,@servername)", con);
                    cmd.Parameters.AddWithValue("@OwnerName", Label5.Text);
                    cmd.Parameters.AddWithValue("@FileInfo", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@FileName", filename);
                    cmd.Parameters.AddWithValue("@FilePath", "~/Google/" + baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + ext);
                    cmd.Parameters.AddWithValue("@FilePart", baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + ext);
                    cmd.Parameters.AddWithValue("@Servername", "Google");

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (i == 3)
                {
                    string sss = Server.MapPath("~/Godaddy/");
                    outputFile = new FileStream(Path.GetDirectoryName(sss) + "\\" + baseFileName + "." +
                       i.ToString().PadLeft(3, Convert.ToChar("0")) + ext, FileMode.Create, FileAccess.Write);

                    cmd = new SqlCommand("insert into fileparttb values(@OwnerName,@FileInfo,@FileName,@FilePath,@Filepart,@Servername)", con);
                    cmd.Parameters.AddWithValue("@OwnerName", Label5.Text);
                    cmd.Parameters.AddWithValue("@FileInfo", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@FileName", filename);
                    cmd.Parameters.AddWithValue("@FilePath", "~/Godaddy/" + baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + ext);
                    cmd.Parameters.AddWithValue("@FilePart", baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + ext);
                    cmd.Parameters.AddWithValue("@Servername", "Godaddy");

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //outputFile = new FileStream(Path.GetDirectoryName(splitfile) + "3" + "\\" + baseFileName + "." +
                    // i.ToString().PadLeft(3, Convert.ToChar("0")) + ext, FileMode.Create, FileAccess.Write);
                    
                    //FileStream outputFile = new FileStream(Path.GetDirectoryName(SourceFile) + "\\" + baseFileName + "." +
                    //   i.ToString().PadLeft(5, Convert.ToChar("0")) + Extension + ".txt", FileMode.Create, FileAccess.Write);
                }

                mergeFolder = Path.GetDirectoryName(SourceFile);

                int bytesRead = 0;
                byte[] buffer = new byte[SizeofEachFile];

                if ((bytesRead = fs.Read(buffer, 0, SizeofEachFile)) > 0)
                {
                    outputFile.Write(buffer, 0, bytesRead);
                    //outp.Write(buffer, 0, BytesRead);

                    string packet = baseFileName + "" + i.ToString().PadLeft(3, Convert.ToChar("0")) + Extension.ToString();
                    Packets.Add(packet);
                }

                outputFile.Close();

            }
            fs.Close();
        }
        catch (Exception Ex)
        {
            throw new ArgumentException(Ex.Message);
        }

        return Split;
    }
}