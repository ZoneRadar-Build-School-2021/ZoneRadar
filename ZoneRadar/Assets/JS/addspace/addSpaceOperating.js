        //營業時間 radio///
        var flag=1;
        //monday
        let flexSwitchCheckDefault = document.querySelector("#flexSwitchCheckDefault");
        let flexRadioDefault1=document.querySelector('#flexRadioDefault1');
        let flexRadioDefault2 = document.querySelector('#flexRadioDefault1hr');

        let StateMon=document.querySelectorAll("#StateMon")[0];
        let StateMon2=document.querySelectorAll("#StateMon")[1];
        let StateMon3=document.querySelectorAll("#StateMon")[2];
        let StateMon4=document.querySelectorAll("#StateMon")[3];

       

        
        flexSwitchCheckDefault.addEventListener("click",function(){
           if(flag==1){
            flexRadioDefault1.disabled=false;
            flexRadioDefault2.disabled=false;
            flag=0;
           }else{
            flexRadioDefault1.disabled=true;
            flexRadioDefault2.disabled=true;
            StateMon.disabled=true;
            StateMon2.disabled=true;
            StateMon3.disabled=true;
            StateMon4.disabled=true;
            flexRadioDefault2.checked=false;
            flexRadioDefault1.checked=false;
            flag=1;
           }
        });
        flexRadioDefault1.addEventListener("click",function(){
            StateMon.disabled = false;
            StateMon2.disabled = false;
            StateMon3.disabled = false;
            StateMon4.disabled = false;
        //    var starttime= document.getElementById("Mon");
        //    starttime.removeAttribute('class');
        //    var MonS = document.getElementById("MonS");
        //    MonS.innerHTML = "06:00";
        //    MonS.setAttribute("value", "06:00");
        //    var MonEnd = document.getElementById("MonEnd");
        //    MonEnd.innerHTML = "23:00";
        //    MonEnd.setAttribute("value", "23:00");
        });

        flexRadioDefault2.addEventListener('click',function () {
            //var MonS = document.getElementById("MonS");
            //MonS.innerHTML = "請選擇營業時間";
            
            //var MonEnd = document.getElementById("MonEnd");
            //MonEnd.innerHTML = "請選擇營業時間";
           
            let StateMon=document.querySelectorAll("#StateMon")[0];
            let StateMon2=document.querySelectorAll("#StateMon")[1];
            let StateMon3=document.querySelectorAll("#StateMon")[2];
            let StateMon4=document.querySelectorAll("#StateMon")[3];
            StateMon.disabled=false;
            StateMon2.disabled=false;
            StateMon3.disabled=false;
            StateMon4.disabled=false;
        });


        //tuesday
        var flagtue=1;
        let flexSwitchCheckDefaultTue=document.querySelector("#flexSwitchCheckDefault2");
        let TueAllDay=document.querySelector('#flexRadioDefault2');
        let Tuehour = document.querySelector('#flexRadioDefault2hr');
        flexSwitchCheckDefaultTue.addEventListener("click",function(){
            if (flagtue==1) {
                TueAllDay.disabled=false;
                Tuehour.disabled=false;
                flagtue=0;
            }else{
                TueAllDay.disabled=true;
                Tuehour.disabled=true;
                TueAllDay.checked=false;
                Tuehour.checked=false;
                let StateTue=document.querySelectorAll("#StateTue")[0];
                let StateTue2=document.querySelectorAll("#StateTue")[1];
                let StateTue3=document.querySelectorAll("#StateTue")[2];
                let StateTue4=document.querySelectorAll("#StateTue")[3];
                StateTue.disabled=true;
                StateTue2.disabled=true;
                StateTue3.disabled=true;
                StateTue4.disabled=true;
                flagtue=1;
            }
        });
    TueAllDay.addEventListener('click',function(){

         let StateTue=document.querySelectorAll("#StateTue")[0];
                let StateTue2=document.querySelectorAll("#StateTue")[1];
                let StateTue3=document.querySelectorAll("#StateTue")[2];
                let StateTue4=document.querySelectorAll("#StateTue")[3];
                StateTue.disabled=true;
                StateTue2.disabled=true;
                StateTue3.disabled=true;
                StateTue4.disabled=true;

    });
    Tuehour.addEventListener('click',function(){

        let StateTue=document.querySelectorAll("#StateTue")[0];
        let StateTue2=document.querySelectorAll("#StateTue")[1];
        let StateTue3=document.querySelectorAll("#StateTue")[2];
        let StateTue4=document.querySelectorAll("#StateTue")[3];
        StateTue.disabled=false;
        StateTue2.disabled=false;
        StateTue3.disabled=false;
        StateTue4.disabled=false;
    })





       //wednesday
       let flexSwitchCheckDefaultWed = document.querySelector("#flexSwitchCheckDefault3");
       let WedAllDay=document.querySelector("#flexRadioDefault3");
       let Wedhour=document.querySelector("#flexRadioDefault3hr");
       var flagWed=1;
       flexSwitchCheckDefaultWed.addEventListener("click",function(){
           if (flagWed==1) {
            Wedhour.disabled=false;
            WedAllDay.disabled=false;
            flagWed=0;
           }else{
            Wedhour.disabled=true;
            WedAllDay.disabled=true;
            WedAllDay.checked=false;
            Wedhour.checked=false;
            let StateWed=document.querySelectorAll("#StateWed")[0];
            let StateWed2=document.querySelectorAll("#StateWed")[1];
            let StateWed3=document.querySelectorAll("#StateWed")[2];
            let StateWed4 = document.querySelectorAll("#StateWed")[3];

               //選擇時間
               //
            StateWed.disabled=true;
            StateWed2.disabled=true;
            StateWed3.document=true;
            StateWed4.document=true;
            flagWed=1;
           }
       });
       WedAllDay.addEventListener('click',function(){
            let StateWed=document.querySelectorAll("#StateWed")[0];
            let StateWed2=document.querySelectorAll("#StateWed")[1];
            let StateWed3=document.querySelectorAll("#StateWed")[2];
            let StateWed4=document.querySelectorAll("#StateWed")[3];
            StateWed.disabled=true;
            StateWed2.disabled=true;
            StateWed3.document=true;
            StateWed4.document=true;
       });
         Wedhour.addEventListener('click',function(){
            let StateWed=document.querySelectorAll("#StateWed")[0];
            let StateWed2=document.querySelectorAll("#StateWed")[1];
            let StateWed3=document.querySelectorAll("#StateWed")[2];
            let StateWed4=document.querySelectorAll("#StateWed")[3];
            StateWed.disabled=false;
            StateWed2.disabled=false;
            StateWed3.document=false;
            StateWed4.document=false;
       });
       //thursday
        let flexSwitchCheckDefaultThu=document.querySelector("#flexSwitchCheckDefault4");
        let ThuAllDay=document.querySelector("#flexRadioDefault4");
        let Thuhour = document.querySelector("#flexRadioDefault4hr");
        let StateThu=document.querySelectorAll("#StateThu")[0];
        let StateThu2=document.querySelectorAll("#StateThu")[1];
        let StateThu3=document.querySelectorAll("#StateThu")[2];
        let StateThu4=document.querySelectorAll("#StateThu")[3];
        var flagThu=1;  
       flexSwitchCheckDefaultThu.addEventListener("click",function (){
           if (flagThu==1) {
            ThuAllDay.disabled=false;
            Thuhour.disabled=false;
            flagThu=0;
           }else{
                ThuAllDay.disabled=true;
                Thuhour.disabled=true;
                ThuAllDay.checked=false;
                Thuhour.checked=false;
                StateThu.disabled=true;
                StateThu2.disabled=true;
                StateThu3.disabled=true;
                StateThu4.disabled=true;
                flagThu=1; 
           }
       });
       ThuAllDay.addEventListener('click',function(){
            StateThu.disabled=true;
            StateThu2.disabled=true;
            StateThu3.disabled=true;
            StateThu4.disabled=true;
       })
     Thuhour.addEventListener("click",function(){
            StateThu.disabled=false;
            StateThu2.disabled=false;
            StateThu3.disabled=false;
            StateThu4.disabled=false;
     })

       //friday
       let flexSwitchCheckDefaultFri=document.querySelector("#flexSwitchCheckDefault5");
        let FridayAllDay=document.querySelector("#flexRadioDefault5");
        let Fridayhour = document.querySelector("#flexRadioDefault5hr");
        let StateFri=document.querySelectorAll("#StateFri")[0];
        let StateFri2=document.querySelectorAll('#StateFri')[1];
        let StateFri3=document.querySelectorAll("#StateFri")[2];
        let StateFri4=document.querySelectorAll("#StateFri")[3];
        flagFri=1;
       flexSwitchCheckDefaultFri.addEventListener('click',function () {
           if(flagFri==1){
            FridayAllDay.disabled=false;
            Fridayhour.disabled=false;
            flagFri=0;
           }else{
                FridayAllDay.disabled=true;
                Fridayhour.disabled=true;
                StateFri.disabled=true;
                StateFri2.disabled=true;
                StateFri3.disabled=true;
                StateFri4.disabled=true;
                FridayAllDay.checked=false;
                Fridayhour.checked=false;
                flagFri=1;
           }
       });
       FridayAllDay.addEventListener("click",function(){
                StateFri.disabled=true;
                StateFri2.disabled=true;
                StateFri3.disabled=true;
                StateFri4.disabled=true;
       });
       Fridayhour.addEventListener("click",function(){
                StateFri.disabled=false;
                StateFri2.disabled=false;
                StateFri3.disabled=false;
                StateFri4.disabled=false;
       })

       //saturday
       let flexSwitchCheckDefaultSat=document.querySelector("#flexSwitchCheckDefault6");
       let SatAllday=document.querySelector("#flexRadioDefault6");
       let Sathour = document.querySelector('#flexRadioDefault6hr');
       let StateSat=document.querySelectorAll("#StateSat")[0];
       let StateSat2=document.querySelectorAll("#StateSat")[1];
       let StateSat3=document.querySelectorAll("#StateSat")[2];
       let StateSat4=document.querySelectorAll("#StateSat")[3];
       var flagSat=1;
       flexSwitchCheckDefaultSat.addEventListener('click',function () {
           if ((flagSat==1)) {
               SatAllday.disabled=false;
               Sathour.disabled=false;
               flagSat=0;
           }else{
                SatAllday.disabled=true;
                Sathour.disabled=true;
                SatAllday.checked=false;
                Sathour.checked=false;
                StateSat.disabled=true;
                StateSat2.disabled=true;
                StateSat3.disabled=true;
                StateSat4.disabled=true;
                flagSat=1;
           };
       });
       SatAllday.addEventListener("click",function(){
                StateSat.disabled=true;
                StateSat2.disabled=true;
                StateSat3.disabled=true;
                StateSat4.disabled=true;
       });
       Sathour.addEventListener('click',function(){
                StateSat.disabled=false;
                StateSat2.disabled=false;
                StateSat3.disabled=false;
                StateSat4.disabled=false;
       });

        //sunday 
        let flexSwitchCheckDefaultSun=document.querySelector("#flexSwitchCheckDefault7");
        let SunAllDay=document.querySelector("#flexRadioDefault7");
        let Sunhour=document.querySelector('#flexRadioDefault7hr');
        let StateSun=document.querySelectorAll('#StateSun')[0];
        let StateSun2=document.querySelectorAll("#StateSun")[1];
        let StateSun3=document.querySelectorAll("#StateSun")[2];
        let StateSun4=document.querySelectorAll("#StateSun")[3];
        var flagSun=1;
        flexSwitchCheckDefaultSun.addEventListener("click",function () {
            if(flagSun==1){
            SunAllDay.disabled=false;
            Sunhour.disabled=false;
            flagSun=0;
            }else{
                SunAllDay.disabled=true;
                Sunhour.disabled=true;
                SunAllDay.checked=false;
                Sunhour.checked=false;
                StateSun.disabled=true;
                StateSun2.disabled=true;
                StateSun3.disabled=true;
                StateSat4.disabled=true;
                flagSun=0;
            }
        });
               SunAllDay.addEventListener("click",function(){
                StateSun.disabled=true;
                StateSun2.disabled=true;
                StateSun3.disabled=true;
                StateSat4.disabled=true;
       });
       Sunhour.addEventListener('click',function(){
                StateSun.disabled=false;
                StateSun2.disabled=false;
                StateSun3.disabled=false;
                StateSun4.disabled=false;
       });

///營業時間 radio///
