//photo upload
var CLOUDINARY_URL = 'https://api.cloudinary.com/v1_1/bs20210831ta/upload';
var CLOUDINARY_UPLOAD_PRESET = 'hblvplct';

var imgview = document.getElementById('imgpic');
var fileupload = document.getElementById('files');

document.getElementById('upload').addEventListener('click', openDialog);
function openDialog() {
    fileupload.click();
    // document.getElementById('fileid').click();
}

fileupload.addEventListener('change', function (event) {
    var file = event.target.files[0];
    var formData = new FormData();
    formData.append('file', file);
    formData.append('upload_preset', CLOUDINARY_UPLOAD_PRESET);

    axios({
        url: CLOUDINARY_URL,
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        data: formData
    }).then(function (res) {
        console.log(res);
        imgview.src = res.data.secure_url;
    }).catch(function (err) {
        console.error(err);
    });
});

//button disabled
let submitButton = document.getElementById("submit");

let input_name = document.getElementById("name");
let input_phone = document.getElementById("phone");
let input_description = document.getElementById("description");
let checkbox_value = document.getElementById("check");

let original_name = document.getElementById("name").value;
let original_phone = document.getElementById("phone").value;
let original_description = document.getElementById("description").value;
let original_checkbox = document.getElementById("check").value;


input_name.addEventListener("keyup", (x) => {
    const Name = x.currentTarget.value;
    submitButton.disabled = false;

    if (Name === original_name) {
        submitButton.disabled = true;
    }
});

input_phone.addEventListener("keyup", (x) => {
    const Phone = x.currentTarget.value;
    submitButton.disabled = false;

    if (Phone === original_phone) {
        submitButton.disabled = true;
    }
});

input_description.addEventListener("keyup", (x) => {
    const Description = x.currentTarget.value;
    submitButton.disabled = false;

    if (Description === original_description) {
        submitButton.disabled = true;
    }
});

checkbox_value.addEventListener('change', e => {
    console.log(e.currentTarget.value);
    const Checking = e.currentTarget.value;
    submitButton.disabled = false;

    if (Checking === original_checkbox) {
        submitButton.disabled = true;
    }
});
