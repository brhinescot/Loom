<%@ Control Language="C#" ClassName="Login" Inherits="Loom.Web.Portal.UI.Controls.Tile" %>
<%@ Register Assembly="Loom.Web.Portal" Namespace="Loom.Web.Portal.UI.Controls" TagPrefix="loom" %>
<div id="login">
    <div id="result" style="height: 70px; visibility: hidden">
    </div>
    <table width="550" summary="Add the administrator account for the portal.">
        <thead>
        <tr>
            <th colspan="2">
                Step 2: Login to your administrator account.
            </th>
        </tr>
        </thead>
        <tbody>
        <tr>
            <td class="vmiddle">
                <img src='<%= ResolveImageResource("login.png") %>' alt="Create Administrator Account"/>
            </td>
            <td>
                <form id="loginform" class="loginform" action="index.html" method="post">
                    <fieldset style="margin: 0; padding: 0;">
                        <loom:AntiForgeryToken runat="server"/>
                        <label for="userlogin">
                            Username
                        </label>
                        <input type="text" tabindex="10" size="20" value="<%= ViewData.Username %>" class="input" id="userlogin" name="username"/>
                        <label for="userpass">
                            Password
                        </label>
                        <input type="password" tabindex="10" size="20" value="" class="input" id="userpass" name="password"/>
                        <p class="remember">
                            <label>
                                <input type="checkbox" tabindex="90" value="remember" id="remember" name="rememberme"/>
                                Remember Me
                            </label>
                        </p>
                        <p class="submit">
                            <input type="submit" tabindex="100" value="Log In" name="submit" id="submit"/>
                        </p>
                    </fieldset>
                </form>
            </td>
        </tr>
        </tbody>
        <tfoot>
        <tr>
            <td colspan="2">
                <span class="forgot">
                    <a href="#">Forgot Password?</a>
                </span>
            </td>
        </tr>
        </tfoot>
    </table>
</div>
<loom:Script runat="server" ID="doLoginJavaScript">
    <DocumentReadyScript>
        var mb = $('#result').messageBox();
        $('#submit').jsonPost({form:'#loginform', url:'~/setup/dologin',
        success:function(args){
        mb.fadeOut(100, function(){
        $(this).css('visibility','visible').fadeIn(200);
        });
        }
        });
    </DocumentReadyScript>
</loom:Script>