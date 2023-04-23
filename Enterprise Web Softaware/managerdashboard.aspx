<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managerdashboard.aspx.cs" Inherits="Enterprise_Web_Softaware.managerdashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Home</title>
  <meta charset="UTF-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css"/>
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/css/all.min.css"/>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/js/bootstrap.bundle.min.js" rel="stylesheet" />
    <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/css/bootstrap.min.css' rel='stylesheet'/>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>
        <script type='text/javascript' src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js'></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css" rel="stylesheet"/>
  <link rel="stylesheet" href="admindashboard.css"/>

</head>
<body>
    <div>
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
            <li><a class="active" href="managerdashboard.aspx">Dashboard</a></li>
            <li><a href="managercategory.aspx">Categories</a></li>
            <li><a href="manageruser.aspx">User</a></li>
            <li><a href="managerprofile.aspx">Profile</a></li>
            <li><a href="logout.aspx">Logout</a></li>
         </ul>
      </nav>
    </div>
    <form id="Form1" runat="server">
    <div class="container-fluid">
        <div class="row">

          <div class="col-lg-6 col-md-6 p-2">
            <div class="card border-primary rounded-0">
              <div class="card-header bg-primary rounded-0">
                <h5 class="card-title text-white mb-1">Total Users</h5>
              </div>
              <div class="card-body">
                <h1 class="display-4 font-weight-bold text-primary text-center">
                    <asp:Label ID="user" runat="server" Text="0" /> </h1>
              </div>
            </div>
          </div>

          <div class="col-lg-6 col-md-6 p-2">
            <div class="card border-success rounded-0">
              <div class="card-header bg-success rounded-0">
                <h5 class="card-title text-white mb-1">Latest Idea</h5>
              </div>
              <div class="card-body">
                <h1 class="display-4 font-weight-bold text-success text-center">
                    <asp:Label ID="latest" runat="server" Text="0" />
                </h1>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-lg-6 col-md-6 p-2">
            <div class="card border-primary rounded-0">
              <div class="card-header bg-primary rounded-0">
                <h5 class="card-title text-white mb-1">Most Popular Idea</h5>
              </div>
              <div class="card-body">
                <h1 class="display-4 font-weight-bold text-primary text-center">
                    <asp:Label ID="popular" runat="server" Text="0" />
                </h1>
              </div>
            </div>
          </div>
          <div class="col-lg-6 col-md-6 p-2">
            <div class="card border-success rounded-0">
              <div class="card-header bg-success rounded-0">
                <h5 class="card-title text-white mb-1">Total Idea Post</h5>
              </div>
              <div class="card-body">
                <h1 class="display-4 font-weight-bold text-success text-center">
                    <asp:Label ID="post" runat="server" Text="0" />
                </h1>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-lg-6 col-md-6 p-2">
            <div class="card border-primary rounded-0">
              <div class="card-header bg-primary rounded-0">
                <h5 class="card-title text-white mb-1">Total Post of Category</h5>
              </div>
              <div class="card-body">
                <canvas id="myChart" style="width:100%;max-width:900px"></canvas>
                  <script>
                      var ctx = document.getElementById("myChart").getContext("2d");
                      var myChart = new Chart(ctx, {
                          type: 'bar',
                          data: {
                              labels: xValues,
                              datasets: [{
                                  label: 'Category with Total Post',
                                  data: yValues,
                                  backgroundColor: [
                                      'rgba(255, 99, 132, 0.2)',
                                      'rgba(54, 162, 235, 0.2)',
                                      'rgba(255, 206, 86, 0.2)',
                                      'rgba(75, 192, 192, 0.2)',
                                      'rgba(153, 102, 255, 0.2)',
                                      'rgba(255, 159, 64, 0.2)'
                                  ],
                                  borderColor: [
                                      'rgba(255, 99, 132, 1)',
                                      'rgba(54, 162, 235, 1)',
                                      'rgba(255, 206, 86, 1)',
                                      'rgba(75, 192, 192, 1)',
                                      'rgba(153, 102, 255, 1)',
                                      'rgba(255, 159, 64, 1)'
                                  ],
                                  borderWidth: 1
                              }]
                          },
                          options: {
                              scales: {
                                  yAxes: [{
                                      ticks: {
                                          beginAtZero: true
                                      }
                                  }]
                              }
                          }
                      });
                  </script>
              </div>
            </div>
          </div>
          <div class="col-lg-6 col-md-6 p-2">
            <div class="card border-primary rounded-0">
              <div class="card-header bg-primary rounded-0">
                <h5 class="card-title text-white mb-1">Number Ideas by each Department</h5>
              </div>
              <div class="card-body">
                  <canvas id="myChart1" style="width:100%;max-width:900px"></canvas>
                  <script>
                      var ctx = document.getElementById("myChart1").getContext("2d");
                      var myChart = new Chart(ctx, {
                          type: 'line',
                          data: {
                              labels: xValues2,
                              datasets: [{
                                  label: 'Department with Total Ideas',
                                  data: yValues2,
                                  backgroundColor: [
                                      'rgba(255, 99, 132, 0.2)',
                                      'rgba(54, 162, 235, 0.2)',
                                      'rgba(255, 206, 86, 0.2)',
                                      'rgba(75, 192, 192, 0.2)',
                                      'rgba(153, 102, 255, 0.2)',
                                      'rgba(255, 159, 64, 0.2)'
                                  ],
                                  borderColor: [
                                      'rgba(255, 99, 132, 1)',
                                      'rgba(54, 162, 235, 1)',
                                      'rgba(255, 206, 86, 1)',
                                      'rgba(75, 192, 192, 1)',
                                      'rgba(153, 102, 255, 1)',
                                      'rgba(255, 159, 64, 1)'
                                  ],
                                  borderWidth: 1
                              }]
                          },
                          options: {
                              scales: {
                                  yAxes: [{
                                      ticks: {
                                          beginAtZero: true
                                      }
                                  }]
                              }
                          }
                      });
                  </script>
              </div>
            </div>
          </div>
        </div>
        <br />
        <div class="row align-items-center">
