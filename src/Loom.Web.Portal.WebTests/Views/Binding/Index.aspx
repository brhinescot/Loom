<%@ Page Language="C#" AutoEventWireup="false" Inherits="Loom.Web.Portal.UI.PortalPartialView" %>

<div>
    <div>
        <select id="select">
            <option class="name"></option>
        </select>
    </div>

    <table id="binding" summary="Binding Test">
        <thead>
        <tr>
            <th>Name</th>
            <th>Phone</th>
            <th>Email</th>
            <th>Website</th>
        </tr>
        </thead>
        <tbody>
        <tr class="itemTemplate">
            <td class="name">[In-line] The user's name is ${name}</td>
            <td class="phone"></td>
            <td class="email"></td>
            <td>
                <a class="homepage" href="#" style="color: White;">Web Site</a>
            </td>
        </tr>
        </tbody>
    </table>

    <div id="repeater">
        <div class="itemTemplate">
            <img class="image" src="#" alt=""/>
            <p class="name"></p>
            <p class="phone"></p>
            <p class="email"></p>
            <p>
                <a class="homepage" href="#" style="color: White;">Web Site</a>
            </p>
        </div>
    </div>
</div>

<script type="text/javascript">
    var dataTemplate = {
        itemTemplate: '.itemTemplate',
        itemBound: function(item, data, index) {
            if (index % 2 === 0)
                item.css('background-color', '#EEF');
            else
                item.css('background-color', '#FFF');
        },
        format: {
            name: "The user's name is ${name}",
            homepage: {
                href: "http://localhost/visit/?site=${homepage}&name=${name}",
                title: "Visit ${name}'s web site.",
                text: function(e) {
                    // Use the parameter's data property to perform complex formatting
                    return "Click to visit " + e.data.name.toUpperCase() + "'S Web Site";
                },
                style: function(e) {
                    return { 'background-color': e.data.color };
                },
                expando: "test"
            },
            phone: function(e) {
                if (e.data.name === "Brian Scott") {
                    e.elem.css({ 'color': '#D00', 'font-weight': 'bold' });
                    e.data.phone = '602-363-9404';
                }
                return e.data.phone;
            },
            image: {
                src: function(e) {
                    return e.data.imageUrl;
                },
                alt: "image",
                title: 'Download Contact'
            }
        },
        events: {
            click: function(e, data, item) {
                item.fadeRemove(200);
                repeater.dataBind(data, 'append');
                return false;
                if (!item.data('portal-defaultColor'))
                    item.data('portal-defaultColor', item.css('background-color'));

                if (item.css('background-color') === item.data('portal-defaultColor'))
                    item.css('background-color', '#CFC');
                else
                    item.css('background-color', item.data('portal-defaultColor'));
                return false;
            }
        }
    };

    var smallSet = [
        {
            name: 'Brian Scott',
            email: 'brhinescot@yahoo.com',
            phone: '888-555-1212',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            name: 'Jeremy Dunn',
            email: 'jeremy@jeremy2.com',
            phone: '888-555-2424',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            name: 'Charlie Herner',
            email: 'charlie@charlie.com',
            phone: '888-555-8787',
            homepage: 'http://www.colossusinteractive.com'
        }
    ];

    var mediumSet = [
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'green',
            name: 'Brian Scott',
            email: 'brhinescot2@yahoo.com',
            phone: '882-555-1212',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'brown',
            name: 'Jeremy Dunn',
            email: 'jeremy2@jermy.com',
            phone: '882-555-2424',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'purple',
            name: 'Charlie Herner',
            email: 'charlie2@charlie.com',
            phone: '882-555-8787',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'orange',
            name: 'Brian Scott',
            email: 'brhinescot2@yahoo.com',
            phone: '882-555-1212',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'gray',
            name: 'Jeremy Dunn',
            email: 'jeremy2@jermy.com',
            phone: '882-555-2424',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'maroon',
            name: 'Charlie Herner',
            email: 'charlie2@charlie.com',
            phone: '882-555-8787',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'teal',
            name: 'Brian Scott',
            email: 'brhinescot2@yahoo.com',
            phone: '882-555-1212',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'silver',
            name: 'Jeremy Dunn',
            email: 'jeremy2@jermy.com',
            phone: '882-555-2424',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'black',
            name: 'Charlie Herner',
            email: 'charlie2@charlie.com',
            phone: '882-555-8787',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'tan',
            name: 'Brian Scott',
            email: 'brhinescot2@yahoo.com',
            phone: '882-555-1212',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'steelblue',
            name: 'Jeremy Dunn',
            email: 'jeremy2@jermy.com',
            phone: '882-555-2424',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'turquoise',
            name: 'Charlie Herner',
            email: 'charlie2@charlie.com',
            phone: '882-555-8787',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'chocolate',
            name: 'Brian Scott',
            email: 'brhinescot2@yahoo.com',
            phone: '882-555-1212',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'fuchsia',
            name: 'Jeremy Dunn',
            email: 'jeremy2@jermy.com',
            phone: '882-555-2424',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'cyan',
            name: 'Charlie Herner',
            email: 'charlie2@charlie.com',
            phone: '882-555-8787',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'navy',
            name: 'Brian Scott',
            email: 'brhinescot2@yahoo.com',
            phone: '882-555-1212',
            homepage: 'http://www.colossusinteractive.com'
        },
        {
            imageUrl: 'imageresource/test/download.gif',
            color: 'indianred',
            name: 'Jeremy Dunn',
            email: 'jeremy2@jermy.com',
            phone: '882-555-2424',
            homepage: 'http://www.colossusinteractive.com'
        }
    ];

    var selectTemplate = {
        format: {
            name: {
                text: '${name}',
                value: '${email}'
            }
        }
    };

    var select = $('#select');
    select.dataTemplate(selectTemplate).dataBind(smallSet);

    var binding = $('#binding');
    binding.dataTemplate(dataTemplate).dataBind(mediumSet);

    var repeater = $('#repeater');
    repeater.dataTemplate(dataTemplate);
</script>