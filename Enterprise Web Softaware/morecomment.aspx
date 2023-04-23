<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="morecomment.aspx.cs" Inherits="Enterprise_Web_Softaware.morecomment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
    <link href="https://netdna.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="https://netdna.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/js/bootstrap.bundle.min.js" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css"/>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <link href="home.css" rel="stylesheet" />
</head>
<body>
    <div>
    <nav>
         <div class="logo">
            Scrumlight Idea
         </div>
         <input type="checkbox" id="click" style="display:none;"/>
         <label for="click" class="menu-btn">
         <i class="fas fa-bars"></i>
         </label>
         <ul>
            <li><a class="active" href="#">Home</a></li>
            <li><a href="profile.aspx">profile</a></li>
            <li><a href="logout.aspx">Logout</a></li>
         </ul>
      </nav>
    </div>
<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet"/>
<div class="container bootdey">
<br />
<div class="col-md-12 bootstrap snippets">
<div class="panel">
    <div class="panel-body">  
        <div class="flex-container">
            <div><a id="latestPost" runat="server" href="home.aspx"><img src="Images/picture.png"/>Latest Post</a></div>
            <div><a id="mostLike" runat="server" href="popular.aspx"><img src="Images/like.png"/>Most Popular Ideas</a></div>
            <div><a id="mostComment" runat="server" href="morecomment.aspx"><img src="Images/bubble-chat.png"/>Most Comment Ideas</a></div>
            <div class="dropdown">
                <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">Categories<span class="caret"></span></button>
                <ul class="dropdown-menu">
                     <asp:Repeater ID="repeatCategory" runat="server">
                <ItemTemplate>
                    <li><a id="category" runat="server" href="#" onserverclick="dropDown_Click" data-category='<%# Eval("categoryName") %>'><%# Server.HtmlEncode(Eval("categoryName").ToString()) %></a></li>
                    <li class="divider"></li>
                </ItemTemplate>
                        </asp:Repeater>
    </ul>
  </div>
        </div>
    </div>
</div>
<form runat="server">
<div class="panel">
<div class="panel-body">
<asp:TextBox runat="server" class="form-control" id="inputPost" rows="2" placeholder="What are you thinking?"></asp:TextBox>
<div class="mar-top clearfix">
<asp:Button runat="server" Text="Share" class="btn btn-sm btn-primary pull-right" type="submit" OnClick="shareButton_Click" OnClientClick="return showTermsModal();"></asp:Button>

<div id="termsModal" class="modal fade" role="dialog">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Terms and Conditions</h4>
      </div>
      <div class="modal-body">
          <div class="form-group">
            <label for="agree">Please read and agree to the terms and conditions before posting your content:</label>
            <textarea class="form-control" runat="server" id="termsText" rows="5" readonly="readonly">Welcome to our website. If you continue to browse and use this website, you are agreeing to comply with and be bound by the following terms and conditions of use, which together with our privacy policy govern our relationship with you in relation to this website. If you disagree with any part of these terms and conditions, please do not use our website.

The term 'us' or 'we' refers to the owner of the website. The term 'you' refers to the user or viewer of our website.

The use of this website is subject to the following terms of use:

The content of the pages of this website is for your general information and use only. It is subject to change without notice.

This website uses cookies to monitor browsing preferences. If you do allow cookies to be used, the following personal information may be stored by us for use by third parties.

Neither we nor any third parties provide any warranty or guarantee as to the accuracy, timeliness, performance, completeness or suitability of the information and materials found or offered on this website for any particular purpose. You acknowledge that such information and materials may contain inaccuracies or errors and we expressly exclude liability for any such inaccuracies or errors to the fullest extent permitted by law.

Your use of any information or materials on this website is entirely at your own risk, for which we shall not be liable. It shall be your own responsibility to ensure that any products, services or information available through this website meet your specific requirements.

This website contains material which is owned by or licensed to us. This material includes, but is not limited to, the design, layout, look, appearance and graphics. Reproduction is prohibited other than in accordance with the copyright notice, which forms part of these terms and conditions.

All trademarks reproduced in this website, which are not the property of, or licensed to the operator, are acknowledged on the website.

Unauthorized use of this website may give rise to a claim for damages and/or be a criminal offence.

From time to time, this website may also include links to other websites. These links are provided for your convenience to provide further information. They do not signify that we endorse the website(s). We have no responsibility for the content of the linked website(s).

Your use of this website and any dispute arising out of such use of the website is subject to the laws of Malaysia.</textarea>
          </div>
            <div class="[ form-group ]">
            <input type="checkbox" name="agreeCheckbox" id="agreeCheckbox" autocomplete="off" runat="server"/>
            <div class="[ btn-group ]">
                <label for="agreeCheckbox" class="[ btn btn-default ]">
                    <span class="[ glyphicon glyphicon-ok ]"></span>
                    <span> </span>
                </label>
                <label for="agreeCheckbox" class="[ btn btn-default active ]">
                    I agree to the terms and Conditions
                </label>
            </div>
        </div>
          <div class="[ form-group ]">
            <input type="checkbox" name="anonymousCheckbox" id="anonymousCheckbox" autocomplete="off" runat="server"/>
            <div class="[ btn-group ]">
                <label for="anonymousCheckbox" class="[ btn btn-default ]">
                    <span class="[ glyphicon glyphicon-ok ]"></span>
                    <span> </span>
                </label>
                <label for="anonymousCheckbox" class="[ btn btn-default active ]">
                    Post as anonymous
                </label>
            </div>
        </div>
          <div class="[ form-group ]">
          <asp:DropDownList ID="selectCategory" runat="server" CssClass="btn2"></asp:DropDownList>
              </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <asp:Button runat="server" Text="Post Content" type="button" class="btn btn-primary" OnClick="postButton_Click" ID="postButton" />
      </div>
    </div>
  </div>
