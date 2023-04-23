<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="Enterprise_Web_Softaware.profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
	<title>Profile</title>
	<meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
	<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css"/>
	<link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css"/>
	<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/js/bootstrap.bundle.min.js" rel="stylesheet" />
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css"/>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
	<link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;900&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Zen+Tokyo+Zoo&display=swap" rel="stylesheet"/>
	<link rel="stylesheet" type="text/css" href="profile.css"/>
               <style type="text/css">
               *{
                   margin: 0px;
                   padding: 0px;
              }

            #login-header{
              text-align: center;
                height: 30px;
               line-height: 30px;
             }

           #login-header a{
                font-size: 24px;
                 text-decoration: none;
                  color: black;
            }
            
             .login{
                 position: absolute;
                     left: 27%;
                top: 37%;
                 width: auto;
                height: auto;
                 z-index: 9999;
                 display: none;
                 flex-direction: column;
                 align-items: center;
                background-color: white;
                left: 50%;
                 margin-left: -256px;
                  margin-top: 20px;
                 border: 1px solid gray;
             }
             
             .login span a{
                  text-decoration: none;
                  border: 1px solid gray;
                  font-size: 12px;
                  color: black;
                  border-radius: 20px;
                  width: 40px;
                  height: 40px;
                  background-color: #fff;
                  position: absolute;
                  top: -20px;
                  right: -20px;
                      padding-top: 10px;
                     padding-left: 5px;
              }

              
              .login-input{
                  margin: 20px 0px 30px 0px;
                  display: flex;
                  flex-direction: row;
              }
              .login-input label{
                    height: 35px;
                    line-height: 35px;
                    padding-left: 10px;
                    font-size: 14px;
              }

              .login-input .textbox{
                  height: 35px;
                line-height: 35px;
                padding-left: 10px;
                font-size: 14px;
              }

                   .loginSubmit {
                       text-align: center;
                       margin-bottom: 10px;
                   }
   </style>
</head>
<body runat="server">
    <form id="form1" runat="server">
	<nav>
         <div class="logo">
            Scrumlight Idea
         </div>
         <input type="checkbox" id="click"/>
         <label for="click" class="menu-btn">
         <i class="fas fa-bars"></i>
         </label>
         <ul>
            <li><a href="home.aspx">Home</a></li>
            <li><a class="active" href="#">profile</a></li>
            <li><a href="logout.aspx">Logout</a></li>
         </ul>
      </nav>
    <section class="main-content">
		<div class="container">
			<h1 class="text-center">Profile</h1>
			<br/>
			<br/>
			
			<div class="row">

                 <div id="login" class="login">
                 <span><a id="closeBtn" href="javascript:void(0)">Close</a></span>

             <div id="login-form">
                 <div class="login-input">
                     <label>User：</label>
                     <asp:TextBox runat="server" type="text" class="textbox" id="username1" placeholder="username"/>
                 </div>
                 
                 <div class="login-input">
                     <label>Email：</label>
                     <asp:TextBox runat="server" type="email" class="textbox" id="useremail" placeholder="name@example.com"/>
                     <script>
                         function validateEmail() {
                             var emailInput = document.getElementById("useremail").value;
                             if (emailInput !== '' && !isValidEmail(emailInput)) {
                                 alert("Please enter correct email");
                             }
                         }


                         function isValidEmail(email) {
                             var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                             return emailRegex.test(email);
                         }
                     </script>
                 </div>

                <div class="login-input">
                     <label>First&nbsp;&nbsp;&nbsp;Name：</label>
                     <asp:TextBox runat="server" type="text" class="textbox" id="firstname1" placeholder="firstname"/>
                 </div>
                 
                 <div class="login-input">
                     <label>Last&nbsp;&nbsp;&nbsp;Name：</label>
                     <asp:TextBox runat="server"  type="text" class="textbox" id="lastname1" placeholder="lastname"/>
                 </div>

                 <div class="login-input">
                     <label>Department：</label>
                     <select name="department" id="selectDepartment">
          <option value="null">Select Department</option>
                    <option value="Business & Marketing">Business & Marketing</option>
                 <option value="Support">Support</option>
                 <option value="Human Resources">Human Resources</option>
                 <option value="Academic">Academic</option>
                         </select>
                 </div>
             </div>

             <input type="submit"  runat="server" id="editSubmit" value="Save" class="loginSubmit" onserverclick="editButton_Click"/>
         </div>

				<div class="col-sm-8 offset-sm-2 col-md-6 offset-md-3 col-lg-4 offset-lg-4">
					<div class="profile-card card rounded-lg shadow p-4 p-xl-5 mb-4 text-center position-relative overflow-hidden">
						<div class="banner"></div>
                        <img src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="" class="img-circle mx-auto mb-3" />
						<h3 class="mb-4">User</h3>
						<div class="text-left mb-4">
							<p class="mb-2" id="user"><i class="fa fa-user mr-2"></i><span id="profiledata" runat="server"></span></p>
							<p class="mb-2" id="email"><i class="fa fa-envelope mr-2"></i><span id="profiledata1" runat="server"></span></p>
							<p class="mb-2" id="first"><i class="fas fa-address-book mr-2"></i><span id="profiledata2" runat="server"></span></p>
							<p class="mb-2" id="last"><i class="far fa-address-book mr-2"></i><span id="profiledata3" runat="server"></span></p>
                            <p class="mb-2" id="position"><i class="fas fa-tag mr-2"></i><span id="profiledata4" runat="server"></span></p>
                            <p class="mb-2" id="department"><i class="fa fa-building mr-2"></i><span id="profiledata5" runat="server"></span></p>
						</div>
						<a href="#edit1" id="adminBtn" class="btn btn-primary btn-lg active" role="button" aria-pressed="true">Edit</a>
					</div>
				</div>
		</div>
            </div>
	</section>
	
	<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
	<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js"></script>
        <script type="text/javascript">
            var login = document.getElementById('login');

            var adminBtn = document.getElementById('adminBtn');
            adminBtn.onclick = function () {
                login.style.display = "flex";
                return false;
            }

            var closeBtn = document.getElementById('closeBtn');
            closeBtn.onclick = function () {
                login.style.display = "none";
                return false;
            }
        </script>
        </form>
</body>

</html>
