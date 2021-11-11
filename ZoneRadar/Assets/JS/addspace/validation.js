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

//檢查地址 中文 地址要含中文
let address = document.querySelector("#address");
let addrinvalid = document.querySelector("#addrinvalid");
function addresscheck() {
    var addressStr = $('#address').val();
    console.log(addressStr);
    if (addressStr.includes("區") || addressStr.includes("市") || addressStr.includes("縣")) {
        addrinvalid.classList.remove('d-none');
        address.classList.add('border-danger');
    }
    else {
        addrinvalid.classList.add('d-none');
        address.classList.remove('border-danger');
    }
}
var types = document.getElementsByName('TypeDetailId');
let typeslist = new Array();
var checktypes;
var checkAmenity;
var checkCleaningOption;
var checkCancellation;
var checkHours;
types.forEach(function (item) {
    typeslist.push(item)
});
var AmenityDetails = document.getElementsByName('AmenityDetailID');
let AmenityDetaillist = new Array();

AmenityDetails.forEach(function (item) {
    AmenityDetaillist.push(item)
});
var CleaningOptionIDs = document.getElementsByName('CleaningOptionID');
let CleaningOptionIDlist = new Array();

CleaningOptionIDs.forEach(function (item) {
    CleaningOptionIDlist.push(item)
});
var CancellationIDs = document.getElementsByName('CancellationID');
let CancellationIDlist = new Array();

CancellationIDs.forEach(function (item) {
    CancellationIDlist.push(item)
});
var OperatingDays = document.getElementsByName('OperatingDay');
let OperatingDaylist = new Array();

OperatingDays.forEach(function (item) {
    OperatingDaylist.push(item)
});
var Hours1 = document.getElementsByName('Hours1');
var Hours2 = document.getElementsByName('Hours2');
var Hours3 = document.getElementsByName('Hours3');
var Hours4 = document.getElementsByName('Hours4');
var Hours5 = document.getElementsByName('Hours5');
var Hours6 = document.getElementsByName('Hours6');
var Hours7 = document.getElementsByName('Hours7');
let  Hourslist = new Array();

Hours1.forEach(function (item) {
    Hourslist.push(item)
});
Hours2.forEach(function (item) {
    Hourslist.push(item)
});
Hours3.forEach(function (item) {
    Hourslist.push(item)
});
Hours4.forEach(function (item) {
    Hourslist.push(item)
});
Hours5.forEach(function (item) {
    Hourslist.push(item)
});
Hours6.forEach(function (item) {
    Hourslist.push(item)
});
Hours7.forEach(function (item) {
    Hourslist.push(item)
});
var MonStart = document.getElementsByClassName("MonselectR");

//function getValue() {
//    var MonStart = $(".MonselectL").val();
//    var MonEnd = $(".MonselectR").val();
//    var MonEndNum = parseInt(MonEnd);
//    var MonStartNum = parseInt(MonStart);
//    var StateTueS = $('.StateTueS').val();
//    var StateTueE = $('.StateTueE').val();
//    var StateTueSNum = parseInt(StateTueS);
//    var StateTueENum = parseInt(StateTueE);
//    var StateWedS = $('.StateWedS').val();
//    var StateWedE = $('.StateWedE').val();
//    var StateWedSNum = parseInt(StateWedS);
//    var StateWedENum = parseInt(StateWedE);
//    let OperatingDaycheckTime = document.querySelector("#OperatingDaycheckTime");
//    if (MonEndNum > MonStartNum) {
//        OperatingDaycheckTime.innerHTML = " ";
//        console.log(1);
//    }
//    if (StateTueENum > StateTueSNum) {
//        OperatingDaycheckTime.innerHTML = " ";

//    }
//    if (StateWedENum > StateWedSNum) {
//        OperatingDaycheckTime.innerHTML = " ";

//    }
//    else {
//        OperatingDaycheckTime.innerHTML = "結束時間不能早於開始時間。";
//    }
    
