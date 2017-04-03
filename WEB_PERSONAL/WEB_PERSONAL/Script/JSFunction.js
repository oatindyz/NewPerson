function allowOnlyNumber(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function maxBuy(max, evt) {
    var m = parseInt(max);
    var input = parseInt(evt.value);

    if (input > max) {
        alert("ไม่สามารถระบุจำนวนมากกว่า : " + max);
        evt.value = max;
    }
function reFocus(id) {
        document.getElementById(id).blur();
        document.getElementById(id).focus();
    }

}
function openPopup(a) {
    document.getElementById(a).style.display = 'block';
}
function closePopup(a) {
    document.getElementById(a).style.display = 'none';
}
function OnClick(evt) {
    var clickCount;

    if (clickCount >= 2) {
        clickCount = 1;          //reset clickCount
        SingleClickFunction();
        return false;
    }
}
function AllowClickOnce(linkbuttonID) {
    var linkbutton = document.getElementById(linkbuttonID);
    if (linkbutton.clicked == '0') {
        linkbutton.clicked = '1';
        linkbutton.disabled = true;
        return true;
    }
    else {
        return false;
    }
}