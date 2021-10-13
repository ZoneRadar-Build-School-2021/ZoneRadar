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
        const searchingModal = document.querySelector('#phone-search');
        const searchingModalBtn = document.querySelector('#phone-search-btn');

        // Global Variables
        let filterOptions, selectedCity, selectedDistrict, selectedType, selectedDate;
        let highBudget, lowBudget, attendees, area, keywords;
        let amenities = [];
        let filter = {
            City: selectedCity,
            District: selectedDistrict,
            Type: selectedType,
            Date: selectedDate,
            HighPrice: highBudget,
            LowPrice: lowBudget,
            Attendees: attendees,
            Amenities: amenities,
            Area: area,
            Keywords: keywords
        };
        let cityDistrictList, typeList, amenityList, amenityIconList;

        // 日曆中文化
        flatpickr.localize(flatpickr.l10ns.zh_tw);

        // 初始化
        axios.get('https://localhost:44322/webapi/spaces/GetFilterData')
            .then(response => {
                filterOptions = response.data;
                // 抓出後端傳來篩選資料
                selectedCity = filterOptions.SelectedCity;
                selectedType = filterOptions.SelectedType;
                selectedDate = filterOptions.SelectedDate;
                // 篩選分組
                cityDistrictList = filterOptions.CityDistrictDictionary;
                typeList = filterOptions.SpaceTypeList;
                amenityList = filterOptions.AmenityList;
                amenityIconList = filterOptions.AmenityIconList;

                // 設定Filter
                setBarFilter();
                setModalFilter();

                // 渲染場地列表
                requestForSpaces();
            })
            .catch(error => console.log(error));


        function setBarFilter() {
            // 設定日曆與事件監聽
            setFlatpickr('#web-date-filter');
            // 鎖定鄉鎮區選單
            disableDistrictOption(districtOptionBarNode);
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
            disableDistrictOption(districtOptionModalNode);
            // 設定縣市選單與事件監聽
            setCityAndDistrictOption(cityOptionModalNode, districtOptionModalNode);
            // 設定類型選單與事件監聽
            setTypeOption(typeOptionModalNode);
            // 設定Modal內其他選項與事件監聽
            filterBtn.addEventListener('click', setModalOptionAndEvent);
            // 設定關鍵字事件監聽
            setSearchBar(searchingModal);
            searchingModalBtn.addEventListener('click', keywordSearch);
        }

        function setFlatpickr(dateNode) {
            if (selectedDate) {
                flatpickr(dateNode, {
                    altInput: true,
                    altFormat: 'Y/m/d',
                    disableMobile: 'true',
                    defaultDate: selectedDate,
                    minDate: "today",
                    maxDate: new Date().fp_incr(60),
                    // change事件監聽
                    onChange: function (selectedDates, dateStr, instance) {
                        selectedDate = dateStr;
                        requestForSpaces();
                    },
                });
            } else {
                flatpickr(dateNode, {
                    altInput: true,
                    altFormat: 'Y/m/d',
                    disableMobile: 'true',
                    minDate: "today",
                    maxDate: new Date().fp_incr(60),
                    // change事件監聽
                    onChange: function (selectedDates, dateStr, instance) {
                        selectedDate = dateStr;
                        requestForSpaces();
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

            if (selectedCity.length === 0) {
                let defaultOption = document.createElement('option');
                defaultOption.innerText = '選擇縣市';
                defaultOption.value = 'default';
                defaultOption.setAttribute('selected', '');
                cityNode.appendChild(defaultOption);
            }

            cities.forEach((city, index) => {
                let option = document.createElement('option');
                option.value = index + 1;
                if (city === selectedCity) {
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
                selectedCity = this.querySelector(`option[value='${this.value}']`).innerText;
                if (selectedCity === '選擇縣市') {
                    selectedCity = '';
                    districtNode.setAttribute('disabled', '');
                }
                selectedDistrict = '';
                requestForSpaces();

                // 設定鄉鎮區選單
                let defaultOption = document.createElement('option');
                defaultOption.innerText = '選擇鄉鎮區';
                defaultOption.value = 'default';
                defaultOption.setAttribute('selected', '');
                districtNode.appendChild(defaultOption);

                if (cityDistrictList[selectedCity] !== undefined) {
                    cityDistrictList[selectedCity].forEach((district, index) => {
                        let option = document.createElement('option');
                        option.value = index + 1;
                        option.innerText = district;
                        districtNode.appendChild(option);
                    })
                }
            })

            districtNode.addEventListener('change', function () {
                selectedDistrict = this.querySelector(`option[value='${this.value}']`).innerText;
                console.log(this.value);
                if (selectedDistrict === '選擇鄉鎮區') {
                    selectedDistrict = '';
                }
                requestForSpaces();
            });
        }

        function setTypeOption(typeNode) {
            if (selectedType.length === 0) {
                let defaultOption = document.createElement('option');
                defaultOption.innerText = '場地類型';
                defaultOption.setAttribute('selected', '');
                typeNode.appendChild(defaultOption);
            }

            typeList.forEach((type, index) => {
                let option = document.createElement('option');
                option.value = index + 1;
                option.innerText = type;
                if (type === selectedType) {
                    option.innerText = type;
                    option.setAttribute('selected', '');
                }
                typeNode.appendChild(option);
            });

            typeNode.addEventListener('change', function () {
                // 渲染畫面
                selectedType = this.querySelector(`option[value='${this.value}']`).innerText;
                requestForSpaces();
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
                let valueArr = [lowBudget, highBudget, attendees, area];

                // 設定預設值
                nodeArr.forEach((node, index) => {
                    node.value = '';
                    if (valueArr[index]) {
                        node.value = valueArr[index];
                    }
                })

                // 設定事件監聽
                valueArr.forEach((value, index) => {
                    nodeArr[index].addEventListener('change', function () {
                        value = this.value;
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
                if (amenities.length !== 0) {
                    amenityOptionNode.querySelectorAll('.btn').forEach(node => {
                        amenities.forEach(item => {
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
                document.querySelector('#filter-modal .clear-btn').addEventListener('click', function () {
                    highBudget = '';
                    lowBudget = '';
                    attendees = '';
                    area = '';
                    amenities = [];

                    requestForSpaces();
                })
                // 確認
                document.querySelector('#filter-modal .save-btn').addEventListener('click', function () {
                    amenities = [];
                    lowBudget = lowPriceInputNode.value;
                    highBudget = highPriceInputNode.value;
                    attendees = attendeeInputNode.value;
                    area = areaInputNode.value;
                    amenityOptionNode.querySelectorAll('.btn[style="border: 2px solid #049DD9"]').forEach(amenity => {
                        amenities.push(amenity.innerText);
                    });
                    bootstrap.Modal.getOrCreateInstance('#filter-modal').hide();
                    requestForSpaces();
                })
            }
        }

        function setSearchBar(node) {
            node.addEventListener('change', function () {
                keywords = this.value;
            })

            selectedCity = '';
            selectedDistrict = '';
            selectedType = '';
            selectedDate = '';
            highBudget = '';
            lowBudget = '';
            attendees = '';
            amenities = '';
            area = '';
        }

        function keywordSearch(e) {
            e.preventDefault();
            requestForSpaces();
        }

        function keywordSearchEnter(e) {
            if (e.key !== 'Enter') return;
            requestForSpaces();
        }

        function requestForSpaces() {
            setPlaceholder();

            filter = {
                City: selectedCity,
                District: selectedDistrict,
                Type: selectedType,
                Date: selectedDate,
                HighPrice: highBudget,
                LowPrice: lowBudget,
                Attendees: attendees,
                Amenities: amenities,
                Area: area,
                Keywords: keywords
            }

            if (filter.Keywords) {
                filter = {
                    City: '',
                    District: '',
                    Type: '',
                    Date: '',
                    HighPrice: '',
                    LowPrice: '',
                    Attendees: '',
                    Amenities: '',
                    Area: '',
                    Keywords: keywords
                }
            }

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
