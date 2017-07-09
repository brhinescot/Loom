function validateCheckbox(val) {
    var controltovalidate =
        document.all ? document.all[val.controltovalidate] : document.getElementById(val.controltovalidate);
    return controltovalidate.checked == true;
}