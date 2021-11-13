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






