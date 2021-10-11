        //停車場的radio 是
        let Yes=document.querySelector("#Yes");
        var parkingPart=document.querySelector("#parkingPart");
        Yes.addEventListener("click",function(){
           
           parkingPart.classList.remove('d-none');
        });
        //停車場的radio 否
        let No=document.querySelector('#No');
        No.addEventListener('click',function(){
           parkingPart.classList.add('d-none');
        })
        //攝影機 是
        let Yesprojection=document.querySelector("#Yesprojection");
        var projectionPart=document.querySelector('#projectionPart');
        Yesprojection.addEventListener('click',function(){
            projectionPart.classList.remove('d-none');
        });

        //攝影機 否
        let NOprojection=document.querySelector("#Noprojection");
        NOprojection.addEventListener('click',function(){
            projectionPart.classList.add('d-none');
        });

//        ///營業時間 radio///
//        var flag=1;
//        //monday
//        let flexSwitchCheckDefault= document.querySelectorAll("#flexSwitchCheckDefault")[0];
//        let flexRadioDefault1=document.querySelectorAll ('#flexRadioDefault1')[0];
//        let flexRadioDefault2=document.querySelectorAll ('#flexRadioDefault1')[1];

//        let StateMon=document.querySelectorAll("#StateMon")[0];
//        let StateMon2=document.querySelectorAll("#StateMon")[1];
//        let StateMon3=document.querySelectorAll("#StateMon")[2];
//        let StateMon4=document.querySelectorAll("#StateMon")[3];

//        flexSwitchCheckDefault.addEventListener("click",function(){
//           if(flag==1){
//            flexRadioDefault1.disabled=false;
//            flexRadioDefault2.disabled=false;
//            flag=0;
//           }else{
//            flexRadioDefault1.disabled=true;
//            flexRadioDefault2.disabled=true;
//            StateMon.disabled=true;
//            StateMon2.disabled=true;
//            StateMon3.disabled=true;
//            StateMon4.disabled=true;
//            flexRadioDefault2.checked=false;
//            flexRadioDefault1.checked=false;
//            flag=1;
//           }
//        });
//        flexRadioDefault1.addEventListener("click",function(){
//            StateMon.disabled=true;
//            StateMon2.disabled=true;
//            StateMon3.disabled=true;
//            StateMon4.disabled=true;
//        });

//        flexRadioDefault2.addEventListener('click',function () {
           
//            let StateMon=document.querySelectorAll("#StateMon")[0];
//            let StateMon2=document.querySelectorAll("#StateMon")[1];
//            let StateMon3=document.querySelectorAll("#StateMon")[2];
//            let StateMon4=document.querySelectorAll("#StateMon")[3];
//            StateMon.disabled=false;
//            StateMon2.disabled=false;
//            StateMon3.disabled=false;
//            StateMon4.disabled=false;
//        });


//        //tuesday
//        var flagtue=1;
//        let flexSwitchCheckDefaultTue=document.querySelectorAll("#flexSwitchCheckDefault")[1];
//        let TueAllDay=document.querySelectorAll ('#flexRadioDefault2')[0];
//        let Tuehour=document.querySelectorAll ('#flexRadioDefault2')[1];
//        flexSwitchCheckDefaultTue.addEventListener("click",function(){
//            if (flagtue==1) {
//                TueAllDay.disabled=false;
//                Tuehour.disabled=false;
//                flagtue=0;
//            }else{
//                TueAllDay.disabled=true;
//                Tuehour.disabled=true;
//                TueAllDay.checked=false;
//                Tuehour.checked=false;
//                let StateTue=document.querySelectorAll("#StateTue")[0];
//                let StateTue2=document.querySelectorAll("#StateTue")[1];
//                let StateTue3=document.querySelectorAll("#StateTue")[2];
//                let StateTue4=document.querySelectorAll("#StateTue")[3];
//                StateTue.disabled=true;
//                StateTue2.disabled=true;
//                StateTue3.disabled=true;
//                StateTue4.disabled=true;
//                flagtue=1;
//            }
//        });
//    TueAllDay.addEventListener('click',function(){
        