//}
window.onmousewheel = function () {
    if (600<window.scrollY <1000) {
        if (typeslist[0].checked || typeslist[1].checked || typeslist[2].checked || typeslist[3].checked || typeslist[4].checked || typeslist[5].checked
            || typeslist[6].checked || typeslist[7].checked || typeslist[8].checked || typeslist[9].checked || typeslist[10].checked || typeslist[11].checked ||
            typeslist[12].checked || typeslist[13].checked || typeslist[14].checked || typeslist[15].checked || typeslist[16].checked)
        {
            typeinvalid.classList.add('d-none');
            checktypes = 1;
        }
        else {
            
            let typeinvalid = document.querySelector("#typeinvalid");
            typeinvalid.classList.remove('d-none');
        }
        if (AmenityDetaillist[0].checked || AmenityDetaillist[1].checked || AmenityDetaillist[2].checked || AmenityDetaillist[3].checked || AmenityDetaillist[4].checked || AmenityDetaillist[5].checked
            || AmenityDetaillist[6].checked || AmenityDetaillist[7].checked || AmenityDetaillist[8].checked || AmenityDetaillist[9].checked || AmenityDetaillist[10].checked || AmenityDetaillist[11].checked ||
            AmenityDetaillist[12].checked || AmenityDetaillist[13].checked || AmenityDetaillist[14].checked || AmenityDetaillist[15].checked || AmenityDetaillist[16].checked) {
            let AmenityDetailinvalid = document.querySelector("#AmenityDetailinvalid");
            AmenityDetailinvalid.classList.add('d-none');
            checkAmenity = 1;
        }
        else {
            let AmenityDetailinvalid = document.querySelector("#AmenityDetailinvalid");
            AmenityDetailinvalid.classList.remove('d-none');
        }
        if (CleaningOptionIDlist[0].checked || CleaningOptionIDlist[1].checked || CleaningOptionIDlist[2].checked || CleaningOptionIDlist[3].checked || CleaningOptionIDlist[4].checked || CleaningOptionIDlist[5].checked
            || CleaningOptionIDlist[6].checked || CleaningOptionIDlist[7].checked || CleaningOptionIDlist[8].checked || CleaningOptionIDlist[9].checked || CleaningOptionIDlist[10].checked || CleaningOptionIDlist[11].checked ||
            CleaningOptionIDlist[12].checked || CleaningOptionIDlist[13].checked || CleaningOptionIDlist[14].checked ||CleaningOptionIDlist[15].checked || CleaningOptionIDlist[16].checked || CleaningOptionIDlist[17].checked) {
            let CleaningOptionIDcheck = document.querySelector("#CleaningOptionIDcheck");
            CleaningOptionIDcheck.classList.add('d-none');
            checkCleaningOption = 1;
        }
        else {
            let CleaningOptionIDcheck = document.querySelector("#CleaningOptionIDcheck");
            CleaningOptionIDcheck.classList.remove('d-none');
        }
        if (CancellationIDlist[0].checked || CancellationIDlist[1].checked || CancellationIDlist[2].checked || CancellationIDlist[3].checked) {
            let CancellationIDcheck = document.querySelector("#CancellationIDcheck");
            CancellationIDcheck.classList.add('d-none');
        }
        else {
            let CancellationIDcheck = document.querySelector("#CancellationIDcheck");
            CancellationIDcheck.classList.remove('d-none');
        }
        if (OperatingDaylist[0].checked || OperatingDaylist[1].checked || OperatingDaylist[2].checked || OperatingDaylist[3].checked || OperatingDaylist[4].checked || OperatingDaylist[5].checked || OperatingDaylist[6].checked) {
            let OperatingDaycheck = document.querySelector("#OperatingDaycheck");
            OperatingDaycheck.classList.add('d-none');
            checkHours = 1;
        }
        else {
            let OperatingDaycheck = document.querySelector("#OperatingDaycheck");
            OperatingDaycheck.classList.remove('d-none');
        }
        if (Hourslist[0].checked || Hourslist[1].checked || Hourslist[2].checked || Hourslist[3].checked || Hourslist[4].checked || Hourslist[5].checked
            || Hourslist[6].checked || Hourslist[7].checked || Hourslist[8].checked || Hourslist[9].checked || Hourslist[10].checked || Hourslist[11].checked ||
            Hourslist[12].checked || Hourslist[13].checked) {
            let OperatingDaycheck = document.querySelector("#OperatingDaycheck");
            OperatingDaycheck.classList.add('d-none');
        }
        else {
            let OperatingDaycheck = document.querySelector("#OperatingDaycheck");
            OperatingDaycheck.classList.remove('d-none');
            OperatingDaycheck.innerHTML = "請填寫營業時間";
        }
        if (Hourslist[0].checked || Hourslist[1].checked || Hourslist[2].checked || Hourslist[3].checked || Hourslist[4].checked || Hourslist[5].checked
            || Hourslist[6].checked || Hourslist[7].checked || Hourslist[8].checked || Hourslist[9].checked || Hourslist[10].checked || Hourslist[11].checked ||
            Hourslist[12].checked || Hourslist[13].checked) {
            let OperatingDaycheck = document.querySelector("#OperatingDaycheck");
            OperatingDaycheck.classList.add('d-none');
        }
        else {
            let OperatingDaycheck = document.querySelector("#OperatingDaycheck");
            OperatingDaycheck.classList.remove('d-none');
            OperatingDaycheck.innerHTML = "請填寫營業時間";
        }
        

    }
}
var checkspaceName;
function spaceNamecheck() {
    let spaceNamecheck = document.querySelector("#spaceNamecheck");
    let SpaceNameinput = document.querySelector("#SpaceName");
    let spaceNameStr = $('#SpaceName').val();
    console.log(spaceNameStr);
    if (spaceNameStr.length<2) {
        spaceNamecheck.classList.remove('d-none');
        SpaceNameinput.classList.add('border-danger');
    } else if (spaceNameStr.length > 20) {
        spaceNamecheck.innerHTML = "名稱字數限制20字";
        spaceNamecheck.classList.remove('d-none');
        SpaceNameinput.classList.add('border-danger');
    }
    else {
        spaceNamecheck.classList.add('d-none');
        SpaceNameinput.classList.remove('border-danger');
        checkspaceName = 1;
    }

}



