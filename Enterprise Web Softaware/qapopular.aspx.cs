using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace Enterprise_Web_Softaware
{
    public partial class qapopular : System.Web.UI.Page
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
                if (position != "QA Coordinator")
                {
                    Response.Redirect("login.aspx");
                }
            }
            if (!IsPostBack)
            {
                string categorysql = "SELECT categoryName FROM Category WHERE closed = '0'";
                SqlCommand categoryCommand = new SqlCommand(categorysql, connection);
                connection.Open();
                SqlDataReader reader = categoryCommand.ExecuteReader();

                while (reader.Read())
                {
                    selectCategory.Items.Add(new ListItem(reader["categoryName"].ToString(), reader["categoryName"].ToString()));
                }
                connection.Close();

                Repeater repeatPost = (Repeater)Page.FindControl("repeatPost");
                Repeater repeatCategory = (Repeater)Page.FindControl("repeatCategory");

                string sqlstring = "SELECT Posts.*, Register.regPosition, Register.regUsername, " +
                "(SELECT likeStatus FROM Likes WHERE likepostID = Posts.postID AND likeregID = @UserID) AS likeStatus, " +
                "(SELECT dislikeStatus FROM Likes WHERE likepostID = Posts.postID AND likeregID = @UserID) AS dislikeStatus, " +
                "(SELECT COUNT(*) FROM Comments WHERE commentpostID = Posts.postID) AS commentCount, " +
                "(SELECT COUNT(*) FROM Likes WHERE likepostID = Posts.postID AND likeStatus = 1) AS likeCount, " +
                "(SELECT COUNT(*) FROM Likes WHERE likepostID = Posts.postID AND dislikeStatus = 1) AS dislikeCount, " +
                "Posts.postClosed, Posts.postID, Posts.postregID, Posts.postContent, Posts.postCategory, Posts.postViews, Posts.postPublishTime, Posts.postAnonymous, Posts.postImage, Posts.postDocument, Posts.documentName, Register.regID " +
                "FROM Posts INNER JOIN Register ON Posts.postregID = Register.regID " +
                "WHERE Posts.postID IN (SELECT TOP 25 likepostID FROM Likes WHERE likeStatus = 1 GROUP BY likepostID ORDER BY COUNT(*) DESC) AND Posts.postClosed = 0 " +
                "GROUP BY Posts.postClosed, Register.regPosition, Posts.postID, Posts.postregID, Register.regUsername, Posts.postContent, Posts.postCategory, Posts.postViews, Posts.postPublishTime, Posts.postAnonymous, Posts.postImage, Posts.postDocument, Posts.documentName, Register.regID;";
                SqlCommand command = new SqlCommand(sqlstring, connection);
                int userID = Convert.ToInt32(Session["UserID"]);
                connection.Open();
                command.Parameters.AddWithValue("@UserID", userID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                int pageSize = 5;

                PagedDataSource pagedData = new PagedDataSource();
                pagedData.DataSource = dataTable.DefaultView;
                pagedData.AllowPaging = true;
                pagedData.PageSize = pageSize;

                int currentPage;
                if (Request.QueryString["page"] != null)
                {
                    currentPage = int.Parse(Request.QueryString["page"]);
                }
                else
                {
                    currentPage = 1;
                }

                pagedData.CurrentPageIndex = currentPage - 1;

                repeatPost.DataSource = pagedData;
                repeatPost.DataBind();

                int totalPage = pagedData.PageCount;
                if (totalPage > 1)
                {
                    Literal litPager = new Literal();
                    litPager.Text = "<div class=\"pager\">";
                    for (int i = 1; i <= totalPage; i++)
                    {
                        if (i == currentPage)
                        {
                            litPager.Text += "<span class=\"current\">" + i.ToString() + "</span>&nbsp;&nbsp;";
                        }
                        else
                        {
                            litPager.Text += "&nbsp;&nbsp;<a href=\"?page=" + i.ToString() + "\">" + i.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;</a>";
                        }
                    }
                    litPager.Text += "</div>";
                    Page.Controls.Add(litPager);
                }

                sqlstring = "SELECT categoryName FROM Category WHERE closed = '0'";
                command = new SqlCommand(sqlstring, connection);

                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                repeatCategory.DataSource = dataTable;
                repeatCategory.DataBind();

                connection.Close();
            }
        }

        protected void dropDown_Click(object sender, EventArgs e)
        {
            HtmlAnchor categoryButton = (HtmlAnchor)sender;

            Repeater repeatPost = (Repeater)Page.FindControl("repeatPost");

            string sqlstring = "SELECT Posts.postID, Register.regPosition, Register.regUsername, " +
                "(SELECT likeStatus FROM Likes WHERE likepostID = Posts.postID AND likeregID = @UserID) AS likeStatus, " +
                "(SELECT dislikeStatus FROM Likes WHERE likepostID = Posts.postID AND likeregID = @UserID) AS dislikeStatus, " +
                "(SELECT COUNT(*) FROM Comments WHERE commentpostID = Posts.postID) AS commentCount, " +
                "(SELECT COUNT(*) FROM Likes WHERE likepostID = Posts.postID AND likeStatus = 1) AS likeCount, " +
                "(SELECT COUNT(*) FROM Likes WHERE likepostID = Posts.postID AND dislikeStatus = 1) AS dislikeCount, " +
                "Posts.postClosed, Posts.postregID, Posts.postContent, Posts.postCategory, Posts.postViews, Posts.postPublishTime, Posts.postAnonymous, Posts.postImage, Posts.postDocument, Posts.documentName, Register.regID " +
                "FROM Posts INNER JOIN Register ON Posts.postregID = Register.regID " +
                "WHERE Posts.postCategory LIKE '%' + @categoryName + '%' AND Posts.postClosed = 0 " +
                "GROUP BY Posts.postClosed, Register.regPosition, Posts.postID, Posts.postregID, Register.regUsername, Posts.postContent, Posts.postCategory, Posts.postViews, Posts.postPublishTime, Posts.postAnonymous, Posts.postImage, Posts.postDocument, Posts.documentName, Register.regID " +
                "ORDER BY Posts.postPublishTime DESC;";
            int userID = Convert.ToInt32(Session["UserID"]);
            string categoryName = categoryButton.Attributes["data-category"];
            SqlCommand command = new SqlCommand(sqlstring, connection);
            connection.Open();
            command.Parameters.AddWithValue("@UserID", userID);
            command.Parameters.AddWithValue("@categoryName", categoryName);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            repeatPost.DataSource = dataTable;
            repeatPost.DataBind();

            connection.Close();
        }

        protected void shareButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "LaunchModal", "$(function() { $('#myModal').modal('show'); });", true);
        }

        protected void postButton_Click(object sender, EventArgs e)
        {
            string smtpServer = "smtp.office365.com";
            int port = 587;
            string subject = "New Post Reminder";
            string body = "Dear QA,\r\n\r\nWe are happy to inform you that a new post has been posted on our forum.\r\n\r\nThank you for using our forum, we look forward to your participation.\r\n\r\nSincerely,\r\nsalute!\r\n\r\nManagement Team";

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
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            string fileName;
            string filePath;
            string folder;

            if (!agreeCheckbox.Checked)
            {
                string script = "window.alert('Please agree to the Terms and Conditions.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }

            int anonymous;
            int userID = Convert.ToInt32(Session["UserID"]);
            string category = selectCategory.SelectedValue;
            string content = inputPost.Text;
            DateTime publishtime = DateTime.Now;

            folder = Server.MapPath("./");
            fileName = fileUpload.PostedFile.FileName;
            fileName = Path.GetFileName(fileName);
            if (fileUpload.Value != "")
            {
                int maxSize = 20 * 1024 * 1024;
                if (fileUpload.PostedFile.ContentLength > maxSize)
                {
                    string script = "window.alert('File size must not exceed 20 MB.Please upload a smaller file.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    return;
                }
                string[] validImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                if (validImageExtensions.Contains(Path.GetExtension(fileName).ToLower()))
                {
                    if (!Directory.Exists(folder + "/images/"))
                    {
                        Directory.CreateDirectory(folder + "/images/");
                    }
                    filePath = folder + "/images/" + fileName;
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                        string script = "window.alert('Something Error!Please try again!');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                        return;
                    }
                    else
                    {
                        fileUpload.PostedFile.SaveAs(filePath);
                        byte[] image = System.IO.File.ReadAllBytes(filePath);
                        if (!anonymousCheckbox.Checked)
                        {
                            anonymous = 0;
                            string imageHex = image != null ? BitConverter.ToString(image).Replace("-", "") : "";
                            string sqlstring = "INSERT INTO Posts (postCategory, postContent,postPublishTime, postViews, postAnonymous, postImage, postClosed, postregID) VALUES (@postCategory, @postContent, @postPublishTime, '0', @postAnonymous, CONVERT(varbinary(max), 0x" + imageHex + "), '0', @postregID)";
                            SqlCommand command = new SqlCommand(sqlstring, connection);
                            command.CommandTimeout = 120;
                            connection.Open();
                            command.Parameters.AddWithValue("@postCategory", category);
                            command.Parameters.AddWithValue("@postContent", content);
                            command.Parameters.AddWithValue("@postPublishTime", publishtime);
                            command.Parameters.AddWithValue("@postAnonymous", anonymous);
                            command.Parameters.AddWithValue("@postregID", userID);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                        else
                        {
                            anonymous = 1;
                            string imageHex = image != null ? BitConverter.ToString(image).Replace("-", "") : "";
                            string sqlstring = "INSERT INTO Posts (postCategory, postContent,postPublishTime, postViews, postAnonymous, postImage, postClosed, postregID) VALUES ( @postCategory, @postContent, @postPublishTime, '0', @postAnonymous, CONVERT(varbinary(max), 0x" + imageHex + "), '0', @postregID)";
                            SqlCommand command = new SqlCommand(sqlstring, connection);
                            command.CommandTimeout = 120;
                            connection.Open();
                            command.Parameters.AddWithValue("@postCategory", category);
                            command.Parameters.AddWithValue("@postContent", content);
                            command.Parameters.AddWithValue("@postPublishTime", publishtime);
                            command.Parameters.AddWithValue("@postAnonymous", anonymous);
                            command.Parameters.AddWithValue("@postregID", userID);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    string insertsql = "UPDATE Category SET categoryPostNumber = categoryPostNumber + 1 WHERE categoryName=@categoryName ";
                    SqlCommand commandInsert = new SqlCommand(insertsql, connection);
                    connection.Open();
                    commandInsert.Parameters.AddWithValue("@categoryName", category);
                    commandInsert.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    if (!Directory.Exists(folder + "/documents/"))
                    {
                        Directory.CreateDirectory(folder + "/documents/");
                    }
                    filePath = folder + "/documents/" + fileName;
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                        string script = "window.alert('Something Error!Please try again!');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                        return;
                    }
                    else
                    {
                        fileUpload.PostedFile.SaveAs(filePath);
                        byte[] document = System.IO.File.ReadAllBytes(filePath);
                        if (!anonymousCheckbox.Checked)
                        {
                            anonymous = 0;
                            string documentHex = document != null ? BitConverter.ToString(document).Replace("-", "") : "";
                            string sqlstring = "INSERT INTO Posts (postCategory, postContent,postPublishTime, postViews, postAnonymous, postDocument, documentName, postClosed, postregID) VALUES ( @postCategory, @postContent, @postPublishTime, '0', @postAnonymous, CONVERT(varbinary(max), 0x" + documentHex + "), @documentName, '0', @postregID)";
                            SqlCommand command = new SqlCommand(sqlstring, connection);
                            command.CommandTimeout = 120;
                            connection.Open();
                            command.Parameters.AddWithValue("@postCategory", category);
                            command.Parameters.AddWithValue("@postContent", content);
                            command.Parameters.AddWithValue("@postPublishTime", publishtime);
                            command.Parameters.AddWithValue("@postAnonymous", anonymous);
                            command.Parameters.AddWithValue("@documentName", fileName);
                            command.Parameters.AddWithValue("@postregID", userID);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                        else
                        {
                            anonymous = 1;
                            string documentHex = document != null ? BitConverter.ToString(document).Replace("-", "") : "";
                            string sqlstring = "INSERT INTO Posts (postCategory, postContent,postPublishTime, postViews, postAnonymous, postDocument, documentName, postClosed, postregID) VALUES ( @postCategory, @postContent, @postPublishTime, '0', @postAnonymous, CONVERT(varbinary(max), 0x" + documentHex + "), @documentName, '0', @postregID)";
                            SqlCommand command = new SqlCommand(sqlstring, connection);
                            command.CommandTimeout = 120;
                            connection.Open();
                            command.Parameters.AddWithValue("@postCategory", category);
                            command.Parameters.AddWithValue("@postContent", content);
                            command.Parameters.AddWithValue("@postPublishTime", publishtime);
                            command.Parameters.AddWithValue("@postAnonymous", anonymous);
                            command.Parameters.AddWithValue("@documentName", fileName);
                            command.Parameters.AddWithValue("@postregID", userID);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    string insertsql = "UPDATE Category SET categoryPostNumber = categoryPostNumber + 1 WHERE categoryName=@categoryName ";
                    SqlCommand commandInsert = new SqlCommand(insertsql, connection);
                    connection.Open();
                    commandInsert.Parameters.AddWithValue("@categoryName", category);
                    commandInsert.ExecuteNonQuery();
                    connection.Close();
                }
            }
            else
            {
                if (!anonymousCheckbox.Checked)
                {
                    anonymous = 0;
                    string sqlstring = "INSERT INTO Posts (postCategory, postContent,postPublishTime, postViews, postAnonymous, postClosed, postregID) VALUES ( @postCategory, @postContent, @postPublishTime, '0', @postAnonymous, '0', @postregID)";
                    SqlCommand command = new SqlCommand(sqlstring, connection);
                    command.CommandTimeout = 120;
                    connection.Open();
                    command.Parameters.AddWithValue("@postCategory", category);
                    command.Parameters.AddWithValue("@postContent", content);
                    command.Parameters.AddWithValue("@postPublishTime", publishtime);
                    command.Parameters.AddWithValue("@postAnonymous", anonymous);
                    command.Parameters.AddWithValue("@postregID", userID);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    anonymous = 1;
                    string sqlstring = "INSERT INTO Posts (postCategory, postContent,postPublishTime, postViews, postAnonymous, postClosed, postregID) VALUES ( @postCategory, @postContent, @postPublishTime, '0', @postAnonymous, '0', @postregID)";
                    SqlCommand command = new SqlCommand(sqlstring, connection);
                    command.CommandTimeout = 120;
                    connection.Open();
                    command.Parameters.AddWithValue("@postCategory", category);
                    command.Parameters.AddWithValue("@postContent", content);
                    command.Parameters.AddWithValue("@postPublishTime", publishtime);
                    command.Parameters.AddWithValue("@postAnonymous", anonymous);
                    command.Parameters.AddWithValue("@postregID", userID);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                string check = "SELECT regDepartment FROM Register WHERE regID=@regID";
                SqlCommand checkC = new SqlCommand(check, connection);
                connection.Open();
                checkC.Parameters.AddWithValue("@regID", userID);
                checkC.ExecuteNonQuery();
                SqlDataReader reader = checkC.ExecuteReader();
                if (reader.Read())
                {
                    string department = (reader["regDepartment"]).ToString();
                    if (department != "N/A")
                    {
                        reader.Close();
                        string insertsql = "UPDATE Category SET categoryPostNumber = categoryPostNumber + 1 WHERE categoryName=@categoryName ";
                        SqlCommand commandInsert = new SqlCommand(insertsql, connection);
                        commandInsert.Parameters.AddWithValue("@categoryName", category);
                        commandInsert.ExecuteNonQuery();

                        string updateDep = "UPDATE Department SET totalPostNumber = totalPostNumber + 1 " +
                            "WHERE departmentName = (SELECT Register.regDepartment FROM Register WHERE regID=@regID);";
                        SqlCommand commandDep = new SqlCommand(updateDep, connection);
                        commandDep.Parameters.AddWithValue("@regID", userID);
                        commandDep.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }

            inputPost.Text = "";
            agreeCheckbox.Checked = false;
            anonymousCheckbox.Checked = false;
            Response.Redirect("qahome.aspx");
        }

        private List<string> GetAdminEmails()
        {
            List<string> adminEmails = new List<string>();
            string sql = "SELECT regEmail FROM Register WHERE regPosition = 'QA Coordinator'";

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

        protected void repeatPost_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repeatImage = e.Item.FindControl("repeatImage") as Repeater;
                Repeater repeatDocument = e.Item.FindControl("repeatDocument") as Repeater;
                Repeater repeatComments = e.Item.FindControl("repeatComments") as Repeater;

                int postID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "postID"));

                string sqlstring = "SELECT postImage FROM Posts WHERE postID = @postID";
                SqlCommand command = new SqlCommand(sqlstring, connection);
                command.Parameters.AddWithValue("@postID", postID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                repeatImage.DataSource = dataTable;
                repeatImage.DataBind();

                sqlstring = "SELECT postDocument, documentName FROM Posts WHERE postID = @postID";
                command = new SqlCommand(sqlstring, connection);
                command.Parameters.AddWithValue("@postID", postID);

                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                repeatDocument.DataSource = dataTable;
                repeatDocument.DataBind();

                sqlstring = "SELECT Register.regPosition, commentAnonymous, commentContent, commentPublishTime, regUsername FROM Comments, Register WHERE Comments.commentpostID = @postID AND Comments.commentregID = Register.regID";
                command = new SqlCommand(sqlstring, connection);
                command.Parameters.AddWithValue("@postID", postID);

                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                repeatComments.DataSource = dataTable;
                repeatComments.DataBind();
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            LinkButton delete = (LinkButton)(sender);
            int postID = Convert.ToInt32(delete.CommandArgument);

            string deleteQuery = "DELETE FROM Likes WHERE likepostID = @postID";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
            deleteCommand.Parameters.AddWithValue("@postID", postID);
            connection.Open();
            deleteCommand.ExecuteNonQuery();

            string deleteQuery2 = "DELETE FROM Comments WHERE commentpostID = @postID";
            SqlCommand deleteCommand2 = new SqlCommand(deleteQuery2, connection);
            deleteCommand2.Parameters.AddWithValue("@postID", postID);
            deleteCommand2.ExecuteNonQuery();

            string deleteQuery3 = "DELETE FROM Posts WHERE postID = @postID";
            SqlCommand deleteCommand3 = new SqlCommand(deleteQuery3, connection);
            deleteCommand3.Parameters.AddWithValue("@postID", postID);
            deleteCommand3.ExecuteNonQuery();

            connection.Close();

            Response.Redirect("qapopular.aspx");
        }

        protected void likeButton_Click(object sender, EventArgs e)
        {
            ImageButton like = (ImageButton)sender;

            int userID = Convert.ToInt32(Session["UserID"]);
            int postID = Convert.ToInt32(like.CommandArgument);

            string checkLike = "SELECT COUNT(*) FROM Likes WHERE likepostID=@postID AND likeregID=@userID";
            SqlCommand checkLikeCommand = new SqlCommand(checkLike, connection);
            connection.Open();
            checkLikeCommand.Parameters.AddWithValue("@postID", postID);
            checkLikeCommand.Parameters.AddWithValue("@userID", userID);
            int count = Convert.ToInt32(checkLikeCommand.ExecuteScalar());

            if (count > 0)
            {
                string checkLikeStatus = "SELECT likeStatus FROM Likes WHERE likepostID=@postID AND likeregID=@userID";
                SqlCommand checkLikeStatusCommand = new SqlCommand(checkLikeStatus, connection);
                checkLikeStatusCommand.Parameters.AddWithValue("@postID", postID);
                checkLikeStatusCommand.Parameters.AddWithValue("@userID", userID);
                int likeStatus = Convert.ToInt32(checkLikeStatusCommand.ExecuteScalar());

                if (likeStatus == 1)
                {
                    string updateLike = "UPDATE Likes SET likeStatus='0', dislikeStatus='0' WHERE likepostID=@postID AND likeregID=@userID";
                    SqlCommand updateCommand = new SqlCommand(updateLike, connection);
                    updateCommand.Parameters.AddWithValue("@postID", postID);
                    updateCommand.Parameters.AddWithValue("@userID", userID);
                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    string updateLike = "UPDATE Likes SET likeStatus='1', dislikeStatus='0' WHERE likepostID=@postID AND likeregID=@userID";
                    SqlCommand updateCommand = new SqlCommand(updateLike, connection);
                    updateCommand.Parameters.AddWithValue("@postID", postID);
                    updateCommand.Parameters.AddWithValue("@userID", userID);
                    updateCommand.ExecuteNonQuery();
                }
                string insertsql = "UPDATE Posts SET postViews = postViews + 1 WHERE postID=@postID ";
                SqlCommand commandInsert = new SqlCommand(insertsql, connection);
                commandInsert.Parameters.AddWithValue("@postID", postID);
                commandInsert.ExecuteNonQuery();
            }
            else
            {
                string insertLike = "INSERT INTO Likes (likeStatus, dislikeStatus, likepostID, likeregID) VALUES ('1', '0', @postID, @userID)";
                SqlCommand insertCommand = new SqlCommand(insertLike, connection);
                insertCommand.Parameters.AddWithValue("@postID", postID);
                insertCommand.Parameters.AddWithValue("@userID", userID);
                insertCommand.ExecuteNonQuery();

                string insertsql = "UPDATE Posts SET postViews = postViews + 1 WHERE postID=@postID ";
                SqlCommand commandInsert = new SqlCommand(insertsql, connection);
                commandInsert.Parameters.AddWithValue("@postID", postID);
                commandInsert.ExecuteNonQuery();
            }

            connection.Close();

            Response.Redirect("qapopular.aspx");
        }

        protected void dislikeButton_Click(object sender, EventArgs e)
        {
            ImageButton dislike = (ImageButton)sender;

            int userID = Convert.ToInt32(Session["UserID"]);
            int postID = Convert.ToInt32(dislike.CommandArgument);

            string checkLike = "SELECT COUNT(*) FROM Likes WHERE likepostID=@postID AND likeregID=@userID";
            SqlCommand checkLikeCommand = new SqlCommand(checkLike, connection);
            connection.Open();
            checkLikeCommand.Parameters.AddWithValue("@postID", postID);
            checkLikeCommand.Parameters.AddWithValue("@userID", userID);
            int count = Convert.ToInt32(checkLikeCommand.ExecuteScalar());

            if (count > 0)
            {
                string checkdislikeStatus = "SELECT dislikeStatus FROM Likes WHERE likepostID=@postID AND likeregID=@userID";
                SqlCommand checkdislikeStatusCommand = new SqlCommand(checkdislikeStatus, connection);
                checkdislikeStatusCommand.Parameters.AddWithValue("@postID", postID);
                checkdislikeStatusCommand.Parameters.AddWithValue("@userID", userID);
                int dislikeStatus = Convert.ToInt32(checkdislikeStatusCommand.ExecuteScalar());

                if (dislikeStatus == 1)
                {
                    string updatedisLike = "UPDATE Likes SET likeStatus='0', dislikeStatus='0' WHERE likepostID=@postID AND likeregID=@userID";
                    SqlCommand updateCommand = new SqlCommand(updatedisLike, connection);
                    updateCommand.Parameters.AddWithValue("@postID", postID);
                    updateCommand.Parameters.AddWithValue("@userID", userID);
                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    string updatedisLike = "UPDATE Likes SET likeStatus='0', dislikeStatus='1' WHERE likepostID=@postID AND likeregID=@userID";
                    SqlCommand updateCommand = new SqlCommand(updatedisLike, connection);
                    updateCommand.Parameters.AddWithValue("@postID", postID);
                    updateCommand.Parameters.AddWithValue("@userID", userID);
                    updateCommand.ExecuteNonQuery();
                }
                string insertsql = "UPDATE Posts SET postViews = postViews + 1 WHERE postID=@postID ";
                SqlCommand commandInsert = new SqlCommand(insertsql, connection);
                commandInsert.Parameters.AddWithValue("@postID", postID);
                commandInsert.ExecuteNonQuery();
            }
            else
            {
                string insertLike = "INSERT INTO Likes (likeStatus, dislikeStatus, likepostID, likeregID) VALUES ('0', '1', @postID, @userID)";
                SqlCommand insertCommand = new SqlCommand(insertLike, connection);
                insertCommand.Parameters.AddWithValue("@postID", postID);
                insertCommand.Parameters.AddWithValue("@userID", userID);
                insertCommand.ExecuteNonQuery();

                string insertsql = "UPDATE Posts SET postViews = postViews + 1 WHERE postID=@postID ";
                SqlCommand commandInsert = new SqlCommand(insertsql, connection);
                commandInsert.Parameters.AddWithValue("@postID", postID);
                commandInsert.ExecuteNonQuery();
            }

            connection.Close();

            Response.Redirect("qapopular.aspx");
        }

        protected void commentSubmit_Click(object sender, EventArgs e)
        {
            Button submitButton = (Button)sender;
            int postID = Convert.ToInt32(submitButton.CommandArgument);

            string smtpServer = "smtp.office365.com";
            int port = 587;
            string subject = "New Comment Reminder";
            string body = "Dear Author,\r\n\r\nWe are happy to inform you that a new comment has been posted on your Idea.\r\n\r\nThank you for using our forum, we look forward to your participation.\r\n\r\nSincerely,\r\nsalute!\r\n\r\nManagement Team";

            List<string> fromEmails = new List<string>() { "scrumlight.sys9@outlook.com", "scrumlight.sys8@outlook.com", "scrumlight.sys@outlook.com" };

            int currentFromEmailIndex = 0;
            int sendCount = 0;

            var message = new MailMessage();
            message.Subject = subject;
            message.Body = body;

            List<string> authorEmails = GetAuthorEmails(postID);
            foreach (string email in authorEmails)
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            DateTime publishtime = DateTime.Now;
            TextBox commentInput = (TextBox)submitButton.Parent.FindControl("commentInput");
            int anonymous;
            int userID = Convert.ToInt32(Session["UserID"]);
            string commentContent = commentInput.Text.Trim();

            string sqlCheck = "SELECT postCategory FROM Posts WHERE postID=@postID";
            SqlCommand commandCheck = new SqlCommand(sqlCheck, connection);
            connection.Open();
            commandCheck.Parameters.AddWithValue("@postID", postID);
            string categoryName = (string)commandCheck.ExecuteScalar();
            connection.Close();

            RepeaterItem repeaterItem = (RepeaterItem)submitButton.NamingContainer;
            CheckBox checkBox = (CheckBox)repeaterItem.FindControl("checkbox");

            if (checkBox.Checked)
            {
                anonymous = 1;
            }
            else
            {
                anonymous = 0;
            }

            string sqlstring = "INSERT INTO Comments (commentContent, commentPublishTime, commentAnonymous, commentregID, commentpostID) VALUES (@commentContent, @commentPublishTime, @commentAnonymous, @commentregID, @commentpostID)";
            SqlCommand command = new SqlCommand(sqlstring, connection);
            connection.Open();
            command.Parameters.AddWithValue("@commentContent", commentContent);
            command.Parameters.AddWithValue("@commentPublishTime", publishtime);
            command.Parameters.AddWithValue("@commentAnonymous", anonymous);
            command.Parameters.AddWithValue("@commentregID", userID);
            command.Parameters.AddWithValue("@commentpostID", postID);
            command.ExecuteNonQuery();
            connection.Close();

            string insertsql = "UPDATE Category SET categoryTotalComment = categoryTotalComment + 1 WHERE categoryName=@categoryName ";
            SqlCommand commandInsert = new SqlCommand(insertsql, connection);
            commandInsert.Parameters.AddWithValue("@categoryName", categoryName);
            connection.Open();
            commandInsert.ExecuteNonQuery();
            connection.Close();

            Response.Redirect("qapopular.aspx");
        }

        private List<string> GetAuthorEmails(int postID)
        {
            List<string> authorEmails = new List<string>();
            string sql = "SELECT Register.regEmail FROM Register JOIN Posts ON Posts.postregID = Register.regID WHERE Posts.postID=@postID";

            using (SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database1.mdf; Integrated Security = True"))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@postID", postID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string email = reader.GetString(0);
                    authorEmails.Add(email);
                }
                reader.Close();
            }

            return authorEmails;
        }
    }
}