var address = document.querySelector("#address");
var city = document.querySelector("#city");
var ZodeCode = document.querySelector("#ZodeCode");
var SpaceName = document.querySelector("#SpaceName");
var area = document.querySelector("#area");
var Maxpeople = document.querySelector("#Maxpeople");
var SpacePrice = document.querySelector("#SpacePrice");
var minSpacePrice = document.querySelector("#minSpacePrice");
var hourDiscount = document.querySelector("#hourDiscount");
var Discount = document.querySelector("#Discount");
var submitAdd = document.querySelector("#submitAdd");

var addressStr = address.value;
var cityStr = city.value;
var ZodeCodeStr = ZodeCode.value;
var SpaceNameStr = SpaceName.value;
var areaStr = area.value;
var MaxpeopleStr = Maxpeople.value;
var SpacePriceStr = SpacePrice.value;
var minSpacePriceStr = minSpacePrice.value;
var hourDiscountStr = hourDiscount.value;
var DiscountStr = Discount.value;



function alertcheck() {
    
    if (addressStr == "") {
        alert("地址未填寫");
        return;
    }
    if (cityStr == "") {
        alert("城市未填寫");
        return;
    }
    if (ZodeCodeStr == "") {
        alert("郵遞區號未填寫");
        return;
    }
    if (SpaceNameStr == "") {
        alert("場地名稱未填寫");
        return;
    }
    if (areaStr == "") {
        alert("坪數未填寫");
        return;
    }
    if (MaxpeopleStr == "") {
        alert("最大人數未填寫");
        return;
    }
    if (SpacePriceStr == "") {
        alert("定價未填寫");
        return;
    }
    if (minSpacePriceStr == "") {
        alert("最小時數未填寫");
        return;
    }
    if (hourDiscountStr == "") {
        alert("每小時折扣多少未填寫");
        return;
    }
    if (DiscountStr == "") {
        alert("折扣未填寫");
        return;
    }
}