var maxlength = 1000;
_editor = CKEDITOR.replace("Introduction");
_editor.on('change', function (event) {
    var oldhtml = _editor.document.getBody().getHtml();
    var description = oldhtml.replace(/<.*?>/ig, "");
    var etop = $("#cke_1_top");
    var _slen = maxlength - description.length;
    console.log(description.length);
    var canwrite = $("<label id='canwrite'>可以輸入1000字</label>");
    if (etop.find("#canwrite").length <250) {
        let IntroductionNamecheck = document.querySelector("#IntroductionNamecheck");
        IntroductionNamecheck.classList.remove('d-none');
    } 
    if (etop.find("#canwrite").length < 1) {
        canwrite.css({ border: '1px #f1f1f1 solid', 'line-height': '28px', color: '#999' });
        etop.prepend(canwrite);
        etop.keypress
    }
    var _label = etop.find("#canwrite");
    if (description.length > maxlength) {
        //alert("最多可以输入"+maxlength+"個文字，您已達到最大字數限制");
        _editor.setData(oldhtml);
        _label.html("還可以輸入0字");
    } else {
        _label.html("還可以輸入" + _slen + "字");
    }
    if (description.length > 250) {
        let IntroductionNamecheck = document.querySelector("#IntroductionNamecheck");
        IntroductionNamecheck.classList.add('d-none');
    }
});



var regexpNum = new RegExp(/^(0|[1-9][0-9]*)$/);
var regexpNum2 = new RegExp(/^(?!0+$)(?!0*\.0*$)\d{1,2}(\.\d{1,1})?$/);

var numcheckA = $('#area').val();

let areacheckNum = $('#area').val();
let MaxpeopleNum = $('#Maxpeople').val();

