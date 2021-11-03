//營業時間 radio///
var flag = 1;
//monday
let flexSwitchCheckDefault = document.querySelector("#flexSwitchCheckDefault");
let flexRadioDefault1 = document.querySelector('#flexRadioDefault1');
let flexRadioDefault2 = document.querySelector('#flexRadioDefault1hr');

let StateMon = document.querySelectorAll("#StateMon")[0];
let StateMon2 = document.querySelectorAll("#StateMon")[1];

flexSwitchCheckDefault.addEventListener("click", function () {
    if (flag == 1) {
        flexRadioDefault1.disabled = false;
        flexRadioDefault2.disabled = false;
        flag = 0;
    } else {
        flexRadioDefault1.disabled = true;
        flexRadioDefault2.disabled = true;
        StateMon.disabled = true;
        StateMon2.disabled = true;
        flexRadioDefault2.checked = false;
        flexRadioDefault1.checked = false;
        flag = 1;
    }
});
flexRadioDefault1.addEventListener("click", function () {
    StateMon.disabled = true;
    StateMon2.disabled = true;
    var MonStart = document.getElementById("MonStart");
    MonStart.innerHTML = "06:00";
    MonStart.setAttribute("value", "06:00");
    var MonEnd = document.getElementById("MonEnd");
    MonEnd.innerHTML = "23:00";
    MonEnd.setAttribute("value", "23:00");
});

flexRadioDefault2.addEventListener('click', function () {
    var MonStart = document.getElementById("MonStart");
    MonStart.innerHTML = "請選擇營業時間";

    var MonEnd = document.getElementById("MonEnd");
    MonEnd.innerHTML = "請選擇營業時間";

    let StateMon = document.querySelectorAll("#StateMon")[0];
    let StateMon2 = document.querySelectorAll("#StateMon")[1];
    StateMon.disabled = false;
    StateMon2.disabled = false;
});


//tuesday
var flag2 = 1;
let Tue = document.querySelector("#O2");
let TueAllDay = document.querySelector('#A2');
let Tuehour = document.querySelector('#hr2');

Tue.addEventListener("click", function () {
    if (flag2 == 1) {
        Tuehour.disabled = false;
        TueAllDay.disabled = false;
        flag2 = 0;
    } else {
        Tuehour.disabled = true;
        TueAllDay.disabled = true;
        TueAllDay.checked = false;
        Tuehour.checked = false;
        let StateTue = document.querySelectorAll("#StateTue")[0];
        let StateTue2 = document.querySelectorAll("#StateTue")[1];

        //選擇時間

        StateTue.disabled = true;
        StateTue2.disabled = true;
        flag2 = 1;
    }
});
TueAllDay.addEventListener('click', function () {
   

    let StateTue = document.querySelectorAll("#StateTue")[0];
    let StateTue2 = document.querySelectorAll("#StateTue")[1];
    StateTue.disabled = true;
    StateTue2.disabled = true;
    var TueStart = document.getElementById("TueStart");
    TueStart.innerHTML = "06:00";
    TueStart.setAttribute("value", "06:00");
    var TueEnd = document.getElementById("TueEnd");
    TueEnd.innerHTML = "23:00";
    TueEnd.setAttribute("value", "23:00");


});
Tuehour.addEventListener('click', function () {

    let StateTue = document.querySelectorAll("#StateTue")[0];
    let StateTue2 = document.querySelectorAll("#StateTue")[1];

    var TueStart = document.getElementById("TueStart");
    TueStart.innerHTML = "請選擇營業時間";

    var TueEnd = document.getElementById("TueEnd");
    TueEnd.innerHTML = "請選擇營業時間";

    StateTue.disabled = false;
    StateTue2.disabled = false;
})





