;
window.addEventListener('load', () => {
    // �D�n�`�I
    const cardListNode = document.querySelector('.card-list');

    // filter bar�`�I
    const cityOptionBarNode = document.querySelector('#web-city-filter');
    const districtOptionBarNode = document.querySelector('#web-district-filter');
    const typeOptionBarNode = document.querySelector('#web-type-filter');
    const searchingBar = document.querySelector('#web-search');
    const searchingBarBtn = document.querySelector('#web-search-btn');
    const filterBtn = document.querySelector('#filter-btn')

    // filter modal�`�I
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

    // ��䤤���
    flatpickr.localize(flatpickr.l10ns.zh_tw);

    // ��l��
    axios.get('https://localhost:44322/webapi/spaces/GetFilterData')
        .then(response => {
            filterOptions = response.data;
            console.log(filterOptions);
            // ��X��ݶǨӿz����
            selectedCity = filterOptions.SelectedCity;
            selectedType = filterOptions.SelectedType;
            selectedDate = filterOptions.SelectedDate;
            // �z�����
            cityDistrictList = filterOptions.CityDistrictDictionary;
            typeList = filterOptions.SpaceTypeList;
            amenityList = filterOptions.AmenityList;
            amenityIconList = filterOptions.AmenityIconList;

            // �]�wFilter
            setBarFilter();
            setModalFilter();

            // ��V���a�C��
            requestForSpaces();
        })
        .catch(error => console.log(error));


    function setBarFilter() {
        // �]�w���P�ƥ��ť
        setFlatpickr('#web-date-filter');
        // ��w�m���Ͽ��
        disableDistrictOption(districtOptionBarNode);
        // �]�w�������P�ƥ��ť
        setCityAndDistrictOption(cityOptionBarNode, districtOptionBarNode);
        // �]�w�������P�ƥ��ť
        setTypeOption(typeOptionBarNode);
        // �]�w����r�ƥ��ť
        setSearchBar(searchingBar);
        searchingBarBtn.addEventListener('click', keywordSearch);
    }

    function setModalFilter() {
        // �]�w���P�ƥ��ť
        setFlatpickr('#phone-date-filter');
        // ��w�m���Ͽ��
        disableDistrictOption(districtOptionModalNode);
        // �]�w�������P�ƥ��ť
        setCityAndDistrictOption(cityOptionModalNode, districtOptionModalNode);
        // �]�w�������P�ƥ��ť
        setTypeOption(typeOptionModalNode);
        // �]�wModal����L�ﶵ�P�ƥ��ť
        filterBtn.addEventListener('click', setModalOptionAndEvent);
        // �]�w����r�ƥ��ť
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
                // change�ƥ��ť
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
                // change�ƥ��ť
                onChange: function (selectedDates, dateStr, instance) {
                    selectedDate = dateStr;
                    requestForSpaces();
                },
            });
        }
    }

    function disableDistrictOption(districtNode) {
        let defaultOption = document.createElement('option');
        defaultOption.innerText = '�m����';
        defaultOption.setAttribute('selected', '');
        districtNode.appendChild(defaultOption);

        districtNode.setAttribute('disabled', '');
    };

    function setCityAndDistrictOption(cityNode, districtNode) {
        let cities = Object.keys(cityDistrictList);

        if (selectedCity.length === 0) {
            let defaultOption = document.createElement('option');
            defaultOption.innerText = '��ܿ���';
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
            // ��V�e��
            selectedCity = this.querySelector(`option[value='${this.value}']`).innerText;
            selectedDistrict = '';
            requestForSpaces();

            // �]�w�m���Ͽ��
            districtNode.innerHTML = '';
            districtNode.removeAttribute('disabled');

            let defaultOption = document.createElement('option');
            defaultOption.innerText = '��ܶm����';
            defaultOption.setAttribute('selected', '');
            districtNode.appendChild(defaultOption);

            cityDistrictList[selectedCity].forEach((district, index) => {
                let option = document.createElement('option');
                option.value = index + 1;
                option.innerText = district;
                districtNode.appendChild(option);
            })
        })

        districtNode.addEventListener('change', function () {
            selectedDistrict = this.querySelector(`option[value='${this.value}']`).innerText;
            requestForSpaces();
        });
    }

    function setTypeOption(typeNode) {
        if (selectedType.length === 0) {
            let defaultOption = document.createElement('option');
            defaultOption.innerText = '���a����';
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
            // ��V�e��
            selectedType = this.querySelector(`option[value='${this.value}']`).innerText;
            requestForSpaces();
        })
    }

    function setModalOptionAndEvent() {
        // �]�w�w�]input���e�P�ƥ��ť
        setInputs();
        // �]�w�]�I���
        setAmenity();
        // �]�wModal�����󪺺�ť�ƥ�
        setBtnClickEvent();
        // �]�wkeyword��ť


        function setInputs() {
            let nodeArr = [lowPriceInputNode, highPriceInputNode, attendeeInputNode, areaInputNode];
            let valueArr = [lowBudget, highBudget, attendees, area];

            // �]�w�w�]��
            nodeArr.forEach((node, index) => {
                node.value = '';
                if (valueArr[index]) {
                    node.value = valueArr[index];
                }
            })

            // �]�w�ƥ��ť
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
                amenityClone.querySelector('img').setAttribute('src', `../Assets/IMG/${amenityIconList[index]}`);
                amenityClone.querySelector('span').innerText = amenity;
                amenityOptionNode.appendChild(amenityClone);
            })

            // �]�w�w�]��
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
            // �M��
            document.querySelector('#filter-modal .clear-btn').addEventListener('click', function () {
                highBudget = '';
                lowBudget = '';
                attendees = '';
                area = '';
                amenities = [];

                requestForSpaces();
            })
            // �T�{
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

            // �W�s��
            venueLink.href = `/Booking/BookingPage/${space.SpaceID}`;

            // �Ϥ�
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

            // ����
            pricePerHourNode.innerText = `NT$${space.PricePerHour}`;

            // �W��
            venueNameNode.innerText = space.SpaceName;

            // �a�}
            let address = document.createElement('span');
            address.classList = 'ms-1'
            address.innerText = `${space.City} ${space.District} ${space.Address}`;
            venueAddressNode.appendChild(address);

            // ����
            if (space.Scores.length === 0) {
                let blank = document.createElement('span');
                blank.innerText = '�ثe�S���������';
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

            // ������
            reviewCountNode.innerText = space.Scores.length;

            // ²��
            capacityNode.innerText = space.Capacity;
            minHourNode.innerText = space.MinHour;
            areaNode.innerText = space.MeasurementOfArea;

            // �]�w����
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