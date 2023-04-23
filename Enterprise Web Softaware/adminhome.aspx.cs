using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Compression;

namespace Enterprise_Web_Softaware
{
    public partial class adminhome : System.Web.UI.Page
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

            string query1 = "UPDATE Posts SET postClosed = 1 " +
                "WHERE postCategory IN (SELECT categoryName FROM Category WHERE finalClosureDate < GETDATE())";
            SqlCommand command1 = new SqlCommand(query1, connection);
            connection.Open();
            int rowsAffected1 = command1.ExecuteNonQuery();
            connection.Close();

            string query = "UPDATE Category SET closed = 1 WHERE closureDate < GETDATE() AND closed = 0";
            SqlCommand commandU = new SqlCommand(query, connection);
            connection.Open();
            int rowsAffected = commandU.ExecuteNonQuery();
            connection.Close();

            string query2 = "UPDATE Category SET categoryTotalView = " +
                "CASE WHEN (SELECT SUM(postViews) FROM Posts WHERE postCategory = Category.categoryName) IS NULL OR " +
                "(SELECT SUM(postViews) FROM Posts WHERE postCategory = Category.categoryName) = 0 THEN 0 " +
                "ELSE (SELECT SUM(postViews) FROM Posts WHERE postCategory = Category.categoryName) END";
            SqlCommand commandU2 = new SqlCommand(query2, connection);
            connection.Open();
            int rowsAffected2 = commandU2.ExecuteNonQuery();
            connection.Close();

            string select = "SELECT categoryName, categoryPostNumber FROM Category";
            SqlCommand command = new SqlCommand(select, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<string> xValues = new List<string>();
            List<int> yValues = new List<int>();

            while (reader.Read())
            {
                string categoryName = reader["categoryName"].ToString();
                string encodedCategoryName = HttpUtility.JavaScriptStringEncode(categoryName);
                xValues.Add(encodedCategoryName);
                yValues.Add((int)reader["categoryPostNumber"]);
            }
            connection.Close();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script>");
            stringBuilder.Append("var xValues = [");
            foreach (string xValue in xValues)
            {
                stringBuilder.Append("\"" + xValue + "\",");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append("];");
            stringBuilder.Append("var yValues = [");
            foreach (int yValue in yValues)
            {
                stringBuilder.Append(yValue + ",");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append("];");
            stringBuilder.Append("</script>");

            LiteralControl literal = new LiteralControl();
            literal.Text = stringBuilder.ToString();
            Page.Header.Controls.Add(literal);

            string select2 = "SELECT departmentName, totalPostNumber FROM Department;";
            SqlCommand command2 = new SqlCommand(select2, connection);
            connection.Open();
            SqlDataReader reader2 = command2.ExecuteReader();
            List<string> xValues2 = new List<string>();
            List<int> yValues2 = new List<int>();

            while (reader2.Read())
            {
                string department = reader2["departmentName"].ToString();
                string encodedDepartment = HttpUtility.JavaScriptStringEncode(department);
                xValues2.Add(encodedDepartment);
                yValues2.Add((int)reader2["totalPostNumber"]);
            }
            connection.Close();

            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("<script>");
            stringBuilder2.Append("var xValues2 = [");
            foreach (string xValue2 in xValues2)
            {
                stringBuilder2.Append("\"" + xValue2 + "\",");
            }
            stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
            stringBuilder2.Append("];");
            stringBuilder2.Append("var yValues2 = [");
            foreach (int yValue2 in yValues2)
            {
                stringBuilder2.Append(yValue2 + ",");
            }
            stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
            stringBuilder2.Append("];");
            stringBuilder2.Append("</script>");

            LiteralControl literal2 = new LiteralControl();
            literal2.Text = stringBuilder2.ToString();
            Page.Header.Controls.Add(literal2);

            string sqlS = "SELECT COUNT(*) AS totalUser FROM Register";
            SqlCommand commandS = new SqlCommand(sqlS, connection);
            connection.Open();
            int totalUser;

            SqlDataReader readerS = commandS.ExecuteReader();
            if (readerS.Read())
            {
                totalUser = Convert.ToInt32(readerS["totalUser"]);
                user.Text = totalUser.ToString();
            }

            readerS.Close();
            connection.Close();

            string sqlS2 = "SELECT TOP 1 postContent FROM Posts WHERE postPublishTime <= GETDATE() ORDER BY postPublishTime DESC";
            SqlCommand commandS2 = new SqlCommand(sqlS2, connection);
            connection.Open();

            SqlDataReader readerS2 = commandS2.ExecuteReader();
            if (readerS2.Read())
            {
                string latestPost = readerS2["postContent"].ToString();
                latest.Text = Server.HtmlEncode(latestPost);
            }

            readerS2.Close();
            connection.Close();

            string sqlS3 = "SELECT Posts.postContent FROM Posts " +
                "INNER JOIN (SELECT TOP 1 likepostID, COUNT(*) AS likecount FROM Likes " +
                "WHERE likeStatus = 1 GROUP BY likepostID ORDER BY COUNT(*) DESC) AS popular " +
                "ON Posts.postID = popular.likepostID " +
                "ORDER BY popular.likecount DESC";
            SqlCommand commandS3 = new SqlCommand(sqlS3, connection);
            connection.Open();

            SqlDataReader readerS3 = commandS3.ExecuteReader();
            if (readerS3.Read())
            {
                string popularPost = readerS3["postContent"].ToString();
                popular.Text = Server.HtmlEncode(popularPost);
            }

            readerS3.Close();
            connection.Close();

            string sqlS4 = "SELECT COUNT(*) AS total FROM Posts";
            SqlCommand commandS4 = new SqlCommand(sqlS4, connection);
            connection.Open();

            SqlDataReader readerS4 = commandS4.ExecuteReader();
            if (readerS4.Read())
            {
                string totalPost = readerS4["total"].ToString();
                post.Text = totalPost.ToString();
            }

            readerS4.Close();
            connection.Close();
        }
    }
}