<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Enterprise_Web_Softaware.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="Login.css" rel="stylesheet" /> 
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
   
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
                <asp:TextBox runat="server" type="email" class="form-control" id="floatingInput" placeholder="name@example.com" required="required"/>
                <label for="floatingInput">Email address</label>
              </div>
              <div class="form-floating mb-3">
                <asp:TextBox runat="server" type="password" class="form-control" id="floatingPassword" placeholder="Password" required="required"/>
                <label for="floatingPassword">Password</label>
                  <asp:label runat="server" ForeColor="Brown" id="passwordLabel" Visible="false">Password</asp:label>
              </div>
              <div class="d-grid">
                <asp:Button runat="server" text="Sign In" class="btn btn-primary btn-login text-uppercase fw-bold" type="submit" OnClick="signInButton_Click"></asp:Button>
              </div>
                <asp:LinkButton runat="server" Text="Forgot Password? Contact Manager." Class="btn-link float-end text-danger" OnClick="forgot_Click"></asp:LinkButton>
             </form>
              <form action="signup.aspx">
              <hr class="my-4"/>
                <div class="d-grid">
                <button class="btn btn-primary btn-signup text-uppercase fw-bold" formaction="signup.aspx" type="submit">Sign Up</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</body>
</html>
