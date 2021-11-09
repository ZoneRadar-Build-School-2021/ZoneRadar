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
    const login_modal = document.querySelector("#login-modal");
    const modal = bootstrap.Modal.getOrCreateInstance(login_modal);

    // -----變數
    let cardIndex = 0;
    let whichCardIndex = 0;
    let operationStartArr = [];
    let operationEndArr = [];
    let operationDayArr = [];
    let minHour, discount, hoursForDiscount, pricePerHour, orderDateArr, capacity, isCollection;
    let dayOfSelectedDate, attendee, startTime, endTimeStartFrom, endTime, selectedDate;
    let lat, lng;
    let spaceID = '';
    let source = {};
    let isLogin;
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
        PricePerHour: 0,
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
            source.OperatingDayList.forEach(day => {
                if (day === 7) day = 0;
                operationDayArr.push(day);
            });

            minHour = source.MinHour;
            discount = source.Discount;
            orderDateArr = source.OrderTimeList;
            hoursForDiscount = source.HoursForDiscount;
            pricePerHour = source.PricePerHour;
            capacity = source.Capacity;
            isCollection = source.IsCollection;
            lat = +(source.Latitude);
            lng = +(source.Longitude);
            // 更新preOrderObj屬性
            preOrderObj.HoursForDiscount = hoursForDiscount;
            preOrderObj.Discount = discount;
            preOrderObj.PricePerHour = pricePerHour;

            const map = L.map('map', {
                center: [lat, lng],
                zoom: 17
            });
            renderCollection();
            setCard();
            setMap(map);
            setMarker(map);
            extendDayBtn.addEventListener('click', extendADay);
            removeDayBtn.addEventListener('click', removeADay);
            submitBtn.addEventListener('click', submitOrder);
            saveBtn.addEventListener('click', checkCollection);
        }).catch(err => console.log(err))
    }

    // 渲染地圖
    function setMap(map) {
        const osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
        const osm = new L.TileLayer(osmUrl, {
            minZoom: 8,
            maxZoom: 19
        });
        map.addLayer(osm);
    }

    // 渲染地圖圖標
    function setMarker(map) {
        const marker = L.marker([lat, lng]);
        const circle = L.circle([lat, lng], {
            color: '#D9831A',
            fillColor: '#D9831A',
            fillOpacity: 0.5,
            radius: 100
        });
        marker.addTo(map);
        circle.addTo(map);
    }

    // 確認是否有被加為收藏
    function renderCollection() {
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
        setCalendar(dateNode, attendeeNode, startTimeNode, endTimeNode);
        setAttendee(attendeeNode);
    }

    // 設定日曆
    function setCalendar(dateNode, attendeeNode, startTimeNode, endTimeNode) {
        // 如果preOrderObj裡沒有值，日曆第一天為今天，若有值，第一天為前一次預訂 + 1天
        let calendarMinDate = dayjs().format(dateFormat);
        if ((cardIndex - 1) >= 0) {
            let dayAfter = dayjs(preOrderObj.DatesArr[cardIndex - 1]).add(1, 'day');
            calendarMinDate = dayAfter.format(dateFormat);
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
                // 開啟其他選項
                attendeeNode.removeAttribute('disabled');
                startTimeNode.removeAttribute('disabled');
                endTimeNode.removeAttribute('disabled');
                // 設定起始時間
                setStartTime(startTimeNode, endTimeNode);
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
    function setAttendee(attendeeNode) {
        attendeeNode.addEventListener('change', function (e) {
            const reg = /^[0-9]+(\.[0-9]{1,3})?$/;
            if (!reg.test(this.value)) {
                this.value = '';
                return;
            }

            let parentNode = this.parentNode.parentNode;
            let validateRow = parentNode.querySelector('.validate-row');
            // 只能輸入數字
            // 如果人數 > 50，數字顯示50
            if (this.value > capacity) {
                this.classList.add('not-validate');
                validateRow.classList.remove('d-none');

                submitBtn.setAttribute('disabled', '');
                extendDayBtn.setAttribute('disabled', '');
                return;
            } else {
                this.classList.remove('not-validate');
                validateRow.classList.add('d-none');

                submitBtn.removeAttribute('disabled');
                extendDayBtn.removeAttribute('disabled');
            };
            // 暫存參加人數
            attendee = this.value;
            let whichCard = e.target.parentNode.parentNode.classList[2];
            whichCardIndex = whichCard.split('-')[1];
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
                // 設定結束時間
                setEndTime(endTimeNode);
                endTimeNode.removeAttribute('disabled');
            },
            onClose: function (selectedDates, dateStr, instance) {
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
            },
            onClose: function (selectedDates, dateStr, instance) {
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
        const calculationNode = document.querySelector('.booking-card .calculation');
        const priceNode = calculationNode.querySelector('.venue-price');
        const totalHourNode = calculationNode.querySelector('.total-hour');
        const discountNode = calculationNode.querySelector('.discount');
        const discountRowNode = calculationNode.querySelector('.discount-row');
        const totalCostNode = calculationNode.querySelector('.total-price');

        calculationNode.classList = 'calculation';

        axios.post('/webapi/spaces/Calculate', preOrderObj).then(res => {
            let totalHour = res.data.Response.TotalHour;
            let totalPrice = res.data.Response.TotalPrice;
            // 渲染畫面
            // 如果超過指定時數則享有優惠
            if (totalHour >= hoursForDiscount) {
                discountRowNode.classList = 'discount-row d-flex mb-2'
            } else {
                discountRowNode.classList = 'discount-row d-none'
            }

            priceNode.innerText = `NT$${pricePerHour.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}`;
            totalHourNode.innerText = totalHour;
            discountNode.innerText = discount;
            totalCostNode.innerText = `NT$${totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}`;


        }).catch(err => console.log(err))
    }

    function extendADay() {
        cardIndex++;

        this.setAttribute('disabled', '');
        removeDayBtn.classList.remove('d-none');
        setCard();
    }

    function removeADay() {
        let latestCard = document.querySelector(`.index-${cardIndex}`);
        orderDetailNode.removeChild(latestCard);

        preOrderObj.DatesArr.length = cardIndex;
        preOrderObj.AttendeesArr.length = cardIndex;
        preOrderObj.StartTimeArr.length = cardIndex;
        preOrderObj.EndTimeArr.length = cardIndex;

        cardIndex--;
        if (cardIndex === 0) {
            this.classList.add('d-none');
            extendDayBtn.removeAttribute('disabled');
        }

        calculate(preOrderObj);
    }

    function checkLogin() {
        axios.get('/webapi/spaces/CheckLogin').then(res => {
            isLogin = res.data.Response;

            if (isLogin === false) {
                modal.show();
                sessionStorage.setItem('targetURL', location.href);
            };
        }).catch(err => console.log(err))
    }

    function submitOrder(e) {
        if (isLogin === false) {
            checkLogin();
        }
        else {
            axios.post('/webapi/spaces/AddPreOrder', preOrderObj).then(res => {
                if (res.data.Status === 'Success') {
                    Swal.fire(
                        '預約成功!',
                        '請於24小時內前往會員中心 > 我的訂單內申請付款',
                        'success'
                    );

                    document.querySelector('.swal2-confirm.swal2-styled').addEventListener('click', redirectToOrderCenter);
                }
            })
        }
    }

    function redirectToOrderCenter() {
        window.location = '/UserCenter/ShopCar';
        this.removeEventListener('click', redirectToOrderCenter);
    }

    function checkCollection() {
        checkLogin();

        let SpaceBriefVM = {
            SpaceID: spaceID,
        }

        if (isCollection === false) {
            addCollection(SpaceBriefVM);
        } else {
            removeCollection(SpaceBriefVM);
        }
    }

    function addCollection(SpaceBriefVM) {
        axios.post('/webapi/spaces/AddCollection', SpaceBriefVM)
            .then(res => {
                if (res.data.Status === 'Success') {
                    Swal.fire(
                        '收藏成功!',
                        '',
                        'success'
                    )
                    isCollection = true;
                    renderCollection();
                }
            }).catch(err => console.log(err))
    }

    function removeCollection(SpaceBriefVM) {
        axios.post('/webapi/spaces/RemoveCollection', SpaceBriefVM)
            .then(res => {
                if (res.data.Status === 'Success') {
                    Swal.fire(
                        '移除收藏成功!',
                        '',
                        'success'
                    )
                    isCollection = false;
                    renderCollection();
                }
            })
    }


    initialize();



})()