function validateBaseCompareValidator2(val) {
    var isvalid = (window.CompareValidatorEvaluateIsValid(val));
    updateControlToValidate(val, isvalid);
    return isvalid;
}

function updateControlToValidate(val, isvalid) {
    var controltovalidate =
        document.all ? document.all[val.controltovalidate] : document.getElementById(val.controltovalidate);
    if (!isvalid) {
        if (val.errorBackColor)
            controltovalidate.style.backgroundColor = val.errorBackColor;
        if (val.errorBorderColor)
            controltovalidate.style.borderColor = val.errorBorderColor;
        if (val.errorBorderStyle)
            controltovalidate.style.borderStyle = val.errorBorderStyle;
        if (val.errorBorderWidth)
            controltovalidate.style.borderWidth = val.errorBorderWidth;
        if (val.errorCssClass)
            controltovalidate.className = val.errorCssClass;
        if (val.errorForeColor)
            controltovalidate.style.color = val.errorForeColor;
        if (val.errorHeight)
            controltovalidate.style.height = val.errorHeight;
        if (val.errorWidth)
            controltovalidate.style.width = val.errorWidth;
    } else {
        if (val.errorBackColor)
            controltovalidate.style.backgroundColor = val.defaultBackColor;
        if (val.errorBorderColor)
            controltovalidate.style.borderColor = val.defaultBorderColor;
        if (val.errorBorderStyle)
            controltovalidate.style.borderStyle = val.defaultBorderStyle;
        if (val.errorBorderWidth)
            controltovalidate.style.borderWidth = val.defaultBorderWidth;
        if (val.errorCssClass)
            controltovalidate.className = val.deafultCssClass;
        if (val.errorForeColor)
            controltovalidate.style.color = val.deafultForeColor;
        if (val.errorHeight)
            controltovalidate.style.height = val.deafultHeight;
        if (val.errorWidth)
            controltovalidate.style.width = val.deafultWidth;
    }
}