function DevInterop_PreloadSwapImages(imgArray) {
    var doc = document;
    if (doc.images) {
        if (!doc.DevInterop_ImageArray)
            doc.DevInterop_ImageArray = new Array();
        for (var i = 0; i < imgArray.length; i++) {
            if (imgArray[i].indexOf("#") != 0) {
                doc.DevInterop_ImageArray[i] = new Image;
                doc.DevInterop_ImageArray[i].src = imgArray[i];
            }
        }
    }
}

function DevInterop_RestoreImage() {
    var x, a = document.DevInterop_Swap;
    for (var i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++)
        x.src = x.oSrc;
}

function DevInterop_FindObject(n, doc) {
    var p, x;
    if (!doc)
        doc = document;
    if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        doc = parent.frames[n.substring(p + 1)].document;
        n = n.substring(0, p);
    }
    if (!(x = doc[n]) && doc.all)
        x = doc.all[n];
    for (var j = 0; !x && j < doc.forms.length; j++)
        x = doc.forms[j][n];
    for (var k = 0; !x && doc.layers && k < doc.layers.length; k++)
        x = DevInterop_FindObject(n, doc.layers[k].document);
    return x;
}

function DevInterop_SwapImage(clientId, source) {
    document.DevInterop_Swap = new Array;
    var j = 0, image;
    if ((image = DevInterop_FindObject(clientId)) != null) {
        document.DevInterop_Swap[j++] = image;
        if (!image.oSrc)
            image.oSrc = image.src;
        image.src = source;
    }
}