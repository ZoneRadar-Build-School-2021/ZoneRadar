;
(function () {
  // flatpickr中文化
  flatpickr.localize(flatpickr.l10ns.zh_tw);
  // 全域變數
  let index = 1;
  let operationStartArr = [];
  let operationEndArr = [];
  let operationDayArr = [];
  let minHour, discount, hoursForDiscount, pricePerHour, orderDateArr;
  let preOrderObj = {
    DatesArr: [],
    AttendeesArr: [],
    StartTimeArr: [],
    EndTimeArr: []
  }
  let spaceID = '';
  let whichCardIndex = 1;
  // 抓取session的場地ID
  if (sessionStorage.getItem('theKey')) {
    spaceID = sessionStorage.getItem('theKey');
    sessionStorage.clear();
  }
  const getURL = `https://localhost:44322/webapi/spaces/GetBookingCardData?id=${spaceID}`;
  // 節點
  const orderDetailNode = document.querySelector('.order-detail-select');
  const extendDayBtn = document.querySelector('.extend');
  const removeDayBtn = document.querySelector('.remove');
  const submitBtn = document.querySelector('.btn-submit');
  const map = L.map('map', {
    center: [25.041824011585646, 121.53629849747963],
    zoom: 17
  });

  // 執行區
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
  setMap();
  setMarker();
  setCard();
  extendDayBtn.addEventListener('click', extendADay);
  removeDayBtn.addEventListener('click', removeADay);
  submitBtn.addEventListener('click', submitOrder);

  // functions定義
  function setMap() {
    const osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    const osm = new L.TileLayer(osmUrl, {
      minZoom: 8,
      maxZoom: 19
    });
    map.addLayer(osm);
  }

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

  function setCard() {
    axios.get(getURL).then(res => {
      const source = res.data;
      const cloneNode = document.querySelector('#order-item-template').content.cloneNode(true);
      const dateNode = cloneNode.querySelector('.start-date');
      const attendeeNode = cloneNode.querySelector('.attendees');
      const startTimeNode = cloneNode.querySelector('.start-time');
      const endTimeNode = cloneNode.querySelector('.end-time');
      minHour = source.MinHour;
      discount = source.Discount;
      hoursForDiscount = source.HoursForDiscount;
      pricePerHour = source.PricePerHour;
      let validation = [];
      let todayDay = 0;
      let attendee = '';
      let startTime = '';
      let endTimeStartFrom = '';
      let endTime = '';
      let selectedDate = '';

      // 執行區
      getOperation();
      setCalendarAndTime();
      setAttendee();
      cloneNode.querySelector('.order-item').classList.add(`day-${index}`);
      orderDetailNode.appendChild(cloneNode);

      // functions定義
      function getOperation() {
        operationStartArr = source.StartTimeList;
        operationEndArr = source.EndTimeList;
        operationDayArr = source.OperatingDayList;
        operationDayArr.forEach(day => {
          if (day === 7) day = 0;
        });
      }

      function setCalendarAndTime() {
        orderDateArr = source.OrderTimeList.map(date => new Date(`${date} 00:00:00`).getTime());
        let firstDay = 'today';
        if (preOrderObj.DatesArr[index - 2]) {
          let temp = preOrderObj.DatesArr[index - 2];
          let tempDate = new Date(temp);
          let tempDatePlus = tempDate.setDate(tempDate.getDate() + 1);
          firstDay = new Date(tempDatePlus);
        }

        // 設定日曆
        flatpickr(dateNode, {
          altInput: true,
          altFormat: 'Y/m/d',
          disableMobile: 'true',
          minDate: firstDay,
          maxDate: new Date().fp_incr(90),
          "disable": [
            function (date) {
              return (operationDayArr.indexOf(date.getDay()) === -1 || orderDateArr.indexOf(date.getTime()) !== -1);
            }
          ],
          // change事件監聽
          onChange: function (selectedDates, dateStr, instance) {
            // 確認選到日期是星期幾
            todayDay = new Date(dateStr).getDay();
            // 暫存今天所選的日期
            selectedDate = dateStr;
            // 設定時間
            setTime();
            // 值來自第幾幾張卡片
            let whichCard = instance.input.parentNode.parentNode.classList[2];
            whichCardIndex = whichCard.split('-')[1];
            // 確認是否合規
            validation = [selectedDate, attendee, startTime, endTime];
            checkValidation(validation);
            // 開啟人數選項
            attendeeNode.removeAttribute('disabled');
          },
        });
      }

      function setTime() {
        let dayIndex = operationDayArr.indexOf(todayDay);
        // 結束時間string to DateTime
        let todayMaxTime = new Date(`${selectedDate} ${operationEndArr[dayIndex]}`);
        // 結束時間為最晚營業時間 - 最少預定時間
        let todayMaxHours = '0' + (todayMaxTime.getHours() - minHour);
        let todayMaxMinutes = '0' + todayMaxTime.getMinutes();
        let maxStartTime = `${todayMaxHours.substr(-2)}:${todayMaxMinutes.substr(-2)}`;

        setStartTime();

        function setStartTime() {
          // 開始時間
          flatpickr(startTimeNode, {
            enableTime: true,
            noCalendar: true,
            dateFormat: "H:i",
            time_24hr: true,
            minuteIncrement: 30,
            minTime: operationStartArr[dayIndex],
            maxTime: maxStartTime,
            defaultDate: operationStartArr[dayIndex],
            disableMobile: "true",
            onValueUpdate: function (selectedDates, dateStr, instance) {
              // 選擇的開始時間string to DateTime
              let todayDateTime = new Date(`${selectedDate} ${dateStr}`)
              // 結束時間點起始點為選擇的開始時間 + 最少預定時數
              let timeStamp = todayDateTime.getTime() + (minHour * 3600000);
              let newDate = new Date(timeStamp);
              let hours = '0' + newDate.getHours();
              let minutes = '0' + newDate.getMinutes();
              endTimeStartFrom = `${hours.substr(-2)}:${minutes.substr(-2)}`;
              // 暫存所選開始時間
              startTime = dateStr;
              // 值來自第幾幾張卡片
              let whichCard = instance.input.parentNode.parentNode.classList[2];
              whichCardIndex = whichCard.split('-')[1];
              // 設定結束時間
              endTimeNode.removeAttribute('disabled');
              setEndTime();
              // validation = [selectedDate, attendee, startTime, endTime];
              // checkValidation(validation);
            },
          });
        }

        function setEndTime() {
          // 結束時間
          flatpickr(endTimeNode, {
            enableTime: true,
            noCalendar: true,
            dateFormat: "H:i",
            time_24hr: true,
            minuteIncrement: 30,
            minTime: endTimeStartFrom,
            maxTime: operationEndArr[dayIndex],
            defaultDate: endTimeStartFrom,
            disableMobile: "true",
            onValueUpdate: function (selectedDates, dateStr, instance) {
              // 暫存結束時間
              endTime = dateStr;
              // 值來自第幾幾張卡片
              let whichCard = instance.input.parentNode.parentNode.classList[2];
              whichCardIndex = whichCard.split('-')[1];
              validation = [selectedDate, attendee, startTime, endTime];
              checkValidation(validation);
            },
          });
        }

      }

      function setAttendee() {
        attendeeNode.addEventListener('keyup', function () {
          this.value = this.value.replace(/[^\d]/g, '');
        })

        attendeeNode.addEventListener('change', function (e) {
          // 暫存參加人數
          attendee = this.value;
          validation = [selectedDate, attendee, startTime, endTime];
          let whichCard = e.target.parentNode.parentNode.classList[2];
          let whichCardIndex = whichCard.split('-')[1];
          checkValidation(validation, whichCardIndex);
          startTimeNode.removeAttribute('disabled');
        })
      }

      function checkValidation(validation) {
        // let trueArr = [];
        // // 判斷傳入的判斷陣列是否都有值
        // validation.forEach(item => {
        //   if (item) {
        //     trueArr.push(true);
        //   }
        // })

        // 如果trueArr的長度為四
        if (validation.length === 4) {
          // 打開加一天和送出鈕
          extendDayBtn.removeAttribute('disabled');
          submitBtn.removeAttribute('disabled');
          // 儲存日期、人數和時間到preOrderObj物件裡
          preOrderObj.DatesArr[whichCardIndex - 1] = validation[0];
          preOrderObj.AttendeesArr[whichCardIndex - 1] = validation[1];
          preOrderObj.StartTimeArr[whichCardIndex - 1] = validation[2];
          preOrderObj.EndTimeArr[whichCardIndex - 1] = validation[3];

          calculate(preOrderObj);
        }
      }
    })
  };

  function calculate(preOrderObj) {
    console.log(preOrderObj);
    const calculationNode = document.querySelector('.booking-card .calculation');
    const priceNode = calculationNode.querySelector('.venue-price');
    const totalHourNode = calculationNode.querySelector('.total-hour');
    const discountNode = calculationNode.querySelector('.discount');
    const discountRowNode = calculationNode.querySelector('.discount-row');
    const totalCostNode = calculationNode.querySelector('.total-price');
    let totalHour = 0;
    let totalCost = 0;

    calculationNode.classList = 'calculation';

    preOrderObj.StartTimeArr.forEach((time, index) => {
      let startTime = new Date(`${preOrderObj.DatesArr[index]} ${time}`).getTime();
      let endTime = new Date(`${preOrderObj.DatesArr[index]} ${preOrderObj.EndTimeArr[index]}`).getTime();
      // 算出當天的小時數
      let hourDiff = (endTime - startTime) / 3600000;
      totalHour += hourDiff;
      // 小時 * 單價
      let cost = hourDiff * pricePerHour;
      totalCost += cost;
    })

    // 如果超過指定時數則享有優惠
    if (totalHour >= hoursForDiscount) {
      totalCost = Math.floor(totalCost * discount);
      discountRowNode.classList = 'discount-row d-flex mb-2'
    }

    priceNode.innerText = `NT$${pricePerHour.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}`;
    totalHourNode.innerText = totalHour;
    discountNode.innerText = discount;
    totalCostNode.innerText = `NT$${totalCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}`;
  }

  function extendADay() {
    index++;

    this.setAttribute('disabled', '');
    removeDayBtn.classList.remove('d-none');
    setCard();
  }

  function removeADay() {
    let latest = document.querySelector(`.day-${index}`);
    orderDetailNode.removeChild(latest);

    preOrderObj.DatesArr.length = index - 1;
    preOrderObj.AttendeesArr.length = index - 1;
    preOrderObj.StartTimeArr.length = index - 1;
    preOrderObj.EndTimeArr.length = index - 1;

    index--;
    if (index === 1) {
      this.classList.add('d-none');
      extendDayBtn.removeAttribute('disabled');
    }

    calculate(preOrderObj);
  }

  function submitOrder() {
    axios.get('https://localhost:44322/webapi/spaces/CheckLogin').then(res => console.log(res.data))

    // Swal.fire(
    //   '預約成功!',
    //   '請於24小時內前往會員中心 > 我的訂單內申請付款',
    //   'success'
    // )
  }

})()