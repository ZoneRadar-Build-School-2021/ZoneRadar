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
let app = new Vue({
    el: '#app',
    data: {
        isDisabled: true,
        inputData: {
            Name: '',
            Phone: '',
            Description: '',
            Checked: ''
        },
        inputDataCheck: {
            NameError: false,
            NameErrorMsg: ''
        },
        originalinputData: {
            Name: '',
            Phone: '',
            Description: '',
            Checked: ''
        },
    },
    watch: {
        inputData: {
            deep: true,
            handler() {
                if (this.originalinputData.Name === this.inputData.Name && this.originalinputData.Phone === this.inputData.Phone && this.originalinputData.Description === this.inputData.Description && this.originalinputData.Checked === this.inputData.Checked) {
                    this.isDisabled = true;
                }
                else if (this.inputData.Name === '') {
                    this.isDisabled = true;
                }
                else {
                    this.isDisabled = false;
                }
            }
        },
        'inputData.name': {
            handler() {
                if (this.inputData.Name == '') {
                    this.inputDataCheck.NameError = true;
                    this.inputDataCheck.NameErrorMsg = 'Name 欄位是必要項';
                }
                else {
                    this.inputDataCheck.NameError = false;
                    this.inputDataCheck.NameErrorMsg = '';
                }
            }
        }
    },
})
