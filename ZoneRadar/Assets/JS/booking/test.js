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
  let cardIndex = 0;
  let whichCardIndex = 0;
  let operationStartArr = [];
  let operationEndArr = [];
  let operationDayArr = [];
  let minHour, discount, hoursForDiscount, pricePerHour, orderDateArr, capacity, isCollection;
  let dayOfSelectedDate, attendee, startTime, endTimeStartFrom, endTime, selectedDate;
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
    SpaceID: spaceID,
    HoursForDiscount: 0,
    Discount: 0,
  }
  const getURL = `/webapi/spaces/GetBookingCardData?id=${spaceID}`;
  const dateFormat = 'YYYY-MM-DD';
  const timeFormat = 'HH:mm'

  // -----函式
  // 取得資料並初始化資料
  function initialize() {
    axios.get(getURL).then(res => {
      source = res.data.Response;
      // 營業起始時間
      operationStartArr = source.StartTimeList;
      // 營業結束時間
      operationEndArr = source.EndTimeList;
      // 星期幾有營業
      operationDayArr = source.OperatingDayList;
      operationDayArr.forEach(day => {
        if (day === 7) day = 0;
      });

      minHour = source.MinHour;
      discount = source.Discount;
      orderDateArr = source.OrderTimeList;
      hoursForDiscount = source.HoursForDiscount;
      pricePerHour = source.PricePerHour;
      capacity = source.Capacity;
      isCollection = source.IsCollection;
      preOrderObj.HoursForDiscount = hoursForDiscount;
      preOrderObj.Discount = discount;

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

  // 確認是否有被加為收藏
  function checkCollection() {
    if (isCollection) {
      heart.style.fontWeight = 900;
    } else {
      heart.style.fontWeight = 300;
    }
  }

  // 設定預約卡片
  function setCard() {
    const cloneNode = document.querySelector('#order-item-template').content.cloneNode(true);
    const dateNode = cloneNode.querySelector('.start-date');
    const attendeeNode = cloneNode.querySelector('.attendees');
    const startTimeNode = cloneNode.querySelector('.start-time');
    const endTimeNode = cloneNode.querySelector('.end-time');



    // 每張卡片加上編號
    cloneNode.querySelector('.order-item').classList.add(`index-${cardIndex}`);
    orderDetailNode.appendChild(cloneNode);
    setCalendar(dateNode, attendeeNode);
    setAttendee(attendeeNode, startTimeNode, endTimeNode);
  }

  // 設定日曆
  function setCalendar(dateNode, attendeeNode) {
    // 如果preOrderObj裡沒有值，日曆第一天為今天，若有值，第一天為前一次預訂 + 1天
    let calendarMinDate = 'today';
    if (preOrderObj.DatesArr[cardIndex - 1]) {
      let datAfter = dayjs(preOrderObj.DatesArr[cardIndex - 1]).add(1, 'day');
      calendarMinDate = datAfter.formate(dateFormat);
    }

    //建立日曆實體
    flatpickr(dateNode, {
      altInput: true,
      altFormat: 'Y/m/d',
      disableMobile: 'true',
      minDate: calendarMinDate,
      maxDate: new Date().fp_incr(90),
      "disable": [
        // disable掉有被預訂和沒有營業的日期
        function (date) {
          return (operationDayArr.indexOf(date.getDay()) === -1 || orderDateArr.indexOf(dayjs(date).format(dateFormat)) !== -1);
        }
      ],
      onChange: function (selectedDates, dateStr, instance) {
        // 暫存今天所選的日期
        selectedDate = dateStr;
        // 確認並暫存選到日期是星期幾
        dayOfSelectedDate = dayjs(dateStr).day();
        // 開啟人數選項
        attendeeNode.removeAttribute('disabled');
        // input來自第幾幾張卡片
        let whichCard = instance.input.parentNode.parentNode.classList[2];
        whichCardIndex = whichCard.split('-')[1];
        // 確認是否開放加一天
        inputGroups = [selectedDate, attendee, startTime, endTime];
        checkExtendDayBtn(inputGroups, whichCardIndex);
      },
    });
  }

  // 設定人數選項
  function setAttendee(attendeeNode, startTimeNode, endTimeNode) {
    attendeeNode.addEventListener('change', function (e) {
      // 只能輸入數字
      const reg = /^[0-9]+(\.[0-9]{1,3})?$/;
      // 如果人數 > 50，數字顯示50
      if (this.value > capacity) this.value = capacity;
      // 暫存參加人數
      attendee = this.value;
      let whichCard = e.target.parentNode.parentNode.classList[2];
      whichCardIndex = whichCard.split('-')[1];
      // 開啟開始時間選項
      startTimeNode.removeAttribute('disabled');
      setStartTime(startTimeNode, endTimeNode);
      // 確認是否開放加一天
      inputGroups = [selectedDate, attendee, startTime, endTime];
      checkExtendDayBtn(inputGroups);
    })
  }

  // 設定起始時間選項
  function setStartTime(startTimeNode, endTimeNode) {
    // 選擇日星期在營業星期arr中的index
    let dayIndex = operationDayArr.indexOf(dayOfSelectedDate);
    // 最早為當天最早營業時間
    let todayMinTime = operationStartArr[dayIndex];
    // 最晚為當天最晚營業時間 - 最少預定時數
    let todayMaxTime = dayjs(`${selectedDate} ${operationEndArr[dayIndex]}`).subtract(minHour, 'hour').format(timeFormat);

    flatpickr(startTimeNode, {
      enableTime: true,
      noCalendar: true,
      dateFormat: "H:i",
      time_24hr: true,
      minuteIncrement: 30,
      minTime: todayMinTime,
      maxTime: todayMaxTime,
      defaultDate: operationStartArr[dayIndex],
      disableMobile: "true",
      onReady: function (selectedDates, dateStr, instance) {
        // 暫存預設的起始時間
        startTime = dateStr;
        // 值來自第幾幾張卡片
        let whichCard = instance.input.parentNode.parentNode.classList[2];
        whichCardIndex = whichCard.split('-')[1];
        // 設定結束時間
        setEndTime(endTimeNode);
        endTimeNode.removeAttribute('disabled');
        // 確認是否開放加一天
        inputGroups = [selectedDate, attendee, startTime, endTime];
        checkExtendDayBtn(inputGroups);
      },
      onChange: function (selectedDates, dateStr, instance) {
        // 暫存所選開始時間
        startTime = dateStr;
        // 變動來自第幾幾張卡片
        let whichCard = instance.input.parentNode.parentNode.classList[2];
        whichCardIndex = whichCard.split('-')[1];
        // 設定結束時間
        setEndTime(endTimeNode);
        // 確認是否開放加一天
        inputGroups = [selectedDate, attendee, startTime, endTime];
        checkExtendDayBtn(inputGroups);
      },
    });
  }

  // 設定結束時間選項
  function setEndTime(endTimeNode) {
    // 選擇日星期在營業星期arr中的index
    let dayIndex = operationDayArr.indexOf(dayOfSelectedDate);
    // 最早為選擇起始時間 + 最少預定時數
    let todayMinTime = dayjs(`${selectedDate} ${startTime}`).add(minHour, 'hour').format(timeFormat);
    // 最晚為當天最晚營業時間
    let todayMaxTime = operationEndArr[dayIndex];

    // 結束時間
    flatpickr(endTimeNode, {
      enableTime: true,
      noCalendar: true,
      dateFormat: "H:i",
      time_24hr: true,
      minuteIncrement: 30,
      minTime: todayMinTime,
      maxTime: todayMaxTime,
      defaultDate: todayMinTime,
      disableMobile: "true",
      onReady: function (selectedDates, dateStr, instance) {
        // 暫存結束時間
        endTime = dateStr;
        // 值來自第幾幾張卡片
        let whichCard = instance.input.parentNode.parentNode.classList[2];
        whichCardIndex = whichCard.split('-')[1];
        // 確認是否開放加一天
        inputGroups = [selectedDate, attendee, startTime, endTime];
        checkExtendDayBtn(inputGroups);
      },
      onChange: function (selectedDates, dateStr, instance) {
        // 暫存結束時間
        endTime = dateStr;
        // 值來自第幾幾張卡片
        let whichCard = instance.input.parentNode.parentNode.classList[2];
        whichCardIndex = whichCard.split('-')[1];
        // 確認是否開放加一天
        inputGroups = [selectedDate, attendee, startTime, endTime];
        checkExtendDayBtn(inputGroups);
      },
    });
  }

  function checkExtendDayBtn(inputGroups) {
    let trueArr = [];
    // 判斷傳入的判斷陣列是否都有值
    inputGroups.forEach(item => {
      if (item) {
        trueArr.push(true);
      }
    })

    // 如果trueArr的長度為四
    if (trueArr.length === 4) {
      // 打開加一天和送出鈕
      extendDayBtn.removeAttribute('disabled');
      submitBtn.removeAttribute('disabled');
      // 儲存日期、人數和時間到preOrderObj物件裡
      preOrderObj.DatesArr[whichCardIndex] = inputGroups[0];
      preOrderObj.AttendeesArr[whichCardIndex] = inputGroups[1];
      preOrderObj.StartTimeArr[whichCardIndex] = inputGroups[2];
      preOrderObj.EndTimeArr[whichCardIndex] = inputGroups[3];

      calculate(preOrderObj);
    }
  }

  function calculate(preOrderObj) {
    console.log(preOrderObj);
    const calculationNode = document.querySelector('.booking-card .calculation');
    const priceNode = calculationNode.querySelector('.venue-price');
    const totalHourNode = calculationNode.querySelector('.total-hour');
    const discountNode = calculationNode.querySelector('.discount');
    const discountRowNode = calculationNode.querySelector('.discount-row');
    const totalCostNode = calculationNode.querySelector('.total-price');

    axios.post('')
  }

  
  initialize();




})()