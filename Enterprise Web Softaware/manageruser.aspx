<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manageruser.aspx.cs" Inherits="Enterprise_Web_Softaware.manageruser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Users</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/css/all.min.css"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/js/bootstrap.bundle.min.js" rel="stylesheet" />
    <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/css/bootstrap.min.css' rel='stylesheet'/>
    <link href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css' rel='stylesheet'/>
    <script type='text/javascript' src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js'></script>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;900&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Zen+Tokyo+Zoo&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="admincategories.css"/>
</head>
<body>
    <nav>
         <div class="logo">
            Scrumlight Idea
         </div>
         <input type="checkbox" id="click"/>
         <label for="click" class="menu-btn">
         <i class="fas fa-bars"></i>
         </label>
         <ul>
            <li><a href="managerhome.aspx">Home</a></li>
            <li><a href="managerdashboard.aspx">Dashboard</a></li>
            <li><a href="managercategory.aspx">Categories</a></li>
            <li><a class="active" href="manageruser.aspx">User</a></li>
            <li><a href="managerprofile.aspx">Profile</a></li>
            <li><a href="logout.aspx">Logout</a></li>
         </ul>
      </nav>
    <br />
    <br />      
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/boxicons/2.1.0/css/boxicons.min.css" integrity="sha512-pVCM5+SN2+qwj36KonHToF2p1oIvoU3bsqxphdOIWMYmgr4ZqD3t5DjKvvetKhXGc/ZG5REYTT6ltKfExEei/Q==" crossorigin="anonymous" referrerpolicy="no-referrer" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/MaterialDesign-Webfont/5.3.45/css/materialdesignicons.css" integrity="sha256-NAxhqDvtY0l4xn+YVa6WjAcmd94NNfttjNsDmNatFVc=" crossorigin="anonymous" />
<form id="Form1" runat="server">
<div class="container">
<div class="row align-items-center">
<div class="col-md-6">
<div class="mb-3">
<h5 class="card-title"> Users List</h5>
</div>
</div>
</div>
<div class="row">
<div class="col-lg-12">
<div class="">
<div class="table-responsive">
<table class="table project-list-table table-nowrap align-middle table-borderless">
<thead>
<tr>
<th scope="col" class="ps-4" style="width: 50px;">
<div class="form-check font-size-16"><input type="checkbox" class="form-check-input" id="contacusercheck" /><label class="form-check-label" for="contacusercheck"></label></div>
</th>
<th scope="col">Name</th>
<th scope="col">Email</th>
<th scope="col">Password</th>
<th scope="col">Position</th>
<th scope="col">Department</th>
<th scope="col" style="width: 200px;">Action</th>
</tr>
</thead>
<tbody>
    <asp:Repeater ID="repeatUser" runat="server">
                <ItemTemplate>
<tr>
<th scope="row" class="ps-4">
<div class="form-check font-size-16"><input type="checkbox" class="form-check-input" id="contacusercheck1" /><label class="form-check-label" for="contacusercheck1"></label></div>
</th>
<td><a class="text-body"><%# Eval("regUsername") %></a></td>
<td><%# Eval("regEmail") %></td>
<td><%# Eval("regPassword") %></td>
<td><%# Eval("regPosition") %></td>
<td><%# Eval("regDepartment") %></td>
<td>
<ul class="list-inline mb-0">
<li class="list-inline-item">
</li><asp:LinkButton runat="server" CommandArgument='<%#Eval("regID")%>' id="editTool" title="Edit" class="px-2 text-primary" OnClick="editToolButton_Click" OnClientClick='<%# "return showEditModal(" + Eval("regID") + ");" %>'><i class='bx bx-pencil font-size-18'></i></asp:LinkButton>

<li class="list-inline-item">
    <asp:LinkButton runat="server" CommandArgument='<%#Eval("regID")%>' id="deleteTool" title="Delete" class="px-2 text-danger" OnClick="deleteButton_Click" OnClientClick='<%# "return showDeleteModal(" + Eval("regID") + ");" %>'><i class="bx bx-trash-alt font-size-18"></i></asp:LinkButton>
