window.onload = ()=>{
    //#region 登入/註冊modal
    let register_modal = document.querySelector("#register-modal");
    let login_modal = document.querySelector("#login-modal");
    document.querySelector("#a-login-modal").addEventListener("click", ()=>{
      bootstrap.Modal.getOrCreateInstance(register_modal).hide();
    })
    document.querySelector("#a-register-modal").addEventListener("click", ()=>{
      bootstrap.Modal.getOrCreateInstance(login_modal).hide();
    })
    //#endregion
  }