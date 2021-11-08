//button disabled
let app = new Vue({
    el: '#app',
    data: {
        isDisabled: true,
        inputData: {
            //Photo: profileData.Photo,
            Name: profileData.Name,
            Phone: profileData.Phone,
            Email: profileData.Email,
            Description: profileData.Description,
            ReceiveEDM: profileData.ReceiveEDM
        },
        inputDataCheck: {
            NameError: false,
            NameErrorMsg: ''
        },
        originalinputData: {
            //Photo: profileData.Photo,
            Name: profileData.Name,
            Phone: profileData.Phone,
            Email: profileData.Email,
            Description: profileData.Description,
            ReceiveEDM: profileData.ReceiveEDM
        },
        profileimage: 'https://img.88icon.com/download/jpg/20200815/cacc4178c4846c91dc1bfa1540152f93_512_512.jpg!88con'
    },
    watch: {
        inputData: {
            deep: true,
            handler() {
                if (this.originalinputData.Name === this.inputData.Name && this.originalinputData.Phone === this.inputData.Phone && this.originalinputData.Description === this.inputData.Description && this.originalinputData.ReceiveEDM === this.inputData.ReceiveEDM) {
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
        'inputData.Name': {
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
    methods: {
        fileChange(e) {
            const file = e.target.files[0]
            this.profileimage = URL.createObjectURL(file)
        },
        imgUpload() {
            let formData = new FormData();
            formData.append('file', this.imageData);
            formData.append('upload_preset', 'yp7sicxt');
            axios.post("https://api.cloudinary.com/v1_1/dt6vz3pav/upload", formData, {
                onUploadProgress: uploadEvent => {
                    console.log('Upload Progress: ' + Math.round(uploadEvent.loaded / uploadEvent.total * 100) + '%')
                },
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            })
            .then(response => {
                //...
            })
            .catch(e => {
                //...
            })
        }
    }
})