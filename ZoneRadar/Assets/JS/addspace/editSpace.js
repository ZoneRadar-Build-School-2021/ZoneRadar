//停車場的 是否
var yesflag = 1;
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
let projectionflag =1;
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
/營業時間 radio///

let openday = document.querySelector("#openday");
openday.addEventListener("click", function () {
    //星期一
    let monAll = document.querySelectorAll("#flexRadioDefault1")[0];
    monAll.setAttribute("value", "Y1");

    let monHour = document.querySelectorAll("#flexRadioDefault1")[1];
    monHour.setAttribute("value", "hr1");
    

    //星期二
    let tueOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
    tueOpen.setAttribute("value", 2);
    tueOpen.setAttribute("name", "OperatingDay");
    tueOpen.setAttribute("id", 'flexSwitchCheckDefault2');
    tueOpen.setAttribute("for", "flexSwitchCheckDefault2");
    let openlabel = document.querySelectorAll("#openlabel")[1];
    openlabel.setAttribute('for',"flexSwitchCheckDefault2")
    //星期二的 全天 小時 label for設定
    let tueAll = document.querySelectorAll('#flexRadioDefault1')[2];
    let tueHour = document.querySelectorAll('#flexRadioDefault1')[3];
    let allDayLabeltue = document.querySelectorAll("#allDayLabel")[1];
    let hourLabeltue = document.querySelectorAll("#hourLabel")[1];

    allDayLabeltue.setAttribute("for", "flexRadioDefault2");
    hourLabeltue.setAttribute("for", "flexRadioDefault2");

    tueAll.setAttribute("value", "Y2");
    tueAll.setAttribute("name", "Hour2");
    tueAll.setAttribute("id", "flexRadioDefault2");
    
    tueHour.setAttribute("value", "hr2");
    tueHour.setAttribute("name", "Hour2");
    tueHour.setAttribute("id", "flexRadioDefault2")


    //星期三
    let wedOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
    wedOpen.setAttribute("value", 3);
    wedOpen.setAttribute("name", "OperatingDay");
    wedOpen.setAttribute("id", 'flexSwitchCheckDefault3');
    wedOpen.setAttribute("for", "flexSwitchCheckDefault3");
    let openlabel3 = document.querySelectorAll("#openlabel")[2];
    openlabel3.setAttribute('for', "flexSwitchCheckDefault3")

    let wedAll = document.querySelectorAll('#flexRadioDefault1')[2];
    let wedHour = document.querySelectorAll('#flexRadioDefault1')[3];
    let allDayLabelwed = document.querySelectorAll("#allDayLabel")[2];
    let hourLabelwed = document.querySelectorAll("#hourLabel")[2];
    allDayLabelwed.setAttribute("for", "flexRadioDefault3");
    hourLabelwed.setAttribute("for", "flexRadioDefault3");
    wedAll.setAttribute("value", "Y3");
    wedAll.setAttribute("name", "Hour3");
    wedAll.setAttribute("id", "flexRadioDefault3");

    wedHour.setAttribute("value", "hr3");
    wedHour.setAttribute("name", "Hour3");
    wedHour.setAttribute("id", "flexRadioDefault3")

    //星期四
    let ThuOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
    ThuOpen.setAttribute("value", 4);
    ThuOpen.setAttribute("name", "OperatingDay");
    ThuOpen.setAttribute("id", 'flexSwitchCheckDefault4');
    ThuOpen.setAttribute("for", "flexSwitchCheckDefault4");
    let openlabel4 = document.querySelectorAll("#openlabel")[3];
    openlabel4.setAttribute('for', "flexSwitchCheckDefault4")

    let ThuAll = document.querySelectorAll('#flexRadioDefault1')[2];
    let ThuHour = document.querySelectorAll('#flexRadioDefault1')[3];
    let allDayLabelThu = document.querySelectorAll("#allDayLabel")[3];
    let hourLabelThu = document.querySelectorAll("#hourLabel")[3];
    allDayLabelThu.setAttribute("for", "flexRadioDefault4");
    hourLabelThu.setAttribute("for", "flexRadioDefault4");

    ThuAll.setAttribute("value", "Y4");
    ThuAll.setAttribute("name", "Hour4");
    ThuAll.setAttribute("id", "flexRadioDefault4");

    ThuHour.setAttribute("value", "hr4");
    ThuHour.setAttribute("name", "Hour4");
    ThuHour.setAttribute("id", "flexRadioDefault4");

    //星期五
    let FriOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
    FriOpen.setAttribute("value", 5);
    FriOpen.setAttribute("name", "OperatingDay");
    FriOpen.setAttribute("id", 'flexSwitchCheckDefault5');
    FriOpen.setAttribute("for", "flexSwitchCheckDefault5");
    let openlabel5 = document.querySelectorAll("#openlabel")[4];//改
    openlabel5.setAttribute('for', "flexSwitchCheckDefault5");

    let FriAll = document.querySelectorAll('#flexRadioDefault1')[2];
    let FriHour = document.querySelectorAll('#flexRadioDefault1')[3];
    let allDayLabelFri = document.querySelectorAll("#allDayLabel")[4];
    let hourLabelFri = document.querySelectorAll("#hourLabel")[4];//改
    allDayLabelFri.setAttribute("for", "flexRadioDefault5");
    hourLabelFri.setAttribute("for", "flexRadioDefault5");

    FriAll.setAttribute("value", "Y5");
    FriAll.setAttribute("name", "Hour5");
    FriAll.setAttribute("id", "flexRadioDefault5");

    FriHour.setAttribute("value", "hr5");
    FriHour.setAttribute("name", "Hour5");
    FriHour.setAttribute("id", "flexRadioDefault5");

    //星期六
    let SatOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
    SatOpen.setAttribute("value", 6);
    SatOpen.setAttribute("name", "OperatingDay");
    SatOpen.setAttribute("id", 'flexSwitchCheckDefault6');
    SatOpen.setAttribute("for", "flexSwitchCheckDefault6");
    let openlabel6 = document.querySelectorAll("#openlabel")[5];//改
    openlabel6.setAttribute('for', "flexSwitchCheckDefault6");

    let SatAll = document.querySelectorAll('#flexRadioDefault1')[2];
    let SatHour = document.querySelectorAll('#flexRadioDefault1')[3];
    let allDayLabelSat = document.querySelectorAll("#allDayLabel")[5];//改
    let hourLabelSat = document.querySelectorAll("#hourLabel")[5];//改
    allDayLabelSat.setAttribute("for", "flexRadioDefault6");
    hourLabelSat.setAttribute("for", "flexRadioDefault6");

    SatAll.setAttribute("value", "Y6");
    SatAll.setAttribute("name", "Hour6");
    SatAll.setAttribute("id", "flexRadioDefault6");

    SatHour.setAttribute("value", "hr6");
    SatHour.setAttribute("name", "Hour6");
    SatHour.setAttribute("id", "flexRadioDefault6");

    //星期日
    let SunOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
    SunOpen.setAttribute("value", 7);
    SunOpen.setAttribute("name", "OperatingDay");
    SunOpen.setAttribute("id", 'flexSwitchCheckDefault7');
    SunOpen.setAttribute("for", "flexSwitchCheckDefault7");
    let openlabel7 = document.querySelectorAll("#openlabel")[6];//改
    openlabel7.setAttribute('for', "flexSwitchCheckDefault7");

    let SunAll = document.querySelectorAll('#flexRadioDefault1')[2];
    let SunHour = document.querySelectorAll('#flexRadioDefault1')[3];
    let allDayLabelSun = document.querySelectorAll("#allDayLabel")[6];//改
    let hourLabelSun = document.querySelectorAll("#hourLabel")[6];//改
    allDayLabelSun.setAttribute("for", "flexRadioDefault7");
    hourLabelSun.setAttribute("for", "flexRadioDefault7");

    SunAll.setAttribute("value", "Y7");
    SunAll.setAttribute("name", "Hour7");
    SunAll.setAttribute("id", "flexRadioDefault7");

    SunHour.setAttribute("value", "hr7");
    SunHour.setAttribute("name", "Hour7");
    SunHour.setAttribute("id", "flexRadioDefault7");

});
    



