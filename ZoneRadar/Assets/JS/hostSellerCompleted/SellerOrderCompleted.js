flatpickr( ".choose-date",
{
    dateFormat: "Y-m-d",
    disableMobile: "true",
    mode: "range",
    locale: "zh_tw",
    onClose: function(selectedDates, dateStr, instance) {
        console.log(dateStr);
    }
});
