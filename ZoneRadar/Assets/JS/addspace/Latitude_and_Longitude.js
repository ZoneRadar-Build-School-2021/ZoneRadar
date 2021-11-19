let i;
let split;
//抓地址的資料
function trans() {
    i = 0;
    $("#target").val("");
    let content = $('#city').val() + $('#district').val() + $('#address').val();
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
    let geocoder = new google.maps.Geocoder();
    geocoder.geocode({
        "address": addr
    }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            let content = $("#target").val();
            $("#target").val(content + results[0].geometry.location.lat());
            $("#target2").val(content + results[0].geometry.location.lng());
            let lat = $('#target').val();
            let lng = $('#target2').val();
            console.log(lat);
            console.log(lng);
        }
        else if (status == google.maps.GeocoderStatus.ZERO_RESULTS) {
            Swal.fire("您輸入的地址可能不存在!");
        }
        else if (status == google.maps.GeocoderStatus.REQUEST_DENIED) {
            Swal.fire("請求被拒絕!");
        }
        else {
            let content = $("#target").val();

            $("#target").val(content + addr + "查無經緯度，或系統發生錯誤！" + "\n");
        }
    });

}


