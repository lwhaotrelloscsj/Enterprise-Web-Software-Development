using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Enterprise_Web_Softaware
{
    public partial class managercategory : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database1.mdf; Integrated Security = True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["Position"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else if (Session["Position"] != null)
            {
                string position = Session["Position"].ToString();
                if (position != "QA Manager")
                {
                    Response.Redirect("login.aspx");
                }
            }
            Repeater repeatCategory = (Repeater)Page.FindControl("repeatCategory");

            string sqlstring = "SELECT * FROM Category WHERE closed = 0;";
            SqlCommand command = new SqlCommand(sqlstring, connection);
            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            repeatCategory.DataSource = dataTable;
            repeatCategory.DataBind();

            connection.Close();
        }

        protected void newCategoryButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "LaunchModal", "$(function() { $('#myModal').modal('show'); });", true);
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            string categoryName = Request.Form["inputCategoryName"];
            string selectedClosureDate = Request.Form["closureDate"];
            string selectedFinalClosureDate = Request.Form["finalClosureDate"];

            if (categoryName.Length > 50)
            {
                string script = "window.alert('Category Name too long!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }

            if (!string.IsNullOrEmpty(selectedClosureDate) && !string.IsNullOrEmpty(selectedFinalClosureDate) && !string.IsNullOrEmpty(categoryName))
            {
                string sqlstring1 = "SELECT COUNT(*) FROM Category WHERE categoryName=@categoryName AND closed = 0";
                SqlCommand command1 = new SqlCommand(sqlstring1, connection);
                command1.Parameters.AddWithValue("@categoryName", categoryName);
                connection.Open();

                int count = (int)command1.ExecuteScalar();

                if (count > 0)
                {
                    string script1 = "window.alert('Category already EXISTS!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script1, true);
                    return;
                }

                string sqlstring2 = "SELECT COUNT(*) FROM Category WHERE categoryName=@categoryName AND closed = 1";
                SqlCommand command2 = new SqlCommand(sqlstring2, connection);
                command2.Parameters.AddWithValue("@categoryName", categoryName);

                int count2 = (int)command2.ExecuteScalar();

                if (count2 > 0)
                {
                    string script2 = "window.alert('Please delete same category from the closed list before adding a same category!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script2, true);
                    return;
                }

                DateTime closureDate = DateTime.Parse(selectedClosureDate);
                DateTime finalClosureDate = DateTime.Parse(selectedFinalClosureDate);

                string insertCategory = "INSERT INTO Category (categoryName, categoryPostNumber, categoryTotalView, categoryTotalComment, closureDate, finalClosureDate, lastEditDate, closed) VALUES (@categoryName, '0', '0', '0', @closureDate, @finalClosureDate, @lastEditDate, '0')";
                SqlCommand insertCommand = new SqlCommand(insertCategory, connection);
                insertCommand.Parameters.AddWithValue("@categoryName", categoryName);
                insertCommand.Parameters.AddWithValue("@closureDate", closureDate);
                insertCommand.Parameters.AddWithValue("@finalClosureDate", finalClosureDate);
                insertCommand.Parameters.AddWithValue("@lastEditDate", DateTime.Now);
                insertCommand.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                string script = "window.alert('Please fill in the blanks before add new category!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }

            Response.Redirect("managercategory.aspx");
        }

        protected void editToolButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "LaunchModal", "$(function() { $('#myModal').modal('show'); });", true);
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            Button edit = (Button)sender;

            int categoryID = Convert.ToInt32(edit.CommandArgument);
            string categoryName = Request.Form["inputName" + categoryID];
            string selectedEditClosureDate = Request.Form["closure"];
            string selectedEditFinalClosureDate = Request.Form["final"];

            if (string.IsNullOrEmpty(selectedEditClosureDate))
            {
                string script = "window.alert('Please choose Closure date before edit!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }

            if (string.IsNullOrEmpty(selectedEditFinalClosureDate))
            {
                string script = "window.alert('Please choose Final Closure date before edit!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }

            DateTime editClosureDate;
            DateTime editFinalClosureDate;

            if (DateTime.TryParse(selectedEditClosureDate, out editClosureDate) && DateTime.TryParse(selectedEditFinalClosureDate, out editFinalClosureDate))
            {
                if (categoryName != "")
                {
                    string sqlstring1 = "SELECT COUNT(*) FROM Category WHERE categoryName=@categoryName AND closed = 0";
                    SqlCommand command1 = new SqlCommand(sqlstring1, connection);
                    command1.Parameters.AddWithValue("@categoryName", categoryName);
                    connection.Open();

                    int count = (int)command1.ExecuteScalar();

                    if (count > 0)
                    {
                        string script1 = "window.alert('Category already EXISTS!');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", script1, true);
                        return;
                    }
                    string updatetCategory = "UPDATE Category SET categoryName=@categoryName, closureDate=@closureDate, finalClosureDate=@finalClosureDate, lastEditDate=@lastEditDate WHERE categoryID=@categoryID";
                    SqlCommand updateCommand = new SqlCommand(updatetCategory, connection);
                    updateCommand.Parameters.AddWithValue("@categoryName", categoryName);
                    updateCommand.Parameters.AddWithValue("@closureDate", editClosureDate);
                    updateCommand.Parameters.AddWithValue("@finalClosureDate", editFinalClosureDate);
                    updateCommand.Parameters.AddWithValue("@lastEditDate", DateTime.Now);
                    updateCommand.Parameters.AddWithValue("@categoryID", categoryID);
                    updateCommand.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    string updatetCategory = "UPDATE Category SET closureDate=@closureDate, finalClosureDate=@finalClosureDate, lastEditDate=@lastEditDate WHERE categoryID=@categoryID";
                    SqlCommand updateCommand = new SqlCommand(updatetCategory, connection);
                    updateCommand.Parameters.AddWithValue("@closureDate", editClosureDate);
                    updateCommand.Parameters.AddWithValue("@finalClosureDate", editFinalClosureDate);
                    updateCommand.Parameters.AddWithValue("@lastEditDate", DateTime.Now);
                    updateCommand.Parameters.AddWithValue("@categoryID", categoryID);
                    connection.Open();
                    updateCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            else
            {
                string script = "window.alert('Please choose a valid date before edit!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }

            Response.Redirect("managercategory.aspx");
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            LinkButton deleteTool = (LinkButton)sender;
            int categoryID = Convert.ToInt32(deleteTool.CommandArgument);

            string select = "SELECT categoryPostNumber, closed FROM Category WHERE categoryID = @categoryID";
            SqlCommand command = new SqlCommand(select, connection);
            command.Parameters.AddWithValue("@categoryID", categoryID);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                int categoryPostNumber = Convert.ToInt32(reader["categoryPostNumber"]);
                int closed = Convert.ToInt32(reader["closed"]);

                if (categoryPostNumber != 0 && closed == 0)
                {
                    string script = "alert('This category cannot be deleted because it have been used')";
                    ScriptManager.RegisterStartupScript(this, GetType(), "DeleteError", script, true);
                }
                else if (categoryPostNumber != 0 && closed == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "LaunchModal", "$(function() { $('#deleteModal" + categoryID + "' ).modal('show'); });", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "LaunchModal", "$(function() { $('#deleteModal" + categoryID + "' ).modal('show'); });", true);
                }
            }

            connection.Close();
        }

        protected void confirmButton_Click(object sender, EventArgs e)
        {
            Button delete = (Button)sender;

            int categoryID = Convert.ToInt32(delete.CommandArgument);

            string deleteQuery = "DELETE FROM Category WHERE categoryID = @categoryID";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
            deleteCommand.Parameters.AddWithValue("@categoryID", categoryID);

            connection.Open();
            deleteCommand.ExecuteNonQuery();
            connection.Close();

            Response.Redirect("managercategory.aspx");
        }
    }
}