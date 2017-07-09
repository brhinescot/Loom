function antiSpamLink(encodedTo) {
    var messageTo = "";
    for (var i = 0; i < encodedTo.length;) {
        var letter = encodedTo.charAt(i) + encodedTo.charAt(i + 1);
        messageTo += String.fromCharCode(parseInt(letter, 16));
        i += 2;
    }
    location.href = messageTo.substring(20, messageTo.length);
}