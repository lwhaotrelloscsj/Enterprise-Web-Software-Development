using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Enterprise_Web_Softaware
{
    public partial class adminprofile : System.Web.UI.Page
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
                if (position != "Admin")
                {
                    Response.Redirect("login.aspx");
                }
            }
            string sqlstring = "SELECT * FROM Register WHERE regID= @UserID";
            SqlCommand command = new SqlCommand(sqlstring, connection);
            int userID = Convert.ToInt32(Session["UserID"]);
            connection.Open();
            command.Parameters.AddWithValue("@UserID", userID);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                profiledata.InnerText = reader["regUsername"].ToString();
                profiledata1.InnerText = reader["regEmail"].ToString();
                profiledata2.InnerText = reader["regFirstname"].ToString();
                profiledata3.InnerText = reader["regLastname"].ToString();
                profiledata4.InnerText = reader["regPosition"].ToString();
                profiledata5.InnerText = reader["regDepartment"].ToString();
            }

            reader.Close();
            connection.Close();
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(Session["UserID"]);

            string username = username1.Text;
            string email = useremail.Text;
            string firstname = firstname1.Text;
            string lastname = lastname1.Text;
            string department = Request.Form["department"];

            if (username.Length > 50 || firstname.Length > 50 || lastname.Length > 50)
            {
                username1.Text = "";
                firstname1.Text = "";
                lastname1.Text = "";
                string script = "window.alert('Input too long!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }

            if (username != "")
            {
                string pattern = "^[a-zA-Z]+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(username))
                {
                    username1.Text = "";
                    firstname1.Text = "";
                    lastname1.Text = "";
                    string script = "window.alert('Please enter valid character!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    return;
                }
                string sqlstring = "UPDATE Register SET regUsername=@regUsername WHERE regID=@regID";
                SqlCommand command = new SqlCommand(sqlstring, connection);
                connection.Open();
                command.Parameters.AddWithValue("@regUsername", username);
                command.Parameters.AddWithValue("@regID", userID);
                command.ExecuteNonQuery();
                connection.Close();
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
                string sqlstring = "UPDATE Register SET regEmail=@regEmail WHERE regID=@regID";
                SqlCommand command = new SqlCommand(sqlstring, connection);
                command.Parameters.AddWithValue("@regEmail", email);
                command.Parameters.AddWithValue("@regID", userID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            if (firstname != "")
            {
                string pattern = "^[a-zA-Z]+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(firstname))
                {
                    username1.Text = "";
                    firstname1.Text = "";
                    lastname1.Text = "";
                    string script = "window.alert('Please enter valid character!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    return;
                }
                string sqlstring = "UPDATE Register SET regFirstname=@regFirstname WHERE regID=@regID";
                SqlCommand command = new SqlCommand(sqlstring, connection);
                connection.Open();
                command.Parameters.AddWithValue("@regFirstname", firstname);
                command.Parameters.AddWithValue("@regID", userID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            if (lastname != "")
            {
                string pattern = "^[a-zA-Z]+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(lastname))
                {
                    username1.Text = "";
                    firstname1.Text = "";
                    lastname1.Text = "";
                    string script = "window.alert('Please enter valid character!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    return;
                }
                string sqlstring = "UPDATE Register SET regLastname=@regLastname WHERE regID=@regID";
                SqlCommand command = new SqlCommand(sqlstring, connection);
                connection.Open();
                command.Parameters.AddWithValue("@regLastname", lastname);
                command.Parameters.AddWithValue("@regID", userID);
                command.ExecuteNonQuery();
                command.CommandTimeout = 120;
                connection.Close();
            }
            if (department != "null")
            {
                string sqlstring = "UPDATE Register SET regDepartment=@regDepartment WHERE regID=@regID";
                SqlCommand command = new SqlCommand(sqlstring, connection);
                connection.Open();
                command.Parameters.AddWithValue("@regDepartment", department);
                command.Parameters.AddWithValue("@regID", userID);
                command.ExecuteNonQuery();
                command.CommandTimeout = 120;
                connection.Close();
            }

            Response.Redirect(Request.RawUrl);
        }
    }
}