$(document).ready(function () {
  tinymce.init({
    selector: 'textarea',
    plugins: 'advlist autolink lists link image charmap print preview hr anchor pagebreak',
    toolbar_mode: 'floating',
  });
  $('#carousel-related-product').slick({
    infinite: true,
    vertical: false,
    autoplay: true,
    arrows: true,
    slidesToShow: 3,
    slidesToScroll: 3,
    dots: true,
  });
})