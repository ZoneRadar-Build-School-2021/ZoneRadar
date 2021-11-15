//button disabled
let app = new Vue({
    el: '#app',
    data: {
        isDisabled: true,
        inputData: {
            Photo: profileData.Photo,
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
            Photo: profileData.Photo,
            Name: profileData.Name,
            Phone: profileData.Phone,
            Email: profileData.Email,
            Description: profileData.Description,
            ReceiveEDM: profileData.ReceiveEDM
        },
        file: {}
    },
    watch: {
        inputData: {
            deep: true,
            handler() {
                if (this.originalinputData.Photo === this.inputData.Photo && this.originalinputData.Name === this.inputData.Name && this.originalinputData.Phone === this.inputData.Phone && this.originalinputData.Description === this.inputData.Description && this.originalinputData.ReceiveEDM === this.inputData.ReceiveEDM) {
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
            var file = e.target.files[0]
            this.file = file
        },
        imgUpload() {
            let formData = new FormData();
            formData.append('file', this.file);
            formData.append('upload_preset', 'yp7sicxt');
            axios({
                url: "https://api.cloudinary.com/v1_1/dt6vz3pav/upload",
                method: 'POST',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                data: formData,
                onUploadProgress: uploadEvent => {
                    console.log('Upload Progress: ' + Math.round(uploadEvent.loaded / uploadEvent.total * 100) + '%')
                }
            })
            .then(response => {
                let ImgUrl = {
                    MemberID: '',
                    ProfileImgUrl: response.data.secure_url
                };
                this.inputData.Photo = response.data.secure_url;
                axios({
                    url: "/webapi/spaces/SaveImg",
                    method: 'POST',
                    data: ImgUrl
                })
                .then(response => {
                    //...
                })
            })
            .catch(e => {
                //...
            })
        },
        removeImg() {
            let ImgUrl = {
                MemberID: '',
                ProfileImgUrl: ''
            };
            axios({
                url: "/webapi/spaces/SaveImg",
                method: 'POST',
                data: ImgUrl
            })
            .then(response => {
                this.inputData.Photo = 'https://res.cloudinary.com/dt6vz3pav/image/upload/v1636172646/court/user-profile_pdbu9q.png'; 
            })
            .catch(e => {
                //...
            })
        }
    }
})

