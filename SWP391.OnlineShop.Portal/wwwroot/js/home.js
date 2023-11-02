//hamburger button
$(document).ready(function () {
    //li hover
    $(".li1").hover(function () {
        $(".hr-nav1").addClass("active");
    },
        function () {
            $(".hr-nav1").removeClass("active");
        }
    );

    $(".li2").hover(function () {
        $(".hr-nav2").addClass("active");
    },
        function () {
            $(".hr-nav2").removeClass("active");
        }
    );
    $(".li3").hover(function () {
        $(".hr-nav3").addClass("active");
    },
        function () {
            $(".hr-nav3").removeClass("active");
        }
    );
    $(".li4").hover(function () {
        $(".hr-nav4").addClass("active");
    },
        function () {
            $(".hr-nav4").removeClass("active");
        }
    );
    $(".li5").hover(function () {
        $(".hr-nav5").addClass("active");
    },
        function () {
            $(".hr-nav5").removeClass("active");
        }
    );
    $(".li6").hover(function () {
        $(".hr-nav6").addClass("active");
    },
        function () {
            $(".hr-nav6").removeClass("active");
        }
    );


    //check cate-mobile
    $(".li2").click(function () {
        console.log("li 2 clicked");
        let cateM = document.getElementById("cate-Mobile");
        cateM.style.transform = "translateX(0)";
    });

    $(".cate-mobile-header").click(function () {
        console.log("cate-mobi clicked");
        let cateM = document.getElementById("cate-Mobile");
        cateM.style.transform = "translateX(101%)";
    });


    //overlay click
    $("#overlay").click(function () {
        let button = document.getElementById("button-nav");
        button.style.background = '#252525';
        $("#button-nav").removeClass("is-active");
    });

    /*trailer*/
    $(".play-video").click(function () {
        $(".trailer").addClass('active');
    });

    $(".close-trailer").click(function () {
        $(".trailer").removeClass('active');
    });

    //contact form
    const inputs = document.querySelectorAll(".input");

    function focusFunc() {
        let parent = this.parentNode;
        parent.classList.add("focus");
    }
    function blurFunc() {
        let parent = this.parentNode;
        if (this.value == "") {
            parent.classList.remove("focus");
        }
    }
    inputs.forEach(input => {
        input.addEventListener("focus", focusFunc);
        input.addEventListener("blur", blurFunc);
    });


    //profile
    $(".secu-discard").click(function() {
        $("input[type='password']").val('');
    });
});

