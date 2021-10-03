;
(function () {
    // DOM Nodes 
    const cardListNode = document.querySelector('.card-list');

    // Flatepickr
    flatpickr.localize(flatpickr.l10ns.zh_tw);
    const dateFilterWeb = flatpickr("#web-date-filter", {
        altInput: true,
        altFormat: "Y/m/d",
        disableMobile: "true"
    });

    const dateFilterPhone = flatpickr("#phone-date-filter", {
        altInput: true,
        altFormat: "Y/m/d",
        disableMobile: "true"
    });

    // Placeholder
    for (i = 0; i <= 7; i++) {
        let placeholder = document.querySelector('#placeholder-template').content.cloneNode(true);
        cardListNode.appendChild(placeholder);
    }

    // onload
    window.addEventListener('load', function () {
        let targetURL = 'https://raw.githubusercontent.com/stevesun5160/FileStorage/main/venue.json';
        fetch(targetURL)
            .then(respone => respone.json())
            .then(result => {
                cardListNode.innerHTML = '';

                for (let venue of Object.keys(result)) {
                    const templateClone = document.querySelector('#card-template').content.cloneNode(true);
                    const venueImgNode = templateClone.querySelector('.swiper-wrapper');
                    const venueNameNode = templateClone.querySelector('.venue-name');
                    const pricePerHourNode = templateClone.querySelector('.venue-price');
                    const ratingNode = templateClone.querySelector('.venue-rating');
                    const revieCountNode = templateClone.querySelector('.review-count');
                    const capacityNode = templateClone.querySelector('.capacity');
                    const minHourNode = templateClone.querySelector('.min-time');
                    const areaNode = templateClone.querySelector('.area');

                    // 圖片
                    result[venue].photos.forEach(imgURL => {
                        let swiperSlide = document.createElement('div');
                        swiperSlide.classList = 'venue-img swiper-slide';
                        swiperSlide.style.backgroundImage = `url(${imgURL})`

                        venueImgNode.appendChild(swiperSlide);
                    })

                    cardListNode.appendChild(templateClone);

                    // 價格
                    pricePerHourNode.innerText = result[venue].pricePerHour;

                    // 名稱
                    venueNameNode.innerText = result[venue].name;

                    // 評價
                    if (result[venue].reviewStars.length === 0) {
                        let blank = document.createElement('span');
                        blank.innerText = "目前沒有任何評價";
                        ratingNode.insertBefore(blank, revieCountNode);
                    } else {
                        let avgStars = result[venue].reviewStars.reduce(function (a, b) {
                            return a + b
                        }, 0) / result[venue].reviewStars.length

                        if (avgStars % 1 === 0) {
                            for (let i = 1; i <= avgStars; i++) {
                                let star = document.createElement('i');
                                star.classList = 'fas fa-star';
                                ratingNode.insertBefore(star, revieCountNode);
                            }
                        } else {
                            for (let i = 1; i < avgStars; i++) {
                                let star = document.createElement('i');
                                star.classList = 'fas fa-star';
                                ratingNode.insertBefore(star, revieCountNode);
                            }
                            let halfStar = document.createElement('i');
                            halfStar.classList = 'fas fa-star-half-alt';
                            ratingNode.insertBefore(halfStar, revieCountNode);
                        }
                    }

                    // 評價數
                    revieCountNode.innerText = result[venue].reviewCount;

                    // 簡介
                    capacityNode.innerText = result[venue].capacity;
                    minHourNode.innerText = result[venue].minTime;
                    areaNode.innerText = result[venue].area;

                    // 設定輪播
                    const swiper = new Swiper('.swiper', {
                        loop: true,
                        pagination: {
                            el: '.swiper-pagination',
                            clickable: true,
                        },
                        navigation: {
                            nextEl: '.swiper-button-next',
                            prevEl: '.swiper-button-prev',
                        },
                    });
                }
            })

    })







})()