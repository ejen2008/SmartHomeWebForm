<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmartHomeWebForm.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Smart System Home Control</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/index.css" />
</head>
<body>
    <div class="container">
        <header>
            <div class="row">
                <img class="col-xs-3 img-responsive vcenter" id="imageHeader" src="content\SmaryHomeLogo.png" /><!-- 
---------------><h1 class="col-xs-9 vcenter" id="head-list">Smart System Home Controls</h1>
            </div>
        </header>
    </div>
    <main>
        <asp:Panel ID="PanelMain" runat="server" CssClass="container">
            <form id="form1" runat="server">
                <asp:Panel ID="PanelApp" runat="server" CssClass="row">
                    <!-- Секция меню -->
                    <asp:Panel ID="PanelMenu" runat="server" CssClass="col-xs-12 col-md-3 inline-block">
                        <h3 class="text-lef" id ="createNewDev">Создать новое устройство</h3>
                        <asp:TextBox ID="TextBoxNameNewDevice" runat="server" CssClass="btn-block" placeholder="Имя устройства" title="Введите имя устройства"></asp:TextBox>
                        <asp:ImageButton ID="AddButtonTV" CssClass="btn-lg ImageButton" runat="server" ImageUrl="content\tv.png" Width="100px" title="Создать телевизионной устройство" />
                        <asp:ImageButton ID="AddButtonSD" CssClass="btn-lg ImageButton" runat="server" ImageUrl="content\SoundDevice.png" Width="100px" title="Создать музыкальное устройство" />
                        <asp:ImageButton ID="AddButtonConditioner" CssClass="btn-lg ImageButton" runat="server" ImageUrl="content\conditioner.png" Width="100px" title="Создать кондиционер" />
                        <asp:ImageButton ID="AddButtonHeater" CssClass="btn-lg ImageButton" runat="server" ImageUrl="content\heater.png" Width="100px" title="Создать отопительно устройство" />
                        <asp:ImageButton ID="AddButtonBlower" CssClass="btn-lg ImageButton" runat="server" ImageUrl="content\blower.png" Width="100px" title="Создать вентилятор" />
                    </asp:Panel>
                    <!-- Секция для устройств -->
                    <asp:Panel ID="Panel1" runat="server" CssClass="col-xs-12 col-md-9 devicePadding">
                    </asp:Panel>
                </asp:Panel>
            </form>
        </asp:Panel>

    </main>
    <footer class="container footer">
        <div class="text-center myContact">
            <address class="footer-address">
                <a href="">ejen2008@mail.ru</a>
            </address>
            <p class="footer-address">&copy;Войтов Е.П., 2017 г.</p>
        </div>
    </footer>
</body>
</html>
