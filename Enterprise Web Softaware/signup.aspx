<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="Enterprise_Web_Softaware.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="Signup.css" rel="stylesheet" /> 
</head>
<body>
    <div class="container">
    <div class="row">
      <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
        <div class="card border-0 shadow rounded-3 my-5">
          <div class="card-body p-4 p-sm-5">
            <h5 class="card-title text-center mb-5 fw-bold fs-10">Sign In</h5>
            <form runat="server">
              <div class="form-floating mb-3">
                <asp:TextBox runat="server" type="text" class="form-control" id="floatingUserName" placeholder="Username" required="required"/>
                <label for="floatingInput">Username</label>
              </div>
              <div class="form-floating mb-3">
                <asp:TextBox runat="server" type="text" class="form-control" id="firstName" placeholder="Firstname" required="required"/>
                <label for="floatingFirstName">First Name</label>
              </div>
              <div class="form-floating mb-3">
                <asp:TextBox runat="server" type="text" class="form-control" id="floatingLastName" placeholder="Lastname" required="required"/>
                <label for="floatingInput">Last Name</label>
              </div>
              <div class="form-floating mb-3">
                <asp:TextBox runat="server" type="email" class="form-control" id="floatingInput" placeholder="name@example.com" required="required"/>
                <label for="floatingInput">Email address</label>
              </div>
              <div class="form-floating mb-3">
                <asp:TextBox runat="server" type="password" class="form-control" id="floatingPassword" placeholder="Password" required="required"/>
                  <label for="floatingPassword">Password</label>
                <asp:label runat="server" ForeColor="Brown" id="passwordLabel" Visible="false">Password</asp:label>
              </div>
              <div class="form-floating mb-3">
                <asp:TextBox runat="server" type="password" class="form-control" id="floatingConfirmPassword" placeholder="Password" required="required"/>
                <label for="floatingPassword">ConfirmPassword</label>
                  <asp:label runat="server" ForeColor="Brown" id="passwordReLabel" Visible="false">Password</asp:label>
              </div>
              <div class="d-grid">
                <asp:Button runat="server" text="Sign Up" OnClick="signUpButton_Click" class="btn btn-primary btn-signup text-uppercase fw-bold" formaction="signup.aspx" type="submit"></asp:Button>
              </div>
              <hr class="my-4"/>
              <div class="hint-text">Already have an account? <a href="login.aspx">Login here</a></div>
             </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</body>
</html>