//wednesday
let flexSwitchCheckDefaultWed = document.querySelector("#flexSwitchCheckDefault3");
let WedAllDay = document.querySelector("#flexRadioDefault3");
let Wedhour = document.querySelector("#flexRadioDefault3hr");
var flagWed = 1;
flexSwitchCheckDefaultWed.addEventListener("click", function () {
    if (flagWed == 1) {
        Wedhour.disabled = false;
        WedAllDay.disabled = false;
        flagWed = 0;
    } else {
        Wedhour.disabled = true;
        WedAllDay.disabled = true;
        WedAllDay.checked = false;
        Wedhour.checked = false;
        let StateWed = document.querySelectorAll("#StateWed")[0];
        let StateWed2 = document.querySelectorAll("#StateWed")[1];

        //選擇時間
       
        StateWed.disabled = true;
        StateWed2.disabled = true;
        flagWed = 1;
    }
});
WedAllDay.addEventListener('click', function () {
    let StateWed = document.querySelectorAll("#StateWed")[0];
    let StateWed2 = document.querySelectorAll("#StateWed")[1];

    var WedStart = document.getElementById("WedStart");
    WedStart.innerHTML = "06:00";
    WedStart.setAttribute("value", "06:00");

    var WedEnd = document.getElementById("WedEnd");
    WedEnd.innerHTML = "23:00";
    WedEnd.setAttribute("value", "23:00");

    StateWed.disabled = true;
    StateWed2.disabled = true;
});
Wedhour.addEventListener('click', function () {
    let StateWed = document.querySelectorAll("#StateWed")[0];
    let StateWed2 = document.querySelectorAll("#StateWed")[1];

    var WedStart = document.getElementById("WedStart");
    WedStart.innerHTML = "請選擇營業時間";

    var WedEnd = document.getElementById("WedEnd");
    WedEnd.innerHTML = "請選擇營業時間";

    StateWed.disabled = false;
    StateWed2.disabled = false;
});
//thursday
let flexSwitchCheckDefaultThu = document.querySelector("#flexSwitchCheckDefault4");
let ThuAllDay = document.querySelector("#flexRadioDefault4");
let Thuhour = document.querySelector("#flexRadioDefault4hr");
let StateThu = document.querySelectorAll("#StateThu")[0];
let StateThu2 = document.querySelectorAll("#StateThu")[1];
var flagThu = 1;
flexSwitchCheckDefaultThu.addEventListener("click", function () {
    if (flagThu == 1) {
        ThuAllDay.disabled = false;
        Thuhour.disabled = false;
        flagThu = 0;
    } else {
        ThuAllDay.disabled = true;
        Thuhour.disabled = true;
        ThuAllDay.checked = false;
        Thuhour.checked = false;
        StateThu.disabled = true;
        StateThu2.disabled = true;
        flagThu = 1;
    }
});
ThuAllDay.addEventListener('click', function () {
    StateThu.disabled = true;
    StateThu2.disabled = true;

    var ThuStart = document.getElementById("ThuStart");
    ThuStart.innerHTML = "06:00";
    ThuStart.setAttribute("value", "06:00");

    var ThuEnd = document.getElementById("ThuEnd");
    ThuEnd.innerHTML = "23:00";
    ThuEnd.setAttribute("value", "23:00");

})
Thuhour.addEventListener("click", function () {
    StateThu.disabled = false;
    StateThu2.disabled = false;

    var ThuStart = document.getElementById("ThuStart");
    ThuStart.innerHTML = "請選擇營業時間";

    var ThuEnd = document.getElementById("ThuEnd");
    ThuEnd.innerHTML = "請選擇營業時間";


})

//friday
let flexSwitchCheckDefaultFri = document.querySelector("#flexSwitchCheckDefault5");
let FridayAllDay = document.querySelector("#flexRadioDefault5");
let Fridayhour = document.querySelector("#flexRadioDefault5hr");
let StateFri = document.querySelectorAll("#StateFri")[0];
let StateFri2 = document.querySelectorAll('#StateFri')[1];
flagFri = 1;
flexSwitchCheckDefaultFri.addEventListener('click', function () {
    if (flagFri == 1) {
        FridayAllDay.disabled = false;
        Fridayhour.disabled = false;
        flagFri = 0;
    } else {
        FridayAllDay.disabled = true;
        Fridayhour.disabled = true;
        StateFri.disabled = true;
        StateFri2.disabled = true;
        FridayAllDay.checked = false;
        Fridayhour.checked = false;
        flagFri = 1;
    }
});
FridayAllDay.addEventListener("click", function () {
    StateFri.disabled = true;
    StateFri2.disabled = true;

    var FriStart = document.getElementById("FriStart");
    FriStart.innerHTML = "06:00";
    FriStart.setAttribute("value", "06:00");

    var FriEnd = document.getElementById("FriEnd");
    FriEnd.innerHTML = "23:00";
    FriEnd.setAttribute("value", "23:00");

});
Fridayhour.addEventListener("click", function () {
    StateFri.disabled = false;
    StateFri2.disabled = false;

    var FriStart = document.getElementById("FriStart");
    FriStart.innerHTML = "請選擇營業時間";

    var FriEnd = document.getElementById("FriEnd");
    FriEnd.innerHTML = "請選擇營業時間";
})