<div class="col-md-6">
<div class="mb-3">
<h5 class="card-title"> Category Closed List</h5>
</div>
</div>
</div>
<div class="row">
<div class="col-lg-12">
<div class="">
<div class="table-responsive" style="border: 1px solid #ccc;  background-color: #f5f5f5;">
<table class="table project-list-table table-nowrap align-middle table-borderless">
<thead>
<tr>
<th scope="col" class="ps-4" style="width: 50px;">
<div class="form-check font-size-16"><input type="checkbox" class="form-check-input" id="contacusercheck" /><label class="form-check-label" for="contacusercheck"></label></div>
</th>
<th scope="col">Category Name</th>
<th scope="col">Total Post</th>
<th scope="col">Total View</th>
<th scope="col">Total Comment</th>
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
<td><a class="text-body"><%# Eval("categoryName") %></a></td>
<td><%# Eval("categoryPostNumber") %></td>
<td><%# Eval("categoryTotalView") %></td>
<td><%# Eval("categoryTotalComment") %></td>
<td>
<ul class="list-inline mb-0">
<li class="list-inline-item">
</li><asp:LinkButton runat="server" CommandArgument='<%#Eval("categoryID")%>' id="downloadTool" title="Edit" class="px-2 text-primary" OnClick="downloadButton_Click" ><i class='bi bi-download font-size-18'></i></asp:LinkButton>
    <li class="list-inline-item">
    <asp:LinkButton runat="server" CommandArgument='<%#Eval("categoryID")%>' id="deleteTool" title="Delete" class="px-2 text-danger" OnClick="deleteButton_Click" OnClientClick='<%# "return showDeleteModal(" + Eval("categoryID") + ");" %>'><i class="bi bi-trash font-size-18"></i></asp:LinkButton>
</li>
    <div id='<%# "deleteModal" + Eval("categoryID") %>' class="modal fade" role="dialog">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title">Delete Category</h4>
          <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
      </div>
      <div class="modal-body">
      <label>Please make sure you have Downloaded CSV file before delete!</label>
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
    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://netdna.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
    <script type='text/javascript' src='https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/js/bootstrap.bundle.min.js'></script>
    <script src="script.js"></script>
</body>
</html>

