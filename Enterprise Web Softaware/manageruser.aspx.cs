using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Enterprise_Web_Softaware
{
    public partial class manageruser : System.Web.UI.Page
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

            Repeater repeatUser = (Repeater)Page.FindControl("repeatUser");

            string sqlstring = "SELECT * FROM Register WHERE regID <> 14;";
            SqlCommand command = new SqlCommand(sqlstring, connection);
            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            repeatUser.DataSource = dataTable;
            repeatUser.DataBind();

            connection.Close();
        }

        protected void editToolButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "LaunchModal", "$(function() { $('#myModal').modal('show'); });", true);
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            Button edit = (Button)sender;

            int regID = Convert.ToInt32(edit.CommandArgument);
            string username = Request.Form["inputName" + regID];
            string email = Request.Form["inputEmail" + regID];
            string password = Request.Form["inputPassword" + regID];
            string selectedPosition = Request.Form["position" + regID];
            string selectedDepartment = Request.Form["department" + regID];

            if (selectedPosition != "null")
            {
                string updatetUser = "UPDATE Register SET regPosition=@regPosition WHERE regID=@regID";
                SqlCommand updateCommand = new SqlCommand(updatetUser, connection);
                updateCommand.Parameters.AddWithValue("@regPosition", selectedPosition);
                updateCommand.Parameters.AddWithValue("@regID", regID);
                connection.Open();
                updateCommand.ExecuteNonQuery();
                connection.Close();
            }

            if (selectedDepartment != "null")
            {
                string updatetUser = "UPDATE Register SET regDepartment=@regDepartment WHERE regID=@regID";
                SqlCommand updateCommand = new SqlCommand(updatetUser, connection);
                updateCommand.Parameters.AddWithValue("@regDepartment", selectedDepartment);
                updateCommand.Parameters.AddWithValue("@regID", regID);
                connection.Open();
                updateCommand.ExecuteNonQuery();
                connection.Close();
            }

            if (username != "")
            {
                string updatetUser = "UPDATE Register SET regUsername=@username WHERE regID=@regID";
                SqlCommand updateCommand = new SqlCommand(updatetUser, connection);
                updateCommand.Parameters.AddWithValue("@username", username);
                updateCommand.Parameters.AddWithValue("@regID", regID);
                connection.Open();
                updateCommand.ExecuteNonQuery();
                connection.Close();
            }

            if (password != "")
            {
                if (password.Length < 6 || password.Length > 100)
                {
                    string script = "window.alert('Password must more than six character!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    return;
                }
                string updatetUser = "UPDATE Register SET regPassword=@regPassword WHERE regID=@regID";
                SqlCommand updateCommand = new SqlCommand(updatetUser, connection);
                updateCommand.Parameters.AddWithValue("@regPassword", password);
                updateCommand.Parameters.AddWithValue("@regID", regID);
                connection.Open();
                updateCommand.ExecuteNonQuery();
                connection.Close();

                string smtpServer = "smtp.office365.com";
                int port = 587;
                string subject = "New Password Reminder";
                string body = password + "Dear User,\r\n\r\nYour Password has been reset.\r\n\r\nThank you for using our forum, we look forward to your participation.\r\n\r\nSincerely,\r\nsalute!\r\n\r\nManagement Team";

                List<string> fromEmails = new List<string>() { "scrumlight.sys9@outlook.com", "scrumlight.sys8@outlook.com", "scrumlight.sys@outlook.com" };

                int currentFromEmailIndex = 0;
                int sendCount = 0;

                var message = new MailMessage();
                message.Subject = subject;
                message.Body = body;

                List<string> forgotEmails = GetForgotEmail(regID);
                foreach (string forgotEmail in forgotEmails)
                {
                    message.To.Add(new MailAddress(forgotEmail));
                }

                using (var client = new SmtpClient(smtpServer, port))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(fromEmails[currentFromEmailIndex], "scrumlight123");

                    try
                    {
                        message.From = new MailAddress(fromEmails[currentFromEmailIndex]);

                        client.Send(message);

                        sendCount++;

                        if (sendCount == 2)
                        {
                            currentFromEmailIndex++;

                            if (currentFromEmailIndex >= fromEmails.Count)
                            {
                                currentFromEmailIndex = 0;
                            }

                            sendCount = 0;
                        }

                        string script = "window.alert('New password sent to user via email!');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            if (email != "")
            {
                string sqlstring1 = "SELECT COUNT(*) FROM Register WHERE regEmail =@email";
                SqlCommand command1 = new SqlCommand(sqlstring1, connection);
                command1.Parameters.AddWithValue("@email", email);
                connection.Open();

                int count = (int)command1.ExecuteScalar();

                if (count > 0)
                {
                    string script = "window.alert('Email already EXISTS!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    return;
                }

                string updatetUser = "UPDATE Register SET regEmail=@regEmail WHERE regID=@regID";
                SqlCommand updateCommand = new SqlCommand(updatetUser, connection);
                updateCommand.Parameters.AddWithValue("@regEmail", email);
                updateCommand.Parameters.AddWithValue("@regID", regID);
                updateCommand.ExecuteNonQuery();
                connection.Close();
            }

            Response.Redirect("manageruser.aspx");
        }

        private List<string> GetForgotEmail(int regID)
        {
            List<string> forgotEmails = new List<string>();
            string sql = "SELECT regEmail FROM Register WHERE regID=@regID";

            using (SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database1.mdf; Integrated Security = True"))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@regID", regID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string email = reader.GetString(0);
                    forgotEmails.Add(email);
                }
                reader.Close();
            }

            return forgotEmails;
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "LaunchModal", "$(function() { $('#myModal').modal('show'); });", true);
        }

        protected void confirmButton_Click(object sender, EventArgs e)
        {
            Button delete = (Button)sender;

            int regID = Convert.ToInt32(delete.CommandArgument);
            int userID = Convert.ToInt32(Session["UserID"]);

            string sql = "SELECT regID FROM Register WHERE regID=@regID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@regID", regID);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                if (regID == userID)
                {
                    string script = "window.alert('Cannot delete yourself!!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    return;
                }
                else
                {
                    reader.Close();

                    string deleteQuery = "DELETE FROM Register WHERE regID = @regID";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@regID", regID);

                    deleteCommand.ExecuteNonQuery();
                    connection.Close();

                    string updateQuery = "UPDATE Posts SET postregID = 14 WHERE postregID IS NULL";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    connection.Open();
                    updateCommand.ExecuteNonQuery();
                    connection.Close();

                    string updateQuery2 = "UPDATE Comments SET commentregID = 14 WHERE commentregID IS NULL";
                    SqlCommand updateCommand2 = new SqlCommand(updateQuery2, connection);
                    connection.Open();
                    updateCommand2.ExecuteNonQuery();
                    connection.Close();

                    string updateQuery3 = "UPDATE Likes SET likeregID = 14 WHERE likeregID IS NULL";
                    SqlCommand updateCommand3 = new SqlCommand(updateQuery3, connection);
                    connection.Open();
                    updateCommand3.ExecuteNonQuery();
                    connection.Close();
                }
            }

            Response.Redirect("manageruser.aspx");
        }
    }
}