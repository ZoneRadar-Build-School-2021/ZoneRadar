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
///營業時間 radio/

//let openday = document.querySelector("#openday");
//openday.addEventListener("click", function () {
//    //星期一
//    let monAll = document.querySelectorAll("#flexRadioDefault1")[0];
//    monAll.setAttribute("value", "Y1");

//    let monHour = document.querySelectorAll("#flexRadioDefault1")[1];
//    monHour.setAttribute("value", "hr1");
    

//    //星期二
//    let tueOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
//    tueOpen.setAttribute("value", 2);
//    tueOpen.setAttribute("name", "OperatingDay");
//    tueOpen.setAttribute("id", 'flexSwitchCheckDefault2');
//    tueOpen.setAttribute("for", "flexSwitchCheckDefault2");
//    let openlabel = document.querySelectorAll("#openlabel")[1];
//    openlabel.setAttribute('for',"flexSwitchCheckDefault2")
//    //星期二的 全天 小時 label for設定
//    let tueAll = document.querySelectorAll('#flexRadioDefault1')[2];
//    let tueHour = document.querySelectorAll('#flexRadioDefault1')[3];
//    let allDayLabeltue = document.querySelectorAll("#allDayLabel")[1];
//    let hourLabeltue = document.querySelectorAll("#hourLabel")[1];

//    allDayLabeltue.setAttribute("for", "flexRadioDefault2");
//    hourLabeltue.setAttribute("for", "flexRadioDefault2");

//    tueAll.setAttribute("value", "Y2");
//    tueAll.setAttribute("name", "Hour2");
//    tueAll.setAttribute("id", "flexRadioDefault2");
    
//    tueHour.setAttribute("value", "hr2");
//    tueHour.setAttribute("name", "Hours2");
//    tueHour.setAttribute("checked","checked")
//    tueHour.setAttribute("id", "flexRadioDefault2")


//    //星期三
//    let wedOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
//    wedOpen.setAttribute("value", 3);
//    wedOpen.setAttribute("name", "OperatingDay");
//    wedOpen.setAttribute("id", 'flexSwitchCheckDefault3');
//    wedOpen.setAttribute("for", "flexSwitchCheckDefault3");
//    let openlabel3 = document.querySelectorAll("#openlabel")[2];
//    openlabel3.setAttribute('for', "flexSwitchCheckDefault3")

//    let wedAll = document.querySelectorAll('#flexRadioDefault1')[2];
//    let wedHour = document.querySelectorAll('#flexRadioDefault1')[3];
//    let allDayLabelwed = document.querySelectorAll("#allDayLabel")[2];
//    let hourLabelwed = document.querySelectorAll("#hourLabel")[2];
//    allDayLabelwed.setAttribute("for", "flexRadioDefault3");
//    hourLabelwed.setAttribute("for", "flexRadioDefault3");
//    wedAll.setAttribute("value", "Y3");
//    wedAll.setAttribute("name", "Hours3");
//    wedAll.setAttribute("id", "flexRadioDefault3");

//    wedHour.setAttribute("value", "hr3");
//    wedHour.setAttribute("name", "Hours3");
//    wedHour.setAttribute("id", "flexRadioDefault3")

//    //星期四
//    let ThuOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
//    ThuOpen.setAttribute("value", 4);
//    ThuOpen.setAttribute("name", "OperatingDay");
//    ThuOpen.setAttribute("id", 'flexSwitchCheckDefault4');
//    ThuOpen.setAttribute("for", "flexSwitchCheckDefault4");
//    let openlabel4 = document.querySelectorAll("#openlabel")[3];
//    openlabel4.setAttribute('for', "flexSwitchCheckDefault4")

//    let ThuAll = document.querySelectorAll('#flexRadioDefault1')[2];
//    let ThuHour = document.querySelectorAll('#flexRadioDefault1')[3];
//    let allDayLabelThu = document.querySelectorAll("#allDayLabel")[3];
//    let hourLabelThu = document.querySelectorAll("#hourLabel")[3];
//    allDayLabelThu.setAttribute("for", "flexRadioDefault4");
//    hourLabelThu.setAttribute("for", "flexRadioDefault4");

//    ThuAll.setAttribute("value", "Y4");
//    ThuAll.setAttribute("name", "Hours4");
//    ThuAll.setAttribute("id", "flexRadioDefault4");

//    ThuHour.setAttribute("value", "hr4");
//    ThuHour.setAttribute("name", "Hours4");
//    ThuHour.setAttribute("id", "flexRadioDefault4");

//    //星期五
//    let FriOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
//    FriOpen.setAttribute("value", 5);
//    FriOpen.setAttribute("name", "OperatingDay");
//    FriOpen.setAttribute("id", 'flexSwitchCheckDefault5');
//    FriOpen.setAttribute("for", "flexSwitchCheckDefault5");
//    let openlabel5 = document.querySelectorAll("#openlabel")[4];//改
//    openlabel5.setAttribute('for', "flexSwitchCheckDefault5");

//    let FriAll = document.querySelectorAll('#flexRadioDefault1')[2];
//    let FriHour = document.querySelectorAll('#flexRadioDefault1')[3];
//    let allDayLabelFri = document.querySelectorAll("#allDayLabel")[4];
//    let hourLabelFri = document.querySelectorAll("#hourLabel")[4];//改
//    allDayLabelFri.setAttribute("for", "flexRadioDefault5");
//    hourLabelFri.setAttribute("for", "flexRadioDefault5");

//    FriAll.setAttribute("value", "Y5");
//    FriAll.setAttribute("name", "Hours5");
//    FriAll.setAttribute("id", "flexRadioDefault5");

//    FriHour.setAttribute("value", "hr5");
//    FriHour.setAttribute("name", "Hours5");
//    FriHour.setAttribute("id", "flexRadioDefault5");

