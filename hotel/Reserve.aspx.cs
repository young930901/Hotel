using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using DayPilot.Utils;

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

    private void DbUpdateEvent(string id, DateTime start, DateTime end, string resource)
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
}