using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using DayPilot.Utils;
using System.Globalization;
using System.Net.Mail;

public partial class Reserve : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadResources();
           
            DayPilotScheduler1.StartDate = new DateTime(DateTime.Today.Year, 1, 1);
            DayPilotScheduler1.Days = Year.Days(DateTime.Today.Year);
            DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, DayPilotScheduler1.Days);
            DayPilotScheduler1.DataBind();

            DayPilotScheduler1.SetScrollX(DateTime.Today);
        }
    }

    protected void DayPilotScheduler1_EventMove(object sender, DayPilot.Web.Ui.Events.EventMoveEventArgs e)
    {
        string id = e.Value;
        DateTime start = e.NewStart;
        DateTime end = e.NewEnd;
        string resource = e.NewResource;

        DbUpdateEvent(id, start, end, resource);

        DayPilotScheduler1.DataSource = DbGetEvents(DayPilotScheduler1.StartDate, DayPilotScheduler1.Days);
        DayPilotScheduler1.DataBind();
        DayPilotScheduler1.Update();
    }

    private DataTable DbGetEvents(DateTime start, int days)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT [id], [name], [eventstart], [eventend], [resource_id] FROM [event] WHERE NOT (([eventend] <= @start) OR ([eventstart] >= @end))", ConfigurationManager.ConnectionStrings["DayPilot"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("start", start);
        da.SelectCommand.Parameters.AddWithValue("end", start.AddDays(days));
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    private void DbUpdateEvent(String id, DateTime start, DateTime end, string resource)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DayPilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE [event] SET eventstart = @start, eventend = @end, resource_id = @resource WHERE id = @id", con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("start", start);
            cmd.Parameters.AddWithValue("end", end);
            cmd.Parameters.AddWithValue("resource", resource);
            cmd.ExecuteNonQuery();
        }
    }

    private void LoadResources()
    {
        DayPilotScheduler1.Resources.Clear();

        SqlDataAdapter da = new SqlDataAdapter("SELECT [id], [name] FROM [resource]", ConfigurationManager.ConnectionStrings["DayPilot"].ConnectionString);
        DataTable dt = new DataTable();
        da.Fill(dt);

        foreach (DataRow r in dt.Rows)
        {
            string name = (string)r["name"];
            string id = Convert.ToString(r["id"]);

            DayPilotScheduler1.Resources.Add(name, id);
        }
    }

    protected void LinkButtonSample_Click(object sender, EventArgs e)
    {
        DbClearResources();
        DbClearEvents();

        Random random = new Random();

        for (int i = 1; i <= 10; i++)
        {
            DbInsertResource("Room " + i.ToString("D2"), i);

            DateTime start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(random.Next(1, 5));
            for (int x = 1; x <= 100; x++)
            {
                int duration = random.Next(3, 8);
                DateTime end = start.AddDays(duration);
                DbInsertEvent(start, end, "Reservation #" + i + "-" + x, i);
                start = end.AddDays(random.Next(1, 5));
            }

        }

        Response.Redirect(Request.Url.PathAndQuery);
    }

    private void DbInsertResource(string name, int id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [resource] (name, id) VALUES(@name, @id)", con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }

    private void DbInsertEvent(DateTime start, DateTime end, string name, int resource)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [event] (eventstart, eventend, name, resource_id) VALUES(@start, @end, @name, @resource)", con);
            cmd.Parameters.AddWithValue("start", start);
            cmd.Parameters.AddWithValue("end", end);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("resource", resource);
            cmd.ExecuteNonQuery();
        }
    }

    private void DbClearEvents()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from [event]", con);
            cmd.ExecuteNonQuery();
        }
    }

    private void DbClearResources()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from [resource]", con);
            cmd.ExecuteNonQuery();
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        DbClearEvents();
        DbClearResources();
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
        conn.Open();
        string checkuser = "select count(*) from [UserData] where UserName='" + ID.Text + "' AND Password='" + PW.Text +"'";
        SqlCommand com = new SqlCommand(checkuser, conn);
        int count = Convert.ToInt32(com.ExecuteScalar().ToString());
        int id;

        DateTime startDate = DateTime.Parse(Start.Text);
        //DateTime startDate = DateTime.ParseExact(Start.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        //DateTime endDate = DateTime.ParseExact(End.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.Parse(End.Text);

        if (count != 1)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Username and Password')</script>");
        }
        else if (startDate > endDate)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Start Date should be earlier than End date')</script>");
        }
        else 
        {
            string checkID = "select id from [resource] where name='" + DropDownList1.SelectedValue + "'";
            com = new SqlCommand(checkID, conn);
            count = Convert.ToInt32(com.ExecuteScalar().ToString());
            id = count;

            string checkDate = "select count(*) from [event] where resource_id='" + count + "' AND eventstart <='" + startDate + "' AND eventend >= '" + startDate + "'";
            //string checkDate = "select count(*) from [event] where eventstart <='" + startDate + "' AND eventend >= '" + startDate + "'";
            com = new SqlCommand(checkDate, conn);
            count = Convert.ToInt32(com.ExecuteScalar().ToString());
            if (count >= 1)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Selected Date is already reserved')</script>");
            }
            DbInsertEvent(startDate, endDate, ID.Text, id);
        }

        string checkEmail = "select email from [UserData] where UserName='" +ID.Text+"'";
        com = new SqlCommand(checkEmail, conn);
        string email = com.ExecuteScalar().ToString();

        Send_Email(email);
        conn.Close();
        Response.Redirect(Request.Url.PathAndQuery);
    }

    public void Send_Email(string emailaddress)
    {
        
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("hotelwebsample@gmail.com");
            mail.To.Add(emailaddress);
            mail.Subject = "Test Mail";
            mail.Body = "This is for testing SMTP mail from GMAIL";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("hotelwebsample", "aabbcc112233");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.ToString() + "');", true);
        }
    }
}