//    //星期六
//    let SatOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
//    SatOpen.setAttribute("value", 6);
//    SatOpen.setAttribute("name", "OperatingDay");
//    SatOpen.setAttribute("id", 'flexSwitchCheckDefault6');
//    SatOpen.setAttribute("for", "flexSwitchCheckDefault6");
//    let openlabel6 = document.querySelectorAll("#openlabel")[5];//改
//    openlabel6.setAttribute('for', "flexSwitchCheckDefault6");

//    let SatAll = document.querySelectorAll('#flexRadioDefault1')[2];
//    let SatHour = document.querySelectorAll('#flexRadioDefault1')[3];
//    let allDayLabelSat = document.querySelectorAll("#allDayLabel")[5];//改
//    let hourLabelSat = document.querySelectorAll("#hourLabel")[5];//改
//    allDayLabelSat.setAttribute("for", "flexRadioDefault6");
//    hourLabelSat.setAttribute("for", "flexRadioDefault6");

//    SatAll.setAttribute("value", "Y6");
//    SatAll.setAttribute("name", "Hours6");
//    SatAll.setAttribute("id", "flexRadioDefault6");

//    SatHour.setAttribute("value", "hr6");
//    SatHour.setAttribute("name", "Hours6");
//    SatHour.setAttribute("id", "flexRadioDefault6");

//    //星期日
//    let SunOpen = document.querySelectorAll("#flexSwitchCheckDefault")[1];
//    SunOpen.setAttribute("value", 7);
//    SunOpen.setAttribute("name", "OperatingDay");
//    SunOpen.setAttribute("id", 'flexSwitchCheckDefault7');
//    SunOpen.setAttribute("for", "flexSwitchCheckDefault7");
//    let openlabel7 = document.querySelectorAll("#openlabel")[6];//改
//    openlabel7.setAttribute('for', "flexSwitchCheckDefault7");

//    let SunAll = document.querySelectorAll('#flexRadioDefault1')[2];
//    let SunHour = document.querySelectorAll('#flexRadioDefault1')[3];
//    let allDayLabelSun = document.querySelectorAll("#allDayLabel")[6];//改
//    let hourLabelSun = document.querySelectorAll("#hourLabel")[6];//改
//    allDayLabelSun.setAttribute("for", "flexRadioDefault7");
//    hourLabelSun.setAttribute("for", "flexRadioDefault7");

//    SunAll.setAttribute("value", "Y7");
//    SunAll.setAttribute("name", "Hours7");
//    SunAll.setAttribute("id", "flexRadioDefault7");

//    SunHour.setAttribute("value", "hr7");
//    SunHour.setAttribute("name", "Hours7");
//    SunHour.setAttribute("id", "flexRadioDefault7");

//});
    



var flag = 1;
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
    if (flag == 0) {
        flexRadioDefault1.disabled = false;
        flexRadioDefault2.disabled = false;
        flag = 1;
    } else {
        flexRadioDefault1.disabled = true;
        flexRadioDefault2.disabled = true;
        StateMon.disabled = true;
        StateMon2.disabled = true;
        flexRadioDefault2.checked = false;
        flexRadioDefault1.checked = false;
        flag = 0;
    }
});
flexRadioDefault1.addEventListener("click", function () {
    StateMon.disabled = true;
    StateMon2.disabled = true;
    StateMon2.length = 1;
    StateMon.length = 1;
    let TimeoptionS = document.querySelectorAll("#Timeoption")[0];
    TimeoptionS.innerHTML= "06:00";
    TimeoptionS.value = "06:00";

    let TimeoptionE = document.querySelectorAll("#Timeoption")[1];
    TimeoptionE.value= "23:00";
    TimeoptionE.innerHTML ="23:00";

});

