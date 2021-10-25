var i;
var split;
//抓地址的資料
function trans() {
    i = 0;
    $("#target").val("");
    var content = $('#city').val() + $('#district').val() + $('#address').val();
    console.log(content);
    split = content.split("\n");
    delayedLoop();
}

function delayedLoop() {
    addressToLatLng(split[i]);
    if (++i == split.length) {
        return;
    }
    window.setTimeout(delayedLoop, 1500);
}

function addressToLatLng(addr) {
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({
        "address": addr
    }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            var content = $("#target").val();
            $("#target").val(content + results[0].geometry.location.lat());
            $("#target2").val(content + results[0].geometry.location.lng());
            var lat = $('#target').val();
            var lng = $('#target2').val();
            console.log(lat);
            console.log(lng);
        }
        else if (status == google.maps.GeocoderStatus.ZERO_RESULTS) {
            alert("您輸入的地址可能不存在!\nThis may occur if the geocoder was passed a non-existent address.");
        }
        else if (status == google.maps.GeocoderStatus.REQUEST_DENIED) {
            alert("請求被拒絕!\nYour request was denied.");
        }
        else {
            var content = $("#target").val();

            $("#target").val(content + addr + "查無經緯度，或系統發生錯誤！" + "\n");
        }
    });

}


