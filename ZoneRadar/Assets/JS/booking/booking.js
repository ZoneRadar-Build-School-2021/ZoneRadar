;
(function () {
  // Swiper
    const swiper = new Swiper('.venue-img-group.swiper', {
    // Optional parameters
    loop: true,

    // If we need pagination
    pagination: {
      el: '.swiper-pagination',
      clickable: true,
    },

    // Navigation arrows
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },

    autoplay: {
      delay: 4000,
      disableOnInteraction: false
    },
  });

  // Flatepickr
  flatpickr.localize(flatpickr.l10ns.zh_tw);
  const chooseDatePicker = flatpickr("#date", {
    altInput: true,
    altFormat: "F j, Y",
    dateFormat: "Y-m-d",
    disableMobile: "true"
  });
  const startTimePicker = flatpickr("#start-time", {
    altInput: true,
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    time_24hr: true,
    disableMobile: "true"
  });
  const endTimePicker = flatpickr("#end-time", {
    altInput: true,
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    time_24hr: true,
    disableMobile: "true"
  });


  const map = L.map('map', {
    center: [25.041824011585646, 121.53629849747963],
    zoom: 17
  });

  // window onload
  window.addEventListener('load', function () {
    setMap();
    setMarker();
  })

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











})()