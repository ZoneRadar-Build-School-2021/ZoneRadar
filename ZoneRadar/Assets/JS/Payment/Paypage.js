let ischeck = document.querySelector("#flexCheckAccpet");
let submit = document.querySelector("#submit-pay");

function checkboxOnclick(ischeckbox) {

    if (ischeckbox.checked == true) {
        submit.removeAttribute("class")
        submit.setAttribute("class", "Button btn btn-primary submit-bkg-req-btn mt-4 mb-4")

    } else {
        submit.setAttribute("class", " Button btn btn-primary submit-bkg-req-btn mt-4 mb-4 disabled");
    }
}