var flag = 0;
//monday
//營業
let flexSwitchCheckDefault = document.querySelectorAll("#flexSwitchCheckDefault")[0];
//全天
let flexRadioDefault1 = document.querySelectorAll('#flexRadioDefault1')[0];
//小時
let flexRadioDefault2 = document.querySelectorAll('#flexRadioDefault1')[1];

//開始時間
let StateMon = document.querySelectorAll("#State")[0];
//結束時間
let StateMon2 = document.querySelectorAll("#State")[1];
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

});

flexRadioDefault2.addEventListener('click', function () {

    let StateMon = document.querySelectorAll("#State")[0];
    let StateMon2 = document.querySelectorAll("#State")[1];
    StateMon.disabled = false;
    StateMon2.disabled = false;
});


//tuesday
var flagtue=0;
let flexSwitchCheckDefaultTue = document.querySelectorAll("#flexSwitchCheckDefault")[1];
let TueAllDay = document.querySelectorAll('#flexRadioDefault1')[2];
let Tuehour = document.querySelectorAll('#flexRadioDefault1')[3];
flexSwitchCheckDefaultTue.addEventListener("click", function () {
    if (flagtue == 1) {
        TueAllDay.disabled = false;
        Tuehour.disabled = false;
        flagtue = 0;
    } else {
        TueAllDay.disabled = true;
        Tuehour.disabled = true;
        TueAllDay.checked = false;
        Tuehour.checked = false;
        let StateTue = document.querySelectorAll("#State")[2];
        let StateTue2 = document.querySelectorAll("#State")[3];
        StateTue.disabled = true;
        StateTue2.disabled = true;
        flagtue = 1;
    }
});
TueAllDay.addEventListener('click', function () {

    let StateTue = document.querySelectorAll("#State")[2];
    let StateTue2 = document.querySelectorAll("#State")[3];
    StateTue.disabled = true;
    StateTue2.disabled = true;

});
Tuehour.addEventListener('click', function () {

    let StateTue = document.querySelectorAll("#State")[2];
    let StateTue2 = document.querySelectorAll("#State")[3];
    StateTue.disabled = false;
    StateTue2.disabled = false;
})

