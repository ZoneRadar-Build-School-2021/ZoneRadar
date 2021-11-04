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
})