;
(function () {
  // -----flatpickr中文化
  flatpickr.localize(flatpickr.l10ns.zh_tw);

  // -----節點
  const orderDetailNode = document.querySelector('.order-detail-select');
  const extendDayBtn = document.querySelector('.extend');
  const removeDayBtn = document.querySelector('.remove');
  const submitBtn = document.querySelector('.btn-submit');
  const saveBtn = document.querySelector('.btn-save.btn');
  const heart = document.querySelector('.btn-save .fa-heart');
  const map = L.map('map', {
    center: [25.041824011585646, 121.53629849747963],
    zoom: 17
  });
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
    autoplay: {
      delay: 4000,
      disableOnInteraction: false
    },
  });

  // -----變數
  // let index = 1;
  // let whichCardIndex = 1;
  let cardIndex = 0;
  let operationStartArr = [];
  let operationEndArr = [];
  let operationDayArr = [];
  let minHour, discount, hoursForDiscount, pricePerHour, orderDateArr, capacity;
  let isCollection = false;
  let spaceID = '';
  let source = {};
  if (sessionStorage.getItem('theKey')) {
    spaceID = sessionStorage.getItem('theKey');
    sessionStorage.clear();
  }
  let preOrderObj = {
    DatesArr: [],
    AttendeesArr: [],
    StartTimeArr: [],
    EndTimeArr: [],
    spaceID: spaceID,
  }
  const getURL = `/webapi/spaces/GetBookingCardData?id=${spaceID}`;

  // -----函式
  // 取得資料並初始化資料
  function initialize() {
    axios.get(getURL).then(res => {
      source = res.data.Response;
      operationStartArr = source.StartTimeList;
      operationEndArr = source.EndTimeList;
      operationDayArr = source.OperatingDayList;
      operationDayArr.forEach(day => {
        if (day === 7) day = 0;
      });
      minHour = source.MinHour;
      discount = source.Discount;
      hoursForDiscount = source.HoursForDiscount;
      pricePerHour = source.PricePerHour;
      capacity = source.Capacity;
      isCollection = source.IsCollection;

      checkCollection();
      setCard();
    }).catch(err => console.log(err))
  }

  // 渲染地圖
  function setMap() {
    const osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    const osm = new L.TileLayer(osmUrl, {
      minZoom: 8,
      maxZoom: 19
    });
    map.addLayer(osm);
  }

  // 渲染地圖圖標
  function setMarker() {
    const marker = L.marker([25.041824011585646, 121.53629849747963]);
    const circle = L.circle([25.041824011585646, 121.53629849747963], {
      color: '#D9831A',
      fillColor: '#D9831A',
      fillOpacity: 0.5,
      radius: 100
    });
    marker.addTo(map);
    circle.addTo(map);
  }

  // 設定預約卡片
  function setCard() {
    const cloneNode = document.querySelector('#order-item-template').content.cloneNode(true);
    const dateNode = cloneNode.querySelector('.start-date');
    const attendeeNode = cloneNode.querySelector('.attendees');
    const startTimeNode = cloneNode.querySelector('.start-time');
    const endTimeNode = cloneNode.querySelector('.end-time');

    // let todayDay = 0;
    // let attendee = '';
    // let startTime = '';
    // let endTimeStartFrom = '';
    // let endTime = '';
    // let selectedDate = '';

    // 每張卡片加上編號
    cloneNode.querySelector('.order-item').classList.add(`index-${cardIndex}`);
    orderDetailNode.appendChild(cloneNode);
    setCalendar();
  }

  // 確認是否有被加為收藏
  function checkCollection() {
    if (isCollection) {
      heart.style.fontWeight = 900;
    } else {
      heart.style.fontWeight = 300;
    }
  }

  // 設定日曆
  function setCalendar() {
    // 
    let calendarMinDate;
    if (preOrderObj.DatesArr[index - 2]) {

    }

  }







})()