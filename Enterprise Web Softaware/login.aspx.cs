using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Enterprise_Web_Softaware
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database1.mdf; Integrated Security = True");
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear(); 
        }

        protected void signInButton_Click(object sender, EventArgs e)
        {
            string email = floatingInput.Text;
            string password = floatingPassword.Text;

            string sqlstring = "SELECT * FROM Register WHERE regEmail=@email  and regPassword=@password";
            SqlCommand command = new SqlCommand(sqlstring, connection);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                int userID = Convert.ToInt32(reader["regID"]);
                Session["UserID"] = userID;
                string position = (reader["regPosition"]).ToString();
                Session["Position"] = position;

                string regPosition = reader["regPosition"].ToString();
                if (regPosition == "Admin")
                {
                    Response.Redirect("adminallpost.aspx");
                }
                else if (regPosition == "QA Coordinator")
                {
                    Response.Redirect("qahome.aspx");
                }
                else if (regPosition == "QA Manager")
                {
                    Response.Redirect("managerhome.aspx");
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
            else
            {
                passwordLabel.Text = "Password Wrong!";
                passwordLabel.Visible = true;
            }
            reader.Close();
            connection.Close();
        }

        protected void forgot_Click(object sender, EventArgs e)
        {
            string emailForgot = floatingInput.Text;

            if (emailForgot == "")
            {
                string script = "window.alert('Please enter email so we can now who are you!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }

            try
            {
                var address = new System.Net.Mail.MailAddress(emailForgot);
            }
            catch
            {
                string script = "window.alert('Please enter correct email!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }

            string smtpServer = "smtp.office365.com";
            int port = 587;
            string subject = "Reset Password Reminder";
            string body = emailForgot + "\r\n\r\nDear QA Manager,\r\n\r\nPlease change password for this user.\r\n\r\nThank you for using our forum, we look forward to your participation.\r\n\r\nSincerely,\r\nsalute!\r\n\r\nManagement Team";

            List<string> fromEmails = new List<string>() { "scrumlight.sys9@outlook.com", "scrumlight.sys8@outlook.com", "scrumlight.sys@outlook.com" };

            int currentFromEmailIndex = 0;
            int sendCount = 0;

            var message = new MailMessage();
            message.Subject = subject;
            message.Body = body;

            List<string> adminEmails = GetAdminEmails();
            foreach (string email in adminEmails)
            {
                message.To.Add(new MailAddress(email));
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

                    string script = "window.alert('Password reset reminder sent to QA Manager! Please wait your email!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private List<string> GetAdminEmails()
        {
            List<string> adminEmails = new List<string>();
            string sql = "SELECT regEmail FROM Register WHERE regPosition = 'QA Manager'";

            using (SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database1.mdf; Integrated Security = True"))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string email = reader.GetString(0);
                    adminEmails.Add(email);
                }
                reader.Close();
            }

            return adminEmails;
        }
    }
}