</li>
        <div id='<%# "editModal" + Eval("regID") %>' class="modal fade" role="dialog">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title">Edit User</h4>
          <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
      </div>
      <div class="modal-body">
          <label>User Name</label>
          <br />
      <input placeholder='<%#Eval("regUsername") %>' type="text" id="inputUsername" name='inputName<%# Eval("regID") %>'/>
          <br />
          <label>Email</label>
          <br />
      <input placeholder='<%#Eval("regEmail") %>' type="email" id="inputEmail" name='inputEmail<%# Eval("regID") %>'/>
          <script>
              function validateEmail() {
                  var emailInput = document.getElementById("inputEmail").value;
                  if (emailInput !== '' && !isValidEmail(emailInput)) {
                      alert("Please enter correct email");
                  }
              }

              function isValidEmail(email) {
                  var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                  return emailRegex.test(email);
              }
                     </script>
      <br />
          <label>Password</label>
          <br />
      <input placeholder='<%#Eval("regPassword") %>' type="text" id="inputPassword" name='inputPassword<%# Eval("regID") %>'/>
          <br />
          <label>Change Position</label>
          <br />
      <select name='position<%# Eval("regID") %>' id='<%# "position" + Eval("regID") %>'>
          <option value="null">Select Position</option>
          <option value="Admin">Admin</option>
    <option value="QA Coordinator">QA Coordinator</option>
    <option value="Staff">Staff</option>
  </select>
      <br />
      <label>Change Department</label>
          <br />
      <select name='department<%# Eval("regID") %>' id='<%# "department" + Eval("regID") %>'>
          <option value="null">Select Department</option>
    <option value="Support">Support</option>
    <option value="Business & Marketing">Business & Marketing</option>
          <option value="Human Resources">Human Resources</option>
          <option value="Academic">Academic</option>
  </select>
      </div>
      <div class="modal-footer">
        <button runat="server" type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
        <asp:Button runat="server" Text="Edit" type="button" CommandArgument='<%#Eval("regID")%>' class="btn btn-primary" OnClick="editButton_Click" ID="edit" />
      </div>
    </div>
  </div>
</div>
    <div id='<%# "deleteModal" + Eval("regID") %>' class="modal fade" role="dialog">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title">Delete User</h4>
          <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
      </div>
      <div class="modal-body">
      <label>Are you sure to delete this user?</label>
      </div>
      <div class="modal-footer">
        <button runat="server" type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
        <asp:Button runat="server" Text="Delete" type="button" CommandArgument='<%#Eval("regID")%>' class="btn btn-primary" OnClick="confirmButton_Click" ID="confrimDelete" />
      </div>
    </div>
  </div>
</div>
    <script>
        function showDeleteModal(regID) {
            $('#deleteModal' + regID).modal('show');
            return false;
        }
    </script>
    <script>
        function showEditModal(regID) {
            $('#editModal' + regID).modal('show');
            return false;
        }
    </script>
 </ul>
</td>
</tr>
</ItemTemplate>
        </asp:Repeater>
</tbody>
</table>
</div>
</div>
</div>
</div>
		</div>
    </form>

<script data-cfasync="false" src="/cdn-cgi/scripts/5c5dd728/cloudflare-static/email-decode.min.js"></script>
<script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
<script type="text/javascript"></script>
<script type='text/javascript' src='https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/js/bootstrap.bundle.min.js'></script>
<script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
<script src="https://netdna.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script type='text/javascript' src='#'></script>
    <script type='text/javascript' src='#'></script>
    <script type='text/javascript' src='#'></script>
    <script type='text/javascript' src='#'></script>
    <script type='text/javascript'>var myLink = document.querySelector('a[href="#"]');
        myLink.addEventListener('click', function (e) {
            e.preventDefault();
        });</script>
</body>
</html>

