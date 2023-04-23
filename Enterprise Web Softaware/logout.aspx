<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="Enterprise_Web_Softaware.logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <style>
		body {
			font-family: Arial, sans-serif;
			font-size: 16px;
			line-height: 1.6;
			background-color: #f2f2f2;
		}
		
		.container {
			margin: 0 auto;
			padding: 20px;
			max-width: 600px;
			text-align: center;
			background-color: #fff;
			border: 1px solid #ccc;
			border-radius: 10px;
			box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
		}
		
		h1 {
			font-size: 36px;
			margin-top: 0;
		}
		
		button {
			font-size: 20px;
			margin: 10px;
			padding: 10px 20px;
			border: none;
			border-radius: 5px;
			background-color: #4CAF50;
			color: #fff;
			cursor: pointer;
			transition: background-color 0.3s ease;
		}
		
		button:hover {
			background-color: #3e8e41;
		}
		
		@media screen and (max-width: 767px) {
			h1 {
				font-size: 28px;
			}
			
			button {
				font-size: 16px;
				padding: 8px 16px;
			}
		}
	</style>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <title>logout</title>
</head>
<body>
    <form id="form1" runat="server">
		<div class="container">
		<h1>Are you sure you want to logout?</h1>
			<asp:Button ID="logoutButton" runat="server" OnClick="logout_Click" Text="Logout"/>
			<asp:Button ID="cancel" runat="server" OnClientClick="JavaScript:window.history.back(1); return false;" Text="Cancel"/>
	</div>
    </form>
</body>
</html>
