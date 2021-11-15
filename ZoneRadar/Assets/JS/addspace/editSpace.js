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


//datlist
//datlist2


//monday~Sun
//營業 checkbox
let MonOpen = document.querySelector("#MonOpen");
let TueOpen = document.querySelector("#TueOpen");
let WedOpen = document.querySelector("#WedOpen");
let ThuOpen = document.querySelector("#ThuOpen");
let FriOpen = document.querySelector("#FriOpen");
let SatOpen = document.querySelector("#SatOpen");
let SunOpen = document.querySelector("#SunOpen");

//全天 monday~Sun radio
let MonAllDay = document.querySelector("#MonAllDay");
let TueAllDay = document.querySelector("#TueAllDay");
let WedAllDay = document.querySelector("#WedAllDay");
let ThuAllDay = document.querySelector("#ThuAllDay");
let FriAllDay = document.querySelector("#FriAllDay");
let SatAllDay = document.querySelector("#SatAllDay");
let SunAllDay = document.querySelector("#SunAllDay");

//小時 monday~Sun radio
let MonHours = document.querySelector("#MonHours");
let TueHours = document.querySelector("#TueHours");
let WedHours = document.querySelector("#WedHours");
let ThuHours = document.querySelector("#ThuHours");
let FriHours = document.querySelector("#FriHours");
let SatHours = document.querySelector("#SatHours");
let SunHours = document.querySelector("#SunHours");

//選擇營業時間的 下拉選單開始 結束 monday~Sun Select
let StateMonS = document.querySelector("#StateMonS");
let StateMonE = document.querySelector("#StateMonE");

let StateTueS = document.querySelector("#StateTueS");
let StateTueE = document.querySelector("#StateTueE");

let StateWedS = document.querySelector("#StateWedS");
let StateWedE = document.querySelector("#StateWedE");

let StateThuS = document.querySelector("#StateThuS");
let StateThuE = document.querySelector("#StateThuE");

let StateFriS = document.querySelector("#StateFriS");
let StateFriE = document.querySelector("#StateFriE");

let StateSatS = document.querySelector("#StateSatS");
let StateSatE = document.querySelector("#StateSatE");

let StateSunS = document.querySelector("#StateSunS");
let StateSunE = document.querySelector("#StateSunE");

//選擇營業時間的一開始顯示資料庫的 要修改的時候變成時間 下拉選單開始 結束 monday~Sun Select
let StartTimeoptionMon = document.querySelector("#StartTimeoptionMon");
let EndTimeoptionMon = document.querySelector("#EndTimeoptionMon");

let StartTimeoptionTue = document.querySelector("#StartTimeoptionTue");
let EndTimeoptionTue = document.querySelector("#EndTimeoptionTue");

let StarTimeoptionWed = document.querySelector("#StarTimeoptionWed");
let EndTimeoptionWed = document.querySelector("#EndTimeoptionWed");

let StartTimeoptionThu = document.querySelector("#StartTimeoptionThu");
let EndTimeoptionThu = document.querySelector("#EndTimeoptionThu");

let StartTimeoptionFri = document.querySelector("#StartTimeoptionFri");
let EndTimeoptionFri = document.querySelector("#EndTimeoptionFri");

let StartTimeoptionSat = document.querySelector("#StartTimeoptionSat");
let EndTimeoptionSat = document.querySelector("#EndTimeoptionSat");

let StartTimeoptionSun = document.querySelector("#StartTimeoptionSun");
let EndTimeoptionSun = document.querySelector("#EndTimeoptionSun");

console.log(datlist[0]);
let Newdatalist = new Array();
let compare=[1,2,3,4,5,6,7]
for (var i = 0; i < datlist.length; i++) {
    Newdatalist.push(datlist[i].OperatingDay);
}

