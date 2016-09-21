using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hotel
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (IsPostBack)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DayPilot"].ConnectionString);
                conn.Open();
                string checkuser = "select count(*) from [UserData] where UserName='" + TextBoxUN.Text + "'";
                SqlCommand com = new SqlCommand(checkuser, conn);
                int count = Convert.ToInt32(com.ExecuteScalar().ToString());
                if (count == 1)
                {
                    Response.Write("User already exists");
                }
                else
                {
                    checkuser = "select count(*) from [UserData] where Email='" + TextBoxEmail.Text + "'";
                    count = Convert.ToInt32(com.ExecuteScalar().ToString());

                    if (count == 1)
                    {
                        Response.Write("Email is already taken");
                    }
                }
                
                conn.Close();

            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BSumit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DayPilot"].ConnectionString);
                conn.Open();
                string insertQuery = "insert into [UserData] (UserName,Email,Password,Country) values (@Uname ,@email, @password, @country)";
                SqlCommand com = new SqlCommand(insertQuery, conn);
                com.Parameters.AddWithValue("@Uname", TextBoxUN.Text);
                com.Parameters.AddWithValue("@email", TextBoxEmail.Text);
                com.Parameters.AddWithValue("@password", TextBoxPass.Text);
                com.Parameters.AddWithValue("@country", SelectCountry.SelectedItem.ToString());

                com.ExecuteNonQuery();
                Response.Redirect("Reserve.aspx");
                Response.Write("Your registration is successful.");

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());

            }
        }

        protected void TextBoxUN_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}