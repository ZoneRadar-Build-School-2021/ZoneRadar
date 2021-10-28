const urlZipcode =
    'https://raw.githubusercontent.com/NickWang841231/NickWang841231.github.io/master/JS%E4%B8%8A%E8%AA%B2/TaiwanAddress_simple/TaiwanAddress_simple.json';

let citySelect, districtSelect, ZipCode;


window.onload = function () {
    citySelect = document.getElementById('city');
    districtSelect = document.getElementById('district');
    submitButton = document.querySelector('input[type=submit]');
    ZipCode = document.getElementById('ZipCode');

    //1.fecth zipcode json並產生city縣市選項
    createSelectOptions(urlZipcode);

    //2.註冊city選取change改變件ZipCode
    //citySelect.onchange = citySelectedChange;
    citySelect.addEventListener('change', citySelectedChange);
    //3.註冊district選取change改變事件
    districtSelect.addEventListener('change', showSelectOption);

    //4.註冊Submit Button的click提交事件
    //submitButton.addEventListener('click', submitData);
};




let zipcodeArray = [];

//1.fecth zipcode json並產生city縣市選項
function createSelectOptions(url) {
    //a.抓取網路JSON資料，動態生City Dropdownlist
    fetch(url)
        .then(response => response.json())
        .then(result => {
            //陣列指派給result
            zipcodeArray = result;
            //console.log(zipcodeArray);

            //zipcodeArray做迭代
            zipcodeArray.forEach(item => {
                let option1 = document.createElement('option');
                option1.value = item.city;
                option1.text = item.city;

                citySelect.add(option1);
            });

            //建立city option ---請選擇縣市---
            let option2 = document.createElement("option");
            option2.value = "";
            //option2.text = "請選擇縣市";
            //option2.setAttribute("selected", "");
            city.add(option2, 0);

        })
        .catch(ex => {
            console.log(ex);
        });



    //b.產生District Dropdownlist ---請選擇行政區---
    districtSelect.disabled = true;

    let option3 = document.createElement("option");
    option3.value = "";
    option3.text = "請選擇行政區";
    districtSelect.add(option3, null);
}

//2.City選項change變更事件
function citySelectedChange(event) {
    ZipCode.innerHTML = "";
    districtSelect.length = 1; //清空第二個下拉式選單,只保留第一個
    //submitButton.disabled = true;

    //取得選取的縣市資料
    var cityValue = citySelect.selectedOptions[0].value;
    var cityText = citySelect.selectedOptions[0].text;

    //如果未選擇City, 清空第二個下拉式選單,只保留第一個option, 最後return 返回  
    if (cityValue = '') {
        districtSelect.disabled = true;

        return;
    }

    //啟用第二個下拉式選單
    districtSelect.disabled = false;

    //產生District Dropdownlist ---請選擇行政區---
    if (districtSelect.length == 0) {
        let option0 = document.createElement("option");
        option0.value = "";
        option0.text = "請選擇行政區";
        districtSelect.add(option0, null);
    }

    //取得District資料
    //因為filter 回傳會是陣列 但只要第一筆 所以用[0]
    var city = zipcodeArray.filter(item => item.city == cityText)[0];
    console.log(cityText)


    let districts = city.districts;
    console.log(districts)
    districts.forEach(dt => {
        let option1 = document.createElement('option');
        option1.setAttribute('data-zipcode', dt.zipCode)
        option1.value = dt.district;
        option1.text = dt.district;
        districtSelect.add(option1);
    });

}

//3.city和district兩層下拉式選單都選取後, 將選擇的city和district顯示出來
function showSelectOption() {
    let cityValue = citySelect.selectedOptions[0].value;
    let cityText = citySelect.selectedOptions[0].text;
    let districtValue = districtSelect.selectedOptions[0].value;
    let zipcode = districtSelect.selectedOptions[0].dataset.zipcode;
    console.log(zipcode)
    // console.log(zipCodevalue)
    //如果city或district有任一個未選, 則submit disable*
    if (cityValue != "" && districtValue != "") {
        ZipCode.setAttribute('value', zipcode);
        // submitButton.disabled = false;
    } else {
        ZipCode.setAttribute('value',);
        // submitButton.disabled = true;
    }
}

//4.Submit提交資料
function submitData() {
    //FormData
    let formData = new FormData();
    formData.append("city", citySelect.selectedOptions[0].value);
    formData.append("district", districtSelect.selectedOptions[0].text);

    let xhr = new XMLHttpRequest();
    xhr.open("POST", "https://www.codemagic.com");
    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    xhr.send(formData);

    setTimeout(() => {
        alert(`你提交了資料: ${ZipCode.innerText}`)
    }, 300);
}
