<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admincategories.aspx.cs" Inherits="Enterprise_Web_Softaware.admincategories" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Categories</title>
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
            <li><a href="adminallpost.aspx">Home</a></li>
            <li><a href="adminhome.aspx">Dashboard</a></li>
            <li><a class="active" href="admincategories.aspx">Categories</a></li>
            <li><a href="adminUser.aspx">User</a></li>
            <li><a href="adminprofile.aspx">Profile</a></li>
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
<h5 class="card-title"> Category List</h5>
</div>
</div>
    <div class="col-md-6 text-end">
    <div class="w3-container">
        <asp:Button runat="server" Text="New category" class="btn btn-sm btn-primary pull-right" type="submit" OnClick="newCategoryButton_Click" OnClientClick="return showAddModal();"></asp:Button>
    </div>
  </div>
</div>
    <div id="addModal" class="modal fade" role="dialog">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title">Add Category</h4>
          <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
      </div>
      <div class="modal-body">
      <label>Categories Name</label>
      <input placeholder="Categories Name" type="text" id="inputCategoryName" name="inputCategoryName"/>
      <label>Closure Date</label>
      <div class="postdate">
        <input type="date" id="addClosureDate" name="closureDate"/>
      </div>
      <br />
      <label>Final Closure Date</label>
      <div class="postdate">
        <input type="date" id="addFinalClosureDate" width="270px" name="finalClosureDate"/>
      </div>
      </div>
      <div class="modal-footer">
        <button runat="server" type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
        <asp:Button runat="server" Text="Add" type="button" class="btn btn-primary" OnClick="addButton_Click" ID="add" />
      </div>
    </div>
  </div>
</div>
    <script>
        function showAddModal() {
            $('#addModal').modal('show');
            return false;
        }

        $(document).ready(function () {
            $("[id^='addClosureDate'], [id^='addFinalClosureDate']").change(function () {
                var closureDate = new Date($(this).closest(".modal-content").find("input[id^='addClosureDate']").val());
                var finalClosureDate = new Date($(this).closest(".modal-content").find("input[id^='addFinalClosureDate']").val());
                var maxYear = new Date();
                maxYear.setFullYear(maxYear.getFullYear() + 10);

                if (closureDate < new Date()) {
                    alert("Closure date cannot be earlier than today!");
                    $(this).val("");
                    return;
                }

                if (finalClosureDate < new Date()) {
                    alert("Final closure date cannot be earlier than today!");
                    $(this).val("");
                    return;
                }

                if (closureDate > finalClosureDate) {
                    alert("Final closure date cannot be earlier than closure date!");
                    $(this).val("");
                    return;
                }

                if (closureDate.getFullYear() > maxYear.getFullYear() || finalClosureDate.getFullYear() > maxYear.getFullYear()) {
                    alert("Dates cannot be more than 10 years in the future!");
                    $(this).val("");
                    return;
                }
            });
        });
    </script>

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
<th scope="col">Category Name</th>
<th scope="col">Closure Date</th>
<th scope="col">Final Closure Date</th>
<th scope="col">Last Edit Date</th>
<th scope="col" style="width: 200px;">Action</th>
</tr>
</thead>
<tbody>
    <asp:Repeater ID="repeatCategory" runat="server">
                <ItemTemplate>
<tr>
<th scope="row" class="ps-4">
<div class="form-check font-size-16"><input type="checkbox" class="form-check-input" id="contacusercheck1" /><label class="form-check-label" for="contacusercheck1"></label></div>
</th>
<td><a class="text-body"><%# Server.HtmlEncode(Eval("categoryName").ToString()) %></a></td>
<td><%# Eval("closureDate") %></td>
<td><%# Eval("finalClosureDate") %></td>
<td><%# Eval("lastEditDate") %></td>
<td>
<ul class="list-inline mb-0">
<li class="list-inline-item">
</li><asp:LinkButton runat="server" CommandArgument='<%#Eval("categoryID")%>' id="editTool" title="Edit" class="px-2 text-primary" OnClick="editToolButton_Click" OnClientClick='<%# "return showEditModal(" + Eval("categoryID") + ");" %>'><i class='bx bx-pencil font-size-18'></i></asp:LinkButton>

<li class="list-inline-item">
    <asp:LinkButton runat="server" CommandArgument='<%#Eval("categoryID")%>' id="deleteTool" title="Delete" class="px-2 text-danger" OnClick="deleteButton_Click" ><i class="bx bx-trash-alt font-size-18"></i></asp:LinkButton>
</li>
        <div id='<%# "editModal" + Eval("categoryID") %>' class="modal fade" role="dialog">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title">Edit Category</h4>
          <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
      </div>
      <div class="modal-body">
          <label>Categories Name</label>
          <br />
      <input placeholder='<%#Eval("categoryName") %>' type="text" id="inputCategoryName" name='inputName<%# Eval("categoryID") %>'/>
      <br />
          <label>Closure Date</label>
      <div class="postdate">
        <input type="date" id='<%# "editClosureDate" + Eval("categoryID") %>' name="closure"/>
      </div>
      <br />
      <label>Final Closure Date</label>
      <div class="postdate">
        <input type="date" id='<%# "editFinalClosureDate" + Eval("categoryID") %>' width="270px" name="final"/>
      </div>

      </div>
      <div class="modal-footer">
        <button runat="server" type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
        <asp:Button runat="server" Text="Edit" type="button" CommandArgument='<%#Eval("categoryID")%>' class="btn btn-primary" OnClick="editButton_Click" ID="edit" />
      </div>
    </div>
  </div>
</div>
    <div id='<%# "deleteModal" + Eval("categoryID") %>' class="modal fade" role="dialog">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title">Delete Category</h4>
          <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
      </div>
      <div class="modal-body">
      <label>Sure delete this category?</label>
      </div>
      <div class="modal-footer">
        <button runat="server" type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
        <asp:Button runat="server" Text="Delete" type="button" CommandArgument='<%#Eval("categoryID")%>' class="btn btn-primary" OnClick="confirmButton_Click" ID="confrimDelete" />
      </div>
    </div>
  </div>
</div>
    <script>
        function showDeleteModal(categoryID) {
            $('#deleteModal' + categoryID).modal('show');
            return false;
        }
        </script>
    <script>
        function showEditModal(categoryID) {
            $('#editModal' + categoryID).modal('show');
            return false;
        }

        $(document).ready(function () {
            $("[id^='editClosureDate'], [id^='editFinalClosureDate']").change(function () {
                var closureDate = new Date($(this).closest(".modal-content").find("input[id^='editClosureDate']").val());
                var finalClosureDate = new Date($(this).closest(".modal-content").find("input[id^='editFinalClosureDate']").val());
                var maxYear = new Date();
                maxYear.setFullYear(maxYear.getFullYear() + 10);

                if (closureDate < new Date()) {
                    alert("Closure date cannot be earlier than today!");
                    $(this).val("");
                    return;
                }

                if (finalClosureDate < new Date()) {
                    alert("Final closure date cannot be earlier than today!");
                    $(this).val("");
                    return;
                }

                if (closureDate > finalClosureDate) {
                    alert("Final closure date cannot be earlier than closure date!");
                    $(this).val("");
                    return;
                }

                if (closureDate.getFullYear() > maxYear.getFullYear() || finalClosureDate.getFullYear() > maxYear.getFullYear()) {
                    alert("Dates cannot be more than 10 years in the future!");
                    $(this).val("");
                    return;
                }
            });
        });
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
<script type="text/javascript">

</script>
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