for (var i = 0; i < Newdatalist.length; i++) {
    switch (Newdatalist[i]) {
        case 1:
            console.log(Newdatalist[i])
            MonOpen.checked = true;
            MonAllDay.disabled = false;
            MonHours.disabled = false;
            if (datlist[i].StartTime.Hours == 6 && datlist[i].EndTime.Hours == 23) {
                MonAllDay.setAttribute('checked', "checked");
                MonAllDay.checked = true;

                StartTimeoptionMon.innerHTML = "06:00";
                StartTimeoptionMon.value = "06:00";
                EndTimeoptionMon.innerHTML = "23:00";
                EndTimeoptionMon.value = "23:00";
            }
            else {
                MonHours.checked = true;
                var ShourString = (datlist[i].StartTime.Hours).toString();
                var EhourString = (datlist[i].EndTime.Hours).toString();
                StartTimeoptionMon.innerHTML = ShourString.padStart(2, '0') + ":00";
                StartTimeoptionMon.value= ShourString.padStart(2, '0') + ":00";
                EndTimeoptionMon.innerHTML = EhourString.padStart(2, '0') + ":00";
                EndTimeoptionMon.value = EhourString.padStart(2, '0') + ":00";
            }
            break;
        case 2:
            TueOpen.checked = true;
            TueAllDay.disabled = false;
            TueHours.disabled = false;
            if (datlist[i].StartTime.Hours == 6 && datlist[i].EndTime.Hours == 23) {
                TueAllDay.setAttribute('checked', "checked");
                TueAllDay.checked = true;

                StartTimeoptionTue.innerHTML = "06:00";
                StartTimeoptionTue.value = "06:00";
                EndTimeoptionTue.innerHTML = "23:00";
                EndTimeoptionTue.value = "23:00";
            }
            else {
                TueHours.checked = true;
                var ShourString = (datlist[i].StartTime.Hours).toString();
                var EhourString = (datlist[i].EndTime.Hours).toString();

                StartTimeoptionTue.innerHTML = ShourString.padStart(2, '0') + ":00";
                StartTimeoptionTue.value = ShourString.padStart(2, '0') + ":00";
                EndTimeoptionTue.innerHTML = EhourString.padStart(2, '0') + ":00";
                EndTimeoptionTue.value = EhourString.padStart(2, '0') + ":00";
            }

            break;

        case 3:
            WedOpen.checked = true;
            WedAllDay.disabled = false;
            WedHours.disabled = false;
            if (datlist[i].StartTime.Hours == 6 && datlist[i].EndTime.Hours == 23) {
                WedAllDay.setAttribute('checked', "checked");
                WedAllDay.checked = true;

                StarTimeoptionWed.innerHTML = "06:00";
                StarTimeoptionWed.value = "06:00";
                EndTimeoptionWed.innerHTML = "23:00";
                EndTimeoptionWed.value = "23:00";
            }
            else {
                WedHours.checked = true;
                var ShourString = (datlist[i].StartTime.Hours).toString();
                var EhourString = (datlist[i].EndTime.Hours).toString();

                StarTimeoptionWed.innerHTML = ShourString.padStart(2, '0') + ":00";
                StarTimeoptionWed.value = ShourString.padStart(2, '0') + ":00";
                EndTimeoptionWed.innerHTML = EhourString.padStart(2, '0') + ":00";
                EndTimeoptionWed.value = EhourString.padStart(2, '0') + ":00";
            }

            break;
        case 4:
            ThuOpen.checked = true;
            ThuAllDay.disabled = false;
            ThuHours.disabled = false;
            if (datlist[i].StartTime.Hours == 6 && datlist[i].EndTime.Hours == 23) {
                ThuAllDay.setAttribute('checked', "checked");
                ThuAllDay.checked = true;
                StartTimeoptionThu.innerHTML = "06:00";
                StartTimeoptionThu.value = "06:00";
                EndTimeoptionThu.innerHTML = "23:00";
                EndTimeoptionThu.value = "23:00";
            }
            else {
                ThuHours.checked = true;
                var ShourString = (datlist[i].StartTime.Hours).toString();
                var EhourString = (datlist[i].EndTime.Hours).toString();

                StartTimeoptionThu.innerHTML = ShourString.padStart(2, '0') + ":00";
                StartTimeoptionThu.value = ShourString.padStart(2, '0') + ":00";
                EndTimeoptionThu.innerHTML = EhourString.padStart(2, '0') + ":00";
                EndTimeoptionThu.value = EhourString.padStart(2, '0') + ":00";
            }
            break;
        case 5:
            FriOpen.checked = true;
            FriAllDay.disabled = false;
            FriHours.disabled = false;
            if (datlist[i].StartTime.Hours == 6 && datlist[i].EndTime.Hours == 23) {
                FriAllDay.setAttribute('checked', "checked");
                FriAllDay.checked = true;
                StartTimeoptionFri.innerHTML = "06:00";
                StartTimeoptionFri.value = "06:00";
                EndTimeoptionFri.innerHTML = "23:00";
                EndTimeoptionFri.value = "23:00";
            }
            else {
                FriHours.checked = true;
                var ShourString = (datlist[i].StartTime.Hours).toString();
                var EhourString = (datlist[i].EndTime.Hours).toString();

                StartTimeoptionFri.innerHTML = ShourString.padStart(2, '0') + ":00";
                EndTimeoptionFri.innerHTML = EhourString.padStart(2, '0') + ":00";
                StartTimeoptionFri.value = ShourString.padStart(2, '0') + ":00";
                EndTimeoptionFri.value = EhourString.padStart(2, '0') + ":00";

            }
            break;
        case 6:
            SatOpen.checked = true;
            SatAllDay.disabled = false;
            SatHours.disabled = false;
            if (datlist[i].StartTime.Hours == 6 && datlist[i].EndTime.Hours == 23) {
                SatAllDay.setAttribute('checked', "checked");
                SatAllDay.checked = true;
                StartTimeoptionSat.innerHTML = "06:00";
                StartTimeoptionSat.value = "06:00";
                EndTimeoptionSat.innerHTML = "23:00";
                EndTimeoptionSat.value = "23:00";
            }
            else {
                SatHours.checked = true;
                var ShourString = (datlist[i].StartTime.Hours).toString();
                var EhourString = (datlist[i].EndTime.Hours).toString();

                StartTimeoptionSat.innerHTML = ShourString.padStart(2, '0') + ":00";
                StartTimeoptionSat.value = ShourString.padStart(2, '0') + ":00";
                EndTimeoptionSat.innerHTML = EhourString.padStart(2, '0') + ":00";
                EndTimeoptionSat.value = ShourString.padStart(2, '0') + ":00";
            }
            break;
        case 7:
            SunOpen.checked = true;
            SunAllDay.disabled = false;
            SunHours.disabled = false;
            if (datlist[i].StartTime.Hours == 6 && datlist[i].EndTime.Hours == 23) {
                SunAllDay.setAttribute('checked', "checked");
                SunAllDay.checked = true;
                StartTimeoptionSun.innerHTML = "06:00";
                StartTimeoptionSun.value = "06:00";
                EndTimeoptionSun.innerHTML = "23:00";
                EndTimeoptionSun.value = "23:00";

            }
            else {
                SunHours.checked = true;
                var ShourString = (datlist[i].StartTime.Hours).toString();
                var EhourString = (datlist[i].EndTime.Hours).toString();

                StartTimeoptionSun.innerHTML = ShourString.padStart(2, '0') + ":00";
                StartTimeoptionSun.value = ShourString.padStart(2, '0') + ":00";
                EndTimeoptionSun.innerHTML = EhourString.padStart(2, '0') + ":00";
                EndTimeoptionSun.value = EhourString.padStart(2, '0') + ":00";
            }
            break;
        default:
            break;

    }
   }

    //todo

    MonOpen.addEventListener("click", function () {
        if (MonOpen.checked == false) {
            MonAllDay.disabled = true;
            MonHours.disabled = true;
            StartTimeoptionMon.innerHTML = "選擇營業時間";
            EndTimeoptionMon.innerHTML = "選擇營業時間";

            MonAllDay.checked = false;
            MonHours.checked = false;
            StateMonS.disabled = true;
            StateMonE.disabled = true;
            console.log("關")
            flagMon = 0;
            console.log("打開")
        }
        else if (MonOpen.checked == true) {
            MonAllDay.disabled = false;
            MonHours.disabled = false;
            StateMonS.disabled = true;
            StateMonE.disabled = true;
            flagMon = 0;
        }
    });
    MonAllDay.addEventListener("click", function () {
        StartTimeoptionMon.innerHTML = "06:00";
        EndTimeoptionMon.innerHTML = "23:00";
        StateMonS.disabled = true;
        StateMonE.disabled = true;
        StateMonS.length = 1;
        StateMonE.length = 1;
    });
    MonHours.addEventListener('click', function () {
        StateMonS.disabled = false;
        StateMonE.disabled = false;
        StartTimeoptionMon.innerHTML = "06:00";
        EndTimeoptionMon.innerHTML = "23:00";
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

        StateMonS.appendChild(option1);
        StateMonS.appendChild(option2);
        StateMonS.appendChild(option3);
        StateMonS.appendChild(option4);
        StateMonS.appendChild(option5);
        StateMonS.appendChild(option6);
        StateMonS.appendChild(option7);
        StateMonS.appendChild(option8);
        StateMonS.appendChild(option9);
        StateMonS.appendChild(option10);
        StateMonS.appendChild(option11);
        StateMonS.appendChild(option12);
        StateMonS.appendChild(option13);
        StateMonS.appendChild(option14);
        StateMonS.appendChild(option15);
        StateMonS.appendChild(option16);
        StateMonS.appendChild(option17);

        StateMonE.appendChild(option18);
        StateMonE.appendChild(option19);
        StateMonE.appendChild(option20);
        StateMonE.appendChild(option21);
        StateMonE.appendChild(option22);
        StateMonE.appendChild(option23);
        StateMonE.appendChild(option24);
        StateMonE.appendChild(option25);
        StateMonE.appendChild(option26);
        StateMonE.appendChild(option27);
        StateMonE.appendChild(option28);
        StateMonE.appendChild(option29);
        StateMonE.appendChild(option30);
        StateMonE.appendChild(option31);
        StateMonE.appendChild(option32);
        StateMonE.appendChild(option33);
        StateMonE.appendChild(option34);

    });
    TueOpen.addEventListener("click", function () {
        if (TueOpen.checked == false) {
            TueAllDay.disabled = true;
            TueHours.disabled = true;
            EndTimeoptionTue.innerHTML = "選擇營業時間";
            StartTimeoptionTue.innerHTML = "選擇營業時間";
            TueAllDay.checked = false;
            TueHours.checked = false;
            StateTueS.disabled = true;
            StateTueE.disabled = true;
        }
        else if (TueOpen.checked == true) {
            flagTue = 1;
            TueAllDay.disabled = false;
            TueHours.disabled = false;
            StateTueS.disabled = true;
            StateTueE.disabled = true;
            flagTue = 0;
        }

    });
    TueAllDay.addEventListener("click", function () {
        StartTimeoptionTue.innerHTML = "06:00";
        EndTimeoptionTue.innerHTML = "23:00";
        StateTueS.length = 1;
        StateTueE.length = 1;
        StateTueS.disabled = true;
        StateTueE.disabled = true;
    });
    TueHours.addEventListener('click', function () {
        StateTueS.disabled = false;
        StateTueE.disabled = false;
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

        StateTueS.appendChild(option1);
        StateTueS.appendChild(option2);
        StateTueS.appendChild(option3);
        StateTueS.appendChild(option4);
        StateTueS.appendChild(option5);
        StateTueS.appendChild(option6);
        StateTueS.appendChild(option7);
        StateTueS.appendChild(option8);
        StateTueS.appendChild(option9);
        StateTueS.appendChild(option10);
        StateTueS.appendChild(option11);
        StateTueS.appendChild(option12);
        StateTueS.appendChild(option13);
        StateTueS.appendChild(option14);
        StateTueS.appendChild(option15);
        StateTueS.appendChild(option16);
        StateTueS.appendChild(option17);

        StateTueE.appendChild(option18);
        StateTueE.appendChild(option19);
        StateTueE.appendChild(option20);
        StateTueE.appendChild(option21);
        StateTueE.appendChild(option22);
        StateTueE.appendChild(option23);
        StateTueE.appendChild(option24);
        StateTueE.appendChild(option25);
        StateTueE.appendChild(option26);
        StateTueE.appendChild(option27);
        StateTueE.appendChild(option28);
        StateTueE.appendChild(option29);
        StateTueE.appendChild(option30);
        StateTueE.appendChild(option31);
        StateTueE.appendChild(option32);
        StateTueE.appendChild(option33);
        StateTueE.appendChild(option34);

    });
    WedOpen.addEventListener("click", function () {
        if (WedOpen.checked == false) {
            WedAllDay.disabled = true;
            WedHours.disabled = true;
            EndTimeoptionWed.innerHTML = "選擇營業時間";
            StarTimeoptionWed.innerHTML = "選擇營業時間";
            WedAllDay.checked = false;
            WedHours.checked = false;
            StateWedS.disabled = true;
            StateWedE.disabled = true;
        }
        else if (WedOpen.checked == true) {
            WedAllDay.disabled = false;
            WedHours.disabled = false;
            StateWedS.disabled = true;
            StateWedE.disabled = true;
        }
    });
    WedAllDay.addEventListener("click", function () {
        StarTimeoptionWed.innerHTML = "06:00";
        EndTimeoptionWed.innerHTML = "23:00";
        StateWedS.length = 1;
        StateWedE.length = 1;
        StateWedS.disabled = true;
        StateWedE.disabled = true;
    });
    WedHours.addEventListener('click', function () {
        StateWedS.disabled = false;
        StateWedE.disabled = false;
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

        StateWedS.appendChild(option1);
        StateWedS.appendChild(option2);
        StateWedS.appendChild(option3);
        StateWedS.appendChild(option4);
        StateWedS.appendChild(option5);
        StateWedS.appendChild(option6);
        StateWedS.appendChild(option7);
        StateWedS.appendChild(option8);
        StateWedS.appendChild(option9);
        StateWedS.appendChild(option10);
        StateWedS.appendChild(option11);
        StateWedS.appendChild(option12);
        StateWedS.appendChild(option13);
        StateWedS.appendChild(option14);
        StateWedS.appendChild(option15);
        StateWedS.appendChild(option16);
        StateWedS.appendChild(option17);

        StateWedE.appendChild(option18);
        StateWedE.appendChild(option19);
        StateWedE.appendChild(option20);
        StateWedE.appendChild(option21);
        StateWedE.appendChild(option22);
        StateWedE.appendChild(option23);
        StateWedE.appendChild(option24);
        StateWedE.appendChild(option25);
        StateWedE.appendChild(option26);
        StateWedE.appendChild(option27);
        StateWedE.appendChild(option28);
        StateWedE.appendChild(option29);
        StateWedE.appendChild(option30);
        StateWedE.appendChild(option31);
        StateWedE.appendChild(option32);
        StateWedE.appendChild(option33);
        StateWedE.appendChild(option34);

    });


    ///
    ThuOpen.addEventListener("click", function () {
        if (ThuOpen.checked == false) {

            ThuAllDay.disabled = true;
            ThuHours.disabled = true;
            EndTimeoptionThu.innerHTML = "選擇營業時間";
            StartTimeoptionThu.innerHTML = "選擇營業時間";
            ThuAllDay.checked = false;
            ThuHours.checked = false;
            StateThuS.disabled = true;
            StateThuE.disabled = true;
        }
        else if (ThuOpen.checked == true) {
            ThuAllDay.disabled = false;
            ThuHours.disabled = false;
            StateThuS.disabled = true;
            StateThuE.disabled = true;
        }
    });
    ThuAllDay.addEventListener("click", function () {
        StartTimeoptionThu.innerHTML = "06:00";
        EndTimeoptionThu.innerHTML = "23:00";
        StateThuS.length = 1;
        StateThuE.length = 1;
        StateThuS.disabled = true;
        StateThuE.disabled = true;
    });
    ThuHours.addEventListener('click', function () {
        StateThuS.disabled = false;
        StateThuE.disabled = false;
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

        StateThuS.appendChild(option1);
        StateThuS.appendChild(option2);
        StateThuS.appendChild(option3);
        StateThuS.appendChild(option4);
        StateThuS.appendChild(option5);
        StateThuS.appendChild(option6);
        StateThuS.appendChild(option7);
        StateThuS.appendChild(option8);
        StateThuS.appendChild(option9);
        StateThuS.appendChild(option10);
        StateThuS.appendChild(option11);
        StateThuS.appendChild(option12);
        StateThuS.appendChild(option13);
        StateThuS.appendChild(option14);
        StateThuS.appendChild(option15);
        StateThuS.appendChild(option16);
        StateThuS.appendChild(option17);

        StateThuE.appendChild(option18);
        StateThuE.appendChild(option19);
        StateThuE.appendChild(option20);
        StateThuE.appendChild(option21);
        StateThuE.appendChild(option22);
        StateThuE.appendChild(option23);
        StateThuE.appendChild(option24);
        StateThuE.appendChild(option25);
        StateThuE.appendChild(option26);
        StateThuE.appendChild(option27);
        StateThuE.appendChild(option28);
        StateThuE.appendChild(option29);
        StateThuE.appendChild(option30);
        StateThuE.appendChild(option31);
        StateThuE.appendChild(option32);
        StateThuE.appendChild(option33);
        StateThuE.appendChild(option34);

    });
    ///5
    FriOpen.addEventListener("click", function () {
        if (FriOpen.checked == false) {

            FriAllDay.disabled = true;
            FriHours.disabled = true;
            EndTimeoptionFri.innerHTML = "選擇營業時間";
            StartTimeoptionFri.innerHTML = "選擇營業時間";
            FriAllDay.checked = false;
            FriHours.checked = false;
            StateThuS.disabled = true;
            StateThuE.disabled = true;
        }
        else if (FriOpen.checked == true) {
            FriAllDay.disabled = false;
            FriHours.disabled = false;
            StateFriS.disabled = true;
            StateFriE.disabled = true;
        }
    });
    FriAllDay.addEventListener("click", function () {
        StartTimeoptionFri.innerHTML = "06:00";
        EndTimeoptionFri.innerHTML = "23:00";
        StateFriS.length = 1;
        StateFriE.length = 1;
        StateFriS.disabled = true;
        StateFriE.disabled = true;
    });
    FriHours.addEventListener('click', function () {
        StateFriS.disabled = false;
        StateFriE.disabled = false;
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

        StateFriS.appendChild(option1);
        StateFriS.appendChild(option2);
        StateFriS.appendChild(option3);
        StateFriS.appendChild(option4);
        StateFriS.appendChild(option5);
        StateFriS.appendChild(option6);
        StateFriS.appendChild(option8);
        StateFriS.appendChild(option9);
        StateFriS.appendChild(option10);
        StateFriS.appendChild(option11);
        StateFriS.appendChild(option12);
        StateFriS.appendChild(option13);
        StateFriS.appendChild(option14);
        StateFriS.appendChild(option15);
        StateFriS.appendChild(option16);
        StateFriS.appendChild(option17);

        StateFriE.appendChild(option18);
        StateFriE.appendChild(option19);
        StateFriE.appendChild(option20);
        StateFriE.appendChild(option21);
        StateFriE.appendChild(option22);
        StateFriE.appendChild(option23);
        StateFriE.appendChild(option24);
        StateFriE.appendChild(option25);
        StateFriE.appendChild(option26);
        StateFriE.appendChild(option27);
        StateFriE.appendChild(option28);
        StateFriE.appendChild(option29);
        StateFriE.appendChild(option30);
        StateFriE.appendChild(option31);
        StateFriE.appendChild(option32);
        StateFriE.appendChild(option33);
        StateFriE.appendChild(option34);
    });
    ///6
    SatOpen.addEventListener("click", function () {
        if (SatOpen.checked == false) {
            SatAllDay.disabled = true;
            SatHours.disabled = true;
            EndTimeoptionSat.innerHTML = "選擇營業時間";
            StartTimeoptionSat.innerHTML = "選擇營業時間";
            SatAllDay.checked = false;
            SatHours.checked = false;
            StateSatS.disabled = true;
            StateSatE.disabled = true;
        }
        else if (SatOpen.checked == true) {
            SatAllDay.disabled = false;
            SatHours.disabled = false;
            StateSatS.disabled = true;
            StateSatE.disabled = true;
        }
    });
    SatAllDay.addEventListener("click", function () {
        StartTimeoptionSat.innerHTML = "06:00";
        EndTimeoptionSat.innerHTML = "23:00";
        StateSatS.length = 1;
        StateSatE.length = 1;
        StateSatS.disabled = true;
        StateSatE.disabled = true;
    });
    SatHours.addEventListener('click', function () {
        StateSatS.disabled = false;
        StateSatE.disabled = false;
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

        StateSatS.appendChild(option1);
        StateSatS.appendChild(option2);
        StateSatS.appendChild(option3);
        StateSatS.appendChild(option4);
        StateSatS.appendChild(option5);
        StateSatS.appendChild(option6);
        StateSatS.appendChild(option8);
        StateSatS.appendChild(option9);
        StateSatS.appendChild(option10);
        StateSatS.appendChild(option11);
        StateSatS.appendChild(option12);
        StateSatS.appendChild(option13);
        StateSatS.appendChild(option14);
        StateSatS.appendChild(option15);
        StateSatS.appendChild(option16);
        StateSatS.appendChild(option17);

        StateSatE.appendChild(option18);
        StateSatE.appendChild(option19);
        StateSatE.appendChild(option20);
        StateSatE.appendChild(option21);
        StateSatE.appendChild(option22);
        StateSatE.appendChild(option23);
        StateSatE.appendChild(option24);
        StateSatE.appendChild(option25);
        StateSatE.appendChild(option26);
        StateSatE.appendChild(option27);
        StateSatE.appendChild(option28);
        StateSatE.appendChild(option29);
        StateSatE.appendChild(option30);
        StateSatE.appendChild(option31);
        StateSatE.appendChild(option32);
        StateSatE.appendChild(option33);
        StateSatE.appendChild(option34);
    });

    //Sun
    SunOpen.addEventListener("click", function () {
        if (SunOpen.checked == false) {
            SunAllDay.disabled = true;
            SunHours.disabled = true;
            EndTimeoptionSun.innerHTML = "選擇營業時間";
            StartTimeoptionSun.innerHTML = "選擇營業時間";
            SunAllDay.checked = false;
            SunHours.checked = false;
            StateSunS.disabled = true;
            StateSunE.disabled = true;
        }
        else if (SunOpen.checked == true) {
            SunAllDay.disabled = false;
            SunHours.disabled = false;
            StateSunS.disabled = true;
            StateSunE.disabled = true;
        }
    });
    SunAllDay.addEventListener("click", function () {
        StartTimeoptionSun.innerHTML = "06:00";
        EndTimeoptionSun.innerHTML = "23:00";
        StateSunS.length = 1;
        StateSunE.length = 1;
        StateSunS.disabled = true;
        StateSunE.disabled = true;
    });
    SunHours.addEventListener('click', function () {
        StateSunS.disabled = false;
        StateSunE.disabled = false;
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

        StateSunS.appendChild(option1);
        StateSunS.appendChild(option2);
        StateSunS.appendChild(option3);
        StateSunS.appendChild(option4);
        StateSunS.appendChild(option5);
        StateSunS.appendChild(option6);
        StateSunS.appendChild(option8);
        StateSunS.appendChild(option9);
        StateSunS.appendChild(option10);
        StateSunS.appendChild(option11);
        StateSunS.appendChild(option12);
        StateSunS.appendChild(option13);
        StateSunS.appendChild(option14);
        StateSunS.appendChild(option15);
        StateSunS.appendChild(option16);
        StateSunS.appendChild(option17);

        StateSunE.appendChild(option18);
        StateSunE.appendChild(option19);
        StateSunE.appendChild(option20);
        StateSunE.appendChild(option21);
        StateSunE.appendChild(option22);
        StateSunE.appendChild(option23);
        StateSunE.appendChild(option24);
        StateSunE.appendChild(option25);
        StateSunE.appendChild(option26);
        StateSunE.appendChild(option27);
        StateSunE.appendChild(option28);
        StateSunE.appendChild(option29);
        StateSunE.appendChild(option30);
        StateSunE.appendChild(option31);
        StateSunE.appendChild(option32);
        StateSunE.appendChild(option33);
        StateSunE.appendChild(option34);
    });

    //CKEDITOR.replace('Introduction');
    //CKEDITOR.replace('HostRules');
    //CKEDITOR.replace('Parking');
    //CKEDITOR.replace('ShootingEquipment');
    //CKEDITOR.replace('Traffic');
    CKEDITOR.replace('OtherAmenity');

