//#region 登入/註冊modal相連的連結
let register_modal = document.querySelector("#register-modal");
let login_modal = document.querySelector("#login-modal");
document.querySelector("#a-login-modal").addEventListener("click", () => {
    bootstrap.Modal.getOrCreateInstance(register_modal).hide();
})
document.querySelector("#a-register-modal").addEventListener("click", () => {
    bootstrap.Modal.getOrCreateInstance(login_modal).hide();
})
//#endregion


//#region 登入modal的前端驗證(Vue)
let login_form_vue = new Vue({
    el: "#login-form-vue",
    data: {
        inputData: {
            account: "",
            password: ""
        },
        inputDataCheck: {
            accountError: false,
            accountErrorMsg: "",
            passwordError: false,
            passwordErrorMsg: ""
        },
        isVerify: false
    },
    watch: {
        "inputData.account": {
            immediate: true,
            handler() {
                let emailRegexp = /^([\w\.\-]){1,64}\@([\w\.\-]){1,64}$/
                if ( this.inputData.account == "") {
                    this.inputDataCheck.accountError = true;
                    this.inputDataCheck.accountErrorMsg = "請填寫此欄位";
                } else if (!emailRegexp.test(this.inputData.account)) {
                    this.inputDataCheck.accountError = true;
                    this.inputDataCheck.accountErrorMsg = "Email輸入格式錯誤";
                } else {
                    this.inputDataCheck.accountError = false;
                    this.inputDataCheck.accountErrorMsg = "";
                }

                this.checkVerify();
            }
        },
        "inputData.password": {
            immediate: true,
            handler() {
                let passwordRegexp = /^(?!.*[^\x21-\x7e])(?=.{6,50})(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$/
                if ( this.inputData.password == "") {
                    this.inputDataCheck.passwordError = true;
                    this.inputDataCheck.passwordErrorMsg = "請填寫此欄位";
                } else if (this.inputData.password.length < 6 || this.inputData.password.length > 50) {
                    this.inputDataCheck.passwordError = true;
                    this.inputDataCheck.passwordErrorMsg = "密碼長度需為6~50字元";
                } else if (!passwordRegexp.test(this.inputData.password)) {
                    this.inputDataCheck.passwordError = true;
                    this.inputDataCheck.passwordErrorMsg = "密碼必須包含至少1個數字、小寫英文和大寫英文";
                }
                else {
                    this.inputDataCheck.passwordError = false;
                    this.inputDataCheck.passwordErrorMsg = "";
                }

                this.checkVerify();
            }
        }
    },
    methods: {
        checkVerify() {
            for (let prop in this.inputDataCheck) {
                if (this.inputDataCheck[prop] == true) {
                    this.isVerify = false;
                    return;
                }
            }
            this.isVerify = true;
        }
    }
})
//#endregion



//#region 登入功能
let login_btn = document.querySelector("#login-submit");
login_btn.addEventListener("click", function () {
    let login_email = document.querySelector("#LoginEmail").value;
    let login_password = document.querySelector("#LoginPassword").value;
    let login_form_data = new FormData();
    login_form_data.append("LoginEmail", login_email);
    login_form_data.append("LoginPassword", login_password);


    axios({
        url: "/MemberCenter/Login",
        method: "POST",
        data: login_form_data,
        headers: {
            Accept: "application/json",
            "Content-Type": "application/x-www-form-urlencoded"
        }
    }).then(response => {
        let icon_string;
        if (response.data.IsSuccessful) {
            member_only.forEach(item => {
                item.classList.remove("d-none");
            })
            customer_only.forEach(item => {
                item.classList.add("d-none");
            })
            icon_string = "success";
            //放上大頭貼
            let user_photos = document.querySelectorAll(".user-pic img");
            user_photos.forEach(item => {
                item.setAttribute("src", response.data.Photo);
            })
            //登入成功後，若有ReturnUrl字串，導去該頁
            if (location.search != "") {
                let queryString = location.search;
                let keyValue = queryString.split("?");
                let returnUrl = keyValue.find(function (item) {
                    return item.includes("ReturnUrl");
                })
                let returnUrlArr = returnUrl.split("=");
                window.location = `${location.origin}${returnUrlArr[1]}`;
            }
        } else {
            icon_string = "error";
        }
        
        Swal.fire({
            title: response.data.ShowMessage,
            icon: icon_string,
            showConfirmButton: true,
            confirmButtonColor: "#be7418",
            confirmButtonText: "OK",
            position: "top"
        })
    }).catch(error => console.log(error))
})
//#endregion