flexRadioDefault2.addEventListener('click', function () {

    let StateMon = document.querySelectorAll("#State")[0];
    let StateMon2 = document.querySelectorAll("#State")[1];
    StateMon.disabled = false;
    StateMon2.disabled = false;

    let option1 = document.createElement("option"); option1.innerHTML = "07:00"; option1.value = "07:00";
    let option2 = document.createElement("option"); option2.innerHTML = "08:00"; option2.value = "08:00";
    let option3 = document.createElement("option"); option3.innerHTML = "09:00"; option3.value = "09:00";
    let option4 = document.createElement("option"); option4.innerHTML = "10:00"; option4.value = "10:00";
    let option5 = document.createElement("option"); option5.innerHTML = "11:00"; option5.value = "11:00";
    let option6 = document.createElement("option"); option6.innerHTML = "12:00"; option6.value = "12:00";
    let option7 = document.createElement("option"); option7.innerHTML = "13:00"; option7.value = "13:00";
    let option8 = document.createElement("option"); option8.innerHTML = "14:00"; option8.value = "14:00";
    let option9 = document.createElement("option"); option9.innerHTML = "15:00"; option9.value = "15:00";
    let option10 = document.createElement("option"); option10.innerHTML = "16:00"; option10.value = "16:00";
    let option11 = document.createElement("option"); option11.innerHTML = "17:00"; option11.value = "17:00";
    let option12 = document.createElement("option"); option12.innerHTML = "18:00"; option12.value = "18:00";
    let option13 = document.createElement("option"); option13.innerHTML = "19:00"; option13.value = "19:00";
    let option14 = document.createElement("option"); option14.innerHTML = "20:00"; option14.value = "20:00";
    let option15 = document.createElement("option"); option15.innerHTML = "21:00"; option15.value = "21:00";
    let option16 = document.createElement("option"); option16.innerHTML = "22:00"; option16.value = "22:00";
    let option17 = document.createElement("option"); option17.innerHTML = "23:00"; option17.value = "23:00";

    let option18 = document.createElement("option"); option18.innerHTML = "07:00"; option18.value = "07:00";
    let option19 = document.createElement("option"); option19.innerHTML = "08:00"; option19.value = "08:00";
    let option20 = document.createElement("option"); option20.innerHTML = "09:00"; option20.value = "09:00";
    let option21 = document.createElement("option"); option21.innerHTML = "10:00"; option21.value = "10:00";
    let option22 = document.createElement("option"); option22.innerHTML = "11:00"; option22.value = "11:00";
    let option23 = document.createElement("option"); option23.innerHTML = "12:00"; option23.value = "12:00";
    let option24 = document.createElement("option"); option24.innerHTML = "13:00"; option24.value = "13:00";
    let option25 = document.createElement("option"); option25.innerHTML = "14:00"; option25.value = "14:00";
    let option26 = document.createElement("option"); option26.innerHTML = "15:00"; option26.value = "15:00";
    let option27 = document.createElement("option"); option27.innerHTML = "16:00"; option27.value = "16:00";
    let option28 = document.createElement("option"); option28.innerHTML = "17:00"; option28.value = "17:00";
    let option29 = document.createElement("option"); option29.innerHTML = "18:00"; option29.value = "18:00";
    let option30 = document.createElement("option"); option30.innerHTML = "19:00"; option30.value = "19:00";
    let option31 = document.createElement("option"); option31.innerHTML = "20:00"; option31.value = "20:00";
    let option32 = document.createElement("option"); option32.innerHTML = "21:00"; option32.value = "21:00";
    let option33 = document.createElement("option"); option33.innerHTML = "22:00"; option33.value = "22:00";
    let option34 = document.createElement("option"); option34.innerHTML = "06:00"; option34.value = "06:00";

    StateMon.appendChild(option1);
    StateMon.appendChild(option2);
    StateMon.appendChild(option3);
    StateMon.appendChild(option4);
    StateMon.appendChild(option5);
    StateMon.appendChild(option6);
    StateMon.appendChild(option7);
    StateMon.appendChild(option8);
    StateMon.appendChild(option9);
    StateMon.appendChild(option10);
    StateMon.appendChild(option11);
    StateMon.appendChild(option12);
    StateMon.appendChild(option13);
    StateMon.appendChild(option14);
    StateMon.appendChild(option15);
    StateMon.appendChild(option16);
    StateMon.appendChild(option17);

    StateMon2.appendChild(option18);
    StateMon2.appendChild(option19);
    StateMon2.appendChild(option20);
    StateMon2.appendChild(option21);
    StateMon2.appendChild(option22);
    StateMon2.appendChild(option23);
    StateMon2.appendChild(option24);
    StateMon2.appendChild(option25);
    StateMon2.appendChild(option26);
    StateMon2.appendChild(option27);
    StateMon2.appendChild(option28);
    StateMon2.appendChild(option29);
    StateMon2.appendChild(option30);
    StateMon2.appendChild(option31);
    StateMon2.appendChild(option32);
    StateMon2.appendChild(option33);
    StateMon2.appendChild(option34);
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
    StateTue.length = 1;
    StateTue2.length = 1;
    let TimeoptionS = document.querySelectorAll("#Timeoption")[2];
    TimeoptionS.innerHTML= "06:00";
    TimeoptionS.value= "06:00";

    let TimeoptionE = document.querySelectorAll("#Timeoption")[3];
    TimeoptionE.value = "23:00";
    TimeoptionE.innerHTML = "23:00";
    StateTue.disabled = true;
    StateTue2.disabled = true;

});
Tuehour.addEventListener('click', function () {

    let StateTue = document.querySelectorAll("#State")[2];
    let StateTue2 = document.querySelectorAll("#State")[3];
    StateTue.disabled = false;
    StateTue2.disabled = false;
    let option1 = document.createElement("option"); option1.innerHTML = "07:00"; option1.value = "07:00";
    let option2 = document.createElement("option"); option2.innerHTML = "08:00"; option2.value = "08:00";
    let option3 = document.createElement("option"); option3.innerHTML = "09:00"; option3.value = "09:00";
    let option4 = document.createElement("option"); option4.innerHTML = "10:00"; option4.value = "10:00";
    let option5 = document.createElement("option"); option5.innerHTML = "11:00"; option5.value = "11:00";
    let option6 = document.createElement("option"); option6.innerHTML = "12:00"; option6.value = "12:00";
    let option7 = document.createElement("option"); option7.innerHTML = "13:00"; option7.value = "13:00";
    let option8 = document.createElement("option"); option8.innerHTML = "14:00"; option8.value = "14:00";
    let option9 = document.createElement("option"); option9.innerHTML = "15:00"; option9.value = "15:00";
    let option10 = document.createElement("option"); option10.innerHTML = "16:00"; option10.value = "16:00";
    let option11 = document.createElement("option"); option11.innerHTML = "17:00"; option11.value = "17:00";
    let option12 = document.createElement("option"); option12.innerHTML = "18:00"; option12.value = "18:00";
    let option13 = document.createElement("option"); option13.innerHTML = "19:00"; option13.value = "19:00";
    let option14 = document.createElement("option"); option14.innerHTML = "20:00"; option14.value = "20:00";
    let option15 = document.createElement("option"); option15.innerHTML = "21:00"; option15.value = "21:00";
    let option16 = document.createElement("option"); option16.innerHTML = "22:00"; option16.value = "22:00";
    let option17 = document.createElement("option"); option17.innerHTML = "23:00"; option17.value = "23:00";

    let option18 = document.createElement("option"); option18.innerHTML = "07:00"; option18.value = "07:00";
    let option19 = document.createElement("option"); option19.innerHTML = "08:00"; option19.value = "08:00";
    let option20 = document.createElement("option"); option20.innerHTML = "09:00"; option20.value = "09:00";
    let option21 = document.createElement("option"); option21.innerHTML = "10:00"; option21.value = "10:00";
    let option22 = document.createElement("option"); option22.innerHTML = "11:00"; option22.value = "11:00";
    let option23 = document.createElement("option"); option23.innerHTML = "12:00"; option23.value = "12:00";
    let option24 = document.createElement("option"); option24.innerHTML = "13:00"; option24.value = "13:00";
    let option25 = document.createElement("option"); option25.innerHTML = "14:00"; option25.value = "14:00";
    let option26 = document.createElement("option"); option26.innerHTML = "15:00"; option26.value = "15:00";
    let option27 = document.createElement("option"); option27.innerHTML = "16:00"; option27.value = "16:00";
    let option28 = document.createElement("option"); option28.innerHTML = "17:00"; option28.value = "17:00";
    let option29 = document.createElement("option"); option29.innerHTML = "18:00"; option29.value = "18:00";
    let option30 = document.createElement("option"); option30.innerHTML = "19:00"; option30.value = "19:00";
    let option31 = document.createElement("option"); option31.innerHTML = "20:00"; option31.value = "20:00";
    let option32 = document.createElement("option"); option32.innerHTML = "21:00"; option32.value = "21:00";
    let option33 = document.createElement("option"); option33.innerHTML = "22:00"; option33.value = "22:00";
    let option34 = document.createElement("option"); option34.innerHTML = "06:00"; option34.value = "06:00";

    StateTue.appendChild(option1);
    StateTue.appendChild(option2);
    StateTue.appendChild(option3);
    StateTue.appendChild(option4);
    StateTue.appendChild(option5);
    StateTue.appendChild(option6);
    StateTue.appendChild(option7);
    StateTue.appendChild(option8);
    StateTue.appendChild(option9);
    StateTue.appendChild(option10);
    StateTue.appendChild(option11);
    StateTue.appendChild(option12);
    StateTue.appendChild(option13);
    StateTue.appendChild(option14);
    StateTue.appendChild(option15);
    StateTue.appendChild(option16);
    StateTue.appendChild(option17);

    StateTue2.appendChild(option18);
    StateTue2.appendChild(option19);
    StateTue2.appendChild(option20);
    StateTue2.appendChild(option21);
    StateTue2.appendChild(option22);
    StateTue2.appendChild(option23);
    StateTue2.appendChild(option24);
    StateTue2.appendChild(option25);
    StateTue2.appendChild(option26);
    StateTue2.appendChild(option27);
    StateTue2.appendChild(option28);
    StateTue2.appendChild(option29);
    StateTue2.appendChild(option30);
    StateTue2.appendChild(option31);
    StateTue2.appendChild(option32);
    StateTue2.appendChild(option33);
    StateTue2.appendChild(option34);

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
    let TimeoptionS = document.querySelectorAll("#Timeoption")[4];
    TimeoptionS.innerHTML = "06:00";
    TimeoptionS.value = "06:00";

    let TimeoptionE = document.querySelectorAll("#Timeoption")[5];
    TimeoptionE.value = "23:00";
    TimeoptionE.innerHTML = "23:00";
    StateWed.length = 1;
    StateWed2.length = 1;
})

