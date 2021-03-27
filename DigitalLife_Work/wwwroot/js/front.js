/*global $, document, window, jQuery, changeColOrder*/
$(document).ready(function () {

    'use strict';

    // ------------------------------------------------------- //
    // initialization dropdown
    // ------------------------------------------------------ //
    $('.dropdown-toggle').dropdown();


    // ------------------------------------------------------- //
    // Make a sticky navbar on scrolling
    // ------------------------------------------------------ //
    $(window).scroll(function () {

        function makeItFixed(x) {

            var body = $('body'),
                navbar = $('nav.navbar');

            if ($(window).scrollTop() >= x) {
                navbar.addClass('fixed-top');
                body.css('padding-top', $('nav.navbar').outerHeight());
            } else {
                navbar.removeClass('fixed-top');
                body.css('padding-top', '0');
            }
        }

        makeItFixed($('.top-bar').outerHeight());
    });


    // ------------------------------------------------------- //
    // shipping form validation
    // ------------------------------------------------------ //
    $('#shipping-address-form').validate({
        messages: {
            firstname: 'please enter your first name',
            lastname: 'please enter your last name',
            email: 'please enter your email address',
            number: 'please enter your phone number',
            address: 'please enter your address',
            city: 'please enter your city',
            country: 'please enter your country',
            postalcode: 'please enter your postal code',
            region: 'please enter your region',
            sfirstname: 'please enter your first name',
            slastname: 'please enter your last name',
            semail: 'please enter your email address',
            snumber: 'please enter your phone number',
            saddress: 'please enter your address',
            scity: 'please enter your city',
            scountry: 'please enter your country',
            spostalcode: 'please enter your postal code',
            sregion: 'please enter your region',
            cardname: 'please enter your card name',
            cardnumber: 'please enter your card number',
            expirymonth: 'please enter expiry month',
            expiryyear: 'please enter expiry year',
            cvv: 'please enter your card CVV number'
        },
        rules: {
            country: {
                selectcheck: true
            }
        }
    });

    jQuery.validator.addMethod('selectcheck', function (value) {
        return (value !== '0');
    });


    // ------------------------------------------------------- //
    // Alternative form show/hide
    // ------------------------------------------------------ //
    $("#another-address").change(function () {

        var alternativeShipping = $('.shipping-alternative');

        if (this.checked) {
            alternativeShipping.show();
        } else {
            alternativeShipping.hide();
        }
    });

    // ------------------------------------------------------- //
    // Multilevel dropdown
    // ------------------------------------------------------ //

    $('ul.dropdown-menu [data-toggle=dropdown]').on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();

        var parent = $(this).parent();
        parent.siblings().removeClass('show');
        parent.toggleClass('show');
    });


    // ------------------------------------------------------- //
    // Increase/Reduce product amount
    // ------------------------------------------------------ //
    $('.minus-btn').on('click', function () {

        var siblings = $(this).siblings('input.quantity');

        if (parseInt(siblings.val(), 10) >= 1) {
            siblings.val(parseInt(siblings.val(), 10) - 1);
        }
    });

    $('.plus-btn').on('click', function () {

        var siblings = $(this).siblings('input.quantity');

        siblings.val(parseInt(siblings.val(), 10) + 1);
    });


    // ------------------------------------------------------- //
    // Items Carousel
    // ------------------------------------------------------ //
    $('.item-slider').owlCarousel({
        loop: true,
        items: 1,
        thumbs: true,
        thumbsPrerendered: true,
        dots: true,
        responsiveClass: false
    });


    // ------------------------------------------------------- //
    // Hero 1 Carousel
    // ------------------------------------------------------ //
    $('.hero-1-slider').owlCarousel({
        loop: true,
        items: 1,
        dots: true,
        autoplaySpeed: 1000,
        dotsSpeed: 1000,
        navText: [
            "<i class='fa fa-angle-left'></i>",
            "<i class='fa fa-angle-right'></i>"
        ],
        responsiveClass: false
    });


    // ------------------------------------------------------- //
    // Hweo 2 Carousel
    // ------------------------------------------------------ //
    $('.hero-2-slider').owlCarousel({
        loop: true,
        items: 1,
        nav: true,
        dots: false,
        autoplaySpeed: 1000,
        navSpeed: 1000,
        navText: [
            "<i class='fa fa-angle-left'></i>",
            "<i class='fa fa-angle-right'></i>"
        ],
        responsiveClass: false
    });

    // ------------------------------------------------------- //
    // Search panel open/close
    // ------------------------------------------------------ //
    $('.search-close').on('click', function () {
        $('.search-overlay').fadeOut();
    });
    $('#search').on('click', function (e) {
        e.preventDefault();
        $('.search-overlay').fadeIn();
        $('.navbar-collapse').removeClass('show');
    });


    // ------------------------------------------------------- //
    // Change columns order
    // ------------------------------------------------------ //
    $(window).on('resize', function () {
        changeColOrder();
    });

    function changeColOrder() {
        if ($(window).outerWidth() < 977) {
            $('.js-pull').addClass('flex-first');
        } else {
            $('.js-pull').removeClass('flex-first');
        }
    }
    changeColOrder();

    // ------------------------------------------------------- //
    // Google Maps
    // ------------------------------------------------------ //
    if ($('#map').length > 0) {


        function initMap() {

            var location = new google.maps.LatLng(50.0875726, 14.4189987);

            var mapCanvas = document.getElementById('map');
            var mapOptions = {
                center: location,
                zoom: 16,
                panControl: false,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            var map = new google.maps.Map(mapCanvas, mapOptions);

            var markerImage = 'img/marker.png';

            var marker = new google.maps.Marker({
                position: location,
                map: map,
                icon: markerImage
            });

            var contentString = '<div class="info-window">' +
                '<h3>Info Window Content</h3>' +
                '<div class="info-content">' +
                '<p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vestibulum tortor quam, feugiat vitae, ultricies eget, tempor sit amet, ante. Donec eu libero sit amet quam egestas semper. Aenean ultricies mi vitae est. Mauris placerat eleifend leo.</p>' +
                '</div>' +
                '</div>';

            var infowindow = new google.maps.InfoWindow({
                content: contentString,
                maxWidth: 400
            });

            marker.addListener('click', function () {
                infowindow.open(map, marker);
            });

            var styles = [{ "featureType": "landscape", "stylers": [{ "saturation": -100 }, { "lightness": 65 }, { "visibility": "on" }] }, { "featureType": "poi", "stylers": [{ "saturation": -100 }, { "lightness": 51 }, { "visibility": "simplified" }] }, { "featureType": "road.highway", "stylers": [{ "saturation": -100 }, { "visibility": "simplified" }] }, { "featureType": "road.arterial", "stylers": [{ "saturation": -100 }, { "lightness": 30 }, { "visibility": "on" }] }, { "featureType": "road.local", "stylers": [{ "saturation": -100 }, { "lightness": 40 }, { "visibility": "on" }] }, { "featureType": "transit", "stylers": [{ "saturation": -100 }, { "visibility": "simplified" }] }, { "featureType": "administrative.province", "stylers": [{ "visibility": "off" }] }, { "featureType": "water", "elementType": "labels", "stylers": [{ "visibility": "on" }, { "lightness": -25 }, { "saturation": -100 }] }, { "featureType": "water", "elementType": "geometry", "stylers": [{ "hue": "#ffff00" }, { "lightness": -25 }, { "saturation": -97 }] }];

            map.set('styles', styles);


        }

        google.maps.event.addDomListener(window, 'load', initMap);


    }

    // ------------------------------------------------------ //
    // For demo purposes, can be deleted
    // ------------------------------------------------------ //

    var stylesheet = $('link#theme-stylesheet');
    $( "<link id='new-stylesheet' rel='stylesheet'>" ).insertAfter(stylesheet);
    var alternateColour = $('link#new-stylesheet');

    if ($.cookie("theme_csspath")) {
        alternateColour.attr("href", $.cookie("theme_csspath"));
    }

    $("#colour").change(function () {

        if ($(this).val() !== '') {

            var theme_csspath = 'css/style.' + $(this).val() + '.css';

            alternateColour.attr("href", theme_csspath);

            $.cookie("theme_csspath", theme_csspath, { expires: 365, path: document.URL.substr(0, document.URL.lastIndexOf('/')) });

        }

        return false;
    });

});

////-----------------------------------------------///
$(document).ready(function () {
    $('.btn-filter').on('click', function () {
        var $target = $(this).data('target');
        if ($target != 'all') {
            $('.table tbody tr').css('display', 'none');
            $('.table tr[data-status="' + $target + '"]').fadeIn('slow');
        } else {
            $('.table tbody tr').css('display', 'none').fadeIn('slow');
        }
    });

    $('#checkall').on('click', function () {
        if ($("#mytable #checkall").is(':checked')) {
            $("#mytable input[type=checkbox]").each(function () {
                $(this).prop("checked", true);
            });

        } else {
            $("#mytable input[type=checkbox]").each(function () {
                $(this).prop("checked", false);
            });
        }
    });
});
