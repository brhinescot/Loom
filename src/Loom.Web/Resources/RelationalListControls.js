function listItemSelected(list1, list2, keepFirst) {
    if (list1 && list2 != null) {
        if (list2.selectedIndex >= 0) {
            list2.originalSelectedValue = list2.options[list2.selectedIndex].value;
        } else {
            list2.originalSelectedValue = "";
        }
        clearComboOrList(list2);
        var listValue;
        if (list1.selectedIndex >= 0) {
            listValue = list1.id + "=" + list1.options[list1.selectedIndex].value;
        } else {
            if (keepFirst) {
                listValue = list1.id + "=" + list1.options[0].value;
            } else {
                listValue = "";
            }
        }
        fillComboBox(list2, listValue);
        if (list2.onchange)
            list2.onchange();
    }
}

function clearComboOrList(list) {
    if (list) {
        list.selectedIndex = -1;
        for (var i = list.options.length - 1; i >= 0; i--) {
            list.options[i] = null;
        }
        list.selectedIndex = -1;
    }
}

function fillComboBox(list, listValue) {
    if (list) {
        if (listValue != "") {
            if (this[list.id + "AssocArray"][listValue]) {
                var arrX = this[list.id + "AssocArray"][listValue];
                for (var i = 0; i < arrX.length; i = i + 2) {
                    list.options[list.options.length] = new Option(arrX[i + 1], arrX[i]);
                    if (list.options[list.options.length - 1].value == list.originalSelectedValue) {
                        list.options[list.options.length - 1].selected = true;
                    }
                }
            } else {
                list.options[0] = new Option("None found", "");
            }
        }
    }
}

function addCommandTo(functionName, newCommand) {
    var scriptString;
    if (functionName) {
        scriptString = new String(functionName);
        if (scriptString.indexOf("{") > 0) {
            scriptString = scriptString.substring(scriptString.indexOf("{") + 1, scriptString.lastIndexOf("}") - 1);
            scriptString += newCommand;
        }
    } else {
        scriptString = new String(newCommand);
    }
    return new Function(scriptString);
}