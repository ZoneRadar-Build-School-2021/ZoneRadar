;
(function () {
  // 照片牆
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


  // 日期選擇器
  flatpickr.localize(flatpickr.l10ns.zh_tw);
  const startDatePicker = flatpickr("#start-date", {
    altInput: true,
    enableTime: true,
    time_24hr: true,
    altFormat: "Y/m/d  H:i",
    minuteIncrement: 30,
    disableMobile: "true"
  });
  const endDatePicker = flatpickr("#end-date", {
    altInput: true,
    enableTime: true,
    time_24hr: true,
    altFormat: "Y/m/d  H:i",
    minuteIncrement: 30,
    disableMobile: "true"
  });

  document.querySelectorAll('.numInput.flatpickr-hour').forEach(item => item.setAttribute('readonly', ''));
  document.querySelectorAll('.numInput.flatpickr-minute').forEach(item => item.setAttribute('readonly', ''));


  // 人數輸入
  document.querySelector('.attendees').addEventListener('keyup', function() {
    this.value = this.value.replace(/[^\d]/g, '');
  })


  // 送出鍵
  document.querySelector('.btn-submit').addEventListener('click', function() {
    Swal.fire(
      '預約成功!',
      '請於24小時內前往會員中心 > 我的訂單內申請付款',
      'success'
    )
  })


  // 地圖
  const map = L.map('map', {
    center: [25.041824011585646, 121.53629849747963],
    zoom: 17
  });

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

  // window onload
  window.addEventListener('load', function () {
    setMap();
    setMarker();
  })







})()