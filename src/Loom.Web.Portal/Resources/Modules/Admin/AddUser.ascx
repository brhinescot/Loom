<%@ Control Language="C#" ClassName="AddUser" Inherits="Loom.Web.Portal.UI.Controls.Tile" %>
<%@ Register TagPrefix="loom" Namespace="Loom.Web.Portal.UI.Controls" %>
<div id="login">
    <div id="result" style="height: 70px; visibility: hidden">
    </div>
    <table width="550" summary="Create an administrator account">
        <thead>
        <tr>
            <th colspan="2">
                Step 1: Create Administrator Account
            </th>
        </tr>
        </thead>
        <tbody>
        <tr>
            <td class="vmiddle">
                <img src='<%= ResolveImageResource("adminsetup.png") %>' alt="Create Administrator Account"/>
            </td>
            <td>
                <form id="loginform" class="loginform" action="#" method="post">
                    <fieldset style="margin: 0; padding: 0;">
                        <loom:AntiForgeryToken runat="server"/>
                        <label>
                            Username
                        </label>
                        <input type="text" name="userName" id="userlogin" class="input" value="" size="20" tabindex="10"/>
                        <label>
                            Email Address
                        </label>
                        <input type="text" name="emailAddress" id="useremail" class="input" value="" size="20" tabindex="10"/>
                        <label>
                            Password
                        </label>
                        <input type="password" name="password" id="userpass" class="input" value="" size="20" tabindex="10"/>
                        <label>
                            Confirm Password
                        </label>
                        <input type="password" name="passwordConfirm" id="userpassconfirm" class="input" value="" size="20" tabindex="10"/>
                        <p class="submit">
                            <input type="submit" name="submit" id="submit" value="Create" tabindex="100"/>
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
                    <a href="#">
                        Need Help?
                    </a>
                </span>
            </td>
        </tr>
        </tfoot>
    </table>
</div>
<loom:Script runat="server" ID="addUserJavaScript">
    <DocumentReadyScript>
        var mb = $('#result').messageBox();
        $('#submit').jsonPost({form:'#loginform', url:'~/setup/insertadmin',
        success:function(args){
        mb.fadeOut(100, function(){
        $(this).css('visibility','visible').fadeIn(200);
        });
        }
        });
    </DocumentReadyScript>
</loom:Script>