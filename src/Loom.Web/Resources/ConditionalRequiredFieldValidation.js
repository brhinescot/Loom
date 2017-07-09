function validateConditionalCompare(val) {
    var controltovalidate =
        document.all ? document.all[val.controltovalidate] : document.getElementById(val.controltovalidate);
    var controltocompare =
        document.all ? document.all[val.controltocompare] : document.getElementById(val.controltocompare);
    var compareValue = controltocompare.options
        ? controltocompare.options[controltocompare.selectedIndex].value
        : controltocompare.type == "checkbox"
        ? controltocompare.checked
        : controltocompare.value;
    var operator = "Equal";
    if (typeof(val.operator) == "string")
        operator = val.operator;
    var isvalid;
    if (window.ValidatorCompare(compareValue, val.triggervalue, operator, val))
        isvalid = controltovalidate.value != null && controltovalidate.value != "";
    else
        isvalid = true;
    updateControlToValidate(val, isvalid);
    return isvalid;
}