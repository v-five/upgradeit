﻿jQuery(document).ready(function ($) {
    detectView();
    showSearch();
    showMenu();
    formLookup();
    CreateProjectLoggedin();
    loginFix();

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

    $("#introdu").change(function () {
        readURL(this);
    });
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

$('.search .search-input').keyup(function () {
    var searchField = $(this).val();

    if (searchField !== "")
    {
        var myExp = new RegExp(searchField, "i");
        $.ajax({
            type: "GET",
            url: '/project-search?keyword=' + searchField,
            crossDomain: true,
        }).done(function (data) {
            var output = '<ul class="searchresults">';
            $.each(data, function (key, val) {
                if ((val.Title.search(myExp) != -1)) {
                    output += '<li>';
                    output += '<h2>' + val.Title + '</h2>';
                    output += '<img src="' + val.ImageUrl + '" alt="' + val.Title + '" />';
                    output += '<p>' + val.Description + '</p>';
                    output += '</li>';
                }
            });
            output += '</ul>';
            $('.search-results').html(output);
        });
    }
    else
    {
        $('.search-results').html("");
    }
    
});


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#replace').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

function CreateProjectLoggedin() {

    $('#create-project').click(function(event){
        if ($("#create-project").hasClass("not-loggedin")) {
            event.preventDefault();
            $('#overlay').css('display', 'initial');
            $('.background-black').css('display', 'initial');
            $('.login').addClass('active');
            $('.signup').removeClass('active');
            $('#login').css('display', 'initial');
            $('#signup').css('display', 'none');
            $('body').addClass('noscroll');
        }
        else {
            //Do the same as before
        }
    });
}


function loginFix() {

    //Login
    if ($('#Username').val() === "") {
        $('#Username').prev('label').removeClass('active highlight');
    }
    else {
        $('#Username').prev('label').addClass('active highlight');
    }

    if ($('#login #Password').val() === "") {
        $('#login #Password').prev('label').removeClass('active highlight');
    }
    else {
        $('#login #Password').prev('label').addClass('active highlight');
    }
    //Register
    if ($('#Email').val() === "") {
        $('#Email').prev('label').removeClass('active highlight');
    }
    else {
        $('#Email').prev('label').addClass('active highlight');
    }

    if ($('#signup #Password').val() === "") {
        $('#signup #Password').prev('label').removeClass('active highlight');
    }
    else {
        $('#signup #Password').prev('label').addClass('active highlight');
    }

    if ($('#ConfirmPassword').val() === "") {
        $('#ConfirmPassword').prev('label').removeClass('active highlight');
    }
    else {
        $('#ConfirmPassword').prev('label').addClass('active highlight');
    }


}