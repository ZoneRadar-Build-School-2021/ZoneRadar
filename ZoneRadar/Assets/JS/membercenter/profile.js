//photo upload
var CLOUDINARY_URL = 'https://api.cloudinary.com/v1_1/bs20210831ta/upload';
var CLOUDINARY_UPLOAD_PRESET = 'hblvplct';

var imgview = document.getElementById('imgpic');
var fileupload = document.getElementById('fileid');

document.getElementById('uploadid').addEventListener('click', openDialog);
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

