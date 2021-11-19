;
(function () {
  // ------主要節點
  const cardListNode = document.querySelector('.card-list');
  const spinnerRow = document.querySelector('.spinner-row');
  const logo = document.querySelector('footer .logo');
  // ------filter bar節點
  const cityOptionBarNode = document.querySelector('#web-city-filter');
  const districtOptionBarNode = document.querySelector('#web-district-filter');
  const typeOptionBarNode = document.querySelector('#web-type-filter');
  // ------filter modal節點
  const cityOptionModalNode = document.querySelector('#phone-city-filter');
  const districtOptionModalNode = document.querySelector('#phone-district-filter');
  const typeOptionModalNode = document.querySelector('#phone-type-filter');
  const lowPriceInputNode = document.querySelector('#low-price');
  const highPriceInputNode = document.querySelector('#high-price');
  const attendeeInputNode = document.querySelector('#attendee-filter');
  const areaInputNode = document.querySelector('#area-filter');
  const amenityOptionNode = document.querySelector('#amenity-filter');
  // ------搜尋條節點
  const searchingBar = document.querySelector('#web-search');
  const searchingBarBtn = document.querySelector('#web-search-btn');
  // ------更多條件節點
  const filterBtn = document.querySelector('#filter-btn');
  // ------全域變數: 後端傳回的filter資料
  let filterCityDistrictList, filterTypeList, filterAmenityList, filterAmenityIconList;
  // ------全域變數: 傳到後端的參數
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
    SentCount: 0,
    AddCount: 0,
  };
  let lastCount = 0;
  // 日曆中文化
  flatpickr.localize(flatpickr.l10ns.zh_tw);

  // ------functions定義
  // 檢查使用裝置
  function checkUsersDevice() {
    const mobileDevice = ['Android', 'webOS', 'iPhone', 'iPad', 'iPod']
    let isMobileDevice = mobileDevice.some(e => navigator.userAgent.match(e))
    if (isMobileDevice) {
      requestOfSpaceNum = 6;
      filter.AddCount = requestOfSpaceNum;
    } else {
      requestOfSpaceNum = 12;
      filter.AddCount = requestOfSpaceNum;
    }
  }

  // 加入placeholder
  function setPlaceholder() {
    for (i = 0; i <= 7; i++) {
      let placeholder = document.querySelector('#placeholder-template').content.cloneNode(true);
      cardListNode.appendChild(placeholder);
    }
  }

  // 移除placeholder
  function removePlaceholder() {
    let placeholders = document.querySelectorAll('.card.placeholder-wave');
    placeholders.forEach((placeholder) => {
      cardListNode.removeChild(placeholder);
    });
  }

  // 創建observer
  function createObserver() {
    const options = {
      root: null,
      rootMargin: '0px',
      threshold: 1.0,
    };

    const observer = new IntersectionObserver(entries => {
      // 取得相交狀態
      entries.forEach(item => {
        if (lastCount <= 0) {
          return;
        }
        if (item.isIntersecting && lastCount > 0) {
          spinnerRow.classList.remove('visually-hidden');
          setTimeout(() => {
            scrollForSpaces(filter);
          }, 1500);
        }
      });
    }, options);

    observer.observe(logo);
  }

  // 取得filter所需的資料
  function getFilterData() {
    let keywords = {};
    if (sessionStorage.getItem('filterVm')) {
      keywords = JSON.parse(sessionStorage.getItem('filterVm'));
    }
    let type = keywords.SelectedType || '';
    let city = keywords.SelectedCity || '';
    let date = keywords.SelectedDate || '';
    sessionStorage.clear();
    const getFilterUrl = `/webapi/spaces/GetFilterData?type=${type}&city=${city}&date=${date}`;

    axios.get(getFilterUrl).then(res => {
      let source = res.data.Response;
      // 取得從首頁傳來的篩選資料，若點選放大鏡進入則為空值
      filter.City = source.SelectedCity;
      filter.Type = source.SelectedType;
      filter.Date = source.SelectedDate;
      // 整理設定filter所需的資料
      filterCityDistrictList = source.CityDistrictDictionary;
      filterTypeList = source.SpaceTypeList;
      filterAmenityList = source.AmenityList;
      filterAmenityIconList = source.AmenityIconList;

      setBarAndModalFilter();
      filterForSpaces(filter);
      createObserver();
    }).catch(err => console.log(err))
  }

  // 設定日曆
  function setCalendar(idSelector = '') {
    flatpickr(idSelector, {
      altInput: true,
      altFormat: 'Y/m/d',
      disableMobile: 'true',
      defaultDate: filter.Date || 'today',
      minDate: 'today',
      maxDate: new Date().fp_incr(90),
      // change事件監聽
      onChange: function (selectedDates, dateStr, instance) {
        filter.Date = dateStr;
        // 如果是電腦版，立刻渲染場地
        if (this.input.id === 'web-date-filter') {
          filterForSpaces(filter);
        }
      },
    });
  }

  function setBarAndModalFilter() {
    // 電腦版
    setCityDistrictFilter(cityOptionBarNode, districtOptionBarNode);
    setTypeOption(typeOptionBarNode);
    setCalendar('#web-date-filter');
    const filterRow = document.querySelector('.filter-row-container');
    filterRow.classList.remove('invisible');
    // 手機版
    setCityDistrictFilter(cityOptionModalNode, districtOptionModalNode);
    setTypeOption(typeOptionModalNode);
    setCalendar('#phone-date-filter');
    // 關鍵字搜尋
    searchingBar.addEventListener('change', function () {
      filter.Keywords = this.value;
    })
    searchingBarBtn.addEventListener('click', function (e) {
      e.preventDefault();
      filterForSpaces(filter);
    })
    // 按Enter搜尋
    window.addEventListener('keyup', function (e) {
      if (e.key !== 'Enter') return;
      filterForSpaces(filter);
    });
    // 更多選項監聽事件
    filterBtn.addEventListener('click', setFilterBtn);
  }

  // 設置城市選項
  function setCityDistrictFilter(cityNode, districtNode) {
    // 取得城市列表
    let cities = Object.keys(filterCityDistrictList);

    // 設定城市選項
    let defaultCity = document.createElement('option');
    defaultCity.innerText = '選擇縣市';
    defaultCity.value = 'default';
    cityNode.appendChild(defaultCity);

    cities.forEach((city, index) => {
      let option = document.createElement('option');
      option.value = index + 1;
      option.innerText = city;
      cityNode.appendChild(option);
    });

    // 設定鄉鎮區預設選項
    let defaultDistrict = document.createElement('option');
    defaultDistrict.innerText = '鄉鎮區';
    defaultDistrict.setAttribute('selected', '');
    districtNode.appendChild(defaultDistrict);

    // 如果filter.City有值，該選項設為selected
    if (filter.City) {
      let indexOfSelectedCity = cities.indexOf(filter.City) + 1;
      cityNode.querySelector(`option[value="${indexOfSelectedCity}"]`).setAttribute('selected', '');
      // 設定對應鄉鎮區
      setDistrictOptions(districtNode);
    } else {
      // 若沒有，則預設為請選擇縣市，且把鄉鎮區選項鎖住
      cityNode.querySelector(`option[value="default"]`).setAttribute('selected', '');
      districtNode.setAttribute('disabled', '');
    }

    // 城市選項加入change事件監聽
    cityNode.addEventListener('change', function (e) {
      cityChangeHandler(e, districtNode);
    });
  }

  // 城市選項change事件委派
  function cityChangeHandler(e, districtNode) {
    // 取得選擇的城市名稱
    filter.City = e.target.querySelector(`option[value='${e.target.value}']`).innerText;

    // 解鎖鄉鎮區選單
    districtNode.innerHTML = '';
    districtNode.removeAttribute('disabled');

    // 如果選項選的是'選擇縣市'，則鎖住鄉鎮區選項
    if (filter.City === '選擇縣市') {
      filter.City = '';
      districtNode.setAttribute('disabled', '');
    }
    filter.District = '';
    // 如果是來自電腦版filter，立刻渲染畫面
    if (e.target.id === 'web-city-filter') {
      filterForSpaces(filter);
    }

    // 設定鄉鎮區選單
    let defaultOption = document.createElement('option');
    defaultOption.innerText = '選擇鄉鎮區';
    defaultOption.value = 'default';
    defaultOption.setAttribute('selected', '');
    districtNode.appendChild(defaultOption);

    // 設定對應鄉鎮區
    setDistrictOptions(districtNode);

    // 鄉鎮選項加入change事件監聽
    districtNode.addEventListener('change', districtChangeHandler);
  }

  function setDistrictOptions(districtNode) {
    // 設置選到城市的對應鄉鎮區
    if (filterCityDistrictList[filter.City]) {
      filterCityDistrictList[filter.City].forEach((district, index) => {
        let option = document.createElement('option');
        option.value = index + 1;
        option.innerText = district;
        districtNode.appendChild(option);
      })
    }
  }

  // 鄉鎮選項change事件委派
  function districtChangeHandler() {
    filter.District = this.querySelector(`option[value='${this.value}']`).innerText;
    if (filter.District === '選擇鄉鎮區') {
      filter.District = '';
    };
    // 如果是來自電腦版filter，立刻渲染畫面
    if (this.id === 'web-district-filter') {
      filterForSpaces(filter);
    }
  }

  // 設定類型選項
  function setTypeOption(typeNode) {
    let defaultType = document.createElement('option');
    defaultType.innerText = '場地類型';
    defaultType.value = 'default';
    defaultType.setAttribute('selected', '');
    typeNode.appendChild(defaultType);

    filterTypeList.forEach((type, index) => {
      let option = document.createElement('option');
      option.value = index + 1;
      option.innerText = type;
      if (type === filter.Type) {
        option.innerText = type;
        option.setAttribute('selected', '');
      }
      typeNode.appendChild(option);
    });

    // 設置類型事件監聽
    typeNode.addEventListener('change', typeChangeHandler);
  }

  // 類型選項change事件委派
  function typeChangeHandler(e) {
    // 渲染畫面
    filter.Type = e.target.querySelector(`option[value='${e.target.value}']`).innerText;
    if (filter.Type === '場地類型') {
      filter.Type = '';
    }
    // 如果是電腦版，立刻渲染畫面
    if (e.target.id === 'web-type-filter') {
      filterForSpaces(filter);
    }
  }

  // 更多選項click事件委派
  function setFilterBtn() {
    setModalInputs();
    setModalAmenity();
    // 清除按鈕
    document.querySelector('#filter-modal .clear-btn').addEventListener('click', clearModalFilter);
    // 確認按鈕
    document.querySelector('#filter-modal .save-btn').addEventListener('click', saveModalFilter);
  }

  // 設定modal內的所有input
  function setModalInputs() {
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

  // 設定modal內設施
  function setModalAmenity() {
    amenityOptionNode.innerHTML = '';

    filterAmenityList.forEach((amenity, index) => {
      let amenityClone = document.querySelector('#amenity-template').content.cloneNode(true);
      amenityClone.querySelector('img').setAttribute('src', `/Assets/IMG/${filterAmenityIconList[index]}`);
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
  }

  // 加上邊框
  function setBorder() {
    this.setAttribute('style', 'border: 2px solid #049DD9');
    this.addEventListener('click', removeBorder);
  }

  // 移除邊框
  function removeBorder() {
    this.removeAttribute('style');
    this.removeEventListener('click', removeBorder);
  }

  // 清除所有選項
  function clearModalFilter() {
    filter.HighPrice = '';
    filter.LowPrice = '';
    filter.Attendees = '';
    filter.Type = '';
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

    filterForSpaces(filter);
    bootstrap.Modal.getOrCreateInstance('#filter-modal').hide();
    document.querySelector('#filter-modal .save-btn').removeEventListener('click', saveModalFilter)
    this.removeEventListener('click', clearModalFilter)
  }

  // 確認所有選項
  function saveModalFilter() {
    filter.Amenities.length = 0;
    amenityOptionNode.querySelectorAll('.btn[style="border: 2px solid #049DD9"]').forEach(amenity => {
      filter.Amenities.push(amenity.innerText);
    });

    filterForSpaces(filter);
    bootstrap.Modal.getOrCreateInstance('#filter-modal').hide();
    document.querySelector('#filter-modal .clear-btn').removeEventListener('click', clearModalFilter)
    this.removeEventListener('click', saveModalFilter);
  }

  // 篩選時發出請求
  function filterForSpaces(filter = {}) {
    cardListNode.innerHTML = '';
    filter.SentCount = 0;
    setPlaceholder();

    setTimeout(() => {
      axios.post('/webapi/spaces/GetFilteredSpaces', filter)
        .then(res => {
          lastCount = +(res.data.Message);
          let spaceList = res.data.Response;
          removePlaceholder();

          if (!spaceList || spaceList.length === 0) {
            showNoResult();
          } else {
            renderSpaceCards(spaceList);
          }
        })
    }, 500);
  }

  // 滾動時發出請求
  function scrollForSpaces(filter = {}) {
    if (document.querySelector('.no-result-container')) {
      cardListNode.innerHTML = '';
    }

    filter.SentCount += requestOfSpaceNum;
    axios.post('/webapi/spaces/GetFilteredSpaces', filter).then(res => {
      lastCount = +(res.data.Message);
      let spaceList = res.data.Response;

      renderSpaceCards(spaceList);
      spinnerRow.classList.add('visually-hidden');
    }).catch(err => console.log(err))
  }

  // 渲染場地
  function renderSpaceCards(spaceList) {
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

      cardListNode.appendChild(templateClone);
    })
  }

  // 找不到場地
  function showNoResult() {
    cardListNode.innerHTML = '';
    let noResult = document.querySelector('#no-result-template').content.cloneNode(true);
    cardListNode.appendChild(noResult)
  }


  // ------執行區
  checkUsersDevice();
  setPlaceholder();
  getFilterData();

})();