//wednesday
let flexSwitchCheckDefaultWed = document.querySelectorAll("#flexSwitchCheckDefault")[2];
let WedAllDay = document.querySelectorAll("#flexRadioDefault1")[4];
let Wedhour = document.querySelectorAll("#flexRadioDefault1")[5];
var flagWed=1;
flexSwitchCheckDefaultWed.addEventListener("click", function () {
    if ((flagWed == 0)) {
        Wedhour.disabled = false;
        WedAllDay.disabled = false;
        flagWed = 1;
    } else {
        Wedhour.disabled = true;
        WedAllDay.disabled = true;
        WedAllDay.checked = false;
        Wedhour.checked = false;
        let StateWed = document.querySelectorAll("#State")[4];
        let StateWed2 = document.querySelectorAll("#State")[5];

        //選擇時間
        //
        StateWed.disabled = true;
        StateWed2.disabled = true;
        flagWed = 0;
    }
});
WedAllDay.addEventListener('click', function () {
    let StateWed = document.querySelectorAll("#State")[4];
    let StateWed2 = document.querySelectorAll("#State")[5];
    StateWed.disabled = true;
    StateWed2.disabled = true;
});
Wedhour.addEventListener('click', function () {
    let StateWed = document.querySelectorAll("#State")[4];
    let StateWed2 = document.querySelectorAll("#State")[5];
    StateWed.disabled = false;
    StateWed2.disabled = false;
});
//thursday
let flexSwitchCheckDefaultThu = document.querySelectorAll("#flexSwitchCheckDefault")[3];
let ThuAllDay = document.querySelectorAll("#flexRadioDefault1")[6];
let Thuhour = document.querySelectorAll("#flexRadioDefault1")[7];
let StateThu = document.querySelectorAll("#State")[6];
let StateThu2 = document.querySelectorAll("#State")[7];
var flagThu=1;
flexSwitchCheckDefaultThu.addEventListener("click", function () {
    if (flagThu == 0) {
        ThuAllDay.disabled = false;
        Thuhour.disabled = false;
        flagThu = 1;
    } else {
        ThuAllDay.disabled = true;
        Thuhour.disabled = true;
        ThuAllDay.checked = false;
        Thuhour.checked = false;
        StateThu.disabled = true;
        StateThu2.disabled = true;
        flagThu = 0;
    }
});
ThuAllDay.addEventListener('click', function () {
    StateThu.disabled = true;
    StateThu2.disabled = true;
})
Thuhour.addEventListener("click", function () {
    StateThu.disabled = false;
    StateThu2.disabled = false;
})

