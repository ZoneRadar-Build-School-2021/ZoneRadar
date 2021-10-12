;
(function () {
    // flatpickr中文化
    flatpickr.localize(flatpickr.l10ns.zh_tw);
    // 變數
    let index = 1;
    let operationStartArr = [];
    let operationEndArr = [];
    let operationDayArr = [];
    let attendeesList = [];
    let startDateTimeList = [];
    let endDateTimeList = [];
    // 節點
    const orderDetailNode = document.querySelector('.order-detail-select');
    const extendDayBtn = document.querySelector('.extend');
    const removeDayBtn = document.querySelector('.remove');
    const submitBtn = document.querySelector('.btn-submit');
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
    const map = L.map('map', {
        center: [25.041824011585646, 121.53629849747963],
        zoom: 17
    });

    // 執行區
    setMap();
    setMarker();
    setSelect();
    extendDayBtn.addEventListener('click', extendADay);
    removeDayBtn.addEventListener('click', removeADay);

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

    function setSelect() {
        const cloneNode = document.querySelector('#order-item-template').content.cloneNode(true);
        const dateNode = cloneNode.querySelector('.start-date');
        const attendeeNode = cloneNode.querySelector('.attendees');
        const startTimeNode = cloneNode.querySelector('.start-time');
        const endTimeNode = cloneNode.querySelector('.end-time');
        let selectedDate = '';
        let attendee = '';
        let startTime = '';
        let endTime = '';
        let validation = [];

        getOperating();
        setCalendar();
        setAttendee();
        setTime();
        cloneNode.querySelector('.order-item').classList.add(`day-${index}`);
        orderDetailNode.appendChild(cloneNode);
        submitBtn.addEventListener('click', submitOrder);

        // 把星期轉換成dayOfWeek，並且抓到營業日、開始時間和結束時間的陣列資料
        function getOperating() {
            const dayDict = {
                '星期日': 0,
                '星期一': 1,
                '星期二': 2,
                '星期三': 3,
                '星期四': 4,
                '星期五': 5,
                '星期六': 6,
            }

            operationStartTime.forEach(item => {
                let hour = item.Hours;
                operationStartArr.push(hour)
            })
            operationEndTime.forEach(item => {
                let hour = item.Hours;
                operationEndArr.push(hour)
            })
            operationDay.forEach(item => {
                let day = dayDict[item];
                operationDayArr.push(day);
            })
        }

        function setCalendar() {
            flatpickr(dateNode, {
                altInput: true,
                altFormat: 'Y/m/d',
                disableMobile: 'true',
                minDate: "today",
                maxDate: new Date().fp_incr(90),
                // change事件監聽
                onChange: function (selectedDates, dateStr, instance) {
                    selectedDate = dateStr;
                    validation = [selectedDate, attendee, startTime, endTime];
                    checkValidation(validation);
                    console.log(selectedDate)
                },
            });
        }

        function setAttendee() {
            attendeeNode.addEventListener('change', function () {
                attendee = this.value;
                validation = [selectedDate, attendee, startTime, endTime];
                checkValidation(validation);
            })

            attendeeNode.addEventListener('keyup', function () {
                this.value = this.value.replace(/[^\d]/g, '');
            })
        }

        function setTime() {
            flatpickr(startTimeNode, {
                enableTime: true,
                noCalendar: true,
                dateFormat: "H:i",
                time_24hr: true,
                minTime: "16:00",
                maxTime: "22:30",
                disableMobile: "true",
                onChange: function (selectedDates, dateStr, instance) {
                    startTime = dateStr;
                    validation = [selectedDate, attendee, startTime, endTime];
                    checkValidation(validation);
                    console.log(startTime)
                },
            });

            flatpickr(endTimeNode, {
                enableTime: true,
                noCalendar: true,
                dateFormat: "H:i",
                time_24hr: true,
                minTime: "16:00",
                maxTime: "22:30",
                disableMobile: "true",
                onChange: function (selectedDates, dateStr, instance) {
                    endTime = dateStr;
                    validation = [selectedDate, attendee, startTime, endTime];
                    checkValidation(validation);
                    console.log(endTime)
                },
            });

            document.querySelectorAll('.numInput').forEach(node => {
                node.setAttribute('readonly', '');
            })

            document.querySelectorAll('.flatpickr-minute').forEach(node => {
                node.setAttribute('step', '30');
            })
        }

        // 確認欄位是否都有填寫，若有則開啟"再一天"按鈕
        function checkValidation(validation) {
            let trueArr = [];
            validation.forEach(item => {
                if (item) {
                    trueArr.push(true);
                }
            })
            if (trueArr.length === 4) {
                extendDayBtn.removeAttribute('disabled');

                submitBtn.removeAttribute('disabled');
            }
        }
    }

    function extendADay() {
        index++;

        this.setAttribute('disabled', '');
        removeDayBtn.classList.remove('d-none');
        setSelect();
    }

    function removeADay() {
        let latest = document.querySelector(`.day-${index}`);
        orderDetailNode.removeChild(latest);

        attendeesList.length = index - 1;
        startDateTimeList.length = index - 1;
        endDateTimeList.length = index - 1;

        index--;
        if (index === 1) {
            this.classList.add('d-none');
            extendDayBtn.removeAttribute('disabled');
        }
    }

    function submitOrder() {
        let dateList = [];
        let startList = [];
        let endList = [];
        let attendeeList = [];
        document.querySelectorAll('.start-date.flatpickr-input').forEach((dateNode, index) => {
            dateList.push(dateNode.value);
        })
        // 抓取參加人數陣列
        document.querySelectorAll('.attendees').forEach(attendeeNode => {
            attendeeList.push(attendeeNode.value);
        })
        document.querySelectorAll('.start-time').forEach(startTimeNode => {
            startList.push(startTimeNode.value);
        })
        document.querySelectorAll('.end-time').forEach(endTimeNode => {
            endList.push(endTimeNode.value);
        })

        // 抓取開始DateTime和結束DateTime陣列
        dateList.forEach((date, index) => {
            startDateTimeList.push(new Date(`${date}T${startList[index]}:00`));
            endDateTimeList.push(new Date(`${date}T${endList[index]}:00`));
        })

        Swal.fire(
            '預約成功!',
            '請於24小時內前往會員中心 > 我的訂單內申請付款',
            'success'
        )
    }

})()