//         let StateTue=document.querySelectorAll("#StateTue")[0];
//                let StateTue2=document.querySelectorAll("#StateTue")[1];
//                let StateTue3=document.querySelectorAll("#StateTue")[2];
//                let StateTue4=document.querySelectorAll("#StateTue")[3];
//                StateTue.disabled=true;
//                StateTue2.disabled=true;
//                StateTue3.disabled=true;
//                StateTue4.disabled=true;

//    });
//    Tuehour.addEventListener('click',function(){
        
//        let StateTue=document.querySelectorAll("#StateTue")[0];
//        let StateTue2=document.querySelectorAll("#StateTue")[1];
//        let StateTue3=document.querySelectorAll("#StateTue")[2];
//        let StateTue4=document.querySelectorAll("#StateTue")[3];
//        StateTue.disabled=false;
//        StateTue2.disabled=false;
//        StateTue3.disabled=false;
//        StateTue4.disabled=false;
//    })
        
  



//       //wednesday
//       let flexSwitchCheckDefaultWed=document.querySelectorAll("#flexSwitchCheckDefault")[2];
//       let WedAllDay=document.querySelectorAll("#flexRadioDefault3")[0];
//       let Wedhour=document.querySelectorAll("#flexRadioDefault3")[1];
//       var flagWed=1;
//       flexSwitchCheckDefaultWed.addEventListener("click",function(){
//           if ((flagWed==1)) {
//            Wedhour.disabled=false;
//            WedAllDay.disabled=false;
//            flagWed=0;
//           }else{
//            Wedhour.disabled=true;
//            WedAllDay.disabled=true;
//            WedAllDay.checked=false;
//            Wedhour.checked=false;
//            let StateWed=document.querySelectorAll("#StateWed")[0];
//            let StateWed2=document.querySelectorAll("#StateWed")[1];
//            let StateWed3=document.querySelectorAll("#StateWed")[2];
//            let StateWed4 = document.querySelectorAll("#StateWed")[3];

//               //選擇時間
//               //
//            StateWed.disabled=true;
//            StateWed2.disabled=true;
//            StateWed3.document=true;
//            StateWed4.document=true;
//            flagWed=1;
//           }
//       });
//       WedAllDay.addEventListener('click',function(){
//            let StateWed=document.querySelectorAll("#StateWed")[0];
//            let StateWed2=document.querySelectorAll("#StateWed")[1];
//            let StateWed3=document.querySelectorAll("#StateWed")[2];
//            let StateWed4=document.querySelectorAll("#StateWed")[3];
//            StateWed.disabled=true;
//            StateWed2.disabled=true;
//            StateWed3.document=true;
//            StateWed4.document=true;
//       });
//         Wedhour.addEventListener('click',function(){
//            let StateWed=document.querySelectorAll("#StateWed")[0];
//            let StateWed2=document.querySelectorAll("#StateWed")[1];
//            let StateWed3=document.querySelectorAll("#StateWed")[2];
//            let StateWed4=document.querySelectorAll("#StateWed")[3];
//            StateWed.disabled=false;
//            StateWed2.disabled=false;
//            StateWed3.document=false;
//            StateWed4.document=false;
//       });
//       //thursday
//        let flexSwitchCheckDefaultThu=document.querySelectorAll("#flexSwitchCheckDefault")[3];
//        let ThuAllDay=document.querySelectorAll("#flexRadioDefault4")[0];
//        let Thuhour=document.querySelectorAll("#flexRadioDefault4")[1];
//        let StateThu=document.querySelectorAll("#StateThu")[0];
//        let StateThu2=document.querySelectorAll("#StateThu")[1];
//        let StateThu3=document.querySelectorAll("#StateThu")[2];
//        let StateThu4=document.querySelectorAll("#StateThu")[3];
//        var flagThu=1;  
//       flexSwitchCheckDefaultThu.addEventListener("click",function (){
//           if (flagThu==1) {
//            ThuAllDay.disabled=false;
//            Thuhour.disabled=false;
//            flagThu=0;
//           }else{
//                ThuAllDay.disabled=true;
//                Thuhour.disabled=true;
//                ThuAllDay.checked=false;
//                Thuhour.checked=false;
//                StateThu.disabled=true;
//                StateThu2.disabled=true;
//                StateThu3.disabled=true;
//                StateThu4.disabled=true;
//                flagThu=1; 
//           }
//       });
//       ThuAllDay.addEventListener('click',function(){
//            StateThu.disabled=true;
//            StateThu2.disabled=true;
//            StateThu3.disabled=true;
//            StateThu4.disabled=true;
//       })
//     Thuhour.addEventListener("click",function(){
//            StateThu.disabled=false;
//            StateThu2.disabled=false;
//            StateThu3.disabled=false;
//            StateThu4.disabled=false;
//     })

