document.addEventListener("DOMContentLoaded", function () {
    let pic = document.querySelectorAll('.star_pic');
    let piclen = pic.length;
    for (let i = 0; i < piclen; i++) {
        pic[i].addEventListener("mouseout", mouseout);
        pic[i].addEventListener("mouseover", mouseover);
        pic[i].addEventListener("click", Click);
    }
    document.getElementById("clean").addEventListener("click", clean);
});

function mouseover() {
    let pic = document.querySelectorAll('.star_pic');
    for (let i = 0; i < this.id.substr(5); i++) {
        pic[i].src = "/Assets/IMG/Usercenter/chngstar.png";
    }
}


function mouseout() {
    let pic = document.querySelectorAll('.star_pic');
    for (let i = 0; i < this.id.substr(5); i++) {
        pic[i].src = "/Assets/IMG/Usercenter/star.png";
        document.getElementById("score").innerHTML = "";
    }
}

function Click() {
    let pic = document.querySelectorAll('.star_pic');
    for (let i = 0; i < this.id.substr(5); i++) {
        pic[i].src = "/Assets/IMG/Usercenter/chngstar.png";
    }
    let piclen = pic.length;
    for (let i = 0; i < piclen; i++) {
        pic[i].removeEventListener("mouseout", mouseout);
        pic[i].removeEventListener("mouseover", mouseover);
        pic[i].removeEventListener("click", Click);
    }
}

function clean() {
    let pic = document.querySelectorAll('.star_pic');
    let piclen = pic.length;
    document.getElementById("score").innerHTML = "";
    for (let i = 0; i < piclen; i++) {
        pic[i].src = "/Assets/IMG/Usercenter/star.png";
        pic[i].addEventListener("mouseout", mouseout);
        pic[i].addEventListener("mouseover", mouseover);
        pic[i].addEventListener("click", Click);
    }
    document.getElementById("clean").addEventListener("click", clean);
}