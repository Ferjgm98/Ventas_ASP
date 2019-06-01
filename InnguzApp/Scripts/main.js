var burguer = document.querySelector('.hamburger');
var menuWrapper = document.querySelector('.nav-container');
var menuItems = document.querySelectorAll('.menu-item a');
var menuItemsArray = Array.prototype.slice.call(menuItems);
var urlArr = window.location.href.split('/');
var urlArgument = urlArr[urlArr.length - 1];

window.addEventListener('scroll', function () {
    if (window.scrollY >= 5 && window.innerWidth < 768) {
        menuWrapper.classList.remove('fixed');
        menuWrapper.classList.add('fixed');
    } else {
        menuWrapper.classList.remove('fixed');
    }
})

if (window.innerWidth >= 768) {
    menuWrapper.classList.remove('fixed');
}

burguer.addEventListener('click', function () {
    burguer.classList.toggle('is-active');
    var mobileMenu = document.querySelector('.mobile-nav');
    mobileMenu.classList.toggle('active');

    mobileMenu.classList.contains('active') ? menuWrapper.classList.add('fixed') : menuWrapper.classList.remove('fixed');
})

menuItemsArray.forEach(function (item, index) {
    var itemUrl = item.href.split('/');
    var itemUrlArg = itemUrl[itemUrl.length - 1];
    if (itemUrlArg === urlArgument) {
        item.classList.add('active');
    }
});



// Actualizar foto

var photo = document.getElementById('photo');
photo.addEventListener('change', function (e) {
    var view = document.querySelector('.photo-view');

    view.src = URL.createObjectURL(photo.files[0]);
})