//       //friday
//       let flexSwitchCheckDefaultFri=document.querySelectorAll("#flexSwitchCheckDefault")[4];
//        let FridayAllDay=document.querySelectorAll("#flexRadioDefault5")[0];
//        let Fridayhour=document.querySelectorAll("#flexRadioDefault5")[1];
//        let StateFri=document.querySelectorAll("#StateFri")[0];
//        let StateFri2=document.querySelectorAll('#StateFri')[1];
//        let StateFri3=document.querySelectorAll("#StateFri")[2];
//        let StateFri4=document.querySelectorAll("#StateFri")[3];
//        flagFri=1;
//       flexSwitchCheckDefaultFri.addEventListener('click',function () {
//           if(flagFri==1){
//            FridayAllDay.disabled=false;
//            Fridayhour.disabled=false;
//            flagFri=0;
//           }else{
//                FridayAllDay.disabled=true;
//                Fridayhour.disabled=true;
//                StateFri.disabled=true;
//                StateFri2.disabled=true;
//                StateFri3.disabled=true;
//                StateFri4.disabled=true;
//                FridayAllDay.checked=false;
//                Fridayhour.checked=false;
//                flagFri=1;
//           }
//       });
//       FridayAllDay.addEventListener("click",function(){
//                StateFri.disabled=true;
//                StateFri2.disabled=true;
//                StateFri3.disabled=true;
//                StateFri4.disabled=true;
//       });
//       Fridayhour.addEventListener("click",function(){
//                StateFri.disabled=false;
//                StateFri2.disabled=false;
//                StateFri3.disabled=false;
//                StateFri4.disabled=false;
//       })
//2
//       //saturday
//       let flexSwitchCheckDefaultSat=document.querySelectorAll("#flexSwitchCheckDefault")[5];
//       let SatAllday=document.querySelectorAll("#flexRadioDefault6")[0];
//       let Sathour=document.querySelectorAll('#flexRadioDefault6')[1];
//       let StateSat=document.querySelectorAll("#StateSat")[0];
//       let StateSat2=document.querySelectorAll("#StateSat")[1];
//       let StateSat3=document.querySelectorAll("#StateSat")[2];
//       let StateSat4=document.querySelectorAll("#StateSat")[3];
//       var flagSat=1;
//       flexSwitchCheckDefaultSat.addEventListener('click',function () {
//           if ((flagSat==1)) {
//               SatAllday.disabled=false;
//               Sathour.disabled=false;
//               flagSat=0;
//           }else{
//                SatAllday.disabled=true;
//                Sathour.disabled=true;
//                SatAllday.checked=false;
//                Sathour.checked=false;
//                StateSat.disabled=true;
//                StateSat2.disabled=true;
//                StateSat3.disabled=true;
//                StateSat4.disabled=true;
//                flagSat=1;
//           };
//       });
//       SatAllday.addEventListener("click",function(){
//                StateSat.disabled=true;
//                StateSat2.disabled=true;
//                StateSat3.disabled=true;
//                StateSat4.disabled=true;
//       });
//       Sathour.addEventListener('click',function(){
//                StateSat.disabled=false;
//                StateSat2.disabled=false;
//                StateSat3.disabled=false;
//                StateSat4.disabled=false;
//       });

