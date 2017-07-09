<%@ Page Language="C#" AutoEventWireup="false" Inherits="Loom.Web.Portal.UI.PortalPartialView" %>

<div>
    <h3>Route Test</h3>
    <p>
        Tests the ability of the portal to post async api routes with json data.
    </p>
    <p>
        <a href="#" id="roleTokenAndUser">Role Token and User Object</a>
    </p>
    <p>
        <a href="#" id="oneToken">One Int32 Route Token</a>
    </p>
    <p>
        <a href="#" id="addMunster">Add Munster</a>
    </p>
    <script type="text/javascript">
        $('#roleTokenAndUser').jsonPost({
            url: '~/Users/AddUser/Administrator',
            data: {
                user: { name: 'Brian', email: 'sdfs@jsdf.com' }
            }
        });

        $('#oneToken').jsonPost({
            url: '~/Users/GetUser/1234'
        });

        $('#addMunster').jsonPost({
            url: '~/Users/AddMunster/342346/Gomez'
        });
    </script>
</div>