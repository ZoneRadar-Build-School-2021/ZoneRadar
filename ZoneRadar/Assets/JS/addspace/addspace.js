//停車場的 是否
var yesflag = 0;
var parkingPart = document.querySelector("#parkingPart");
let flexSwitchCheckDefaultYes = document.querySelector("#flexSwitchCheckDefaultYes");
flexSwitchCheckDefaultYes.addEventListener("click", function () {

    if (yesflag == 0) {
        parkingPart.classList.remove('d-none');
        yesflag = 1;
    }
    else {
        parkingPart.classList.add('d-none');
        yesflag = 0;
    }
});
//攝影機 是
let projectionflag =0;
let Yesprojection = document.querySelector("#Yesprojection");
var projectionPart = document.querySelector('#projectionPart');
Yesprojection.addEventListener('click', function () {
    if (projectionflag == 0) {

        projectionPart.classList.remove('d-none');
        projectionflag = 1;
    } else {
        projectionPart.classList.add('d-none');
        projectionflag = 0;
    }
    
});

//攝影機 否
//let NOprojection = document.querySelector("#Noprojection");
//NOprojection.addEventListener('click', function () {
//    projectionPart.classList.add('d-none');
//});



// Example starter JavaScript for disabling form submissions if there are invalid fields
//表單沒填寫
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()
///文字編輯器 

CKEDITOR.replace('Introduction');
CKEDITOR.replace('HostRules');
CKEDITOR.replace('Parking');
CKEDITOR.replace('ShootingEquipment');
CKEDITOR.replace('Traffic');


///上傳照片
FilePond.parse(document.body);