//friday
let flexSwitchCheckDefaultFri = document.querySelectorAll("#flexSwitchCheckDefault")[4];
let FridayAllDay = document.querySelectorAll("#flexRadioDefault1")[8];
let Fridayhour = document.querySelectorAll("#flexRadioDefault1")[9];
let StateFri = document.querySelectorAll("#State")[8];
let StateFri2 = document.querySelectorAll('#State')[9];
var flagFri=1;
flexSwitchCheckDefaultFri.addEventListener('click', function () {
    if (flagFri == 0) {
        FridayAllDay.disabled = false;
        Fridayhour.disabled = false;
        flagFri = 1;
    } else {
        FridayAllDay.disabled = true;
        Fridayhour.disabled = true;
        StateFri.disabled = true;
        StateFri2.disabled = true;
        FridayAllDay.checked = false;
        Fridayhour.checked = false;
        flagFri = 0;
    }
});
FridayAllDay.addEventListener("click", function () {
    StateFri.disabled = true;
    StateFri2.disabled = true;
});
Fridayhour.addEventListener("click", function () {
    StateFri.disabled = false;
    StateFri2.disabled = false;
})

//saturday
let flexSwitchCheckDefaultSat = document.querySelectorAll("#flexSwitchCheckDefault")[5];
let SatAllday = document.querySelectorAll("#flexRadioDefault1")[10];
let Sathour = document.querySelectorAll('#flexRadioDefault1')[11];
let StateSat = document.querySelectorAll("#State")[10];
let StateSat2 = document.querySelectorAll("#State")[11];
var flagSat=1;
flexSwitchCheckDefaultSat.addEventListener('click', function () {
    if ((flagSat == 0)) {
        SatAllday.disabled = false;
        Sathour.disabled = false;
        flagSat = 1;
    } else {
        SatAllday.disabled = true;
        Sathour.disabled = true;
        SatAllday.checked = false;
        Sathour.checked = false;
        StateSat.disabled = true;
        StateSat2.disabled = true;
        flagSat = 0;
    };
});
SatAllday.addEventListener("click", function () {
    StateSat.disabled = true;
    StateSat2.disabled = true;
});
Sathour.addEventListener('click', function () {
    StateSat.disabled = false;
    StateSat2.disabled = false;
});

//sunday 
let flexSwitchCheckDefaultSun = document.querySelectorAll("#flexSwitchCheckDefault")[6];
let SunAllDay = document.querySelectorAll("#flexRadioDefault1")[12];
let Sunhour = document.querySelectorAll('#flexRadioDefault1')[13];
let StateSun = document.querySelectorAll('#State')[12];
let StateSun2 = document.querySelectorAll("#State")[13];
var flagSun=1;
flexSwitchCheckDefaultSun.addEventListener("click", function () {
    if (flagSun == 0) {
        SunAllDay.disabled = false;
        Sunhour.disabled = false;
        flagSun = 1;
    } else {
        SunAllDay.disabled = true;
        Sunhour.disabled = true;
        SunAllDay.checked = false;
        Sunhour.checked = false;
        StateSun.disabled = true;
        StateSun2.disabled = true;
        flagSun = 0;
    }
});
SunAllDay.addEventListener("click", function () {
    StateSun.disabled = true;
    StateSun2.disabled = true;
});
Sunhour.addEventListener('click', function () {
    StateSun.disabled = false;
    StateSun2.disabled = false;
});


CKEDITOR.replace('textPartType');
CKEDITOR.replace('textPartRule');
CKEDITOR.replace('textPartPark');
CKEDITOR.replace('textPartProjection');
CKEDITOR.replace('textPartClean');
CKEDITOR.replace('textPartTransportation');
CKEDITOR.replace('OtherAmenity');



///上傳照片
FilePond.parse(document.body);