Wedhour.addEventListener('click', function () {
    let StateWed = document.querySelectorAll("#State")[4];
    let StateWed2 = document.querySelectorAll("#State")[5];
    StateWed.disabled = false;
    StateWed2.disabled = false;

    let option1 = document.createElement("option"); option1.innerHTML = "07:00"; option1.value = "07:00";
    let option2 = document.createElement("option"); option2.innerHTML = "08:00"; option2.value = "08:00";
    let option3 = document.createElement("option"); option3.innerHTML = "09:00"; option3.value = "09:00";
    let option4 = document.createElement("option"); option4.innerHTML = "10:00"; option4.value = "10:00";
    let option5 = document.createElement("option"); option5.innerHTML = "11:00"; option5.value = "11:00";
    let option6 = document.createElement("option"); option6.innerHTML = "12:00"; option6.value = "12:00";
    let option7 = document.createElement("option"); option7.innerHTML = "13:00"; option7.value = "13:00";
    let option8 = document.createElement("option"); option8.innerHTML = "14:00"; option8.value = "14:00";
    let option9 = document.createElement("option"); option9.innerHTML = "15:00"; option9.value = "15:00";
    let option10 = document.createElement("option"); option10.innerHTML = "16:00"; option10.value = "16:00";
    let option11 = document.createElement("option"); option11.innerHTML = "17:00"; option11.value = "17:00";
    let option12 = document.createElement("option"); option12.innerHTML = "18:00"; option12.value = "18:00";
    let option13 = document.createElement("option"); option13.innerHTML = "19:00"; option13.value = "19:00";
    let option14 = document.createElement("option"); option14.innerHTML = "20:00"; option14.value = "20:00";
    let option15 = document.createElement("option"); option15.innerHTML = "21:00"; option15.value = "21:00";
    let option16 = document.createElement("option"); option16.innerHTML = "22:00"; option16.value = "22:00";
    let option17 = document.createElement("option"); option17.innerHTML = "23:00"; option17.value = "23:00";

    let option18 = document.createElement("option"); option18.innerHTML = "07:00"; option18.value = "07:00";
    let option19 = document.createElement("option"); option19.innerHTML = "08:00"; option19.value = "08:00";
    let option20 = document.createElement("option"); option20.innerHTML = "09:00"; option20.value = "09:00";
    let option21 = document.createElement("option"); option21.innerHTML = "10:00"; option21.value = "10:00";
    let option22 = document.createElement("option"); option22.innerHTML = "11:00"; option22.value = "11:00";
    let option23 = document.createElement("option"); option23.innerHTML = "12:00"; option23.value = "12:00";
    let option24 = document.createElement("option"); option24.innerHTML = "13:00"; option24.value = "13:00";
    let option25 = document.createElement("option"); option25.innerHTML = "14:00"; option25.value = "14:00";
    let option26 = document.createElement("option"); option26.innerHTML = "15:00"; option26.value = "15:00";
    let option27 = document.createElement("option"); option27.innerHTML = "16:00"; option27.value = "16:00";
    let option28 = document.createElement("option"); option28.innerHTML = "17:00"; option28.value = "17:00";
    let option29 = document.createElement("option"); option29.innerHTML = "18:00"; option29.value = "18:00";
    let option30 = document.createElement("option"); option30.innerHTML = "19:00"; option30.value = "19:00";
    let option31 = document.createElement("option"); option31.innerHTML = "20:00"; option31.value = "20:00";
    let option32 = document.createElement("option"); option32.innerHTML = "21:00"; option32.value = "21:00";
    let option33 = document.createElement("option"); option33.innerHTML = "22:00"; option33.value = "22:00";
    let option34 = document.createElement("option"); option34.innerHTML = "06:00"; option34.value = "06:00";

    StateWed.appendChild(option1);
    StateWed.appendChild(option2);
    StateWed.appendChild(option3);
    StateWed.appendChild(option4);
    StateWed.appendChild(option5);
    StateWed.appendChild(option6);
    StateWed.appendChild(option7);
    StateWed.appendChild(option8);
    StateWed.appendChild(option9);
    StateWed.appendChild(option10);
    StateWed.appendChild(option11);
    StateWed.appendChild(option12);
    StateWed.appendChild(option13);
    StateWed.appendChild(option14);
    StateWed.appendChild(option15);
    StateWed.appendChild(option16);
    StateWed.appendChild(option17);

    StateWed2.appendChild(option18);
    StateWed2.appendChild(option19);
    StateWed2.appendChild(option20);
    StateWed2.appendChild(option21);
    StateWed2.appendChild(option22);
    StateWed2.appendChild(option23);
    StateWed2.appendChild(option24);
    StateWed2.appendChild(option25);
    StateWed2.appendChild(option26);
    StateWed2.appendChild(option27);
    StateWed2.appendChild(option28);
    StateWed2.appendChild(option29);
    StateWed2.appendChild(option30);
    StateWed2.appendChild(option31);
    StateWed2.appendChild(option32);
    StateWed2.appendChild(option33);
    StateWed2.appendChild(option34);

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
    StateThu.length = 1;
    StateThu2.length = 1;
    let TimeoptionS = document.querySelectorAll("#Timeoption")[6];
    TimeoptionS.innerHTML = "06:00";
    TimeoptionS.value = "06:00";

    let TimeoptionE = document.querySelectorAll("#Timeoption")[7];
    TimeoptionE.value = "23:00";
    TimeoptionE.innerHTML = "23:00";
   
})
Thuhour.addEventListener("click", function () {
    StateThu.disabled = false;
    StateThu2.disabled = false;

    let option1 = document.createElement("option"); option1.innerHTML = "07:00"; option1.value = "07:00";
    let option2 = document.createElement("option"); option2.innerHTML = "08:00"; option2.value = "08:00";
    let option3 = document.createElement("option"); option3.innerHTML = "09:00"; option3.value = "09:00";
    let option4 = document.createElement("option"); option4.innerHTML = "10:00"; option4.value = "10:00";
    let option5 = document.createElement("option"); option5.innerHTML = "11:00"; option5.value = "11:00";
    let option6 = document.createElement("option"); option6.innerHTML = "12:00"; option6.value = "12:00";
    let option7 = document.createElement("option"); option7.innerHTML = "13:00"; option7.value = "13:00";
    let option8 = document.createElement("option"); option8.innerHTML = "14:00"; option8.value = "14:00";
    let option9 = document.createElement("option"); option9.innerHTML = "15:00"; option9.value = "15:00";
    let option10 = document.createElement("option"); option10.innerHTML = "16:00"; option10.value = "16:00";
    let option11 = document.createElement("option"); option11.innerHTML = "17:00"; option11.value = "17:00";
    let option12 = document.createElement("option"); option12.innerHTML = "18:00"; option12.value = "18:00";
    let option13 = document.createElement("option"); option13.innerHTML = "19:00"; option13.value = "19:00";
    let option14 = document.createElement("option"); option14.innerHTML = "20:00"; option14.value = "20:00";
    let option15 = document.createElement("option"); option15.innerHTML = "21:00"; option15.value = "21:00";
    let option16 = document.createElement("option"); option16.innerHTML = "22:00"; option16.value = "22:00";
    let option17 = document.createElement("option"); option17.innerHTML = "23:00"; option17.value = "23:00";

    let option18 = document.createElement("option"); option18.innerHTML = "07:00"; option18.value = "07:00";
    let option19 = document.createElement("option"); option19.innerHTML = "08:00"; option19.value = "08:00";
    let option20 = document.createElement("option"); option20.innerHTML = "09:00"; option20.value = "09:00";
    let option21 = document.createElement("option"); option21.innerHTML = "10:00"; option21.value = "10:00";
    let option22 = document.createElement("option"); option22.innerHTML = "11:00"; option22.value = "11:00";
    let option23 = document.createElement("option"); option23.innerHTML = "12:00"; option23.value = "12:00";
    let option24 = document.createElement("option"); option24.innerHTML = "13:00"; option24.value = "13:00";
    let option25 = document.createElement("option"); option25.innerHTML = "14:00"; option25.value = "14:00";
    let option26 = document.createElement("option"); option26.innerHTML = "15:00"; option26.value = "15:00";
    let option27 = document.createElement("option"); option27.innerHTML = "16:00"; option27.value = "16:00";
    let option28 = document.createElement("option"); option28.innerHTML = "17:00"; option28.value = "17:00";
    let option29 = document.createElement("option"); option29.innerHTML = "18:00"; option29.value = "18:00";
    let option30 = document.createElement("option"); option30.innerHTML = "19:00"; option30.value = "19:00";
    let option31 = document.createElement("option"); option31.innerHTML = "20:00"; option31.value = "20:00";
    let option32 = document.createElement("option"); option32.innerHTML = "21:00"; option32.value = "21:00";
    let option33 = document.createElement("option"); option33.innerHTML = "22:00"; option33.value = "22:00";
    let option34 = document.createElement("option"); option34.innerHTML = "06:00"; option34.value = "06:00";

    StateThu.appendChild(option1);
    StateThu.appendChild(option2);
    StateThu.appendChild(option3);
    StateThu.appendChild(option4);
    StateThu.appendChild(option5);
    StateThu.appendChild(option6);
    StateThu.appendChild(option7);
    StateThu.appendChild(option8);
    StateThu.appendChild(option9);
    StateThu.appendChild(option10);
    StateThu.appendChild(option11);
    StateThu.appendChild(option12);
    StateThu.appendChild(option13);
    StateThu.appendChild(option14);
    StateThu.appendChild(option15);
    StateThu.appendChild(option16);
    StateThu.appendChild(option17);

    StateThu2.appendChild(option18);
    StateThu2.appendChild(option19);
    StateThu2.appendChild(option20);
    StateThu2.appendChild(option21);
    StateThu2.appendChild(option22);
    StateThu2.appendChild(option23);
    StateThu2.appendChild(option24);
    StateThu2.appendChild(option25);
    StateThu2.appendChild(option26);
    StateThu2.appendChild(option27);
    StateThu2.appendChild(option28);
    StateThu2.appendChild(option29);
    StateThu2.appendChild(option30);
    StateThu2.appendChild(option31);
    StateThu2.appendChild(option32);
    StateThu2.appendChild(option33);
    StateThu2.appendChild(option34);

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
    StateFri.length = 1;
    StateFri2.length = 1;
    let TimeoptionS = document.querySelectorAll("#Timeoption")[8];
    TimeoptionS.innerHTML = "06:00";
    TimeoptionS.value = "06:00";

    let TimeoptionE = document.querySelectorAll("#Timeoption")[9];
    TimeoptionE.value = "23:00";
    TimeoptionE.innerHTML = "23:00";

});
Fridayhour.addEventListener("click", function () {
    StateFri.disabled = false;
    StateFri2.disabled = false;
    let option1 = document.createElement("option"); option1.innerHTML = "07:00"; option1.value = "07:00";
    let option2 = document.createElement("option"); option2.innerHTML = "08:00"; option2.value = "08:00";
    let option3 = document.createElement("option"); option3.innerHTML = "09:00"; option3.value = "09:00";
    let option4 = document.createElement("option"); option4.innerHTML = "10:00"; option4.value = "10:00";
    let option5 = document.createElement("option"); option5.innerHTML = "11:00"; option5.value = "11:00";
    let option6 = document.createElement("option"); option6.innerHTML = "12:00"; option6.value = "12:00";
    let option7 = document.createElement("option"); option7.innerHTML = "13:00"; option7.value = "13:00";
    let option8 = document.createElement("option"); option8.innerHTML = "14:00"; option8.value = "14:00";
    let option9 = document.createElement("option"); option9.innerHTML = "15:00"; option9.value = "15:00";
    let option10 = document.createElement("option"); option10.innerHTML = "16:00"; option10.value = "16:00";
    let option11 = document.createElement("option"); option11.innerHTML = "17:00"; option11.value = "17:00";
    let option12 = document.createElement("option"); option12.innerHTML = "18:00"; option12.value = "18:00";
    let option13 = document.createElement("option"); option13.innerHTML = "19:00"; option13.value = "19:00";
    let option14 = document.createElement("option"); option14.innerHTML = "20:00"; option14.value = "20:00";
    let option15 = document.createElement("option"); option15.innerHTML = "21:00"; option15.value = "21:00";
    let option16 = document.createElement("option"); option16.innerHTML = "22:00"; option16.value = "22:00";
    let option17 = document.createElement("option"); option17.innerHTML = "23:00"; option17.value = "23:00";

    let option18 = document.createElement("option"); option18.innerHTML = "07:00"; option18.value = "07:00";
    let option19 = document.createElement("option"); option19.innerHTML = "08:00"; option19.value = "08:00";
    let option20 = document.createElement("option"); option20.innerHTML = "09:00"; option20.value = "09:00";
    let option21 = document.createElement("option"); option21.innerHTML = "10:00"; option21.value = "10:00";
    let option22 = document.createElement("option"); option22.innerHTML = "11:00"; option22.value = "11:00";
    let option23 = document.createElement("option"); option23.innerHTML = "12:00"; option23.value = "12:00";
    let option24 = document.createElement("option"); option24.innerHTML = "13:00"; option24.value = "13:00";
    let option25 = document.createElement("option"); option25.innerHTML = "14:00"; option25.value = "14:00";
    let option26 = document.createElement("option"); option26.innerHTML = "15:00"; option26.value = "15:00";
    let option27 = document.createElement("option"); option27.innerHTML = "16:00"; option27.value = "16:00";
    let option28 = document.createElement("option"); option28.innerHTML = "17:00"; option28.value = "17:00";
    let option29 = document.createElement("option"); option29.innerHTML = "18:00"; option29.value = "18:00";
    let option30 = document.createElement("option"); option30.innerHTML = "19:00"; option30.value = "19:00";
    let option31 = document.createElement("option"); option31.innerHTML = "20:00"; option31.value = "20:00";
    let option32 = document.createElement("option"); option32.innerHTML = "21:00"; option32.value = "21:00";
    let option33 = document.createElement("option"); option33.innerHTML = "22:00"; option33.value = "22:00";
    let option34 = document.createElement("option"); option34.innerHTML = "06:00"; option34.value = "06:00";

    StateFri.appendChild(option1);
    StateFri.appendChild(option2);
    StateFri.appendChild(option3);
    StateFri.appendChild(option4);
    StateFri.appendChild(option5);
    StateFri.appendChild(option6);
    StateFri.appendChild(option7);
    StateFri.appendChild(option8);
    StateFri.appendChild(option9);
    StateFri.appendChild(option10);
    StateFri.appendChild(option11);
    StateFri.appendChild(option12);
    StateFri.appendChild(option13);
    StateFri.appendChild(option14);
    StateFri.appendChild(option15);
    StateFri.appendChild(option16);
    StateFri.appendChild(option17);

    StateFri2.appendChild(option18);
    StateFri2.appendChild(option19);
    StateFri2.appendChild(option20);
    StateFri2.appendChild(option21);
    StateFri2.appendChild(option22);
    StateFri2.appendChild(option23);
    StateFri2.appendChild(option24);
    StateFri2.appendChild(option25);
    StateFri2.appendChild(option26);
    StateFri2.appendChild(option27);
    StateFri2.appendChild(option28);
    StateFri2.appendChild(option29);
    StateFri2.appendChild(option30);
    StateFri2.appendChild(option31);
    StateFri2.appendChild(option32);
    StateFri2.appendChild(option33);
    StateFri2.appendChild(option34);

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
    StateSat.length = 1;
    StateSat2.length = 1;
    let TimeoptionS = document.querySelectorAll("#Timeoption")[10];
    TimeoptionS.innerHTML = "06:00";
    TimeoptionS.value = "06:00";

    let TimeoptionE = document.querySelectorAll("#Timeoption")[11];
    TimeoptionE.value = "23:00";
    TimeoptionE.innerHTML = "23:00";

});
Sathour.addEventListener('click', function () {
    StateSat.disabled = false;
    StateSat2.disabled = false;

    let option1 = document.createElement("option"); option1.innerHTML = "07:00"; option1.value = "07:00";
    let option2 = document.createElement("option"); option2.innerHTML = "08:00"; option2.value = "08:00";
    let option3 = document.createElement("option"); option3.innerHTML = "09:00"; option3.value = "09:00";
    let option4 = document.createElement("option"); option4.innerHTML = "10:00"; option4.value = "10:00";
    let option5 = document.createElement("option"); option5.innerHTML = "11:00"; option5.value = "11:00";
    let option6 = document.createElement("option"); option6.innerHTML = "12:00"; option6.value = "12:00";
    let option7 = document.createElement("option"); option7.innerHTML = "13:00"; option7.value = "13:00";
    let option8 = document.createElement("option"); option8.innerHTML = "14:00"; option8.value = "14:00";
    let option9 = document.createElement("option"); option9.innerHTML = "15:00"; option9.value = "15:00";
    let option10 = document.createElement("option"); option10.innerHTML = "16:00"; option10.value = "16:00";
    let option11 = document.createElement("option"); option11.innerHTML = "17:00"; option11.value = "17:00";
    let option12 = document.createElement("option"); option12.innerHTML = "18:00"; option12.value = "18:00";
    let option13 = document.createElement("option"); option13.innerHTML = "19:00"; option13.value = "19:00";
    let option14 = document.createElement("option"); option14.innerHTML = "20:00"; option14.value = "20:00";
    let option15 = document.createElement("option"); option15.innerHTML = "21:00"; option15.value = "21:00";
    let option16 = document.createElement("option"); option16.innerHTML = "22:00"; option16.value = "22:00";
    let option17 = document.createElement("option"); option17.innerHTML = "23:00"; option17.value = "23:00";

    let option18 = document.createElement("option"); option18.innerHTML = "07:00"; option18.value = "07:00";
    let option19 = document.createElement("option"); option19.innerHTML = "08:00"; option19.value = "08:00";
    let option20 = document.createElement("option"); option20.innerHTML = "09:00"; option20.value = "09:00";
    let option21 = document.createElement("option"); option21.innerHTML = "10:00"; option21.value = "10:00";
    let option22 = document.createElement("option"); option22.innerHTML = "11:00"; option22.value = "11:00";
    let option23 = document.createElement("option"); option23.innerHTML = "12:00"; option23.value = "12:00";
    let option24 = document.createElement("option"); option24.innerHTML = "13:00"; option24.value = "13:00";
    let option25 = document.createElement("option"); option25.innerHTML = "14:00"; option25.value = "14:00";
    let option26 = document.createElement("option"); option26.innerHTML = "15:00"; option26.value = "15:00";
    let option27 = document.createElement("option"); option27.innerHTML = "16:00"; option27.value = "16:00";
    let option28 = document.createElement("option"); option28.innerHTML = "17:00"; option28.value = "17:00";
    let option29 = document.createElement("option"); option29.innerHTML = "18:00"; option29.value = "18:00";
    let option30 = document.createElement("option"); option30.innerHTML = "19:00"; option30.value = "19:00";
    let option31 = document.createElement("option"); option31.innerHTML = "20:00"; option31.value = "20:00";
    let option32 = document.createElement("option"); option32.innerHTML = "21:00"; option32.value = "21:00";
    let option33 = document.createElement("option"); option33.innerHTML = "22:00"; option33.value = "22:00";
    let option34 = document.createElement("option"); option34.innerHTML = "06:00"; option34.value = "06:00";

    StateSat.appendChild(option1);
    StateSat.appendChild(option2);
    StateSat.appendChild(option3);
    StateSat.appendChild(option4);
    StateSat.appendChild(option5);
    StateSat.appendChild(option6);
    StateSat.appendChild(option7);
    StateSat.appendChild(option8);
    StateSat.appendChild(option9);
    StateSat.appendChild(option10);
    StateSat.appendChild(option11);
    StateSat.appendChild(option12);
    StateSat.appendChild(option13);
    StateSat.appendChild(option14);
    StateSat.appendChild(option15);
    StateSat.appendChild(option16);
    StateSat.appendChild(option17);

    StateSat2.appendChild(option18);
    StateSat2.appendChild(option19);
    StateSat2.appendChild(option20);
    StateSat2.appendChild(option21);
    StateSat2.appendChild(option22);
    StateSat2.appendChild(option23);
    StateSat2.appendChild(option24);
    StateSat2.appendChild(option25);
    StateSat2.appendChild(option26);
    StateSat2.appendChild(option27);
    StateSat2.appendChild(option28);
    StateSat2.appendChild(option29);
    StateSat2.appendChild(option30);
    StateSat2.appendChild(option31);
    StateSat2.appendChild(option32);
    StateSat2.appendChild(option33);
    StateSat2.appendChild(option34);

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
    StateSun.length = 1;
    StateSun2.length = 1;
    let TimeoptionS = document.querySelectorAll("#Timeoption")[12];
    TimeoptionS.innerHTML = "06:00";
    TimeoptionS.value = "06:00";

    let TimeoptionE = document.querySelectorAll("#Timeoption")[13];
    TimeoptionE.value = "23:00";
    TimeoptionE.innerHTML = "23:00";
});
Sunhour.addEventListener('click', function () {
    StateSun.disabled = false;
    StateSun2.disabled = false;

    let option1 = document.createElement("option"); option1.innerHTML = "07:00"; option1.value = "07:00";
    let option2 = document.createElement("option"); option2.innerHTML = "08:00"; option2.value = "08:00";
    let option3 = document.createElement("option"); option3.innerHTML = "09:00"; option3.value = "09:00";
    let option4 = document.createElement("option"); option4.innerHTML = "10:00"; option4.value = "10:00";
    let option5 = document.createElement("option"); option5.innerHTML = "11:00"; option5.value = "11:00";
    let option6 = document.createElement("option"); option6.innerHTML = "12:00"; option6.value = "12:00";
    let option7 = document.createElement("option"); option7.innerHTML = "13:00"; option7.value = "13:00";
    let option8 = document.createElement("option"); option8.innerHTML = "14:00"; option8.value = "14:00";
    let option9 = document.createElement("option"); option9.innerHTML = "15:00"; option9.value = "15:00";
    let option10 = document.createElement("option"); option10.innerHTML = "16:00"; option10.value = "16:00";
    let option11 = document.createElement("option"); option11.innerHTML = "17:00"; option11.value = "17:00";
    let option12 = document.createElement("option"); option12.innerHTML = "18:00"; option12.value = "18:00";
    let option13 = document.createElement("option"); option13.innerHTML = "19:00"; option13.value = "19:00";
    let option14 = document.createElement("option"); option14.innerHTML = "20:00"; option14.value = "20:00";
    let option15 = document.createElement("option"); option15.innerHTML = "21:00"; option15.value = "21:00";
    let option16 = document.createElement("option"); option16.innerHTML = "22:00"; option16.value = "22:00";
    let option17 = document.createElement("option"); option17.innerHTML = "23:00"; option17.value = "23:00";

    let option18 = document.createElement("option"); option18.innerHTML = "07:00"; option18.value = "07:00";
    let option19 = document.createElement("option"); option19.innerHTML = "08:00"; option19.value = "08:00";
    let option20 = document.createElement("option"); option20.innerHTML = "09:00"; option20.value = "09:00";
    let option21 = document.createElement("option"); option21.innerHTML = "10:00"; option21.value = "10:00";
    let option22 = document.createElement("option"); option22.innerHTML = "11:00"; option22.value = "11:00";
    let option23 = document.createElement("option"); option23.innerHTML = "12:00"; option23.value = "12:00";
    let option24 = document.createElement("option"); option24.innerHTML = "13:00"; option24.value = "13:00";
    let option25 = document.createElement("option"); option25.innerHTML = "14:00"; option25.value = "14:00";
    let option26 = document.createElement("option"); option26.innerHTML = "15:00"; option26.value = "15:00";
    let option27 = document.createElement("option"); option27.innerHTML = "16:00"; option27.value = "16:00";
    let option28 = document.createElement("option"); option28.innerHTML = "17:00"; option28.value = "17:00";
    let option29 = document.createElement("option"); option29.innerHTML = "18:00"; option29.value = "18:00";
    let option30 = document.createElement("option"); option30.innerHTML = "19:00"; option30.value = "19:00";
    let option31 = document.createElement("option"); option31.innerHTML = "20:00"; option31.value = "20:00";
    let option32 = document.createElement("option"); option32.innerHTML = "21:00"; option32.value = "21:00";
    let option33 = document.createElement("option"); option33.innerHTML = "22:00"; option33.value = "22:00";
    let option34 = document.createElement("option"); option34.innerHTML = "06:00"; option34.value = "06:00";

    StateSun.appendChild(option1);
    StateSun.appendChild(option2);
    StateSun.appendChild(option3);
    StateSun.appendChild(option4);
    StateSun.appendChild(option5);
    StateSun.appendChild(option6);
    StateSun.appendChild(option7);
    StateSun.appendChild(option8);
    StateSun.appendChild(option9);
    StateSun.appendChild(option10);
    StateSun.appendChild(option11);
    StateSun.appendChild(option12);
    StateSun.appendChild(option13);
    StateSun.appendChild(option14);
    StateSun.appendChild(option15);
    StateSun.appendChild(option16);
    StateSun.appendChild(option17);

    StateSun2.appendChild(option18);
    StateSun2.appendChild(option19);
    StateSun2.appendChild(option20);
    StateSun2.appendChild(option21);
    StateSun2.appendChild(option22);
    StateSun2.appendChild(option23);
    StateSun2.appendChild(option24);
    StateSun2.appendChild(option25);
    StateSun2.appendChild(option26);
    StateSun2.appendChild(option27);
    StateSun2.appendChild(option28);
    StateSun2.appendChild(option29);
    StateSun2.appendChild(option30);
    StateSun2.appendChild(option31);
    StateSun2.appendChild(option32);
    StateSun2.appendChild(option33);
    StateSun2.appendChild(option34);

});
CKEDITOR.replace('Introduction');
CKEDITOR.replace('HostRules');
CKEDITOR.replace('Parking');
CKEDITOR.replace('ShootingEquipment');
CKEDITOR.replace('Traffic');
CKEDITOR.replace('OtherAmenity');

//刪除 時間option
//function removeOption() {
//    var deleteObj = document.getElementById("Timeoption");
//    deleteObj.classList.add("d-none");
//};

///上傳照片
FilePond.parse(document.body);
