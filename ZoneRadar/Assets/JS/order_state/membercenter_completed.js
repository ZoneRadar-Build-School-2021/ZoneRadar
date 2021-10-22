let mytextarea = document.getElementById("textsend");
let myselect = document.querySelector('.myselect');
let myradio = document.querySelectorAll('.myradio');
let mybtn = document.querySelector('.mybtn');



function success() {
    if (mytextarea.value != "" && (myselect.value == '1' || myselect.value == '2' || myselect.value == '3' || myselect.value == '4' || myselect.value == '5') && (myradio[0].checked || myradio[1].checked)) {
        mybtn.disabled = false;
    } else {
        mybtn.disabled = true;
    }
}