</div>

<script>
    function showTermsModal() {
        $('#termsModal').modal('show');
        return false; 
    }
</script>
    <input id="fileUpload" type="file" runat="server" name="fileUpload"/>  
</div>
</div>
</div>
<asp:Repeater ID="repeatPost" runat="server" OnItemDataBound="repeatPost_ItemDataBound">
    <ItemTemplate>        
        <div class="panel">
            <div class="panel-body">
                <div class="media-block">
                    <a class="media-left">
                        <asp:Image ID="imgProfile" runat="server" CssClass="img-circle img-sm" ImageUrl='<%# Eval("regPosition").ToString().Equals("QA Coordinator") ? "https://bootdey.com/img/Content/avatar/avatar2.png" : "https://bootdey.com/img/Content/avatar/avatar1.png" %>' AlternateText="Profile Picture" />
                    </a>
                        <div class="media-body">
                            <div class="mar-btm">
                                <asp:label runat="server" ID="username" class="btn-link text-semibold media-heading box-inline"><%# Eval("postAnonymous").ToString() == "1" ? "Anonymous" : Eval("regUsername") %></asp:label>
                                <p class="text-muted text-sm"><%# Eval("postPublishTime") %></p>
                                <asp:label runat="server" id="postIdLabel" style="display:none;"><%# Eval("postID") %></asp:label>
                            </div>
                            <div class="w3-tag w3-small w3-blue"><%# Server.HtmlEncode(Eval("postCategory").ToString()) %></div>
                        <p><%# Server.HtmlEncode(Eval("postContent").ToString()) %></p>
                            <asp:Repeater id="repeatImage" runat="server">
                                <ItemTemplate>
                                    <%# Eval("postImage") != DBNull.Value ? "<img id='image' class='img-responsive thumbnail' alt='Image' src='data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("postImage")) + "' />" : "" %>
                        </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater id="repeatDocument" runat="server" >
                                <ItemTemplate>             
                                    <%# Eval("postDocument") != DBNull.Value ? "<a href='data:application/octet-stream;base64," + Convert.ToBase64String((byte[])Eval("postDocument")) + "' download='" + Eval("documentName") + "'>" + Eval("documentName") + "</a>" : "" %>
                                </ItemTemplate>
                            </asp:Repeater>
                                    <div class="pad-ver">
                            <div class="btn-group">
                                <div>
                                    <asp:ImageButton id="like" CommandArgument='<%#Eval("postID")%>' Style="width:40px; height:30px" CssClass="btn btn-sm btn-default btn-hover-success" ImageUrl='<%# Eval("likeStatus").ToString() == "1" ? "images/bluelike.png" : "images/like.png" %>' OnClick="likeButton_Click" runat="server" /><span runat="server" id="likecount"><%# Eval("likeCount") %></span>
                                    <asp:ImageButton id="dislike" CommandArgument='<%#Eval("postID")%>' Style="width:40px; height:30px" CssClass="btn btn-sm btn-default btn-hover-success" ImageUrl='<%# Eval("dislikeStatus").ToString() == "1" ? "images/bluedislike.png" : "images/dislike.png" %>' OnClick="dislikeButton_Click" runat="server" /><span runat="server" id="dislikecount"><%# Eval("dislikeCount") %></span>
                                </div>
                            </div>
                            <asp:ImageButton id="comment" Style="width:40px; height:30px" CssClass="btn btn-sm btn-default btn-hover-primary" ImageUrl="images/bubble-chat.png" runat="server"/><span runat="server" id="commentCount"><%# Eval("commentCount") %></span>
                        </div>
                        <hr/>
                            <div id="commentSection" runat="server"></div>
                            <div id="commentForm" runat="server" style="display:block; position:relative;">
                                <div>
                                    <asp:Textbox id="commentInput" style="height:50px; width:100%;" placeholder="Write your comment here..." runat="server"></asp:Textbox>
                                    <asp:Button id="commentSubmit" style="margin-bottom: 10px;" CommandArgument='<%#Eval("postID")%>' text="Submit" type="submit" OnClick="commentSubmit_Click" runat="server" CssClass="submit-button"/>
                                     <asp:Checkbox  runat="server" type="checkbox" name="checkbox" id="checkbox"/><label id="anonymous" name="anonymous">Anonymous</label>
                                </div>
                            </div>
                        <div>
                            <asp:Repeater id="repeatComments" runat="server">
                                <ItemTemplate>
                                    <div class="media-block">
                                        <a class="media-left">
                                            <asp:Image ID="imgProfile" runat="server" CssClass="img-circle img-sm" ImageUrl='<%# Eval("regPosition").ToString().Equals("QA Coordinator") ? "https://bootdey.com/img/Content/avatar/avatar2.png" : "https://bootdey.com/img/Content/avatar/avatar1.png" %>' AlternateText="Profile Picture" />
                                        </a>
                                        <div class="media-body">
                                            <div class="mar-btm">
                                                <a class="btn-link text-semibold media-heading box-inline"><%# Eval("commentAnonymous").ToString() == "1" ? "Anonymous" : Eval("regUsername") %></a>
                                                <p class="text-muted text-sm">
                                                    <i class="fa fa-mobile fa-lg"></i><%# Eval("commentPublishTime") %>
                                                </p>
                                            </div>
                                            <p><%# Server.HtmlEncode(Eval("commentContent").ToString()) %></p>
                                            <hr/>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
</form>
</div>
</div>
<script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
<script src="https://netdna.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
<script type="text/javascript">

</script>
</body>
</html>

