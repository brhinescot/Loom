function SetSingleRadioChecked(nameregex, current) {
    var re = new RegExp(nameregex);
    for (var i = 0; i < document.forms[0].elements.length; i++) {
        var elm = document.forms[0].elements[i];
        if (elm.type == "radio") {
            if (re.test(elm.name))
                elm.checked = false;
        }
    }
    current.checked = true;
}