var checkarea;
function Numcheck() {
    let areacheckNum = $('#area').val();
    let areacheck = document.querySelector("#areacheck")
    if (regexpNum.test(areacheckNum) && areacheckNum != 0) {
        checkarea = 1;
        areacheck.classList.add('d-none');
        let exampleFormControlInput1 = $('exampleFormControlInput1');
        exampleFormControlInput1.classList.add('border-danger');
    } else{
        areacheck.classList.remove('d-none');
        areacheck.classList.add('border-danger');
    }
}
console.error(checkarea);
var checkMaxpeople;
function Maxpeoplecheck() {
    let MaxpeopleNum = $('#Maxpeople').val();
    let Maxpeoplecheck = document.querySelector("#Maxpeoplecheck")
    if (regexpNum.test(MaxpeopleNum) && MaxpeopleNum!=0) {
        Maxpeoplecheck.classList.add('d-none');
        let Maxpeople = $('#Maxpeople');
        Maxpeople.classList.remove('border-danger');
        checkMaxpeople = 1;
    } else {
        Maxpeoplecheck.classList.remove('d-none');
        Maxpeoplecheck.classList.add('border-danger');
        let Maxpeople = $('#Maxpeople');
        Maxpeople.classList.add('border-danger');
    }
}
var checkSpacePrice;
function SpacePricecheck() {
    let SpacePriceNum = $('#SpacePrice').val();
    let SpacePricelecheck= document.querySelector("#SpacePricelecheck")
    if (regexpNum.test(SpacePriceNum) && SpacePriceNum!=0) {
        SpacePricelecheck.classList.add('d-none');
        let SpacePrice = $('#SpacePrice');
        SpacePrice.classList.remove('border-danger');
        checkSpacePrice = 1;
    } else {
        SpacePricelecheck.classList.remove('d-none');
        SpacePricelecheck.classList.add('border-danger');
        let SpacePrice = $('#SpacePrice');
        SpacePrice.classList.add('border-danger');
    }
}
var checkminSpacePrice;
function minSpacePricecheck() {
    let minSpacePriceNum = $('#minSpacePrice').val();
    let minSpacePricecheck = document.querySelector("#minSpacePricecheck")
    if (regexpNum.test(minSpacePriceNum) && minSpacePriceNum!=0) {
        minSpacePricecheck.classList.add('d-none');
        let minSpacePrice = $('#minSpacePrice');
        minSpacePrice.classList.remove('border-danger');
        checkminSpacePrice = 1;
    } else {
        minSpacePricecheck.classList.remove('d-none');
        minSpacePricecheck.classList.add('border-danger');
        let minSpacePrice = $('#minSpacePrice');
        minSpacePrice.classList.add('border-danger');
    }
}
var checkhourDiscounte;
function hourDiscountecheck() {
    let hourDiscountNum = $('#hourDiscount').val();
    let hourDiscountecheck = document.querySelector("#hourDiscountecheck")
    if (regexpNum.test(hourDiscountNum) && hourDiscountNum!=0) {
        hourDiscountecheck.classList.add('d-none');
        let hourDiscount = $('#hourDiscount');
        hourDiscount.classList.remove('border-danger');
        console.log(regexpNum.test(hourDiscountNum));
        checkhourDiscounte = 1;
    } else {
        hourDiscountecheck.classList.remove('d-none');
        hourDiscountecheck.classList.add('border-danger');
        let hourDiscount = $('#hourDiscount');
        hourDiscount.classList.add('border-danger');
    }
}
var checkDiscount;
function Discountcheck() {
    let DiscountNum = $('#Discount').val();
    let Discountcheck = document.querySelector("#Discountcheck")
    if (regexpNum2.test(DiscountNum) && DiscountNum!=0) {
        Discountcheck.classList.add('d-none');
        let Discount = $('#Discount');
        Discount.classList.add('border-danger');
        checkDiscount = 1;
    } else {
        Discountcheck.classList.remove('d-none');
        Discountcheck.classList.add('border-danger');
        let Discount = $('#Discount');
        Discount.classList.remove('border-danger');
    }
}

var addressStr = $('#address').val();
console.log(checkAmenity);
function openSubmitBtn() {
    if ( checkAmenity == 1 && checktypes == 1) {
        console.log(8);
        var success = document.querySelector("#success");
        success.disabled = false;
    }

}
openSubmitBtn();
