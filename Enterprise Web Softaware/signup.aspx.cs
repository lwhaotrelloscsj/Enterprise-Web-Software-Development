using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Enterprise_Web_Softaware
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database1.mdf; Integrated Security = True");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void signUpButton_Click(object sender, EventArgs e)
        {
            string email = floatingInput.Text;
            string password = floatingPassword.Text;
            string username = floatingUserName.Text;
            string repassword = floatingConfirmPassword.Text;
            string firstname = firstName.Text;
            string lastname = floatingLastName.Text;
            string position = "Staff";
            string department = "N/A";

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

            if (password.Length < 6 || password.Length > 100)
            {
                passwordLabel.Text = "Please enter Password with more than six character!";
                passwordLabel.Visible = true;
                passwordReLabel.Visible = false;
            }

            if (repassword != password)
            {
                passwordReLabel.Text = "Please enter correct Password!";
                passwordReLabel.Visible = true;
                passwordLabel.Visible = false;
            }
            
            if ((password.Length > 5 && password.Length < 100) && repassword == password)
            {
                string sqlstring = "INSERT INTO Register (regEmail, regPassword, regUsername, regFirstname, regLastname, regPosition, regDepartment) VALUES (@email, @password, @username, @firstname, @lastname, @regPosition, @regDepartment)";
                SqlCommand command = new SqlCommand(sqlstring, connection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@repassword", repassword);
                command.Parameters.AddWithValue("@firstname", firstname);
                command.Parameters.AddWithValue("@lastname", lastname);
                command.Parameters.AddWithValue("@regPosition", position);
                command.Parameters.AddWithValue("@regDepartment", department);
                command.ExecuteNonQuery();
                connection.Close();

                Response.Redirect("Login.aspx");
            }
        }
    }
}