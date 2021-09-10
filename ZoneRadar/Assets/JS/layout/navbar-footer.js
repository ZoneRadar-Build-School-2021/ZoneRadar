window.onload = () => {
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

    //#region 導覽列的成為場地主登入前後連結更改
    // let become_host_link = document.querySelector("nav.shared-navbar .become-host-link");
    // become_host_link.setAttribute("href", "~/MemberCenter/SellerCenter");
    // become_host_link.removeAttribute("data-bs-toggle");
    // become_host_link.removeAttribute("data-bs-target");
    //#endregion
}