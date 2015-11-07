jQuery(document).ready(function ($) {
    detectView();
    showSearch();
    showMenu();
    formLookup();

    var owl = $('.owl-carousel-about');
    owl.owlCarousel({
        loop: true,
        nav: true,
        dots: true,
        navText: ["", ""],
        rewindNav:false,
        items: 1
        
    });


    var owl = $('.owl-carousel-jobs');
    owl.owlCarousel({
        loop: true,
        nav: true,
        navText: ["", ""],
        items: 1,
        responsive:{
        0:{
            items:1
        },
        768:{
            items:3
        },
        1025:{
            items:3
        }
    }
    });
    scroll();
});


function scroll() {

    $('.scrollto').click('click', function (e) {
        e.preventDefault();

        $('html, body').animate({
            scrollTop: ($('.title-features').offset().top)
        }, 1000);
    });
}



function showSearch() {
    
    $('.search a').on('click', function (e) {
        e.preventDefault();
        $('.search .search-input').fadeToggle();
    });

    $(document).on('click', function (e) {
        if ($(e.target).parents('.search').length == 0) {
            $('.search .search-input').fadeOut();
        }
    })
}
function showMenu() {
    
    $('.menu span').click('click', function (e) {
        e.preventDefault();
        $('.menu ul').slideToggle();
    });
}




function formLookup() {
    $('.form').find('input, textarea').on('keyup blur focus', function (e) {

        var $this = $(this),
          label = $this.prev('label');

        if (e.type === 'keyup') {
            if ($this.val() === '') {
                label.removeClass('active highlight');
            } else {
                label.addClass('active highlight');
            }
        } else if (e.type === 'blur') {
            if ($this.val() === '') {
                label.removeClass('active highlight');
            } else {
                label.removeClass('highlight');
            }
        } else if (e.type === 'focus') {

            if ($this.val() === '') {
                label.removeClass('highlight');
            } else if ($this.val() !== '') {
                label.addClass('highlight');
            }
        }

    });

    $('.tab a').on('click', function (e) {

        e.preventDefault();

        $(this).parent().addClass('active');
        $(this).parent().siblings().removeClass('active');

        target = $(this).attr('href');

        $('.tab-content > div').not(target).hide();

        $(target).fadeIn(600);

    });

    $('#registerO').click(function () {
        $('#overlay').css('display', 'inline-block');
        $('.background-black').css('display', 'initial');
        $('.signup').addClass('active');
        $('.login').removeClass('active');
        $('#signup').css('display', 'initial');
        $('#login').css('display', 'none');
        $('body').addClass('noscroll');
    })


    $('#loginO').click(function () {
        $('#overlay').css('display', 'initial');
        $('.background-black').css('display', 'initial');
        $('.login').addClass('active');
        $('.signup').removeClass('active');
        $('#login').css('display', 'initial');
        $('#signup').css('display', 'none');
        $('body').addClass('noscroll');
    })

    $(document).mouseup(function (e) {
        var container = $("#overlay");

        if (!container.is(e.target) // if the target of the click isn't the container...
            && container.has(e.target).length === 0) // ... nor a descendant of the container
        {
            container.hide();
            $('.background-black').css('display', 'none');
            $('body').removeClass('noscroll');
        }
        
    });
    $(document).keydown(function (e) {
        // ESCAPE key pressed
        var container = $("#overlay");
        if (e.keyCode == 27) {
            container.hide();
            $('.background-black').css('display', 'none');
            $('body').removeClass('noscroll');
        }
    });
   
}