//saturday
let flexSwitchCheckDefaultSat = document.querySelector("#flexSwitchCheckDefault6");
let SatAllday = document.querySelector("#flexRadioDefault6");
let Sathour = document.querySelector('#flexRadioDefault6hr');
let StateSat = document.querySelectorAll("#StateSat")[0];
let StateSat2 = document.querySelectorAll("#StateSat")[1];
var flagSat = 1;
flexSwitchCheckDefaultSat.addEventListener('click', function () {
    if ((flagSat == 1)) {
        SatAllday.disabled = false;
        Sathour.disabled = false;
        flagSat = 0;
    } else {
        SatAllday.disabled = true;
        Sathour.disabled = true;
        SatAllday.checked = false;
        Sathour.checked = false;
        StateSat.disabled = true;
        StateSat2.disabled = true;
        flagSat = 1;
    };
});
SatAllday.addEventListener("click", function () {
    StateSat.disabled = true;
    StateSat2.disabled = true;
    var SatStart = document.getElementById("SatStart");
    SatStart.innerHTML = "06:00";
    SatStart.setAttribute("value", "06:00");

    var SatEnd = document.getElementById("SatEnd");
    SatEnd.innerHTML = "23:00";
    SatEnd.setAttribute("value", "23:00");

});
Sathour.addEventListener('click', function () {
    StateSat.disabled = false;
    StateSat2.disabled = false;

    var SatStart = document.getElementById("SatStart");
    SatStart.innerHTML = "請選擇營業時間";

    var SatEnd = document.getElementById("SatEnd");
    SatEnd.innerHTML = "請選擇營業時間";

});

//sunday 
let flexSwitchCheckDefaultSun = document.querySelector("#flexSwitchCheckDefault7");
let SunAllDay = document.querySelector("#flexRadioDefault7");
let Sunhour = document.querySelector('#flexRadioDefault7hr');
let StateSun = document.querySelectorAll('#StateSun')[0];
let StateSun2 = document.querySelectorAll("#StateSun")[1];
var flagSun = 1;
flexSwitchCheckDefaultSun.addEventListener("click", function () {
    if (flagSun == 1) {
        SunAllDay.disabled = false;
        Sunhour.disabled = false;
        flagSun = 0;
    } else {
        SunAllDay.disabled = true;
        Sunhour.disabled = true;
        SunAllDay.checked = false;
        Sunhour.checked = false;
        StateSun.disabled = true;
        StateSun2.disabled = true;
        flagSun = 1;
    }
});
SunAllDay.addEventListener("click", function () {
    StateSun.disabled = true;
    StateSun2.disabled = true;
    var SunStrat = document.getElementById("SunStrat");
    SunStrat.innerHTML = "06:00";
    SunStrat.setAttribute("value", "06:00");

    var SunEnd = document.getElementById("SunEnd");
    SunEnd.innerHTML = "23:00";
    SunEnd.setAttribute("value", "23:00");

});
Sunhour.addEventListener('click', function () {
    var SunStrat = document.getElementById("SunStrat");
    SunStrat.innerHTML = "請選擇營業時間";

    var SunEnd = document.getElementById("SunEnd");
    SunEnd.innerHTML = "請選擇營業時間";

    StateSun.disabled = false;
    StateSun2.disabled = false;

});
CKEDITOR.replace('OtherAmenity');
///營業時間 radio///


// Example starter JavaScript for disabling form submissions if there are invalid fields
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
