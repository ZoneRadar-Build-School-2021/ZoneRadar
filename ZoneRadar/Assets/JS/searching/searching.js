;
(function () {
    window.addEventListener('load', () => {
        // 主要節點
        const cardListNode = document.querySelector('.card-list');

        // filter bar節點
        const cityOptionBarNode = document.querySelector('#web-city-filter');
        const districtOptionBarNode = document.querySelector('#web-district-filter');
        const typeOptionBarNode = document.querySelector('#web-type-filter');
        const searchingBar = document.querySelector('#web-search');
        const searchingBarBtn = document.querySelector('#web-search-btn');
        const filterBtn = document.querySelector('#filter-btn')

        // filter modal節點
        const cityOptionModalNode = document.querySelector('#phone-city-filter');
        const districtOptionModalNode = document.querySelector('#phone-district-filter');
        const typeOptionModalNode = document.querySelector('#phone-type-filter');
        const lowPriceInputNode = document.querySelector('#low-price');
        const highPriceInputNode = document.querySelector('#high-price');
        const attendeeInputNode = document.querySelector('#attendee-filter');
        const areaInputNode = document.querySelector('#area-filter');
        const amenityOptionNode = document.querySelector('#amenity-filter')

        // Global Variables
        let filter = {
            City: '',
            District: '',
            Type: '',
            Date: '',
            HighPrice: '',
            LowPrice: '',
            Attendees: '',
            Amenities: [],
            Area: '',
            Keywords: '',
        };
        let cityDistrictList, typeList, amenityList, amenityIconList;

        // 日曆中文化
        flatpickr.localize(flatpickr.l10ns.zh_tw);

        // 初始化
        let type, city, date;
        if (sessionStorage.getItem('filterVm')) {
            let keywords = JSON.parse(sessionStorage.getItem('filterVm'));
            type = keywords.SelectedType;
            city = keywords.SelectedCity;
            date = keywords.SelectedDate;
        } else {
            type = '';
            city = '';
            date = '';
        }

        const getUrl = `https://localhost:44322/webapi/spaces/GetFilterData?type=${type}&city=${city}&date=${date}`;
        sessionStorage.clear();

        axios.get(getUrl)
            .then(response => {
                document.querySelector('#web-date-filter').value = '';
                document.querySelector('#phone-date-filter').value = '';
                filterOptions = response.data;
                // 抓出後端傳來篩選資料
                filter.City = filterOptions.SelectedCity;
                filter.Type = filterOptions.SelectedType;
                filter.Date = filterOptions.SelectedDate;

                // 篩選分組
                cityDistrictList = filterOptions.CityDistrictDictionary;
                typeList = filterOptions.SpaceTypeList;
                amenityList = filterOptions.AmenityList;
                amenityIconList = filterOptions.AmenityIconList;

                // 設定Filter
                setBarFilter();
                setModalFilter();

                // 渲染場地列表
                requestForSpaces(filter);
            })
            .catch(error => console.log(error));


        function setBarFilter() {
            // 設定日曆與事件監聽
            setFlatpickr('#web-date-filter');
            // 鎖定鄉鎮區選單
            if (!filter.City) {
                disableDistrictOption(districtOptionBarNode);
            } else {
                let defaultOption = document.createElement('option');
                defaultOption.innerText = '選擇鄉鎮區';
                defaultOption.value = 'default';
                defaultOption.setAttribute('selected', '');
                districtOptionBarNode.appendChild(defaultOption);
                cityDistrictList[filter.City].forEach((district, index) => {
                    let option = document.createElement('option');
                    option.value = index + 1;
                    option.innerText = district;
                    districtOptionBarNode.appendChild(option);
                })
            }
            // 設定縣市選單與事件監聽
            setCityAndDistrictOption(cityOptionBarNode, districtOptionBarNode);
            // 設定類型選單與事件監聽
            setTypeOption(typeOptionBarNode);
            // 設定關鍵字事件監聽
            setSearchBar(searchingBar);
            searchingBarBtn.addEventListener('click', keywordSearch);
            window.addEventListener('keyup', keywordSearchEnter);
        }

        function setModalFilter() {
            // 設定日曆與事件監聽
            setFlatpickr('#phone-date-filter');
            // 鎖定鄉鎮區選單
            if (!filter.City) {
                disableDistrictOption(districtOptionModalNode);
            } else {
                let defaultOption = document.createElement('option');
                defaultOption.innerText = '選擇鄉鎮區';
                defaultOption.value = 'default';
                defaultOption.setAttribute('selected', '');
                districtOptionModalNode.appendChild(defaultOption);
                cityDistrictList[filter.City].forEach((district, index) => {
                    let option = document.createElement('option');
                    option.value = index + 1;
                    option.innerText = district;
                    districtOptionModalNode.appendChild(option);
                })
            }
            // 設定縣市選單與事件監聽
            setCityAndDistrictOption(cityOptionModalNode, districtOptionModalNode);
            // 設定類型選單與事件監聽
            setTypeOption(typeOptionModalNode);
            // 設定Modal內其他選項與事件監聽
            filterBtn.addEventListener('click', setModalOptionAndEvent);
            // 設定關鍵字事件監聽
            //setSearchBar(searchingModal);
            //searchingModalBtn.addEventListener('click', keywordSearch);
        }

        function setFlatpickr(dateNode) {
            if (filter.Date) {
                flatpickr(dateNode, {
                    altInput: true,
                    altFormat: 'Y/m/d',
                    disableMobile: 'true',
                    defaultDate: filter.Date,
                    minDate: "today",
                    maxDate: new Date().fp_incr(90),
                    // change事件監聽
                    onChange: function (selectedDates, dateStr, instance) {
                        filter.Date = dateStr;
                        if (this.id === 'web-date-filter') {
                            requestForSpaces(filter);
                        }
                    },
                });
            } else {
                flatpickr(dateNode, {
                    altInput: true,
                    altFormat: 'Y/m/d',
                    disableMobile: 'true',
                    minDate: "today",
                    maxDate: new Date().fp_incr(90),
                    // change事件監聽
                    onChange: function (selectedDates, dateStr, instance) {
                        filter.Date = dateStr;
                        if (this.id === 'web-date-filter') {
                            requestForSpaces(filter);
                        }
                    },
                });
            }
        }

        function disableDistrictOption(districtNode) {
            let defaultOption = document.createElement('option');
            defaultOption.innerText = '鄉鎮區';
            defaultOption.setAttribute('selected', '');
            districtNode.appendChild(defaultOption);

            districtNode.setAttribute('disabled', '');
        };

        function setCityAndDistrictOption(cityNode, districtNode) {
            let cities = Object.keys(cityDistrictList);

            if (!filter.City) {
                let defaultOption = document.createElement('option');
                defaultOption.innerText = '選擇縣市';
                defaultOption.value = 'default';
                defaultOption.setAttribute('selected', '');
                cityNode.appendChild(defaultOption);
            }

            cities.forEach((city, index) => {
                let option = document.createElement('option');
                option.value = index + 1;
                if (city === filter.City) {
                    option.innerText = city;
                    option.setAttribute('selected', '');
                }
                option.innerText = city;
                cityNode.appendChild(option);
            });

            cityNode.addEventListener('change', function () {
                // 解鎖鄉鎮區選單
                districtNode.innerHTML = '';
                districtNode.removeAttribute('disabled');

                // 渲染畫面
                filter.City = this.querySelector(`option[value='${this.value}']`).innerText;
                if (filter.City === '選擇縣市') {
                    filter.City = '';
                    districtNode.setAttribute('disabled', '');
                }
                filter.District = '';
                if (this.id === 'web-city-filter') {
                    requestForSpaces(filter);
                }

                // 設定鄉鎮區選單
                let defaultOption = document.createElement('option');
                defaultOption.innerText = '選擇鄉鎮區';
                defaultOption.value = 'default';
                defaultOption.setAttribute('selected', '');
                districtNode.appendChild(defaultOption);

                if (cityDistrictList[filter.City]) {
                    cityDistrictList[filter.City].forEach((district, index) => {
                        let option = document.createElement('option');
                        option.value = index + 1;
                        option.innerText = district;
                        districtNode.appendChild(option);
                    })
                }
            })

            districtNode.addEventListener('change', function () {
                filter.District = this.querySelector(`option[value='${this.value}']`).innerText;
                if (filter.District === '選擇鄉鎮區') {
                    filter.District = '';
                }
                if (this.id === 'web-district-filter') {
                    requestForSpaces(filter);
                }
            });
        }

        function setTypeOption(typeNode) {
            //if (!filter.Type) {
            //    let defaultOption = document.createElement('option');
            //    defaultOption.innerText = '場地類型';
            //    defaultOption.value = 'default';
            //    defaultOption.setAttribute('selected', '');
            //    typeNode.appendChild(defaultOption);
            //}
            let defaultOption = document.createElement('option');
            defaultOption.innerText = '場地類型';
            defaultOption.value = 'default';
            defaultOption.setAttribute('selected', '');
            typeNode.appendChild(defaultOption);

            typeList.forEach((type, index) => {
                let option = document.createElement('option');
                option.value = index + 1;
                option.innerText = type;
                if (type === filter.Type) {
                    option.innerText = type;
                    option.setAttribute('selected', '');
                }
                typeNode.appendChild(option);
            });

            typeNode.addEventListener('change', function () {
                // 渲染畫面
                filter.Type = this.querySelector(`option[value='${this.value}']`).innerText;
                if (filter.Type === '場地類型') {
                    filter.Type = '';
                }
                if (this.id === 'web-type-filter') {
                    requestForSpaces(filter);
                }
            })
        }

        function setModalOptionAndEvent() {
            // 設定預設input內容與事件監聽
            setInputs();
            // 設定設施選單
            setAmenity();
            // 設定Modal內條件的監聽事件
            setBtnClickEvent();
            // 設定keyword監聽


            function setInputs() {
                let nodeArr = [lowPriceInputNode, highPriceInputNode, attendeeInputNode, areaInputNode];
                let valueArr = ['LowPrice', 'HighPrice', 'Attendees', 'Area'];

                // 設定預設值
                nodeArr.forEach((node, index) => {
                    node.value = '';
                    if (valueArr[index]) {
                        node.value = filter[valueArr[index]];
                    }
                })

                // 設定事件監聽
                valueArr.forEach((value, index) => {
                    nodeArr[index].addEventListener('change', function () {
                        filter[value] = this.value;
                    })
                })
            }

            function setAmenity() {
                amenityOptionNode.innerHTML = '';

                amenityList.forEach((amenity, index) => {
                    let amenityClone = document.querySelector('#amenity-template').content.cloneNode(true);
                    amenityClone.querySelector('img').setAttribute('src', `/Assets/IMG/${amenityIconList[index]}`);
                    amenityClone.querySelector('span').innerText = amenity;
                    amenityOptionNode.appendChild(amenityClone);
                })

                // 設定預設值
                if (filter.Amenities) {
                    amenityOptionNode.querySelectorAll('.btn').forEach(node => {
                        filter.Amenities.forEach(item => {
                            if (node.innerText === item) {
                                node.setAttribute('style', 'border: 2px solid #049DD9');
                            }
                        })
                    })
                    amenityOptionNode.querySelectorAll('.btn:not([style="border: 2px solid #049DD9"])').forEach(unselected => {
                        unselected.addEventListener('click', setBorder);
                    })
                    amenityOptionNode.querySelectorAll('.btn[style="border: 2px solid #049DD9"]').forEach(selected => {
                        selected.addEventListener('click', removeBorder);
                    })
                } else {
                    amenityOptionNode.querySelectorAll('.btn:not([style="border: 2px solid #049DD9"])').forEach(unselected => {
                        unselected.addEventListener('click', setBorder);
                    })

                    amenityOptionNode.querySelectorAll('.btn[style="border: 2px solid #049DD9"]').forEach(selected => {
                        selected.addEventListener('click', removeBorder);
                    })
                }

                function setBorder() {
                    this.setAttribute('style', 'border: 2px solid #049DD9');
                    this.addEventListener('click', removeBorder);
                }

                function removeBorder() {
                    this.removeAttribute('style');
                    this.removeEventListener('click', removeBorder);
                }
            }

            function setBtnClickEvent() {
                // 清除
                document.querySelector('#filter-modal .clear-btn').addEventListener('click', clearModalFilter)

                // 確認
                document.querySelector('#filter-modal .save-btn').addEventListener('click', saveModalFilter)

                function clearModalFilter() {
                    filter.HighPrice = '';
                    filter.LowPrice = '';
                    filter.Attendees = '';
                    filter.Area = '';
                    filter.Amenities.length = 0;

                    if (document.querySelector('#phone-city-filter').value !== 'default') {
                        document.querySelector('#phone-city-filter').value = 'default';
                        filter.City = '';
                    }
                    if (document.querySelector('#phone-district-filter').value !== 'default') {
                        document.querySelector('#phone-district-filter').value = 'default';
                        filter.District = '';
                    }
                    if (document.querySelector('#phone-date-filter').value) {
                        document.querySelector('#phone-date-filter').value = '';
                        filter.Date = '';
                    }

                    requestForSpaces(filter);
                    bootstrap.Modal.getOrCreateInstance('#filter-modal').hide();
                    document.querySelector('#filter-modal .save-btn').removeEventListener('click', saveModalFilter)
                    this.removeEventListener('click', clearModalFilter)
                }

                function saveModalFilter() {
                    filter.Amenities.length = 0;
                    amenityOptionNode.querySelectorAll('.btn[style="border: 2px solid #049DD9"]').forEach(amenity => {
                        filter.Amenities.push(amenity.innerText);
                    });

                    requestForSpaces(filter);
                    bootstrap.Modal.getOrCreateInstance('#filter-modal').hide();
                    document.querySelector('#filter-modal .clear-btn').removeEventListener('click', clearModalFilter)
                    this.removeEventListener('click', saveModalFilter);
                }
            }
        }

        function setSearchBar(node) {
            node.addEventListener('change', function () {
                filter.Keywords = this.value;
            })
        }

        function keywordSearch(e) {
            e.preventDefault();
            requestForSpaces(filter);
        }

        function keywordSearchEnter(e) {
            if (e.key !== 'Enter') return;
            requestForSpaces(filter);
        }

        function requestForSpaces(filter) {
            setPlaceholder();

            //if (filter.Keywords) {
            //    filter.City = '';
            //    filter.District = '';
            //    filter.Type = '';
            //    filter.Date = '';
            //    filter.HighPrice = '';
            //    filter.LowPrice = '';
            //    filter.Attendees = '';
            //    filter.Amenities = [];
            //    filter.Area = '';
            //}

            console.log(filter)

            axios.post('https://localhost:44322/webapi/spaces/GetFilteredSpaces', filter)
                .then(response => {
                    let spaceList = response.data;
                    renderSpaceCards(spaceList);
                })
        }

        function setPlaceholder() {
            cardListNode.innerHTML = '';
            for (i = 0; i <= 7; i++) {
                let placeholder = document.querySelector('#placeholder-template').content.cloneNode(true);
                cardListNode.appendChild(placeholder);
            }
        }

        function renderSpaceCards(spaceList) {
            cardListNode.innerHTML = '';
            spaceList.forEach(space => {
                const templateClone = document.querySelector('#card-template').content.cloneNode(true);
                const venueLink = templateClone.querySelector('.card-link');
                const venueImgNode = templateClone.querySelector('.swiper-wrapper');
                const venueNameNode = templateClone.querySelector('.venue-name');
                const venueAddressNode = templateClone.querySelector('.venue-address');
                const pricePerHourNode = templateClone.querySelector('.venue-price');
                const ratingNode = templateClone.querySelector('.venue-rating');
                const reviewCountNode = templateClone.querySelector('.review-count');
                const capacityNode = templateClone.querySelector('.capacity');
                const minHourNode = templateClone.querySelector('.min-time');
                const areaNode = templateClone.querySelector('.area');

                // 超連結
                venueLink.href = `/Booking/BookingPage/${space.SpaceID}`;

                // 圖片
                // space.SpaceImageURLList.forEach(imgURL => {
                //   let swiperSlide = document.createElement('div');
                //   swiperSlide.classList = 'venue-img swiper-slide';
                //   swiperSlide.style.backgroundImage = `url(${imgURL})`;

                //   venueImgNode.appendChild(swiperSlide);
                // });
                let swiperSlide = document.createElement('div');
                swiperSlide.classList = 'venue-img swiper-slide';
                swiperSlide.style.backgroundImage = `url(${space.SpaceImageURLList[0]})`;
                venueImgNode.appendChild(swiperSlide);

                // 價格
                pricePerHourNode.innerText = `NT$${space.PricePerHour}`;

                // 名稱
                venueNameNode.innerText = space.SpaceName;

                // 地址
                let address = document.createElement('span');
                address.classList = 'ms-1'
                address.innerText = `${space.City} ${space.District} ${space.Address}`;
                venueAddressNode.appendChild(address);

                // 評價
                if (space.Scores.length === 0) {
                    let blank = document.createElement('span');
                    blank.innerText = '目前沒有任何評價';
                    ratingNode.insertBefore(blank, reviewCountNode);
                } else {
                    let avgStars = space.Scores.reduce(function (a, b) {
                        return a + b;
                    }, 0) / space.Scores.length;

                    if (avgStars % 1 === 0) {
                        for (let i = 1; i <= avgStars; i++) {
                            let star = document.createElement('i');
                            star.classList = 'fas fa-star';
                            ratingNode.insertBefore(star, reviewCountNode);
                        }
                    } else {
                        for (let i = 1; i < avgStars; i++) {
                            let star = document.createElement('i');
                            star.classList = 'fas fa-star';
                            ratingNode.insertBefore(star, reviewCountNode);
                        }
                        let halfStar = document.createElement('i');
                        halfStar.classList = 'fas fa-star-half-alt';
                        ratingNode.insertBefore(halfStar, reviewCountNode);
                    }
                }

                // 評價數
                reviewCountNode.innerText = space.Scores.length;

                // 簡介
                capacityNode.innerText = space.Capacity;
                minHourNode.innerText = space.MinHour;
                areaNode.innerText = space.MeasurementOfArea;

                // 設定輪播
                // const swiper = new Swiper('.swiper', {
                //   loop: true,
                //   pagination: {
                //     el: '.swiper-pagination',
                //     clickable: true,
                //   },
                //   navigation: {
                //     nextEl: '.swiper-button-next',
                //     prevEl: '.swiper-button-prev',
                //   },
                // });

                cardListNode.appendChild(templateClone);
            })
        }


    })
})();