//        //sunday 
//        let flexSwitchCheckDefaultSun=document.querySelectorAll("#flexSwitchCheckDefault")[6];
//        let SunAllDay=document.querySelectorAll("#flexRadioDefault7")[0];
//        let Sunhour=document.querySelectorAll('#flexRadioDefault7')[1];
//        let StateSun=document.querySelectorAll('#StateSun')[0];
//        let StateSun2=document.querySelectorAll("#StateSun")[1];
//        let StateSun3=document.querySelectorAll("#StateSun")[2];
//        let StateSun4=document.querySelectorAll("#StateSun")[3];
//        var flagSun=1;
//        flexSwitchCheckDefaultSun.addEventListener("click",function () {
//            if(flagSun==1){
//            SunAllDay.disabled=false;
//            Sunhour.disabled=false;
//            flagSun=0;
//            }else{
//                SunAllDay.disabled=true;
//                Sunhour.disabled=true;
//                SunAllDay.checked=false;
//                Sunhour.checked=false;
//                StateSun.disabled=true;
//                StateSun2.disabled=true;
//                StateSun3.disabled=true;
//                StateSat4.disabled=true;
//                flagSun=0;
//            }
//        });
//               SunAllDay.addEventListener("click",function(){
//                StateSun.disabled=true;
//                StateSun2.disabled=true;
//                StateSun3.disabled=true;
//                StateSat4.disabled=true;
//       });
//       Sunhour.addEventListener('click',function(){
//                StateSun.disabled=false;
//                StateSun2.disabled=false;
//                StateSun3.disabled=false;
//                StateSun4.disabled=false;
//       });

///營業時間 radio///
var flag =0;
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
        flag =0;
    } else {
        flexRadioDefault1.disabled = true;
        flexRadioDefault2.disabled = true;
        StateMon.disabled = true;
        StateMon2.disabled = true;
        flexRadioDefault2.checked = false;
        flexRadioDefault1.checked = false;
        flag =1;
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
var flagtue;
let flexSwitchCheckDefaultTue = document.querySelectorAll("#flexSwitchCheckDefault")[1];
let TueAllDay = document.querySelectorAll('#flexRadioDefault1')[2];
let Tuehour = document.querySelectorAll('#flexRadioDefault1')[3];
flexSwitchCheckDefaultTue.addEventListener("click", function () {
    if (flagtue == 0) {
        TueAllDay.disabled = false;
        Tuehour.disabled = false;
        flagtue = 1;
    } else {
        TueAllDay.disabled = true;
        Tuehour.disabled = true;
        TueAllDay.checked = false;
        Tuehour.checked = false;
        let StateTue = document.querySelectorAll("#State")[2];
        let StateTue2 = document.querySelectorAll("#State")[3];
        StateTue.disabled = true;
        StateTue2.disabled = true;
        flagtue =0;
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
var flagWed;
flexSwitchCheckDefaultWed.addEventListener("click", function () {
    if ((flagWed ==0)) {
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
var flagThu;
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
var flagFri;
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
var flagSat;
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
var flagSun;
flexSwitchCheckDefaultSun.addEventListener("click", function () {
    if (flagSun ==0) {
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

CKEDITOR.replace('textPartType');
CKEDITOR.replace('textPartRule');
CKEDITOR.replace('textPartPark');
CKEDITOR.replace('textPartProjection');
CKEDITOR.replace('textPartClean');
CKEDITOR.replace('textPartTransportation');


///上傳照片
FilePond.parse(document.body);