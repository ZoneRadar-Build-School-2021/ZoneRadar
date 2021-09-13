;(function () {
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
  });

  flatpickr.localize(flatpickr.l10ns.zh_tw);
  const dateFilterWeb = flatpickr("#web-date-filter", {
    altInput: true,
    altFormat: "Y/m/d",
    disableMobile: "true"
  });

  const dateFilterPhone = flatpickr("#phone-date-filter", {
    altInput: true,
    altFormat: "Y/m/d",
    disableMobile: "true"
  });
})()