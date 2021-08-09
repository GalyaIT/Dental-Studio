const navbarToggler=document.querySelector(".navbar-toggler");
const navbarMenu=document.querySelector(".navbar ul");
const navbarLinks=document.querySelectorAll(".navbar a")

navbarToggler.addEventListener("click",navbarTogglerClick);

function navbarTogglerClick(){
    navbarToggler.classList.toggle("open-navbar-toggler");
    navbarMenu.classList.toggle("open");
}

navbarLinks.forEach(elem=>elem.addEventListener("click",navbarLinkClick));

function navbarLinkClick(){
    if(navbarMenu.classList.contains("open")){
        navbarToggler.click();
    }
}




function confirmDelete(uniqueId, isTrue) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isTrue) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}



