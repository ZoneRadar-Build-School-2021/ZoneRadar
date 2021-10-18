//#region swiper-activity
var swiper_activity = new Swiper('.swiper-activity', {
    slidesPerView: 2.3,
    spaceBetween: 10,
    speed: 600,
    freeMode: true,
    navigation: {
        nextEl: '.swiper-activity-button-next',
        prevEl: '.swiper-activity-button-prev',
    },
    breakpoints: {
        768: {
            slidesPerView: 3,
            spaceBetween: 20,
            freeMode: false
        },
        992: {
            slidesPerView: 5,
            spaceBetween: 30,
        }
    }
});
//#endregion
//#region swiper-location
var swiper_location = new Swiper(".swiper-location", {
    slidesPerView: 2.3,
    grid: {
        rows: 2,
    },
    spaceBetween: 10,
    speed: 600,
    freeMode: true,
    navigation: {
        nextEl: '.swiper-location-button-next',
        prevEl: '.swiper-location-button-prev',
    },
    breakpoints: {
        768: {
            slidesPerView: 3
        }
    },
});
//#endregion
//#region swiper-comment
var swiper_comment = new Swiper('.swiper-comment', {
    slidesPerView: 1,
    spaceBetween: 10,
    speed: 600,
    autoplay: {
        delay: 4000,
        disableOnInteraction: false,
    },
    loop: true,
    breakpoints: {
        768: {
            slidesPerView: 2,
            spaceBetween: 20,
        },
        992: {
            slidesPerView: 3,
            spaceBetween: 30,
        },
        1200: {
            slidesPerView: 4,
            spaceBetween: 30,
        }
    },
});
    //#endregion


window.addEventListener("load", () => {
    //#region ¤Û¿O¤ù
    let slide_imgs = document.querySelectorAll(".slide-img");
    slideImgShow();
    setInterval(slideImgShow, 15000);
    function slideImgShow() {
        for (let i = 0; i < slide_imgs.length; i++) {
            setTimeout(() => {
                slide_imgs[i].classList.remove("slide-img-show");
                if (i == slide_imgs.length - 1) {
                    slide_imgs[0].classList.add("slide-img-show");
                } else {
                    slide_imgs[i + 1].classList.add("slide-img-show");
                }
            }, i * 5000)
        }
    }
    //#endregion
    //#region Flatpickr
    let flatpickr_date = document.querySelector(".homepage-search .select-date");
    let date_picker = flatpickr(flatpickr_date,
        {
            minDate: "today",
            altInput: true,
            altFormat: 'Y/m/d',
            dateFormat: "Y-m-d",
            disableMobile: "true",
            locale: "zh_tw",
            onChange: function (selectedDates, dateStr, instance) {
                console.log(dateStr);
            }
        });
    //#endregion    
})